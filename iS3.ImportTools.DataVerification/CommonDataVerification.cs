using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;

namespace iS3.ImportTools.DataVerification
{
    public class CommonDataVerification : IDataVerification
    {
        public CommonDataFormat Verification(CommonDataFormat rawCDF)
        {
            return rawCDF;
        }
    }
}
