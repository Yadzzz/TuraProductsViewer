using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using SelectPdf;

namespace TuraProductsViewer.Services
{
    public class PdfCreatorService
    {
        private readonly ILogger<CreatorService> logger;

        public PdfCreatorService(ILogger<CreatorService> logger)
        {
            this.logger = logger;
        }

        public void CreateHtml(string html)
        {
            // read parameters from the webpage
            string htmlString = html;
            string baseUrl = "/Produktbilder/";

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            //set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;

            //converter.Options.PdfStandard = PdfStandard.PdfA;

            converter.Options.MarginLeft = 20;
            converter.Options.MarginRight = 0;

            //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;

            // create a new pdf document converting an url
            //PdfDocument doc = converter.ConvertHtmlString(htmlString, baseUrl);
            PdfDocument doc = converter.ConvertHtmlString(htmlString);

            // save pdf document
            doc.Save("wwwroot/pdf/sample.pdf");

            // close pdf document
            doc.Close();
        }

        public MemoryStream GetHTMLStream(string html)
        {
            // read parameters from the webpage
            string htmlString = html;

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            //set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;

            converter.Options.PdfStandard = PdfStandard.PdfA;

            converter.Options.MarginLeft = 20;
            converter.Options.MarginRight = 0;

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(htmlString);

            using (MemoryStream stream = new MemoryStream())
            {
                //Saving the PDF document into the stream
                doc.Save(stream);
                //Closing the PDF document
                doc.Close();
                return stream;
            }
        }

        public MemoryStream? GetPDFStream(string html, string headerTitle, string language)
        {
            // read parameters from the webpage
            string htmlString = html;
            string baseUrl = "/Produktbilder/";

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            //set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.PdfStandard = PdfStandard.PdfA;

            //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;

            //converter.Options.MarginLeft = 0;
            //converter.Options.MarginRight = 0;
            //converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
            //converter.Options.WebPageWidth = 992;

            //converter.Options.PdfCompressionLevel = PdfCompressionLevel.NoCompression;



            //Header settings
            converter.Options.DisplayHeader = true;
            converter.Header.DisplayOnFirstPage = true;
            converter.Header.DisplayOnOddPages = true;
            converter.Header.DisplayOnEvenPages = true;
            converter.Header.Height = 70;

            PdfHtmlSection headerHtml = new PdfHtmlSection("<style>@import url('https://fonts.googleapis.com/css?family=Open+Sans'); .header { padding-top:20px; width: 100%; height:150px; text-align: center; background: #3F3F3F;  color: white;  font-size: 30px; font-family: \"Open Sans\", sans-serif;}</style><div class=\"header\"><h3> " + headerTitle + "</h3></div>", "");
            headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Header.Add(headerHtml);

            //Footer settings
            converter.Options.DisplayFooter = true;
            converter.Footer.DisplayOnFirstPage = true;
            converter.Footer.DisplayOnOddPages = true;
            converter.Footer.DisplayOnEvenPages = true;
            converter.Footer.Height = 40;

            PdfTextSection text2 = new PdfTextSection(170, 0, GetFooterContactInformation(language), new Font("Arial", 8));
            //text2.HorizontalAlign = PdfTextHorizontalAlign.Center;
            converter.Footer.Add(text2);

            PdfTextSection dateText = new PdfTextSection(460, 30, "PDF Created [" + DateTime.Now + "]", new Font("Arial", 8));
            //text2.HorizontalAlign = PdfTextHorizontalAlign.Right;
            converter.Footer.Add(dateText);

            converter.Options.JpegCompressionEnabled = true;
            converter.Options.JpegCompressionLevel = 50;
            converter.Options.ScaleImages = true;

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(htmlString);

            doc.CompressionLevel = PdfCompressionLevel.Best;

            //doc.Save("wwwroot/pdf/sample.pdf");

            using (MemoryStream stream = new MemoryStream())
            {
                //Saving the PDF document into the stream
                doc.Save(stream);

                Console.WriteLine("STREAM: " + stream.Length);

                //Closing the PDF document
                doc.Close();
                return stream;
            }
        }

        public MemoryStream GetPDFStreamBarcodeLayout(string htmlString, string language)
        {
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.NoAdjustment;

            //Footer settings
            //converter.Options.DisplayFooter = true;
            //converter.Footer.DisplayOnFirstPage = true;
            //converter.Footer.DisplayOnOddPages = true;
            //converter.Footer.DisplayOnEvenPages = true;
            //converter.Footer.Height = 40;

            //PdfTextSection text2 = new PdfTextSection(170, 0, GetFooterContactInformation(language), new System.Drawing.Font("Arial", 8));
            //converter.Footer.Add(text2);

            //PdfTextSection dateText = new PdfTextSection(460, 30, "PDF Created [" + DateTime.Now + "]", new System.Drawing.Font("Arial", 8));
            //converter.Footer.Add(dateText);

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
    }
}
