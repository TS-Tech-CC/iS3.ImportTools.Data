using iS3.ImportTools.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Interface
{
    public interface IDataImporter
    {
        List<System.Data.DataSet> Import(PmEntiretyDef standard);
        System.Data.DataSet Import(string path, PmEntiretyDef standard);
    }
}
