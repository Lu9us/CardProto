using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna;
using Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CardProto.System.UI
{
    public class NetworkUpdatableString : UIRenderable,Util.DataInterface
    {
        string val = "";
        public NetworkUpdatableString(DataMapManager mg,string tag)
        {
            mg.AddClient(tag,this);
        }

        public override void Render(SpriteBatch batch,ContentManager cm)
        {
            try
            {
                string[] data = val.Split('|');
                int i = 0;
                foreach (string sd in data)
                {
                    batch.DrawString(cm.Load<SpriteFont>("Font\\Console"), sd, new Microsoft.Xna.Framework.Vector2(x, y + i + 5), Color.Black);
                    i += 15;
                }
            }
            catch (Exception e)
            {
                batch.DrawString(cm.Load<SpriteFont>("Font\\Console"), "Rendering failed for "+ToString(), new Microsoft.Xna.Framework.Vector2(x, y),Color.Red);
            }
        }

        public void Reset()
        {
            val = "";
        }
        public void Update(object data, DataMap source)
        {
            val += "|" + data;
        }
    }
}
