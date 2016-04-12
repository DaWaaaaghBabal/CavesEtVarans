using CavesEtVarans.rules;
using CavesEtVarans.utils;
using System.Collections.Generic;

namespace CavesEtVarans.battlefield {
	public abstract class Grid<T> where T : ICoordinates, utils.IDisposable {

		protected CubeArray<T> tiles;
		protected int heightDivisor;

		/// <summary> Returns the distance between two tiles from the point of view of the game,
		/// i.e ignoring Pythagoras' theorem and using the grid abstraction.
		///This distance is used everywhere a range is required.
		/// </summary>
		public abstract int GameDistance(T a, T b);
		/// <summary>Returns all tiles within a distance of radius from given tile.
		/// Uses the game's definition of distance.
		/// </summary>
		public abstract HashSet<T> GetArea(T tile, int radius);
		
		/// <summary>
		/// Returns true if the two tiles are adjacent. A tile is not adjacent to itself. 
		/// This method doesn't take height / layer into account, making it unsuitable to check game distances.
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <returns></returns>
		public abstract bool Adjacent(T t1, T t2);
		
		public Grid() {
			tiles = new CubeArray<T>(15, 15, 10);
			heightDivisor = RulesConstants.HEIGHT_DIVISOR;
		}

		public void AddTile(T tile) {
			tiles.Add(tile, tile.Row, tile.Column, tile.Layer);
		}
		public T Get(int row, int column, int layer) {
            if (row >= tiles.Rows | row < 0 | column >= tiles.Columns | column < 0 | layer >= tiles.Layers | layer < 0) return default(T);
			return tiles.Get(row, column, layer);
		}

		public T[] GetStack(int row, int column) {
            if (row >= tiles.Rows | row < 0 | column >= tiles.Columns | column < 0) return new T[0];
            return tiles.Get(row, column, Axis.Layer);
		}

		public void RemoveTile(T t) {
			tiles.Remove(t);
		}
		public void RemoveTile(int row, int column, int layer) {
			tiles.Remove(row, column, layer);
		}

        protected void SelectTile(HashSet<T> result, int row, int column, int layer)
        {
            T tile = tiles.Get(row, column, layer);
            if (tile != null)
                result.Add(tile);
        }

        protected void SelectTile(HashSet<T> result, float row, float column, float layer)
        {
            SelectTile(result, (int)row, (int)column, (int)layer);
        }

        /// <summary>
        /// Returns an UNORDERED set of all tiles traversed by the line from one tile to another. 
        /// The algorithm is a "super-supercover" of sorts : if the line passes through a corner, we return all 8
        /// tiles that intersect at that corner (4 on one layer, + maybe 4 on the above layer). 
        /// @TODO Corners block LoS, that's not a very permissive rule, perhaps tweak it a bit ?
        /// </summary>
        public HashSet<T> Line(T startTile, T endTile) {
            float distance = 2 * GameDistance(startTile, endTile);
            HashSet<T> result = new HashSet<T>();
            int dR = endTile.Row - startTile.Row;
            int dC = endTile.Column - startTile.Column;
            int dL = endTile.Layer - startTile.Layer;

            // We consider coordinates to be the center of the corresponding tile, 
            // and draw lines center to center, hence the offset.
            float offset = 0.5f;
            float mso = 0.001f; // multisampling offset
            for (int i = 0; i <= distance; i++)
            {
                float row = offset + startTile.Row + dR * i / distance;
                float column = offset + startTile.Column + dC * i / distance;
                float layer = offset + startTile.Layer + dL * i / distance;
                //@TODO hideous...
                SelectTile(result, row + mso, column + mso, layer + mso);
                SelectTile(result, row + mso, column + mso, layer - mso);
                SelectTile(result, row + mso, column - mso, layer + mso);
                SelectTile(result, row + mso, column - mso, layer - mso);
                SelectTile(result, row - mso, column + mso, layer + mso);
                SelectTile(result, row - mso, column + mso, layer - mso);
                SelectTile(result, row - mso, column - mso, layer + mso);
                SelectTile(result, row - mso, column - mso, layer - mso);
            }
                
            return result;
        }

	}
}