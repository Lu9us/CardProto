using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD42.Services.Crops
{
    public enum State
    {
        Failed,
        Succeded,
        Growing

    }


   public class Crop
    {
        public Vector2 location;
        public State state;
        public int age = 0;
    }
}
