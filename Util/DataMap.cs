using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
   
    

    [Serializable]
   public class DataMap
    {
        [JsonProperty()]
        internal List<string> keys = new List<string>();
        [JsonProperty()]
        internal List<object> data = new List<object>();
        [JsonProperty()]
        internal List<string> typeNames = new List<string>();


        public void AddData(string key, object value)
        {
            if (!keys.Contains(key))
            {
                keys.Add(key);
                data.Add(value);
                typeNames.Add(value.GetType().FullName);
            }
            else
            {
                data[keys.IndexOf(key)] = value;
            }
        }

        public object GetData(string key)
        {
            if (keys.Contains(key))
            {
              return data[keys.IndexOf(key)];
            }
            return null;
        }
    }
}
