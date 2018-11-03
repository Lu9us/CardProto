using GameLib.Client.System;
using GameLib.Client.System.SoundHandlers;
using GameLib.DataStructures;
using GameLib.DataStructures.Implementations;
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
        public SoundEffectAtlas soundEffects;
        public List<Player> players = new List<Player>();
        public bool gameRunning = false;
        public WorldState world = new WorldState();
        public Camera camera;
        public long FrameCount;
        public int SecondFrameCount { get { return (int)(FrameCount % 60);} }
        public int runtime { get { return (int)(FrameCount / 60); } }
        public IExtendedVarTable varTable = new HashVarTable();
    }
}
