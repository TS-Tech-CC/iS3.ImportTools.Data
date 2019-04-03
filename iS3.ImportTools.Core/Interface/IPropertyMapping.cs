using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Models;
namespace iS3.ImportTools.Core.Interface
{
    //*
    // the mapping interface for property mapping config
    public interface IPropertyMapping
    {
        Dictionary<string,string> MappingRule { get; set; }
        CommonDataFormat Mapping(CommonDataFormat rawCDF);
    }
}
