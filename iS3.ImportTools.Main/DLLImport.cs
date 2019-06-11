using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace iS3.ImportTools.Main
{
    public class DllImport
    {
        //public static Dictionary<string, Extensions> domainExtension = new Dictionary<string, Extensions>();
        //public static Dictionary<string, Assembly> assemblyDict = new Dictionary<string, Assembly>();
        //static List<Assembly> _loadedExtensions = new List<Assembly>();

        public static void LoadExtension()
        {
            Assembly[] allAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            string exeLocation = Directory.GetCurrentDirectory();
            string WareHousePath = exeLocation + "\\..\\..\\..\\WareHouse";
            if (!Directory.Exists(WareHousePath))
            {
                System.Windows.MessageBox.Show("WareHouse文件夹不存在！！！");
                return;
            }


            foreach (string eachProcedure in Directory.GetDirectories(WareHousePath))
            {
                var dlls = Directory.EnumerateFiles(eachProcedure, "*.dll", SearchOption.TopDirectoryOnly);
                foreach (string dllPath in dlls)
                {
                    Assembly.LoadFrom(dllPath);
                }
            }

            Assembly[] allAssemblies0 = AppDomain.CurrentDomain.GetAssemblies();

        }
    }
}
