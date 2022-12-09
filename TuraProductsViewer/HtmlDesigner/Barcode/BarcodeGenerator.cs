using SelectPdf;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

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
            var barcode = new NetBarcode.Barcode(data, NetBarcode.Type.Code128, true, 233, 90);
            //var barcode = new NetBarcode.Barcode(data, NetBarcode.Type.Code128, true);
            var value = barcode.GetBase64Image();

            return value;
        }

        public static string GetBase64Image(string data, int width, int height, bool showEAN)
        {
            var barcode = new NetBarcode.Barcode(data, NetBarcode.Type.Code128, showEAN, width, height);
            var value = barcode.GetBase64Image();

            return value;
        }

        public static string GetBase64Image(string data, int width, int height, bool showEAN, int fontSize)
        {
            FontCollection collection = new();
            FontFamily family = collection.Add(Environment.CurrentDirectory + "/wwwroot/fonts/micross.ttf"); //Use path combine?
            Font font = family.CreateFont(fontSize);

            var barcode = new NetBarcode.Barcode(data, NetBarcode.Type.Code128, showEAN, width, height, font);
            var value = barcode.GetBase64Image();

            return value;
        }
    }
}
