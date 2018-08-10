using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class StateObject
    {
        public StateObject(int bufferSize,Socket clientSocket)
        {
            this.bufferSize = bufferSize;
            data = new byte[bufferSize];
            client = clientSocket;
        }
        public readonly int bufferSize;
        public byte[] data;
        public Socket client;
        public MemoryStream EntirePacket = new MemoryStream();

    }
    public class NetworkInterface
    {


       public Socket main;
        public List<Socket> clients = new List<Socket>();
        private byte[] networkBuffer;
        public NetworkInterface()
        {
    
        }

 
        public void SetBuffer(int var)
        {
            networkBuffer = new byte[var];
        }
        public void Accept(IAsyncResult result)
        {
            Socket listener = (Socket)result.AsyncState;
            Socket handler = listener.EndAccept(result);
            Console.WriteLine("Connected with "+handler);
            clients.Add(handler);
            main.BeginAccept(Accept, main);
        }

        public void HostServer()
        {
             main = new Socket(SocketType.Stream, ProtocolType.Tcp);
            long local = Dns.GetHostEntry(Dns.GetHostName())
.AddressList.First(
f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Address;

            main.Bind(new IPEndPoint(local, 3000));
            main.Listen(120);
            main.BeginAccept(Accept,main);
        }
        public void ConnectToServer()
        {
             main = new Socket(SocketType.Stream, ProtocolType.Tcp);
            string local = Dns.GetHostEntry(Dns.GetHostName())
     .AddressList.First(
         f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
     .ToString();
            main.Connect(local, 3000);


        }
    }
}
