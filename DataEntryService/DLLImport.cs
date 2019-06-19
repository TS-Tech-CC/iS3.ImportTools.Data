using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace DataEntryService
{
    public class DllImport
    {

        public static void LoadExtension()
        {
            Assembly[] allAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
            string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
            string WareHousePath = assemblyDirPath + "\\..\\..\\..\\WareHouse";

            if (!Directory.Exists(WareHousePath))
            {
                iS3.ImportTools.Core.Log.LogWrite.log.Error($"WareHouse文件夹{WareHousePath}不存在！！！");
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
