using CardProto.System;
using GameLib.Client.System.GraphicsHandler;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarInHeven.DataStructures.GameData;

namespace WarInHeven.DataStructures.Rendering
{
    public class FleetRendering : Renderable
    {
        StarMap map;

        public FleetRendering(StarMap map)
        {
            this.map = map;
        }

        public override void Render(TextureAtlas atlas, SpriteBatch batch, RenderCallHelper helper)
        {
            foreach (Fleet f in map.fleets)
            {
                helper.Draw(atlas.GetTextureData("fleet"), (int)f.position.position.X + 25, (int)f.position.position.Y, layer, f.owner.color);
            }
        }
    }
}
