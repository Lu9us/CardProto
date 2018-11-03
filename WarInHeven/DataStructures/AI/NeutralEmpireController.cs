using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib.Client.System;
using GameLib.Client.System.GraphicsHandlers;
using GameLib.Server;
using WarInHeven.DataStructures.Interfaces;

namespace WarInHeven.DataStructures.AI
{
    public class NeutralEmpireController : EmpireController
    {
        private Empire empire;
        private int empCOunt = 0;
        public NeutralEmpireController(Empire empire) : base(empire)
        {
           this.empire = empire;
        }

        public override void Update(GameState gs)
        {
            StarMap worldState = gs.varTable.GetItem<StarMap>("world");
            if (worldState.empires.Where(a => a.active).Count() < 5)
            {
                Star startingPoint = worldState.list[RandomHelper.getRandomInt(0, worldState.list.Count)];
                Empire parent = worldState.empires.First(a => a.planets.Contains(startingPoint));
                parent.planets.Remove(startingPoint);
                Empire newEmpire = new Empire();
                newEmpire.name = "empire " + empCOunt;
                newEmpire.parent = parent;
                parent.children.Add(newEmpire);
                newEmpire.planets.Add(startingPoint);
                newEmpire.controller = new AIEmpireController(newEmpire);
                newEmpire.color = GraphicsHelper.getRandomColor();
                startingPoint.color = newEmpire.color;
                worldState.addList.Add(newEmpire);
                empCOunt++;

                
            }
           
        }
    }
}
