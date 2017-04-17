using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.CsvUtil
{
    public class CsvConfiguration
    {
        public int FirstDataRowIndex { get; set; } = -1;

        public int ColumnNameRowIndex { get; set; } = -1;

        public bool NoColumnNameRow { get; set; }
    }
}
