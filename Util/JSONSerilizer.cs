using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Interfaces;
using Newtonsoft.Json;

namespace Util
{
    public class JSONSerilizer : ISerizilizer
    {
      
        public T DeSerilize<T>(byte[] data)
        {
            string sData = Encoding.ASCII.GetString(data);
            return JsonConvert.DeserializeObject<T>(sData);
        }

        public T DeSerilize<T>(string data)
        {
            throw new NotImplementedException();
        }

        public byte[] Serilize<T>(T data)
        {
         string sdata = JsonConvert.SerializeObject(data);
            return Encoding.ASCII.GetBytes(sdata);
        }

        public string SerilizeString<T>(T data)
        {
          
            throw new NotImplementedException();
        }
    }
}
