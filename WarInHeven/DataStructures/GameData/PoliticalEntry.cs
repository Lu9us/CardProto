using System;
namespace WarInHeven.DataStructures.GameData
{
    public enum PoliticalState
    {
        PEACE,WAR,TREATY,ALLIES
    }
    public class PoliticalEntry
    {
        public String cause;
        public int value;
        public Empire causingEmpire;
        public PoliticalEntry()
        {
            
        }
    }
}
