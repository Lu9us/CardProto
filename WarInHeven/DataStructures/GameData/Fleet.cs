using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib.Client.UI.Clickable;
using Microsoft.Xna.Framework;
using WarInHeven.DataStructures.AI;
using WarInHeven.DataStructures.Interfaces;
using WarInHeven.DataStructures.UI;

namespace WarInHeven.DataStructures.GameData
{
    public class Fleet : IClickable,AITarget
    {
        public Guid id = Guid.NewGuid();
        public String name;
        public Empire owner;
        public Star position;
        public FleetController controller;
        public int strength;

       public Fleet()
        {
            screenSpace = false;
            InfoPanel.infoPanel.labels[0].Update("oh hi");
        }

        public Star getLocation()
        {
            return position;
        }

        public override void LeftClick()
        {
            InfoPanel.infoPanel.labels[0].Update(name);
            InfoPanel.infoPanel.labels[1].Update(owner.name);
            InfoPanel.infoPanel.labels[2].Update(strength.ToString());
            InfoPanel.infoPanel.labels[3].Update(position.ToString());

        }
        public override void RightClick()
        {
          
        }

        public override void UiUpdate()
        {
            this.hitBox = new Microsoft.Xna.Framework.Rectangle(position.position.ToPoint() + new Point(25, 0), new Point(25, 25));
        }
    }
}
