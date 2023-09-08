using System.Diagnostics.CodeAnalysis;
using Celnet.Domain.Interfaces;
using Celnet.ENet;

namespace Celnet.Infrastructure.ENet
{
    public class Client : AbstractPeer, IClient
    {
        [SuppressMessage("ReSharper", "NotAccessedField.Local")] 
        private readonly ENetService _eNetService;
        private Host? _host;
        private Peer? _peer;
        protected override Host? Host => _host;

        public bool IsConnected => _host != null && _peer is { State: PeerState.Connected };

        public Client(ENetService enetService)
        {
            _eNetService = enetService;
        }

        public void Connect(string ip, ushort port, int channelLimit)
        {
            _host = new Host();
            _host.Create();
            var address = new Address
            {
                Port = port
            };
            address.SetHost(ip);

            _peer = _host.Connect(address, channelLimit);
        }

        public void Disconnect()
        {
            _peer?.Disconnect(0);
            _host?.Flush();
        }

        public void Dispose()
        {
            _host?.Dispose();
        }
    }
}