using GameLib.DataStructures.Interface;
using GameLib.Server;
using GameLib.Server.Services;
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
        ResourceService rs;

        public void CreateImprovement(Improvement item)
        {
            if (rs != null)
            {
                if (rs.getScore() >= item.cost)
                {
                    if (!improvments.Any(cs => cs.position == item.position))
                    {
                        improvments.Add(item);
                        rs.DecreasePoints(item.cost, "improvment spawned at " + item.position);
                    }
                }
            }
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
            if (rs == null)
            {
                rs = ((ResourceService)ServiceController.runningServices[typeof(ResourceService).FullName]);
            }

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
