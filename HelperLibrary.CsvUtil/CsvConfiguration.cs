using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.CsvUtil
{
    public class CsvConfiguration
    {
        /// <summary>
        /// Get or set row index of starting row of data.
        /// </summary>
        public int FirstDataRowIndex { get; set; }

        /// <summary>
        /// Get or set row index of column name row(header row).
        /// </summary>
        public int ColumnNameRowIndex { get; set; }

        /// <summary>
        /// Get or set if there is no column name row.
        /// When set to true, the ColumnNameRowIndex property are ignored.
        /// </summary>
        public bool NoColumnNameRow { get; set; }
    }
}
