using SixLabors.ImageSharp;
using System.Drawing;

namespace TuraProductsViewer.HtmlDesigner.Barcode
{
    public static class BarcodeGenerator
    {
        public static SixLabors.ImageSharp.Image GetBarcode(string data)
        {
            var barcode = new NetBarcode.Barcode(data, NetBarcode.Type.EAN13, true);
            var image = barcode.GetImage();

            image.SaveAsJpeg(@"C:\Users\yadma\OneDrive\Dev\Barcodes\" + data + ".jpg");

            return image;
        }

        public static string GetBase64Image(string data)
        {
            Console.WriteLine(data);

            var barcode = new NetBarcode.Barcode(data, NetBarcode.Type.Code128, true);
            var value = barcode.GetBase64Image();

            return value;
        }
    }
}
