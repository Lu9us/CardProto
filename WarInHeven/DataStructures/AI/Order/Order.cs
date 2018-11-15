using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarInHeven.DataStructures.GameData;

namespace WarInHeven.DataStructures.AI.Orders
{
    public enum OrderType
    {
        MoveTo,
        Invade,
        Attack
    }
   public class Order
    {
      public OrderType type;
      public AITarget target;
      public bool beingDone = false;
      public Fleet actor;
      public int Priority;
    }
}
