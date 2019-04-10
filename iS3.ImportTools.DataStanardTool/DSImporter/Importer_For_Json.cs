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
            return readJson(path);
            //!!return GetSample();
        }
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
            dgDef.PropertyContainer.Add(new PropertyMeta("ID", "Int", null, "这是编号字段", "['zh':'编号','en':'ID']", true));
            dgDef.PropertyContainer.Add(new PropertyMeta("BoreholeID", "string", null, "这是钻孔编号", "['zh':'钻孔编号','en':'BoreholeID']", true));
            dgDef.PropertyContainer.Add(new PropertyMeta("BoreholeTime", "dateTime", null, "这是钻孔时间", "['zh':'钻孔时间','en':'BoreholeTime']", true));
            dgDef.PropertyContainer.Add(new PropertyMeta("BoreholeDepth", "double", "m", "这是钻孔深度", "['zh':'钻孔深度','en':'BoreholeDepth']", true));
            return dsDef;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public PmEntiretyDef readJson(string path)
        {
            
            var fullPath = Directory.GetFiles(path, "*.txt");

            if (!(fullPath[0] == null))
            {
                FileStream fs = new FileStream(fullPath[0], FileMode.Open, FileAccess.Read);
                int n = (int)fs.Length;
                byte[] b = new byte[n];
                int r = fs.Read(b, 0, n);
                string json = Encoding.Default.GetString(b, 0, n);
                PmEntiretyDef standard = JsonConvert.DeserializeObject<PmEntiretyDef>(json);
                return standard;
            }
            else
            {
                //
                if (true)
                {
                    int a, b;
                    a = 1;
                    b = 1;
                    a = b;
                    return null;
                }
            }
        }
    }
}
