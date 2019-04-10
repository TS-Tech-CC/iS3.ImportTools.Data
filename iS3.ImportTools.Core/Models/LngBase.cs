using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace iS3.ImportTools.Core.Models
{
    public class LngBase
    {
        // {"en":"Borehole","zh":"钻孔"}
        public string LngStr;


        public void SetLngStr(LngDic lngDict)
        {
            LngStr = Newtonsoft.Json.JsonConvert.SerializeObject(lngDict);
        }

        public string GetLngStr(LngType lngType)
        {
            if (LngStr != null)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<LngDic>(LngStr)[lngType];
            else
                return "不支持的语言";
        }

    }

    public class LngDic : Dictionary<LngType, string>
    {


    }
}
