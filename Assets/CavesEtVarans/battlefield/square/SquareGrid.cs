using System;
using System.Collections.Generic;

namespace CavesEtVarans.battlefield
{
	public class SquareGrid<T> : Grid<T> where T : utils.IDisposable, ICoordinates {

		public override int GameDistance (T a, T b) {
			int dX = Math.Abs (a.Row - b.Row);
			int dY = Math.Abs (a.Column - b.Column);
			int dH = Math.Abs (a.Layer - b.Layer);
			// We divide dH so that we can have some height variety, like low obstacles, without affecting ranges.
			return dX + dY + dH / heightDivisor;
		}

		public override HashSet<T> GetArea(T tile, int radius) {
			HashSet<T> result = new HashSet<T>();
			for (int dR = -radius ; dR <= radius ; dR ++) {
				int dCMax = radius - Math.Abs(dR);
				for (int dC = -dCMax ; dC <= dCMax ; dC ++) {
					int dLMax = radius - Math.Abs(dR) - Math.Abs(dC);
					for (int dL = 0 ; dL <= dLMax ; dL++) {
						//The height division forces us to do some shenanigans
						for (int dH = 0 ; dH < heightDivisor ; dH ++) {
							int layerOffset = heightDivisor * dL + dH;
							SelectTile(result, tile.Row + dR, tile.Column + dC, tile.Layer + layerOffset);
							SelectTile(result, tile.Row + dR, tile.Column + dC, tile.Layer - layerOffset);
						}
					}
				}
			}
			return result;
		}
		public override bool Adjacent(T a, T b) {
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
