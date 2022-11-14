using System.Text;

namespace TuraProductsViewer.Services
{
    public class FileCreatorService
    {
        public string GenerateHTML(PDFCreatorService creatorService)
        {
            int a = 0;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<style>");
            sb.AppendLine("*{\r\n  box-sizing: border-box;\r\n  margin:0;\r\n  padding:0;\r\n  overflow-x,overflow-y:hidden;\r\n  font-face: \"verdana\",\"sans-serif\";\r\n  box-sizing: border-box;\r\n}\r\n\r\nbody{\r\n  background:#E6E8E1;\r\n}\r\n\r\n.header {\r\n  padding: 60px;\r\n  text-align: center;\r\n  background: #3F3F3F;\r\n  color: white;\r\n  font-size: 30px;\r\n}\r\n\r\n.content {padding:20px;}\r\n\r\n.row {\r\n  display: flex;\r\n  justify-content: space-between;\r\n  /*border: 1px solid green;*/\r\n}\r\n\r\n.container{\r\n  height: 400px;\r\n  width: 100px;\r\n  margin: 20px auto;\r\n  margin-top:20px;\r\n  border-radius:20px;\r\n  box-shadow: 1px 2px 10px 0px rgba(0,0,0,0.3);\r\n  background-color:#E6E8E1;\r\n  float:left;\r\n  padding: 50px;\r\n  width: 40%;\r\n  );\r\n}");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");

            sb.AppendLine("<body>");

            //Header
            sb.AppendLine("<div class=\"header\">");
            sb.AppendLine("<h2>Tura Scandinavia</h2>");
            sb.AppendLine("<!--<p>child-title</p>-->");
            sb.AppendLine("</div>");

            //Content
            sb.AppendLine("<div class=\"content\">");

            foreach(var product in creatorService.GetProducts())
            {
                if(a == 0)
                {
                    sb.AppendLine("<div class=\"row\">");
                }

                sb.AppendLine("<div class=\"container\">");
                sb.AppendLine("<center><p>" + product.VariantId + "</p></center>");
                sb.AppendLine("<br />");
                sb.AppendLine("<div style=\"float:left\"><p >Image</p></div>");
                sb.AppendLine("<div style=\"float:right\"><p>" + product.SeItemName + "</p></div>");
                sb.AppendLine("</div>");

                a++;

                if (a >= 2)
                {
                    sb.AppendLine("</div>");
                    a = 0;
                }
            }

            //Row
            //sb.AppendLine("<div class=\"row\">");

            //sb.AppendLine("<div class=\"container\">");
            //sb.AppendLine("<center><p>Product</p></center>");
            //sb.AppendLine("<br />");
            //sb.AppendLine("<div style=\"float:left\"><p >Image</p></div>");
            //sb.AppendLine("<div style=\"float:right\"><p>Product Description</p></div>");
            //sb.AppendLine("</div>");

            //sb.AppendLine("<div class=\"container\">");
            //sb.AppendLine("<center><p>Product</p></center>");
            //sb.AppendLine("<br />");
            //sb.AppendLine("<div style=\"float:left\"><p >Image</p></div>");
            //sb.AppendLine("<div style=\"float:right\"><p>Product Description</p></div>");
            //sb.AppendLine("</div>");

            //sb.AppendLine("</div>");

            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
    }
}
