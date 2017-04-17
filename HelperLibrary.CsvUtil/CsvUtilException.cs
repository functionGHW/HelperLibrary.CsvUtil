using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.CsvUtil
{
    public class CsvUtilException : Exception
    {
        public CsvUtilException(string message) : base(message)
        {
        }

        public CsvUtilException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
