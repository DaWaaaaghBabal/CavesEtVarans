
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
        public int Size { set; get; }

		public Character Character { get; set; }
		public bool IsFree {
			get {
				return Character == null;
			}
			private set { }
		}

        public Tile(int row, int column, int layer, int baseCost) {
            Row = row;
            Column = column;
            Layer = layer;
            movementCost = new Statistic(baseCost);
            Size = 1;
        }

		public int GetMovementCost() {
			return movementCost.GetValue();
		}

		public void Dispose() {
			// @TODO delete the graphic tile via events
		}

		public override  string ToString () {
			return "Tile (" + Row + ", " + Column + ", " + Layer + ")";
		}

        public EffectResult DispatchEffect(IEffect effect, int suffix) {
            return effect.Apply(this, suffix);
        }
        public void DispatchActivation(ITargetSelector selector, int suffix) {
            selector.Activate(this, 1, suffix);
        }
        public void DispatchTermination(ITargetSelector selector, int suffix) {
            selector.Terminate();
        }
    }
}