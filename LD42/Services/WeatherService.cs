using GameLib.DataStructures.Interface;
using GameLib.Server;
using GameLib.Unified.Services.MapService;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace LD42.Services
{
    public class WeatherService : IService
    {
        public int[,] tempMap;
        public int[,] rainMap;
        public int[,] waterMap;
        Random r = new Random();
        int tick = 0;
        GameState gs;
        public bool start = false;
        public void OnClose()
        {

        }

      
      

        public void OnReciveMessage(object message)
        {

        }

        public void OnStart(GameState s)
        {
            tempMap = new int[100, 100];
            rainMap = new int[100, 100];
            waterMap = new int[100, 100];

            for (int i = 0; i < tempMap.GetLength(0); i++)
            {
                for (int j = 0; j < tempMap.GetLength(1); j++)
                {
                    waterMap[i, j] = 10+ r.Next(0,70); 
                    tempMap[i, j] = 0+ r.Next(-10, 30); 
                    rainMap[i, j] = 0 + r.Next(0, 23); 
                }
            }
        
            gs = s;
            gs.varTable.AddItem("weather", this);
        }

        public void OnUpdate()
        {
            if (!start)
            {
                try
                {
                    CalculateTiles();
                    start = true;
                }
                catch (Exception e)
                { }
            }
            if (tick == 120)
            {
                CalculateTiles();
                tick = 0;
            }
            tick++;
        }



        private void CalculateTiles()
        {
            int[,] tileMap = gs.varTable.GetItem<TileMap>("map").tileMap;

            for (int i = 0; i < tempMap.GetLength(0); i++)
            {
                for (int j = 0; j < tempMap.GetLength(1); j++)
                {
                    waterMap[i, j] += rainMap[i, j] - tempMap[i, j];
                    if (waterMap[i, j] > 0 && tempMap[i, j] > 0)
                    {
                        tileMap[i, j] = 0;
                    }
                    if (waterMap[i, j] > 10 && tempMap[i, j] < 0)
                    {
                        tileMap[i, j] = 3;
                    }
                    if (waterMap[i, j] > 60 && tempMap[i, j] >= 0)
                    {
                        tileMap[i, j] = 4;
                    }

                    if (waterMap[i, j] <= 0 && tempMap[i, j] >= 0)
                    {
                        tileMap[i, j] = 1;
                    }
                    if (MapService.NeighboursContainValue(4, tileMap, i, j))
                    {
                        tileMap[i, j] = 0;
                    }


                }

            }

        }
    }
}
