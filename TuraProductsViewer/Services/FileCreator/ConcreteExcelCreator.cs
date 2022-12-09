namespace TuraProductsViewer.Services.FileCreator
{
    public class ConcreteExcelCreator : FileCreatorFactory
    {
        public override IFile CreateExcelFile(CreatorService creatorService)
        {
            return new ExcelFile(creatorService);
        }
    }
}
