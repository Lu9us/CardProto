using CardProto.System;
using GameLib.Client.System.GraphicsHandler;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Unified.GameObject
{
    public class GameObjectRenderer : Renderable
    {
        Dictionary<Guid, GameObject> renderingData;
       public GameObjectRenderer(Dictionary<Guid, GameObject> renderingData)
        {
            this.renderingData = renderingData;
            this.layer = 4;
        }
        public override void Render(TextureAtlas atlas, SpriteBatch batch, RenderCallHelper helper)
        {
            foreach (GameObject g in renderingData.Values)
            {

                helper.Draw(atlas.GetTextureData(g.spriteList[0].Key),(int)g.pos.X, (int)g.pos.Y,0);
            }
        }
    }
}
