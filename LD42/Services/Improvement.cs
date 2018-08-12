using GameLib.Server;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD42.Services
{
  public abstract class Improvement
    {
      public Guid ID;
      public  Vector2 position;
      public string sprite;
      public int cost;
      public abstract void Update(GameState gs);
    }
}
