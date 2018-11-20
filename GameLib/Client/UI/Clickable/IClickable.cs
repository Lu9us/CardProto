using System;
using Microsoft.Xna.Framework;

namespace GameLib.Client.UI.Clickable
{
    public abstract class IClickable
    {
        public static ClickableService service;
        public bool enabled = true;
        public bool screenSpace = true;
        public Rectangle hitBox = new Rectangle();

        public IClickable()
        {
            if(service != null)
            {
                service.registerObject(this);
            }
        }

        public abstract void LeftClick();
        public abstract void RightClick();
        public abstract void UiUpdate();
        ~IClickable()
        {
            if (service != null)
            {
                service.deRegisterObject(this);
            }
        }
    }
}
