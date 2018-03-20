# HelperLibrary.CsvUtil
a demo class library for CSV file reading and writing

Support comma, double quote, and line break.

Basic usage:
```csharp
using HelperLibrary.CsvUtil;

var reader = new CsvReader();
var rows =  reader.Read(File.ReadAllText("path_to_csv_file"));

foreach (var row in rows)
{
    foreach (string cell in row)
    {
        Console.Write(cell + ", ");
    }
    Console.WriteLine();
}


List<List<string>> content = ...;
var writer = new CsvWriter();
File.WriteAllText("path_to_save", writer.Write(content));
```

For more examples, see "ExampleProject".
