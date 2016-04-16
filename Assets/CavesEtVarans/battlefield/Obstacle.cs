using CavesEtVarans.utils;

namespace CavesEtVarans.battlefield {
    public class Obstacle : ICoordinates, IDisposable {
        public int Column { get; private set; }
        public int Layer { get; private set; }
        public int Row { get; private set; }

        public double Cover { get; private set; }

        private static Obstacle empty = new Obstacle(0, 0, 0, 0);
        public static Obstacle Empty { get { return empty; } private set { } }

        public Obstacle(int row, int column, int layer, double coverage) {
            Row = row;
            Column = column;
            Layer = layer;
            Cover = coverage;
        }

        public Obstacle(Tile t, double coverage) {
            Row = t.Row;
            Column = t.Column;
            Layer = t.Layer;
            Cover = coverage;
        }

        public void Dispose() {
            // Do nothing yet;
        }

        public override string ToString() {
            return "Obstacle (" + Row + ", " + Column + ", " + Layer + "), " + 100 * (Cover) + "%";
        }
    }
}