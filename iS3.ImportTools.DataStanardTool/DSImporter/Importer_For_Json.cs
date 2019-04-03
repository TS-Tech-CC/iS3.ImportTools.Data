using iS3.ImportTools.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Models;

namespace iS3.ImportTools.DataStanardTool.DSImporter
{
    public class Importer_For_Json : IDSImporter
    {
        public DataStandardDef Import(string path)
        {
            return GetSample();
        }
        public DataStandardDef GetSample()
        {
            //定义隧道数据标准和地质域
            DataStandardDef dsDef = new DataStandardDef()
            {
                Code = "TunnelStandard",
                Description = "This a Tunnel DataStandard",
            };
            DomainDef ddDef = new DomainDef()
            {
                Code = "Geology",
                Desciption = "This a Geology Domain",
            };
            dsDef.DomainContainer.Add(ddDef);

            //定义地质域内的数据结构
            DGObjectDef dgDef = new DGObjectDef()
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
    }
}
