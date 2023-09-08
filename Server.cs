using System;
using System.Diagnostics.CodeAnalysis;
using Celnet.Domain.Interfaces;
using Celnet.ENet;

namespace Celnet.Infrastructure.ENet
{
    public class Server : AbstractPeer, IServer
    {
        [SuppressMessage("ReSharper", "NotAccessedField.Local")] 
        private readonly ENetService _enetService;
        private Host? _host;
        protected override Host? Host => _host;

        public bool IsRunning => _host != null;

        public Server(ENetService enetService)
        {
            _enetService = enetService;
        }
        
        public void Create(ushort port, int maxPeers)
        {
            if (_host != null) throw new InvalidOperationException("Server is already running");
            var address = new Address
            {
                Port = port
            };
            _host = new Host();
            _host.Create(address, maxPeers);
        }

        public void Dispose()
        {
            foreach (var peer in Peers.Values)
            {
                peer.DisconnectNow(0);
            }
            _host?.Flush();
            _host?.Dispose();
        }
    }
}