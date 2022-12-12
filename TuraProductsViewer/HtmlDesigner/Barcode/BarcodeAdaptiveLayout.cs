using Microsoft.AspNetCore.Html;
using SelectPdf;
using Serilog;
using System.Drawing;
using System.Text;
using TuraProductsViewer.Services;

namespace TuraProductsViewer.HtmlDesigner.Barcode
{
    public class BarcodeAdaptiveLayout
    {
        private Microsoft.Extensions.Logging.ILogger logger { get; set; }
        private StringBuilder stringBuilder { get; set; }
        private CreatorService creatorService { get; set; }
        private List<PdfDocument> pdfDocuments { get; set; }


        public BarcodeAdaptiveLayout(CreatorService crtService)
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
                    stringBuilder.AppendLine("<html><body style=\"width:800px; margin-left:53px; margin-right:auto; margin-top:44px; \">");
                }

                if (newPageInterval == 66)
                {
                    html += "<div style=\"page-break-after: always\">\r\n\"\r\n</div>";
                    html += "<div style=\"padding-top:44px;\"></div>";
                    newPageInterval = 0;
                }

                if (interval == 0)
                {
                    html += "<div style=\"padding-bottom: 96px;\">";
                }

                html += "<div style=\"width:115px; height:35px; float:left; text-align: center; padding-right:0px;\">";
                if (this.creatorService.PriceType == PriceType.None)
                {
                    html += "<div style=\" height:26px;\"></div>";
                }
                else
                {
                    html += "<p style=\"display:inline; font: bold 26px Arial;\">" + (this.creatorService.PriceType == PriceType.Rek ? this.creatorService.FinalizePrice(product.UnitPrice) : this.creatorService.FinalizePrice(product)) + "</p>";
                }
                html += "<img style=\"max-width: 100%; max-height:100%;\" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" />"; //width:120px; height:45px;
                html += "<p style=\"display:inline; font: 9px verdana;\">Tura: " + product.VariantId + "</p>";
                html += "</div>";

                if (this.creatorService.ShowEANCode)
                {
                    html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 160, 45, true, 5));
                }
                else
                {
                    html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 160, 35, false, 5));
                }

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

                if (pdfProductsPageInterval == 330 || this.creatorService.GetProductsCount() == productsAdded)
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
            catch(Exception ex)
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
