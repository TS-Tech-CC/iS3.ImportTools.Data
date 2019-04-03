using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Models;
using iS3.ImportTools.Core.Interface;

namespace iS3.ImportTools.DataStanardTool.DSExporter
{
    public class Exporter_For_DLL : IDSExporter
    {
        public bool Exporter(DataStandardDef dataStandard, string path)
        {
            return true;
        }
    }
}
