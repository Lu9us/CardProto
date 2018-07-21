using GameLib.DataStructures;
using GameLib.DataStructures.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Server
{
   public class GameState
    {
        public bool gameRunning = false;
        public Player[] players = new Player[2];
        public WorldState world = new WorldState();
        public int FrameCount;
        public int SecondFrameCount { get { return FrameCount % 60;} }
        public IExtendedVarTable varTable = new HashVarTable();
    }
}
