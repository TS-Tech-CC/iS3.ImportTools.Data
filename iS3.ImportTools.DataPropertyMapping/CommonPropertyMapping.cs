using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;

namespace iS3.ImportTools.DataPropertyMapping
{
    public class CommonPropertyMapping : IPropertyMapping
    {
        public Dictionary<string, string> MappingRule { get; set; }

        public CommonDataFormat Mapping(CommonDataFormat rawCDF)
        {
            return rawCDF;
        }
        public CommonPropertyMapping()
        {
            MappingRule = new Dictionary<string, string>();
        }
    }
}
