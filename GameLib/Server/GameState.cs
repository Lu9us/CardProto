using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace GameLib.Server
{
   public class GameState
    {
        public DataMapManager dataManager = new DataMapManager();
       public Player[] players = new Player[2];
    }
}
