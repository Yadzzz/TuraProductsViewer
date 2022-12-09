namespace TuraProductsViewer.Services.FileCreator
{
    public abstract class FileCreatorFactory
    {
        public abstract IFile CreateExcelFile(CreatorService creatorService);
    }
}
