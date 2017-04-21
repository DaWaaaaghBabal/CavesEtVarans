using System;
using CavesEtVarans.gui;
using UnityEngine;
using CavesEtVarans.graphics;

namespace CavesEtVarans.battlefield {

	public class HexGridProvider : HexOrSquare {
        protected override Grid<T> InitGrid<T>() {
			return new HexGrid<T>();
		}

		protected override GenerationStrategy InitGridGenerator() {
			return new HexGenerator();
		}

		protected override HighlightStrategy InitHighlight() {
			return new HexHighlighter(WallPrefab);
		}

        protected override LineOfSightStrategy InitLineOfSight() {
            Grid<Obstacle> obstacles = InitGrid<Obstacle>();
            return new HexLineOfSight(obstacles);
        }

        protected override OrientationStrategy InitOrientation() {
			return new HexOrientation();
		}

		protected override PlacementStrategy InitPlacement() {
			return new HexPlacement();
		}
	}
}
