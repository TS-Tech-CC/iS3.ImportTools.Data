using iS3.ImportTools.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Interface
{
    //*
    // The Common Interface for DataStandard exporter, such as exporter to dll,json, and so on
    //
    public interface IDSExporter
    {
        //export dataStandard to ...
        //return the state of export
        bool Export(PmEntiretyDef dataStandard, string path = null);
        bool Export(PmDomainDef domain, string path = null);
    }
}
