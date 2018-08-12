using GameLib.DataStructures;
using GameLib.DataStructures.Implementations;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Unified.GameObject
{
    public class GameObjectTemplate
    {
        public string name;
        public HashVarTable vars;
        public List<KeyValuePair<string, Vector2>> spriteList = new List<KeyValuePair<string, Vector2>>();
    }
}
