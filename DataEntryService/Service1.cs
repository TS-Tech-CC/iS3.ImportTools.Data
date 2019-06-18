using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DataEntryService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        
        protected override void OnStart(string[] args)
        {
            FileStream fs = new FileStream(@"C:\Users\CHB\Desktop\新建文本文档.txt", FileMode.OpenOrCreate);
            byte[] data = Encoding.Default.GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\r\n");
            fs.Write(data, 0, data.Length);


            try
            {
                WebApi.WebServer.Start();
            }
            catch (Exception err)
            {
                data = Encoding.Default.GetBytes(err.Message);
                fs.Write(data, 0, data.Length);
            }

            fs.Close();
        }

        protected override void OnStop()
        {
            FileStream fs = new FileStream(@"C:\Users\CHB\Desktop\新建文本文档.txt", FileMode.Append);
            byte[] data = Encoding.Default.GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            fs.Write(data, 0, data.Length);
            fs.Close();
        }
    }
}
