using Microsoft.AspNetCore.Components.Forms;
using System.Security.Policy;
using Sylvan.Data.Excel;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Reflection.Metadata.Ecma335;

namespace TuraProductsViewer.Services
{
    public class ReadFileService
    {
        private readonly ILogger<CreatorService> logger;

        public ReadFileService(ILogger<CreatorService> _logger)
        {
            this.logger = _logger;
        }

        /// <summary>
        /// Reads from given text file
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Dicitonary filled with data [key:productId] [value:price]</returns>
        public async Task<Dictionary<string, string>> ReadFromUploadedFileWithPrices(InputFileChangeEventArgs e)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            try
            {
                using (var reader = new StreamReader(e.File.OpenReadStream()))
                {
                    string? line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (line.Contains("\t"))
                        {
                            string[] manipulatedLine = line.Split("\t");
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
            }
            catch(Exception exception)
            {
                this.logger.LogError(exception.ToString());
            }

            return data;
        }

        /// <summary>
        /// Reads from given excel file
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Dicitonary filled with data [key:productId] [value:price]</returns>
        public async Task<Dictionary<string,string>> ReadFromUploadedExcelFileFileWithPrices(InputFileChangeEventArgs e)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            try
            {
                using (Stream stream = new MemoryStream())
                {
                    await e.File.OpenReadStream().CopyToAsync(stream);

                    using (var edr = ExcelDataReader.Create(stream, ExcelWorkbookType.ExcelXml))
                    {
                        var dt = new DataTable();

                        if (dt == null)
                        {
                            return null;
                        }

                        dt.Load(edr);

                        if (dt.Columns.Count == 0)
                        {
                            return null;
                        }

                        if (double.TryParse(dt.Columns[0].ColumnName, out double productId))
                        {

                            if (double.TryParse(dt.Columns[1].ColumnName, out double productPrice))
                            {
                                data.Add(dt.Columns[0].ColumnName, dt.Columns[1].ColumnName);
                            }
                            else
                            {
                                data.Add(dt.Columns[0].ColumnName, string.Empty);
                            }
                        }

                        foreach (DataRow row in dt.Rows)
                        {
                            if (double.TryParse((string)row[0], out double id))
                            {
                                if (row.IsNull(1))
                                {
                                    data.Add(id.ToString(), string.Empty);
                                    continue;
                                }

                                if (double.TryParse((string)row[1], out double price))
                                {
                                    data.Add(id.ToString(), price.ToString());
                                }
                                else
                                {
                                    data.Add(id.ToString(), string.Empty);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception exception)
            {
                this.logger.LogError(exception.ToString());
            }

            return data;
        }
    }
}
