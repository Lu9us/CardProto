using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarInHeven.DataStructures.AI;
using WarInHeven.DataStructures.Interfaces;

namespace WarInHeven.DataStructures.GameData
{
   public class Fleet : AITarget
    {
        public Guid id = Guid.NewGuid();
        public String name;
        public Empire owner;
        public Star position;
        public FleetController controller;
        public int strength;
        public Star getLocation()
        {
            return position;
        }
    }
}
