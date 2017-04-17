/* 
 * FileName:    User.cs
 * Author:      functionghw<functionghw@hotmail.com>
 * CreateTime:  2017/4/17 13:23:18
 * Version:     v1.0
 * Description:
 * */

using HelperLibrary.CsvUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProject
{
    [CsvConfig]
    public class User
    {
        [CsvColumn(Index = 0)]
        public int Id { get; set; }

        [CsvColumn(Index = 1)]
        public string Name { get; set; }

        [CsvColumn(Index = 2)]
        public DateTime CreateTime { get; set; }

        [CsvColumn(Index = 3)]
        public bool IsAdmin { get; set; }

        public override string ToString()
        {
            return $"{{ Id={Id}, Name={Name}, CreateTime={CreateTime}, IsAdmin={IsAdmin} }}";
        }
    }
}
