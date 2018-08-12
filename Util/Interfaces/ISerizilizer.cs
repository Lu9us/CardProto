using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Interfaces
{
   public interface ISerizilizer
    {
          byte[] Serilize<T>(T data);
          string SerilizeString<T>(T data);
          T DeSerilize<T>(byte[] data);
          T DeSerilize<T>(string data);
       
    }
}
