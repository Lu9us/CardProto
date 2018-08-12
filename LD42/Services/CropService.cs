using GameLib.DataStructures.Interface;
using GameLib.Server;
using GameLib.Server.Services;
using GameLib.Unified.Services.MapService;
using LD42.Services;
using LD42.Services.Crops;
using LD42.Services.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD42
{
    public class CropService : IService
    {

        public List<Crop> crops = new List<Crop>();
        private GameState gs;
        public ResourceService rs;
        public WeatherService weather;
        CropRenderer c;
        public void AddCrop(Crop c)
        {
            if (rs != null)
            {
        
                if (rs.getScore() >= 1)
                {
                    if (!crops.Any(cs => cs.location == c.location))
                    {
                        gs.soundEffects.playSound("SoundEffects/shoveling");
                        rs.DecreasePoints(1, "Plated Crop");
                        crops.Add(c);
                    }
                }
            }
        }
        public void OnClose()
        {
          
        }

        public void OnReciveMessage(object message)
        {
          
        }

        public void OnStart(GameState s)
        {
            gs = s;
            c = new CropRenderer(crops);
        }

        public void OnUpdate()
        {
            try
            {
                int[,] tileMap = gs.varTable.GetItem<TileMap>("map").tileMap;
                if (rs == null)
                {
                    rs = ((ResourceService)ServiceController.runningServices[typeof(ResourceService).FullName]);
                }

                if (weather == null)
                {
                    weather = ((WeatherService)ServiceController.runningServices[typeof(WeatherService).FullName]);
                }
                foreach (Crop c in crops.Where(c => c.state == State.Growing))
                {
                    if (weather.tempMap[(int)c.location.X, (int)c.location.Y] > 60 || weather.waterMap[(int)c.location.X, (int)c.location.Y] > 60)
                    {
                        c.state = State.Failed;
                        rs.DecreasePoints(1, "Failed  Crop");
                    }
                    
                    else if (c.age > 2*60)
                    {
                        c.state = State.Succeded;
                        rs.IncreasePoints(2, "Successful Crop");
                    }

                        tileMap[(int)c.location.X, (int)c.location.Y] = 2;
                    if (weather.waterMap[(int)c.location.X, (int)c.location.Y] > 0)
                    {
                        c.age++;
                    }
                }
                crops.RemoveAll(a => a.state != State.Growing);
            }
            catch (Exception e)
            {
            }
           
        }
    }
}
