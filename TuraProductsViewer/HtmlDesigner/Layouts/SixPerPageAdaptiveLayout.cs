using Microsoft.AspNetCore.Components;
using SelectPdf;
using Serilog;
using System;
using System.Reflection;
using System.Text;
using TuraProductsViewer.Services;

namespace TuraProductsViewer.HtmlDesigner.Layouts
{
    public class SixPerPageAdaptiveLayout
    {
        private Microsoft.Extensions.Logging.ILogger logger { get; set; }
        private StringBuilder stringBuilder { get; set; }
        private CreatorService creatorService { get; set; }
        private ImageService imageService { get; set; }
        private bool isHTML { get; set; }
        private string title { get; set; }
        private string language { get; set; }
        private Dictionary<string, string> languageVariables { get; set; }
        private string clickImageLink { get; set; }
        private List<PdfDocument> pdfDocuments { get; set; }

        public SixPerPageAdaptiveLayout(CreatorService crtService, ImageService imgService, bool isHtml, string pageTitle, string language, Dictionary<string, string> languageVariables, string clickImgLink)
        {
            this.stringBuilder = new();
            this.creatorService = crtService;
            this.imageService = imgService;
            this.isHTML = isHtml;
            this.title = pageTitle;
            this.languageVariables = languageVariables;
            this.clickImageLink = clickImgLink;

            this.pdfDocuments = new List<PdfDocument>();

            //Temporary logger
            var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog());
            var logger = loggerFactory.CreateLogger(string.Empty);
            this.logger = logger;
        }

