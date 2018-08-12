using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib.Server;
using GameLib.Unified.Services.MapService;

namespace LD42.Services
{
    public class Aqufier : Improvement
    {
        public Aqufier()
        {
            sprite = "aqufier";
            cost = 25;
        }
        int maxValue = 24;
        int currentValue = 0;
        public override void Update(GameState gs)
        {
            try
            {
                
                WeatherService weather = gs.varTable.GetItem<WeatherService>("weather");
                MapService.SetNeighboursContainValue(2, weather.waterMap, (int)position.X, (int)position.Y);
            }
            catch (Exception e)
            {
            }
        }
    }
}
