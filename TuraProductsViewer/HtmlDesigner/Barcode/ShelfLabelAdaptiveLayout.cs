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
                    stringBuilder.AppendLine("<html><body style=\"width:900px; margin-left:0px; margin-right:auto; margin-top:0px; \">");
                }

                if (newPageInterval == 18)
                {
                    html += "<div style=\"page-break-after: always\">.</div>";
                    newPageInterval = 0;
                }

                if (interval == 0)
                {
                    html += "<div style=\"padding-top: 5px; padding-bottom: 0px; margin: 0 auto;  overflow: hidden; \" class=\"clearfix\">";
                }

                html += "<div class=\"clearfix\" style=\"margin: 0 auto; width:120px; height:320px; overflow: hidden; float:left; text-align: center; padding-right:10px; padding-left;0px; border-width: 1px 1px 1px 1px;  border-style: dotted;\">";
                //html += "<img style=\"max-width: 100%; max-height:100%; transform:rotate(270deg);\" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" />";
                html += "<br /><br /><br /><div class=\"clearfix\"><img style=\"max-width:auto; max-height:auto; transform:rotate(270deg); \" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" /></div>";
                //html += "<img style=\"max-width:auto; max-height:auto; transform:rotate(270deg); \" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" />";
                html += "<br /><br /><br /><hr style=\"border-top: dotted 1px;\" /><div class=\"clearfix\" style=\"padding-left:3px; height:45px; overflow: hidden; font-size:11px; \">" + product.GetItemName(this.creatorService.Language) + "</div>";
                html += "<p><small>" + product.VariantId + "</small></p>";
                html += "<hr style=\"border-top: dotted 1px;\" /><div class=\"clearfix\" style=\" font-size:20px; height 20px; font-weight: bold;\">" + this.creatorService.FinalizePrice(product) + "</div>";
                html += "</div>";

                html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 120, 50, true, 5));

                interval++;
                productsInterval++;
                newPageInterval++;
                pdfProductsPageInterval++;
                productsAdded++;

                if (interval == 6 || productsInterval == this.creatorService.GetProductsCount())
                {
                    html += "</div>";
                    interval = 0;
                }

                stringBuilder.AppendLine(html);

                if (pdfProductsPageInterval == 90 || this.creatorService.GetProductsCount() == productsAdded)
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
                converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

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
