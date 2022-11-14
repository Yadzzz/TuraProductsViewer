using Microsoft.AspNetCore.Components.Forms;

namespace TuraProductsViewer.Services
{
    public class ReadFileService
    {
        /// <summary>
        /// Reads from given text file
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Data from text file</returns>
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

        /// <summary>
        /// Reads from given text file
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Dicitonary filled with data [key:productId] [value:price]</returns>
        public async Task<Dictionary<string,string>> ReadFromUploadedFileWithPrices(InputFileChangeEventArgs e)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            using (var reader = new StreamReader(e.File.OpenReadStream()))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.Contains(" "))
                    {
                        string[] manipulatedLine = line.Split(" ");
                        data.Add(manipulatedLine[0], manipulatedLine[1]);
                    }
                    else
                    {
                        data.Add(line, "");
                    }
                }

                e.File.OpenReadStream().Close(); //Necessary?
                //reader.Close();
                //reader.Dispose();
            }

            return data;
        }
    }
}
