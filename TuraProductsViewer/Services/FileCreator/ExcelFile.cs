using Sylvan.Data.Excel;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Security.Policy;

namespace TuraProductsViewer.Services.FileCreator
{
    public class ExcelFile : IFile, IDisposable
    {
        public CreatorService CreatorService { get; set; }

        public ExcelFile(CreatorService creatorService)
        {
            this.CreatorService = creatorService;
        }

        public MemoryStream GetStreamData()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (var edw = ExcelDataWriter.Create(stream, ExcelWorkbookType.ExcelXml))
                {
                    DbDataReader dr = this.GetDataTable().CreateDataReader();
                    edw.Write(dr);

                    return stream;
                }
            }
        }

        public DataTable GetDataTable()
        {
            Dictionary<string, string> languageVariables = FileCreatorUtilities.GetCreatorUtilities().GetLanguageVariables(this.CreatorService.Language);
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(languageVariables["artnrvariable"]);
            dataTable.Columns.Add(languageVariables["varumarkevariable"]);

            //Set Price collumns
            if (this.CreatorService.PriceType == PriceType.None)
            {

            }
            else if (this.CreatorService.PriceType == PriceType.Rek)
            {
                dataTable.Columns.Add(languageVariables["rekprisvariable"]);
            }
            else if (this.CreatorService.PriceType == PriceType.Netto)
            {
                dataTable.Columns.Add(languageVariables["prisvariable"]);
            }
            else if (this.CreatorService.PriceType == PriceType.RekNetto)
            {
                dataTable.Columns.Add(languageVariables["prisvariable"]);
                dataTable.Columns.Add(languageVariables["rekprisvariable"]);
            }
            else if (this.CreatorService.PriceType == PriceType.Kund)
            {
                dataTable.Columns.Add(languageVariables["prisvariable"]);
            }
            else if (this.CreatorService.PriceType == PriceType.KundRek)
            {
                dataTable.Columns.Add(languageVariables["prisvariable"]);
                dataTable.Columns.Add(languageVariables["rekprisvariable"]);
            }
            else
            {
                dataTable.Columns.Add(languageVariables["rekprisvariable"]);
            }

            dataTable.Columns.Add(languageVariables["eanvariable"]);

            if (this.CreatorService.ShowPackagingMeasurment)
            {
                dataTable.Columns.Add(languageVariables["frpstlvariable"]);
            }

            if (this.CreatorService.ShowInStock)
            {
                dataTable.Columns.Add(languageVariables["ilagervariable"]);
            }

            //DataColumn imageColumn = new DataColumn("MyImage"); //Create the column.
            //imageColumn.DataType = System.Type.GetType("System.Byte[]"); //Type byte[] to store image bytes.
            //imageColumn.AllowDBNull = true;
            //imageColumn.Caption = "My Image";

            //dataTable.Columns.Add(imageColumn);

            foreach (var product in this.CreatorService.GetProducts())
            {
                Queue<string> rowData = new Queue<string>();
                rowData.Enqueue(product.VariantId);
                rowData.Enqueue(product.Brand != null ? product.Brand : "N/A");

                if (this.CreatorService.PriceType == PriceType.None)
                {

                }
                else if (this.CreatorService.PriceType == PriceType.Rek)
                {
                    rowData.Enqueue(product.UnitPrice.ToString("F2"));
                }
                else if (this.CreatorService.PriceType == PriceType.Netto)
                {
                    rowData.Enqueue(this.CreatorService.FinalizePrice(product));
                }
                else if (this.CreatorService.PriceType == PriceType.RekNetto)
                {
                    rowData.Enqueue(this.CreatorService.FinalizePrice(product));
                    rowData.Enqueue(product.UnitPrice.ToString("F2"));
                }
                else if (this.CreatorService.PriceType == PriceType.Kund)
                {
                    rowData.Enqueue(this.CreatorService.FinalizePrice(product));
                }
                else if (this.CreatorService.PriceType == PriceType.KundRek)
                {
                    rowData.Enqueue(this.CreatorService.FinalizePrice(product));
                    rowData.Enqueue(product.UnitPrice.ToString("F2"));
                }
                else
                {
                    rowData.Enqueue(product.UnitPrice.ToString("F2"));
                }

                rowData.Enqueue(product.PrimaryEANCode);

                if (this.CreatorService.ShowPackagingMeasurment)
                {
                    rowData.Enqueue(product.QtyPerUnitOfMeasure.ToString("F0") + " " + languageVariables["measurment"]);
                }

                if (this.CreatorService.ShowInStock)
                {
                    if (this.CreatorService.ShowInStockCount)
                    {
                        rowData.Enqueue(product.AvailableQty > 0 ? (product.AvailableQty.ToString("F0") + " " + languageVariables["measurment"]) : languageVariables["no"]);
                    }
                    else
                    {
                        rowData.Enqueue(product.AvailableQty > 0 ? languageVariables["yes"] : languageVariables["no"]);
                    }
                }


                //https://www.turascandinavia.com/image/280006.jpg

                //byte[] imageBytes = new byte[1024];

                //Task.Run(async () =>
                //{
                //    using (var client = new HttpClient())
                //    {
                //        using (var response = await client.GetAsync("https://www.turascandinavia.com/image/280006.jpg"))
                //        {
                //            imageBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                //        }
                //    }
                //});

                //var row = dataTable.Rows.Add(rowData.ToArray());
                //row["MyImage"] = imageBytes;

                dataTable.Rows.Add(rowData.ToArray());
            }

            return dataTable;
        }

        public void Dispose()
        {

        }
    }
}
