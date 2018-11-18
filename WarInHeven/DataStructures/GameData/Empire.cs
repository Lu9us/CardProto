using GameLib.Client.System.GraphicsHandlers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarInHeven.DataStructures.GameData;
using WarInHeven.DataStructures.Interfaces;

namespace WarInHeven
{
    public class Empire
    {
        public Guid ID = Guid.NewGuid();
        public string name;
        public Empire parent;
        public List<Empire> children = new List<Empire>();
        public IController controller;
        public Color color;
        public double money;
        public List<Star> planets = new List<Star>();
        public bool active = true;
        public List<Fleet> fleets = new List<Fleet>();


        public Empire()
        {
            name = ID.ToString();
            color = GraphicsHelper.getRandomColor();
        }

        public bool isCapital(Star star)
        {
            if(planets.Count > 0)
            {
                return planets[0].id == star.id;
            }
            return false;
        }

        public void update(StarMap sm)
        {
            foreach (Star star in planets)
            {
                money += star.baseWealthRate * (star.population / 2);
            }
        }
    }
}
