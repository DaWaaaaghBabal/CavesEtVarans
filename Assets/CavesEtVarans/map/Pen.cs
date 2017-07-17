using CavesEtVarans.battlefield;

namespace CavesEtVarans.map {
    public abstract class Pen {
        protected int[][] directions;
        protected int currentDirection;
        protected int row, column;
    
        public Pen() {
            directions = HexOrSquare.ProvideRingDirections();
            currentDirection = 0;
            row = 0;
            column = 0;
        }

        protected void Orient() {
            // Orientation is determined by position. We determine in which hexant we are, then orient to follow the corresponding side of an hexagon.
            // Diagonals are treated by prioritising the "leftiest" direction, i.e. the direction that is on the counterclockwise side of the diagonal.
            // Because of the order of the direction array, 
            // 0 = 7 o'clock, 1 = 9 o'clock, 2 = 11 o'clock, 3 = 1 o'clock, 4 = 3 o'clock and 5 = 5 o'clock.
            if (row == 0) {
                if (column > 0)
                    currentDirection = 2; // We're on the 3 o'clock axis, so we face 11 o'clock.
                else currentDirection = 5; // 9 o'clock axis, facing 5 o'clock.
            } else if (column == 0) {
                if (row > 0)
                    currentDirection = 0; // 11 o'clock axis, facing 7 o'clock.
                else currentDirection = 3; // 5 o'clock axis, facing 1 o'clock.
            } else if (row == column) {
                if (row > 0)
                    currentDirection = 1; // 1 axis, facing 9.
                else currentDirection = 4; // 7 axis, facing 3.
            } else if (row > 0) { // Diagonals are treated, now for the hexants.
                if (column > row)
                    currentDirection = 2;// 1-3 hexant, facing 11 o'clock.
                else if (column > 0)
                    currentDirection = 1; // 11-1 hexant, facing 9.
                else currentDirection = 0; // 9-11 hexant, facing 7.
            } else {
                if (column < row)
                    currentDirection = 5; // 7-9, facing 5.
                else if (column < 0)
                    currentDirection = 4; // 5-7, facing 3.
                else currentDirection = 3; // 3-5, facing 1.
            }
        }

        /// <summary>
        /// Calculates a direction based on the current orientation and a number of steps.
        /// </summary>
        /// <param name="steps">Number of steps to turn. Negative = left, positive = right.</param>
        /// <returns></returns>
        protected int[] Turn(int steps) {
            return directions[((currentDirection + steps) % 6 + 6) % 6];
        }

        public void MoveTo(int[] space) {
            row = space[0];
            column = space[1];
        }

        protected int[] GetNeighbour(int[] direction) {
            return new int[] { row + direction[0], column + direction[1] };
        }
    }
}