using iS3.ImportTools.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataManager.DataManagerFolder
{
    public class DataChecker
    {
        public DataSet dataSet { get; set; }
        public DataTable dataTable { get; set; }
        public PmEntiretyDef standardDef { get; set; }
        public DataChecker(DataTable table, PmEntiretyDef standard)
        {
            dataTable = table;
            standardDef = standard;
        }
        public DataChecker(DataSet set, PmEntiretyDef standard)
        {
            dataSet = set;
            standardDef = standard;

        }
        public bool Check()
        {
            try
            {
                if (dataSet != null)
                {
                    PmDomainDef domain = standardDef.DomainContainer.Find(x => x.Code == dataSet.DataSetName);
                    foreach (DataTable table in dataSet.Tables)
                    {
                        PmDGObjectDef objectDef = domain.DGObjectContainer.Find(x => x.LangStr == table.TableName);
                        CheckRows(table, objectDef);
                    }
                }
                else if (dataTable != null)
                {
                    PmDGObjectDef objectDef = standardDef.GetDGObjectDefByName(dataTable.TableName);
                    CheckRows(dataTable, objectDef);
                }
                return true;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }

        }

        private bool CheckRows(DataTable table, PmDGObjectDef objectDef)
        {
            DirectoryInfo localPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string path = localPath.Parent.Parent.FullName + @"\Data\" + "CheckResult.txt";
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            fs.Close();
            StreamWriter streamWriter = new StreamWriter(path: path, append: true);
            int line = 1;
            foreach (DataRow row in table.Rows)
            {
                int column = 1;
                foreach (PropertyMeta meta in objectDef.PropertyContainer)
                {
                    string time = "\t" + DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
                    if (meta.RegularExp != null)
                    {
                        if (row[meta.LangStr] != null & !Regex.IsMatch(row[meta.LangStr].ToString(), meta.RegularExp))
                        {
                            string message = "Data Format Error At sheet:" + objectDef.Code;
                            message += ",line:" + line++.ToString();
                            message += ",column:" + column++.ToString();
                            message += (@"\t" + time);
                            streamWriter.WriteLine(message);
                        }
                    }
                    else
                    {
                        //string message = "Lack of data check regulations at: " + objectDef.Code+"."+meta.PropertyName;
                        //message += time;
                        //streamWriter.WriteLine(message);
                    }
                }
            }
            streamWriter.Flush();
            streamWriter.Close();
            return true;
        }
    }
}
