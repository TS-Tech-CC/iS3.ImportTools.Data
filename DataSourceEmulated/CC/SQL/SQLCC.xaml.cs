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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DataSourceEmulated.CC
{
    /// <summary>
    /// SQLCC.xaml 的交互逻辑
    /// </summary>
    public partial class SQLCC : UserControl
    {

        DispatcherTimer SQLDTimer = new DispatcherTimer();


        public SQLCC()
        {
            InitializeComponent();

            SQLDTimer.Interval = TimeSpan.FromSeconds(1);
            SQLDTimer.Tick += SQLDTimer_Tick;
        }

        private void SQLDTimer_Tick(object sender, EventArgs e)
        {
            DataSourceSQLContext model = new DataSourceSQLContext(tbConnStr.Text);
            MyEntity myEntity = new MyEntity() { Id = 15, SensorID = 0194, DateTime = DateTime.Now, Value1 = 128, Value2 = 256, Value3 = 512, Value4 = 1024 };
            model.MyEntity.Add(myEntity);
            model.SaveChanges();

            lblSQLContent.Content = $"{myEntity.Id}\r\n{myEntity.SensorID}\r\n{ myEntity.DateTime}\r\n{myEntity.Value1}\r\n{myEntity.Value2}\r\n{myEntity.Value3}\r\n{myEntity.Value4}";
        }



        private void BtnSQL_Checked(object sender, RoutedEventArgs e)
        {
            SQLDTimer.Start();
        }

        private void BtnSQL_Unchecked(object sender, RoutedEventArgs e)
        {
            SQLDTimer.Stop();
        }
    }
}
