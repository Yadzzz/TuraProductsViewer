using DataRetriever.Models;

namespace TuraProductsViewer.Services
{
    public class CreatorService
    {
        private readonly ILogger<CreatorService> logger;
        private List<DataRetriever.Models.ProductsDataModel> products { get; set; }
        public string CurrencyCode { get; set; } = "SEK";
        public string Language { get; set; } = "Swedish";
        public bool UsePackagingImage { get; set; } = false;
        public bool ShowInStock { get; set; } = false;
        public bool ShowInStockCount { get; set; } = false;
        public bool ShowPackagingMeasurment { get; set; } = false;
        public PriceType PriceType { get; set; } = PriceType.Rek;
        public string CustomerId { get; set; } = string.Empty;
        public Dictionary<string,string> SpecialCustomerPrices { get; set; }

        public CreatorService(ILogger<CreatorService> _logger)
        {
            this.logger = _logger;
            this.products = new List<DataRetriever.Models.ProductsDataModel>();
            this.SpecialCustomerPrices = new Dictionary<string, string>();
        }

        /// <summary>
        /// Adds the given products to the list
        /// </summary>
        /// <param name="product">Instance of the product</param>
        public void AddProduct(DataRetriever.Models.ProductsDataModel product)
        {
            if(this.products.Contains(product))
            {
                return;
            }

            this.products.Add(product);
        }

        /// <summary>
        /// Removes the given product from the list
        /// </summary>
        /// <param name="product">Instance of the product</param>
        public void RemoveProduct(DataRetriever.Models.ProductsDataModel product)
        {
            if(!this.products.Contains(product))
            {
                return;
            }

            this.products.Remove(product);
        }

        /// <summary>
        /// Check if products exists in the list
        /// </summary>
        /// <param name="product">Instance of the product</param>
        /// <returns>If product exists</returns>
        public bool ContainsProduct(DataRetriever.Models.ProductsDataModel product)
        {
            return this.products.Contains(product);
        }

        /// <summary>
        /// Gets all the products
        /// </summary>
        /// <returns>List of products</returns>
        public List<DataRetriever.Models.ProductsDataModel> GetProducts()
        {
            return this.products;
        }

        /// <summary>
        /// Gets the total products count
        /// </summary>
        /// <returns>Products count</returns>
        public int GetProductsCount()
        {
            return this.products.Count;
        }

        /// <summary>
        /// Clears all products from Creator Service
        /// </summary>
        public void ClearProducts()
        {
            this.products.Clear();
        }

        /// <summary>
        /// Gets the Price based on User Actions & Inputs
        /// </summary>
        /// <param name="productsData">ProductDataModel</param>
        /// <returns></returns>
        public string GetPrice(ProductsDataModel productsData)
        {
            if(productsData.SpecialCustomerEditedPrice > 0)
            {
                return productsData.SpecialCustomerEditedPrice.ToString();
            }
            
            if(this.SpecialCustomerPrices.TryGetValue(productsData.VariantId, out string value))
            {
                return value;
            }

            return productsData.UnitPriceWithoutVat.ToString("F2");
        }

        /// <summary>
        /// Gets ProductsDataModel Object based Product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductsDataModel? this[string productId]
        {
            get
            {
                foreach(var product in this.products)
                {
                    if (product.VariantId == productId)
                        return product;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets ProductsDataModel Object based on Index position in List<ProductsDataModel>
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductsDataModel? this[int index]
        {
            get
            {
                return this.products[index];
            }
        }
    }

    public enum PriceType
    {
        Rek,
        Netto,
        RekNetto,
        Kund,
        KundRek,
        None
    }
}
