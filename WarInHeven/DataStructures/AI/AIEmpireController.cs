using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib.Client.System;
using GameLib.Server;
using WarInHeven.DataStructures.Interfaces;

namespace WarInHeven.DataStructures.AI
{
    public class AIEmpireController : EmpireController
    {
        Empire empire;
        AIState AIState = AIState.EXPAND;
        public AIEmpireController(Empire empire) : base(empire)
        {
            this.empire = empire;
        }

        public override void Update(GameState gs)
        {
            StarMap starMap=gs.varTable.GetItem<StarMap>("world");
            Empire neutral = starMap.empires.First(a => starMap.isNeutral(a));
            if (AIState == AIState.EXPAND)
            {
                Star choice = empire.planets[RandomHelper.getRandomInt(0, empire.planets.Count)];
                foreach (Star planet in choice.neigbours)
                {
                    if (neutral.planets.Contains(planet))
                    {
                        neutral.planets.Remove(planet);
                        planet.color = empire.color;
                        empire.planets.Add(planet);
                        break;
                    }
                }
            }
         
        }
    }
}
