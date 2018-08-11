using GameLib.DataStructures.Interface;
using GameLib.Server;
using GameLib.Unified.Services.MapService;
using LD42.Services.Renderers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace LD42.Services
{
    public class ImprovementService : IService, DataInterface
    {
        List<Improvement> improvments = new List<Improvement>();
        GameState gs;
        TileMap tm;

        public void CreateImprovement(Improvement item)
        {
            improvments.Add(item);
        }

        public void OnClose()
        {
           
        }

        public void OnReciveMessage(object message)
        {
           
        }

        public void OnStart(GameState s)
        {
            gs = s;
            ImprovementRenderer renderer = new ImprovementRenderer(improvments);  
        }

        public void OnUpdate()
        {
            if (tm == null)
            {
                try
                {
                    tm = gs.varTable.GetItem<TileMap>("map");
                }
                catch (Exception e)
                {

                }

            }
            else
            { 
                foreach (Improvement i in improvments)
                {
                    i.Update(gs);
                }
            }
          
        }

        public void Update(object data, DataMap source)
        {
         
        }
    }
}
