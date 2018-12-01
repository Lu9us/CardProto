using System;
using System.Collections.Generic;

namespace GameLib.EventSystem
{
    public class EventBus
    {
        private List<Event> events = new List<Event>();

        public void pushEvent(Event e)
        {
            events.Add(e);
        }

        public List<Event> getLast(int count)
        {
            List<Event> data = new List<Event>();
            int iterations = 0;
            for (int i = events.Count-1; i>=0; i--)
            {
                data.Add(events[i]);
                iterations++;
                if(iterations >= count)
                {
                    break;
                }
            }
            return data;
        }


        public EventBus()
        {
        }
    }
}
