using GameLib.AI.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.DataStructures.AI.PathFinding
{
    public class AStarPathFinder : IPathFinder
    {
        public delegate int HeuristicComparitor(PathFindingNode a, PathFindingNode b);

        HeuristicComparitor comparitor;

        public AStarPathFinder(HeuristicComparitor comparitor)
        {
            this.comparitor = comparitor;
        }
        public PathFindingResult PathFindFromCurrentPosition(PathFindingNode startPoint, PathFindingNode target)
        {
            PathFindingResult result = new PathFindingResult();
            List<PathFindingNode> neighbours = startPoint.GetNeighbours();
            List<PathFindingNode> openNodes = new List<PathFindingNode>();
            List<PathFindingNode> closedNodes = new List<PathFindingNode>();
            List<PathFindingNode> pathing = new List<PathFindingNode>();
            PathFindingNode currentNode = startPoint;
            openNodes.Add(startPoint);

            while (openNodes.Count > 0)
            {

                openNodes.Sort(delegate (PathFindingNode a, PathFindingNode b)
                {
                    return comparitor.Invoke(a, b);
                });
                currentNode = openNodes[0];
                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);
                if (currentNode == target)
                {
                    pathing.Add(target);
                    result.result = pathing;
                    result.success = true;
                    return result;
                }
                else
                {
                    foreach (PathFindingNode neighbour in currentNode.GetNeighbours())
                    {
                       

                        if (closedNodes.Contains(neighbour) || openNodes.Contains(neighbour))
                        {
                            continue;
                        }
                        if (openNodes.Count > 0 && comparitor(neighbour, target) > comparitor(openNodes.Last(), target))
                        {
                            continue;
                        }
                        openNodes.Add(neighbour);
                    }
                    pathing.Add(currentNode);
                }


            }
            result.result = pathing;
            result.success = false;

            return result;
        }


    }
}
