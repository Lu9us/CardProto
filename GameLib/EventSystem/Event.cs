using System;
namespace GameLib.EventSystem
{
    public abstract class Event
    {
        public static EventBus eventBus;

        public Event()
        {
            if (eventBus != null)
            {
                eventBus.pushEvent(this);
            }
        }

       public string eventType;
       public long eventTimeStamp;


        public abstract String log();

        public override string ToString()
        {
            return log();
        }
    }
}
