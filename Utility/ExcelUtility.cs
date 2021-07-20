using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Carrental.Utility
{
    public class ExcelUtility
    {
        private static List<string> GetExcelColumnHeadersByPassingWS(ExcelWorksheet ws)
        {
            List<string> excelColumnHeaders = new List<string>();
            int actualColumns = ws?.Dimension?.End?.Column ?? 0;
            int consideredColumns = 0;

            if (actualColumns == 0)
                return null;

            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            {
                string firstColumn = firstRowCell.Text;
                excelColumnHeaders.Add(firstColumn);
                consideredColumns++;
            }
            return excelColumnHeaders;
        }
        public static IEnumerable<T> GetExcelData<T>(Stream stream) where T : new()
        {
            List<T> returnList = new List<T>();
            var obj = new T();
            var properties = obj.GetType().GetProperties();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excelPack = new ExcelPackage())
            {
                excelPack.Load(stream);

                ExcelWorksheet ws = excelPack.Workbook.Worksheets[0];

                List<string> excelColumnHeaders = GetExcelColumnHeadersByPassingWS(ws);

                for (int rowNum = 2; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var returnObject = new T();

                    foreach (var columnHeader in excelColumnHeaders)
                    {
                        var excelHeaderColumnIndex = excelColumnHeaders.IndexOf(columnHeader);
                        var property = properties.FirstOrDefault(x => x.Name == columnHeader);
                        var excelCellText =ws.Cells[rowNum, excelHeaderColumnIndex + 1].Value.ToString();
                        if (property != null)
                        {
                            if (property.PropertyType == typeof(DateTime))
                            {
                                var dateTime = DateTime.ParseExact(excelCellText,"MM/dd/yyyy", CultureInfo.InvariantCulture);
                               // var value = Convert.ChangeType(dateTime, property.PropertyType);

                                property.SetValue(returnObject, dateTime);
                            }
                            else
                            {
                                var value = Convert.ChangeType(excelCellText, property.PropertyType);

                                property.SetValue(returnObject, value);
                            }
                        }
                    }

                    returnList.Add(returnObject);
                }
            }

            return returnList;
        }
    }
}
