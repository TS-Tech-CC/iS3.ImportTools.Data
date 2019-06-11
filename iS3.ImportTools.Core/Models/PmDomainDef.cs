using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
    /// <summary>
    /// Domain which refers to specific engineering field
    /// </summary>
    public class PmDomainDef:LangBase
    {
        public string Code { get; set; }

        public string Desciption { get; set; }
        public List<PmDGObjectDef> DGObjectContainer { get; set; }

        public PmDomainDef()
        {
            DGObjectContainer = new List<PmDGObjectDef>();
        }
    }
}
