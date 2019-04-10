using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;

namespace iS3.ImportTools.DataFormatConverter
{
    public class DataFormatConvert_ForFS : IDataFormatConverter
    {
        public DataCarrier Convert(string rawData)
        {

            PmDGObjectDef srcDGObjectDef = new PmDGObjectDef()
            {
                Code = "FSSensor",
                Desctiption = "Monitoring Point of FeiShang System",
                PropertyContainer = new List<PropertyMeta>()
                {
                    new PropertyMeta(){ PropertyName = "ID" },
                    new PropertyMeta(){ PropertyName = "SensorID" },
                    new PropertyMeta(){ PropertyName = "DateTime" },
                    new PropertyMeta(){ PropertyName = "Value1" },
                    new PropertyMeta(){ PropertyName = "Value2" },
                    new PropertyMeta(){ PropertyName = "Value3" },
                    new PropertyMeta(){ PropertyName = "Value4" },
                }
            };



            DataCarrier srcDC = new DataCarrier(srcDGObjectDef);

            srcDC.DataContainer.Rows.Add(rawData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));


            return srcDC;
        }
    }
}
