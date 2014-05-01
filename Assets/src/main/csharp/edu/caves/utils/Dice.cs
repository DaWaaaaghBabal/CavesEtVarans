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

        // Roll(X,Y,Z) = roll XdY+Z
        public static int Roll(int numberOfDices, int numberOfFaces, int offset) {
            int total = offset;
            for (int i = 0; i < numberOfDices; i++) {
                total += GetRandom().Next(1, numberOfFaces + 1); // +1 because Random#Next returns between minValue (included) and maxValue (excluded).
            }
            return total;
        }
        // Roll(2,100) <=> roll 2d100 and sum them up
        public static int Roll(int numberOfDices, int numberOfFaces) {
            return Roll(numberOfDices, numberOfFaces, 0);
        }
    }

}

