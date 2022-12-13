namespace TuraProductsViewer.Services.FileCreator
{
    public class ConcreteExcelCreator : FileCreatorFactory
    {
        public override IFile CreateExcelFile(CreatorService creatorService, ImageService imageService)
        {
            return new ExcelFile(creatorService, imageService);
        }
    }
}
