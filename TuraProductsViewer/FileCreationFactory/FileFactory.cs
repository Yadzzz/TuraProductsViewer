using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;

namespace TuraProductsViewer.FileCreationFactory
{
    public class FileFactory
    {
        private ConcurrentQueue<FileCollection> fileCollectionsPool;

        public FileFactory()
        {
            Task.Run(() => this.InitializePool());
        }

        private void InitializePool()
        {
            if (this.EmptyFolder())
            {
                this.fileCollectionsPool = new ConcurrentQueue<FileCollection>();

                for (int i = 0; i < 20; i++)
                {
                    FileCollection collection = new FileCollection(this.GenerateId());
                }
            }
            else
            {
                //Could not empty folder, log and take different approach to not overload the disk space
                this.fileCollectionsPool = new ConcurrentQueue<FileCollection>(this.GetCurrentFiles());
            }
        }

        private bool EmptyFolder()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo("../TuraProductsViewer/wwwroot/html/");

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch(Exception e)
            {
                //Log Error
                return false;
            }

            return true;
        }

        private List<FileCollection> GetCurrentFiles()
        {
            List<FileCollection> ids = new List<FileCollection>();

            DirectoryInfo di = new DirectoryInfo("../TuraProductsViewer/wwwroot/html/");
            
            foreach(FileInfo file in di.GetFiles())
            {
                string id = file.Name;

                if (id.Contains(".html"))
                    id = id.Replace(".html", "");

                ids.Add(new FileCollection(Convert.ToInt32(id)));
            }

            return ids;
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

        private int GenerateId()
        {
            int i = 1;
            
            foreach(var fileCollection in this.fileCollectionsPool)
            {
                if(fileCollection.Id > i)
                {
                    i = fileCollection.Id;
                }
            }

            return i++;
        }

        /// <summary>
        /// Returns an FileCollection object from the pool.
        /// </summary>
        public FileCollection FetchFileCollectionObject()
        {
            FileCollection? fileCollection;
            if(this.fileCollectionsPool.TryDequeue(out fileCollection))
            {
                return fileCollection;
            }
            else
            {
                return this.GenerateNewFileCollection();
            }
        }

        public void RetrievePoolObject(FileCollection fileCollection)
        {
            this.fileCollectionsPool.Enqueue(fileCollection);
        }

        public FileCollection GenerateNewFileCollection()
        {
            return new FileCollection(this.GenerateId());
        }
    }
}
