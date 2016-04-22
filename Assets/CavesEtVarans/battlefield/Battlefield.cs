using System.Collections.Generic;
using CavesEtVarans.character;

namespace CavesEtVarans.battlefield {
	public class Battlefield {

		private static Battlefield instance;
		private static Battlefield Instance {
			set { }
			get {
				if (instance == null) {
					instance = new Battlefield();
				}
				return instance;
			}
		}

		private Grid<Tile> grid;
		private OrientationStrategy orientation;
        private LineOfSightStrategy lineOfSight;

		public static void Init() {
			Instance.grid = HexOrSquare.ProvideGrid();
			Instance.orientation = HexOrSquare.ProvideOrientation();
            Instance.lineOfSight = HexOrSquare.ProvideLineOfSight();
		}

		public static int GameDistance(Tile t1, Tile t2) {
			return Instance.grid.GameDistance(t1, t2);
        }

        public static Orientation Direction(Tile startTile, Tile targetTile) {
            return Instance.orientation.Direction(startTile, targetTile);
        }

        public static Flanking Flanking(Character source, Character target) {
            return Instance.orientation.CalculateFlanking(source.Tile, target.Tile, target.Orientation);
        }

        public static void Move(Character target, Tile targetTile)
        {
            target.Tile.Character = null;
            target.Tile = targetTile;
            targetTile.Character = target;
        }

        public static void AddTile(Tile tile) {
			Instance.grid.Add(tile);
            int row = tile.Row;
            int column = tile.Column;
            for (int i = 0 ; i <= tile.Layer ; i++) {
                int layer = tile.Layer - i;
                Obstacle obstacle = new Obstacle(row, column, i, 1);
                if (layer >= 0)
                    Instance.lineOfSight.Add(obstacle);
            }
		}

		public static Tile Get(int row, int column, int layer) {
			return Instance.grid.Get(row, column, layer);
		}

		public static HashSet<Tile> GetArea(ICoordinates center, int minRadius, int maxRadius) {
			return Instance.grid.GetArea(center, minRadius, maxRadius);
		}

		public static Tile[] GetStack(int row, int column) {
			return Instance.grid.GetStack(row, column);
		}

		public static bool AreNeighbours(Tile t1, Tile t2) {
			return Instance.grid.Adjacent(t1, t2);
		}

        public static Line<ICoordinates> LineOfSight (ICoordinates t1, int h1, ICoordinates t2, int h2) {
            Line<ICoordinates> LoS = Instance.lineOfSight.LineOfSight(t1, h1, t2, h2);
            LoS.Compare = CompareObstacles;
            return LoS;
        }

        private static int CompareObstacles(ICoordinates c1, ICoordinates c2) {
            // Returns the most favorable obstacle, i.e. the one with the less coverage.
            // (100% = full obstruction, 0% = clear line).
            Obstacle o1 = (Obstacle) c1;
            Obstacle o2 = (Obstacle) c2;
            if (o1 == null) return -1;
            if (o2 == null) return 1;
            if (o1.Cover < o2.Cover) return 1;
            if (o1.Cover > o2.Cover) return -1;
            return 0;
        }
	}
}
