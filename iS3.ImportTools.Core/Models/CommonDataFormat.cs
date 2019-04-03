using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
    public class CommonDataFormat
    {
        public DGObjectDef DataDef { get; set; }
        public DataTable DataContainer { get; set; }
    }
}
