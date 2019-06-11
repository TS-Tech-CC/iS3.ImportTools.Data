using iS3.ImportTools.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Interface
{
    /// <summary>
    /// generate a templete for data input,default path is desktop
    /// </summary>
    public interface IDataExporter
    {
        /// <summary>
        /// generate a templete for data input
        /// </summary>
        /// <param name="path"></param>
        /// <returns>return exprot result:Success of fail</returns>
        bool Export(PmEntiretyDef standard, string path = null);
        bool Export(PmDomainDef domain, string path = null);
    }
}
