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

        public void DrawString(string font,string text, int x, int y, int layer)
        {
            SpriteFont sf = atlas.GetFont(font);

            if (font != null)
            {
                sb.DrawString(sf, text, new Vector2(x, y), Color.White);
            }
        }
        public void DrawString(string font, string text, int x, int y, int layer,Color color)
        {
            SpriteFont sf = atlas.GetFont(font);

            if (font != null)
            {
                sb.DrawString(sf, text, new Vector2(x, y), color);
            }
        }
        public void Draw(TextureAlias ts,int x,int y,int layer)
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
        public void Draw(TextureAlias ts, int x, int y, int layer,Color color)
        {
            if (ts.w == -1 || ts.h == -1)
            {
                sb.Draw(atlas.GetTexture(ts.resource), new Vector2(x, y), color);
            }
            else
            {
                sb.Draw(atlas.GetTexture(ts.resource), new Vector2(x * ts.offsetX, y * ts.offsetY), new Rectangle(ts.x, ts.y, ts.w, ts.h), color);
            }

        }
        public void DrawLine(TextureAlias ts, Vector2 start, Vector2 end, int width = 1)
        {
            Texture2D texture = atlas.GetTexture(ts.resource);
            
            sb.Draw(texture, start, null, Color.White,
                        (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
                        new Vector2(0f, (float)texture.Height),
                        new Vector2(Vector2.Distance(start, end), 1f),
                        SpriteEffects.None, 0f);
        }
        public void DrawLine(TextureAlias ts, Vector2 start, Vector2 end,Color color, int width = 1)
        {
            Texture2D texture = atlas.GetTexture(ts.resource);

            sb.Draw(texture, start, null, color,
                        (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
                        new Vector2(0f, (float)texture.Height),
                        new Vector2(Vector2.Distance(start, end), 1f),
                        SpriteEffects.None, 0f);
        }
    }
}
