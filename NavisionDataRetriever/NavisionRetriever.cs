using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NavisionDataRetriever
{
    public class NavisionRetriever
    {
        private ILogger<NavisionRetriever> logger;
        private string _soap { get; set; }
        private string[] _productIds { get; set; }
        private string _customerId { get; set; }
        private string _currencyCode { get; set; }

        public NavisionRetriever(ILogger<NavisionRetriever> _logger)
        {
            this.logger = _logger;
        }

        /// <summary>
        /// Prepares the required fields and SOAP request
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="customerId"></param>
        /// <param name="currencyCode"></param>
        public void PrepareRequest(string[] productIds, string customerId, string currencyCode)
        {
            this._soap = string.Empty;
            this._productIds = productIds;
            this._customerId = customerId;
            this._currencyCode = currencyCode;

            this.prepareSoap();
        }

        /// <summary>
        /// Prepares SOAP data
        /// </summary>
        private void prepareSoap()
        {
            string timeZone = TimeZoneInfo.Local.BaseUtcOffset.ToString();
            string date = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
            date += "+" + timeZone.Remove(5, timeZone.Count() - 5);

            this._soap += @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:wsm=""urn:microsoft-dynamics-schemas/codeunit/WSMiscFunctions"" xmlns:wsit=""urn:microsoft-dynamics-nav/xmlports/WSItemSalesPrice"">
    <soapenv:Header/>
    <soapenv:Body>
        <wsm:ItemCustomerPriceBulk>
            <!--1 or more repetitions:-->";

            foreach (string id in this._productIds)
            {
                this._soap += "<wsm:arrItem>" + id + "</wsm:arrItem>";
            }

            this._soap += "<wsm:customerNo>" + this._customerId + "</wsm:customerNo>";
            this._soap += "<wsm:currencyCode>" + this._currencyCode + "</wsm:currencyCode>";
            this._soap += "<wsm:date>" + date + "</wsm:date>";

            this._soap += @"<wsm:itemPriceBulkXML>
                            <!--Zero or more repetitions:-->";

            this._soap += "<wsit:ItemPriceBulk>";
            this._soap += @"<wsit:ItemNo/>
                                <!--Zero or more repetitions:-->";
            this._soap += @"<wsit:Prices>
                            <!--1 or more repetitions:-->";
            this._soap += "<wsit:Price>?</wsit:Price>";
            this._soap += "<wsit:Currency>?</wsit:Currency>";
            this._soap += "<wsit:MinQuantity>0</wsit:MinQuantity>";
            this._soap += "<wsit:SalesCode>?</wsit:SalesCode>";
            this._soap += "</wsit:Prices>";
            this._soap += "</wsit:ItemPriceBulk>";

            this._soap += "</wsm:itemPriceBulkXML>";
            this._soap += "</wsm:ItemCustomerPriceBulk>";
            this._soap += "</soapenv:Body>";
            this._soap += "</soapenv:Envelope>";
        }

        /// <summary>
        /// Gets the prices
        /// </summary>
        /// <returns>XML String containing related data</returns>
        private string loadPrices()
        {
            if(this._soap == null || this._productIds == null || this._productIds.Length == 0 || this._customerId == null || this._currencyCode == null)
            {
                return string.Empty;
            }

            try
            {
                string data = string.Empty;

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://NAVSVC1.tura.local:7047/DynamicsNAV110/WS/Tura%20Scandinavia%20AB/Codeunit/WSMiscFunctions");
                req.ContentType = "text/xml;";
                req.Method = "POST";
                req.Headers.Add("Accept", "*/*");
                req.Headers.Add("SOAPAction", @"'#GET'");
                req.Credentials = new NetworkCredential("readernav", "1234QWer");

                using (Stream stm = req.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stm))
                    {
                        stmw.Write(this._soap);
                    }
                }

                using (WebResponse response = req.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            data = reader.ReadToEnd();
                        }
                    }
                }

                return data;
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// Reads the XML string and reads the data
        /// </summary>
        /// <returns>Dictionary<ProductId, CustomerPrice></returns>
        public Dictionary<string, string> GetPrices()
        {
            Dictionary<string, string> itemPrices = new Dictionary<string, string>(); //Id / Price
            string pricesXml = this.loadPrices();

            try
            {
                if (pricesXml == null)
                {
                    return itemPrices;
                }

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(pricesXml);

                if (xmlDocument == null)
                {
                    return null;
                }

                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDocument.NameTable);
                manager.AddNamespace("bhr", "urn:microsoft-dynamics-nav/xmlports/WSItemSalesPrice");

                if (manager == null)
                {
                    return null;
                }

                XmlNodeList xnList = xmlDocument.SelectNodes("//bhr:ItemPriceBulk", manager);
                XmlNodeList xnPrices = xmlDocument.SelectNodes("//bhr:Prices", manager);

                if (xnList != null && xnPrices != null && (xnList.Count >= xnPrices.Count))
                {
                    int i = 0;
                    foreach (XmlNode xn in xnList)
                    {
                        string id = xn["ItemNo"].InnerText;
                        string price = xnPrices[i]["Price"].InnerText;

                        if(price.Contains(","))
                        {
                            price = price.Replace(",", "");
                        }

                        if(price.Contains("."))
                        {
                            price = price.Replace(".", ",");
                        }

                        if (id != null && price != null)
                        {
                            //CustomerItemData customerItemData = new CustomerItemData(id, price);
                            itemPrices.Add(id, price);
                        }

                        i++;
                    }
                }
            }
            catch(Exception e)
            {
                this.logger.LogError(e, e.ToString());
                return null;
            }

            return itemPrices;
        }
    }
}
