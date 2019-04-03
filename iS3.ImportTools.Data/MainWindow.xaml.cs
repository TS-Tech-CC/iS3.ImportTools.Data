using iS3.ImportTools.Core.Models;
using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.DataStanardTool;
using iS3.ImportTools.DataStanardTool.DSExporter;
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
using iS3.ImportTools.DataPropertyMapping;
using iS3.ImportTools.DataVerification;

namespace iS3.ImportTools.Main
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //step1 : find the aim object def
            string aimDGObjectType = "Borehole";
            StandardLoader loader = new StandardLoader();//get standard from local json file in debug catagory
            //IDSImporter idsImporter = null;
            //DataStandardDef def = idsImporter.Import(null);
            DataStandardDef standard = loader.getStandard();
            IDSExporter exporter = new Exporter_For_JSON();
            exporter.Export(standard,AppDomain.CurrentDomain.BaseDirectory);//输出

            DGObjectDef aimDGObjectDef = standard.getDGObjectDefByCode(aimDGObjectType);

            //step2 : create a instance of dataSchema for data importer
            DataSchema dataSchema = new DataSchema();

            //step3 : choose the type of data source and convert to the commonDataFormat
            dataSchema.dataFormatConverter = null;
            CommonDataFormat rawCDF = dataSchema.dataFormatConverter.Convert(null);

            //setp4 : choose the mapping rule between the commonDataFormat and aimDGObjectDef
            dataSchema.dataPropertyMapping = new CommonPropertyMapping();
            CommonDataFormat mappingCDF = dataSchema.dataPropertyMapping.Mapping(rawCDF);

            //step5 : verify the data
            dataSchema.dataVerification = new CommonDataVerification();
            CommonDataFormat verificationCDF = dataSchema.dataVerification.Verification(mappingCDF);

            //step6 : set the data access frequency, such as one time access, access at a time, realtime access ,push access
            //-------

            //setp7 : save the data

            //step8 : exporter the data
        }

        private void MainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
