using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.DataStructures.Implementations
{
    public class HashVarTable : IExtendedVarTable
    {
        private Dictionary<string, object> dataTable = new Dictionary<string, object>();
        public void AddItem(string name, object data)
        {
            if (!dataTable.ContainsKey(name))
            {
                dataTable.Add(name, data);
            }
        }

        public bool ContainsItem(string name)
        {
            return dataTable.ContainsKey(name);
        }

        public bool DeleteItem(string name)
        {
           return dataTable.Remove(name);
        }

        public n GetItem<n>(string name)
        {
            return (n)dataTable[name];
        }

        public object GetItem(string name)
        {
            return dataTable[name];
        }

        public void SetItem(string name, object data)
        {
            if (ContainsItem(name))
            {
                dataTable[name] = data;
            }
        }
    }
}
