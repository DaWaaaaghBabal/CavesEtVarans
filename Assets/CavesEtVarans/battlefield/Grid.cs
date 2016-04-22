using CavesEtVarans.rules;
using CavesEtVarans.utils;
using System.Collections.Generic;
using System;
using CavesEtVarans.skills.target;

namespace CavesEtVarans.battlefield {
	public abstract class Grid<T> where T : ICoordinates {

		protected CubeArray<T> content;
		protected int heightDivisor;
		protected int[][] ringDirections;

		/// <summary> Returns the distance between two tiles from the point of view of the game,
		/// i.e ignoring Pythagoras' theorem and using the grid abstraction.
		/// </summary>
		public int GameDistance(ICoordinates a, ICoordinates b) {
            int dH = Math.Abs(a.Layer - b.Layer);
            return PlaneDistance(a, b) + dH / heightDivisor;
        }

        /// <summary>
        /// Returns the distance between two tiles, ignoring the height difference.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public abstract int PlaneDistance(ICoordinates a, ICoordinates b);

        /// <summary>Returns all tiles within a distance of radius from given tile.
        /// Uses the LineOfSightChecker given as argument to decide whether to include
        /// a given tile based on line of sight.
        /// Uses the game's definition of distance.
        /// </summary>
        public HashSet<T> GetArea(ICoordinates center, int minRadius, int maxRadius) {
			HashSet<T> result = new HashSet<T>();
			for (int dH = 0 ; dH < heightDivisor * (maxRadius + 1) ; dH++) {
				int h = dH / heightDivisor;
				int minR = minRadius - h;
				int maxR = maxRadius -h;
				for (int r = minR ; r <= maxR ; r++) {
					SelectRing(center, result, dH, r);
					SelectRing(center, result, -dH, r);
				}
			}
			return result;
		}

		private void SelectRing(ICoordinates center, HashSet<T> result, int dH, int r) {
			int[] centerCoord = new int[] {center.Row, center.Column, center.Layer + dH };
			SelectRing(centerCoord, r, result);
		}

		/// <summary>
		/// Select a ring of tiles of a given radius around the center and adds them to the set given as argument.
		/// </summary>
		/// <param name="radius"></param>
		/// <param name="result"></param>
		protected void SelectRing(int[] centerCoord, int radius, HashSet<T> result) {
			
			// We start on the right then select all segments of the ring
			int row = centerCoord[0];
			int column = centerCoord[1] + radius;
			int layer = centerCoord[2];
			if (radius == 0) {
				Select(result, row, column, layer);
			} else {
				foreach (int[] direction in ringDirections) {
					int dR = direction[0];
					int dC = direction[1];
					for (int i = 0 ; i < radius ; i++) {
						row += dR;
						column += dC;
						Select(result, row, column, layer);
					}
				}
			}
		}

		/// <summary>
		/// Returns true if the two tiles are adjacent. A tile is not adjacent to itself. 
		/// This method doesn't take height / layer into account, making it unsuitable to check game distances.
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <returns></returns>
		public abstract bool Adjacent(ICoordinates t1, ICoordinates t2);
		
		public Grid() {
			content = new CubeArray<T>(15, 15, 10);
			heightDivisor = RulesConstants.HEIGHT_DIVISOR;
			ringDirections = InitRingDirections();
		}

		protected abstract int[][] InitRingDirections();

		public void Add(T t) {
			content.Add(t, t.Row, t.Column, t.Layer);
            //AddObstacle(new Obstacle(t, 0.0));
		}
        
        public T Get(int row, int column, int layer) {
            if (row >= content.Rows | row < 0 | column >= content.Columns | column < 0 | layer >= content.Layers | layer < 0) return default(T);
			return content.Get(row, column, layer);
		}

		public T[] GetStack(int row, int column) {
            if (row >= content.Rows | row < 0 | column >= content.Columns | column < 0) return new T[0];
            return content.Get(row, column, Axis.Layer);
		}

		public void Remove(T t) {
			content.Remove(t);
		}
		public void Remove(int row, int column, int layer) {
			content.Remove(row, column, layer);
		}

        protected void Select(HashSet<T> result, int row, int column, int layer)
        {
            T tile = content.Get(row, column, layer);
            if (tile != null)
                result.Add(tile);
        }
    }
}