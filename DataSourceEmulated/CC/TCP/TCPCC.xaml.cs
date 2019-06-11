using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace DataSourceEmulated.CC
{
    /// <summary>
    /// TCPCC.xaml 的交互逻辑
    /// </summary>
    public partial class TCPCC : UserControl
    {
        TcpListener tcpListener;
        List<TcpClient> obcClient = new List<TcpClient>();
        ObservableCollection<string> obcContent = new ObservableCollection<string>();

        public TCPCC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cbIP.ItemsSource = Dns.GetHostAddresses(Dns.GetHostName()).Concat(new IPAddress[] { new IPAddress(256 * 256 * 256 + 127) });
            cbIP.SelectedIndex = cbIP.Items.Count - 1;
            lbTCPContent.ItemsSource = obcContent;
        }

        private void BtnTCP_Checked(object sender, RoutedEventArgs e)
        {
            tcpListener = new TcpListener(cbIP.SelectedValue as IPAddress, Convert.ToInt32(tbPort.Text));
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(AcceptTCPL, tcpListener);
        }


        private void AcceptTCPL(IAsyncResult ar)
        {
            TcpListener lstn = (TcpListener)ar.AsyncState;
            TcpClient client;
            try
            {
                client = lstn.EndAcceptTcpClient(ar);
            }
            catch (ObjectDisposedException err)
            {
                return;
            }

            this.Dispatcher.Invoke(new Action(() =>
            {
                obcClient.Add(client);
                obcContent.Add("");
                lbTCPClient.ItemsSource = obcClient.Select(x => x.Client.RemoteEndPoint);
            }));


            Task.Run(() =>
            {

                NetworkStream stream = client.GetStream();
                string port = client.Client.RemoteEndPoint.ToString().Split(new char[] { ':' })[1];


                while (true)
                {
                    string strSend = $"133,{port},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},32,64,128,256";
                    byte[] byteSend = Encoding.Default.GetBytes(strSend);

                    try
                    {
                        stream.WriteAsync(byteSend, 0, byteSend.Count());
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            obcContent[obcClient.IndexOf(client)] = strSend;
                        }));

                        Thread.Sleep(1000);

                    }
                    catch (System.IO.IOException err)
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            obcContent.RemoveAt(obcClient.IndexOf(client));
                            obcClient.Remove(client);
                            lbTCPClient.ItemsSource = obcClient.Select(x => x.Client.RemoteEndPoint);
                        }));

                        break;
                    }
                    catch (ObjectDisposedException err)
                    {
                        break;
                    }


                }
            });

            lstn.BeginAcceptTcpClient(AcceptTCPL, lstn);

        }

        private void BtnTCP_Unchecked(object sender, RoutedEventArgs e)
        {
            tcpListener.Stop();

            foreach (TcpClient item in obcClient)
            {
                item.Close();
            }
            obcClient.Clear();
            lbTCPClient.ItemsSource = obcClient.Select(x => x.Client.RemoteEndPoint);

            obcContent.Clear();

        }
    }
}
