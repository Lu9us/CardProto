using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Util.Interfaces;

namespace Util
{
  public  class Serilizer: ISerizilizer
    {

        public byte[] Serilize<T>(T data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(ms, data);
                return ms.ToArray();

            }

        }
        public  T DeSerilize<T>(byte [] data)
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
                catch(Exception e)
                {
                    throw e;
                 
                }
            }
        }

        public T DeSerilize<T>(byte[] data,int size)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    ms.Write(data, 0, data.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    BinaryFormatter b = new BinaryFormatter();
                    return (T)b.Deserialize(ms);
                }
                catch (Exception e)
                {
                    throw e;

                }
            }
        }

        public string SerilizeString<T>(T data)
        {
            throw new NotImplementedException();
        }

        public T DeSerilize<T>(string data)
        {
            throw new NotImplementedException();
        }

    }
}
