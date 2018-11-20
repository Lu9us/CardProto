using System;
using System.Collections.Generic;
using System.Linq;
using GameLib.DataStructures.Interface;
using GameLib.Server;
using Microsoft.Xna.Framework.Input;
using Util;

namespace GameLib.Client.UI.Clickable
{
    public class ClickableService : IService,DataInterface
    {
        List<IClickable> clickables = new List<IClickable>();
        GameState gameState;
        public void registerObject(IClickable obj)
        {
            clickables.Add(obj);
        }
        public void deRegisterObject(IClickable obj)
        {
            clickables.Remove(obj);
        }


        public ClickableService()
        {

        }

        public void OnClose()
        {
           
        }

        public void OnReciveMessage(object message)
        {
         
        }

        public void OnStart(GameState s)
        {
            s.dataManager.AddClient("input",this);
            IClickable.service = this;
            gameState = s;
        }

        public void OnUpdate()
        {
          
        }

        public void Update(object data, DataMap source)
        {
            
            if (data is MouseState)
            {
                MouseState md = (MouseState)data;
                if (md.LeftButton == ButtonState.Pressed || md.RightButton == ButtonState.Pressed)
                {
                    List<IClickable> enabled = clickables.Where(a => a.enabled).ToList();
                    foreach (IClickable element in enabled.Where(a => a.screenSpace))
                    {
                        element.UiUpdate();
                        if (element.hitBox.Contains(md.Position))
                        {
                            if (md.LeftButton == ButtonState.Pressed)
                            {
                                element.LeftClick();
                                return;
                            }
                            else
                            {
                                element.RightClick();
                                 return;
                            }
                        }
                    }
                    foreach (IClickable element in enabled.Where(a => !a.screenSpace))
                    {
                        element.UiUpdate();
                        if (element.hitBox.Contains(gameState.camera.mouseToWorld(md.Position.ToVector2())))
                        {
                            if (md.LeftButton == ButtonState.Pressed)
                            {
                                element.LeftClick();
                                return;
                            }
                            else
                            {
                                element.RightClick();
                                return;
                            }
                        }
                    }
                }

            }
           
        }
    }
}
