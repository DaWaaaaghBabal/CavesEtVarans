﻿using System;

namespace CavesEtVarans.battlefield {
	public class HexGrid<T> : Grid<T> where T : ICoordinates {

		public override int PlaneDistance(ICoordinates a, ICoordinates b) {
			int dC = a.Column - b.Column;
			int dR = a.Row - b.Row;
			int absR = Math.Abs(dR);
			int absC = Math.Abs(dC);
			int absZ = Math.Abs(dR - dC);

			return Math.Max(absZ, Math.Max(absR, absC));
		}

		public override bool Adjacent(ICoordinates a, ICoordinates b) {
			int dR = a.Row - b.Row;
			if (Math.Abs(dR) > 1)
				return false;
			int dC = a.Column - b.Column;
			if (Math.Abs(dC) > 1)
				return false;
			return Math.Abs(dC + dR) > 0;
		}

		protected override int[][] InitRingDirections() {
			return new int[][] {
				new int[] { -1, -1 }, //clockwise from 3 o'clock
				new int[] { 0, -1 },
				new int[] { 1, 0 },
				new int[] { 1, 1 },
				new int[] { 0, 1 },
				new int[] { -1, 0 }
			};
		}
	}
}
