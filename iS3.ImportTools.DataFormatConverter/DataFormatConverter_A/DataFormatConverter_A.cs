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
                    new PropertyMeta(){ PropertyName = "ID" ,DataType = "int"},
                    new PropertyMeta(){ PropertyName = "SensorID" ,DataType = "string"},
                    new PropertyMeta(){ PropertyName = "DateTime" ,DataType = "dateTime"},
                    new PropertyMeta(){ PropertyName = "Value1" ,DataType = "double"},
                    new PropertyMeta(){ PropertyName = "Value2" ,DataType = "double"},
                    new PropertyMeta(){ PropertyName = "Value3" ,DataType = "double"},
                    new PropertyMeta(){ PropertyName = "Value4" ,DataType = "double"},
                }
            };



            DataCarrier srcDC = new DataCarrier(srcDGObjectDef);

            srcDC.DataContainer.Rows.Add(rawData.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));


            return srcDC;
        }
    }
}
