using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;
using iS3.ImportTools.DataStanardTool.DSExporter;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.DataStanardTool.DSImporter
{
    public class Importer_For_Exl : IDSImporter
    {
        PmEntiretyDef standardDef { get; set; }

        public PmEntiretyDef Import(string StandardName)
        {
            DirectoryInfo localPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string path = localPath.Parent.Parent.FullName + "\\Standard\\" + StandardName + ".xlsx";
            return ReadExl(ReadWorkbook(path));
        }

        public PmEntiretyDef ReadExl(IWorkbook workbook)
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory + @"Standard\";
            //DataStandardDef standardDef = new StandardLoader().getStandard(path);

            ISheet sheet = workbook.GetSheetAt(0);
            for (int i = 1; i < sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                Row2Object(row);
            }
            IDSExporter exporter = new Exporter_For_JSON();
            exporter.Export(this.standardDef);
            workbook.Close();
            return this.standardDef;
        }

        IWorkbook ReadWorkbook(string path)
        {
            try
            {
                this.standardDef = new PmEntiretyDef { Code = Path.GetFileNameWithoutExtension(path) };
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                if (path.IndexOf(".xlsx") > 0) // for excel version over 2007
                    return new XSSFWorkbook(fs);
                else if (path.IndexOf(".xls") > 0) //for excel version 97-03
                    return new HSSFWorkbook(fs);
            }
            catch (Exception e)
            {
                throw e;
                //System.Windows.MessageBox.Show(e.Message);
                return null;
            }
            return null;
        }
        public void Row2Object(IRow row)
        {

            try
            {
                string domainName = row.GetCell(0).ToString();
                string domainDes = row.GetCell(1)?.ToString();
                string domainlangStr = row.GetCell(2)?.ToString();
                string objectName = row.GetCell(3)?.ToString();
                string objDescrip = row.GetCell(4)?.ToString();
                string objLangStr = row.GetCell(5)?.ToString();
                string propertyName = row.GetCell(6).ToString();
                bool IsKey = row.GetCell(7)?.ToString() == null ? false : row.GetCell(7).ToString() == "TRUE";
                string dataType = row.GetCell(8)?.ToString().Replace(" ", "");
                bool Nullable = row.GetCell(9)?.ToString() == null ? true : row.GetCell(9).ToString() != "FALSE";
                string unit = row.GetCell(10)?.ToString();
                string regularExp = row.GetCell(11)?.ToString();
                string proLanStr = row.GetCell(12)?.ToString();
                string proDes = row.GetCell(13)?.ToString();


                PmDomainDef domain = null;
                PmDGObjectDef objectDef = null;
                PropertyMeta property = new PropertyMeta
                {
                    PropertyName = propertyName,
                    IsKey = IsKey,
                    Nullable = Nullable,
                    DataType = dataType,
                    Unit = unit,
                    Description = proDes,
                    RegularExp = regularExp,
                    LangStr = proLanStr
                };

                if (this.standardDef.DomainContainer.Exists(x => x.Code == domainName))
                {
                    domain = this.standardDef.DomainContainer.Find(x => x.Code == domainName);

                    if (domain.DGObjectContainer.Exists(x => x.Code == objectName))
                    {
                        objectDef = domain.DGObjectContainer.Find(x => x.Code == objectName);
                        if (!objectDef.PropertyContainer.Exists(x => x.PropertyName == property.PropertyName))
                            objectDef.PropertyContainer.Add(property);
                    }
                    else
                    {
                        objectDef = new PmDGObjectDef
                        {
                            Code = objectName,
                            Desctiption = objDescrip,
                            LangStr = objLangStr
                        };
                        objectDef.PropertyContainer.Add(property);
                        domain.DGObjectContainer.Add(objectDef);
                    }
                }
                else
                {
                    domain = new PmDomainDef
                    {
                        Code = domainName,
                        Desciption = domainDes,
                        LangStr = domainlangStr
                    };
                    objectDef = new PmDGObjectDef
                    {
                        Code = objectName,
                        Desctiption = objDescrip,
                        LangStr = objLangStr
                    };
                    objectDef.PropertyContainer.Add(property);
                    domain.DGObjectContainer.Add(objectDef);
                    this.standardDef.DomainContainer.Add(domain);
                }
            }
            catch (Exception e)
            {
                throw e;
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
    }
}
