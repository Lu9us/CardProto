using GameLib.Client.System.GraphicsHandler;
using GameLib.Unified.Services.MapService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using Util.Interfaces;

namespace CardProto.System
{
  public class Renderer
    {
       public static List<Renderable> data = new List<Renderable>();
        private static ISerizilizer serizilizer = new JSONSerilizer();
        public static void LoadRenderingAliases(String file,TextureAtlas atlas)
        {
            if (!File.Exists(file))
            {
                List<TextureAlias> list = new List<TextureAlias>();
                list.Add(new TextureAlias());
                File.WriteAllBytes(file,serizilizer.Serilize<List<TextureAlias>>(list));
            }
            byte[] mapdata = File.ReadAllBytes(file);
            List<TextureAlias> textureList = serizilizer.DeSerilize<List<TextureAlias>>(mapdata);
            foreach(TextureAlias alias in textureList)
            {
                atlas.CreateTexture(alias);  
            }
        }


        public static int SortByLayer(Renderable a, Renderable b)
        {
            return b.layer - a.layer;
        }

    }
}
