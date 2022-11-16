using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using SelectPdf;

namespace TuraProductsViewer.Pages
{
    public static class PdfCreator
    {
        public static void CreateHtml(string html)
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
    }
}
