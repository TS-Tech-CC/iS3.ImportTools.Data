using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.DataManagerFolder
{
    /// <summary>
    /// generate exl templete for data input
    /// </summary>
    class TempleteExporter_Excel : IDSExporter
    {

        string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        string fileName;

        PmEntiretyDef standard;
        PmDomainDef domain;


        /// <summary>
        /// export standard to excel for data input
        /// </summary>
        /// <param name="standard"></param>
        /// <param name="path">the path where excel will generate at</param>
        /// <returns></returns>
        public bool Export(PmEntiretyDef standard, string path = null)
        {
            this.standard = standard;
            this.path = path ?? this.path;
            try
            {
                foreach (PmDomainDef domain in standard.DomainContainer)
                {
                    this.domain = domain;
                    Export();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        public bool Export(PmDomainDef domain, string path = null)
        {
            this.domain = domain;
            this.path = path ?? this.path;

            return Export();
        }

        private bool Export()
        {
            try
            {
                IWorkbook workbook = new HSSFWorkbook();
                fileName = path + "\\" + domain.Code + ".xls";
                write2Exl(domain, workbook);
                saveExl(workbook);
            }
            catch (Exception e
            )
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        bool write2Exl(PmDomainDef domain, IWorkbook workbook)
        {
            foreach (PmDGObjectDef objectDef in domain.DGObjectContainer)
            {
                string sheetName = objectDef.LangStr ?? objectDef.Code;
                ISheet sheet = workbook.CreateSheet(sheetName);
                writeDescription(sheet, objectDef);
                wrtieTitle(sheet, objectDef);
            }
            return true;
        }

        void writeDescription(ISheet sheet, PmDGObjectDef objectDef)
        {
            IRow row0 = sheet.CreateRow(0);
            row0.CreateCell(0).SetCellValue(objectDef.Code + "表");
            row0.CreateCell(1).SetCellValue(objectDef.LangStr);
            row0.CreateCell(3).SetCellValue("请勿修改sheet名");

            for (int i = 0; i < 20; i++)
            {
                sheet.SetColumnWidth(i, 20 * 150);
            }
        }

        void wrtieTitle(ISheet sheet, PmDGObjectDef item)
        {
            IRow row1 = sheet.CreateRow(1);
            IRow row2 = sheet.CreateRow(2);
            int i = 0;
            foreach (PropertyMeta property in item.PropertyContainer)
            {
                row1.CreateCell(i).SetCellValue(property.LangStr);
                row2.CreateCell(i++).SetCellValue(property.DataType);
            }

        }

        void saveExl(IWorkbook workbook)
        {
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Close();
        }
    }
}
