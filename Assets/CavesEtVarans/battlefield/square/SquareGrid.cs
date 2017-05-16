using System;

namespace CavesEtVarans.battlefield
{
	public class SquareGrid<T> : Grid<T> where T : ICoordinates {

		public override int PlaneDistance (ICoordinates a, ICoordinates b) {
			int dX = Math.Abs (a.Row - b.Row);
			int dY = Math.Abs (a.Column - b.Column);
			// We divide dH so that we can have some height variety, like low obstacles, without affecting ranges.
			return dX + dY;
		}
		
		public override bool Adjacent(ICoordinates a, ICoordinates b) {
			int dR = Math.Abs(a.Row - b.Row);
			if (dR > 1)
				return false;
			int dC = Math.Abs(a.Column - b.Column);
			if (dC > 1)
				return false;
			return dR * dC == 0;
		}
	}
}
