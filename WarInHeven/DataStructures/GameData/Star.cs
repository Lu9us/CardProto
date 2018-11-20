using GameLib.AI.PathFinding;
using GameLib.Client.System;
using GameLib.Client.UI.Clickable;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarInHeven.DataStructures.AI;
using WarInHeven.DataStructures.UI;

namespace WarInHeven
{
   public class Star: IClickable,AITarget,PathFindingNode
    {
        public Guid id = Guid.NewGuid();
        public String name = "";
        public Vector2 position = new Vector2();
        public Star[] neigbours = new Star[5];
        public Color color;
        public Empire empire;
        public double baseWealthRate;
        public double population;
        public int inferstructure = 1;
        public double popUpdateRate = 0.5;
        public double resistance = 0;

        public Star()
        {
           baseWealthRate = RandomHelper.getRandomDouble(1, 5);
           population = RandomHelper.getRandomDouble(1,4);
            this.screenSpace = false;
        }

        public static int Sort(Star a, Star b)
        {
            return (int)Math.Abs( Vector2.Distance(a.position, b.position));
        }
        public static int Sort(PathFindingNode a, PathFindingNode b)
        {
            Star aCast = (Star)a;
            Star bCast = (Star)b;
            return (int)Math.Abs(Vector2.Distance(aCast.position, bCast.position));
        }

        public void update(StarMap map)
        {
            if (!map.isNeutral(empire))
            {
                if (population < inferstructure * popUpdateRate * 10)
                {
                    population += popUpdateRate * inferstructure;
                }

                if (!empire.isCapital(this))
                {
                    
                    if (inferstructure < 2)
                    {
                        resistance+=5;
                    }
                    if (resistance > 50)
                    {
                        if (RandomHelper.getRandomInt(max: 50) > 90)
                        {
                            map.MakeNewEmpire(this, true);
                            resistance = 0;
                        }
                    }
                }
            }
        }


        public Star getLocation()
        {
            return this;
        }

        public List<PathFindingNode> GetNeighbours()
        {
            List<PathFindingNode> nodes = new List<PathFindingNode>();
            foreach (Star s in neigbours)
            {
                nodes.Add(s);
            }
            return nodes;
        }

        public override void LeftClick()
        {
            InfoPanel.infoPanel.labels[0].Update(name);
            InfoPanel.infoPanel.labels[1].Update(empire.name);
            InfoPanel.infoPanel.labels[2].Update(position.ToString());
        }

        public override void RightClick()
        {
            throw new NotImplementedException();
        }

        public override void UiUpdate()
        {
            hitBox = new Rectangle(this.position.ToPoint(), new Point(25, 25));
        }
    }
}
