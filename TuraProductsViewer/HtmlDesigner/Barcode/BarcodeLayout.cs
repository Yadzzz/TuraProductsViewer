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
                stringBuilder.AppendLine("<html><body style=\"width:800px; margin-left:auto; margin-right:auto; margin-top:44px; \">");
            }
            else
            {
                stringBuilder.AppendLine("<html><body style=\"width:800px; margin-left:53px; margin-right:auto; margin-top:44px; \">");
            }

            int interval = 0;
            int productsInterval = 0;
            int newPageInterval = 0;
            foreach (var product in creatorService.GetProducts())
            {
                string html = string.Empty;

                if(newPageInterval == 66 && !this.isHTML)
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
                html += "<p style=\"display:inline; font: bold 26px Arial;\">" + this.creatorService.FinalizePrice(product) + "</p>";
                html += "<img style=\"max-width: 100%; max-height:100%;\" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" />"; //width:120px; height:45px;
                html += "<p style=\"display:inline; font: 9px verdana;\">Tura: " + product.VariantId + "</p>";
                html += "</div>";

                html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 160,45, true, 5));
                interval++;
                productsInterval++;
                newPageInterval++;

                if (interval == 6 || productsInterval == this.creatorService.GetProductsCount())
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
