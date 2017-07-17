using System;
using CavesEtVarans.gui;
using UnityEngine;
using CavesEtVarans.graphics;
using System.Collections.Generic;

namespace CavesEtVarans.battlefield {

	public class HexGridProvider : HexOrSquare {
        protected override Grid<T> InitGrid<T>() {
			return new HexGrid<T>();
		}

		protected override HighlightStrategy InitHighlight() {
			return new HexHighlighter(EdgePrefab);
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
