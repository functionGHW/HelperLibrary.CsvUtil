/* 
 * FileName:    CsvColumnAttribute.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/4/17 13:29:49
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
    [AttributeUsage(AttributeTargets.Property)]
    public class CsvColumnAttribute : Attribute
    {
        public CsvColumnAttribute() { }

        public CsvColumnAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Column name in CSV table
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Column index in CSV table, if property Name was given, this property will be ignored.
        /// </summary>
        public int Index { get; set; } = -1;

        /// <summary>
        /// Type of convert which is used to convert string value to target property value.
        /// Note: This type must implement ICsvDataConverter
        /// </summary>
        public Type Converter { get; set; }
    }
}
