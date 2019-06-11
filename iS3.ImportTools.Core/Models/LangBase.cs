using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace iS3.ImportTools.Core.Models
{
    public class LangBase
    {
        public string LangStr
        {
            get; set;

        }


        public static string ConvertToLanStr(LangDict langdict)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(langdict);
        }
        public class LangDict : Dictionary<LangType, string>
        {
        }

    }

}