        public MemoryStream InitializePDF()
        {
            int productsAdded = 0;
            int productsInterval = 0;

            int interval = 0;
            int pageInterval = 0;
            foreach (var product in creatorService.GetProducts())
            {
                string html = string.Empty;

                if (productsInterval == 0)
                {
                    stringBuilder.AppendLine(this.GetStartDesign());
                }

                if (pageInterval == 6 && productsInterval != 0)
                {
                    html += "<div style=\"page-break-after: always\">.</div>";
                    pageInterval = 0;
                }

                if (interval == 0)
                {
                    html += "<div class=\"col-xs-12 col-md-12\" >";
                }

                html += "<div class=\"col-xs-12 col-md-4\">\r\n\t<!-- First product box start here-->\r\n\t<div class=\"prod-info-main prod-wrap clearfix\">\r\n\t\t<div class=\"row\">\r\n\t\t\t\t<div class=\"col-md-12 col-sm-12 col-xs-12\">\r\n\t\t\t\t\t<div class=\"product-image\"> \r\n\t\t\t\t\t\t <a href=\"{@imageclicklink@}\"><img src=\"{@image@}\" class=\"img-responsive\"></a> \r\n\t\t\t\t\t</div>\r\n\t\t\t\t</div>\r\n\t\t</div>\r\n        <div class=\"row\">\r\n            <div class=\"col-md-12 col-sm-12 col-xs-12\">\r\n\t\t\t\t<div class=\"product-detail\">\r\n                    <h5 class=\"name\"  style=\"height:35px;\">\r\n                        <b><a href=\"{@imageclicklink@}\" style=\"color: black;\">{@productname@}</a></b>\r\n                   \r\n                    </h5>\r\n\t\t\t\t</div><br />";

                html = html.Replace("{@imageclicklink@}", this.clickImageLink + product.VariantId);
                html = html.Replace("{@productname@}", product.GetItemName(creatorService.Language));

                //html += this.AddDataRow(this.languageVariables["artnrvariable"], product.VariantId);
                html += this.AddDataRowWithLink(this.languageVariables["artnrvariable"], product.VariantId, this.clickImageLink + product.VariantId);
                html += this.AddDataRow(this.languageVariables["varumarkevariable"], product.Brand != null ? product.Brand : "N/A");

                if (this.creatorService.PriceType == PriceType.Rek)
                {
                    html += this.AddDataRow(this.languageVariables["rekprisvariable"], product.UnitPrice.ToString("F2") + " " + creatorService.CurrencyCode.ToUpper());
                }
                else if (this.creatorService.PriceType == PriceType.Netto)
                {
                    //html += this.AddDataRow(this.languageVariables["prisvariable"], product.UnitPriceWithoutVat.ToString("F2") + " " + creatorService.CurrencyCode.ToUpper());
                    html += this.AddDataRow(this.languageVariables["prisvariable"], this.creatorService.FinalizePrice(product) + " " + creatorService.CurrencyCode.ToUpper());
                }
                else if (this.creatorService.PriceType == PriceType.RekNetto)
                {
                    //html += this.AddDataRow(this.languageVariables["prisvariable"], product.UnitPriceWithoutVat.ToString("F2") + " " + creatorService.CurrencyCode.ToUpper());
                    html += this.AddDataRow(this.languageVariables["prisvariable"], this.creatorService.FinalizePrice(product) + " " + creatorService.CurrencyCode.ToUpper());
                    html += this.AddDataRow(this.languageVariables["rekprisvariable"], product.UnitPrice.ToString("F2") + " " + creatorService.CurrencyCode.ToUpper());
                }
                else if (this.creatorService.PriceType == PriceType.Kund)
                {
                    //if (this.creatorService.SpecialCustomerPrices != null)
                    //{
                    //    string price;
                    //    if (this.creatorService.SpecialCustomerPrices.TryGetValue(product.VariantId, out price))
                    //    {
                    //        html += this.AddDataRow(this.languageVariables["prisvariable"], price + " " + creatorService.CurrencyCode.ToUpper());
                    //    }
                    //}

                    html += this.AddDataRow(this.languageVariables["prisvariable"], this.creatorService.FinalizePrice(product) + " " + creatorService.CurrencyCode.ToUpper());
                }
                else if (this.creatorService.PriceType == PriceType.KundRek)
                {
                    html += this.AddDataRow(this.languageVariables["prisvariable"], this.creatorService.FinalizePrice(product) + " " + creatorService.CurrencyCode.ToUpper());
                    html += this.AddDataRow(this.languageVariables["rekprisvariable"], product.UnitPrice.ToString("F2") + " " + creatorService.CurrencyCode.ToUpper());
                }
                else if (this.creatorService.PriceType == PriceType.None)
                {

                }
                else
                {
                    html += this.AddDataRow(this.languageVariables["rekprisvariable"], product.UnitPrice.ToString("F2") + " " + creatorService.CurrencyCode.ToUpper());
                }

                html += this.AddDataRow(this.languageVariables["eanvariable"], product.PrimaryEANCode);

                if (creatorService.ShowPackagingMeasurment)
                {
                    html += this.AddDataRow(this.languageVariables["frpstlvariable"], product.QtyPerUnitOfMeasure.ToString("F0") + " " + this.languageVariables["measurment"]);
                }

                if (creatorService.ShowInStock)
                {
                    if (creatorService.ShowInStockCount)
                    {
                        html += this.AddDataRow(this.languageVariables["ilagervariable"], product.AvailableQty > 0 ? (product.AvailableQty.ToString("F0") + " " + this.languageVariables["measurment"]) : this.languageVariables["no"]);
                    }
                    else
                    {
                        html += this.AddDataRow(this.languageVariables["ilagervariable"], product.AvailableQty > 0 ? this.languageVariables["yes"] : this.languageVariables["no"]);
                    }
                }

                if (creatorService.UsePackagingImage)
                {
                    if (this.isHTML)
                    {
                        html = html.Replace("{@image@}", imageService.GetWebPackagingImagePath(product.VariantId));
                    }
                    else
                    {
                        html = html.Replace("{@image@}", imageService.GetObseletePackagingImagePath(product.VariantId));
                    }
                }
                else
                {
                    if (this.isHTML)
                    {
                        html = html.Replace("{@image@}", imageService.GetWebImagePath(product.VariantId));
                    }
                    else
                    {
                        html = html.Replace("{@image@}", imageService.GetObseleteImagePath(product.VariantId));
                    }
                }

                html += "<br /><br /><br /><br /><br /><br />\r\n\r\n                <!--<br /> <br /> <br /> <br /> <br /> <hr />\r\n\r\n\t\t\t\t<div class=\"product-info smart-form\">\r\n\t\t\t\t\t<div class=\"row\">\r\n\t\t\t\t\t\t<div class=\"col-md-12\"> \r\n\t\t\t\t\t\t\t<a href=\"javascript:void(0);\" class=\"btn btn-danger\">Add to cart</a>\r\n                            <a href=\"javascript:void(0);\" class=\"btn btn-info\">More info</a>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</div>-->\r\n\t\t\t</div>\r\n        </div>\r\n\t</div>\r\n\t<!-- end product -->\r\n    \r\n</div>";

                stringBuilder.AppendLine(html);

                interval++;
                pageInterval++;

                if (interval == 3)
                {
                    html += "</div>";
                }

                productsInterval++;
                productsAdded++;

                if (productsInterval == 30 || productsAdded == this.creatorService.GetProductsCount())
                {
                    stringBuilder.AppendLine(this.GetEndDesign());

                    HtmlToPdf converter = new HtmlToPdf();
                    converter.Options.PdfPageSize = PdfPageSize.A4;
                    converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                    converter.Options.PdfStandard = PdfStandard.PdfA;

                    converter.Options.DisplayHeader = true;
                    converter.Header.DisplayOnFirstPage = true;
                    converter.Header.DisplayOnOddPages = true;
                    converter.Header.DisplayOnEvenPages = true;
                    converter.Header.Height = 70;
                    PdfHtmlSection headerHtml = new PdfHtmlSection("<style>@import url('https://fonts.googleapis.com/css?family=Open+Sans'); .header { padding-top:20px; width: 100%; height:150px; text-align: center; background: #3F3F3F;  color: white;  font-size: 30px; font-family: \"Open Sans\", sans-serif;}</style><div class=\"header\"><h3>" + this.title + "</h3></div>", "");
                    headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                    converter.Header.Add(headerHtml);
                    converter.Options.DisplayFooter = true;
                    converter.Footer.DisplayOnFirstPage = true;
                    converter.Footer.DisplayOnOddPages = true;
                    converter.Footer.DisplayOnEvenPages = true;
                    converter.Footer.Height = 40;
                    PdfTextSection text2 = new PdfTextSection(170, 0, GetFooterContactInformation(this.creatorService.Language), new System.Drawing.Font("Arial", 8));
                    converter.Footer.Add(text2);
                    PdfTextSection dateText = new PdfTextSection(460, 30, "PDF Created [" + DateTime.Now + "]", new System.Drawing.Font("Arial", 8));
                    converter.Footer.Add(dateText);

                    converter.Options.JpegCompressionEnabled = true;
                    converter.Options.JpegCompressionLevel = 50;
                    converter.Options.PdfCompressionLevel = PdfCompressionLevel.Best;
                    converter.Options.ScaleImages = true;

                    try
                    {
                        PdfDocument pdfDocument = converter.ConvertHtmlString(this.stringBuilder.ToString(), @"\\192.168.1.21\Produktbilder");
                        this.pdfDocuments.Add(pdfDocument);
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError(ex.ToString());
                        return null;
                    }

                    this.stringBuilder.Clear();
                    productsInterval = 0;
                    pageInterval = 0;
                }
            }

            try
            {
                PdfDocument doc = new PdfDocument();

                foreach (var document in this.pdfDocuments)
                {
                    doc.Append(document);
                }

                doc.JpegCompressionEnabled = true;
                doc.JpegCompressionLevel = 50;
                doc.CompressionLevel = PdfCompressionLevel.Best;

                using (MemoryStream stream = new MemoryStream())
                {
                    doc.Save(stream);
                    Console.WriteLine(stream.Length);
                    //stream.Close();
                    doc.Close();

                    foreach (var document in this.pdfDocuments)
                    {
                        document.Close();
                    }

                    return stream;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
                return null;
            }
        }

        private string GetStartDesign()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>\r\n<html>\r\n<head>\r\n\r\n<link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css\" integrity=\"sha512-dTfge/zgoMYpP7QbHy4gWMEGsbsdZeCXz7irItjcC3sPUFtf0kuFbDz/ixG7ArTxmDjLXDmezHubeNikyKGVyQ==\" crossorigin=\"anonymous\">\r\n<link href=\"https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css\" rel=\"stylesheet\">");
            sb.AppendLine("<style>\r\nbody{\r\n /* to centre page on screen*/\r\n    margin-left: 25px;\r\n    margin-right: auto;\r\n    margin-top:20px;\r\n    background:#fff;\r\n}\r\n img { width: 230px;\r\n object-fit:contain;\r\n margin:auto; } @import url('https://fonts.googleapis.com/css?family=Open+Sans'); \r\n.header {\r\n  padding: 30px;\r\n  text-align: center;\r\n  background: #3F3F3F;\r\n  color: white;\r\n  font-size: 30px; font-family: \"Open Sans\", sans-serif;\r\n}\r\n\r\n.prod-info-main {\r\n height: 500px;\r\n    border: 1px solid #CEEFFF;\r\n    margin-bottom: 15px;\r\n    margin-top: 15px;\r\n    background: #fff;\r\n    padding: 6px;\r\n    -webkit-box-shadow: 0 1px 4px 0 rgba(255, 255, 255, 0.5);\r\n    box-shadow: 0 1px 1px 0 rgba(223, 223, 223, 0.5);\r\n}\r\n\r\n.prod-info-main .product-image {\r\n    background-color: #ffffff;\r\n    display: block;\r\n    min-height: 238px;\r\n    overflow: hidden;\r\n    position: relative;\r\n    border: 1px solid #f3f9fc;\r\n    padding-top: 0px;\r\n}\r\n\r\n.prod-info-main .product-deatil {\r\n    border-bottom: 1px solid #dfe5e9;\r\n    padding-bottom: 17px;\r\n    padding-left: 16px;\r\n    padding-top: 16px;\r\n    position: relative;\r\n    background: #fff\r\n}\r\n\r\n.product-content .product-deatil h5 a {\r\n    color: #2f383d;\r\n    font-size: 15px;\r\n    line-height: 19px;\r\n    text-decoration: none;\r\n    padding-left: 0;\r\n    margin-left: 0\r\n}\r\n\r\n.prod-info-main .product-deatil h5 a span {\r\n    color: #9aa7af;\r\n    display: block;\r\n    font-size: 13px\r\n}\r\n\r\n.prod-info-main .product-deatil span.tag1 {\r\n    border-radius: 50%;\r\n    color: #fff;\r\n    font-size: 15px;\r\n    height: 50px;\r\n    padding: 13px 0;\r\n    position: absolute;\r\n    right: 10px;\r\n    text-align: center;\r\n    top: 10px;\r\n    width: 50px\r\n}\r\n\r\n.prod-info-main .product-deatil span.sale {\r\n    background-color: #21c2f8\r\n}\r\n\r\n.prod-info-main .product-deatil span.discount {\r\n    background-color: #71e134\r\n}\r\n\r\n.prod-info-main .product-deatil span.hot {\r\n    background-color: #fa9442\r\n}\r\n\r\n.prod-info-main .description {\r\n    \r\n    font-size: 14px;\r\n    line-height: 20px;\r\n    padding: 10px 14px 16px 19px;\r\n    background: #fff\r\n}\r\n\r\n.prod-info-main .product-info {\r\n    padding: 11px 19px 10px 20px\r\n}\r\n\r\n.prod-info-main .product-info a.add-to-cart {\r\n    color: #2f383d;\r\n    font-size: 13px;\r\n    padding-left: 16px\r\n}\r\n\r\n.prod-info-main name.a {\r\n    padding: 5px 10px;\r\n    margin-left: 16px\r\n}\r\n\r\n.product-info.smart-form .btn {\r\n    padding: 6px 12px;\r\n    margin-left: 12px;\r\n    margin-top: -10px\r\n}\r\n\r\n.load-more-btn {\r\n    background-color: #21c2f8;\r\n    border-bottom: 2px solid #037ca5;\r\n    border-radius: 2px;\r\n    border-top: 2px solid #0cf;\r\n    margin-top: 20px;\r\n    padding: 9px 0;\r\n    width: 100%\r\n}\r\n\r\n.product-block .product-deatil p.price-container span,\r\n.prod-info-main .product-deatil p.price-container span,\r\n.shipping table tbody tr td p.price-container span,\r\n.shopping-items table tbody tr td p.price-container span {\r\n    color: #21c2f8;\r\n    font-family: Lato, sans-serif;\r\n    font-size: 24px;\r\n    line-height: 20px\r\n}\r\n\r\n.product-info.smart-form .rating label {\r\n    margin-top:15px;\r\n    \r\n}\r\n\r\n.prod-wrap .product-image span.tag2 {\r\n    position: absolute;\r\n    top: 10px;\r\n    right: 10px;\r\n    width: 36px;\r\n    height: 36px;\r\n    border-radius: 50%;\r\n    padding: 10px 0;\r\n    color: #fff;\r\n    font-size: 11px;\r\n    text-align: center\r\n}\r\n\r\n.prod-wrap .product-image span.tag3 {\r\n    position: absolute;\r\n    top: 10px;\r\n    right: 20px;\r\n    width: 60px;\r\n    height: 36px;\r\n    border-radius: 50%;\r\n    padding: 10px 0;\r\n    color: #fff;\r\n    font-size: 11px;\r\n    text-align: center\r\n}\r\n\r\n.prod-wrap .product-image span.sale {\r\n    background-color: #57889c;\r\n}\r\n\r\n.prod-wrap .product-image span.hot {\r\n    background-color: #a90329;\r\n}\r\n\r\n.prod-wrap .product-image span.special {\r\n    background-color: #3B6764;\r\n}\r\n.shop-btn {\r\n    position: relative\r\n}\r\n\r\n.shop-btn>span {\r\n    background: #a90329;\r\n    display: inline-block;\r\n    font-size: 10px;\r\n    box-shadow: inset 1px 1px 0 rgba(0, 0, 0, .1), inset 0 -1px 0 rgba(0, 0, 0, .07);\r\n    font-weight: 700;\r\n    border-radius: 50%;\r\n    padding: 2px 4px 3px!important;\r\n    text-align: center;\r\n    line-height: normal;\r\n    width: 19px;\r\n    top: -7px;\r\n    left: -7px\r\n}\r\n\r\n.product-deatil hr {\r\n    padding: 0 0 5px!important\r\n}\r\n\r\n.product-deatil .glyphicon {\r\n    color: #3276b1\r\n}\r\n\r\n.product-deatil .product-image {\r\n    border-right: 0px solid #fff !important\r\n}\r\n\r\n.product-deatil .name {\r\n    margin-top: 0;\r\n    margin-bottom: 0\r\n}\r\n\r\n.product-deatil .name small {\r\n    display: block\r\n}\r\n\r\n.product-deatil .name a {\r\n    margin-left: 0\r\n}\r\n\r\n.product-deatil .price-container {\r\n    font-size: 24px;\r\n    margin: 0;\r\n    font-weight: 300;\r\n    \r\n}\r\n\r\n.product-deatil .price-container small {\r\n    font-size: 12px;\r\n    \r\n}\r\n\r\n.product-deatil .fa-2x {\r\n    font-size: 16px!important\r\n}\r\n\r\n.product-deatil .fa-2x>h5 {\r\n    font-size: 12px;\r\n    margin: 0\r\n}\r\n\r\n.product-deatil .fa-2x+a,\r\n.product-deatil .fa-2x+a+a {\r\n    font-size: 13px\r\n}\r\n\r\n.product-deatil .certified {\r\n    margin-top: 10px\r\n}\r\n\r\n.product-deatil .certified ul {\r\n    padding-left: 0\r\n}\r\n\r\n.product-deatil .certified ul li:not(first-child) {\r\n    margin-left: -3px\r\n}\r\n\r\n.product-deatil .certified ul li {\r\n    display: inline-block;\r\n    background-color: #f9f9f9;\r\n    padding: 13px 19px\r\n}\r\n\r\n.product-deatil .certified ul li:first-child {\r\n    border-right: none\r\n}\r\n\r\n.product-deatil .certified ul li a {\r\n    text-align: left;\r\n    font-size: 12px;\r\n    color: #6d7a83;\r\n    line-height: 16px;\r\n    text-decoration: none\r\n}\r\n\r\n.product-deatil .certified ul li a span {\r\n    display: block;\r\n    color: #21c2f8;\r\n    font-size: 13px;\r\n    font-weight: 700;\r\n    text-align: center\r\n}\r\n\r\n.product-deatil .message-text {\r\n    width: calc(100% - 70px)\r\n}\r\n\r\n@media only screen and (min-width:1024px) {\r\n    .prod-info-main div[class*=col-md-4] {\r\n        padding-right: 0\r\n    }\r\n    .prod-info-main div[class*=col-md-8] {\r\n        padding: 0 13px 0 0\r\n    }\r\n    .prod-wrap div[class*=col-md-5] {\r\n        padding-right: 0\r\n    }\r\n    .prod-wrap div[class*=col-md-7] {\r\n        padding: 0 13px 0 0\r\n    }\r\n    .prod-info-main .product-image {\r\n        border-right: 1px solid #dfe5e9\r\n    }\r\n    .prod-info-main .product-info {\r\n        position: relative\r\n    }\r\n}\r\n</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>\r\n<div class=\"container\" style=\"float:left\">");

