using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.AI.PathFinding
{
   public interface PathFindingNode
    {
        List<PathFindingNode> GetNeighbours();
    }
}
