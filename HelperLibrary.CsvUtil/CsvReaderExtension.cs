/* 
 * FileName:    CsvReaderExtension.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/4/17 13:56:37
 * Version:     v1.0
 * Description:
 * */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.CsvUtil
{
    public static class CsvReaderExtension
    {
        public static List<T> ReadData<T>(this CsvReader csvReader, TextReader reader) where T : new()
        {
            var result = csvReader.Read(reader);

            return ConvertToObject<T>(result);
        }

        private static List<T> ConvertToObject<T>(List<List<string>> result) where T : new()
        {
            var propConfigs = CreatePropertyConfig(typeof(T));
            var type = typeof(T);

            var list = new List<T>();
            foreach (var r in result)
            {
                var model = new T();

                foreach (var p in propConfigs)
                {
                    var prop = type.GetProperty(p.Property);

                    string valueStr = r[p.ColumnIndex];
                    object value = GetConverter(p.Converter).CsvContentToData(valueStr, prop.PropertyType);
                    prop.SetValue(model, value);
                }
                list.Add(model);
            }

            return list;
        }

        private static ICsvDataConverter GetConverter(string converter)
        {
            return DefaultDataConverter.Instance;
        }

        private static List<PropertyConfiguration> CreatePropertyConfig(Type modelType)
        {
            var list = new List<PropertyConfiguration>();

            var props = modelType.GetProperties();

            foreach (var prop in props)
            {
                var propConfig = CreateOnePropConfig(prop);
                if (propConfig != null)
                {
                    list.Add(propConfig);
                }
            }
            return list;
        }

        private static PropertyConfiguration CreateOnePropConfig(PropertyInfo prop)
        {
            var colAttr = prop.GetCustomAttribute<CsvColumnAttribute>();
            if (colAttr == null)
                return null;

            var result = new PropertyConfiguration()
            {
                Property = prop.Name,
                ColumnName = colAttr.Name,
                ColumnIndex = colAttr.Index,
                //Converter = colAttr.Converter == null ? null : colAttr.Converter.AssemblyQualifiedName,
            };
            // 未指定Column和ColumnIndex配置，则默认使用属性名称作为列名
            if (result.ColumnName == null && result.ColumnIndex < 0)
                result.ColumnName = prop.Name;

            return result;
        }
    }
}
