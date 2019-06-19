using DataEntryService.WebAPI;
using iS3.ImportTools.Core.Log;
using iS3.ImportTools.Core.Models;
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

            WebServer.Start();
            iS3.ImportTools.Core.Log.LogWrite.log.Info("服务已开启");

            DllImport.LoadExtension();
        }


        static DataSchema dataSchema;
        public static bool StartOneThread()
        {
            try
            {
                DataSchemaConfig config1 = new DataSchemaConfig()
                {
                    DataAcquireDllName = "DataAcquire_SQL",
                    DataFormatConverterDllName = "DataFormatConverter_DataTable",
                    DataPropertyMappingDllName = "DataPropertyMapping_A",
                    DataVerificationDllName = "DataVerification_A",
                    DataWritingDllName = "DataWriting_MultiTableCount",
                    WritingConfig = new DataSchemaWritingConfig()
                    {
                        IP = "127.0.0.1",
                        DBName = "EmulatedDS",
                        UserID = "sa",
                        Pwd = "123456",
                        TableName = "DataDestination",

                        TimeSpan = 60,
                        TimeFieldName = "BoreholeTime",


                        NumberCount = 16,
                    }

                };

                dataSchema = new DataSchema(config1);
                LogWrite.log.Info("DataSchema初始化成功");

                dataSchema.Start();

                LogWrite.log.Info("线程成功开启");
                return true;

            }
            catch (Exception err)
            {

                iS3.ImportTools.Core.Log.LogWrite.log.Error(err.Message);
                return false;
            }


        }



        public static bool StopTheThread()
        {
            dataSchema.Stop();

            return true;
        }


        protected override void OnStop()
        {

        }
    }
}
