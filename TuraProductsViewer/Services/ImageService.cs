using System.Net;

namespace TuraProductsViewer.Services
{
    public class ImageService
    {
        private string rootProductsImageFolder;
        private string webProductsImagePath;

        public ImageService()
        {
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

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(imagePath);
            request.Method = "HEAD";

            try
            {
                request.GetResponse();
            }
            catch
            {
                return "https://vendev.pro/pictures/missing.jpg";
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
