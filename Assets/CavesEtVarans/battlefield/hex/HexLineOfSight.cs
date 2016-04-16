using System;

namespace CavesEtVarans.battlefield {
    public class HexLineOfSight : LineOfSightStrategy {
        public HexLineOfSight(Grid<Obstacle> obstacles) : base(obstacles) { }

        public override int[] GridCoordinates(double[] coord) {
            int row, column, layer = (int)Math.Round(coord[2]);
            double X = coord[0];
            double Y = coord[1];
            /* First we need to check which row we're in.
             * We consider that a pointy-top hexagon has a width of exactly 1,
             * which means a height of sqrt(3/4).
             */
            double rowHeight = Math.Sqrt(3/4.0);
            row = (int) Math.Round(Y / rowHeight);

            /* Now that we have the row, we can get the column.
             * We need to offset X by -cos 120° on each row, as the grid is sheared.
             */
            column = (int) Math.Round(X + row * .5);

            /* We have assigned the point to a rectangle that is centered on the hexagon
             * and has height = sqrt(3/4) and width = 1. The top and bottom points of the hexagon
             * are out of this square, and the corners of that rectangle are out of the hexagon.
             * We need to reassign the corners to the neighbouring hexes.
             * That will take care of the hexagon points, which are actually the corners of a neighbouring rectangle.
             */
            double dX = X - Math.Round(X);
            double dY = Y - row * rowHeight;
            double pointHeight = Math.Sqrt(1/3.0);
            if (dY >= pointHeight * (1 - dX)) { // upper right corner
                row++; column++;
            } else if (dY > pointHeight * (1 + dX)) { // upper left
                row++;
            } else if (dY <= pointHeight * (-1 + dX)) { // lower right
                row--;
            } else if (dY < pointHeight * (-1 - dX)) { // lower left
                row--; column--;
            }

            return new int[] { row, column, layer };
        }

        public override double[] CalculateCoordinates(ICoordinates c, int height) {
            // Pointy-topped hexagons with width = 1, so the unit vectors are X = (1, 0, 0), Y (cos 120, sin 120, 0) and Z (0, 0, 1)
            double cos120 = -0.5f;
            double sin120 = Math.Sqrt(3) / 2;
            double x = c.Column + c.Row * cos120;
            double y =            c.Row * sin120;
            double z = c.Layer + height;
            return new double[] { x, y, z };
        }
    }
}