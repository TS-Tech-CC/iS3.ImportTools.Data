using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Log;
using Newtonsoft.Json;

namespace iS3.ImportTools.Core.Models
{
    //*
    //  数据方案
    //
    public class DataSchema : INotifyPropertyChanged
    {
        #region 界面显示
        public event PropertyChangedEventHandler PropertyChanged;

        private string _state;
        public string state
        {
            get { return _state; }
            set
            {
                _state = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("state"));
            }
        }

        

        private DataView _chkDV;
        public DataView chkDV
        {
            get { return _chkDV; }
            set
            {

                _chkDV = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("chkDV"));
            }
        }
        #endregion




        private DataSchemaConfig _Config;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config">数据处理方案配置类</param>
        public DataSchema(DataSchemaConfig config)
        {
            _Config = config;
            state = config.DataAcquireDllName;
            Assembly[] AllAssembly = AppDomain.CurrentDomain.GetAssemblies();
            

            Assembly ASDataAcq = AllAssembly.FirstOrDefault(x => x.FullName.Contains(config.DataAcquireDllName));           //查找指定程序集（指定DLL）
            if (ASDataAcq == null)
                LogWrite.log.Error($"Assembly中不存在名称为：{config.DataAcquireDllName}的程序集");
            Type TYDataAcq = ASDataAcq.GetTypes().FirstOrDefault(x => x.FullName.Contains(config.DataAcquireDllName));      //在该DLL中查找指定类
            DataAcquire = (IDataAcquire)ASDataAcq.CreateInstance(TYDataAcq.FullName);                                       //从该DLL创建其中指定类的实例
            MethodStart = TYDataAcq.GetMethod("Start");                                                                     //从类中获取指定的方法
            MethodStop = TYDataAcq.GetMethod("Stop");


            Assembly ASDataCvt = AllAssembly.FirstOrDefault(x => x.FullName.Contains(config.DataFormatConverterDllName));
            Type TYDataCvt = ASDataCvt.GetTypes().FirstOrDefault(x => x.FullName.Contains(config.DataFormatConverterDllName));
            DataFormatConverter = (IDataFormatConverter)ASDataCvt.CreateInstance(TYDataCvt.FullName);
            MethodConvert = TYDataCvt.GetMethod("Convert");


            Assembly ASDataMap = AllAssembly.FirstOrDefault(x => x.FullName.Contains(config.DataPropertyMappingDllName));
            Type TYDataMap = ASDataMap.GetTypes().FirstOrDefault(x => x.FullName.Contains(config.DataPropertyMappingDllName));
            DataPropertyMapping = (IPropertyMapping)ASDataMap.CreateInstance(TYDataMap.FullName);
            MethodMapping = TYDataMap.GetMethod("Mapping");


            Assembly ASDataVfc = AllAssembly.FirstOrDefault(x => x.FullName.Contains(config.DataVerificationDllName));
            Type TYDataVfc = ASDataVfc.GetTypes().FirstOrDefault(x => x.FullName.Contains(config.DataVerificationDllName));
            DataVerification = (IDataVerification)ASDataVfc.CreateInstance(TYDataVfc.FullName);
            MethodVerification = TYDataVfc.GetMethod("Verification");


            Assembly ASDataWrt = AllAssembly.FirstOrDefault(x => x.FullName.Contains(config.DataWritingDllName));
            Type TYDataWrt = ASDataWrt.GetTypes().FirstOrDefault(x => x.FullName.Contains(config.DataWritingDllName));
            DataWriting = (IDataWriting)ASDataWrt.CreateInstance(TYDataWrt.FullName);
            MethodWriting = TYDataWrt.GetMethod("Writing");


        }

        

        public void Start()
        {
            try
            {
                MethodStart.Invoke(DataAcquire, new Action<object>[] { DataProcess });
            }
            catch (Exception err)
            {
                LogWrite.log.Error(err.Message + "  " + err.InnerException.Message);
            }
        }
        public void Stop()
        {
            MethodStop.Invoke(DataAcquire, new object[] { });
        }



        private IDataAcquire DataAcquire { get; set; }
        private MethodInfo MethodStart { get; set; }
        private MethodInfo MethodStop { get; set; }




        private IDataFormatConverter DataFormatConverter { get; set; }
        private MethodInfo MethodConvert { get; set; }



        private IPropertyMapping DataPropertyMapping { get; set; }
        private MethodInfo MethodMapping { get; set; }




        private IDataVerification DataVerification { get; set; }
        private MethodInfo MethodVerification { get; set; }



        private IDataWriting DataWriting { get; set; }
        private MethodInfo MethodWriting { get; set; }



        private void DataProcess(object RawData)
        {
            try
            {
                LogWrite.log.Info("成功读取到数据" + JsonConvert.SerializeObject(RawData as DataTable));

                DataCarrier srcDC = (DataCarrier)MethodConvert.Invoke(DataFormatConverter, new object[] { RawData });
                LogWrite.log.Info("成功转换为" + srcDC.DataContainer.Rows.Count + "条数据");

                DataCarrier aimDC = (DataCarrier)MethodMapping.Invoke(DataPropertyMapping, new object[] { srcDC });
                LogWrite.log.Info("成功映射为" + aimDC.DataContainer.Rows.Count + "条数据");


                DataCarrier chkDC = (DataCarrier)MethodVerification.Invoke(DataVerification, new object[] { aimDC });
                LogWrite.log.Info("成功校验");


                ////chkDV = chkDC.DataContainer.DefaultView;
                ////state = state.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[0] + ": " + DateTime.Now.ToString("HH:mm:ss.fff");

                LogWrite.log.Info("准备写入");
                MethodWriting.Invoke(DataWriting, new object[] { _Config.WritingConfig, chkDC });

                LogWrite.log.Info("写入成功");

            }
            catch (Exception err)
            {
                LogWrite.log.Error("数据处理流程报错：" + err.Message + "," + err.InnerException.Message);
            }

        }
    }
}
