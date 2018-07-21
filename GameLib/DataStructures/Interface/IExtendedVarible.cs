using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.DataStructures
{
   public interface IExtendedVarTable
    {
        void AddItem(string name, object data);
        n GetItem<n>(string name);
        object GetItem(string name);
        bool ContainsItem(string name);
        void SetItem(string name, object data);
        bool DeleteItem(string name);

    }
}
