using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;
using iS3.ImportTools.DataStanardTool.DSImporter;

namespace iS3.ImportTools.DataPropertyMapping
{
    public class DataPropertyMapping_ForFS : IPropertyMapping
    {
        public Dictionary<string, string> MappingRule { get; set; }
        public DataPropertyMapping_ForFS()
        {
            MappingRule = new Dictionary<string, string>();
        }






        public DataCarrier Mapping(DataCarrier rawCDF)
        {
            string aimDGObjectType = "Borehole";
            PmEntiretyDef def = new Importer_For_Json().GetSample();
            PmDGObjectDef aimDGObjectDef = def.getDGObjectDefByCode(aimDGObjectType);

            DataCarrier aimDC = new DataCarrier(aimDGObjectDef);

            foreach (DataRow itemdr in rawCDF.DataContainer.Rows)
            {
                aimDC.DataContainer.Rows.Add(new object[] { itemdr["ID"], itemdr["SensorID"],   itemdr["DateTime"],         itemdr["Value1"] });
                aimDC.DataContainer.Rows.Add(new object[] { itemdr["ID"], itemdr["SensorID"],   itemdr["DateTime"],         itemdr["Value2"] });
                aimDC.DataContainer.Rows.Add(new object[] { itemdr["ID"], DBNull.Value,         itemdr["DateTime"] + "123", itemdr["Value3"] });
                aimDC.DataContainer.Rows.Add(new object[] { itemdr["ID"], itemdr["SensorID"],   itemdr["DateTime"],         itemdr["Value4"] });
            }




            return aimDC;
        }





    }
}
