using CardProto.System;
using GameLib.Client.System.GraphicsHandler;
using LD42.Services.Crops;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD42.Services.Renderers
{
    public class CropRenderer : Renderable
    {
        List<Crop> crops;
        public CropRenderer(List<Crop> crops)
        {
            this.crops = crops;
            layer = 4;
        }

        public override void Render(TextureAtlas atlas, SpriteBatch batch, RenderCallHelper helper)
        {
            foreach (Crop data in  crops.Where(c => c.state == State.Growing))
            {
              
                    helper.Draw(atlas.GetTextureData("crop"), (int)data.location.X, (int)data.location.Y, 30);
                
            }
        }
    }
}
