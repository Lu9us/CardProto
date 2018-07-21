using GameLib.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.DataStructures.Interface
{
   public interface IService
    {
        void OnStart(GameState s);
        void OnUpdate();
        void OnClose();
        void OnReciveMessage(object message);
    }
}
