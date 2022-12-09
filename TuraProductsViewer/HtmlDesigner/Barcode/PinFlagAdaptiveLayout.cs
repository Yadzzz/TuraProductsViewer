using SelectPdf;
using System.Drawing;
using System.Text;
using TuraProductsViewer.Services;

namespace TuraProductsViewer.HtmlDesigner.Barcode
{
    public class PinFlagAdaptiveLayout
    {
        private StringBuilder stringBuilder { get; set; }
        private CreatorService creatorService { get; set; }
        private List<PdfDocument> pdfDocuments { get; set; }

        public PinFlagAdaptiveLayout(CreatorService crtService)
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
                    stringBuilder.AppendLine("<html><body style=\"width:800px; margin-left:27px; margin-right:auto; margin-top:57px; \">");
                }

                if (newPageInterval == 24)
                {
                    html += "<div style=\"page-break-after: always\">.</div>";
                    html += "<div style=\"padding-top:57px;\"></div>";
                    newPageInterval = 0;
                }

                if (interval == 0)
                {
                    html += "<div style=\"padding-bottom: 128px;\">";
                }

                html += "<div style=\"width:170px; height:100px; float:left; text-align: center; padding-right:40px; padding-left:40px; padding-bottom:8px; \">";
                html += "<img style=\"max-width: 100%; max-height:100%;\" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" />"; //width:120px; hiehgt:45px;
                //html += "<p style=\"display:inline\"><b>" + product.PrimaryEANCode + "</b></p>";
                html += "<br /><div style=\"padding-left:0px; height:22px; overflow: hidden; font-size:10px; \">" + product.GetItemName(this.creatorService.Language) + "</div>";
                html += "<p style=\" font: bold 13px verdana;\">Art.Nr: " + product.VariantId + "</p>";
                html += "<p style=\" display:inline; font: 8px verdana;\">........................................................</p>";
                html += "</div>";

                html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 236, 50, true, 5));

                interval++;
                productsInterval++;
                newPageInterval++;
                pdfProductsPageInterval++;
                productsAdded++;

                if (interval == 3 || productsInterval == this.creatorService.GetProductsCount())
                {
                    html += "</div>";
                    interval = 0;
                }

                stringBuilder.AppendLine(html);

                if (pdfProductsPageInterval == 120 || this.creatorService.GetProductsCount() == productsAdded)
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
