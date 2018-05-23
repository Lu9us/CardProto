using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardProto.System
{
   public abstract class Renderable
    {
        public Renderable()
        {
            draw = true;
            Renderer.data.Add(this);
        }
        public int x;
        public int y;
        public int layer;
        public bool draw;
        public abstract void Render(SpriteBatch batch,ContentManager cm);
        public void Dispose()
        {
            Renderer.data.Remove(this);
        }
    }
}
