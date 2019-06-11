using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
    public class DataType
    {
        public Dictionary<string, string> Gettype { get; set; }

        public Dictionary<string, string> GetSQLType { get; set; }

        public DataType()
        {
            Dictionary<string, string> GetType = new Dictionary<string, string>();
            GetType.Add("ID", "string");
            GetType.Add("0DP", "int");
            GetType.Add("2DP", "double");
            GetType.Add("PA", "string");
            GetType.Add("DMS", "double");
            GetType.Add("1DP", "double");
            GetType.Add("2SF", "double");
            GetType.Add("3DP", "double");
            GetType.Add("3SF", "string");
            GetType.Add("DT", "string");
            GetType.Add("INT", "int");
            GetType.Add("MC", "double");
            GetType.Add("T", "string");
            GetType.Add("Y/N", "bool");
            GetType.Add("无", "string");
            GetType.Add("string", "string");
            this.Gettype = GetType;


            GetSQLType = new Dictionary<string, string>()
            {
                //{ "System.Byte"        ,"[tinyint]"        },
                //{ "System.Int16"       ,"[smallint]"       },
                //{ "System.Int32"       ,"[int]"            },
                //{ "System.Int64"       ,"[bigint]"         },
                //{ "System.Byte[]"      ,"[varbinary](MAX)" },
                //{ "System.Boolean"     ,"[bit]"            },
                //{ "System.String"      ,"[nvarchar](MAX)"  },
                //{ "System.DateTime"    ,"[datetime]"       },
                //{ "System.Single"      ,"[real]"           },      //32位单精度浮点数,Float
                //{ "System.Double"      ,"[float]"          },      //64位单精度浮点数
                //{ "System.Decimal"     ,"[decimal](18,4)"  },      //SQL中decimal类似于numeric

                { "int"                ,"[int]"            },
                { "bool"               ,"[bit]"            },
                { "dateTime"           ,"[datetime]"       },
                { "string"             ,"[nvarchar](MAX)"  },
                { "double"             ,"[float]"          },
            };
        }
    }
}
