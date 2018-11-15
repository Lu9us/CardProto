using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib.Client.System;
using GameLib.Server;
using WarInHeven.DataStructures.AI.Orders;
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

        public List<Order> openOrders = new List<Order>();
        public List<Order> impossibleOrders = new List<Order>();

        public override void Update(GameState gs)
        {
            StarMap starMap=gs.varTable.GetItem<StarMap>("world");
            Empire neutral = starMap.empires.First(a => starMap.isNeutral(a));
            if (AIState == AIState.EXPAND)
            {
                Star choice = empire.planets[RandomHelper.getRandomInt(0, empire.planets.Count)];
                if (empire.fleets.Count < 1 || empire.fleets.Count < openOrders.Count(a=>!a.beingDone)/2)
                {
                    starMap.MakeNewFleet(empire, empire.planets[0]);
                }
                foreach (Star planet in choice.neigbours)
                {
                    if (planet != null)
                    {
                        if (planet.empire == neutral)
                        {
                            Order order = new Orders.Order { target = planet, type = Orders.OrderType.MoveTo, Priority = 10};
                            if (!openOrders.
                                Any(a => a.target == order.target 
                                && a.type == order.type)
                                && !impossibleOrders.Any(a => a.target == order.target 
                                && a.type == order.type
                                )
                                )
                            { 
                                openOrders.Add(order);
                            }
                            break;
                        }
                    }
                }
            }
         
        }
    }
}
