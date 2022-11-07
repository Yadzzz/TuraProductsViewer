using DataAccess;

namespace TuraProductsViewer.Services
{
    public class ProductSearchService
    {
        private readonly DataAccessManager _dataAccessManager;

        public ProductSearchService(DataAccessManager dataAccessManager)
        {
            this._dataAccessManager = dataAccessManager;
        }

        /// <summary>
        /// Loads all products
        /// </summary>
        /// <returns>List of all products</returns>
        public List<DataAccess.Models.Product> LoadAllProducts()
        {
            return this._dataAccessManager.DataAccessContext.Products.OrderBy(a => a.Id).ToList();
        }

        /// <summary>
        /// Loads x amount of products.
        /// </summary>
        /// <param name="count">Amount of products</param>
        /// <returns>List of x products</returns>
        public List<DataAccess.Models.Product> LoadProducts(int count)
        {
            return this._dataAccessManager.DataAccessContext.Products.OrderBy(a => a.Id).Take(count).ToList();
        }

        /// <summary>
        /// Search Products by name
        /// </summary>
        /// <param name="productName">Product Name to search for</param>
        /// <returns>A list of elements that contains given string</returns>
        public List<DataAccess.Models.Product> SearchProductByName(string productName)
        {
            var products = _dataAccessManager.DataAccessContext.Products.Where(p => p.Name.Contains(productName)).Take(10);

            return products.ToList();
        }
    }
}
