using SelectPdf;
using System.Drawing;
using System.Text;
using TuraProductsViewer.Services;

namespace TuraProductsViewer.HtmlDesigner.Barcode
{
    public class PinFlagLayout
    {
        private StringBuilder stringBuilder { get; set; }
        private CreatorService creatorService { get; set; }
        private bool isHTML { get; set; }

        public PinFlagLayout(CreatorService crtService, bool isHtml)
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
                stringBuilder.AppendLine("<html><body style=\"width:800px; margin-left:auto; margin-right:auto; margin-top:0px; \">");
            }
            else
            {
                stringBuilder.AppendLine("<html><body style=\"width:800px; margin-left:25px; margin-right:auto; margin-top:0px; \">");
            }

            int interval = 0;
            int productsInterval = 0;
            int newPageInterval = 0;
            foreach (var product in creatorService.GetProducts())
            {
                string html = string.Empty;

                if (newPageInterval == 24 && !this.isHTML)
                {
                    html += "<div style=\"page-break-after: always\">.</div>";
                    newPageInterval = 0;
                }

                if (interval == 0)
                {
                    html += "<div style=\"padding-top: 30px; padding-bottom: 100px;\">";
                }

                html += "<div style=\"width:170px; height:120px; float:left; text-align: center; padding-right:40px; padding-left:40px; border-bottom: 1px solid #202020;\">";
                html += "<img style=\"max-width: 100%; max-height:100%;\" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" />"; //width:120px; hiehgt:45px;
                //html += "<p style=\"display:inline\"><b>" + product.PrimaryEANCode + "</b></p>";
                html += "<br /><p style=\"display:inline\"><small>" + product.GetItemName(this.creatorService.Language) + "</small></p>";
                html += "<br /><h3 style=\"display:inline\">Art.Nr: " + product.VariantId + "</h3>";
                html += "</div>";

                html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 236, 50, true, 5));

                interval++;
                productsInterval++;
                newPageInterval++;

                if (interval == 3 || productsInterval == this.creatorService.GetProductsCount())
                {
                    html += "</div>";
                    interval = 0;
                }

                stringBuilder.AppendLine(html);
            }

            stringBuilder.AppendLine("</body></html>");
        }

        public string GetHTML()
        {
            return stringBuilder.ToString();
        }
    }
}
