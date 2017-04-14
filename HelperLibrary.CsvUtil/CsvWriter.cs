using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HelperLibrary.CsvUtil
{
    /// <summary>
    /// CSV content writer
    /// </summary>
    public class CsvWriter
    {
        /// <summary>
        /// Write CSV content to a TextWriter
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="writer"></param>
        public void WriteTo(IEnumerable<IEnumerable<string>> rows, TextWriter writer)
        {
            if (rows == null)
                throw new ArgumentNullException(nameof(rows));

            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            WriteTo(rows, s => writer.WriteLine(s));
        }

        /// <summary>
        /// Write CSV content as a string
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public string Write(IEnumerable<IEnumerable<string>> rows)
        {
            if (rows == null)
                throw new ArgumentNullException(nameof(rows));

            var contentBuilder = new StringBuilder();
            WriteTo(rows, s => contentBuilder.AppendLine(s));
            return contentBuilder.ToString();
        }
        
        private void WriteTo(IEnumerable<IEnumerable<string>> rows, Action<string> lineHandler)
        {
            foreach (var row in rows)
            {
                var processedRow = row.Select(PorcessContent);
                string line = string.Join(",", processedRow);
                lineHandler(line);
            }
        }

        private string PorcessContent(string content)
        {
            if (string.IsNullOrEmpty(content))
                return "";

            if (content.Contains("\""))
            {
                return "\"" + content.Replace("\"", "\"\"") + "\"";
            }
            if (content.Contains(",")
                || content.Contains("\n"))
            {
                return "\"" + content + "\"";
            }
            return content;
        }
    }
}
