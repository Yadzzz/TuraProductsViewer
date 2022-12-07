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
        private bool isHTML { get; set; }
        private string clickImageLink { get; set; }
        private List<PdfDocument> pdfDocuments { get; set; }


        public BarcodeAdaptiveLayout(CreatorService crtService, bool isHtml)
        {
            this.stringBuilder = new();
            this.creatorService = crtService;
            this.isHTML = isHtml;
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

                if(pdfProductsPageInterval == 0)
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

                html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 150,40, true, 5));

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

                if(pdfProductsPageInterval == 200 || this.creatorService.GetProductsCount() == productsAdded)
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

            //Footer settings
            converter.Options.DisplayFooter = true;
            converter.Footer.DisplayOnFirstPage = true;
            converter.Footer.DisplayOnOddPages = true;
            converter.Footer.DisplayOnEvenPages = true;
            converter.Footer.Height = 40;

            PdfTextSection text2 = new PdfTextSection(170, 0, GetFooterContactInformation(this.creatorService.Language), new System.Drawing.Font("Arial", 8));
            converter.Footer.Add(text2);

            PdfTextSection dateText = new PdfTextSection(460, 30, "PDF Created [" + DateTime.Now + "]", new System.Drawing.Font("Arial", 8));
            converter.Footer.Add(dateText);

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(html);

            doc.CompressionLevel = PdfCompressionLevel.Best;

            this.pdfDocuments.Add(doc);
        }

        public string GetFooterContactInformation(string language)
        {
            if (language.ToLower().Contains("swedish"))
            {
                return "Tura Scandinavia AB - Tura +46 (0)300 56 89 20  info@turascandinavia.com";
            }
            else if (language.ToLower().Contains("norwegian"))
            {
                return "Tura Scandinavia AB - Tura +47 22 62 74 80 ordre@turascandinavia.com";
            }
            else if (language.ToLower().Contains("finnish"))
            {
                return "Tura Scandinavia AB - Tura +358 (0)207 600 950 finland@turascandinavia.com";
            }
            else if (language.ToLower().Contains("danish"))
            {
                return "Tura Scandinavia AB - Tura +45 48 18 78 81 dk@turascandinavia.com";
            }
            else if (language.ToLower().Contains("english"))
            {
                return "Tura Scandinavia AB - Tura +46 (0)300 56 89 20 info@turascandinavia.com";
            }
            else
            {
                return "Tura Scandinavia AB - Tura +46 (0)300 56 89 20  info@turascandinavia.com";
            }
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
