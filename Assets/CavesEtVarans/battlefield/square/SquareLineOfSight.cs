using System;

namespace CavesEtVarans.battlefield {
    internal class SquareLineOfSight : LineOfSightStrategy {
        public SquareLineOfSight(Grid<Obstacle> obstacles) : base(obstacles) { }

        public override int[] GridCoordinates(double[] coord) {
            int[] gridCoord = new int[coord.Length];
            for (int i = 0 ; i < coord.Length ; i++)
                gridCoord[i] = (int) Math.Round(coord[i]);
            return gridCoord;
        }

        public override double[] CalculateCoordinates(ICoordinates c, int height) {
            return new double[] { c.Row, c.Column, c.Layer + height };
        }
    }
}