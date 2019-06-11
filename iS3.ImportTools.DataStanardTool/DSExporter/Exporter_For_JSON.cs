using iS3.ImportTools.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Models;
using Newtonsoft.Json;
using System.IO;
using iS3.ImportTools.DataStanardTool.StandardManager;

namespace iS3.ImportTools.DataStanardTool.DSExporter
{
    public class Exporter_For_JSON : IDSExporter
    {

        public bool Export(PmEntiretyDef dataStandard, string path = null)
        {
            try
            {
                string json = JsonConvert.SerializeObject(dataStandard);
                if (path == null)
                {
                    DirectoryInfo localPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                    path = localPath.Parent.Parent.FullName + "\\Standard\\" + dataStandard.Code + ".json";
                }
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(json);
                sw.Flush();
                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Export(StandardFilter filter)
        {

            string json = JsonConvert.SerializeObject(filter);
            return true;
        }

        public bool Export(PmDomainDef domain, string path = null)
        {
            throw new NotImplementedException();
        }
    }
}
