using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
    public class PmEntiretyDef:LangBase
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public List<PmDomainDef> DomainContainer { get; set; }
        public PmEntiretyDef()
        {
            DomainContainer = new List<PmDomainDef>();
        }
        public PmDGObjectDef GetDGObjectDefByCode(string Code)
        {
            foreach (PmDomainDef domain in DomainContainer)
            {
                return domain.DGObjectContainer.Find(DGObject => DGObject.Code == Code);
            }
            return null;
        }
        public PmDGObjectDef GetDGObjectDefByName(string name)
        {
            foreach (var domain in DomainContainer)
            {
                return domain.DGObjectContainer.Find(DGObject => DGObject.LangStr == name);
            }
            return null;
        }
    }
}
