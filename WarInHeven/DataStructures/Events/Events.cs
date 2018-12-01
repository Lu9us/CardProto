using System;
using GameLib.EventSystem;

namespace WarInHeven.DataStructures.Events
{
    public class EmpireEvent: Event
     {
         
        Empire empire;
        string details;

        public EmpireEvent(Empire empire,string details,long time)
        {
            this.eventType = "EmpireEvent";
            this.empire = empire;
            this.details = details;
            this.eventTimeStamp = time;
        }

        public override string log()
        {
            return eventTimeStamp+" : " +details;
        }
    }
}
