using GameLib.DataStructures.Interface;
using GameLib.Server;
using GameLib.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace LD42
{
    public class InventoryService : IService, Util.DataInterface
    {
        GameState state;

        public void OnClose()
        {
            
        }

        public void OnReciveMessage(object message)
        {
            
        }

        public void OnStart(GameState s)
        {
            state = s;
            if (ServiceController.getRuntime() == Runtime.HYBRID)
            {

            }
          
        }

        public void OnUpdate()
        {
            
        }

        public void Update(object data, DataMap source)
        {
           
        }
    }
}
