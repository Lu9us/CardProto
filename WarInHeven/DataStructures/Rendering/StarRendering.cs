using CardProto.System;
using GameLib.Client.System.GraphicsHandler;
using GameLib.Server;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarInHeven
{
   public class StarRendering : Renderable
    {
        private GameState gs;
        List<Star> starList = new List<Star>();

        public StarRendering(GameState gs,List<Star> starMap)
        {
            starList = starMap;
            this.gs = gs;
        }

        public override void Render(TextureAtlas atlas, SpriteBatch batch, RenderCallHelper helper)
        {
            
            foreach (Star s in starList)
            {
                
                helper.Draw(atlas.GetTextureData("star"), (int)s.position.X, (int)s.position.Y, this.layer,s.color);
                helper.DrawString("Font\\Game",s.name, (int)s.position.X+40, (int)s.position.Y+10, this.layer, s.color);
                helper.DrawString("Font\\Game", s.population.ToString(), (int)s.position.X + 40, (int)s.position.Y + 20, this.layer, s.color);
                helper.DrawString("Font\\Game", s.baseWealthRate.ToString(), (int)s.position.X + 40, (int)s.position.Y + 35, this.layer, s.color);
                helper.DrawString("Font\\Game", s.resistance.ToString(), (int)s.position.X + 40, (int)s.position.Y + 45, this.layer, s.color);
                foreach (Star sn in s.neigbours)
                {
                    if (sn != null)
                    {
                        helper.DrawLine(atlas.GetTextureData("line"), s.position, sn.position,sn.color,10);
                    }
                }
            }
        }
    }
}  
