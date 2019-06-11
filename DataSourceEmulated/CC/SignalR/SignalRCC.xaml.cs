using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Hosting;
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
using System.Windows.Threading;

namespace DataSourceEmulated.CC
{
    /// <summary>
    /// SignalRCC.xaml 的交互逻辑
    /// </summary>
    public partial class SignalRCC : UserControl
    {

        IHubProxy hubProxy;
        DispatcherTimer SignalRDTimer = new DispatcherTimer();


        public SignalRCC()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            WebApp.Start("http://127.0.0.1:55138/signalr");


            HubConnection hubConn = new HubConnection("http://127.0.0.1:55138/signalr");
            hubProxy = hubConn.CreateHubProxy("SignalRHub");
            hubProxy.On("sendMessage", (x) =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    lbSignalRContent.Content = x;
                }));
            });
            await hubConn.Start();
            SignalRDTimer.Interval = TimeSpan.FromSeconds(1);
            SignalRDTimer.Tick += SignalRDTimer_Tick;
        }




        private void BtnSignalR_Checked(object sender, RoutedEventArgs e)
        {
            SignalRDTimer.Start();
        }

        private void BtnSignalR_Unchecked(object sender, RoutedEventArgs e)
        {
            SignalRDTimer.Stop();
        }


        private async void SignalRDTimer_Tick(object sender, EventArgs e)
        {
            var msg = $"133,65,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},32,64,128,256";
            await hubProxy.Invoke("Send", msg);
        }
    }
}
