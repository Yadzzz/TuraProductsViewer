using DataRetriever.Models;

namespace TuraProductsViewer.Services
{
    public class APIService
    {
        /// <summary>
        /// Fetches data from the ProductsData data from the API
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="currencyCode">Currency Code</param>
        /// <returns>ProductsDataModel with all data related to product Id {<paramref name="id"/>}</returns>
        public ProductsDataModel GetProductsData(string id, string currencyCode)
        {
            DataRetriever.API.ProductsData productsData = new DataRetriever.API.ProductsData();

            return productsData.Fetch(id, currencyCode);
        }
    }
}
