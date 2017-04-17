/* 
 * FileName:    ICsvDataConverter.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/4/17 13:41:49
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
    public interface ICsvDataConverter
    {
        object CsvContentToData(string value, Type TargetType);
    }
}
