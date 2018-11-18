using GameLib.Client.UI;
using GameLib.DataStructures.Interface;
using GameLib.Server;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using WarInHeven.DataStructures.AI;

namespace WarInHeven.DataStructures
{
    public class ControllerService : IService, DataInterface
    {
        GameState gs;
        MouseState lastState;

        Label[] starData = new Label[5];

        public void OnClose()
        {

        }

        public void OnReciveMessage(object message)
        {

        }

        public void OnStart(GameState s)
        {
            gs = s;
            for (int i = 0; i < 5; i++)
            {
                starData[i] = new Label(new Vector2(5, 10 * i));
            }

            gs.dataManager.AddClient("input", this);
        }

        public void OnUpdate()
        {

        }

        public void Update(object data, DataMap source)
        {
            starData[2].Update(gs.runtime.ToString());
            StarMap world = gs.varTable.GetItem<StarMap>("world");
            if (data is Keys[])
            {
                Keys[] keys = (Keys[])data;
                if (keys.Contains(Keys.W))
                {
                    gs.camera.pos = new Microsoft.Xna.Framework.Vector2(gs.camera.pos.X, gs.camera.pos.Y - 10);
                }
                if (keys.Contains(Keys.S))
                {
                    gs.camera.pos = new Microsoft.Xna.Framework.Vector2(gs.camera.pos.X, gs.camera.pos.Y + 10);
                }
                if (keys.Contains(Keys.D))
                {
                    gs.camera.pos = new Microsoft.Xna.Framework.Vector2(gs.camera.pos.X + 10, gs.camera.pos.Y);
                }
                if (keys.Contains(Keys.A))
                {
                    gs.camera.pos = new Microsoft.Xna.Framework.Vector2(gs.camera.pos.X - 10, gs.camera.pos.Y);
                }
                if(keys.Contains(Keys.Q))
                {
                    gs.camera.zoom += 0.01f;
                }
                if (keys.Contains(Keys.E))
                {
                    gs.camera.zoom -= 0.01f;
                }
                if (keys.Contains(Keys.Tab))
                {
                    FleetController.AiDebug = !FleetController.AiDebug;
                }

            }
            if (data is MouseState)
            {
                
                MouseState md = (MouseState)data;
                Vector2 mp = gs.camera.mouseToWorld(new Microsoft.Xna.Framework.Vector2(md.X, md.Y));
                foreach (Star s in world.list)
                {
                    if (new Rectangle(s.position.ToPoint(), new Point((int)(100*gs.camera.zoom))).Contains(mp))
                    {
                        starData[0].Update(s.name);
                        starData[1].Update(world.empires.First(a => a.planets.Contains(s)).name);
                     
                    }

                }
                if (lastState != null)
                {
                    if (md.ScrollWheelValue > lastState.ScrollWheelValue)
                    {
                        gs.camera.zoom -= 0.01f;
                    }
                    if (md.ScrollWheelValue < lastState.ScrollWheelValue)
                    {
                        gs.camera.zoom += 0.01f;
                    }

                }
                lastState = md;

            }
        }
    }
}
