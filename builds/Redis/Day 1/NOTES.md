# REDIS

*Command - Response*
C# has `TcpListener` a high-level wrapper that listens to incoming TCP connections, `IPAddress.Any` listens to all network intefaces (like `0.0.0.0`), via `LAN IP`, `localhost` etc. Alternative is `IPAddress.Loopback`, only accepts connections from the same machine.

`AcceptSocket` is blocking call, pauses program until a client connects, `Socket` is lower level gives us more control.

*The Berkeley socket Api*
OS implements the TCP/IP layer/stack. Program asks the kernel for network services through system calls.

The API for these calls is `Berkeley Sockets` which dates to BSD Unix (1983). It's `POSIX` sockets on Linux and MacOS and `Winsock` on Windows.

The steps for any program boils down to this:
    1. `socket()` - asks the kernel to create one and return the `handle`
    2. `bind()` - bind the socket to an address and port (example: 0.0.0.0:6379)
    3. `listen()` - mark it a passive socket and queue the incoming connections
    4. `accept()` - blocks the program until a client connects
    5. `recv()`/`send()` - read/write bytes
    6. `close()` - tear down the connection

*C# high level wrapper*
In C# `TcpListener` just stores the addr and port, what actually calls the kernel is `Start()` which does 3 syscalls at once - `socket()`, `bind()`, `listen()`. The `AcceptSocket()` calls `accept()`, `Send()` calls `send()` and `Close()` calls `close()`. 
However the `Socket` class is a more direct mapping to the real API, with `Bind`, `Listen`, `Accept`, `Send`, and `Receive`. 
The same program can be written with only `Socket`:
```
var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
server.Bind(new IPEndPoint(IPAddress.Any, 6379));
server.Listen(5);
Socket client = server.Accept();
```
`IPAddress` is a typed container for an IP address. Under the hood it's similar to C implementation of `struct in_addr`.

*Multi-Client handler*
Used `Task.Run` to use threads for multiple client connection responses simultaneously. 
Should use `Event Loop` like the actual implementation of Redis.