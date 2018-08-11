using GameLib.Server;
using GameLib.Unified.Services.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD42.Services
{
    public class Drain : Improvement
    {

        public Drain()
        {
            sprite = "drain";
        }
        public override void Update(GameState gs)
        {
            try
            {
              WeatherService weather = gs.varTable.GetItem<WeatherService>("weather");
                TileMap tm = gs.varTable.GetItem<TileMap>("map");
                MapService.SetNeighboursContainValue(-20, weather.waterMap, (int)position.X, (int)position.Y);
            }
            catch (Exception e)
            {
            }
        }
    }
}
