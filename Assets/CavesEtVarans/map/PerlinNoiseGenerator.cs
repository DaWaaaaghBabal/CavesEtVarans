using CavesEtVarans.utils;
using System;

namespace CavesEtVarans.map {
    public class PerlinNoiseGenerator {

        private double[][] gradients = new double[][] {
            new double[]{-1, -1, 0}, new double[]{-1, 1, 0}, new double[]{1, -1, 0}, new double[]{1, 1, 0},
            new double[]{-1, 0, -1}, new double[]{-1, 0, 1}, new double[]{1, 0, -1}, new double[]{1, 0, 1},
            new double[]{0, -1, -1}, new double[]{0, -1, 1}, new double[]{0, 1, -1}, new double[]{0, 1, 1},
        };

        private Hasher hasher;
        public PerlinNoiseGenerator(int seed) {
            hasher = new Hasher(seed);
        }
        public double PerlinNoise(double x, double y, double z, double noiseSize, int depth) {
            double sample = 0;
            int multiplier = 1;
            for (int i = 0; i < depth; i++) {
                sample += SampleFromGrid(new double[] { multiplier * x / noiseSize, multiplier * y / noiseSize, multiplier / noiseSize }) / multiplier;
                multiplier *= 2;
            }
            return sample;
        }

        private double SampleFromGrid(double[]xyz) {
            //step 1 : figure out in which cell of the grid we fall.
            int[] cellXYZ = new int[] {
                (int) Math.Floor(xyz[0]),
                (int) Math.Floor(xyz[1]),
                (int) Math.Floor(xyz[2])};
            double[] deltaXYZ = new double[] {
                xyz[0]-cellXYZ[0],
                xyz[1]-cellXYZ[1],
                xyz[2]-cellXYZ[2],
            };
            // We iterate on the 8 corners of a cubic cell and store the dot products in a 3D array.
            double[,,] dots = new double[2, 2, 2];
            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < 2; j++) {
                    for (int k = 0; k < 2; k++) {
                        int[] corner = new int[] {
                            cellXYZ[0] + i,
                            cellXYZ[1] + j,
                            cellXYZ[2] + k };
                        double[] distance = new double[] {
                            deltaXYZ[0] - i,
                            deltaXYZ[1] - j,
                            deltaXYZ[2] - k, };
                        int hash = hasher.Hash(corner[0], corner[1], corner[2]);
                        double[] gradient = gradients[hash % 12];
                        dots[i, j, k] = DotProduct(distance, gradient);
                    }
                }
            }
            return Interpolate(deltaXYZ, dots);
        }

        private double DotProduct(double[] vect1, double[] vect2) {
            return vect2[0] * vect1[0] + vect2[1] * vect1[1] + vect2[2] * vect1[2];
        }

        private double Interpolate(double[] deltaXYZ, double[,,] dots) {
            //Starting with the 4 X-aligned segments of the cube :
            double[,] interX = new double[2, 2];
            interX[0, 0] = Interpolate(dots[0, 0, 0], dots[1, 0, 0], deltaXYZ[0]);
            interX[0, 1] = Interpolate(dots[0, 0, 1], dots[1, 0, 1], deltaXYZ[0]);
            interX[1, 0] = Interpolate(dots[0, 1, 0], dots[1, 1, 0], deltaXYZ[0]);
            interX[1, 1] = Interpolate(dots[0, 1, 1], dots[1, 1, 1], deltaXYZ[0]);
            // We now have a square, interpolate on the 2 Y-aligned segments :
            double[] interY = new double[2];
            interY[0] = Interpolate(interX[0, 0], interX[1, 0], deltaXYZ[1]);
            interY[1] = Interpolate(interX[0, 1], interX[1, 1], deltaXYZ[1]);
            // Finish on the Z-aligned segment and return :
            return Interpolate(interY[0], interY[1], deltaXYZ[2]);
        }

        private double Interpolate(double v1, double v2, double t) {
            double interpolator = 6 * Math.Pow(t, 5) - 15 * Math.Pow(t, 4) + 10 * Math.Pow(t, 3);
            return v1 + interpolator * (v2 - v1);
        }
    }
}