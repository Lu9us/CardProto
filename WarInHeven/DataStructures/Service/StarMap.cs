using GameLib.Client.System;
using GameLib.Client.System.GraphicsHandlers;
using GameLib.DataStructures.Interface;
using GameLib.Server;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using WarInHeven.DataStructures.AI;

namespace WarInHeven
{
    public class StarMap : IService, DataInterface
    {
     
        GameState gs;
     public List<Empire> deleteList = new List<Empire>();
     public List<Empire> addList = new List<Empire>();
     public List<Star> list = new List<Star>();
     public List<Empire> empires = new List<Empire>();

        public void OnClose()
        {
         
        }
        public bool isNeutral(Empire e)
        {
            return e.name == "neutral";
        }
        public void OnReciveMessage(object message)
        {
        
        }

        public void OnStart(GameState s)
        {
            gs = s;
            gs.camera.zoom = 0.3f;
            gs.camera.pos = new Microsoft.Xna.Framework.Vector2(500, 500);
            Empire e = new Empire();
            e.name = "neutral";
            e.color = Color.Black;
            e.controller = new NeutralEmpireController(e);
            empires.Add(e);
        
            for (int i = 0; i < 1000; i++)
            {
                Star star = new Star();
                star.name = "oh hi mark";
                star.position = new Microsoft.Xna.Framework.Vector2(RandomHelper.getRandomInt(0,10000),RandomHelper.getRandomInt(0,10000));
                star.color = e.color;
                star.neigbours = new Star[RandomHelper.getRandomInt(2, 9)];
                list.Add(star);
                e.planets.Add(star);
                
            }
          
            foreach (Star star in list)
            {
                List<Star> copyList = new List<Star>(list);
                copyList.Sort((v2, v1) => Vector2.Distance(v1.position, star.position).CompareTo(Vector2.Distance(v2.position, star.position)));
                copyList.Reverse();

                for (int i = 1; i < star.neigbours.Length; i++)
                {
                    if ( i < list.Count)
                    {
                        star.neigbours[i] = copyList[i];
                    }
                }
            }
            StarRendering starRendering = new StarRendering(s,list);
            gs.varTable.AddItem("world", this);
        }

        public void OnUpdate()
        {
            foreach (Empire empire in empires)
            {
                if (empire.controller != null)
                {
                    empire.controller.Update(gs);
                }
            }
            foreach (Empire e in addList)
            {
                empires.Add(e);
            }
            addList.Clear();
            foreach (Empire e in deleteList)
            {
                empires.Remove(e);
            }
            deleteList.Clear();
        
        }

        public void Update(object data, DataMap source)
        {
        
        }
    }
}
