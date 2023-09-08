using System;
using Celnet.ENet;

namespace Celnet.Infrastructure.ENet
{
    public sealed class ENetService
    {
        public ENetService()
        {
            if (!Library.Initialize())
                throw new InvalidOperationException("Failed to initialize ENet");
        }

        ~ENetService()
        {
            Library.Deinitialize();
        }
    }
}