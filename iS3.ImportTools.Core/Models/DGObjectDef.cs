using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
  public  class DGObjectDef:LangBase
    {
        public string Code { get; set; }
        public string Desctiption { get; set; }
        public List<PropertyMeta> PropertyContainer { get; set; }
        public DGObjectDef()
        {
            PropertyContainer = new List<PropertyMeta>();
        }
    }
}
