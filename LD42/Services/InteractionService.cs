﻿using GameLib.DataStructures.Interface;
using GameLib.Server;
using GameLib.Unified.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace LD42.Services
{
    public class InteractionService : IService, DataInterface
    {
        GameState gs;
        private Keys[] thisState = new Keys[0];
        private Keys[] lastState = new Keys[0];
        private Player player;
        private GameObjectService service;
        private WeatherService weather;
        private ImprovementService improvements;
        private CropService crops;
        public void OnClose()
        {
            gs.dataManager.RemoveClient(this);
        }

        public void OnReciveMessage(object message)
        {
           
        }

        public void OnStart(GameState s)
        {
            gs = s;
            gs.dataManager.AddClient("input", this);
        }

        public void OnUpdate()
        {
            if (service == null)
            {
                service = ((GameObjectService)GameLib.Server.Services.ServiceController.runningServices[typeof(GameObjectService).FullName]);
            }
            if (improvements == null)
            {
                improvements = ((ImprovementService)GameLib.Server.Services.ServiceController.runningServices[typeof(ImprovementService).FullName]);
            }
            if (crops == null)
            {
                crops = ((CropService)GameLib.Server.Services.ServiceController.runningServices[typeof(CropService).FullName]);
            }
            if (player == null)
            {
                player = gs.players[0];
            }
            if (thisState != null)
            {
                lastState = thisState;
            }

        }

        public void Update(object data, DataMap source)
        {
            try
            {
                thisState = (Keys[])data;
                weather = gs.varTable.GetItem<WeatherService>("weather");
                GameObject playerGo = service.GetGameObject(player.playerGOID);
                Vector2 facing = MovementService.globalFacingToVector[(int)player.varTable.GetItem<Facing>("facing")];
                Vector2 pos = playerGo.pos + facing;
                if (pos.X >= 0 && pos.Y >= 0 && pos.Y < weather.rainMap.GetLength(0) && pos.Y < weather.rainMap.GetLength(1))
                {

                    if (thisState.Contains(Keys.X) && !lastState.Contains(Keys.X))
                    {
                        Drain d = new Drain();
                        d.position = pos;
                        improvements.CreateImprovement(d);
                    }
                    if (thisState.Contains(Keys.Z) && !lastState.Contains(Keys.Z))
                    {
                        Heater d = new Heater();
                        d.position = pos;
                        improvements.CreateImprovement(d);
                    }
                    if (thisState.Contains(Keys.C) && !lastState.Contains(Keys.C))
                    {
                        Aqufier d = new Aqufier();
                        d.position = pos;
                        improvements.CreateImprovement(d);

                    }
                    if (thisState.Contains(Keys.V) && !lastState.Contains(Keys.V))
                    {
                        Crops.Crop c = new Crops.Crop();
                        c.location = pos;
                        c.state = Crops.State.Growing;
                        crops.AddCrop(c);

                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}