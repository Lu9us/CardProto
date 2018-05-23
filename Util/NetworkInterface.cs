using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class NetworkInterface
    {
       public Socket main;
       public List<Socket> clients = new List<Socket>();
        public NetworkInterface()
        {
    
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
