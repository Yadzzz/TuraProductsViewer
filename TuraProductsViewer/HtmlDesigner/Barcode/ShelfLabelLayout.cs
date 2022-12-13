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
                stringBuilder.AppendLine("<html><body style=\"width:1200px; margin-left:auto; margin-right:auto; margin-top:42px; \">");
            }
            else
            {
                stringBuilder.AppendLine("<html><body style=\"width:1200px; margin-left:33px; margin-right:auto; margin-top:42px; \">");
            }

            int interval = 0;
            int productsInterval = 0;
            int newPageInterval = 0;
            foreach (var product in creatorService.GetProducts())
            {
                string html = string.Empty;

                if (newPageInterval == 33 && !this.isHTML)
                {
                    html += "<div style=\"page-break-after: always\">.</div>";
                    html += "<div style=\"padding-top:42px;\"></div>";
                    newPageInterval = 0;
                }

                if (interval == 0)
                {
                    html += "<div style=\"padding-top: 0px; padding-bottom: 0px; margin: 0 auto;  overflow: hidden; \" class=\"clearfix\">";
                }

                //border-width: 1px 1px 1px 1px; border-color: black; border-style: solid;

                html += "<div class=\"clearfix\" style=\"margin: 0 auto; width:92.5px; height:241px; overflow: hidden; float:left; text-align: center; border-width: 1px 1px 1px 1px;  border-style: dotted;\">";
                //html += "<img style=\"max-width: 100%; max-height:100%; transform:rotate(270deg);\" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" />";
                html += "<br /><br /><div class=\"clearfix\" style=\"height:45px; \"><img style=\"max-width:100%; max-height:100%; transform:rotate(270deg); \" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" /></div>";
                //html += "<img style=\"max-width:auto; max-height:auto; transform:rotate(270deg); \" src=\"data:image/png;base64, {@base64img@}\" alt=\"Red dot\" />";
                html += "<br /><hr style=\"border-top: dotted 1px;\" /><div class=\"clearfix\" style=\"padding-left:3px; height:40px; overflow: hidden; font-size:11px; \">" + product.GetItemName(this.creatorService.Language) + "</div>";
                html += "<div style=\"height:10px;\"><p style=\" font: 8px sans-serif;\">" + product.VariantId + "</p></div>";

                if (this.creatorService.PriceType == PriceType.None)
                {
                    html += "<hr style=\"border-top: dotted 1px;\" /><div class=\"clearfix\" style=\" font-size:20px; height 20px; font-weight: bold;\"></div>";
                }
                else
                {
                    html += "<hr style=\"border-top: dotted 1px;\" /><div class=\"clearfix\" style=\" font-size:20px; height 20px; font-weight: bold;\">" + (this.creatorService.PriceType == PriceType.Rek ? this.creatorService.FinalizePrice(product.UnitPrice) : this.creatorService.FinalizePrice(product)) + "</div>";
                }
                html += "</div>";

                if (this.creatorService.ShowEANCode)
                {
                    html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 150, 70, true, 3));
                }
                else
                {
                    html = html.Replace("{@base64img@}", BarcodeGenerator.GetBase64Image(product.PrimaryEANCode, 150, 60, false, 4));
                }

                interval++;
                productsInterval++;
                newPageInterval++;

                if (interval == 11 || productsInterval == this.creatorService.GetProductsCount())
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
