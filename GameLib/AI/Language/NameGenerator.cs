using System;
using GameLib.Client.System;
using Util;

namespace GameLib.AI.Language
{
    public class NameGenerator
    {
        public string[] cylibleStructers = {"vcv","v","v'cve","cvv","vc's","av","quv" };

        public string generateName(int length,String [] templateStructs = null){

            String nameTemplate = "";

            string[] nameStructures;
            if(templateStructs != null)
            {
                nameStructures = templateStructs;
            }
            else
            {
                nameStructures = cylibleStructers;
            }


            for (int i = 0; i < length;i++)
            {
                nameTemplate += nameStructures[RandomHelper.getRandomInt(max: nameStructures.Length)]; 
            }
            string name ="";
            for (int ix = 0; ix < nameTemplate.Length;ix++)
            {
                //convert to switch at somepoint
                if(nameTemplate[ix] == 'v')
                {
                    name += CharacterTypes.getRandomVowel();
                }
                else if(nameTemplate[ix] == 'c')
                {
                    name += CharacterTypes.getRandomConstant();  
                }
                else
                {
                    name += nameTemplate[ix];
                }
            }
            return name;
        }
      
    }
}
