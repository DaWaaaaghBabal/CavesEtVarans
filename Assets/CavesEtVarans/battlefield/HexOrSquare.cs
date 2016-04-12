using CavesEtVarans.graphics;
using CavesEtVarans.gui;
using UnityEngine;

namespace CavesEtVarans.battlefield {
	public abstract class HexOrSquare {
		private static HexOrSquare instance;
		
		protected GameObject wallPrefab;
		private Grid<Tile> grid;
		private HighlightStrategy highlight;
        //@TODO creates a dependency between model and grahpics. FIX THAT. 
        //Note : might not be fixable : hex/square dichotomy affects both graphics and model.
		private PlacementStrategy placement;
		private GenerationStrategy gridGenerator;
		private OrientationStrategy orientation;

		public HexOrSquare(GameObject wallPrefab) {
			this.wallPrefab = wallPrefab;
			grid = InitGrid();
			highlight = InitHighlight();
			placement = InitPlacement();
			gridGenerator = InitGridGenerator();
			orientation = InitOrientation();
			placement.InitAngles(orientation);
		}

		public static void SetSingleton(HexOrSquare singleton) {
			instance = singleton;
		}

		public static Grid<Tile> ProvideGrid() {
			return instance.grid;
		}
		public static HighlightStrategy ProvideHighlight() {
			return instance.highlight;
		}
		public static PlacementStrategy ProvidePlacement() {
			return instance.placement;
		}
		internal static OrientationStrategy ProvideOrientation() {
			return instance.orientation;
		}
		public static GenerationStrategy ProvideGridGenerator() {
			return instance.gridGenerator;
		}

		protected abstract Grid<Tile> InitGrid();
		protected abstract GenerationStrategy InitGridGenerator();
		protected abstract HighlightStrategy InitHighlight();
		protected abstract PlacementStrategy InitPlacement();
		protected abstract OrientationStrategy InitOrientation();
	}
}
