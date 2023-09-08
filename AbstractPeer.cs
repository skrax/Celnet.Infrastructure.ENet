using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Celnet.Domain;
using Celnet.Domain.Events;
using Celnet.Domain.Interfaces;
using Celnet.ENet;

namespace Celnet.Infrastructure.ENet
{
    public abstract class AbstractPeer : IPeer
    {
        public event Action<PeerConnectEvent>? OnPeerConnected;
        public event Action<PeerDisconnectEvent>? OnPeerDisconnected;
        public event Action<PeerTimeoutEvent>? OnPeerTimeout;
        public event Action<PeerReceiveEvent>? OnPeerReceive;

        protected abstract Host? Host { get; }

        protected readonly Dictionary<uint, Peer> Peers;

        protected AbstractPeer()
        {
            Peers = new Dictionary<uint, Peer>();
        }

        public void Poll()
        {
            if (Host is null) return;
            if (Host.CheckEvents(out var enetEvent) <= 0)
            {
                if (Host.Service(15, out enetEvent) <= 0)
                    return;
            }

            switch (enetEvent.Type)
            {
                case EventType.None:
                    break;
                case EventType.Connect:
                    Peers.Add(enetEvent.Peer.ID, enetEvent.Peer);
                    OnPeerConnected?.Invoke(new PeerConnectEvent
                    {
                        PeerId = enetEvent.Peer.ID
                    });
                    break;
                case EventType.Disconnect:
                    Peers.Remove(enetEvent.Peer.ID);
                    OnPeerDisconnected?.Invoke(new PeerDisconnectEvent
                    {
                        PeerId = enetEvent.Peer.ID
                    });
                    break;
                case EventType.Receive:
                    var data = new byte[enetEvent.Packet.Length];
                    Marshal.Copy(enetEvent.Packet.Data, data, 0, enetEvent.Packet.Length);
                    OnPeerReceive?.Invoke(new PeerReceiveEvent
                    {
                        PeerId = enetEvent.Peer.ID,
                        ChannelId = enetEvent.ChannelID,
                        Data = data
                    });
                    enetEvent.Packet.Dispose();
                    break;

                case EventType.Timeout:
                    OnPeerTimeout?.Invoke(new PeerTimeoutEvent
                    {
                        PeerId = enetEvent.Peer.ID
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Host.Flush();
        }

        public bool TrySend(PeerSendArgs args)
        {
            if (Host is null) return false;
            if (!Peers.TryGetValue(args.PeerId, out var peer)) return false;

            var packet = default(Packet);
            packet.Create(args.Data);

            return peer.Send(args.ChannelId, ref packet);
        }
    }
}