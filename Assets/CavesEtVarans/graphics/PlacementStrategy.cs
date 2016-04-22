using CavesEtVarans.rules;
using System.Collections.Generic;
using UnityEngine;
using CavesEtVarans.battlefield;

namespace CavesEtVarans.graphics {
	public abstract class PlacementStrategy {	
		protected Vector3 unitRow;
		protected Vector3 unitColumn;
		protected Vector3 unitLayer;
		// Stores, for each possible orientation, the corresponding rotation angle around the vertical axis (right = 0).
		protected Dictionary<Orientation, int> orientationAngles;

		// The angle between two directions. 90 for a square grid, 45 for a square grid with diagonals, 60 for a hex grid.
		protected int baseAngle;
		public void InitAngles(OrientationStrategy orientationStrategy) {
			int angle = 0;
			orientationAngles = new Dictionary<Orientation, int>();
			foreach(Orientation orientation in orientationStrategy.AllDirections()) {
				orientationAngles.Add(orientation, angle);
				angle += baseAngle;
			}
		}

		public Vector3 Place(int row, int column, int layer) {
			Vector3 result = row * unitRow 
			+ column * unitColumn 
			+ layer * unitLayer;
			return result;
		}

		public int AngleForOrientation(Orientation orientation) {
			return orientationAngles[orientation];
		}
	}

	public class SquarePlacement : PlacementStrategy {
		public SquarePlacement() {
			unitRow = new Vector3(0, 0, 1);
			unitColumn = new Vector3(1, 0, 0);
			unitLayer = new Vector3(0, 1.5f / RulesConstants.HEIGHT_DIVISOR, 0);
			baseAngle = 90;
		}
	}

	public class HexPlacement : PlacementStrategy {
		public HexPlacement() {
			unitColumn = new Vector3(1, 0, 0);
			unitRow = Quaternion.Euler(0, -120, 0) * unitColumn;
			unitLayer = new Vector3(0, 1.2f / RulesConstants.HEIGHT_DIVISOR, 0);
			baseAngle = 60;
		}
	}
}