using NetMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace DataSourceEmulated.CC
{
    /// <summary>
    /// NetMQCC.xaml 的交互逻辑
    /// </summary>
    public partial class NetMQCC : UserControl
    {
        NetMQ.Sockets.PublisherSocket pubSocket;
        DispatcherTimer NetMQDTimer = new DispatcherTimer();

        public NetMQCC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cbNetMQIP.ItemsSource = Dns.GetHostAddresses(Dns.GetHostName()).Concat(new IPAddress[] { new IPAddress(256 * 256 * 256 + 127) });
            cbNetMQIP.SelectedIndex = cbNetMQIP.Items.Count - 1;
            NetMQDTimer.Interval = TimeSpan.FromSeconds(1);
            NetMQDTimer.Tick += NetMQDTimer_Tick;
        }

        private void BtnNetMQ_Checked(object sender, RoutedEventArgs e)
        {
            pubSocket = new NetMQ.Sockets.PublisherSocket();
            pubSocket.Bind($"tcp://{cbNetMQIP.SelectedValue.ToString()}:{tbNetMQPort.Text}");
            NetMQDTimer.Start();
        }

        private void NetMQDTimer_Tick(object sender, EventArgs e)
        {
            lbNetMQContent.Content = "";
            string[] SubscriberStr = new string[] { "A", "B", "C" };
            foreach (string item in SubscriberStr)
            {
                var msg = item + $"133,65,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},32,64,128,256";
                pubSocket.SendFrame(msg);
                lbNetMQContent.Content += "\r\n" + msg;
            }
        }

        private void BtnNetMQ_Unchecked(object sender, RoutedEventArgs e)
        {
            pubSocket.Close();
            NetMQDTimer.Stop();
        }
    }
}
