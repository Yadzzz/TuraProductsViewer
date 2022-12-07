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

                if(newPageInterval == 40 && !this.isHTML)
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

                if (interval == 5 || productsInterval == this.creatorService.GetProductsCount())
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
