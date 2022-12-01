using System.Net;
using System.Security.Policy;

namespace TuraProductsViewer.Services
{
    public class ImageService
    {
        private readonly ILogger<CreatorService> logger;
        private readonly string rootProductsImageFolder;
        private readonly string webProductsImagePath;

        public ImageService(ILogger<CreatorService> _logger)
        {
            this.logger = _logger;
            this.rootProductsImageFolder = "/Produktbilder/";
            this.webProductsImagePath = "https://www.turascandinavia.com/image/";
        }

        /// <summary>
        /// Web Image Path
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Image Path</returns>
        public string GetWebImagePath(string productId)
        {
            string imagePath = this.webProductsImagePath;
            imagePath += productId;
            imagePath += ".jpg";

            return imagePath;
        }

        /// <summary>
        /// Web Packaging Image Path
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Packaging Image Path</returns>
        public string GetWebPackagingImagePath(string productId)
        {
            string imagePath = this.webProductsImagePath;
            imagePath += productId;
            imagePath += "ver.jpg";

            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(imagePath);
            //request.Method = "HEAD";

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(imagePath);
                request.GetResponse();
            }
            catch
            {
                //return "https://vendev.pro/pictures/missing.jpg";
                //return "../pictures/missing.jpg";
                
                this.logger.LogWarning("Packaging Picture [" + productId + "] missing.");

                return "https://pdftest.turascandinavia.com/pictures/missing.jpg";
            }

            return imagePath;
        }

        /// <summary>
        /// Gets image path
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Path to {productId} image</returns>
        public string GetImagePath(string productId)
        {
            /// Produktbilder / 2 / 235204 / 235204.jpg
            string imagePath = this.rootProductsImageFolder;
            imagePath += productId.Substring(0, 1);
            imagePath += "/";
            imagePath += productId;
            imagePath += "/";
            imagePath += productId;
            imagePath += ".jpg";

            return imagePath;
        }

        /// <summary>
        /// Gets image path
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Path to {productId} packaging image</returns>
        public string GetPackagingImagePath(string productId)
        {
            /// Produktbilder / 2 / 235204 / 235204.jpg
            string imagePath = this.rootProductsImageFolder;
            imagePath += productId.Substring(0, 1);
            imagePath += "/";
            imagePath += productId;
            imagePath += "/";
            imagePath += productId;
            imagePath += "ver.jpg";

            return imagePath;
        }
    }
}
