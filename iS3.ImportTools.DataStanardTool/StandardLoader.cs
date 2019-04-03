using System;
using iS3.ImportTools.Core.Models;
using System.IO;
using iS3.ImportTools.DataStanardTool.DSImporter;

namespace iS3.ImportTools.DataStanardTool
{
    public class StandardLoader
    {
        string path { get; set; }
        public StandardLoader()
        {
            this.path = AppDomain.CurrentDomain.BaseDirectory;

        }
        public DataStandardDef Loadfile()
        {
            var dlls = Directory.GetFiles(path, "*.dll");
            if (!dlls.Equals(null))
            {
                Importer_For_Dll importer = new Importer_For_Dll();
                return importer.Import(dlls[0]);
            }
            var jsons = Directory.GetFiles(path, "*.json");
            if (!jsons.Equals(null))
            {
                Importer_For_Json importer = new Importer_For_Json();
                return importer.Import(jsons[0]);
            }
            var xmls = Directory.GetFiles(path, "*.xml");
            if (!xmls.Equals(null))
            {
                Importer_For_Xml importer = new Importer_For_Xml();
                return importer.Import(xmls[0]);
            }
            return null;


        }
    }
}

