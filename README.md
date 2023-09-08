# Celnet.Infrastructure.ENet

The Celnet.Infrastructure.ENet library is a powerful implementation of the Celnet.Domain peer interfaces using the ENet networking library.
This library simplifies the development of networking applications by providing ready-to-use client and server components built on ENet.

## Overview

Celnet.Infrastructure.ENet is a .NET library that bridges the gap between the Celnet.Domain library's peer interfaces and the ENet networking library.
It offers pre-built implementations of these interfaces, allowing you to quickly create networked applications using ENet as the underlying transport layer.

## Features

### Client Implementation

The library provides a client implementation, `Client`, that extends the `AbstractPeer` class and implements the `IClient` interface from Celnet.Domain. Key features include:

- **Connection Management**: Easily connect to a server, disconnect, and check the connection status.
- **ENet Integration**: Utilizes ENet's underlying functionality for efficient networking.

Here's an example of how to use the `Client` class:

```csharp
using Celnet.Infrastructure.ENet;

// Create a Client instance
var client = new Client(enetService);

// Connect to a server
client.Connect("127.0.0.1", 12345, 10);

// Check if the client is connected
bool isConnected = client.IsConnected;

// Disconnect from the server
client.Disconnect();

// Dispose the client
client.Dispose()
```

### Server Implementation

The library also provides a server implementation, `Server`, that extends the `AbstractPeer` class and implements the `IServer` interface from Celnet.Domain. Key features include:

- **Server Creation**: Easily create a server with specified port and maximum peer limit.
- **Peer Management**: Manage connected peers and their interactions.
- **ENet Integration**: Utilizes ENet's underlying functionality for efficient networking.

Here's an example of how to use the `Server` class:

```csharp
using Celnet.Infrastructure.ENet;

// Create a Server instance
var server = new Server(enetService);

// Create and start the server
server.Create(12345, 32);

// Check if the server is running
bool isRunning = server.IsRunning;

// Dispose of the server and disconnect all peers
server.Dispose();
```

## Getting Started

To get started with Celnet.Infrastructure.ENet, follow these steps:

1. Install the Celnet.Infrastructure.ENet library using NuGet Package Manager or add it to your project's references.

2. Import the necessary namespaces:
   
   ```csharp
   using Celnet.Infrastructure.ENet;
   ```

3. Create instances of the `Client` and `Server` classes to build your networked application. Configure them according to your specific requirements.

4. Utilize the provided client and server components to handle networking tasks, such as connecting to servers, managing peers, and sending/receiving messages.

## Contributing

We welcome contributions to the Celnet.Infrastructure.ENet library. If you have ideas for improvements, bug fixes, or new features, please open an issue or create a pull request on our GitHub repository.

## License

This library is licensed under the MIT License. See the [LICENSE](https://github.com/skrax/Celnet.Infrastructure.ENet/blob/main/LICENSE) file for details.
