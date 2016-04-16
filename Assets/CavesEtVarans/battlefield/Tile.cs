using CavesEtVarans.character;
using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;
using CavesEtVarans.utils;

namespace CavesEtVarans.battlefield {
	public class Tile : IDisposable, ITargetable, ICoordinates
    {

        Tile ITargetable.Tile {
            get { return this; }
            set{ }
        }
		private Statistic movementCost;

		public int Layer { private set; get; }
		public int Row { private set; get; }
		public int Column { private set; get; }

		public Character Character { get; set; }
		public bool IsFree {
			get {
				return Character == null;
			}
			private set { }
		}
        public int Size { set; get; }

        public Tile(int row, int column, int layer, int baseCost) : this(row, column, layer, baseCost, 6) {
            // the default thickness of a tile is 6.
		}
        public Tile(int row, int column, int layer, int baseCost, int thickness) {
            Row = row;
            Column = column;
            Layer = layer;
            movementCost = new Statistic(baseCost);
            Size = thickness;
        }

		public int GetMovementCost(Context context) {
			return movementCost.GetValue(context);
		}

		public void Dispose() {
			// @TODO delete the graphic tile via events
		}

		public override  string ToString () {
			return "Tile (" + Row + ", " + Column + ", " + Layer + ")";
		}
	}
}