using GameLib.DataStructures.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Server.Services
{
    public enum Runtime
    {
        CLIENT,
        SERVER
    }
   public static class ServiceController
    {
        internal static readonly Dictionary<string, IService> runningServices = new Dictionary<string, IService>();
        private static GameState gameState;
        private static Runtime systemRuntime;

        public static void LoadGameState(GameState state)
        {
            gameState = state;
        }
        public static void setRuntime(Runtime rt)
        {
            systemRuntime = rt;
        }
        public static Runtime getRuntime()
        {
            return systemRuntime;
        }

        public static void StartNewService(IService s)
        {
            s.OnStart(gameState);
            runningServices.Add(s.GetType().ToString(), s);
        }

        public static void SendInterServiceMessage(string serviceName, object message)
        {
            if (runningServices.ContainsKey(serviceName))
            {
                runningServices[serviceName].OnReciveMessage(message);
            }

        }

        public static void RunServices()
        {
            foreach (IService s in runningServices.Values)
            {
                s.OnUpdate();
            }

        }

    }
}
