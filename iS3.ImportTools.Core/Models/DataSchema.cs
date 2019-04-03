using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Interface;

namespace iS3.ImportTools.Core.Models
{
    //*
    //  数据方案
    //
    public class DataSchema
    {
        public IDataFormatConverter dataFormatConverter { get; set; }

        public IPropertyMapping dataPropertyMapping { get; set; }

        public IDataVerification dataVerification { get; set; }
    }
}
