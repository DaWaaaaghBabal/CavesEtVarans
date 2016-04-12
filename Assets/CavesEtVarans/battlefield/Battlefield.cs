using CavesEtVarans.gui;
using System.Collections.Generic;
using System;
using CavesEtVarans.character;

namespace CavesEtVarans.battlefield {
	public class Battlefield {

		private static Battlefield instance;
		public static Battlefield Instance {
			set { }
			get {
				if (instance == null) {
					instance = new Battlefield();
				}
				return instance;
			}
		}

		private Grid<Tile> grid;
		private OrientationStrategy orientationStrategy;

		public static void Init() {
			Instance.grid = HexOrSquare.ProvideGrid();
			Instance.orientationStrategy = HexOrSquare.ProvideOrientation();
		}

		public static int GameDistance(Tile t1, Tile t2) {
			return Instance.grid.GameDistance(t1, t2);
		}

        public static HashSet<Tile> Line(Tile t1, Tile t2) {
            return Instance.grid.Line(t1, t2);
        }

        public static Orientation Direction(Tile startTile, Tile targetTile) {
			return Instance.orientationStrategy.CalculateDirection(startTile, targetTile);
        }

        public static void Move(Character target, Tile targetTile)
        {
            target.Tile.Character = null;
            target.Tile = targetTile;
            targetTile.Character = target;
        }

        public static void AddTile(Tile tile) {
			Instance.grid.AddTile(tile);
		}

		public static Tile Get(int row, int column, int layer) {
			return Instance.grid.Get(row, column, layer);
		}

		public static HashSet<Tile> GetArea(Tile tile, int radius) {
			return Instance.grid.GetArea(tile, radius);
		}

		public static Tile[] GetStack(int row, int column) {
			return Instance.grid.GetStack(row, column);
		}

		public static bool AreNeighbours(Tile t1, Tile t2) {
			return Instance.grid.Adjacent(t1, t2);
		}
	}
}
