﻿using DataRetriever.Models;

namespace TuraProductsViewer.Services
{
    public class APIService
    {
        private readonly ILogger<APIService> logger;

        public APIService(ILogger<APIService> _logger)
        {
            this.logger = _logger;
        }

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

        /// <summary>
        /// Fetches data from the ProductsData data from the API Async
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="currencyCode">Currency Code</param>
        /// <returns>ProductsDataModel with all data related to product Id {<paramref name="id"/>}</returns>
        public async Task<ProductsDataModel> GetProductsDataAsync(string id, string currencyCode)
        {
            DataRetriever.API.ProductsData productsData = new DataRetriever.API.ProductsData();

            return productsData.Fetch(id, currencyCode);
        }
    }
}

