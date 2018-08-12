using GameLib.DataStructures.Interface;
using GameLib.Server;
using GameLib.Server.Services;
using GameLib.Unified.GameObject;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace GameLib.Unified.Services
{
    public class PlayerService : IService, DataInterface
    {
        public List<Player> playerData = new List<Player>();
        GameState gs;
        GameObjectService service;
        public void OnClose()
        {
          
        }

        public void OnReciveMessage(object message)
        {
         
        }

        public void OnStart(GameState s)
        {
            gs = s;
            playerData = gs.players;
        }

        public void OnUpdate()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            if (service == null)
            {
                service = ((GameObject.GameObjectService)ServiceController.runningServices[typeof(GameObject.GameObjectService).FullName]);
            }

            foreach (Player player in playerData)
            {
                if (player.playerGOID == Guid.Empty || !gs.varTable.GetItem<Dictionary<Guid, GameObject.GameObject>>("GameObjects").ContainsKey(player.playerGOID))
                {
                    Guid g = Guid.NewGuid();
                    GameObjectRequest r = new GameObjectRequest();
                    r.eventType = GameObjectEvent.CREATE;
                    r.ID = g;
                    r.name = "Player";
                    r.pos = Vector2.Zero + new Vector2(rand.Next(99), rand.Next(99));
                    player.playerGOID = g;
                    service.AddGameObjectRequest(r);
                }

            }
        }

        public void Update(object data, DataMap source)
        {
            throw new NotImplementedException();
        }
    }
}
