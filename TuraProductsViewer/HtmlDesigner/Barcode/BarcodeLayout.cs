using SelectPdf;
using System.Drawing;
using System.Text;
using TuraProductsViewer.Services;

namespace TuraProductsViewer.HtmlDesigner.Barcode
{
    public class BarcodeLayout
    {
        private StringBuilder stringBuilder { get; set; }
        private CreatorService creatorService { get; set; }
        private bool isHTML { get; set; }
        private string clickImageLink { get; set; }

        public BarcodeLayout(CreatorService crtService, bool isHtml)
        {
            this.stringBuilder = new();
            this.creatorService = crtService;
            this.isHTML = isHtml;

            this.Initialize();
        }

        private void Initialize()
        {
            if (this.isHTML)
            {
                stringBuilder.AppendLine("<html><body style=\"height:210mm; width:297mm; margin-left:auto; margin-right:auto; margin-top:0px;\">");
            }
            else
            {
                stringBuilder.AppendLine("<html><body style=\"height:210mm; width:297mm; margin-left:30px; margin-right:auto; margin-top:0px;\">");
            }

            int interval = 0;
            int productsInterval = 0;
            foreach (var product in creatorService.GetProducts())
            {
                string html = string.Empty;

                if (interval == 0)
                {
                    html += "<div style=\"padding-top: 40px; padding-bottom: 40px;\">";
                }

                html += "<div style=\"width:180px; height:50; float:left;\"> <img style=\"width:150px; height:60px;\" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" /> </div>";
                html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode));

                interval++;
                productsInterval++;

                if (interval == 6 || productsInterval == this.creatorService.GetProductsCount())
                {
                    html += "</div>";
                    interval = 0;
                }

                stringBuilder.AppendLine(html);
            }

            stringBuilder.AppendLine("</body></html>");
        }

        public MemoryStream GetStream()
        {
            string htmlString = this.stringBuilder.ToString();

            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;
            //converter.Options.PdfStandard = PdfStandard.PdfA;

            //converter.Options.DisplayHeader = true;
            //converter.Header.DisplayOnFirstPage = true;
            //converter.Header.DisplayOnOddPages = true;
            //converter.Header.DisplayOnEvenPages = true;
            //converter.Header.Height = 70;

            //PdfHtmlSection headerHtml = new PdfHtmlSection("<style>@import url('https://fonts.googleapis.com/css?family=Open+Sans'); .header { padding-top:20px; width: 100%; height:150px; text-align: center; background: #3F3F3F;  color: white;  font-size: 30px; font-family: \"Open Sans\", sans-serif;}</style><div class=\"header\"><h3> " + headerTitle + "</h3></div>", "");
            //headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            //converter.Header.Add(headerHtml);

            //Footer settings
            converter.Options.DisplayFooter = true;
            converter.Footer.DisplayOnFirstPage = true;
            converter.Footer.DisplayOnOddPages = true;
            converter.Footer.DisplayOnEvenPages = true;
            converter.Footer.Height = 40;

            PdfTextSection text2 = new PdfTextSection(170, 0, GetFooterContactInformation(this.creatorService.Language), new Font("Arial", 8));
            converter.Footer.Add(text2);

            PdfTextSection dateText = new PdfTextSection(460, 30, "PDF Created [" + DateTime.Now + "]", new Font("Arial", 8));
            converter.Footer.Add(dateText);

            //converter.Options.JpegCompressionEnabled = true;
            //converter.Options.JpegCompressionLevel = 50;
            //converter.Options.ScaleImages = true;

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(htmlString);

            doc.CompressionLevel = PdfCompressionLevel.Best;

            using (MemoryStream stream = new MemoryStream())
            {
                doc.Save(stream);

                Console.WriteLine("STREAM: " + stream.Length);

                doc.Close();
                return stream;
            }
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

        public string GetHTML()
        {
            return stringBuilder.ToString();
        }
    }
}
