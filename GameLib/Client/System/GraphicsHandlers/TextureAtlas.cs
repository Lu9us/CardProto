using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Client.System.GraphicsHandler
{
   public class TextureAtlas
    {
        private Dictionary<string, TextureAlias> dataStrore;
        private Dictionary<string, Texture2D> textureStore;
        private Dictionary<string, SpriteFont> fontStore;
        private ContentManager cm;
       
        public TextureAtlas(ContentManager cm)
        {
            this.cm = cm;
            dataStrore = new Dictionary<string, TextureAlias>();
            textureStore = new Dictionary<string, Texture2D>();
            fontStore = new Dictionary<string, SpriteFont>();
        }

        public TextureAlias GetTextureData(string name)
        {
            try
            {
                return dataStrore[name];
            }
            catch
            {
                return null;
            }
        }


        public void CreateTexture(string fileName, string name, int x = 0, int y = 0, int w = -1, int h = -1,int oX = 0,int oY = 0)
        {
            Texture texture;

            if ((texture = GetTexture(fileName)) != null)
            {
                TextureAlias textureAlias = new TextureAlias();
                textureAlias.x = x;
                textureAlias.y = y;
                textureAlias.h = h;
                textureAlias.w = w;
                textureAlias.offsetX = oX;
                textureAlias.offsetY = oY;
                textureAlias.name = name;
                textureAlias.resource = fileName;
                dataStrore.Add(name, textureAlias);
            }
        }

        public void CreateTexture(TextureAlias data)
        {
           

            if ( GetTexture(data.resource) != null)
            {
                dataStrore.Add(data.name, data);
            }
            
        }

        internal Texture2D GetTexture(string name)
        {
            try
            {
                if (!textureStore.ContainsKey(name))
                {
                    textureStore.Add(name,cm.Load<Texture2D>(name));
                }
                return textureStore[name];
            }
            catch
            {
                return null;
            }
        }
        public SpriteFont GetFont(string name)
        {
            try
            {
                if (!fontStore.ContainsKey(name))
                {
                    fontStore.Add(name, cm.Load<SpriteFont>(name));
                }
                return fontStore[name];
            }
            catch
            {
                return null;
            }
        }

    }
}
