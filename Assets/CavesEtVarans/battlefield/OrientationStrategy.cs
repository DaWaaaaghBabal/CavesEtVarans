using System;
using UnityEngine;

namespace CavesEtVarans.battlefield
{
	public abstract class OrientationStrategy {
		protected Flanking[] flankings;

		/// <summary>
		/// Returns the direction from t1 to t2. The direction is
		/// one of the grid's base directions (e.g. left / right / up / down for a square grid)
		/// and not a vector or an angle.
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <returns></returns>
		public Orientation Direction (Tile t1, Tile t2) {
            return CalculateDirection(t1, t2)[0];
        }

        /// <summary>
        /// Returns exactly two directions. If targetTile is exactly on the
        /// border between two directions, the method will return both.
        /// Otherwise, it will return twice the same orientation.
        /// </summary>
        /// <param name="startTile"></param>
        /// <param name="targetTile"></param>
        /// <returns></returns>
        protected abstract Orientation[] CalculateDirection(Tile startTile, Tile targetTile);

        /// <summary>
        /// Returns all directions handled by the game, clockwise from the right.
        /// </summary>
        /// <returns></returns>
        public abstract Orientation[] AllDirections ();

		/// <summary>
		/// Based on the attack's source's position, the target's position, and the target's position,
		/// determines if the attack is coming from the front / flanck / back.
		/// </summary>
		/// <param name="sourceTile"></param>
		/// <param name="targetTile"></param>
		/// <param name="targetOrientation"></param>
		/// <returns></returns>
		public Flanking CalculateFlanking (Tile sourceTile, Tile targetTile, Orientation targetOrientation) {
			Orientation [] directionToSource = CalculateDirection (targetTile, sourceTile);
            int d1 = OrientationDistance (directionToSource[0], targetOrientation);
            int d2 = OrientationDistance (directionToSource[0], targetOrientation);
            return flankings[Math.Min(d1, d2)];
		}

        private int OrientationDistance(Orientation orientation, Orientation targetOrientation) {
            int leftDistance = orientation.LeftDistance(targetOrientation);
            return Math.Min(leftDistance, AllDirections().Length - leftDistance);
        }
    }

	public class SquareOrientation : OrientationStrategy {
		private Orientation threeOClock = new Orientation ();
		private Orientation twelveOClock = new Orientation ();
		private Orientation nineOClock = new Orientation ();
		private Orientation sixOClock = new Orientation ();

		public SquareOrientation () {
			threeOClock.Left = twelveOClock;
			twelveOClock.Left = nineOClock;
			nineOClock.Left = sixOClock;
			sixOClock.Left = threeOClock;

			flankings = new Flanking[]{ Flanking.Front, Flanking.Flank, Flanking.Back };
		}

		protected override Orientation[] CalculateDirection (Tile t1, Tile t2) {
			int dR = t2.Row - t1.Row;
			int dC = t2.Column - t2.Column;
            Orientation[] result = new Orientation[2];
            if (dR == dC) // diagonal
                return dC > 0 ?
                    new Orientation[] { twelveOClock, threeOClock } :
                    new Orientation[] { sixOClock, nineOClock };
            if (dR == -dC) // the other diagonal
                return dC > 0 ?
                    new Orientation[] { nineOClock, twelveOClock } :
                    new Orientation[] { threeOClock, sixOClock };
            if (Math.Abs (dR) > Math.Abs (dC))
				return dR > 0 ?
                    new Orientation[] { twelveOClock, twelveOClock } :
                    new Orientation[] { sixOClock, sixOClock };
            return dC > 0 ?
                    new Orientation[] { threeOClock, threeOClock } :
                    new Orientation[] { nineOClock, nineOClock };
        }

		public override Orientation[] AllDirections () {
			return new Orientation[] { threeOClock, sixOClock, nineOClock, twelveOClock };
		}
	}

	public class HexOrientation : OrientationStrategy {
		private Orientation threeOClock = new Orientation ();
		private Orientation oneOClock = new Orientation ();
		private Orientation elevenOClock = new Orientation ();
		private Orientation nineOClock = new Orientation ();
		private Orientation sevenOClock = new Orientation ();
		private Orientation fiveOClock = new Orientation ();

		private double cos60;
		private double sin60;
		private double cos30;

		public HexOrientation () {
			cos60 = 0.5;
			sin60 = Math.Sqrt (0.75);
			cos30 = sin60;
			threeOClock.Left = oneOClock;
			oneOClock.Left = elevenOClock;
			elevenOClock.Left = nineOClock;
			nineOClock.Left = sevenOClock;
			sevenOClock.Left = fiveOClock;
			fiveOClock.Left = threeOClock;
            
            flankings = new Flanking[] { Flanking.Front, Flanking.FrontFlank, Flanking.BackFlank, Flanking.Back };
        }

		protected override Orientation[] CalculateDirection (Tile t1, Tile t2) {
            int dR = t2.Row - t1.Row;
            int dC = t2.Column - t1.Column;

            double dX = dC - cos60 * dR;
            double dY = sin60 * dR;
            double norm = Math.Sqrt (dX * dX + dY * dY);
            double cos = dX / norm;
            double mso = 0.001; // oversampling offset
            // Strict inequalities mean we assign all tiles on a diagonal to the direction on the right of the border.
            return new Orientation[] { Cos2Dir(dY, cos - mso), Cos2Dir(dY, cos + mso) };
        }

        private Orientation Cos2Dir(double dY, double cos) {
            if (cos < -cos30)
                return nineOClock;
            if (cos < 0)
                return dY > 0 ? elevenOClock : sevenOClock;
            if (cos < cos30)
                return dY > 0 ? oneOClock : fiveOClock;
            return threeOClock;
        }

        public override Orientation[] AllDirections () {
			return new Orientation[] { threeOClock, fiveOClock, sevenOClock, nineOClock, elevenOClock, oneOClock };
		}
	}
}