using System;
using System.Collections.Generic;

namespace CavesEtVarans.battlefield {
    public abstract class LineOfSightStrategy {
        protected Grid<Obstacle> obstacles;
        public LineOfSightStrategy(Grid<Obstacle> obstacles) {
            this.obstacles = obstacles;
        }

        public Line<ICoordinates> LineOfSight(ICoordinates start, int startHeight, ICoordinates end, int endHeight) {
            double[] startCoord = CalculateCoordinates(start, startHeight);
            double[] endCoord = CalculateCoordinates(end, endHeight);
            double distance = LineLength(start, end);

            double[] delta = new double[3];
            for (int i = 0 ; i < 3 ; i++) delta[i] = endCoord[i] - startCoord[i];

            // we don't want to include the start and end tile : the line is *between* two points
            Line<ICoordinates> result = new Line<ICoordinates>((int)distance-1);
            for (int i = 1 ; i < distance ; i++) {
                double ratio = i / distance;
                double[] coord = new double[3];
                for (int j = 0 ; j < 3 ; j++)
                    coord[j] = startCoord[j] + delta[j] * ratio;
                List<ICoordinates> sample = Sample(coord);
                result.Add(sample, i - 1);
            }

            return result;
        }
        
        private List<ICoordinates> Sample(double[] coord) {
            List<ICoordinates> result = new List<ICoordinates>();
            double[] mso = new double[] { -.001, .001 };
            foreach (double i in mso) {
                foreach (double j in mso) {
                    foreach (double k in mso) {
                        double[] overSampleCoord = new double[] {coord[0] + i, coord[1] + j, coord[2] + k};
                        int[] gridCoord = GridCoordinates(overSampleCoord);
                        Select(result, gridCoord[0], gridCoord[1], gridCoord[2]);
                    }
                }
            }
            return result;
        }

        public void Add(Obstacle obstacle) {
            obstacles.Add(obstacle);
        }

        private void Select(List<ICoordinates> result, int row, int column, int layer) {
            Obstacle obstacle = obstacles.Get(row, column, layer);
            if (obstacle == null) obstacle = Obstacle.Empty;
            if (!result.Contains(obstacle))
                result.Add(obstacle);
        }

        private double LineLength(ICoordinates tile1, ICoordinates tile2) {
            int dH = Math.Abs(tile1.Layer - tile2.Layer);
            return Math.Max(obstacles.PlaneDistance(tile1, tile2), dH);
        }

        /// <summary>
        /// Returns the grid coordinates for a point given by its cartesian coordinates.
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public abstract int[] GridCoordinates(double[] coord);

        /// <summary>
        /// Returns the cartesion coordinates of the center of a tile, offset vertically
        /// depending on the height of what is standing on it.
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public abstract double[] CalculateCoordinates(ICoordinates tile, int height);
    }
}