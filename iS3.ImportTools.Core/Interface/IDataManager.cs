using iS3.ImportTools.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Interface
{
    public interface IDataBaseManager
    {
        void Data2DB(System.Data.DataSet dataSet, PmEntiretyDef standardDef);

    }
}
