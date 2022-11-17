using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRetriever.Models
{
    public class ProductsDataModel
    {
        //ProductDescriptions
        public string VariantId { get; set; } = null!;
        public string? BaseId { get; set; }
        public string? Gtin { get; set; }
        public int? ActivityCode { get; set; }
        public string? ManufacturerItemNo { get; set; }
        public string? Brand { get; set; }
        public string? SeItemName { get; set; }
        public string? SeItemText { get; set; }
        public string? NoItemName { get; set; }
        public string? NoItemText { get; set; }
        public string? DkItemName { get; set; }
        public string? DkItemText { get; set; }
        public string? FiItemName { get; set; }
        public string? FiItemText { get; set; }
        public string? EnItemName { get; set; }
        public string? EnItemText { get; set; }

        //SalesPrices
        public string Timestamp { get; set; } = null!;
        public string ItemNo { get; set; } = null!;
        public int SalesType { get; set; }
        public string SalesCode { get; set; } = null!;
        public DateTime StartingDate { get; set; }
        public string CurrencyCode { get; set; } = null!;
        public string VariantCode { get; set; } = null!;
        public string UnitOfMeasureCode { get; set; } = null!;
        public decimal MinimumQuantity { get; set; }
        //public decimal UnitPrice { get; set; }
        public double UnitPrice { get; set; }
        public byte PriceIncludesVat { get; set; }
        public byte AllowInvoiceDisc { get; set; }
        public string VatBusPostingGrPrice { get; set; } = null!;
        public DateTime EndingDate { get; set; }
        public byte AllowLineDisc { get; set; }

        //TiNavItem data
        public string PrimaryEANCode { get; set; }
        public string UnitOfMeasure { get; set; }

        //TiNavQtyTmp
        public decimal AvailableQty { get; set; }

        //TiNavItemUnitOfMeasure
        public decimal QtyPerUnitOfMeasure { get; set; }

        //Price withtout vat
        public decimal UnitPriceWithoutVat { get; set; }

        /// <summary>
        /// Sets all the variables Data
        /// </summary>
        public void SetData(string variandId, string? baseId, string? gtin, int? activityCode, string? manufacturerItemNo,
                                                string? brand, string? seItemName, string? seItemText, string? noItemName, string? noItemText,
                                                string? dkItemName, string? dkItemText, string? fiItemName, string? fiItemText, string? enItemName,
                                                string? enItemText, string timestamp, string itemNo, int salesType, string salesCode, DateTime startingDate,
                                                string currencyCode, string variantCode, string unitOfMeasureCode, decimal minimumQuantity, decimal unitPrice,
                                                byte priceIncludesVat, byte allowInvoiceDisc, string vatBusPostingGrPrice, DateTime endingDate, byte allowLineDisc)
        {
            this.VariantId = variandId;
            this.BaseId = baseId;
            this.Gtin = gtin;
            this.ActivityCode = activityCode;
            this.ManufacturerItemNo = manufacturerItemNo;
            this.Brand = brand;
            this.SeItemName = seItemName;
            this.SeItemText = seItemText;
            this.NoItemName = noItemName;
            this.NoItemText = noItemText;
            this.DkItemName = dkItemName;
            this.DkItemText = dkItemText;
            this.FiItemName = fiItemName;
            this.FiItemText = fiItemText;
            this.EnItemName = enItemName;
            this.EnItemText = enItemText;
            this.Timestamp = timestamp;
            this.ItemNo = itemNo;
            this.SalesType = salesType;
            this.SalesCode = salesCode;
            this.StartingDate = startingDate;
            this.CurrencyCode = currencyCode;
            this.VariantCode = variantCode;
            this.UnitOfMeasureCode = unitOfMeasureCode;
            this.MinimumQuantity = minimumQuantity;
            this.UnitPrice = (double)unitPrice;
            this.PriceIncludesVat = priceIncludesVat;
            this.AllowInvoiceDisc = allowInvoiceDisc;
            this.VatBusPostingGrPrice = vatBusPostingGrPrice;
            this.EndingDate = endingDate;
            this.AllowLineDisc = allowLineDisc;
            //Set the rest variables
        }

        /// <summary>
        /// Gets the product name based on given language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>The product name in given language</returns>
        public string GetItemName(string language)
        {
            if (language == string.Empty || language.ToLower().Contains("swedish"))
            {
                return this.SeItemName;
            }
            else if (language.ToLower().Contains("norwegian"))
            {
                return this.NoItemName;
            }
            else if (language.ToLower().Contains("finnish"))
            {
                return this.FiItemName;
            }
            else if (language.ToLower().Contains("danish"))
            {
                return this.DkItemName;
            }
            else if (language.ToLower().Contains("english"))
            {
                return this.EnItemName;
            }
            else
            {
                return this.SeItemName;
            }
        }

        public void SetItemName(string language, string itemName)
        {
            if (language == string.Empty || language.ToLower().Contains("swedish"))
            {
                this.SeItemName = itemName;
            }
            else if (language.ToLower().Contains("norwegian"))
            {
                this.NoItemName = itemName;
            }
            else if (language.ToLower().Contains("finnish"))
            {
                this.FiItemName = itemName;
            }
            else if (language.ToLower().Contains("danish"))
            {
                this.DkItemName = itemName;
            }
            else if (language.ToLower().Contains("english"))
            {
                this.EnItemName = itemName;
            }
            else
            {
                this.SeItemName = itemName;
            }
        }

        public void SetItemText(string language, string text)
        {
            if (language == string.Empty || language.ToLower().Contains("swedish"))
            {
                this.SeItemText = text;
            }
            else if (language.ToLower().Contains("norwegian"))
            {
                this.NoItemText = text;
            }
            else if (language.ToLower().Contains("finnish"))
            {
                this.FiItemText = text;
            }
            else if (language.ToLower().Contains("danish"))
            {
                this.DkItemText = text;
            }
            else if (language.ToLower().Contains("english"))
            {
                this.EnItemText = text;
            }
            else
            {
                this.SeItemText = text;
            }
        }

        public void SetUnitPrice(string price)
        {
            if (price == string.Empty || price == null)
            {
                return;
            }

            double decimalPrice;
            if (double.TryParse(price, out decimalPrice))
            {
                this.UnitPrice = decimalPrice;
            }
        }

        /// <summary>
        /// Gets the image link
        /// </summary>
        /// <returns>Returns image link for the product</returns>
        public string GetImageLink()
        {
            string link = "//192.168.1.21/Produktbilder/";
            link += this.VariantId[0] + "/";
            link += this.VariantId + "/";
            link += this.VariantId + ".jpg";

            return link;
        }
    }
}
