// UploadDownload File
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    // On trouve ici tout ce qui se rapporte au Web
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public FileResult GetXlsFile()
        {
            var arr = GenerateTemplateExcel();
            return File(arr, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GetProducts.xlsx");
        }

        [HttpPost]
        public JsonResult Upload()
        {
            var result = 0;
            if (Request.Files.Count > 0 && Request.Files[0] != null && Request.Files[0].ContentLength > 0)
            {
                var file = Request.Files[0];
                result = ParseFileAndReturnNumberOfLine(file);
            }
            return Json($"File Processed (number of line {result})", JsonRequestBehavior.DenyGet);
        }

        private int ParseFileAndReturnNumberOfLine(HttpPostedFileBase file)
        {
            var numberOfLine = 0;
            using (var package = new ExcelPackage(file.InputStream))
            {
                var workBook = package.Workbook;
                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        var currentWorksheet = workBook.Worksheets.First();
                        var startRow = 2;
                        for (int rowNumber = startRow; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                        {
                            var products = new UserProductModel();
                            int columnIndex = 1;
                            if (currentWorksheet.Cells[rowNumber, columnIndex].Value != null)
                            {
                                products.ProductName = currentWorksheet.Cells[rowNumber, columnIndex].Value.ToString();
                            }

                            columnIndex++;
                            if (currentWorksheet.Cells[rowNumber, columnIndex].Value != null)
                            {
                                products.Category = currentWorksheet.Cells[rowNumber, columnIndex].Value.ToString();
                            }

                            columnIndex++;
                            if (currentWorksheet.Cells[rowNumber, columnIndex].Value != null)
                            {
                                decimal price = 0.0M;
                                decimal.TryParse(currentWorksheet.Cells[rowNumber, columnIndex].Value.ToString(), out price);
                                products.Price = price;
                            }

                            columnIndex++;
                            if (currentWorksheet.Cells[rowNumber, columnIndex].Value != null)
                            {
                                products.Model = currentWorksheet.Cells[rowNumber, columnIndex].Value.ToString();
                            }
                            numberOfLine++;
                        }
                    }
                }
                return numberOfLine;
            }
        }

        private static byte[] GenerateTemplateExcel()
        {
            byte[] str;
            List<UserProductModel> products = new UserProductModel[]{
                new UserProductModel { Id = 1, ProductName = "Yamaha", Category = "Moto", Price = 12000, Model = "MT09 SP"},
                new UserProductModel { Id = 2, ProductName = "BMW", Category = "Moto", Price = 21000, Model = "S1000R"},
                new UserProductModel { Id = 3, ProductName = "Honda", Category = "Moto", Price = 13000, Model = "Hornet 1000"},
                new UserProductModel { Id = 4, ProductName = "Yamaha", Category = "Moto", Price = 8999, Model = "MT07"},
                new UserProductModel { Id = 5, ProductName = "BMW", Category = "Moto", Price = 23000, Model = "S1000RR"},
                new UserProductModel { Id = 6, ProductName = "Honda", Category = "Moto", Price = 17500, Model = "CB 1000 F"}
            }.ToList();

            int i = 1;
            using (var xls = new ExcelPackage())
            {
                var sheet = xls.Workbook.Worksheets.Add("Products");
                sheet.Cells[1, 1].Value = "Name";
                sheet.Cells[1, 2].Value = "Category";
                sheet.Cells[1, 3].Value = "Price";
                sheet.Cells[1, 4].Value = "Model";

                foreach (var item in products)
                {
                    i++;
                    sheet.Cells[i, 1].Value = item.ProductName;
                    sheet.Cells[i, 2].Value = item.Category;
                    sheet.Cells[i, 3].Value = item.Price.ToString("#.##");
                    sheet.Cells[i, 4].Value = item.Model;
                }
                str = xls.GetAsByteArray();
            }
            return str;
        }

    }
}
