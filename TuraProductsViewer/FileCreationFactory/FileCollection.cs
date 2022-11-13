namespace TuraProductsViewer.FileCreationFactory
{
    public class FileCollection
    {
        public string Id { get; set; }
        public string PdfPath { get; private set; }

        public FileCollection(int id)
        {
            this.Id = id;
            this.PdfPath = "../TuraProductsViewer/wwwroot/pdf/" + id + ".pdf";
        }
    }
}
