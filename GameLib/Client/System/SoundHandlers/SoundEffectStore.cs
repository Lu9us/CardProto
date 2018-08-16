using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Client.System.SoundHandlers
{
    public class SoundEffectAtlas
    {
        ContentManager cm;
        Dictionary<string, SoundEffect> sfDictionary = new Dictionary<string, SoundEffect>();
        public SoundEffectAtlas(ContentManager content)
        {
            cm = content;

        }

        public void playSound(string effect)
        {
            try
            {
                if (!sfDictionary.ContainsKey(effect))
                {

                    SoundEffect seffect = cm.Load<SoundEffect>(effect);
                    if (seffect != null)
                    {
                        sfDictionary.Add(effect, seffect);
                    }
                    else
                    {
                        return;
                    }
                }
                sfDictionary[effect].Play();
            }

            catch (Exception e)
            {

            }

        }

    }
}
