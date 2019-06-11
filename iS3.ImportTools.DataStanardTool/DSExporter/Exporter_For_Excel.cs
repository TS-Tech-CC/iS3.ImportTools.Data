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

namespace iS3.ImportTools.DataStanardTool.DSExporter
{

    public class Exporter_Excel : IDataExporter
    {

        string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        string fileName;

        PmEntiretyDef standard;
        PmDomainDef domain;



        public bool Export(PmEntiretyDef standard, string path = null)
        {
            this.standard = standard;
            if (path == null)
            {
            }
            else
            {
                this.path = path;
            }
            bool succeed = Export();
            if (succeed)
            {
                //System.Windows.MessageBox.Show("The Exl templete generated successfully at Desktop!");
            }
            else
            {
                throw new Exception("Someting getting wrong during generating,Please try again!");
                //System.Windows.MessageBox.Show("Someting getting wrong during generating,Please try again!");
            }
            return succeed;
        }

        public bool Export(PmDomainDef domain, string path = null)
        {
            this.domain = domain;
            if (path != null)
            {
                this.path = path;
            }
            bool succeed = Export();
            if (succeed)
            {
                //System.Windows.MessageBox.Show("The Exl templete generated successfully at Desktop!");
            }
            else
            {
                throw new Exception("Someting getting wrong during generating,Please try again!");
                //System.Windows.MessageBox.Show("Someting getting wrong during generating,Please try again!");
            }
            return succeed;
        }

        /// <summary>
        /// export standard to excel for data input
        /// </summary>
        /// <param name="path">the path where excel will generate to</param>
        /// <param name="standard"></param>
        /// <returns></returns>
        bool Export()
        {
            //default file format 'xls'
            IWorkbook workbook = new HSSFWorkbook();
            try
            {
                if (standard == null)
                {
                    fileName = path + "\\default.xls";
                    write2Exl(this.domain, workbook);
                }
                else
                {
                    fileName = path + "\\" + standard.Code + ".xls";
                    foreach (PmDomainDef domain in standard.DomainContainer)
                    {
                        write2Exl(domain, workbook);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            saveExl(workbook);
            return true;

        }


        bool write2Exl(PmDomainDef domain, IWorkbook workbook)
        {
            foreach (PmDGObjectDef item in domain.DGObjectContainer)
            {
                ISheet sheet = workbook.CreateSheet(item.Code);
                writeDescription(sheet, item);
                wrtieTitle(sheet, item);
            }
            return true;
        }

        void writeDescription(ISheet sheet, PmDGObjectDef item)
        {
            IRow row0 = sheet.CreateRow(0);
            row0.CreateCell(0).SetCellValue(item.Code + "表");
            row0.CreateCell(1).SetCellValue(item.Desctiption);
            row0.CreateCell(3).SetCellValue("请勿修改sheet名");

            for (int i = 0; i < 20; i++)
            {
                sheet.SetColumnWidth(i, 20 * 175);
            }
        }

        void wrtieTitle(ISheet sheet, PmDGObjectDef item)
        {
            IRow row1 = sheet.CreateRow(1);
            int i = 0;
            foreach (PropertyMeta property in item.PropertyContainer)
            {

                row1.CreateCell(i++).SetCellValue(property.LangStr + property.DataType);
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
