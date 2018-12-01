using GameLib.Client.System.GraphicsHandlers;
using GameLib.EventSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarInHeven.DataStructures.Events;
using WarInHeven.DataStructures.GameData;
using WarInHeven.DataStructures.Interfaces;

namespace WarInHeven
{
    public class Empire
    {
        public Guid ID = Guid.NewGuid();
        public string name;
        public Empire parent;
        public List<Empire> children = new List<Empire>();
        public IController controller;
        public Color color;
        public double money;
        public List<Star> planets = new List<Star>();
        public bool active = true;
        public List<Fleet> fleets = new List<Fleet>();
        private List<PoliticalEntry> politicalEntries = new List<PoliticalEntry>();
        public Dictionary<Empire, PoliticalState> currentPoliticalState = new Dictionary<Empire, PoliticalState>();

        public int freindshipWithEmpire(Empire empire)
        {
            int view = 0;
            foreach (PoliticalEntry entry in politicalEntries.Where((arg) => arg.causingEmpire.ID == empire.ID))
            {
                view += entry.value;
            }
            return view;
        }

        public Empire()
        {
            name = ID.ToString();
            color = GraphicsHelper.getRandomColor();
            money = 40;
        }

        public bool isCapital(Star star)
        {
            if (planets.Count > 0)
            {
                return planets[0].id == star.id;
            }
            return false;
        }

        public void update(StarMap sm)
        {
            foreach (Star star in planets)
            {
                money += star.baseWealthRate * (star.population / 2);

            }
        }

        public void pushPoliticalEntry(PoliticalEntry e)
        {
            
            this.politicalEntries.Add(e);
        }
        public void changePoliticalState(Empire empire, PoliticalState state)
        {
            new EmpireEvent(this, empire.name + "has changed political state to" +state.ToString().ToLower()+" "+ this.name,-1);
            if(currentPoliticalState.ContainsKey(empire))
            {
                currentPoliticalState[empire] = state;  
            }
            else
            {
                currentPoliticalState.Add(empire, state);
            }
        }
    }
   
}
