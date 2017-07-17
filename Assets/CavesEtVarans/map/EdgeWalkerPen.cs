using System;
using CavesEtVarans.utils;

namespace CavesEtVarans.map {
    class EdgeWalkerPen : Pen {
        private DualKeyDictionary<int, int, int> edge;
        private DualKeyDictionary<int, int, Region> grid;

        public DualKeyDictionary<int, int, int> GenerateEdge(DualKeyDictionary<int, int, Region> grid) { 
            this.grid = grid;
            edge = new DualKeyDictionary<int, int, int>();
            // The first step is to find an edge anywhere, so we advance along the row
            while (IsOccupied(row, column + 1))
                column++;
            bool oriented = false;
            // Once we're on the edge, facing the void (and staring into the abyss), we turn left until we face another edge tile. This is our starting orientation.
            // We're turning counter-clockwise from facing 3 o'clock (i.e. direction 4), hence the array order.
            foreach (int i in new int[] { 3, 2, 1, 0, 5 }) {
                int nR = row + directions[i][0];
                int nC = column + directions[i][1];
                if (IsOccupied(nR, nC) && ! oriented) {
                    currentDirection = i;
                    oriented = true; // now that we've found a tile, no need to turn again.
                }
            }
            while (!Finished()) {
                Step();
            }
            return edge;
        }

        private bool Finished() {
            // We're finished when we're on a position we have already passed through AND we're on the same direction as on our previous passage.
            return edge.ContainsKeys(row, column) && edge[row][column] == currentDirection;
        }

        private void Step() {
            /* We arrived on space A from space B. We are aligned with the BA vector, so B is right behind us. We want to walk the edge
             * so we need to be as far from the center as we can ; we will go in a counter-clockwise spiral, trying to be as far on the right as we can the whole time.
             */
            edge.Add(row, column, currentDirection);
            bool hasMoved = false;
            for (int i = 1; i > -4 && !hasMoved; i--) {
                int[] neighbourCoord = GetNeighbour(Turn(i));
                int nR = neighbourCoord[0], nC = neighbourCoord[1];
                if (
                    IsOccupied(nR, nC) 
                    && IsEdge(nR, nC)
                ) {
                    MoveTo(neighbourCoord);
                    currentDirection = ((currentDirection + i) % 6 + 6) % 6;
                    hasMoved = true;
                }
            }
        }

        private bool IsEdge(int row, int column) {
            foreach (int[] direction in directions) {
                int nR = row + direction[0], nC = column + direction[1];
                if (!IsOccupied(nR, nC)) return true;
            }
            return false;
        }

        private bool IsOccupied(int row, int column) {
            return grid.ContainsKeys(row, column);
        }
    }
}
