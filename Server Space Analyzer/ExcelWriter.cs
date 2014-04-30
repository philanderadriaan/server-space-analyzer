using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Disk_Space_Analyzer
{
    public class ExcelWriter
    {
        public ExcelWriter()
        {
        }

        public void overwrite(String the_path, List<List<String>> the_data)
        {
            int row_count = the_data.Count();
            XSSFWorkbook book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet();
            for (int i = 0; i < row_count; i++)
            {
                List<String> row_data = the_data[i];
                int column_count = row_data.Count();
                IRow row = sheet.CreateRow(i);
                for (int j = 0; j < column_count; j++)
                {
                    String cell_data = row_data[j];
                    ICell cell = row.CreateCell(j, CellType.String);
                    cell.SetCellValue(cell_data);
                }
            }
            for (int i = 0; i < row_count; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            FileStream stream = new FileStream(@the_path, FileMode.Create, FileAccess.Write);
            book.Write(stream);
        }
    }
}