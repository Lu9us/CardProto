using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib.AI.PathFinding;
using GameLib.Client.UI;
using GameLib.DataStructures.AI.PathFinding;
using GameLib.Server;
using Microsoft.Xna.Framework;
using WarInHeven.DataStructures.AI.Orders;
using WarInHeven.DataStructures.GameData;
using WarInHeven.DataStructures.Interfaces;

namespace WarInHeven.DataStructures.AI
{
    public class FleetController : IController
    {
        private static Dictionary<FleetController, List<Label>> aiDebugPanel = new Dictionary<FleetController, List<Label>>();
        public Fleet fleet;
        private Order currentOrder;
        public static bool AiDebug = false;
        IPathFinder pathFinder = new AStarPathFinder(Star.Sort);
        List<PathFindingNode> currentPath;
        public bool busy = false;

        public FleetController(Fleet f)
        {
            fleet = f;
            aiDebugPanel[this] = new List<Label>();
            for (int i = 0; i < 5; i++)
            {
                Label l = new Label(new Microsoft.Xna.Framework.Vector2(aiDebugPanel.Count + i * 10, aiDebugPanel.Count * 50 + i * 10));
                l.UpdateColor(fleet.owner.color);
                aiDebugPanel[this].Add(l);
            }

        }



        public void AIDebug()
        {
            
            String currentPathString = "current path: ";
            if (currentPath != null)
            {
                foreach (PathFindingNode s in this.currentPath)
                {
                    currentPathString += ((Star)s).name + " " + ((Star)s).position;
                }
            }
            if (AiDebug)
            {
                if (currentOrder != null)
                {
                    aiDebugPanel[this][0].Update(((Star)currentOrder.target).name + " " + ((Star)currentOrder.target).position);
                }
                aiDebugPanel[this][1].Update(currentPathString);
                aiDebugPanel[this][2].Update(((AIEmpireController)fleet.owner.controller).openOrders.Count.ToString());
                aiDebugPanel[this][3].Update(fleet.position.name + " " + fleet.position.position);
            }
            else
            {
                aiDebugPanel[this][0].Update(String.Empty);
                aiDebugPanel[this][1].Update(String.Empty);
                aiDebugPanel[this][2].Update(String.Empty);
                aiDebugPanel[this][3].Update(String.Empty);
            }



        }


        public void Update(GameState gs)
        {
            StarMap starMap = gs.varTable.GetItem<StarMap>("world");
            if (currentOrder == null)
            {
               List<Order> orderedOrders = new List<Order>(((AIEmpireController)fleet.owner.controller).openOrders);
           
                    orderedOrders.Sort((v1, v2) => v1.Priority.CompareTo(v2.Priority));
                    orderedOrders.Reverse();
              

                foreach (Order order in orderedOrders)
                {
                    if (order.beingDone == false)
                    {
                        currentOrder = order;
                        order.beingDone = true;
                        order.actor = this.fleet;
                        return;
                    }
                }
            }

            if (currentOrder != null)
            {
                if (currentPath == null)
                {
                    PathFindingResult result = pathFinder.PathFindFromCurrentPosition(fleet.position, (Star)currentOrder.target);

                    if (result.success)
                    {
                        currentPath = result.result;
                        busy = true;
                    }
                    else
                    {

                        ((AIEmpireController)fleet.owner.controller).openOrders.Remove(currentOrder);
                        ((AIEmpireController)fleet.owner.controller).impossibleOrders.Add(currentOrder);
                        currentOrder = null;
                        currentPath = null;
                        busy = false;

                    }
                }



                else
                {
                    try
                    {
                        if (currentPath.Count > 0)
                        {
                            if (currentPath[0] != null)
                            {
                                fleet.position = (Star)currentPath[0];
                                currentPath.Remove(fleet.position);
                            }
                        }
                        if (currentPath.Count == 0 || fleet.position.id == ((Star)currentOrder.target).id)
                        {
                            busy = false;
                            currentOrder = null;
                            currentPath = null;

                            ((AIEmpireController)fleet.owner.controller).openOrders.Remove(currentOrder);

                            if (starMap.isNeutral(fleet.position.empire))
                            {
                                starMap.SetPlanetToEmpire(fleet.owner, fleet.position);
                            }
                            else if(fleet.position.empire == fleet.owner)
                            {
                                fleet.position.resistance += -60;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.StackTrace);
                    }
                }
            }
            
            AIDebug();
        }
    }
}
