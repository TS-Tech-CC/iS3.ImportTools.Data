using System;
using System.IO;
using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;
using Newtonsoft.Json;
using System.Text;

namespace iS3.ImportTools.DataStanardTool.DSImporter
{
    public class Importer_For_Json : IDSImporter
    {
        public PmEntiretyDef Import(string path)
        {
            path = path ?? (AppDomain.CurrentDomain.BaseDirectory + @"Standard\Geology.json");
            return ReadJson(path);
            //return GetSample();
        }
        /// <summary>
        /// just for test
        /// </summary>
        /// <returns></returns>
        public PmEntiretyDef GetSample()
        {
            //定义隧道数据标准和地质域
            PmEntiretyDef dsDef = new PmEntiretyDef()
            {
                Code = "TunnelStandard",
                Description = "This a Tunnel DataStandard",
            };
            PmDomainDef ddDef = new PmDomainDef()
            {
                Code = "Geology",
                Desciption = "This a Geology Domain",
            };
            dsDef.DomainContainer.Add(ddDef);

            //定义地质域内的数据结构
            PmDGObjectDef dgDef = new PmDGObjectDef()
            {
                Code = "Borehole",
                Desctiption = "This a Borehole DGObject"
            };
            ddDef.DGObjectContainer.Add(dgDef);

            //定义钻孔中的属性内容
            dgDef.PropertyContainer.Add(new PropertyMeta("ID",              "int",      null,   "这是编号字段", "['zh':'编号','en':'ID']",                  true,     false,    regularExpression: @"\d"));
            dgDef.PropertyContainer.Add(new PropertyMeta("BoreholeID",      "string",   null,   "这是钻孔编号", "['zh':'钻孔编号','en':'BoreholeID']",      false,    true,   regularExpression: @""));
            dgDef.PropertyContainer.Add(new PropertyMeta("BoreholeTime",    "dateTime", null,   "这是钻孔时间", "['zh':'钻孔时间','en':'BoreholeTime']",    false,    true));
            dgDef.PropertyContainer.Add(new PropertyMeta("BoreholeDepth",   "double",   "m",    "这是钻孔深度", "['zh':'钻孔深度','en':'BoreholeDepth']",   false,    true));
            return dsDef;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public PmEntiretyDef ReadJson(string path)
        {
            if (path != null)
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fs, Encoding.UTF8);
                string json = streamReader.ReadToEnd();
                fs.Close();
                streamReader.Close();
                PmEntiretyDef standard = JsonConvert.DeserializeObject<PmEntiretyDef>(json);
                return standard;
            }
            else
            {
                return null;
            }
        }

    }
}
