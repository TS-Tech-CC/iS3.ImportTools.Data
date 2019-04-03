using System;
using iS3.ImportTools.Core.Models;
using System.IO;
using iS3.ImportTools.DataStanardTool.DSImporter;
using iS3.ImportTools.Core.Interface;

namespace iS3.ImportTools.DataStanardTool
{
    public class StandardLoader
    {
        string path { get; set; }
        public StandardLoader()
        {
            this.path = AppDomain.CurrentDomain.BaseDirectory;

        }
        public DataStandardDef getStandard()
        {

            //!!!!poay attention to other file which will affect the import process
            IDSImporter importer = null;
            importer = new Importer_For_Json();//!!
            return importer.Import("");//!!
            var dlls = Directory.GetFiles(path, "Geology.dll");
            if (!dlls.Equals(null))
            {
               importer = new Importer_For_Dll();
               return importer.Import(dlls[0]);
            }
            var jsons = Directory.GetFiles(path, "*.json");
            if (!jsons.Equals(null))
            {
                importer = new Importer_For_Json();
                return importer.Import("");
            }
            var xmls = Directory.GetFiles(path, "*.xml");
            if (!xmls.Equals(null))
            {
                importer = new Importer_For_Xml();
                return importer.Import(xmls[0]);
            }
            return null;


        }
    }
}

