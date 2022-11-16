﻿using System.Text;

namespace TuraProductsViewer.Services
{
    public class FileCreatorService
    {
        public string HTML(CreatorService creatorService, ImageService imageService, bool isHTML)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>\r\n<html>\r\n<head>\r\n\r\n<link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css\" integrity=\"sha512-dTfge/zgoMYpP7QbHy4gWMEGsbsdZeCXz7irItjcC3sPUFtf0kuFbDz/ixG7ArTxmDjLXDmezHubeNikyKGVyQ==\" crossorigin=\"anonymous\">\r\n<link href=\"https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css\" rel=\"stylesheet\">");
           if(isHTML)
            {
                sb.AppendLine("<style>\r\nbody{\r\n    height:210mm;\r\n    width:297mm;\r\n    /* to centre page on screen*/\r\n    margin-left: auto;\r\n    margin-right: auto;\r\n    margin-top:20px;\r\n    background:#fff;\r\n}\r\n\r\n.header {\r\n  padding: 30px;\r\n  text-align: center;\r\n  background: #3F3F3F;\r\n  color: white;\r\n  font-size: 30px;\r\n}\r\n\r\n.prod-info-main {\r\n    height: 480px;\r\n    border: 1px solid #CEEFFF;\r\n    margin-bottom: 20px;\r\n    margin-top: 10px;\r\n    background: #fff;\r\n    padding: 6px;\r\n    -webkit-box-shadow: 0 1px 4px 0 rgba(255, 255, 255, 0.5);\r\n    box-shadow: 0 1px 1px 0 rgba(223, 223, 223, 0.5);\r\n}\r\n\r\n.prod-info-main .product-image {\r\n    background-color: #f3f9fc;\r\n    display: block;\r\n    min-height: 238px;\r\n    overflow: hidden;\r\n    position: relative;\r\n    border: 1px solid #f3f9fc;\r\n    padding-top: 40px;\r\n}\r\n\r\n.prod-info-main .product-deatil {\r\n    border-bottom: 1px solid #dfe5e9;\r\n    padding-bottom: 17px;\r\n    padding-left: 16px;\r\n    padding-top: 16px;\r\n    position: relative;\r\n    background: #fff\r\n}\r\n\r\n.product-content .product-deatil h5 a {\r\n    color: #2f383d;\r\n    font-size: 15px;\r\n    line-height: 19px;\r\n    text-decoration: none;\r\n    padding-left: 0;\r\n    margin-left: 0\r\n}\r\n\r\n.prod-info-main .product-deatil h5 a span {\r\n    color: #9aa7af;\r\n    display: block;\r\n    font-size: 13px\r\n}\r\n\r\n.prod-info-main .product-deatil span.tag1 {\r\n    border-radius: 50%;\r\n    color: #fff;\r\n    font-size: 15px;\r\n    height: 50px;\r\n    padding: 13px 0;\r\n    position: absolute;\r\n    right: 10px;\r\n    text-align: center;\r\n    top: 10px;\r\n    width: 50px\r\n}\r\n\r\n.prod-info-main .product-deatil span.sale {\r\n    background-color: #21c2f8\r\n}\r\n\r\n.prod-info-main .product-deatil span.discount {\r\n    background-color: #71e134\r\n}\r\n\r\n.prod-info-main .product-deatil span.hot {\r\n    background-color: #fa9442\r\n}\r\n\r\n.prod-info-main .description {\r\n    \r\n    font-size: 14px;\r\n    line-height: 20px;\r\n    padding: 10px 14px 16px 19px;\r\n    background: #fff\r\n}\r\n\r\n.prod-info-main .product-info {\r\n    padding: 11px 19px 10px 20px\r\n}\r\n\r\n.prod-info-main .product-info a.add-to-cart {\r\n    color: #2f383d;\r\n    font-size: 13px;\r\n    padding-left: 16px\r\n}\r\n\r\n.prod-info-main name.a {\r\n    padding: 5px 10px;\r\n    margin-left: 16px\r\n}\r\n\r\n.product-info.smart-form .btn {\r\n    padding: 6px 12px;\r\n    margin-left: 12px;\r\n    margin-top: -10px\r\n}\r\n\r\n.load-more-btn {\r\n    background-color: #21c2f8;\r\n    border-bottom: 2px solid #037ca5;\r\n    border-radius: 2px;\r\n    border-top: 2px solid #0cf;\r\n    margin-top: 20px;\r\n    padding: 9px 0;\r\n    width: 100%\r\n}\r\n\r\n.product-block .product-deatil p.price-container span,\r\n.prod-info-main .product-deatil p.price-container span,\r\n.shipping table tbody tr td p.price-container span,\r\n.shopping-items table tbody tr td p.price-container span {\r\n    color: #21c2f8;\r\n    font-family: Lato, sans-serif;\r\n    font-size: 24px;\r\n    line-height: 20px\r\n}\r\n\r\n.product-info.smart-form .rating label {\r\n    margin-top:15px;\r\n    \r\n}\r\n\r\n.prod-wrap .product-image span.tag2 {\r\n    position: absolute;\r\n    top: 10px;\r\n    right: 10px;\r\n    width: 36px;\r\n    height: 36px;\r\n    border-radius: 50%;\r\n    padding: 10px 0;\r\n    color: #fff;\r\n    font-size: 11px;\r\n    text-align: center\r\n}\r\n\r\n.prod-wrap .product-image span.tag3 {\r\n    position: absolute;\r\n    top: 10px;\r\n    right: 20px;\r\n    width: 60px;\r\n    height: 36px;\r\n    border-radius: 50%;\r\n    padding: 10px 0;\r\n    color: #fff;\r\n    font-size: 11px;\r\n    text-align: center\r\n}\r\n\r\n.prod-wrap .product-image span.sale {\r\n    background-color: #57889c;\r\n}\r\n\r\n.prod-wrap .product-image span.hot {\r\n    background-color: #a90329;\r\n}\r\n\r\n.prod-wrap .product-image span.special {\r\n    background-color: #3B6764;\r\n}\r\n.shop-btn {\r\n    position: relative\r\n}\r\n\r\n.shop-btn>span {\r\n    background: #a90329;\r\n    display: inline-block;\r\n    font-size: 10px;\r\n    box-shadow: inset 1px 1px 0 rgba(0, 0, 0, .1), inset 0 -1px 0 rgba(0, 0, 0, .07);\r\n    font-weight: 700;\r\n    border-radius: 50%;\r\n    padding: 2px 4px 3px!important;\r\n    text-align: center;\r\n    line-height: normal;\r\n    width: 19px;\r\n    top: -7px;\r\n    left: -7px\r\n}\r\n\r\n.product-deatil hr {\r\n    padding: 0 0 5px!important\r\n}\r\n\r\n.product-deatil .glyphicon {\r\n    color: #3276b1\r\n}\r\n\r\n.product-deatil .product-image {\r\n    border-right: 0px solid #fff !important\r\n}\r\n\r\n.product-deatil .name {\r\n    margin-top: 0;\r\n    margin-bottom: 0\r\n}\r\n\r\n.product-deatil .name small {\r\n    display: block\r\n}\r\n\r\n.product-deatil .name a {\r\n    margin-left: 0\r\n}\r\n\r\n.product-deatil .price-container {\r\n    font-size: 24px;\r\n    margin: 0;\r\n    font-weight: 300;\r\n    \r\n}\r\n\r\n.product-deatil .price-container small {\r\n    font-size: 12px;\r\n    \r\n}\r\n\r\n.product-deatil .fa-2x {\r\n    font-size: 16px!important\r\n}\r\n\r\n.product-deatil .fa-2x>h5 {\r\n    font-size: 12px;\r\n    margin: 0\r\n}\r\n\r\n.product-deatil .fa-2x+a,\r\n.product-deatil .fa-2x+a+a {\r\n    font-size: 13px\r\n}\r\n\r\n.product-deatil .certified {\r\n    margin-top: 10px\r\n}\r\n\r\n.product-deatil .certified ul {\r\n    padding-left: 0\r\n}\r\n\r\n.product-deatil .certified ul li:not(first-child) {\r\n    margin-left: -3px\r\n}\r\n\r\n.product-deatil .certified ul li {\r\n    display: inline-block;\r\n    background-color: #f9f9f9;\r\n    padding: 13px 19px\r\n}\r\n\r\n.product-deatil .certified ul li:first-child {\r\n    border-right: none\r\n}\r\n\r\n.product-deatil .certified ul li a {\r\n    text-align: left;\r\n    font-size: 12px;\r\n    color: #6d7a83;\r\n    line-height: 16px;\r\n    text-decoration: none\r\n}\r\n\r\n.product-deatil .certified ul li a span {\r\n    display: block;\r\n    color: #21c2f8;\r\n    font-size: 13px;\r\n    font-weight: 700;\r\n    text-align: center\r\n}\r\n\r\n.product-deatil .message-text {\r\n    width: calc(100% - 70px)\r\n}\r\n\r\n@media only screen and (min-width:1024px) {\r\n    .prod-info-main div[class*=col-md-4] {\r\n        padding-right: 0\r\n    }\r\n    .prod-info-main div[class*=col-md-8] {\r\n        padding: 0 13px 0 0\r\n    }\r\n    .prod-wrap div[class*=col-md-5] {\r\n        padding-right: 0\r\n    }\r\n    .prod-wrap div[class*=col-md-7] {\r\n        padding: 0 13px 0 0\r\n    }\r\n    .prod-info-main .product-image {\r\n        border-right: 1px solid #dfe5e9\r\n    }\r\n    .prod-info-main .product-info {\r\n        position: relative\r\n    }\r\n}\r\n</style>");
            }
           else
            {
                sb.AppendLine("<style>\r\nbody{\r\n /* to centre page on screen*/\r\n    margin-left: auto;\r\n    margin-right: auto;\r\n    margin-top:20px;\r\n    background:#fff;\r\n}\r\n\r\n.header {\r\n  padding: 30px;\r\n  text-align: center;\r\n  background: #3F3F3F;\r\n  color: white;\r\n  font-size: 30px;\r\n}\r\n\r\n.prod-info-main {\r\n    height: 480px;\r\n    border: 1px solid #CEEFFF;\r\n    margin-bottom: 20px;\r\n    margin-top: 10px;\r\n    background: #fff;\r\n    padding: 6px;\r\n    -webkit-box-shadow: 0 1px 4px 0 rgba(255, 255, 255, 0.5);\r\n    box-shadow: 0 1px 1px 0 rgba(223, 223, 223, 0.5);\r\n}\r\n\r\n.prod-info-main .product-image {\r\n    background-color: #f3f9fc;\r\n    display: block;\r\n    min-height: 238px;\r\n    overflow: hidden;\r\n    position: relative;\r\n    border: 1px solid #f3f9fc;\r\n    padding-top: 40px;\r\n}\r\n\r\n.prod-info-main .product-deatil {\r\n    border-bottom: 1px solid #dfe5e9;\r\n    padding-bottom: 17px;\r\n    padding-left: 16px;\r\n    padding-top: 16px;\r\n    position: relative;\r\n    background: #fff\r\n}\r\n\r\n.product-content .product-deatil h5 a {\r\n    color: #2f383d;\r\n    font-size: 15px;\r\n    line-height: 19px;\r\n    text-decoration: none;\r\n    padding-left: 0;\r\n    margin-left: 0\r\n}\r\n\r\n.prod-info-main .product-deatil h5 a span {\r\n    color: #9aa7af;\r\n    display: block;\r\n    font-size: 13px\r\n}\r\n\r\n.prod-info-main .product-deatil span.tag1 {\r\n    border-radius: 50%;\r\n    color: #fff;\r\n    font-size: 15px;\r\n    height: 50px;\r\n    padding: 13px 0;\r\n    position: absolute;\r\n    right: 10px;\r\n    text-align: center;\r\n    top: 10px;\r\n    width: 50px\r\n}\r\n\r\n.prod-info-main .product-deatil span.sale {\r\n    background-color: #21c2f8\r\n}\r\n\r\n.prod-info-main .product-deatil span.discount {\r\n    background-color: #71e134\r\n}\r\n\r\n.prod-info-main .product-deatil span.hot {\r\n    background-color: #fa9442\r\n}\r\n\r\n.prod-info-main .description {\r\n    \r\n    font-size: 14px;\r\n    line-height: 20px;\r\n    padding: 10px 14px 16px 19px;\r\n    background: #fff\r\n}\r\n\r\n.prod-info-main .product-info {\r\n    padding: 11px 19px 10px 20px\r\n}\r\n\r\n.prod-info-main .product-info a.add-to-cart {\r\n    color: #2f383d;\r\n    font-size: 13px;\r\n    padding-left: 16px\r\n}\r\n\r\n.prod-info-main name.a {\r\n    padding: 5px 10px;\r\n    margin-left: 16px\r\n}\r\n\r\n.product-info.smart-form .btn {\r\n    padding: 6px 12px;\r\n    margin-left: 12px;\r\n    margin-top: -10px\r\n}\r\n\r\n.load-more-btn {\r\n    background-color: #21c2f8;\r\n    border-bottom: 2px solid #037ca5;\r\n    border-radius: 2px;\r\n    border-top: 2px solid #0cf;\r\n    margin-top: 20px;\r\n    padding: 9px 0;\r\n    width: 100%\r\n}\r\n\r\n.product-block .product-deatil p.price-container span,\r\n.prod-info-main .product-deatil p.price-container span,\r\n.shipping table tbody tr td p.price-container span,\r\n.shopping-items table tbody tr td p.price-container span {\r\n    color: #21c2f8;\r\n    font-family: Lato, sans-serif;\r\n    font-size: 24px;\r\n    line-height: 20px\r\n}\r\n\r\n.product-info.smart-form .rating label {\r\n    margin-top:15px;\r\n    \r\n}\r\n\r\n.prod-wrap .product-image span.tag2 {\r\n    position: absolute;\r\n    top: 10px;\r\n    right: 10px;\r\n    width: 36px;\r\n    height: 36px;\r\n    border-radius: 50%;\r\n    padding: 10px 0;\r\n    color: #fff;\r\n    font-size: 11px;\r\n    text-align: center\r\n}\r\n\r\n.prod-wrap .product-image span.tag3 {\r\n    position: absolute;\r\n    top: 10px;\r\n    right: 20px;\r\n    width: 60px;\r\n    height: 36px;\r\n    border-radius: 50%;\r\n    padding: 10px 0;\r\n    color: #fff;\r\n    font-size: 11px;\r\n    text-align: center\r\n}\r\n\r\n.prod-wrap .product-image span.sale {\r\n    background-color: #57889c;\r\n}\r\n\r\n.prod-wrap .product-image span.hot {\r\n    background-color: #a90329;\r\n}\r\n\r\n.prod-wrap .product-image span.special {\r\n    background-color: #3B6764;\r\n}\r\n.shop-btn {\r\n    position: relative\r\n}\r\n\r\n.shop-btn>span {\r\n    background: #a90329;\r\n    display: inline-block;\r\n    font-size: 10px;\r\n    box-shadow: inset 1px 1px 0 rgba(0, 0, 0, .1), inset 0 -1px 0 rgba(0, 0, 0, .07);\r\n    font-weight: 700;\r\n    border-radius: 50%;\r\n    padding: 2px 4px 3px!important;\r\n    text-align: center;\r\n    line-height: normal;\r\n    width: 19px;\r\n    top: -7px;\r\n    left: -7px\r\n}\r\n\r\n.product-deatil hr {\r\n    padding: 0 0 5px!important\r\n}\r\n\r\n.product-deatil .glyphicon {\r\n    color: #3276b1\r\n}\r\n\r\n.product-deatil .product-image {\r\n    border-right: 0px solid #fff !important\r\n}\r\n\r\n.product-deatil .name {\r\n    margin-top: 0;\r\n    margin-bottom: 0\r\n}\r\n\r\n.product-deatil .name small {\r\n    display: block\r\n}\r\n\r\n.product-deatil .name a {\r\n    margin-left: 0\r\n}\r\n\r\n.product-deatil .price-container {\r\n    font-size: 24px;\r\n    margin: 0;\r\n    font-weight: 300;\r\n    \r\n}\r\n\r\n.product-deatil .price-container small {\r\n    font-size: 12px;\r\n    \r\n}\r\n\r\n.product-deatil .fa-2x {\r\n    font-size: 16px!important\r\n}\r\n\r\n.product-deatil .fa-2x>h5 {\r\n    font-size: 12px;\r\n    margin: 0\r\n}\r\n\r\n.product-deatil .fa-2x+a,\r\n.product-deatil .fa-2x+a+a {\r\n    font-size: 13px\r\n}\r\n\r\n.product-deatil .certified {\r\n    margin-top: 10px\r\n}\r\n\r\n.product-deatil .certified ul {\r\n    padding-left: 0\r\n}\r\n\r\n.product-deatil .certified ul li:not(first-child) {\r\n    margin-left: -3px\r\n}\r\n\r\n.product-deatil .certified ul li {\r\n    display: inline-block;\r\n    background-color: #f9f9f9;\r\n    padding: 13px 19px\r\n}\r\n\r\n.product-deatil .certified ul li:first-child {\r\n    border-right: none\r\n}\r\n\r\n.product-deatil .certified ul li a {\r\n    text-align: left;\r\n    font-size: 12px;\r\n    color: #6d7a83;\r\n    line-height: 16px;\r\n    text-decoration: none\r\n}\r\n\r\n.product-deatil .certified ul li a span {\r\n    display: block;\r\n    color: #21c2f8;\r\n    font-size: 13px;\r\n    font-weight: 700;\r\n    text-align: center\r\n}\r\n\r\n.product-deatil .message-text {\r\n    width: calc(100% - 70px)\r\n}\r\n\r\n@media only screen and (min-width:1024px) {\r\n    .prod-info-main div[class*=col-md-4] {\r\n        padding-right: 0\r\n    }\r\n    .prod-info-main div[class*=col-md-8] {\r\n        padding: 0 13px 0 0\r\n    }\r\n    .prod-wrap div[class*=col-md-5] {\r\n        padding-right: 0\r\n    }\r\n    .prod-wrap div[class*=col-md-7] {\r\n        padding: 0 13px 0 0\r\n    }\r\n    .prod-info-main .product-image {\r\n        border-right: 1px solid #dfe5e9\r\n    }\r\n    .prod-info-main .product-info {\r\n        position: relative\r\n    }\r\n}\r\n</style>");
            }
            sb.AppendLine("</head>");
            sb.AppendLine("<body>\r\n<div class=\"container\" style=\"float:left\"><div class=\"header\">\r\n<h2>Tura Scandinavia</h2>\r\n</div>");

