using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System.Drawing;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace TuraProductsViewer.Pages
{
    public class PDFSharp
    {
        public static MemoryStream GetStream(string html)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument pdf = PdfGenerator.GeneratePdf(html, PageSize.A4);
            
            foreach(var page in pdf.Pages)
            {
                XGraphics gfx = XGraphics.FromPdfPage(page);
                gfx.DrawRectangle(XBrushes.Black, 0, 0, gfx.PdfPage.Width.Point, 63);
            }

            MemoryStream stream = new MemoryStream();
            pdf.Save(stream);

            return stream;
        }
    }
}
