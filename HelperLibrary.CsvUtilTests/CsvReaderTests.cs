using NUnit.Framework;
using HelperLibrary.CsvUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.CsvUtil.Tests
{
    [TestFixture()]
    public class CsvReaderTests
    {
        [TestCase("", 0, new int[0])]
        [TestCase("aaa,bbb,ccc", 1, new[] { 3 })]
        [TestCase("aaa,bbb,ccc,", 1, new[] { 4 })]
        [TestCase("aaa,bbb,ccc,\n", 1, new[] { 4 })]
        [TestCase("aaa,bbb,ccc,\n", 1, new[] { 4 })]
        [TestCase("aaa,bbb,ccc,ddd\neee,fff\n", 2, new[] { 4, 2 })]
        [TestCase("aaa,bbb,ccc,ddd\r\neee,fff\r\n", 2, new[] { 4, 2 })]
        [TestCase("aaa,bbb,ccc,ddd\n\neee,fff\n", 3, new[] { 4, 0, 2 })]
        [TestCase("aaa,bbb,ccc,\"ddd\neee\",fff\n", 1, new[] { 5 })]
        public void ReadFromStringTest(string content, int expectedRowCount, int[] cellCounts)
        {
            // setup
            var reader = new CsvReader();

            // act
            var result = reader.Read(content);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRowCount, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(cellCounts[i], result[i].Count);
            }
        }

        [TestCase("\"\"abc\",def", 2, "abc\"")]
        [TestCase("a\"abc\",def", 2, "a\"abc\"")]
        [TestCase("a\"\"abc\",def", 2, "a\"\"abc\"")]
        [TestCase("\"abc\"abc\",def", 2, "abcabc\"")]
        [TestCase("\"abc\"\"abc\",def", 2, "abc\"abc")]
        [TestCase(" \"abc\"\"abc\",def", 2, " \"abc\"\"abc\"")]
        public void ReadFromStringCommaTest(string content, int expectedCount, string expecetdStr)
        {
            // setup
            var reader = new CsvReader();

            // act
            var result = reader.Read(content);

            // assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(expectedCount, result[0].Count);
            Assert.AreEqual(expecetdStr, result[0][0]);
        }

        [Test()]
        public void ReadFromStringNullInputTest()
        {
            // setup
            var reader = new CsvReader();

            // act & assert
            Assert.Catch<ArgumentNullException>(
                () => reader.Read((string) null)
            );
            Assert.Catch<ArgumentNullException>(
                () => reader.Read((TextReader)null)
            );
            Assert.Catch<ArgumentNullException>(
                () => reader.Read(null, null)
            );
        }
    }
}