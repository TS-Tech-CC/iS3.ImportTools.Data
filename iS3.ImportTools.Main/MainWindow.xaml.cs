using iS3.ImportTools.Core.Models;
using iS3.ImportTools.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Data;
using System.Timers;

namespace iS3.ImportTools.Main
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<DataSchema> DataSchemaOBC = new ObservableCollection<DataSchema>();
        private List<DataSchemaConfig> DataSchemaConfigList = new List<DataSchemaConfig>();

        
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            lbDataSchemaState.ItemsSource = DataSchemaOBC;

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DllImport.LoadExtension();

            //SQL
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
            DataSchemaConfigList.Add(config1);


            ////TCP
            //DataSchemaConfig config2 = new DataSchemaConfig()
            //{
            //    DataAcquireDllName = "DataAcquire_TCP",
            //    DataFormatConverterDllName = "DataFormatConverter_A",
            //    DataPropertyMappingDllName = "DataPropertyMapping_A",
            //    DataVerificationDllName = "DataVerification_A",
            //    DataWritingDllName = "DataWriting_SingleTable"
            //};
            //DataSchemaConfigList.Add(config2);

            ////NetMQ
            //DataSchemaConfig config3 = new DataSchemaConfig()
            //{
            //    DataAcquireDllName = "DataAcquire_NetMQ",
            //    DataFormatConverterDllName = "DataFormatConverter_A",
            //    DataPropertyMappingDllName = "DataPropertyMapping_A",
            //    DataVerificationDllName = "DataVerification_A",
            //    DataWritingDllName = "DataWriting_SingleTable"
            //};
            //DataSchemaConfigList.Add(config3);


            ////SignalR
            //DataSchemaConfig config4 = new DataSchemaConfig()
            //{
            //    DataAcquireDllName = "DataAcquire_SignalR",
            //    DataFormatConverterDllName = "DataFormatConverter_A",
            //    DataPropertyMappingDllName = "DataPropertyMapping_A",
            //    DataVerificationDllName = "DataVerification_A",
            //    DataWritingDllName = "DataWriting_SingleTable"
            //};
            //DataSchemaConfigList.Add(config4);
        }




        private void SQLStart_Checked(object sender, RoutedEventArgs e)
        {
            foreach (DataSchemaConfig itemConfig in DataSchemaConfigList)
            {
                DataSchema dataSchema = new DataSchema(itemConfig);
                dataSchema.Start();
                DataSchemaOBC.Add(dataSchema);
            }

        }

        private void SQLStart_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataSchema itemDataSchema in DataSchemaOBC)
            {
                itemDataSchema.Stop();
            }
            DataSchemaOBC.Clear();
        }



    }
}
