## Welcome to my first TCP Chatroom!

### Project Description
This TCP chatroom was created at a workshop for Networking held by Game Dev Knights hosted by GitHub user [1samuel411](https://github.com/1samuel411)! <br />
I built on what he taught me by adding usernames, a scrolling chatbox, and a more streamlined IP entry system.

[Link to Exampe Video](https://youtu.be/eShlQrGn2aI)

#### How it works
- Utilizing the System.Net and Systems.Net.Sockets namespaces, a server is created outside of Unity on a specified IP and Port.

```C#
    public class Server
    {
        private Socket serverSocketTCP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private byte[] buffer = new byte[1024];
        private Dictionary<Socket, int> connectedUsers = new Dictionary<Socket, int>();

        public void Setup(string ip, string port)
        {
            Console.WriteLine("Creating a server on IP: " + ip + ":" + port);

            IPAddress iPAddress = IPAddress.Parse(ip);
            serverSocketTCP.Bind(new IPEndPoint(iPAddress, int.Parse(port)));
            serverSocketTCP.Listen(100);

            BeginAccepting();
        }
```
- Clients are then able to connect to that server through the Unity Client Application

```C#
    public void Connect(string ip = "", int port = 25565)
    {
        if(clientSocket == null)
        {
            return;
        }
        if(IsConnected())
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }

        this.ip = ip;
        this.port = port;
        StartCoroutine(ConnectCoroutine());
    }
```
- When a user sends a message, it is sent to the server through TCP
```C#
    public void SendToServer(int header, string message)
    {
        if(IsConnected() == false)
        {
            Debug.Log("Not connected!");
            return;
        }
        
        byte[] tempArray = Encoding.UTF8.GetBytes(message);

        byte[] dataArray = new byte[tempArray.Length + 1];
        dataArray[0] = Convert.ToByte(header);
        Array.Copy(tempArray, 0, dataArray, 1, tempArray.Length);

        Debug.Log("Sending to server: " + message);

        clientSocket.BeginSend(dataArray, 0, dataArray.Length, SocketFlags.None, SendCallback, clientSocket);
    }
```
- The server receives this message as a callback
```C#
private void RecieveCallback(IAsyncResult Result)
        {
            Socket clientSocket = (Socket)Result.AsyncState;
            if (clientSocket.Connected == false)
            {
                Console.WriteLine("Attempting to recieve from disconnected client");
                return;
            }

            int recieved = clientSocket.EndReceive(Result);

            if (recieved <= 0)
            {
                BeginReceiving(clientSocket);
                return;
            }

            byte[] dataRecieved = new byte[recieved];

            Array.Copy(buffer, dataRecieved, recieved);

            int clientSender = connectedUsers[clientSocket];

            byte header = dataRecieved[0];
            byte[] messageData = new byte[dataRecieved.Length - 1];
            Array.Copy(dataRecieved, 1, messageData, 0, messageData.Length);

            string messageRecieved = Encoding.UTF8.GetString(messageData);

            Console.WriteLine("Recieved from (" + clientSender + "): " + header + " - " + messageRecieved);
```
- And then sends it out to the clients once again
```C#
       private void SendToClient(Socket socket, int header, string message)
        {
            if (socket.Connected == false)
            {
                Console.WriteLine("Not connected!");
                return;
            }

            byte[] tempArray = Encoding.UTF8.GetBytes(message);

            byte[] dataArray = new byte[tempArray.Length + 1];
            dataArray[0] = Convert.ToByte(header);
            Array.Copy(tempArray, 0, dataArray, 1, tempArray.Length);

            Console.WriteLine("Sending data");
            
            
            
            socket.BeginSend(dataArray, 0, dataArray.Length, SocketFlags.None, SendCallback, socket);
        }
```
- The client receives the message and 
