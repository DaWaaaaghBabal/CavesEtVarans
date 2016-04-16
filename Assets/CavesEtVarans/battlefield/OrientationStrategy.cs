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
		private Orientation right = new Orientation ("right");
		private Orientation up = new Orientation ("up");
		private Orientation left = new Orientation ("left");
		private Orientation down = new Orientation ("down");

		public SquareOrientation () {
			right.Left = up;
			up.Left = left;
			left.Left = down;
			down.Left = right;

			flankings = new Flanking[]{ Flanking.Front, Flanking.Flank, Flanking.Back };
		}

		protected override Orientation[] CalculateDirection (Tile t1, Tile t2) {
			int dR = t2.Row - t1.Row;
			int dC = t2.Column - t2.Column;
            Orientation[] result = new Orientation[2];
            if (dR == dC) // diagonal
                return dC > 0 ?
                    new Orientation[] { up, right } :
                    new Orientation[] { down, left };
            if (dR == -dC) // the other diagonal
                return dC > 0 ?
                    new Orientation[] { left, up } :
                    new Orientation[] { right, down };
            if (Math.Abs (dR) > Math.Abs (dC))
				return dR > 0 ?
                    new Orientation[] { up, up } :
                    new Orientation[] { down, down };
            return dC > 0 ?
                    new Orientation[] { right, right } :
                    new Orientation[] { left, left };
        }

		public override Orientation[] AllDirections () {
			return new Orientation[] { right, down, left, up };
		}
	}

	public class HexOrientation : OrientationStrategy {
		private Orientation right = new Orientation ("right");
		private Orientation upRight = new Orientation ("up, right");
		private Orientation upLeft = new Orientation ("up, left");
		private Orientation left = new Orientation ("left");
		private Orientation downLeft = new Orientation ("down, left");
		private Orientation downRight = new Orientation ("down, right");

		private double cos60;
		private double sin60;
		private double cos30;

		public HexOrientation () {
			cos60 = 0.5;
			sin60 = Math.Sqrt (0.75);
			cos30 = sin60;
			right.Left = upRight;
			upRight.Left = upLeft;
			upLeft.Left = left;
			left.Left = downLeft;
			downLeft.Left = downRight;
			downRight.Left = right;
            
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
                return left;
            if (cos < 0)
                return dY > 0 ? upLeft : downLeft;
            if (cos < cos30)
                return dY > 0 ? upRight : downRight;
            return right;
        }

        public override Orientation[] AllDirections () {
			return new Orientation[] { right, downRight, downLeft, left, upLeft, upRight };
		}
	}
}