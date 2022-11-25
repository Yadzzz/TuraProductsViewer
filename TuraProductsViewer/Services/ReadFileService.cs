using Microsoft.AspNetCore.Components.Forms;
using System.Security.Policy;
using Sylvan.Data.Excel;
using System.Data;
using System.Reflection.PortableExecutable;

namespace TuraProductsViewer.Services
{
    public class ReadFileService
    {
        /// <summary>
        /// Reads from given text file
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Dicitonary filled with data [key:productId] [value:price]</returns>
        public async Task<Dictionary<string, string>> ReadFromUploadedFileWithPrices(InputFileChangeEventArgs e)
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

        public async Task ReadFromUploadedExcelFileFileWithPrices(InputFileChangeEventArgs e)
        {
            Stream stream = new MemoryStream();
            await e.File.OpenReadStream().CopyToAsync(stream);

            var edr = ExcelDataReader.Create(stream, ExcelWorkbookType.ExcelXml);

            var dt = new DataTable();
            dt.Load(edr);

            //if (double.TryParse(dt.Columns[0].ColumnName, out double productId))
            //{
            //    Console.WriteLine(productId);
            //}

            //if (double.TryParse(dt.Columns[1].ColumnName, out double productPrice))
            //{
            //    Console.WriteLine(productId);
            //}

            //Console.WriteLine(dt.Columns[0].ColumnName);
            //Console.WriteLine(dt.Columns[1].ColumnName);

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row[0]);
                Console.WriteLine(row[1]);
            }

        }
    }
}
