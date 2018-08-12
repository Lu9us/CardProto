using GameLib.Client.Services;
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

        public static bool NeighboursContainValue(int value, int[,] data, int posx, int posy)
        {
            bool result = false; 
            for (int i = posx - 1; i < posx + 2; i++)
            {
                for (int j = posy - 1; j < posy + 2; j++)
                {
                    if (i > 0 && i < data.GetLength(0) && j > 0 && j < data.GetLength(0))
                    {
                        if (data[i, j] == value)
                        {
                            return true;
                        }
                    }
                   
                }

            }
            return false;
        }

        public static void SetNeighboursContainValue(int value, int[,] data, int posx, int posy)
        {
            for (int i = posx - 1; i < posx + 2; i++)
            {
                for (int j = posy - 1; j < posy + 2; j++)
                {
                    if (i > 0 && i < data.GetLength(0) && j > 0 && j < data.GetLength(0))
                    {
                        data[i,j] += value;
                    }

                }

            }
          
        }


        ISerizilizer serizilizer = new JSONSerilizer();
        GameState gs = new GameState();
        TileMap map;
        List<Tile> tiles = new List<Tile>();
        bool tilesRecived;
        bool mapRecived;
        MapRenderer renderer;

        public MapService()
        {
         
            
        }
        public void OnClose()
        {
            Console.WriteLine("MapService stopping");
        }

        public void OnReciveMessage(object message)
        {
        
        }

        public void OnStart(GameState s)
        {
            Console.WriteLine("MapService Start");
            gs = s;
            if (ServiceController.getRuntime() == Runtime.SERVER||ServiceController.getRuntime()==Runtime.HYBRID)
            {
                if (File.Exists("Server.tile"))
                {
                    byte[] tiledata = File.ReadAllBytes("Server.tile");
                    tiles = serizilizer.DeSerilize<List<Tile>>(tiledata);
                    gs.varTable.AddItem("tiles", tiles);


                    if (File.Exists("Server.map"))
                    {


                        byte[] mapdata = File.ReadAllBytes("Server.map");
                        map = serizilizer.DeSerilize<TileMap>(mapdata);
                        
                    }
                    else
                    {
                        map = new TileMap();

                    }
                    if (ServiceController.getRuntime() == Runtime.HYBRID)
                    {
                        renderer = new MapRenderer(tiles, map);
                    }
                }
                else
                {
                    if (!File.Exists("Server.tile"))
                    {
                        List<Tile> list = new List<Tile>();
                        list.Add(new Tile());
                        File.WriteAllBytes("Server.tile", serizilizer.Serilize<List<Tile>>(list));
                    }
                }
            }
            else
            {
                gs.dataManager.getCurrentMap().AddData("map:sendTile", new object());
            }
           gs.dataManager.AddClient("map", this);
            gs.varTable.AddItem("map", map);
        }

        public void OnUpdate()
        {
            Console.WriteLine("MapService Update");
            if (ServiceController.getRuntime() == Runtime.CLIENT)
            {
                if (!tilesRecived)
                {
                    gs.dataManager.getCurrentMap().AddData("map:sendTile", new object());
                }
                if (!mapRecived)
                {
                    gs.dataManager.getCurrentMap().AddData("map:sendTile", new object());
                }
            }
        }

        public void Update(object data, DataMap source)
        {
            if (ServiceController.getRuntime() == Runtime.SERVER)
            {
                Console.WriteLine("MapService data received");
                processDataServer(data, source);
            }
            else if (ServiceController.getRuntime() == Runtime.CLIENT)
            {
                processDataClient(data, source);
            }
            else if (ServiceController.getRuntime() == Runtime.HYBRID)
            {
                processDataHybrid(data, source);
            }
        }

        private void processDataHybrid(object data, DataMap source)
        {


        }

        private void processDataServer(object data,DataMap source)
        {
            object request;
            if (( request = source.GetData("map:sendTile")) != null)
            {
                gs.dataManager.getCurrentMap().AddData("map:tileData", tiles);

            }
            if ((request = source.GetData("map:sendMap")) != null)
            {
                gs.dataManager.getCurrentMap().AddData("map:map", TileMap.pack( map.tileMap));

            }

        }
        private void processDataClient(object data, DataMap source)
        {
            List<Tile> tileData;

            if ((tileData = (List<Tile>)source.GetData("map:tileData")) != null)
            {
                tiles = tileData;
                gs.dataManager.getCurrentMap().AddData("map:sendMap", new object());
                tilesRecived = true;
            }
            if (source.GetData("map:map") != null)
            {
                map = new TileMap { tileMap = TileMap.unpack((string)source.GetData("map:map")) };
                renderer = new MapRenderer(tiles, map);
                mapRecived = true;
            }
        }
    }
}
