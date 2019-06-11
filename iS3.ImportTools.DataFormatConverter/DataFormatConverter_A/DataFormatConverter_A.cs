using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;

namespace iS3.ImportTools.DataFormatConverter
{
    public class DataFormatConverter_A : IDataFormatConverter
    {
        public DataCarrier Convert(object rawData)
        {

            PmDGObjectDef srcDGObjectDef = new PmDGObjectDef()
            {
                Code = "FSSensor",
                Desctiption = "XXXXXXXXXXXXXXX",
                PropertyContainer = new List<PropertyMeta>()
                {
                    new PropertyMeta(){ PropertyName = "ID" ,DataType = typeof(int)},
                    new PropertyMeta(){ PropertyName = "SensorID" ,DataType = typeof(string)},
                    new PropertyMeta(){ PropertyName = "DateTime" ,DataType = typeof(DateTime)},
                    new PropertyMeta(){ PropertyName = "Value1" ,DataType = typeof(double)},
                    new PropertyMeta(){ PropertyName = "Value2" ,DataType = typeof(double)},
                    new PropertyMeta(){ PropertyName = "Value3" ,DataType = typeof(double)},
                    new PropertyMeta(){ PropertyName = "Value4" ,DataType = typeof(double)},
                }
            };



            DataCarrier srcDC = new DataCarrier(srcDGObjectDef);

            srcDC.DataContainer.Rows.Add(rawData.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));


            return srcDC;
        }
    }
}
