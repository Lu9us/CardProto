using GameLib.AI.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.DataStructures.AI.PathFinding
{
    public interface IPathFinder
    {
        PathFindingResult PathFindFromCurrentPosition(PathFindingNode startPoint, PathFindingNode target);
    }
}
