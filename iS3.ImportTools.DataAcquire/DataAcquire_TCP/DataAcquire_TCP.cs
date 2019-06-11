using iS3.ImportTools.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iS3.ImportTools.DataAcquire
{
    public class DataAcquire_TCP : IDataAcquire
    {
        /*
        目前问题：
        1、TcpLisenter没有建立怎么办
        2、连接断开怎么办
         */


        private TcpClient tcpClient;
        private Action<object> DataProcessHandler;

        public void Start(Action<object> DataProcessHandler)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 60000);


            
            Thread threadRead = new Thread(ReadData);
            threadRead.Start();


            this.DataProcessHandler += DataProcessHandler;
        }


        private void ReadData()
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] recvData = new byte[1024 * 10];
            while (tcpClient.Connected)
            {
                try
                {
                    int bufSize = tcpClient.ReceiveBufferSize;
                    int count = stream.Read(recvData, 0, bufSize);
                    string str = Encoding.ASCII.GetString(recvData, 0, count);
                    DataProcessHandler?.Invoke(str);
                }
                catch (System.IO.IOException err)
                { }
            }
        }


        public void Stop()
        {
            tcpClient.Close();
            this.DataProcessHandler -= DataProcessHandler;
        }
    }
}
