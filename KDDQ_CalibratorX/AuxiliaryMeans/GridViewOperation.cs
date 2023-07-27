using Krypton.Toolkit;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AuxiliaryMeans
{
    public static class GridViewOperation
    {
        public static void LoadExcelFile(string filePath, Krypton.Toolkit.KryptonDataGridView gridview1, Krypton.Toolkit.KryptonDataGridView gridview2)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(file);
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                // 读取第一个工作表
                ISheet sheet1 = workbook.GetSheetAt(0);
                dt1 = ConvertSheetToDataTable(sheet1);
                gridview1.DataSource = dt1;

                // 读取第二个工作表
                ISheet sheet2 = workbook.GetSheetAt(1);
                dt2 = ConvertSheetToDataTable(sheet2);
                gridview2.DataSource = dt2;
            }
        }

        private static DataTable ConvertSheetToDataTable(ISheet sheet)
        {
            DataTable dt = new DataTable();
            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                dt.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null || row.Cells.All(d => d == null || string.IsNullOrWhiteSpace(d.ToString())))
                {
                    // 这一行是空的，跳过
                    continue;
                }

                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    ICell cell = row.GetCell(j);
                    if (cell != null)
                    {
                        dataRow[j] = cell.ToString();
                    }
                    else
                    {
                        dataRow[j] = ""; // 或者你可以设置为其他的默认值
                    }
                }

                dt.Rows.Add(dataRow);
            }

            return dt;
        }


        public static void DeleteCell(object sender, EventArgs e, Krypton.Toolkit.KryptonDataGridView gridview)
        {
            if (gridview.CurrentCell != null)
            {
                gridview.CurrentCell.Value = null;
            }
        }

        public static void DeleteRow(object sender, EventArgs e, Krypton.Toolkit.KryptonDataGridView gridview)
        {
            try
            {
                if (gridview.CurrentRow != null)
                {
                    gridview.Rows.Remove(gridview.CurrentRow);
                }
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.ToString());
            }

        }

        public static void AddRow(object sender, EventArgs e, Krypton.Toolkit.KryptonDataGridView gridview)
        {
            gridview.Rows.Add();
        }




        public static void CopyCell(object sender, EventArgs e, Krypton.Toolkit.KryptonDataGridView gridview)
        {
            if (gridview.CurrentCell != null)
            {
                Clipboard.SetText(gridview.CurrentCell.Value.ToString());
            }
        }
    }
}
