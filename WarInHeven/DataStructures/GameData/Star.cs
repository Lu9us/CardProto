using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarInHeven
{
   public class Star
    {
        public Guid id = Guid.NewGuid();
        public String name = "";
        public Vector2 position = new Vector2();
        public Star[] neigbours = new Star[5];
        public Color color;
        public static int Sort(Star a, Star b)
        {
            return (int)Math.Abs( Vector2.Distance(a.position, b.position));
        }
    }
}
