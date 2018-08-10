using GameLib.DataStructures.Interface;
using GameLib.Server;
using GameLib.Server.Services;
using GameLib.Server.Services.ServiceLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using Util.Interfaces;

namespace GameLib.Unified.Services.MapService
{
    public class MapService : IService, DataInterface
    {
        ISerizilizer serizilizer = new JSONSerilizer();
        GameState gs = new GameState();
        TileMap map;
        List<Tile> tiles = new List<Tile>();

        public MapService()
        {
         
            
        }
        public void OnClose()
        {
            Console.WriteLine("MapService stopping");
        }

        public void OnReciveMessage(object message)
        {
            Console.WriteLine("MapService message Recieved");
        }

        public void OnStart(GameState s)
        {
            Console.WriteLine("MapService Start");
            gs = s;
            if (ServiceController.getRuntime() == Runtime.SERVER)
            {
                if (File.Exists("Server.map"))
                {
                    if (File.Exists("Server.tile"))
                    {
                        byte[] tiledata = File.ReadAllBytes("Server.tile");
                         tiles = serizilizer.DeSerilize<List<Tile>>(tiledata);
                    }

                    byte[] mapdata = File.ReadAllBytes("Server.map");
                    map = serizilizer.DeSerilize<TileMap>(mapdata);
                    gs.varTable.AddItem("map", map);
                }
                else
                {
                    map = new TileMap();
                    tiles = new List<Tile>();
                    Tile t = new Tile();
                    t.filePath = "default";
                }
            }
           gs.dataManager.AddClient("Map", this);
        }

        public void OnUpdate()
        {
            Console.WriteLine("MapService Update");
        }

        public void Update(object data, DataMap source)
        {
            Console.WriteLine("MapService data received");
        }
    }
}
