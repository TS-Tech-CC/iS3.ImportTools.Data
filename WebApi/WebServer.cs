using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public class WebServer
    {
        static IDisposable server = null;

        public static void Start()
        {
            StartOptions opt = new StartOptions();
            //opt.Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("Port"));
            //string baseAddress = string.Format("http://{0}:{1}/", System.Configuration.ConfigurationManager.AppSettings.Get("ip"), System.Configuration.ConfigurationManager.AppSettings.Get("Port"));

            opt.Port = int.Parse("8050");
            string baseAddress = "http://*:8050/";
            server = WebApp.Start<Startup>(url: baseAddress);
            //Console.WriteLine(String.Format("host 已启动：{0}", baseAddress));
        }
    }
}
