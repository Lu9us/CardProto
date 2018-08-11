using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Client.System.GraphicsHandler
{
   public class RenderCallHelper
    {
        SpriteBatch sb;
        TextureAtlas atlas;
        public RenderCallHelper(SpriteBatch sb,TextureAtlas atlas)
        {
            this.sb = sb;
            this.atlas = atlas;
        }

        public void Draw(TextureAlias ts,int x,int y,int layer )
        {
            if (ts.w == -1 || ts.h == -1)
            {
                sb.Draw(atlas.GetTexture(ts.resource), new Vector2(x, y), Color.White);
            }
            else
            {
                sb.Draw(atlas.GetTexture(ts.resource), new Vector2(x*ts.offsetX, y*ts.offsetY),new Rectangle(ts.x,ts.y,ts.w,ts.h), Color.White);
            }

        }

    }
}
