using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Server
{
  public class Player
    {
        public int playerID;
        public Socket Client;
        public string Name;
    }
}
