﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
   
    

    [Serializable]
   public class DataMap
    {
       internal List<string> keys = new List<string>();
       internal List<object> data = new List<object>();

        public void AddData(string key, object value)
        {
            if (!keys.Contains(key))
            {
                keys.Add(key);
                data.Add(value);
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
