using GameLib.DataStructures.Interface;
using GameLib.Server;
using GameLib.Server.Services;
using GameLib.Unified.GameObject;
using GameLib.Unified.Services.MapService;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace LD42
{
    public enum Facing
    {
        NORTH = 0,
        EAST  = 1,
        SOUTH = 2,
        WEST = 3
    }

    




    public class MovementService : IService, DataInterface
    {
        private GameState gs;
        private Keys[] thisState;
        private Keys[] lastState;
        private Player player;
        private GameObjectService service;
        public List<Vector2> facingToVector = new List<Vector2>();
        public static List<Vector2> globalFacingToVector;

        public void OnClose()
        {
           
        }

        public void OnReciveMessage(object message)
        {
            
        }

        public void OnStart(GameState s)
        {
            gs = s;
            gs.dataManager.AddClient("input", this);
            facingToVector = new List<Vector2>()
            {
                new Vector2(0,-1),
                new Vector2(-1,0),
                new Vector2(0,1),
                 new Vector2(1,0)
            };
            globalFacingToVector = facingToVector;
        }

        public void OnUpdate()
        {
            if (service == null)
            {
                service = ((GameObjectService)ServiceController.runningServices[typeof(GameObjectService).FullName]);
            }
            if (player == null)
            {
                player = gs.players[0];
                player.varTable.AddItem("facing", Facing.SOUTH);
            }
            if (thisState != null)
            {
                lastState = thisState;
            }
            
        }

        public void Update(object data, DataMap source)
        {
            thisState = (Keys[])data;
            int[,] tileMap = gs.varTable.GetItem<TileMap>("map").tileMap;
         
            try
            {
                GameObject playerGo = service.GetGameObject(player.playerGOID);
                Vector2 lastPos = playerGo.pos;
                if (lastState == null)
                {
                    lastState = new Keys[0];
                }
                if (thisState.Contains(Keys.W) && !lastState.Contains(Keys.W))
                {
                    if (playerGo.pos.Y > 0)
                    {
                        playerGo.pos.Y--;
                        player.varTable.SetItem("facing", Facing.NORTH);
                    }
                }
                if (thisState.Contains(Keys.A) && !lastState.Contains(Keys.A))
                {
                    if (playerGo.pos.X > 0 )
                    {
                        playerGo.pos.X--;
                        player.varTable.SetItem("facing", Facing.EAST);
                    }

                }
                if (thisState.Contains(Keys.D) && !lastState.Contains(Keys.D))
                {
                    if (playerGo.pos.X < tileMap.GetLength(0))
                    {
                        playerGo.pos.X++;
                        player.varTable.SetItem("facing", Facing.WEST);
                    }

                }
                if (thisState.Contains(Keys.S) && !lastState.Contains(Keys.S))
                {
                    if (playerGo.pos.Y < tileMap.GetLength(1))
                    {
                        playerGo.pos.Y++;
                        player.varTable.SetItem("facing", Facing.SOUTH);
                    }
                }

            }
            catch (Exception e)
            {


            }

        }
    }
}
