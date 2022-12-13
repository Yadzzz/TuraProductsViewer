using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
using Sylvan.Data.Excel;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Security.Policy;
using System;

namespace TuraProductsViewer.Services.FileCreator
{
    public class ExcelFile : IFile, IDisposable
    {
        public CreatorService CreatorService { get; set; }
        public ImageService ImageService { get; set; }

        public ExcelFile(CreatorService creatorService, ImageService imageService)
        {
            this.CreatorService = creatorService;
            this.ImageService = imageService;
        }

        public MemoryStream GetStreamWithPictures()
        {
            MemoryStream stream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo("MyWorkbook.xlsx")))
            {
                using (ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1"))
                {
                    //worksheet.Cells[1, 1].Value = "Test";

                    Dictionary<string, string> languageVariables = FileCreatorUtilities.GetCreatorUtilities().GetLanguageVariables(this.CreatorService.Language);

                    int collumnsCount = 1;
                    worksheet.Cells[1, collumnsCount++].Value = languageVariables["artnrvariable"];
                    worksheet.Cells[1, collumnsCount++].Value = languageVariables["varumarkevariable"];

                    //Set Price collumns
                    if (this.CreatorService.PriceType == PriceType.None)
                    {

                    }
                    else if (this.CreatorService.PriceType == PriceType.Rek)
                    {
                        worksheet.Cells[1, collumnsCount++].Value = languageVariables["rekprisvariable"];
                    }
                    else if (this.CreatorService.PriceType == PriceType.Netto)
                    {
                        worksheet.Cells[1, collumnsCount++].Value = languageVariables["prisvariable"];
                    }
                    else if (this.CreatorService.PriceType == PriceType.RekNetto)
                    {
                        worksheet.Cells[1, collumnsCount++].Value = languageVariables["prisvariable"];
                        worksheet.Cells[1, collumnsCount++].Value = languageVariables["rekprisvariable"];
                    }
                    else if (this.CreatorService.PriceType == PriceType.Kund)
                    {
                        worksheet.Cells[1, collumnsCount++].Value = languageVariables["prisvariable"];
                    }
                    else if (this.CreatorService.PriceType == PriceType.KundRek)
                    {
                        worksheet.Cells[1, collumnsCount++].Value = languageVariables["prisvariable"];
                        worksheet.Cells[1, collumnsCount++].Value = languageVariables["rekprisvariable"];
                    }
                    else
                    {
                        worksheet.Cells[1, collumnsCount++].Value = languageVariables["rekprisvariable"];
                    }

                    worksheet.Cells[1, collumnsCount++].Value = languageVariables["eanvariable"];

                    if (this.CreatorService.ShowPackagingMeasurment)
                    {
                        worksheet.Cells[1, collumnsCount++].Value = languageVariables["frpstlvariable"];
                    }

                    if (this.CreatorService.ShowInStock)
                    {
                        worksheet.Cells[1, collumnsCount++].Value = languageVariables["ilagervariable"];
                    }

                    worksheet.Cells[1, collumnsCount++].Value = "Image"; //Change to respective language
                    worksheet.Cells[1, collumnsCount].Value = "Link"; //Change to respective language

                    collumnsCount = 1;
                    int rowCount = 2;
                    foreach (var product in this.CreatorService.GetProducts())
                    {
                        worksheet.Cells[rowCount, collumnsCount++].Value = product.VariantId;
                        worksheet.Cells[rowCount, collumnsCount++].Value = product.Brand != null ? product.Brand : "N/A";

                        if (this.CreatorService.PriceType == PriceType.None)
                        {

                        }
                        else if (this.CreatorService.PriceType == PriceType.Rek)
                        {
                            worksheet.Cells[rowCount, collumnsCount++].Value = product.UnitPrice.ToString("F2");
                        }
                        else if (this.CreatorService.PriceType == PriceType.Netto)
                        {
                            worksheet.Cells[rowCount, collumnsCount++].Value = this.CreatorService.FinalizePrice(product);
                        }
                        else if (this.CreatorService.PriceType == PriceType.RekNetto)
                        {
                            worksheet.Cells[rowCount, collumnsCount++].Value = this.CreatorService.FinalizePrice(product);
                            worksheet.Cells[rowCount, collumnsCount++].Value = product.UnitPrice.ToString("F2");
                        }
                        else if (this.CreatorService.PriceType == PriceType.Kund)
                        {
                            worksheet.Cells[rowCount, collumnsCount++].Value = this.CreatorService.FinalizePrice(product);
                        }
                        else if (this.CreatorService.PriceType == PriceType.KundRek)
                        {
                            worksheet.Cells[rowCount, collumnsCount++].Value = this.CreatorService.FinalizePrice(product);
                            worksheet.Cells[rowCount, collumnsCount++].Value = product.UnitPrice.ToString("F2");
                        }
                        else
                        {
                            worksheet.Cells[rowCount, collumnsCount++].Value = product.UnitPrice.ToString("F2");
                        }

                        worksheet.Cells[rowCount, collumnsCount++].Value = product.PrimaryEANCode;

                        if (this.CreatorService.ShowPackagingMeasurment)
                        {
                            worksheet.Cells[rowCount, collumnsCount++].Value = product.QtyPerUnitOfMeasure.ToString("F0") + " " + languageVariables["measurment"];
                        }

                        if (this.CreatorService.ShowInStock)
                        {
                            if (this.CreatorService.ShowInStockCount)
                            {
                                worksheet.Cells[rowCount, collumnsCount++].Value = product.AvailableQty > 0 ? (product.AvailableQty.ToString("F0") + " " + languageVariables["measurment"]) : languageVariables["no"];
                            }
                            else
                            {
                                worksheet.Cells[rowCount, collumnsCount++].Value = product.AvailableQty > 0 ? languageVariables["yes"] : languageVariables["no"];
                            }
                        }

                        var imgPath = this.ImageService.GetObseleteImagePath(product.VariantId);
                        var pic = worksheet.Drawings.AddPicture(("image" + rowCount), imgPath);
                        var left = (worksheet.Dimension.End.Column - pic.From.Column + 1) / 2;
                        var top = (worksheet.Dimension.End.Row - pic.From.Row + 1) / 2;
                        pic.SetSize(60, 50);
                        pic.SetPosition(rowCount - 1, 10, collumnsCount++ - 1, left);

                        worksheet.Cells[rowCount, collumnsCount].Hyperlink = new Uri(FileCreatorUtilities.GetCreatorUtilities().GetImageClickLink(this.CreatorService.Language) + product.VariantId);
                        worksheet.Cells[rowCount, collumnsCount].Value = "Link";

                        worksheet.Row(rowCount).Height = 50;

                        rowCount++;
                        collumnsCount = 1;
                    }

                    foreach (var cell in worksheet.Cells)
                    {
                        cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cell.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Cells.AutoFitColumns();

                    package.SaveAs(stream);
                }
            }

            return stream;
        }

        public void Dispose()
        {

        }
    }
}
