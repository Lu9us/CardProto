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
using WarInHeven.DataStructures.GameData;
using WarInHeven.DataStructures.Rendering;

namespace WarInHeven
{
    public class StarMap : IService, DataInterface
    {
     
        GameState gs;
     public List<Empire> deleteList = new List<Empire>();
     public List<Empire> addList = new List<Empire>();
     public List<Star> list = new List<Star>();
     public List<Empire> empires = new List<Empire>();
        public List<Fleet> fleets = new List<Fleet>();
        long lastRuntime = 0;
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
                star.name = star.id.ToString();
                star.position = new Microsoft.Xna.Framework.Vector2(RandomHelper.getRandomInt(0,10000),RandomHelper.getRandomInt(0,10000));
                star.color = e.color;
                star.neigbours = new Star[RandomHelper.getRandomInt(2, 9)];
                star.empire = e;
                list.Add(star);
                e.planets.Add(star);

                
            }
          
            foreach (Star star in list)
            {
                List<Star> copyList = new List<Star>(list);
                copyList.Sort((v2, v1) => Math.Abs( Vector2.Distance(v1.position, star.position)).CompareTo(Math.Abs( Vector2.Distance(v2.position, star.position))) );
                copyList.Reverse();

                for (int i = 1; i < star.neigbours.Length; i++)
                {
                    if ( i < list.Count)
                    {
                        star.neigbours[i] = copyList[i];
                    }
                }
                List<Star> data = star.neigbours.ToList();
                data.RemoveAll(item => item == null);
                star.neigbours = data.ToArray();
            }

          

            StarRendering starRendering = new StarRendering(s,list);
            FleetRendering fleetRendering = new FleetRendering(this);
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
            if (gs.runtime > lastRuntime)
            {
                foreach (Fleet fleet in fleets)
                {
                    fleet.controller.Update(gs);
                }

                foreach (Empire e in empires)
                {
                    e.update(this);
                }
                foreach (Star s in list)
                {
                    s.update(this);
                }

                lastRuntime = gs.runtime;
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

        public void MakeNewFleet(Empire e, Star planet)
        {
            Fleet f = new Fleet();
            f.name = "fleet";
            f.owner = e;
            e.fleets.Add(f);
            f.position = planet;
            fleets.Add(f);
            f.controller = new FleetController(f);
            }

        public void SetPlanetToEmpire(Empire empire, Star planet)
        {
        
            planet.empire.planets.Remove(planet);
            planet.color = empire.color;
            planet.empire = empire;
            empire.planets.Add(planet);

        }

        public void MakeNewEmpire(Star planet)
        {
            Empire empire = new Empire();
            empire.parent = planet.empire;
            empire.controller = new AIEmpireController(empire);
            planet.empire.children.Add(empire);
            addList.Add(empire);
            SetPlanetToEmpire(empire, planet);
            
        }

        public void Update(object data, DataMap source)
        {
        
        }
    }
}
