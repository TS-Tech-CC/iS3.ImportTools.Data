using iS3.ImportTools.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Interface
{
    //*
    // The interface of DataFormat Converter for plenty of raw data source, such as SQL,EXCEL...
    // The result of converter should be both data meta desc and data instance, using the CommonDataFormat;
    //
    public interface IDataFormatConverter
    {
        // data source may be different,
        // take string data for example
        DataCarrier Convert(object rawData);
    }
}
