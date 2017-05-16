using System;
using UnityEngine;
using UnityEngine.UI;
using RNG = UnityEngine.Random;

namespace CavesEtVarans.map {
    public class PerlinNoiseGenerator {
        public int Seed { set; get; }

        // Hash lookup table as defined by Ken Perlin.  This is a randomly arranged array of all numbers from 0-255 inclusive.
        // @TODO instead of hardcoding a table, randomize it from seed.
        private int[] lookupTable = {
            151, 160, 137, 91, 90, 15, 131, 13, 201, 95, 96, 53, 194, 233, 7, 225,
            140, 36, 103, 30, 69, 142, 8, 99, 37, 240, 21, 10, 23, 190, 6, 148,
            247, 120, 234, 75, 0, 26, 197, 62, 94, 252, 219, 203, 117, 35, 11, 32,
            57, 177, 33, 88, 237, 149, 56, 87, 174, 20, 125, 136, 171, 168, 68, 175,
            74, 165, 71, 134, 139, 48, 27, 166, 77, 146, 158, 231, 83, 111, 229, 122,
            60, 211, 133, 230, 220, 105, 92, 41, 55, 46, 245, 40, 244, 102, 143, 54,
            65, 25, 63, 161, 1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169, 200,
            196, 135, 130, 116, 188, 159, 86, 164, 100, 109, 198, 173, 186, 3, 64, 52,
            217, 226, 250, 124, 123, 5, 202, 38, 147, 118, 126, 255, 82, 85, 212, 207,
            206, 59, 227, 47, 16, 58, 17, 182, 189, 28, 42, 223, 183, 170, 213, 119, 248,
            152, 2, 44, 154, 163, 70, 221, 153, 101, 155, 167, 43, 172, 9, 129, 22,
            39, 253, 19, 98, 108, 110, 79, 113, 224, 232, 178, 185, 112, 104, 218, 246,
            97, 228, 251, 34, 242, 193, 238, 210, 144, 12, 191, 179, 162, 241, 81, 51,
            145, 235, 249, 14, 239, 107, 49, 192, 214, 31, 181, 199, 106, 157, 184, 84,
            204, 176, 115, 121, 50, 45, 127, 4, 150, 254, 138, 236, 205, 93, 222, 114,
            67, 29, 24, 72, 243, 141, 128, 195, 78, 66, 215, 61, 156, 180
        };

        private double[][] gradients = new double[][] {
            new double[]{-1, -1, 0}, new double[]{-1, 1, 0}, new double[]{1, -1, 0}, new double[]{1, 1, 0},
            new double[]{-1, 0, -1}, new double[]{-1, 0, 1}, new double[]{1, 0, -1}, new double[]{1, 0, 1},
            new double[]{0, -1, -1}, new double[]{0, -1, 1}, new double[]{0, 1, -1}, new double[]{0, 1, 1},
        };

        public double PerlinNoise(double x, double y, double z, double noiseSize, int depth) {
            double sample = 0;
            int multiplier = 1;
            for (int i = 0; i < depth; i++) {
                sample += SampleFromGrid(new double[] { Seed + multiplier * x / noiseSize, Seed + multiplier * y / noiseSize, Seed + multiplier / noiseSize }) / multiplier;
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
                        int hash = Hash(corner[0], corner[1], corner[2]);
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

        private int Hash(int gridX, int gridY, int gridZ) {
            int s = 255; // size of the hash table and period of the PRNG
            int hash = lookupTable[(gridX % s + s) % s] + gridY;
            hash = lookupTable[(hash % s + s) % s] + gridZ;
            return lookupTable[(hash % s + s) % s];
        }
    }
}