            return sb.ToString();
        }

        private string GetEndDesign()
        {
            return "</div>\r\n</body>\r\n</html>\r\n";
        }

        private string AddDataRow(string variable, string data)
        {
            string htmlData = string.Empty;
            htmlData += "<div style='float:left; width:30%; margin-left:0px'>";
            htmlData += "<b>" + variable + "</b>:";
            htmlData += "</div>";
            htmlData += "<div style='float:left; width:40%; margin-left:60px'>";
            htmlData += data;
            htmlData += "</div><br />";

            return htmlData;
        }

        private string AddDataRowWithLink(string variable, string data, string link)
        {
            string htmlData = string.Empty;
            htmlData += "<div style='float:left; width:30%; margin-left:0px'>";
            htmlData += "<b>" + variable + "</b>:";
            htmlData += "</div>";
            htmlData += "<div style='float:left; width:40%; margin-left:60px'>";
            htmlData += "<a href=\"{@link@}\" style=\"color: black;\">" + data + "</a>";
            htmlData += "</div><br />";

            return htmlData.Replace("{@link@}", link);
        }

        public string GetFooterContactInformation(string language)
        {
            if (language.ToLower().Contains("swedish"))
            {
                return "Tura Scandinavia AB - Tura +46 (0)300 56 89 20  info@turascandinavia.com";
            }
            else if (language.ToLower().Contains("norwegian"))
            {
                return "Tura Scandinavia AB - Tura +47 22 62 74 80 ordre@turascandinavia.com";
            }
            else if (language.ToLower().Contains("finnish"))
            {
                return "Tura Scandinavia AB - Tura +358 (0)207 600 950 finland@turascandinavia.com";
            }
            else if (language.ToLower().Contains("danish"))
            {
                return "Tura Scandinavia AB - Tura +45 48 18 78 81 dk@turascandinavia.com";
            }
            else if (language.ToLower().Contains("english"))
            {
                return "Tura Scandinavia AB - Tura +46 (0)300 56 89 20 info@turascandinavia.com";
            }
            else
            {
                return "Tura Scandinavia AB - Tura +46 (0)300 56 89 20  info@turascandinavia.com";
            }
        }
    }
}
