using CardProto.System;
using GameLib.Client.System.GraphicsHandler;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD42.Services.Renderers
{
  public class ImprovementRenderer:Renderable
    {
        List<Improvement> dataset;
        public ImprovementRenderer(List<Improvement> data)
        {
            dataset = data;
            layer = 3;
        }

        public override void Render(TextureAtlas atlas, SpriteBatch batch, RenderCallHelper helper)
        {
            foreach (Improvement data in dataset)
            {
                helper.Draw(atlas.GetTextureData(data.sprite), (int)data.position.X, (int)data.position.Y, 30);
            }
        }
    }
}
