using CavesEtVarans.map;
using CavesEtVarans.rules;
using System;
using UnityEngine;

namespace CavesEtVarans.battlefield {
	public abstract class GenerationStrategy : IMapGenerator {
        public Texture2D HeightMap { set; get; }

        public void GenerateMap(CreateTile CreateTile) {
            PerlinNoiseGenerator noiseGenerator = new PerlinNoiseGenerator();
            noiseGenerator.Seed = 197;
			for (int row = 0 ; row < HeightMap.height ; row++) {
				for (int column = 0 ; column < HeightMap.width ; column++) {
                    Color col = HeightMap.GetPixel(column, row);
                    int layer = (int) (col.b * 10);
                    //double noise = (int)noiseGenerator.PerlinNoise(column, row, 0, 10, 3);
                    //int layer = (int)noise * 10;
					if(col.a > 0.2)
                        PlaceTile(row, column, layer, CreateTile);
				}
			}
		}

		protected abstract void PlaceTile(int row, int column, int layer, CreateTile createTile);
	}
	public class SquareGenerator : GenerationStrategy {
		protected override void PlaceTile(int row, int column, int layer, CreateTile createTile) {
			createTile(row, column, layer, LandType.PLAIN);
		}
	}

	public class HexGenerator : GenerationStrategy {
		protected override void PlaceTile(int row, int column, int layer, CreateTile createTile) {
            int columnOffset = row >> 1; // Offset column by 1 every second row to bring the grid from a lozange to a rectangle
            createTile(row, column + columnOffset, layer, LandType.PLAIN);
		}
	}
}