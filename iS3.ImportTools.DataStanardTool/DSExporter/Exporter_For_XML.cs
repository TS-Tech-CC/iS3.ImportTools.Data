﻿using iS3.ImportTools.Core.Interface;
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
        public bool Exporter(DataStandardDef dataStandard, string path)
        {
            return true;
        }
    }
}
