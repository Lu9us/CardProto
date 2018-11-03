using GameLib.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarInHeven.DataStructures.Interfaces
{
    public abstract class EmpireController : IController
    {
        public Empire empire;
        public EmpireController(Empire empire)
        {
            this.empire = empire;
        }
        public abstract void Update(GameState gs);

    }
}
