using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
  public static class Serilizer
    {

        public static byte[] Serilize<T>(T data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(ms, data);
                return ms.GetBuffer();

            }

        }
        public static T Desrilize<T>(byte [] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    ms.Write(data, 0, data.Length);
                    ms.Position = 0;
                    BinaryFormatter b = new BinaryFormatter();
                    return (T)b.Deserialize(ms);
                }
                catch
                {
                    return default(T);
                }
            }
        }

    }
}
