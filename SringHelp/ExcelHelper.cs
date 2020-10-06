using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace SringHelp
{
    public static class ExcelHelper
    {
        public static DataTable ReadExcelFileToDataTable(this IWorkbook workbook)
        {
            var dataTable = new DataTable();

            var sheet = workbook.GetSheetAt(0);
            var rowFirst = sheet.GetRow(0);

            dataTable.Columns.AddRange(rowFirst.Cells.Select(d => new DataColumn(d.ToString(), typeof(string))).ToArray());

            foreach (IRow row in sheet)
            {
                var daRow = dataTable.NewRow();
                int cellIndex = 0;
                foreach (var cell in row)
                {
                    daRow[cellIndex++] = cell?.ToString();
                }
            }
            if (dataTable.Rows.Count > 2)
            {
                dataTable.Rows.RemoveAt(0);
            }
            return dataTable;
        }

        /// <summary>
        /// 获取考生账号
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetUserAccounts(this IWorkbook workbook)
        {
            var sheet = workbook.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                yield return sheet.GetRow(i).GetCell(0)?.ToString();
            }
        }
        public static IWorkbook GetWorkBookFromFile(string fileName)
        {
            using Stream stream = File.OpenRead(fileName);

            if (Path.GetExtension(fileName).ToLower() == ".xls")
            {
                return new HSSFWorkbook(stream);
            }
            else
            {
                return new XSSFWorkbook(stream);
            }
        }
    }
}
