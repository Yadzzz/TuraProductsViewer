using System.Collections.Concurrent;
using System.Data.SqlTypes;

namespace TuraProductsViewer.FileCreationFactory
{
    public class FileFactory
    {
        private ConcurrentQueue<FileCollection> fileCollectionsPool;

        public FileFactory()
        {
            this.fileCollectionsPool = new ConcurrentQueue<FileCollection>();
            this.InitializePool();
        }

        private void InitializePool()
        {
            for(int i = 0; i < 20; i++)
            {
                FileCollection collection = new FileCollection();
            }
        }

        private int IdIntervallStart
        {
            get
            {
                return 1;
            }
        }

        private int IdIntervallEnd
        {
            get
            {
                return 20;
            }
        }

        private void CreateNewPoolFileCollection()
        {
            
        }
    }
}b
