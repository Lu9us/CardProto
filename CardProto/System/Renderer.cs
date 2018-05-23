using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardProto.System
{
  public class Renderer
    {
       public static List<Renderable> data = new List<Renderable>();

        public static int SortByLayer(Renderable a, Renderable b)
        {
            if (a.layer < b.layer)
            {
                return 1;
            }
            return 0;
        }

    }
}
