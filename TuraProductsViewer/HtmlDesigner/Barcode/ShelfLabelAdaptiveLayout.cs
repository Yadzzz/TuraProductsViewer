using SelectPdf;
using Serilog;
using System.Drawing;
using System.Text;
using TuraProductsViewer.Services;

namespace TuraProductsViewer.HtmlDesigner.Barcode
{
    public class ShelfLabelAdaptiveLayout
    {
        private Microsoft.Extensions.Logging.ILogger logger { get; set; }
        private StringBuilder stringBuilder { get; set; }
        private CreatorService creatorService { get; set; }
        private List<PdfDocument> pdfDocuments { get; set; }

        public ShelfLabelAdaptiveLayout(CreatorService crtService)
        {
            this.stringBuilder = new();
            this.creatorService = crtService;
            this.pdfDocuments = new List<PdfDocument>();

            //Temporary logger
            var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog());
            var logger = loggerFactory.CreateLogger(string.Empty);
            this.logger = logger;

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
                    stringBuilder.AppendLine("<html><body style=\"width:1200px; margin-left:33px; margin-right:auto; margin-top:42px; \">");
                }

                if (newPageInterval == 33)
                {
                    html += "<div style=\"page-break-after: always\">.</div>";
                    html += "<div style=\"padding-top:42px;\"></div>";
                    newPageInterval = 0;
                }

                if (interval == 0)
                {
                    html += "<div style=\"padding-top: 0px; padding-bottom: 0px; margin: 0 auto;  overflow: hidden; \" class=\"clearfix\">";
                }

                html += "<div class=\"clearfix\" style=\"margin: 0 auto; width:92.5px; height:241px; overflow: hidden; float:left; text-align: center; border-width: 1px 1px 1px 1px;  border-style: dotted;\">";
                html += "<br /><br /><div class=\"clearfix\" style=\"height:45px; \"><img style=\"max-width:100%; max-height:100%; transform:rotate(270deg); \" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" /></div>";
                html += "<br /><hr style=\"border-top: dotted 1px;\" /><div class=\"clearfix\" style=\"padding-left:3px; height:40px; overflow: hidden; font-size:11px; \">" + product.GetItemName(this.creatorService.Language) + "</div>";
                html += "<div style=\"height:10px;\"><p style=\" font: 8px sans-serif;\">" + product.VariantId + "</p></div>";
                html += "<hr style=\"border-top: dotted 1px;\" /><div class=\"clearfix\" style=\" font-size:20px; height 20px; font-weight: bold;\">" + this.creatorService.FinalizePrice(product) + "</div>";
                html += "</div>";

                if (this.creatorService.ShowEANCode)
                {
                    html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 150, 70, true, 3));
                }
                else
                {
                    html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 150, 60, false, 4));
                }
                interval++;
                productsInterval++;
                newPageInterval++;
                pdfProductsPageInterval++;
                productsAdded++;

                if (interval == 11 || productsInterval == this.creatorService.GetProductsCount())
                {
                    html += "</div>";
                    interval = 0;
                }

                stringBuilder.AppendLine(html);

                if (pdfProductsPageInterval == 165 || this.creatorService.GetProductsCount() == productsAdded)
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
            try
            {
                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.PdfPageSize = PdfPageSize.A4;
                converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;

                converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.NoAdjustment;

                PdfDocument doc = converter.ConvertHtmlString(html);
                doc.CompressionLevel = PdfCompressionLevel.Best;

                this.pdfDocuments.Add(doc);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
            }
        }

        public MemoryStream GetMemoryStream()
        {
            try
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
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
