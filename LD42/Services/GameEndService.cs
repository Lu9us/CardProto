using GameLib.Client.UI;
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
        public void OnClose()
        {
            
        }

        public void GameOver()
        {
            label = new Label(new Microsoft.Xna.Framework.Vector2(250,200));
            label.Update("GAME OVER");
            developer = new Label(new Microsoft.Xna.Framework.Vector2(150, 220));
            developer.Update("Developed by Anthony Emberson");
            score = new Label(new Microsoft.Xna.Framework.Vector2(170, 240));
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
