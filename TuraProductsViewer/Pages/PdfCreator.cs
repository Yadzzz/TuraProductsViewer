﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using SelectPdf;

namespace TuraProductsViewer.Pages
{
    public class PdfCreator
    {
        public void CreateHtml(string html)
        {
            // read parameters from the webpage
            string htmlString = html;
            string baseUrl = "/Produktbilder/";

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            //set converter options
            converter.Options.PdfPageSize  = PdfPageSize.A4;
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

        public MemoryStream GetPDFStream(string html, string headerTitle)
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

            //converter.Options.MarginLeft = 0;
            //converter.Options.MarginRight = 0;
            //converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
            //converter.Options.WebPageWidth = 992;

            //converter.Options.PdfCompressionLevel = PdfCompressionLevel.Best;

            //Header settings
            converter.Options.DisplayHeader = true;
            converter.Header.DisplayOnFirstPage = true;
            converter.Header.DisplayOnOddPages = true;
            converter.Header.DisplayOnEvenPages = true;
            converter.Header.Height = 70;

            PdfHtmlSection headerHtml = new PdfHtmlSection("<style>.header { padding-top:20px; width: 100%; height:150px; text-align: center; background: #3F3F3F;  color: white;  font-size: 30px;}</style><div class=\"header\"><h3> " + headerTitle + "</h3></div>", "");
            headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Header.Add(headerHtml);

            //Footer settings
            converter.Options.DisplayFooter = true;
            converter.Footer.DisplayOnFirstPage = true;
            converter.Footer.DisplayOnOddPages = true;
            converter.Footer.DisplayOnEvenPages = true;
            converter.Footer.Height = 40;

            // page numbers can be added using a PdfTextSection object
            //PdfTextSection text = new PdfTextSection(0, 8, "Page: {page_number} of {total_pages}  ", new System.Drawing.Font("Arial", 8));
            //text.HorizontalAlign = PdfTextHorizontalAlign.Right;
            //converter.Footer.Add(text);

            PdfTextSection text2 = new PdfTextSection(0, 0, "Tura Scandinavia AB - Tura +46 (0)300 56 89 20  info@turascandinavia.com", new System.Drawing.Font("Arial", 8));
            text2.HorizontalAlign = PdfTextHorizontalAlign.Center;
            converter.Footer.Add(text2);

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
    }
}
