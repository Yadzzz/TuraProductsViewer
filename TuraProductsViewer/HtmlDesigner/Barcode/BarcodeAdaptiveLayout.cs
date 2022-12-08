using Microsoft.AspNetCore.Html;
using SelectPdf;
using System.Drawing;
using System.Text;
using TuraProductsViewer.Services;

namespace TuraProductsViewer.HtmlDesigner.Barcode
{
    public class BarcodeAdaptiveLayout
    {
        private StringBuilder stringBuilder { get; set; }
        private CreatorService creatorService { get; set; }
        private List<PdfDocument> pdfDocuments { get; set; }


        public BarcodeAdaptiveLayout(CreatorService crtService)
        {
            this.stringBuilder = new();
            this.creatorService = crtService;
            this.pdfDocuments = new List<PdfDocument>();

            this.Initialize();
        }

        private void Initialize()
        {
            int interval = 0;
            int productsInterval = 0;
            int newPageInterval = 0;
            int pdfProductsPageInterval = 0;
            int productsAdded = 0;
            foreach (var product in creatorService.GetProducts())
            {
                string html = string.Empty;

                if (pdfProductsPageInterval == 0)
                {
                    stringBuilder.AppendLine("<html><body style=\"width:800px; margin-left:25px; margin-right:auto; margin-top:0px; \">");
                }

                if (newPageInterval == 40)
                {
                    html += "<div style=\"page-break-after: always\">\r\n\"\r\n</div>";
                    newPageInterval = 0;
                }

                if (interval == 0)
                {
                    html += "<div style=\"padding-top: 30px; padding-bottom: 100px;\">";
                }

                html += "<div style=\"width:150px; height:35px; float:left; text-align: center; padding-right:5px;\">";
                html += "<h1 style=\"display:inline\">" + this.creatorService.FinalizePrice(product) + "</h1>";
                html += "<img style=\"max-width: 100%; max-height:100%;\" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" />"; //width:120px; height:45px;
                html += "<p style=\"display:inline\">Tura: " + product.VariantId + "</p>";
                html += "</div>";

                html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 150, 40, true, 5));

                interval++;
                productsInterval++;
                newPageInterval++;
                pdfProductsPageInterval++;
                productsAdded++;

                if (interval == 5 || productsInterval == this.creatorService.GetProductsCount())
                {
                    html += "</div>";
                    interval = 0;
                }

                stringBuilder.AppendLine(html);

                if (pdfProductsPageInterval == 200 || this.creatorService.GetProductsCount() == productsAdded)
                {
                    stringBuilder.AppendLine("</body></html>");
                    this.AppendPDFPage(stringBuilder.ToString());
                    this.stringBuilder.Clear();
                    pdfProductsPageInterval = 0;
                    newPageInterval = 0;
                }
            }
        }

        private void AppendPDFPage(string html)
        {
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.NoAdjustment;

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(html);

            doc.CompressionLevel = PdfCompressionLevel.Best;

            this.pdfDocuments.Add(doc);
        }

        public MemoryStream GetMemoryStream()
        {
            PdfDocument doc = new PdfDocument();

            foreach (var document in this.pdfDocuments)
            {
                doc.Append(document);
            }

            doc.CompressionLevel = PdfCompressionLevel.Best;

            using (MemoryStream stream = new MemoryStream())
            {
                doc.Save(stream);
                Console.WriteLine("STREAM: " + stream.Length);
                doc.Close();

                foreach (var document in this.pdfDocuments)
                {
                    document.Close();
                }

                return stream;
            }
        }
    }
}
