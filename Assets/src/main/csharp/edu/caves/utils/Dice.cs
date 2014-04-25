using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans
{
    class Dice
    {
        private static Random r = null;

        protected static Random GetRandom()
        {
            if (r == null)
            {
                r = new Random();
            }
            return r;
        }

        // Roll(2,100) <=> roll 2d100 and sum them up
        public static int Roll(int numberOfDices, int numberOfFaces) 
        {
            int total = 0;
            for (int i = 0; i < numberOfDices; i++)
            {
                total += GetRandom().Next(1, numberOfFaces);
            }
            return total;
        }

    }

}

