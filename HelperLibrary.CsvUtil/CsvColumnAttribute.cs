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

        public CsvColumnAttribute(string name) { }

        public string Name { get; set; }

        public int Index { get; set; } = -1;

        public Type Converter { get; set; }
    }
}
