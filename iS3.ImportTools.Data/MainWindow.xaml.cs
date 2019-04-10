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
using iS3.ImportTools.DataStanardTool.DSImporter;
using iS3.ImportTools.DataFormatConverter;

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
            PmEntiretyDef def = new Importer_For_Json().GetSample();
            PmDGObjectDef aimDGObjectDef = def.getDGObjectDefByCode(aimDGObjectType);


            string dsad = aimDGObjectDef.GetLngStr(LngType.zh);



            //step2 : create a instance of dataSchema for data importer
            DataSchema dataSchema = new DataSchema();
            dataSchema.dataFormatConverter = new DataFormatConvert_ForFS();
            dataSchema.dataPropertyMapping = new DataPropertyMapping_ForFS();
            dataSchema.dataVerification = new DataVerification_ForFS();



            //stepX : 从数据源按批次获取数据
            string strData = "133,15,2019-04-09 10:00:00,32,64,128,256";



            //step3 : choose the type of data source and convert to the commonDataFormat
            DataCarrier srcDC = dataSchema.dataFormatConverter.Convert(strData);



            //setp4 : choose the mapping rule between the commonDataFormat and aimDGObjectDef
            DataCarrier aimDC = dataSchema.dataPropertyMapping.Mapping(srcDC);



            //step5 : verify the data
            DataCarrier chkDC = dataSchema.dataVerification.Verification(aimDC);



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
