using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Space_Analyzer
{
    public class XLSWriter
    {
        FileStream my_stream;


        public XLSWriter(String the_path)
        {
            my_stream = new FileStream(@the_path, FileMode.Create, FileAccess.Write);
        }

        public void overwrite(List<List<String>> the_data)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet();
            for (int i = 0; i < the_data.Count(); i++)
            {
                List<String> row_data = the_data[i];
                IRow row = sheet.CreateRow(i);
                for (int j = 0; j < row_data.Count(); j++)
                {
                    String cell_data = row_data[j];
                    ICell cell = row.CreateCell(j, CellType.String);
                    cell.SetCellValue(cell_data);
                }
            }
            for (int i = 0; i < the_data.Count(); i++)
            {
                sheet.AutoSizeColumn(i);
            }
            book.Write(my_stream);
        }
    }
}