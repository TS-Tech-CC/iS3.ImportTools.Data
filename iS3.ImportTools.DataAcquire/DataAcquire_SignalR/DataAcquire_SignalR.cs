using iS3.ImportTools.Core.Interface;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.DataAcqire
{
    public class DataAcquire_SignalR : IDataAcquire
    {

        private HubConnection hubConn;

        public async void Start(Action<object> DataProcessHandler)
        {

            hubConn = new HubConnection("http://127.0.0.1:55138/signalr");
            IHubProxy hubProxy = hubConn.CreateHubProxy("SignalRHub");
            hubProxy.On("sendMessage", DataProcessHandler);
            await hubConn.Start();
        }

        public void Stop()
        {
            hubConn.Stop();
        }
    }
}
