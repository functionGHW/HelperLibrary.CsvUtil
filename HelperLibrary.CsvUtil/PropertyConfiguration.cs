/* 
 * FileName:    PropertyConfiguration.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/4/17 14:19:55
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
    internal class PropertyConfiguration
    {
        /// <summary>
        /// Property name
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Index of column this property mapping to.
        /// Note: When property ColumnName was given, this property will be ignored.
        /// </summary>
        public int ColumnIndex { get; set; } = -1;

        /// <summary>
        /// Name of column this property mapping to.
        /// Note：keep this property null or empty string if you want to use property ColumnIndex.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Type of convert which is used to convert string value to target property value.
        /// Note: This type must implement ICsvDataConverter
        /// </summary>
        public Type Converter { get; set; }
    }
}
