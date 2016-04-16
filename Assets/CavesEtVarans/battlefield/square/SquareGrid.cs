using System;
using System.Collections.Generic;

namespace CavesEtVarans.battlefield
{
	public class SquareGrid<T> : Grid<T> where T : ICoordinates {

		public override int PlaneDistance (ICoordinates a, ICoordinates b) {
			int dX = Math.Abs (a.Row - b.Row);
			int dY = Math.Abs (a.Column - b.Column);
			// We divide dH so that we can have some height variety, like low obstacles, without affecting ranges.
			return dX + dY;
		}

		public override HashSet<T> GetArea(ICoordinates tile, int radius) {
			HashSet<T> result = new HashSet<T>();
			for (int dR = -radius ; dR <= radius ; dR ++) {
				int dCMax = radius - Math.Abs(dR);
				for (int dC = -dCMax ; dC <= dCMax ; dC ++) {
					int dLMax = radius - Math.Abs(dR) - Math.Abs(dC);
					for (int dL = 0 ; dL <= dLMax ; dL++) {
						//The height division forces us to do some shenanigans
						for (int dH = 0 ; dH < heightDivisor ; dH ++) {
							int layerOffset = heightDivisor * dL + dH;
							Select(result, tile.Row + dR, tile.Column + dC, tile.Layer + layerOffset);
							Select(result, tile.Row + dR, tile.Column + dC, tile.Layer - layerOffset);
						}
					}
				}
			}
			return result;
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
