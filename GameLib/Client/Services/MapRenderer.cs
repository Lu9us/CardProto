using CardProto.System;
using GameLib.Client.System.GraphicsHandler;
using GameLib.DataStructures.Interface;
using GameLib.Server;
using GameLib.Unified.Services.MapService;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Client.Services
{
    public class MapRenderer : Renderable
    {
        List<Tile> tileData;
        TileMap map;
        public MapRenderer(List<Tile> data, TileMap tileMap)
        {
            layer = 1;
            tileData = data;
            map = tileMap;
        }

        public override void Render(TextureAtlas atlas, SpriteBatch batch,RenderCallHelper renderCallHelper)
        {
            for (int x = 0; x < map.tileMap.GetLength(1); x++)
            {
                for (int y = 0; y < map.tileMap.GetLength(1); y++)
                {
                    renderCallHelper.Draw(atlas.GetTextureData(tileData[map.tileMap[x,y]].filePath),x,y,30);
                }
            }
            
        }
    }
}
