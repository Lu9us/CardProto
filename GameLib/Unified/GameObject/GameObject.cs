using GameLib.DataStructures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Unified.GameObject
{
    public class GameObject
    {
       public Guid ID;
       public string name;
       public Vector2 pos;
       public IExtendedVarTable vars;
       public List<KeyValuePair<string, Vector2>> spriteList = new List<KeyValuePair<string, Vector2>>();
    }
}
