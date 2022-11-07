using DataAccess.Models;

namespace TuraProductsViewer.Services
{
    public class PDFCreatorService
    {
        private List<Product> products { get; set; }

        public PDFCreatorService()
        {
            this.products = new List<Product>();
        }

        /// <summary>
        /// Adds the given products to the list
        /// </summary>
        /// <param name="product">Instance of the product</param>
        public void AddProduct(Product product)
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
        public void RemoveProduct(Product product)
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
        public bool ContainsProduct(Product product)
        {
            return this.products.Contains(product);
        }

        /// <summary>
        /// Gets all the products
        /// </summary>
        /// <returns>List of products</returns>
        public List<Product> GetProducts()
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
    }
}
