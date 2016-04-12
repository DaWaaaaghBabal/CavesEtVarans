using System;
using System.Collections.Generic;
using CavesEtVarans.battlefield;
using UnityEngine;
using CavesEtVarans.graphics;

namespace CavesEtVarans.gui {
	public abstract class HighlightStrategy {
		protected HashSet<Tile> highlightedArea;
		protected int maxHeightDelta;

		public HighlightStrategy(GameObject wallPrefab) {
			maxHeightDelta = 1;
			highlightedArea = new HashSet<Tile>();
		}

		public void HighlightArea(HashSet<Tile> area) {
			highlightedArea.UnionWith(area);
			foreach(Tile t in area) {
				WallTile(t);
			}
		}

		/// <summary>
		/// Returns the positions of all neighbours of any tile.
		/// should return an int [N][4] where N is the number of neighbours 
		/// a tile is expected to have (4 in a square grid, 8 if you count diagonals, 6 in a hex grid).
		/// Each 3-int array should be {row offset, column offset, offset angle, wall angle}.
		/// </summary>
		/// <returns></returns>
		public abstract int[][] NeighbourDirections();

		private void WallTile(Tile t) {
			foreach(int[]direction in NeighbourDirections()) {
				TestEdge(t, direction);
			}
		}

		protected void TestEdge(Tile tile, int[] direction) {
			bool isLinked = false;
			int row = tile.Row + direction[0];
			int column = tile.Column + direction[1];
			Tile[] stack = Battlefield.GetStack(row, column);
			foreach (Tile t in stack) {
				if (t != null) {
					isLinked |= highlightedArea.Contains(t) && Math.Abs(t.Layer - tile.Layer) <= maxHeightDelta;
				}
			}
			
			if (!isLinked)
				HighlightEdge(tile, direction);
		}

		private void HighlightEdge(Tile tile, int[] direction) {
			GraphicTile graphicsTile = GraphicBattlefield.GetSceneTile(tile);
            graphicsTile.HighlightEdge(direction[2]);
		}
		
		public void ClearHighlightedArea() {
			foreach (Tile t in highlightedArea) {
                GraphicBattlefield.GetSceneTile(t).ClearEdges();
            }
			highlightedArea = new HashSet<Tile>();
		}
	}

	public class SquareHighlighter : HighlightStrategy {
		public SquareHighlighter(GameObject wallPrefab) : base(wallPrefab) { }

		public override int[][] NeighbourDirections() {
			return new int[][] { 
				new int[] { 1, 0, 1 },		// row + 1, column : up
				new int[] { -1, 0, 3 },	// row - 1, column : down
				new int[] { 0, 1, 0 },		// row, column + 1 : right
				new int[] { 0, -1, 1 }	// row, column - 1 : left
			};
		}
	}

	public class HexHighlighter : HighlightStrategy {
		public HexHighlighter(GameObject wallPrefab) : base(wallPrefab) { }

		public override int[][] NeighbourDirections() {
			return new int[][] {
				new int[] { 0, 1, 0 },		// row, column + 1 : right
				new int[] { 0, -1, 3 },		// row, column - 1 : left
				new int[] { 1, 1, 1 },		// row + 1, column + 1 : up, right
				new int[] { -1, -1, 4 },	// row - 1, column - 1 : down, left
				new int[] { 1, 0, 2 },		// row + 1, column : up, left
				new int[] { -1, 0, 5 },		// row - 1, column : down, right
			};
		}
	}
}
