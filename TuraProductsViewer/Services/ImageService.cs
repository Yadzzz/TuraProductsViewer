using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TuraProductsViewer.Services
{
    public class ImageService
    {
        private string rootProductsImageFolderPrimary;
        private string rootProdcutsImageFolderSecondary;

        public ImageService()
        {
            this.rootProductsImageFolderPrimary = "/Produktbilder/";
            this.rootProdcutsImageFolderSecondary = "Produktbilder/";
        }

        /// <summary>
        /// Gets image path
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Path to {productId} image</returns>
        public string GetImagePath(string productId)
        {
            /// Produktbilder / 2 / 235204 / 235204.jpg
            string imagePath = this.rootProductsImageFolderPrimary;
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
            string imagePath = this.rootProductsImageFolderPrimary;
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
