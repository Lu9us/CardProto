using GameLib.Server;
using GameLib.Unified.Services.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD42.Services
{
   public class Heater: Improvement
    {
        public Heater()
        {
            sprite = "heater";
        }
        public override void Update(GameState gs)
        {
            try
            {

                WeatherService weather = gs.varTable.GetItem<WeatherService>("weather");
                MapService.SetNeighboursContainValue(25, weather.tempMap, (int)position.X, (int)position.Y);
            }
            catch (Exception e)
            {
            }
        }

    }
}
