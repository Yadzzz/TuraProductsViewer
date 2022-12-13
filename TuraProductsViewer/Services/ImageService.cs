using System.Net;
using System.Security.Policy;

namespace TuraProductsViewer.Services
{
    public class ImageService
    {
        private readonly ILogger<CreatorService> logger;
        private readonly string rootProductsImageFolder;
        private readonly string webProductsImagePath;
        private readonly string networkPath;

        public ImageService(ILogger<CreatorService> _logger)
        {
            this.logger = _logger;
            this.rootProductsImageFolder = "/Produktbilder/";
            this.networkPath = "\\\\192.168.1.21\\Produktbilder\\";

            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

            this.webProductsImagePath = configuration["ImagesBase:ProductImagesBaseUrl"] ?? this.rootProductsImageFolder;
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

                return "https://pdf.turascandinavia.com/pictures/missing.jpg";
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

        /// <summary>
        /// Gets the whole image path, some functions requires the whole path
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Path to {productId} image</returns>
        public string GetObseleteImagePath(string productId)
        {
            /// \\192.168.1.21\Produktbilder\2\235204\235204.jpg
            string imagePath = this.networkPath;
            imagePath += productId.Substring(0, 1);
            imagePath += "\\";
            imagePath += productId;
            imagePath += "\\";
            imagePath += productId;
            imagePath += ".jpg";

            return imagePath;
        }

        /// <summary>
        /// Gets the whole image path, some functions requires the whole path
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Path to {productId} image</returns>
        public string GetObseletePackagingImagePath(string productId)
        {
            /// \\192.168.1.21\Produktbilder\2\235204\235204.jpg
            string imagePath = this.networkPath;
            imagePath += productId.Substring(0, 1);
            imagePath += "\\";
            imagePath += productId;
            imagePath += "\\";
            imagePath += productId;
            imagePath += "ver.jpg";

            return imagePath;
        }

        /// <summary>
        /// Gets image in Base64
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Path to {productId} image</returns>
        public string GetBase64Image(string productId)
        {
            byte[] imageBytes = File.ReadAllBytes(this.GetObseleteImagePath(productId));

            return Convert.ToBase64String(imageBytes);
        }
    }
}
