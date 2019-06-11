using iS3.ImportTools.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Models;

namespace iS3.ImportTools.DataStanardTool.DSExporter
{
    public class Exporter_For_XML : IDSExporter
    {
        public bool Export(PmEntiretyDef dataStandard, string path)
        {
            return true;
        }

        public bool Export(PmDomainDef domain, string path = null)
        {
            throw new NotImplementedException();
        }
    }
}
