using iS3.ImportTools.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Interface
{
    //*
    // The Common Interface for DataStandard importer, such as import from dll,json, and so on
    //
    public interface IDSImporter
    {
        PmEntiretyDef Import(string path);
    }
}