            foreach (var product in creatorService.GetProducts())
            {
                string html = "<div class=\"col-xs-12 col-md-3\" style=\"width:33.3%;\">\r\n\t<!-- First product box start here-->\r\n\t<div class=\"prod-info-main prod-wrap clearfix\">\r\n\t\t<div class=\"row\">\r\n\t\t\t\t<div class=\"col-md-12 col-sm-12 col-xs-12\">\r\n\t\t\t\t\t<div class=\"product-image\"> \r\n\t\t\t\t\t\t<img src=\"{@image@}\" class=\"img-responsive\"> \r\n\t\t\t\t\t</div>\r\n\t\t\t\t</div>\r\n\t\t</div>\r\n        <div class=\"row\">\r\n            <div class=\"col-md-12 col-sm-12 col-xs-12\">\r\n\t\t\t\t<div class=\"product-detail\">\r\n                    <h5 class=\"name\">\r\n                        <p>{@productname@} </p>\r\n                        <small><p>{@artnr@}</p></small>\r\n                    </h5>\r\n\t\t\t\t</div>\r\n\r\n\t\t\t\t<div class=\"description\">\r\n\r\n                    <div style='float:left; width:30%'>\r\n                        Art Nr:\r\n                    </div>\r\n                    <div style='float:left; width:40%; margin-left:30px'>\r\n                        {@artnr@}\r\n                    </div>\r\n\r\n                    <div style='float:left; width:30%'>\r\n                        Varumärke: \r\n                    </div>\r\n                    <div style='float:left; width:40%; margin-left:30px'>\r\n                        {@varumarke@}\r\n                    </div>\r\n\r\n                    <div style='float:left; width:30%'>\r\n                        Rek. Pris: \r\n                    </div>\r\n                    <div style='float:left; width:40%; margin-left:30px'>\r\n                        {@rekpris@}\r\n                    </div>\r\n\r\n                    <div style='float:left; width:30%'>\r\n                        EAN: \r\n                    </div>\r\n                    <div style='float:left; width:40%; margin-left:30px'>\r\n                        {@ean@}\r\n                    </div>\r\n\r\n                    <div style='float:left; width:30%'>\r\n                        Frp Stl: \r\n                    </div>\r\n                    <div style='float:left; width:40%; margin-left:30px'>\r\n                        {@frpstl@}\r\n                    </div>\r\n\r\n                    <div style='float:left; width:30%'>\r\n                        I Lager: \r\n                    </div>\r\n                    <div style='float:left; width:40%; margin-left:30px'>\r\n                        {@ilager@}\r\n                    </div>\r\n\t\t\t\t</div>\r\n                \r\n                <br /><br /><br /><br /><br /><br />\r\n\r\n                <!--<br /> <br /> <br /> <br /> <br /> <hr />\r\n\r\n\t\t\t\t<div class=\"product-info smart-form\">\r\n\t\t\t\t\t<div class=\"row\">\r\n\t\t\t\t\t\t<div class=\"col-md-12\"> \r\n\t\t\t\t\t\t\t<a href=\"javascript:void(0);\" class=\"btn btn-danger\">Add to cart</a>\r\n                            <a href=\"javascript:void(0);\" class=\"btn btn-info\">More info</a>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</div>-->\r\n\t\t\t</div>\r\n        </div>\r\n\t</div>\r\n\t<!-- end product -->\r\n    \r\n</div>";
                html = html.Replace("{@productname@}", product.GetItemName(creatorService.Language));
                html = html.Replace("{@artnr@}", product.VariantId);
                html = html.Replace("{@varumarke@}", product.ManufacturerItemNo);
                html = html.Replace("{@rekpris@}", product.UnitPrice.ToString());
                html = html.Replace("{@ean@}", "N/A");
                html = html.Replace("{@frpstl@}", "N/A");
                html = html.Replace("{@ilager@}", "N/A");
                html = html.Replace("{@image@}", imageService.GetImagePath(product.VariantId));

                sb.AppendLine(html);
            }

            sb.AppendLine("</div>\r\n</body>\r\n</html>\r\n");

            return sb.ToString();
        }
    }
}