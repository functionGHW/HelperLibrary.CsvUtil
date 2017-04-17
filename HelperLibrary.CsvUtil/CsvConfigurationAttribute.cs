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

        public int FirstDataRowIndex { get; set; } = -1;

        public int ColumnNameRowIndex { get; set; } = -1;

        public bool NoColumnNameRow { get; set; }
    }
}
