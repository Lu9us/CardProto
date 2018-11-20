using System;
using GameLib.Client.UI;
namespace WarInHeven.DataStructures.UI
{
    public class InfoPanel
    {
        private InfoPanel()
        {
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i] = new Label(new Microsoft.Xna.Framework.Vector2(50, 150 + (i * 15)));
                labels[i].Update("|");
            }
        }

       public Label[] labels = new Label[5];

        public static InfoPanel infoPanel{ get { if (infoPanelInternal == null) { infoPanelInternal = new InfoPanel(); } return infoPanelInternal; }}
        private static InfoPanel infoPanelInternal;


    }
}
