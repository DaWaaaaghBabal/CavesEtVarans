using System;
using UnityEngine;

namespace CavesEtVarans.battlefield
{
	public abstract class OrientationStrategy {
		protected Flanking[] flankings;

		/// <summary>
		/// Returns the direction from one tile to another. The direction is
		/// one of the grid's base directions (e.g. left / right / up / down for a square grid)
		/// and not a vector or an angle.
		/// </summary>
		/// <param name="startTile"></param>
		/// <param name="targetTile"></param>
		/// <returns></returns>
		public abstract Orientation CalculateDirection (Tile startTile, Tile targetTile);

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
			Orientation directionToSource = CalculateDirection (targetTile, sourceTile);
			Orientation temp = targetOrientation;
			int max = AllDirections ().Length;
			for (int i = 0; i < max; i++) {
				if (temp == directionToSource) {
					return flankings [Math.Min (i, max - i)];
				}
				temp = temp.Right;
			}
			// Won't happen
			return 0;
		}
	}

	public class SquareOrientation : OrientationStrategy {
		private Orientation right = new Orientation ();
		private Orientation up = new Orientation ();
		private Orientation left = new Orientation ();
		private Orientation down = new Orientation ();

		public SquareOrientation () {
			right.Left = up;
			up.Left = left;
			left.Left = down;
			down.Left = right;

			right.Right = down;
			down.Right = left;
			left.Right = up;
			up.Right = left;
			flankings = new Flanking[]{ Flanking.Front, Flanking.Flank, Flanking.Back };
		}

		public override Orientation CalculateDirection (Tile startTile, Tile targetTile) {
			int dR = targetTile.Row - startTile.Row;
			int dC = targetTile.Column - targetTile.Column;
			if (Math.Abs (dR) > Math.Abs (dC))
				return dR > 0 ? up : down;
			return dC > 0 ? right : left;
		}

		public override Orientation[] AllDirections () {
			return new Orientation[] { right, down, left, up };
		}
	}

	public class HexOrientation : OrientationStrategy {
		private Orientation right = new Orientation ();
		private Orientation upRight = new Orientation ();
		private Orientation upLeft = new Orientation ();
		private Orientation left = new Orientation ();
		private Orientation downLeft = new Orientation ();
		private Orientation downRight = new Orientation ();

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

			right.Right = downRight;
			downRight.Right = downLeft;
			downLeft.Right = left;
			left.Right = upLeft;
			upLeft.Right = upRight;
			upRight.Right = right;
            flankings = new Flanking[] { Flanking.Front, Flanking.FrontFlank, Flanking.BackFlank, Flanking.Back };
        }

		public override Orientation CalculateDirection (Tile startTile, Tile targetTile) {
			int dR = targetTile.Row - startTile.Row;
			int dC = targetTile.Column - startTile.Column;

			double dX = dC - cos60 * dR;
			double dY = sin60 * dR;
			double norm = Math.Sqrt (dX * dX + dY * dY);
			double cos = dX / norm;
			// Strict inequalities mean we assign all tiles on a diagonal to the direction on the right of the border.
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