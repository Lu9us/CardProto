﻿using GameLib.Client.UI;
using GameLib.DataStructures.Interface;
using GameLib.Server;
using LD42.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD42
{
    public class GameEndService : IService
    {


        InteractionService interactionService;
        MovementService movementService;
        ResourceService resourceService;
        Label label;
        Label developer;
        Label score;
        GameState gs;
        public void OnClose()
        {
            
        }

        public void GameOver()
        {
            gs.soundEffects.playSound("SoundEffects/death");
            label = new Label(new Microsoft.Xna.Framework.Vector2(320,200));
            label.Update("GAME OVER");
            score = new Label(new Microsoft.Xna.Framework.Vector2(340, 240));
            score.Update("Score: "+resourceService.getScore());
            GameLib.Server.Services.ServiceController.runningServices.Remove(typeof(InteractionService).FullName);
            interactionService.OnClose();
            GameLib.Server.Services.ServiceController.runningServices.Remove(typeof(MovementService).FullName);
            movementService.OnClose();
            resourceService.OnClose();
        }

        public void OnReciveMessage(object message)
        {
           
        }

        public void OnStart(GameState s)
        {
            gs = s;
        }

        public void OnUpdate()
        {

            if (interactionService == null)
            {
                interactionService = ((InteractionService)GameLib.Server.Services.ServiceController.runningServices[typeof(InteractionService).FullName]);
            }
            if (resourceService == null)
            {
                resourceService = ((ResourceService)GameLib.Server.Services.ServiceController.runningServices[typeof(ResourceService).FullName]);
            }
            if (movementService == null)
            {
                movementService = ((MovementService)GameLib.Server.Services.ServiceController.runningServices[typeof(MovementService).FullName]);
            }
        }
    }
}