using GameLib.DataStructures;
using GameLib.DataStructures.Implementations;
using GameLib.DataStructures.Interface;
using GameLib.Server;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;


namespace GameLib.Unified.GameObject
{
    public class GameObjectService : IService, DataInterface
    {
        private Dictionary<Guid, GameObject> dataStore = new Dictionary<Guid, GameObject>();
        private JSONSerilizer s = new JSONSerilizer();

        public Dictionary<string, GameObjectTemplate> templates = new Dictionary<string, GameObjectTemplate>();

        GameState gs;
        private void SpawnNewGameObject(Vector2 pos, GameObjectTemplate template,Guid proposedGUID)
        {
            GameObject go = new GameObject();
            if (proposedGUID == Guid.Empty)
            {
                go.ID = Guid.NewGuid();
            }
            else
            {
                go.ID = proposedGUID;
            }
            go.name = template.name;
            go.vars = s.DeSerilize<HashVarTable>( s.Serilize<HashVarTable>(template.vars));
            go.spriteList = template.spriteList;
            go.pos = pos;
            dataStore.Add(go.ID, go);
        }

        private void DestroyGameObject(Guid data)
        {
            dataStore.Remove(data);
        }

        public GameObject GetGameObject(Guid data)
        {
           return dataStore[data];
        }

        public void OnClose()
        {
            
        }

        public void OnReciveMessage(object message)
        {
        
        }

        public void OnStart(GameState s)
        {
            gs = s;
            LoadTemplates("Config/gameObjects.template");
            s.dataManager.AddClient("GameObject", this);
            gs.varTable.AddItem("GameObjects", dataStore);
            GameObjectRenderer r = new GameObjectRenderer(dataStore);
        }

        public void LoadTemplates(string filename)
        {
            if (!File.Exists(filename))
            {
                GameObjectTemplate template = new GameObjectTemplate();
                template.spriteList = new List<KeyValuePair<string, Vector2>>();
                template.vars = new HashVarTable();
                template.spriteList.Add(new KeyValuePair<string, Vector2>("test", Vector2.Zero));
                 templates.Add("test", template);
                File.WriteAllBytes(filename, s.Serilize<Dictionary<string, GameObjectTemplate>>(templates));
            }
            byte[] tiledata = File.ReadAllBytes(filename);
            
            templates = s.DeSerilize<Dictionary<string,GameObjectTemplate>>(tiledata);
            
        }

        public void OnUpdate()
        {
          
        }

        public void Update(object data, DataMap source)
        {
          
            if (data != null)
            {
                foreach(GameObjectRequest request in (List<GameObjectRequest>) data)
                {
                    switch (request.eventType)
                    {
                        case GameObjectEvent.CREATE:
                            SpawnNewGameObject(request.pos,templates[request.name],request.ID);
                            break;
                        case GameObjectEvent.DESTROY:
                            DestroyGameObject(request.ID);
                            break;
                        case GameObjectEvent.SEND:
                            break;
                        case GameObjectEvent.SENDLIST:
                            break;
                    }
                }

            }

        }
        public  void AddGameObjectRequest(GameObjectRequest r)
        {
            if (gs.dataManager.getCurrentMap().GetData("GameObject") == null)
            {
                gs.dataManager.getCurrentMap().AddData("GameObject",new List<GameObjectRequest>());
            }
            ((List<GameObjectRequest>)gs.dataManager.getCurrentMap().GetData("GameObject")).Add(r);
        }
    }
    public enum GameObjectEvent
    {
        CREATE,DESTROY,SEND,SENDLIST

    }
    public class GameObjectRequest
    {
      public  string sendingService;
      public GameObjectEvent eventType;
      public Guid ID;
      public string name;
      public Vector2 pos;
    }
}
