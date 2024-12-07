using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository.Data
{
    public static class SQlSyntax
    {
        public static string Varchar => "VARCHAR";
        public static string NVarchar => "NVARCHAR(max)";
        public static string Decimal => "decimal(18,2)";
        public static string Int => "INT";
        public static string DateTime => "DATETIME";
    }

    ///public enum SQlSynatx
    ///{
    ///    VARCHAR=0, NVARCHAR=1,decimal
    ///}

}
