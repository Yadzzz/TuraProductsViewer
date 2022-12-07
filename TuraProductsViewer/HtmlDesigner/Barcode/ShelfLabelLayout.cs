using SelectPdf;
using System.Drawing;
using System.Text;
using TuraProductsViewer.Services;

namespace TuraProductsViewer.HtmlDesigner.Barcode
{
    public class ShelfLabelLayout
    {
        private StringBuilder stringBuilder { get; set; }
        private CreatorService creatorService { get; set; }
        private bool isHTML { get; set; }

        public ShelfLabelLayout(CreatorService crtService, bool isHtml)
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
                stringBuilder.AppendLine("<style>.clearfix:after{\r\n  clear: both;\r\n  content: \".\";\r\n  display: block;\r\n  height: 0;\r\n  visibility: hidden;\r\n  font-size: 0;\r\n}</style>");
                stringBuilder.AppendLine("<html><body style=\"width:900px; margin-left:auto; margin-right:auto; margin-top:0px; \">");
            }
            else
            {
                stringBuilder.AppendLine("<html><body style=\"width:900px; margin-left:25px; margin-right:auto; margin-top:0px; \">");
            }

            int interval = 0;
            int productsInterval = 0;
            foreach (var product in creatorService.GetProducts())
            {
                string html = string.Empty;

                if (interval == 0)
                {
                    html += "<div style=\"padding-top: 5px; padding-bottom: 0px; \" class=\"clearfix\">";
                }

                //border-width: 1px 1px 1px 1px; border-color: black; border-style: solid;

                html += "<div class=\"clearfix\" style=\"margin: 0 auto; width:120px; height:300px; overflow: hidden; float:left; text-align: center; padding-right:10px; padding-left;0px; border-width: 1px 1px 1px 1px;  border-style: dotted;\">";
                //html += "<img style=\"max-width: 100%; max-height:100%; transform:rotate(270deg);\" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" />";
                html += "<br /><br /><br /><div class=\"clearfix\"><img style=\"max-width:auto; max-height:auto; transform:rotate(270deg); \" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" /></div>";
                //html += "<img style=\"max-width:auto; max-height:auto; transform:rotate(270deg); \" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" />";
                html += "<br /><br /><br /><hr style=\"border-top: dotted 1px;\" /><div class=\"clearfix\" style=\"padding-left:3px; height:35px; overflow: hidden; font-size:11px; \">" + product.GetItemName(this.creatorService.Language) + "</div>";
                html += "<p><small>" + product.VariantId + "</small></p>";
                html += "<hr style=\"border-top: dotted 1px;\" /><div class=\"clearfix\" style=\" font-size:20px; height 20px; font-weight: bold;\">100,00</div>";
                html += "</div>";

                html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 120, 50, true, 5));
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

        public string GetHTML()
        {
            return stringBuilder.ToString();
        }
    }
}
