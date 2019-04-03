using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;

namespace iS3.ImportTools.DataFormatConverter
{
    public class DataFormatConvert_For_TXT : IDataFormatConverter
    {
        public CommonDataFormat Convert(string rawData)
        {
            return new CommonDataFormat();
        }
    }
}
