using CavesEtVarans.battlefield;
using CavesEtVarans.graphics;
using System;
using UnityEngine;

namespace CavesEtVarans.map {
    public class PerlinMapGenerator : IMapGenerator {

        public int Seed { set; get; }
        public int Radius { set; get; }
        public int NoiseSize { set; get; }
        public int Depth { set; get; }
        public int MaxAltitude { set; get; }
        public bool Island { set; get; }
        public double SeaToLand { set; get; }
        public double PlainToForest { set; get; }
        private PerlinNoiseGenerator altitudeGenerator, forestGenerator;
        private PlacementStrategy placementStrategy;

        public void GenerateMap(CreateTile createTile) {
            altitudeGenerator = new PerlinNoiseGenerator() { Seed = Seed };
            forestGenerator = new PerlinNoiseGenerator() { Seed = 2 * Seed };
            placementStrategy = new HexPlacement();
            // The map is generated in rings expanding from the center.
            for (int i = 1; i <= Radius; i++) {
                GenerateRing(Radius, Radius, i, (double)i / Radius, createTile);
            }
            PlaceTile(Radius, Radius, 0, createTile);
        }

        private void GenerateRing(int centerRow, int centerColumn, int radius, double edgeToCenter, CreateTile createTile) {
            int row = centerRow;
            int column = centerColumn + radius;
            foreach (int[] direction in HexOrSquare.ProvideRingDirections()) {
                int dR = direction[0];
                int dC = direction[1];
                for (int j = 0; j < radius; j++) {
                    row += dR;
                    column += dC;
                    PlaceTile(row, column, edgeToCenter, createTile);
                }
            }
        }

        private void PlaceTile(int row, int column, double edgeToCenter, CreateTile createTile) {
            Vector3 pos = placementStrategy.Place(row, column, 0);
            double heightNoise = (1 + altitudeGenerator.PerlinNoise(pos.x, pos.z, 0, NoiseSize, Depth)) / 2;
            heightNoise *= Island ? 1.5 * Math.Sqrt(1 - edgeToCenter) : 1;
            int layer = 0;
            LandType landType = LandType.SEA;
            if (heightNoise > SeaToLand) {
                layer = (int)((heightNoise - SeaToLand) * MaxAltitude);
                double forestNoise = (1 + forestGenerator.PerlinNoise(row, column, 0, NoiseSize, Depth)) / 2;
                landType = forestNoise - heightNoise < PlainToForest ?
                    LandType.PLAIN : LandType.FOREST;
            } 
            createTile(row, column, layer, landType);
        }

        private int DistanceToCenter(int row, int column, int centerRow, int centerColumn) {
            int dR = row - centerRow;
            int dC = column - centerColumn;
            int absR = Math.Abs(dR);
            int absC = Math.Abs(dC);
            int absZ = Math.Abs(dR - dC);
            return Math.Max(absZ, Math.Max(absR, absC));
        }
    }
}