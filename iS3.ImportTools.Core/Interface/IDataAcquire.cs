using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Interface
{
    public interface IDataAcquire
    {
        void Start(Action<object> DataProcessHandler);
        void Stop();
    }
}
