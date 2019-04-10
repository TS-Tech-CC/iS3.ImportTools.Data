using iS3.ImportTools.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Models;
using Newtonsoft.Json;

namespace iS3.ImportTools.DataStanardTool.DSExporter
{
    public class Exporter_For_JSON : IDSExporter
    {
        public bool Export(PmEntiretyDef dataStandard, string path)
        {            
            return write2Json(dataStandard,path).Equals(null);
        }
        public string write2Json(PmEntiretyDef dataStandard,string path)
        {
            string json =JsonConvert.SerializeObject(dataStandard);

            return json;
        }
        public bool json2File()
        {
            return true;
        }
    }
}
