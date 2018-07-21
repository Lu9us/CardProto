using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
   public class DataMapManager
    {
        private DataMap currentMap = new DataMap();
        private DataMap LastRecivedMap;
        private List<KeyValuePair<string, DataInterface>> clients = new List<KeyValuePair<string, DataInterface>>();

        public void ReciveRaw(byte[] data)
        {
            try
            {
                Packet pdata = Serilizer.Desrilize<Packet>(data);
                DataMap dmap = Serilizer.Desrilize<DataMap>(pdata.data);
                dmap.AddData("GameState:PSize", pdata.length);
                dmap.AddData("GameState:Packet", "PacketSize: " + data.Length);
                dmap.AddData("GameState:FreePacket","PacketFree: "+( data.Length - pdata.length));
                ReciveData(dmap);
            }
            catch (Exception e)
            {
                LastRecivedMap.AddData("GameState:Exception", e.Message);
                ReciveData(LastRecivedMap);

            }

        }
            public void ReciveData(DataMap data)
        {
            if (data != null)
            {
                LastRecivedMap = data;
                foreach (string s in data.keys)
                {
                    foreach (var a in clients.Where(a => a.Key == s || a.Key == s.Split(':')[0]))
                    {
                        a.Value.Update(data.GetData(s), data);
                    }
                }
            }

        }
        public void CreateNewMap()
        {
            currentMap = new DataMap();
        }
        public DataMap getCurrentMap()
        {
            return currentMap;
        }
        public void SendData(Socket s)
        {
            Packet p = new Packet();
            p.data = Serilizer.Serilize<DataMap>(currentMap);
            p.length = p.data.Length;

        s.Send(Serilizer.Serilize<Packet>(p));

        }

        public void AddClient(string tag, DataInterface client)
        {
            clients.Add(new KeyValuePair<string, DataInterface>(tag, client));
        }

    }
}
