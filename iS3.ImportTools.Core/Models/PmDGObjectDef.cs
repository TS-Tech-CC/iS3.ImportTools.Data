using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
    public class PmDGObjectDef:LangBase
    { 
        public string Code { get; set; }
        public string Desctiption { get; set; }
        public List<PropertyMeta> PropertyContainer { get; set; }
        public PmDGObjectDef()
        {
            PropertyContainer = new List<PropertyMeta>();
        }











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

            var dic = new DataType().GetSQLType;
            string Ryu = string.Empty;

            foreach (PropertyMeta itemPM in PropertyContainer)
            {
                Ryu += $"[{itemPM.PropertyName}]            {dic[itemPM.DataType]}          {(itemPM.Nullable?"NULL": "NOT NULL")}            {(itemPM.IsKey? "IDENTITY(1,1)" : "")},";
            }


            Ryu = "CREATE TABLE TableName (" + Ryu + ")";
            return Ryu;
        }
    }

    
}
