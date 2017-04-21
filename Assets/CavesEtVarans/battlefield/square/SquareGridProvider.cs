﻿using System;
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
			return new SquareHighlighter(WallPrefab);
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
	}
}
