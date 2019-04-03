using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
    public class DataStandardDef:LangBase
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public List<DomainDef> DomainContainer { get; set; }
        public DataStandardDef()
        {
            DomainContainer = new List<DomainDef>();
        }
    }
}
