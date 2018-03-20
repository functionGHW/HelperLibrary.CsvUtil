using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace HelperLibrary.CsvUtil
{
    internal class Args
    {
        internal Args(Func<int, char> readChar, Func<int, bool> isEndOfContent, Action<List<List<string>>> groupHandler, int groupSize = 50)
        {
            Contract.Assert(readChar != null);
            Contract.Assert(isEndOfContent != null);
            Contract.Assert(groupHandler != null);
            
            if (groupSize < 1)
                groupSize = 1;

            CurrentRow = new List<string>();
            Index = 0;
            LineCount = 1;

            State = ReadState.CellBegin;
            tmp = new StringBuilder();

            this.readChar = readChar;
            this.isEndOfContent = isEndOfContent;
            this.groupHandler = groupHandler;
            this.groupSize = groupSize;
            group = new List<List<string>>(groupSize);
        }

        private Func<int, bool> isEndOfContent;

        private Func<int, char> readChar;

        private Action<List<List<string>>> groupHandler;

        private int groupSize;

        // current state
        public ReadState State;

        // index of char to read next
        public int Index;

        // number of current text line to read
        public long LineCount;

        // to save reading result 
        private List<List<string>> group;

        // to cache content of current row
        public List<string> CurrentRow;

        // using as a cell stack, cache cell content read.
        private StringBuilder tmp;

        public char ReadChar()
        {
            return readChar(Index++);
        }

        public bool IsEndOfContent()
        {
            return isEndOfContent(Index);
        }

        // push stack content as a cell and then clear it.
        public void PushCell()
        {
            CurrentRow.Add(tmp.ToString());
            tmp.Clear();
        }

        // push current row to result and then begin a new line.
        public void PushRow()
        {
            group.Add(CurrentRow);
            if (group.Count >= groupSize)
            {
                Flush();
            }
            CurrentRow = new List<string>();
        }

        public void Flush()
        {
            if (group.Count > 0)
            {
                groupHandler(group.ToList());
                group.Clear();
            }
        }

        // push a char to the cell stack.
        public void PushChar(char ch)
        {
            tmp.Append(ch);
        }
    }

}
