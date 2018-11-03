using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Client.System.GraphicsHandlers
{
   public static class GraphicsHelper
    {
        public static Color getRandomColor()
        {
            return new Color((byte)RandomHelper.getRandomInt(0, 256), (byte)RandomHelper.getRandomInt(0, 256), (byte)RandomHelper.getRandomInt(0, 256));
        }
    }
}
