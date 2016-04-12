﻿using System;
using System.Collections.Generic;

namespace CavesEtVarans.battlefield {
	public class HexGrid<T> : Grid<T> where T : ICoordinates, utils.IDisposable {

		public override int GameDistance(T a, T b) {
			int dC = a.Column - b.Column;
			int dR = a.Row - b.Row;
			int absR = Math.Abs(dR);
			int absC = Math.Abs(dC);
			int absZ = Math.Abs(dR - dC);
			int dH = Math.Abs(a.Layer - b.Layer);

			int planeDist = Math.Max(absZ, Math.Max(absR, absC));
			int heightDist = dH / heightDivisor;

			return planeDist + heightDist;
		}

		public override HashSet<T> GetArea(T tile, int radius) {
			HashSet<T> result = new HashSet<T>();
			for (int dR = -radius ; dR <= radius ; dR++) {
                int dCMin = Math.Max(-radius, -radius + dR);
				int dCMax = Math.Min(radius, radius + dR);
				for (int dC = dCMin ; dC <= dCMax ; dC++) {
					// we need to calculate the plane distance to know the boundaries on the vertical direction.
					int absZ = Math.Abs(dR-dC);
					int absR = Math.Abs(dR);
					int absC = Math.Abs(dC);
					int planeDist = Math.Max(absZ, Math.Max(absR, absC));
					int dLMax = radius - planeDist;
					for (int dL = 0 ; dL <= dLMax ; dL++) {
						//The height division forces us to do some shenanigans
						for (int dH = 0 ; dH < heightDivisor ; dH++) {
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
			int dR = a.Row - b.Row;
			if (Math.Abs(dR) > 1)
				return false;
			int dC = a.Column - b.Column;
			if (Math.Abs(dC) > 1)
				return false;
            return Math.Abs(dC + dR) > 0;
        }
	}	
}