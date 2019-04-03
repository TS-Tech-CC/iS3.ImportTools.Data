using iS3.ImportTools.Core.Models;
using iS3.ImportTools.DataStanardTool;
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

namespace iS3.ImportTools.Data
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StandardLoader loader = new StandardLoader();
            DataStandardDef standard = loader.Loadfile();
        }

        private void MainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
