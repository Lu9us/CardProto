using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    [Serializable]
    public class Packet
    {
        public byte[] data;
        public int length;
    }
}
