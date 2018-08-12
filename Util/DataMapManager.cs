using log4net;
using log4net.Config;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
        private ISerizilizer serizilizer = new JSONSerilizer();
        private DataMap currentMap = new DataMap();
        private DataMap LastRecivedMap;
        private List<KeyValuePair<string, DataInterface>> clients = new List<KeyValuePair<string, DataInterface>>();
        private List<KeyValuePair<string, DataInterface>> garbageList = new List<KeyValuePair<string, DataInterface>>();
        public DataMapManager()
        {
            BasicConfigurator.Configure();
            log.Info("DataMapManager started");
        }

        public void ReciveRaw(Socket s)
        {
            try
            {
                log.Debug("Receiving data ");
                //  string logS = "";
                //  int i = 0;
                //  int zeroCount = 0;
                //  foreach (byte by in data)
                //  {
                //      if (by == 0)
                //      {
                //          zeroCount++;
                //      }
                //      else
                //      {
                //          zeroCount = 0;
                //      }
                //      if (zeroCount > 6)
                //      {
                //          break;
                //      }
                //      logS += by.ToString("X2");
                //      i++;
                //      if (i == 10)
                //      {
                //          logS += Environment.NewLine;
                //          i = 0;
                //      }
                //
                //  }

                NetworkStream networkStream = new NetworkStream(s);
                StreamReader sr = new StreamReader(networkStream);
               // Packet pdata = serizilizer.DeSerilize<Packet>(data);
                DataMap dmap = serizilizer.DeSerilize<DataMap>(sr.ReadLine());
                // dmap.AddData("GameState:PSize", pdata.length);
                
               for(int i =0;i<dmap.data.Count;i++)
                {
             
                    if (dmap.data[i] is JToken)
                    {
                        dmap.data[i] = ((JToken)dmap.data[i]).ToObject(Type.GetType(dmap.typeNames[i]));
                    }
                    if(dmap.data[i] is JObject)
                    {
                        dmap.data[i] = ((JObject)dmap.data[i]).ToObject(Type.GetType(dmap.typeNames[i]));
                    }
                    if (dmap.data[i] is JArray)
                    {
                        dmap.data[i] = ((JArray)dmap.data[i]).ToObject(Type.GetType(dmap.typeNames[i]));
                    }
                    i++;
                }

                dmap.AddData("GameState:Packet", 0);//+ data.Length);
                //dmap.AddData("GameState:FreePacket","PacketFree: "+( data.Length));
                ReciveData(dmap);
            }
            catch (Exception e)
            {
                if (LastRecivedMap == null)
                {
                    LastRecivedMap = new DataMap();
                }
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
            foreach (KeyValuePair<string, DataInterface> kvp in garbageList)
            {
                clients.Remove(kvp);
            }
            garbageList.Clear();
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
            // Packet p = new Packet();
            // p.data = serizilizer.Serilize<DataMap>(currentMap);
            // p.length = p.data.Length;
            NetworkStream stream = new NetworkStream(s);
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine(serizilizer.SerilizeString<DataMap>(currentMap));
            sw.Flush();
        }

        public void AddClient(string tag, DataInterface client)
        {
            clients.Add(new KeyValuePair<string, DataInterface>(tag, client));
        }
        public void RemoveClient(DataInterface client)
        {
            garbageList.Add(clients.Find(t => t.Value == client));
        }
    }
}
