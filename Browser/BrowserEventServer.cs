using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser
{
    class BrowserEventServer : IBrowserEventHandler
    {
        private PairSocket _Server;

        public BrowserEventServer(string eventAddress)
        {
            _Server = new PairSocket();
            _Server.Bind(eventAddress);
        }

        public void AddressChanged(string address)
        {
            var browserEvent = new BrowserEvent { Address = address };
            _Server.SendFrame(JsonConvert.SerializeObject(browserEvent));
        }
    }
}
