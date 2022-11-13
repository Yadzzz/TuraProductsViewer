using Microsoft.AspNetCore.Components.Forms;

namespace TuraProductsViewer.Services
{
    public class ReadFileService
    {
        /// <summary>
        /// Reads all content from text file
        /// </summary>
        /// <param name="fileSource">Image path</param>
        /// <param name="fileContent">Empty list to pass by reference to fill data with</param>
        /// <param name="errorMessage">Empty string returning an error message on error event</param>
        /// <returns>A list of product Ids</returns>
        public bool ReadTextFile(string fileSource, out List<string> fileContent, out string errorMessage)
        {
            fileContent = new List<string>();
            errorMessage = string.Empty;

            if (!File.Exists(fileSource))
            {
                errorMessage = "File not found";
                return false;
            }

            using (var stream = File.OpenRead(fileSource))
            {
                if (stream == null)
                {
                    errorMessage = "An error occured during file reading.";
                    return false;
                }

                using (var reader = new StreamReader(stream))
                {
                    if (reader == null)
                    {
                        errorMessage = "An error occured during file reading.";
                        return false;
                    }

                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(" "))
                        {
                            string[] manipulatedLine = line.Split(" ");
                            fileContent.Add(manipulatedLine[0]);
                        }
                        else
                        {
                            fileContent.Add(line);
                        }
                    }
                }
            }

            return true;
        }

        public async Task<List<string>> ReadFromUploadedFile(InputFileChangeEventArgs e)
        {
            List<string> productIds = new List<string>();
            
            using (var reader = new StreamReader(e.File.OpenReadStream()))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.Contains(" "))
                    {
                        string[] manipulatedLine = line.Split(" ");
                        productIds.Add(manipulatedLine[0]);
                    }
                    else
                    {
                        productIds.Add(line);
                    }
                }

                e.File.OpenReadStream().Close(); //Necessary?
                //reader.Close();
                //reader.Dispose();
            }

            return productIds;
        }
    }
}
