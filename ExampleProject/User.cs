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
    [CsvConfiguration(FirstDataRowIndex = 2)]
    public class User
    {
        [CsvColumn("No")]
        public int Id { get; set; }

        [CsvColumn()]
        public string Name { get; set; }

        [CsvColumn(Index = 2)]
        public DateTime CreateTime { get; set; }

        [CsvColumn("")]
        public bool? IsAdmin { get; set; }

        public override string ToString()
        {
            string isAdmin = "null";
            if (IsAdmin.HasValue)
            {
                isAdmin = IsAdmin.Value ? bool.TrueString : bool.FalseString;
            }
            return $"{{ Id={Id}, Name={Name}, CreateTime={CreateTime}, IsAdmin={isAdmin} }}";
        }
    }
}
