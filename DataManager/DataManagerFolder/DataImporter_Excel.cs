using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.DataManagerFolder
{
    public class DataImporter_Excel : IDataImporter
    {


        public List<DataSet> Import(PmEntiretyDef standard)
        {
            List<DataSet> domainContainer = null;
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true
            };
            if (ofd.ShowDialog() == true)
            {
                foreach (string path in ofd.FileNames)
                {
                    domainContainer.Add(Import(path, standard));
                }
            }

            return domainContainer;
        }

        public DataSet Import(string path, PmEntiretyDef standard)
        {

            try
            {
                string domainName = Path.GetFileNameWithoutExtension(path);
                PmDomainDef domain = standard.DomainContainer.Find(x => x.Code == domainName);
                DataSet ds = new DataSet(domainName);

                IWorkbook wb = ReadWorkbook(path);

                List<string> sheetNames = GetSheetNames(wb);
                //sheetNames equal to objectName
                foreach (string sheetName in sheetNames)
                {
                    PmDGObjectDef objectDef = domain.DGObjectContainer.Find(x => x.LangStr == sheetName);
                    DataTable dt = ReadSheet(wb.GetSheet(sheetName), objectDef);
                    if (dt != null) ds.Tables.Add(dt);
                }
                return ds;
            }

            catch (Exception)
            {
                return null;
            }

        }


        IWorkbook ReadWorkbook(string path)
        {
            IWorkbook workbook = null;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                if (path.IndexOf(".xlsx") > 0) // for excel version over 2007
                    return workbook = new XSSFWorkbook(fs);
                else if (path.IndexOf(".xls") > 0) //for excel version 97-03
                    return workbook = new HSSFWorkbook(fs);
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        List<string> GetSheetNames(IWorkbook wb)
        {

            try
            {
                if (wb == null)
                {
                    return null;

                }
                else
                {
                    List<string> sheetNames = new List<string>();
                    for (int i = 0; i < wb.NumberOfSheets; i++)
                    {
                        sheetNames.Add(wb.GetSheetName(i));
                    }
                    return sheetNames;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// sheet to object datatable
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        DataTable ReadSheet(ISheet sheet, PmDGObjectDef objectDef)
        {

            if (sheet == null)
            {
                return null;
            }
            else
            {
                DataTable dt = new DataTable(objectDef.LangStr);
                if (sheet.PhysicalNumberOfRows < 4) return dt;
                foreach (PropertyMeta meta in objectDef.PropertyContainer)
                {
                    dt.Columns.Add(meta.LangStr);
                }

                foreach (IRow row in sheet)
                {
                    DataRow dr = dt.NewRow();
                    int i = 0;
                    foreach (PropertyMeta meta in objectDef.PropertyContainer)
                    {
                        if (row.GetCell(i) != null)
                        {
                            dr[meta.LangStr] = row.GetCell(i++);
                        }
                        else
                        {
                            i++;
                            dr[meta.LangStr] = null;
                        }
                    }
                    dt.Rows.Add(dr);
                }
                for (int i = 0; i < 3; i++)
                {
                    dt.Rows.RemoveAt(0);    //remove the decription line(first 3 linesS)
                }
                return dt;
            }
        }
    }
}
