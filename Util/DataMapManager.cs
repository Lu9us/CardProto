using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Util.Interfaces;

namespace Util
{
   public class DataMapManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DataMapManager));
        private ISerizilizer serizilizer = new Serilizer();
        private DataMap currentMap = new DataMap();
        private DataMap LastRecivedMap;
        private List<KeyValuePair<string, DataInterface>> clients = new List<KeyValuePair<string, DataInterface>>();

        public DataMapManager()
        {
            BasicConfigurator.Configure();
            log.Info("DataMapManager started");
        }

        public void ReciveRaw(byte[] data,int size)
        {
            try
            {
                log.Debug("Receiving data ");
                string logS = "";
                int i = 0;
                int zeroCount = 0;
                foreach (byte by in data)
                {
                    if (by == 0)
                    {
                        zeroCount++;
                    }
                    else
                    {
                        zeroCount = 0;
                    }
                    if (zeroCount > 6)
                    {
                        break;
                    }
                    logS += by.ToString("X2");
                    i++;
                    if (i == 10)
                    {
                        logS += Environment.NewLine;
                        i = 0;
                    }

                }
                log.Debug(logS);
                Packet pdata = serizilizer.DeSerilize<Packet>(data);
                DataMap dmap = serizilizer.DeSerilize<DataMap>(pdata.data);
                dmap.AddData("GameState:PSize", pdata.length);
                dmap.AddData("GameState:Packet", "PacketSize: " + data.Length);
                dmap.AddData("GameState:FreePacket","PacketFree: "+( data.Length - pdata.length));
                ReciveData(dmap);
            }
            catch (Exception e)
            {
                LastRecivedMap.AddData("GameState:Exception", e.Message);
                ReciveData(LastRecivedMap);
                log.Error(e);
                log.Error(e.StackTrace);

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
            p.data = serizilizer.Serilize<DataMap>(currentMap);
            p.length = p.data.Length;

        s.Send(serizilizer.Serilize<Packet>(p));

        }

        public void AddClient(string tag, DataInterface client)
        {
            clients.Add(new KeyValuePair<string, DataInterface>(tag, client));
        }

    }
}
