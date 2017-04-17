using HelperLibrary.CsvUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReaderAndWriterTest();
            ReadToObjectTest();

            Console.ReadLine();
        }


        static void ReadToObjectTest()
        {
            string filePath = "Users.csv";


            var reader = new CsvReader();

            var users = reader.ReadData<User>(File.OpenText(filePath));

            foreach(var u in users)
            {
                Console.WriteLine(u);
            }
        }

        static void ReaderAndWriterTest()
        {
            string filePath = "example.csv";
            var reader = new CsvReader();
            var writer = new CsvWriter();
            string content = null;
            List<List<string>> result;


            Console.WriteLine("    read from text.\n");
            content = File.ReadAllText(filePath);
            result = reader.Read(content);

            PrintContent(result);

            Console.WriteLine("    read from file directly.\n");
            using (var fr = File.OpenText(filePath))
            {
                result = reader.Read(fr);
            }

            PrintContent(result);

            Console.WriteLine("    write as a string in memory\n");
            content = writer.Write(result);
            Console.WriteLine(content);


            Console.WriteLine("    write to a TextWriter(Console.Out)\n");
            writer.WriteTo(result, Console.Out);

            // for reading large files, using this overload to reduce memory usage.
            using (var fr = File.OpenText(filePath))
            {
                using (var fw = File.CreateText("test2.csv"))
                {
                    // write once every 100 rows
                    int groupSize = 100;
                    reader.Read(fr, group => writer.WriteTo(group, fw), groupSize);
                }
            }

        }

        private static void PrintContent(List<List<string>> result)
        {
            Console.WriteLine(" * * * * * * * * * * *");
            foreach (var row in result)
            {
                foreach (var cell in row)
                {
                    Console.Write("{0}\t", cell);
                }
                Console.WriteLine();
            }
            Console.WriteLine(" * * * * * * * * * * *");
        }
    }
}
