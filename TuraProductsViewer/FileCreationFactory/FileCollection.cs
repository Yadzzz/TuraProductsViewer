using Microsoft.AspNetCore.Components.Forms;
using System.Text;

namespace TuraProductsViewer.FileCreationFactory
{
    public class FileCollection
    {
        public int Id { get; set; }
        public string HtmlPath { get; private set; }

        public FileCollection(int id)
        {
            this.Id = id;
            this.HtmlPath = "../TuraProductsViewer/wwwroot/html/" + id + ".html";
        }

        public bool CreateHTML()
        {
            try
            {
                //IronPdf.ChromePdfRenderer Renderer = new IronPdf.ChromePdfRenderer();
                //// add a header to very page easily
                //Renderer.RenderingOptions.FirstPageNumber = 1;
                //Renderer.RenderingOptions.TextHeader.DrawDividerLine = true;
                //Renderer.RenderingOptions.TextHeader.CenterText = "Title";
                //Renderer.RenderingOptions.TextHeader.FontSize = 24;
                //// add a footer too
                //Renderer.RenderingOptions.TextFooter.DrawDividerLine = true;
                //Renderer.RenderingOptions.TextFooter.FontSize = 16;
                //Renderer.RenderingOptions.TextFooter.LeftText = "{date} {time}";
                //Renderer.RenderingOptions.TextFooter.RightText = "{page} of {total-pages}";
                //string htmlText = "<h1> This is Sample Pdf file</h1> <p> This is the demo for Csharp Created Pdf using IronPdf </p> <p> IronPdf is a library which provides build in functions for creating, reading <br> and manuplating pdf files with just few lines of code. </p>";
                //Renderer.RenderHtmlAsPdf(htmlText).SaveAs("../TuraProductsViewer/wwwroot/pdf/test.pdf");

                //StringBuilder sb = new StringBuilder();
                //sb.AppendLine("<h3>Test H3</h3>");
                //sb.AppendLine("<h1>Test</h1>");
                //byte[] buffer = new UTF8Encoding(true).GetBytes(sb.ToString());
                //var f = File.Create(this.HtmlPath, buffer.Length);
                //f.Write(buffer, 0, buffer.Length);

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

                //Row
                sb.AppendLine("<div class=\"row\">");

                sb.AppendLine("<div class=\"container\">");
                sb.AppendLine("<center><p>Product</p></center>");
                sb.AppendLine("</div>");

                sb.AppendLine("<div class=\"container\">");
                sb.AppendLine("<center><p>Product</p></center>");
                sb.AppendLine("</div>");

                sb.AppendLine("</div>");

                sb.AppendLine("</div>");
                sb.AppendLine("</body>");
                sb.AppendLine("</html>");

                byte[] buffer = new UTF8Encoding(true).GetBytes(sb.ToString());

                //using (var f = File.Create(this.HtmlPath, buffer.Length))
                //{
                //    f.Write(buffer, 0, buffer.Length);
                //}

                FileInfo fi = new FileInfo(this.HtmlPath);

                //using (StreamWriter sw = fi.CreateText())
                //{
                //    sw.WriteLine("New file created: {0}", DateTime.Now.ToString());
                //    sw.WriteLine("Author: Mahesh Chand");
                //    sw.WriteLine("Add one more line ");
                //    sw.WriteLine("Add one more line ");
                //    sw.WriteLine("Done! ");
                //}
                

            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
