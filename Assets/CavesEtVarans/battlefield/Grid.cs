using CavesEtVarans.rules;
using CavesEtVarans.utils;
using System.Collections.Generic;
using System;
using CavesEtVarans.skills.target;

namespace CavesEtVarans.battlefield {
	public abstract class Grid<T> where T : ICoordinates {

		protected CubeArray<T> content;
		protected int heightDivisor;

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
        public abstract HashSet<T> GetArea(ICoordinates tile, int radius);
		
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
		}

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