using System;
using System.Collections.Generic;
using CavesEtVarans.graphics;
using CavesEtVarans.gui;
using UnityEngine;

namespace CavesEtVarans.battlefield {

	public class SquareGridProvider : HexOrSquare {
		protected override Grid<T> InitGrid<T>() {
			return new SquareGrid<T>();
		}

		protected override GenerationStrategy InitGridGenerator() {
			return new SquareGenerator();
		}

		protected override HighlightStrategy InitHighlight() {
			return new SquareHighlighter(EdgePrefab);
		}

        protected override LineOfSightStrategy InitLineOfSight() {
            Grid<Obstacle> obstacles = InitGrid<Obstacle>();
            return new SquareLineOfSight(obstacles);
        }

        protected override OrientationStrategy InitOrientation() {
			return new SquareOrientation();
		}

		protected override PlacementStrategy InitPlacement() {
			return new SquarePlacement();
		}

        protected override IEnumerable<int[]> InitRingDirections() {
            return new int[][] {
                new int[] { -1, -1 }, //clockwise from 3 o'clock
				new int[] { 1, -1 },
                new int[] { 1, 1 },
                new int[] { -1, 1 },
            };
        }
    }
}
