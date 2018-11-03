using GameLib.Client.System.GraphicsHandlers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public List<Star> planets = new List<Star>();
        public bool active = true;
        public Empire()
        {
            name = "";
            color = GraphicsHelper.getRandomColor();
        }
    }
}
