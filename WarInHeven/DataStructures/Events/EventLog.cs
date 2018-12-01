using System;
using GameLib.Client.UI;
using GameLib.EventSystem;
using Microsoft.Xna.Framework;

namespace WarInHeven.DataStructures.Events
{
    public class EventLog
    {
        Label[] label = new Label[15];
        public EventLog()
        {
            for (int i = 0; i < label.Length; i++)
            {
                label[i] = new Label(new Vector2(0, 400+(i*12)));
            }
        }

        public void update(EventBus eventBus)
        {
            var events = eventBus.getLast(15);
            foreach(Label l in label)
            {
                l.Update("");
            }
            for (int i = 0; i < events.Count; i++)
            {
                
                label[i].Update(events[i].log()); 
            }
        }
    }
}
