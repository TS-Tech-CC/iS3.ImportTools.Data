using System;
using iS3.ImportTools.Core.Models;
using System.IO;
using iS3.ImportTools.DataStanardTool.DSImporter;
using iS3.ImportTools.Core.Interface;

namespace iS3.ImportTools.DataStanardTool.StandardManager
{
    public class StandardLoader
    {
        string path { get; set; }
        public StandardLoader()
        {
            DirectoryInfo localPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            path = localPath.Parent.Parent.Parent.FullName + "\\WareHouse\\Standard\\";
        }
        public PmEntiretyDef GetStandard()
        {
            IDSImporter importer = new Importer_For_Json();
            return importer.Import(path + "Geology.json");
        }

        public PmEntiretyDef GetStandard(string StandardName)
        {
            IDSImporter importer = new Importer_For_Json();
            return importer.Import(path + StandardName + ".json");
        }
        public StandardFilter CreateFilter()
        {
            var fullPath = Directory.GetFiles(path, "filter.json");

            if ((fullPath[0] != null))
            {
                FileStream fs = new FileStream(fullPath[0], FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fs, System.Text.Encoding.UTF8);
                string json = streamReader.ReadToEnd();
                fs.Close();
                streamReader.Close();
                StandardFilter filter = Newtonsoft.Json.JsonConvert.DeserializeObject<StandardFilter>(json);
                return filter;
            }
            else
            {
                return null;
            }
        }
    }
}

