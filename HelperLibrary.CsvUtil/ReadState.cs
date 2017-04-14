using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperLibrary.CsvUtil
{
    internal enum ReadState
    {
        // begin a new cell
        CellBegin,
        // reading a cell
        CellReading,
        // reading a cell that between double quotes.
        QuoteCell,
        // try to catch the end double quote char.
        QuoteCellChkEnd,
    }
}
