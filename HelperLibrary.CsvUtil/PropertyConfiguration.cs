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
        /// 属性名称
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 对应表格列的索引，-1表示不使用此配置。
        /// 注意：Column属性优先级高于ColumnIndex，如果存在ColumnName(不为null或空字符串)，则忽略该项。
        /// </summary>
        public int ColumnIndex { get; set; } = -1;

        /// <summary>
        /// 对应表格的列名称，规定表格起始行的第一行为列名称。
        /// 注意：如果要使用ColumnIndex配置，则该属性保持null或空字符串。
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 实现IDataConverter接口的数据转换类型的名称，包含命名空间。如果类型不在当前程序集中，需要加入程序集名称限定。
        /// 如果值为null，则默认使用DefaultDataConverter类型
        /// </summary>
        public string Converter { get; set; }
    }
}
