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
        /// <summary>
        /// Read CSV data and convert to model list.
        /// </summary>
        /// <typeparam name="T">type of model</typeparam>
        /// <param name="csvReader"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> ReadData<T>(this CsvReader csvReader, TextReader reader) where T : new()
        {
            if (csvReader == null)
                throw new ArgumentNullException(nameof(csvReader));

            var list = new List<T>();

            csvReader.Read(reader, rows =>
            {
                var result = ConvertToObject<T>(rows);
                list.AddRange(result);
            });

            return list;
        }

        private static List<T> ConvertToObject<T>(List<List<string>> result) where T : new()
        {
            var type = typeof(T);

            var csvConfig = CreateCsvConfiguration(type);
            var propConfigs = CreatePropertyConfig(type);

            var headers = new ColumnNameIndexPair[0];
            int defaultFirstDataRow = csvConfig.NoColumnNameRow ? 0 : 1;
            int firstDataRow = csvConfig.FirstDataRowIndex < 0
                ? defaultFirstDataRow
                : csvConfig.FirstDataRowIndex;

            if (firstDataRow >= result.Count)
                throw new CsvUtilException("Invalid FirstDataRowIndex");

            if (csvConfig.NoColumnNameRow)
            {
                if (propConfigs.Any(p => !string.IsNullOrEmpty(p.ColumnName)))
                    throw new CsvUtilException("Can not use ColumnName when CSV configuration NoColumnNameRow = true\n"
                        + "Please only use ColumnIndex if the content doesn't have a row for column names, or set NoColumnNameRow = false");

            }
            else
            {
                int columnNameRow = csvConfig.ColumnNameRowIndex < 0 ? firstDataRow - 1 : csvConfig.ColumnNameRowIndex;

                if (columnNameRow >= firstDataRow)
                    throw new CsvUtilException("FirstDataRowIndex must be great than FirstDataRowIndex");

                var columnRow = result[columnNameRow];
                headers = columnRow.Select(cell => new ColumnNameIndexPair()
                {
                    Column = cell,
                    Index = columnRow.IndexOf(cell)
                }).ToArray();

            }

            var validMappings = (from c in propConfigs
                                 let tmp = headers.FirstOrDefault(h => h.Column == c.ColumnName)
                                 let hasColumn = !string.IsNullOrEmpty(c.ColumnName)
                                 where type.GetProperty(c.Property) != null && (!hasColumn || (hasColumn && tmp != null))
                                 select new
                                 {
                                     c.Property,
                                     c.Converter,
                                     Index = string.IsNullOrEmpty(c.ColumnName) ? c.ColumnIndex :
                                            tmp == null ? -1 : tmp.Index
                                 }).ToArray();

            var list = new List<T>();
            foreach (var r in result.Skip(firstDataRow))
            {
                var model = new T();
                foreach (var p in validMappings)
                {
                    if (p.Index >= r.Count)
                        continue;

                    var prop = type.GetProperty(p.Property);
                    string valueStr = r[p.Index];
                    var converter = GetConverter(p.Converter);
                    object value = converter.CsvContentToData(valueStr, prop.PropertyType);
                    prop.SetValue(model, value);
                }
                list.Add(model);
            }
            return list;
        }

        private static CsvConfiguration CreateCsvConfiguration(Type type)
        {
            var config = new CsvConfiguration();
            var attr = type.GetCustomAttribute<CsvConfigurationAttribute>();
            if (attr == null)
                return config;

            config.FirstDataRowIndex = attr.FirstDataRowIndex;
            config.ColumnNameRowIndex = attr.ColumnNameRowIndex;
            config.NoColumnNameRow = attr.NoColumnNameRow;

            return config;
        }

        private static ICsvDataConverter GetConverter(Type converter)
        {
            if (converter == null)
                return DefaultDataConverter.DefaultInstance;

            return Activator.CreateInstance(converter) as ICsvDataConverter;
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
                Converter = colAttr.Converter
            };
            // use property name as column name, if there is no column name and index given.
            if (string.IsNullOrEmpty(result.ColumnName) && result.ColumnIndex < 0)
                result.ColumnName = prop.Name;

            return result;
        }

        private class ColumnNameIndexPair
        {
            public string Column { get; set; }

            public int Index { get; set; }

            public override string ToString()
            {
                return $"{{ Column={Column}, Index={Index} }}";
            }
        }
    }
}
