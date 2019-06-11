using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFormatConverter_DataTable
{
    public class DataFormatConverter_DataTable : IDataFormatConverter
    {
        public DataCarrier Convert(object rawData)
        {

            DataCarrier srcDC = new DataCarrier();
            srcDC.DataContainer = rawData as DataTable;

            srcDC.DataDef = new PmDGObjectDef()
            {
                Code = "",
                Desctiption = "",
                PropertyContainer = new List<PropertyMeta>()
            };

            foreach (DataColumn item in srcDC.DataContainer.Columns)
            {
                srcDC.DataDef.PropertyContainer.Add(new PropertyMeta() { PropertyName = item.ColumnName });
            }

            return srcDC;

        }
    }
}
