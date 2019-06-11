using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
    public class PmDGObjectDef:LngBase
    { 
        public string Code { get; set; }
        public string Desctiption { get; set; }
        public List<PropertyMeta> PropertyContainer { get; set; }
        public PmDGObjectDef()
        {
            PropertyContainer = new List<PropertyMeta>();
        }


        private Dictionary<string, string> TypeDic = new Dictionary<string, string>()
        {
            { "System.Byte"        ,"[tinyint]"        },
            { "System.Int16"       ,"[smallint]"       },
            { "System.Int32"       ,"[int]"            },
            { "System.Int64"       ,"[bigint]"         },

            { "System.Byte[]"      ,"[varbinary](MAX)" },
            { "System.Boolean"     ,"[bit]"            },


            { "System.String"      ,"[nvarchar](MAX)"  },

            { "System.DateTime"    ,"[datetime]"       },

            { "System.Single"      ,"[real]"           },      //32位单精度浮点数,Float
            { "System.Double"      ,"[float]"          },      //64位单精度浮点数

            { "System.Decimal"     ,"[decimal](18,4)"  },      //SQL中decimal类似于numeric

        };

        public string GetTableCreateSQL()
        {
            /*
           
            CREATE TABLE [dbo].[Monitoring_MonData](
	                                                    [ID]                [int] IDENTITY(1,1) NOT NULL,
	                                                    [SensorName]        [nvarchar](max) NULL,
	                                                    [Part]              [nvarchar](max) NULL,
	                                                    [AcqTime]           [datetime] NULL,
	                                                    [RecTime]           [datetime] NULL,
	                                                    [Value]             [decimal](18, 2) NULL,
	                                                    [Data]              [decimal](18, 2) NULL,
	                                                    [CurrVariation]     [decimal](18, 2) NULL,
	                                                    [AccuVariation]     [decimal](18, 2) NULL,
	                                                    [VariationRate]     [decimal](18, 2) NULL,
	                                                    [ValuePerDesign]    [decimal](18, 2) NULL,
	                                                    [Remark]            [nvarchar](max) NULL,
                                                   )

             */


            string Ryu = string.Empty;

            foreach (PropertyMeta itemPM in PropertyContainer)
            {
                Ryu += $"[{itemPM.PropertyName}]            {TypeDic[itemPM.DataType.ToString()]}          {(itemPM.NotNull?"NOT NULL":"NULL")}            {(itemPM.isKey? "IDENTITY(1,1)" : "")},";
            }


            Ryu = "CREATE TABLE TableName (" + Ryu + ")";
            return Ryu;
        }
    }

    
}
