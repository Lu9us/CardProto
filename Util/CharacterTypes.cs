using System;
using System.Linq;

namespace Util
{
    public class CharacterTypes
    {
        static Random random = new Random(Guid.NewGuid().GetHashCode());
        public static readonly char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
        public static bool isVowel(char c)
        {
            return vowels.Contains(Char.ToLower(c));
        }

        public static char getRandomVowel()
        {
            return vowels[random.Next(vowels.Length)];
        }
        public static char getRandomConstant()
        {
            char c = (char)random.Next(97, 123);
            while(vowels.Contains(c))
            {
                c = (char)random.Next(97, 123);
            }
            return c;
        }

    }
}
