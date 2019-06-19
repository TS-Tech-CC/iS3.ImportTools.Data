using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace DataEntryService.WebAPI
{
    public class WebServer
    {
        static IDisposable server = null;

        public static void Start()
        {
            try
            {
                StartOptions opt = new StartOptions();
                opt.Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("Port"));
                string baseAddress = string.Format("http://{0}:{1}/", System.Configuration.ConfigurationManager.AppSettings.Get("ip"), System.Configuration.ConfigurationManager.AppSettings.Get("Port"));

                //opt.Port = int.Parse("8050");
                //string baseAddress = "http://*:8050/";
                server = WebApp.Start<Startup>(url: baseAddress);

                iS3.ImportTools.Core.Log.LogWrite.log.Info($"host 已启动：{baseAddress}");
            }
            catch (Exception err)
            {
                iS3.ImportTools.Core.Log.LogWrite.log.Error(err.Message);
            }
        }
    }
}
