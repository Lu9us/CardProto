using GameLib.Client.UI;
using GameLib.DataStructures.Interface;
using GameLib.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD42.Services
{
    public class ResourceService : IService
    {
        private int points = 0;

        List<string> ledger = new List<string>();
        Label label;
        Label label2;

        public void IncreasePoints(int value, string reason)
        {
            points += value;
            ledger.Add(reason + " points: " + value);
        }
        public void DecreasePoints(int value, string reason)
        {
            points -= value;
            ledger.Add(reason + " points: -" + value);
        }
        public int getScore()
        {
            return points;
        }

        public void OnClose()
        {
            File.WriteAllText("Ledger.txt", "Final Score: "+getScore()+Environment.NewLine);
        
            File.AppendAllLines("Ledger.txt", ledger);
        }

        public void OnReciveMessage(object message)
        {
          
        }

        public void OnStart(GameState s)
        {
            IncreasePoints(30, "GAME START");
            label = new Label(new Microsoft.Xna.Framework.Vector2(0, 35));
            label2 = new Label(new Microsoft.Xna.Framework.Vector2(0, 45));
        }

        public void OnUpdate()
        {
            label.Update("points: " + points);
            label2.Update("last ledger entry: " + ledger.Last());
        }
    }
}
