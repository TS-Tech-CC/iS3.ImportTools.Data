using iS3.ImportTools.Core.Interface;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iS3.ImportTools.DataAcqire
{
    public class DataAcquire_NetMQ : IDataAcquire
    {
        private SubscriberSocket subSocket;
        private Action<object> DataProcessHandler;

        public void Start(Action<object> DataProcessHandler)
        {

            subSocket = new SubscriberSocket();
            subSocket.Connect("tcp://127.0.0.1:60001");
            subSocket.Subscribe("A");

            Thread thread = new Thread(ReadData);
            thread.Start();


            this.DataProcessHandler = DataProcessHandler;
        }

        private void ReadData()
        {
            while (true)
            {
                string messageReceived = subSocket.ReceiveFrameString();
                DataProcessHandler?.Invoke(messageReceived);
            }
        }

        public void Stop()
        {
            subSocket.Close();
            DataProcessHandler -= DataProcessHandler;
        }
    }
}
