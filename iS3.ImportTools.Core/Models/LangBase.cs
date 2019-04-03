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
        //* 
        // the language string of this Domain display name
        // ["en":"Borehole","chs":"钻孔"]
        public string LangStr { get; set; }

        public static string ConvertToLanStr(LangDict langDict)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(langDict);
        }
    }
    public class LangDict : Dictionary<LangType, string> { }
}
