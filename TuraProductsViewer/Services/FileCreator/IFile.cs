using System.Data;

namespace TuraProductsViewer.Services.FileCreator
{
    public interface IFile
    {
        public CreatorService CreatorService { get; set; }
        public ImageService ImageService { get; set; }
        public MemoryStream GetStreamWithPictures();
    }
}
