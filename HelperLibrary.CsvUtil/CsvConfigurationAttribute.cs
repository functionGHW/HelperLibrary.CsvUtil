/* 
 * FileName:    CsvConfigAttribute.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/4/17 13:28:32
 * Version:     v1.0
 * Description:
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.CsvUtil
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CsvConfigurationAttribute : Attribute
    {
        public CsvConfigurationAttribute() { }
        
        /// <summary>
        /// Get or set row index of starting row of data.
        /// </summary>
        public int FirstDataRowIndex { get; set; } = -1;

        /// <summary>
        /// Get or set row index of column name row(header row).
        /// </summary>
        public int ColumnNameRowIndex { get; set; } = -1;

        /// <summary>
        /// Get or set if there is no column name row.
        /// When set to true, the ColumnNameRowIndex property are ignored.
        /// Note: If true, indexes of all Column must be given by CsvColumnAttribute.Index property.
        /// </summary>
        public bool NoColumnNameRow { get; set; }
    }
}
