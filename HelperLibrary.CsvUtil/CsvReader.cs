using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HelperLibrary.CsvUtil
{
    /// <summary>
    /// CSV content reader
    /// </summary>
    public class CsvReader
    {
        /// <summary>
        /// Read content from a TextReader, and handle 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="groupHandler"></param>
        /// <param name="groupSize"></param>
        public void Read(TextReader reader, Action<List<List<string>>> groupHandler, int groupSize = 50)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (groupHandler == null)
                throw new ArgumentNullException(nameof(groupHandler));

            var arg = new Args(readChar: i => (char)reader.Read(),
                isEndOfContent: i => reader.Peek() == -1,
                groupHandler: groupHandler,
                groupSize: groupSize);

            Read(arg);
        }

        /// <summary>
        /// Read content from a TextReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<List<string>> Read(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            var rows = new List<List<string>>();
            var arg = new Args(readChar: i => (char)reader.Read(),
                isEndOfContent: i => reader.Peek() == -1,
                groupHandler: rs => rows.AddRange(rs));

            Read(arg);
            return rows;
        }

        /// <summary>
        /// Read content from text
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public List<List<string>> Read(string content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            var rows = new List<List<string>>();
            var arg = new Args(readChar: i => content[i],
                isEndOfContent: i => i >= content.Length,
                groupHandler: rs => rows.AddRange(rs));

            Read(arg);
            return rows;
        }

        private void Read(Args arg)
        {
            // using FSM(finite-state machine)
            while (!arg.IsEndOfContent())
            {
                switch (arg.State)
                {
                    case ReadState.CellBegin:
                        OnCellBegin(arg);
                        break;
                    case ReadState.CellReading:
                        OnCellReading(arg);
                        break;
                    case ReadState.QuoteCell:
                        OnQuoteCell(arg);
                        break;
                    case ReadState.QuoteCellChkEnd:
                        OnQuoteCellChkEnd(arg);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(arg.State));
                }
            }

            // check for ending
            switch (arg.State)
            {
                case ReadState.CellBegin:
                    if (arg.CurrentRow.Count > 0)
                    {
                        arg.PushCell();
                        arg.PushRow();
                    }
                    break;
                case ReadState.CellReading:
                case ReadState.QuoteCell:
                case ReadState.QuoteCellChkEnd:
                    arg.PushCell();
                    arg.PushRow();
                    break;
            }
            arg.Flush();
        }

        private void OnQuoteCellChkEnd(Args arg)
        {
            var ch = arg.ReadChar();
            switch (ch)
            {
                case '"':
                    arg.PushChar('"');
                    arg.State = ReadState.QuoteCell;
                    break;
                case ',':
                    arg.PushCell();
                    arg.State = ReadState.CellBegin;
                    break;
                case '\r':
                    break;
                case '\n':
                    arg.PushCell();
                    arg.PushRow();
                    arg.State = ReadState.CellBegin;
                    break;
                default:
                    arg.PushChar(ch);
                    arg.State = ReadState.CellReading;
                    break;
            }
        }
        private void OnQuoteCell(Args arg)
        {
            var ch = arg.ReadChar();
            switch (ch)
            {
                case '"':
                    arg.State = ReadState.QuoteCellChkEnd;
                    break;
                default:
                    if (ch == '\n')
                        arg.LineCount++;

                    arg.PushChar(ch);
                    break;
            }
        }

        private void OnCellReading(Args arg)
        {
            var ch = arg.ReadChar();
            switch (ch)
            {
                case ',':
                    arg.PushCell();
                    arg.State = ReadState.CellBegin;
                    break;
                case '\r':
                    break;
                case '\n':
                    arg.LineCount++;
                    arg.PushCell();
                    arg.PushRow();
                    arg.State = ReadState.CellBegin;
                    break;
                default:
                    arg.PushChar(ch);
                    break;
            }
        }

        private void OnCellBegin(Args arg)
        {
            var ch = arg.ReadChar();
            switch (ch)
            {
                case '\r':
                    break;
                case '\n':
                    if (arg.CurrentRow.Count > 0)
                    {
                        arg.PushCell();
                    }
                    arg.LineCount++;
                    arg.PushRow();
                    break;
                case ',':
                    arg.PushCell();
                    arg.State = ReadState.CellBegin;
                    break;
                case '"':
                    arg.State = ReadState.QuoteCell;
                    break;
                default:
                    arg.PushChar(ch);
                    arg.State = ReadState.CellReading;
                    break;
            }
        }
    }
}
