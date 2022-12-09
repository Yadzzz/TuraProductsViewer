using System.Data;

namespace TuraProductsViewer.Services.FileCreator
{
    public interface IFile
    {
        public CreatorService CreatorService { get; set; }
        public MemoryStream GetStreamData();
        public DataTable GetDataTable();
    }
}
