using CardProto.System.UI;
using GameLib.Client.System.GraphicsHandler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Client.UI
{
    public class Label : UIRenderable
    {
        Vector2 position;
        string value ="";
        Color c = Color.Black;
        public Label(Vector2 position)
        {
            this.position = position;
        }
        public void Update(string value)
        {
            this.value = value;
        }
        public void UpdateColor(Color c)
        {
            this.c = c;
        }
        public override void Render(TextureAtlas atlas, SpriteBatch batch, RenderCallHelper helper)
        {
            batch.DrawString(atlas.GetFont("Font\\Game"), value, position,c);
        }
    }
}
