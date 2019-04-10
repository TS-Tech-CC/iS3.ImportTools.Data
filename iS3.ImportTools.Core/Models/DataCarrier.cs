using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
    public class DataCarrier
    {
        public PmDGObjectDef DataDef { get; set; }
        public DataTable DataContainer { get; set; }

        public DataCarrier(PmDGObjectDef ObjDef)
        {
            DataDef = ObjDef;

            DataContainer = new DataTable(ObjDef.Code);
            foreach (PropertyMeta itemPM in ObjDef.PropertyContainer)
            {
                DataContainer.Columns.Add(itemPM.PropertyName);
            }

        }
    }
}
