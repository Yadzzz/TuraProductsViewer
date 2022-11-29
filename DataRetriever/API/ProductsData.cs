using DataRetriever.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DataRetriever.API
{
    public class ProductsData
    {
        //<<<<<<<<Impelement logging>>>>>>>>>
        private string _apiUrl;

        public ProductsData()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

            this._apiUrl = configuration["apiurl"];
        }

        /// <summary>
        /// Fetches Data from API provider
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="currencyCode">The currency code to retrieve data based on</param>
        /// <returns>Returns selected data from API if product id exists otherwise empty object</returns>
        public ProductsDataModel? Fetch(string id, string currencyCode)
        {
            ProductsDataModel? productsData = null;

            //var client = new RestClient("https://localhost:5001/api/ProductsData/" + id + "/" + currencyCode);
            using (var client = new RestClient(this._apiUrl + id + "/" + currencyCode))
            {
                var request = new RestRequest();
                request.AddHeader("ApiKey", "ba932ec7-3d66-487c-bcd0-4e17c8a2dfb3");
                RestResponse response = client.Execute(request);

                if (response.Content != null)
                {
                    try
                    {
                        productsData = JsonConvert.DeserializeObject<ProductsDataModel>(response.Content);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }

            return productsData;
        }

        /// <summary>
        /// Fetches Data from API provider
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="currencyCode">The currency code to retrieve data based on</param>
        /// <returns>Returns selected data from API if product id exists otherwise empty object</returns>
        public async Task<ProductsDataModel?> FetchAsync(string id, string currencyCode)
        {
            ProductsDataModel? productsData = null;

            //var client = new RestClient("https://localhost:5001/api/ProductsData/" + id + "/" + currencyCode);
            using (var client = new RestClient(this._apiUrl + id + "/" + currencyCode))
            {
                var request = new RestRequest();
                request.AddHeader("ApiKey", "ba932ec7-3d66-487c-bcd0-4e17c8a2dfb3");
                RestResponse response = await client.ExecuteAsync(request);

                if (response.Content != null)
                {
                    try
                    {
                        productsData = JsonConvert.DeserializeObject<ProductsDataModel>(response.Content);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }

            return productsData;
        }

        /// <summary>
        /// Gets the final API URL to retrieve Data from
        /// </summary>
        /// <returns>Finalized URL</returns>
        private string FinalizeApiUrl(string id, string salesCode, string currencyCode)
        {
            return this._apiUrl += id + "/" + salesCode + "/" + currencyCode;
        }
    }
}
