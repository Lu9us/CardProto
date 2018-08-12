using GameLib.Client.UI;
using GameLib.DataStructures.Interface;
using GameLib.Server;
using GameLib.Server.Services;
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
        ResourceService resources;
        Random r = new Random(Guid.NewGuid().GetHashCode());
        int tick = 0;
        bool[] envormentWarnings = new bool[2];
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
                    waterMap[i, j] = 10 + r.Next(-1, 70);
                    tempMap[i, j] = 0 + r.Next(1, 40);
                    rainMap[i, j] = 0 + r.Next(-1, 10);
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
            if (resources == null)
            {
                resources = ((ResourceService)ServiceController.runningServices[typeof(ResourceService).FullName]);
            }

            if (tick % 60 == 0)
            {
                CalculateTiles();
            }
            if (tick == 640)
            {
                tick = 0;
            }
            for (int i = 0; i < tempMap.GetLength(0); i++)
            {
                for (int j = 0; j < tempMap.GetLength(1); j++)
                {
                    if (waterMap[i, j] < 0)
                    {
                        waterMap[i, j] = 0;
                    }
                    if (waterMap[i, j] > 100)
                    {
                        waterMap[i, j] = 100;
                    }
                    if (tempMap[i, j] < -70)
                    {
                        tempMap[i, j] = -70;

                    }
                    if (tempMap[i, j] > 80)
                    {
                        tempMap[i, j] = 80;
                    }

                }
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

                    waterMap[i, j] += rainMap[i, j] - (tempMap[i, j]/3);

                    if (waterMap[i, j] >= 50)
                    {
                        MapService.SetNeighboursContainValue(waterMap[i, j] / 100, waterMap, i, j);
                        waterMap[i, j] -= waterMap[i, j] / 100;
                    }
                    if (waterMap[i, j] > 0 && tempMap[i, j] > 0)
                    {
                        tileMap[i, j] = 0;
                        if (waterMap[i, j] > 60)
                        {
                            tileMap[i, j] = 4;
                        }
                    }
                    else if (waterMap[i, j] > 10 && tempMap[i, j] < 0)
                    {
                        tileMap[i, j] = 3;
                    }



                    if (waterMap[i, j] <= 0 && tempMap[i, j] >= 0)
                    {
                        tileMap[i, j] = 1;
                    }

                    if (gs.FrameCount == 60 * 3)
                    {
                        tempMap[i, j] += 4;
                        rainMap[i, j] -= 4;
                        if (!envormentWarnings[0])
                        {
                            resources.IncreasePoints(0, "Global warming started");
                            envormentWarnings[0] = true;
                        }
                    }
                    if (gs.FrameCount > 60 * 7)
                    {
                        rainMap[i, j] += 2;
                        if (!envormentWarnings[1])
                        {

                            resources.IncreasePoints(0, "Global flooding started");
                            envormentWarnings[1] = true;
                        }
                    }
                    if (gs.FrameCount > 60 * 18)
                    {
                        rainMap[i, j] -= 4;
                        tempMap[i, j] -= 2;

                    }
                    if (gs.FrameCount > 60 * 24)
                    {
                        gs.FrameCount = 0;

                    }
                }


            }

        }
    }
}

