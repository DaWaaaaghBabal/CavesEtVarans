using CavesEtVarans.graphics;
using CavesEtVarans.gui;
using UnityEngine;

namespace CavesEtVarans.battlefield {

	public class SquareGridProvider : HexOrSquare {
		public SquareGridProvider(GameObject wallPrefab) : base(wallPrefab) { }
		protected override Grid<Tile> InitGrid() {
			return new SquareGrid<Tile>();
		}

		protected override GenerationStrategy InitGridGenerator() {
			return new SquareGenerator();
		}

		protected override HighlightStrategy InitHighlight() {
			return new SquareHighlighter(wallPrefab);
		}

		protected override OrientationStrategy InitOrientation() {
			return new SquareOrientation();
		}

		protected override PlacementStrategy InitPlacement() {
			return new SquarePlacement();
		}
	}
}
