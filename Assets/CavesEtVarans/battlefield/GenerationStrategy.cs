using CavesEtVarans.rules;
using System;
using UnityEngine;

namespace CavesEtVarans.battlefield {
	public abstract class GenerationStrategy {
		public void GenerateMap(Texture2D heightMap, CreateTile CreateTile) {
			for (int row = 0 ; row < heightMap.height ; row++) {
				for (int column = 0 ; column < heightMap.width ; column++) {
                    Color col = heightMap.GetPixel(column, row);
                    int layer = (int) (col.b * 10);
					if(col.a > 0.2)
                        PlaceTile(row, column, layer, heightMap, CreateTile);
				}
			}
		}

		protected abstract void PlaceTile(int row, int column, int layer, Texture2D heightMap, CreateTile createTile);
	}
	public class SquareGenerator : GenerationStrategy {
		protected override void PlaceTile(int row, int column, int layer, Texture2D heightMap, CreateTile createTile) {
			createTile(row, column, layer, RulesConstants.BASE_MOVEMENT_COST);
		}
	}

	public class HexGenerator : GenerationStrategy {
		protected override void PlaceTile(int row, int column, int layer, Texture2D heightMap, CreateTile createTile) {
            //int columnOffset = (heightMap.height - row) >> 1;
            int columnOffset = row >> 1;
            createTile(row, column + columnOffset, layer, RulesConstants.BASE_MOVEMENT_COST);
		}
	}
}