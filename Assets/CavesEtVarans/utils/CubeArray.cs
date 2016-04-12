using System;
using System.Collections.Generic;

namespace CavesEtVarans.utils
{
	/* A CubeArray is a 3-dimensional array of elements.
     * An element can be stored at any (x,y,z) tuple. 
     * The array can return a plane, a line or a column, depending on the number of parameters provided.
     * int x, Axis axis : plane normal to the specified axis, with coordinate on that axis = x.
     * int x, int y, Axis axis : line on the specified axis, with the two other coordinates = x and y.
     * int x, int y, int z : element at the specified coordinates.
     */ 
	public class CubeArray<T> where T : IDisposable
	{
		private T[,,] content;

		public int Rows { set; get; }
        public int Columns { set; get; }
        public int Layers { set; get; }
		private Dictionary<Axis, int[,]>[] permutationMatrixes;

		// Initializes the cube with initial sizes on all directions. These dimensions should be as close as possible
		// to the actual dimensions of the data to insert to limit rewrites.
		public CubeArray (int initRows, int initColumns, int initLayers)
		{
			Rows = initRows;
			Columns = initLayers;
			Layers = initColumns;
			InitCube ();

			permutationMatrixes = new Dictionary<Axis, int[,]>[2];
			InitPermutationMatrixes ();
		}

		private void InitCube ()
		{
			content = new T[Rows, Columns, Layers];
		}

		// Returns {x,y,z} of the given element.
		public int[] GetPositionOf (T item)
		{
			for (int i = 0; i < Rows; i++) {
				for (int j = 0; j < Columns; j++) {
					for (int k = 0; k < Layers; k++) {
						if (item.Equals (content [i, j, k])) {
							return new int[3]{i,j,k};
						}
					}
				}
			}
			return null;
		}

		// Will dynamically extend the cube if needed.
		public void Add (T t, int row, int column, int layer)
		{
            if (t != null)
            {
                if (row >= Rows || column >= Columns || layer >= Layers)
                {
                    int oldRows = Rows, oldColumns = Columns, oldLayers = Layers;
                    Rows = Math.Max(row + 1, Rows);
                    Columns = Math.Max(column + 1, Columns);
                    Layers = Math.Max(layer + 1, Layers);
                    T[,,] oldContent = content;
                    InitCube();
                    CopyContent(oldContent, oldRows, oldColumns, oldLayers);
                }

                if (content[row, column, layer] != null)
                {
                    content[row, column, layer].Dispose();
                }

                content[row, column, layer] = t;
            }
		}

		// Copies all elements from the specified cube in this one. Will extend the cube if needed.
		public void Add (CubeArray<T> cube)
		{
			T[,,] newContent = cube.content;
			CopyContent (newContent, cube.Rows, cube.Columns, cube.Layers);
		}

		private void CopyContent (T[, ,] newContent, int contentRows, int contentColumns, int contentLayers)
		{
			// Iterate backwards. This way, we add the highest index first, which allows us to limit the number of reallocations.
			for (int i = contentRows - 1; i >= 0; i--) {
				for (int j = contentColumns - 1; j >= 0; j--) {
					for (int k = contentLayers - 1; k >= 0; k--) {
						Add (newContent [i, j, k], i, j, k);
					}
				}
			}
		}

		public void Remove(int row, int column, int layer) {
			if (row < Rows && row >= 0 && column < Columns && column >= 0 && layer < Layers && column >= 0) {
                if (content[row, column, layer] != null) content[row, column, layer].Dispose();
                content[row, column, layer] = default(T);
			}			
		}
		public void Remove(T t) {
			int[] p = GetPositionOf(t);
			if (p != null) content[p[0], p[1], p[2]] = default(T);
		}

		// Returns a cell.
		public T Get (int row, int column, int layer)
		{
			if (row >= Rows || row < 0 || column >= Columns || column < 0 || layer >= Layers || layer < 0) {
				return default(T);
			}
			return content [row, column, layer];
		}
        /// <summary>
		/// Returns a plane. The provided Axis is used as the normal to the plane :
        /// Get(n, Axis.Row) will return all columns and layers in the nth row
        /// Get(n, Axis.Column) will return all rows and layers in the nth column
        /// Get(n, Axis.Layer) will return all rows and columns in the nth layer
        /// </summary>
        /// <param name="index"></param>
        /// <param name="normal"></param>
        /// <returns></returns>
		public T[,] Get (int index, Axis normal)
		{
			int[,] planeDimensions = GetPlaneDimensions (normal);
			int planeLength = planeDimensions [0, 0], planeWidth = planeDimensions [1, 0];
			int[,] coord;
			T[,] result = new T[planeLength, planeWidth];
			for (int i = 0; i < planeLength; i++) {
				for (int j = 0; j < planeWidth; j++) {
					coord = ArrangePlaneCoord (i, j, index, normal);
					result [i, j] = content [coord [0, 0], coord [1, 0], coord [2, 0]];
				}
			}
			return result;
		}
		// Returns a line. The provided Axis is used as the axis of the line:
		// If Axis.X is provided, the plane will be the subset of the cube where y = index1 and z = index2.
		public T[] Get (int index1, int index2, Axis normal)
		{
			int lineLength = GetLineLength (normal);
			int[,] coord;
			T[] result = new T[lineLength];
			for (int i = 0; i < lineLength; i++) {
				coord = ArrangeLineCoord (i, index1, index2, normal);
				result [i] = content [coord [0, 0], coord [1, 0], coord [2, 0]];
			}
			return result;
		}
		private int[,] GetPlaneDimensions (Axis normal)
		{
			int[,] matrix = new int[,] { { Rows }, { Columns }, { Layers } };
			return permutationMatrixes [0] [normal].MatrixProduct (matrix);
		}
		private int[,] ArrangePlaneCoord (int i, int j, int fixedCoord, Axis normal)
		{
			int[,] matrix = new int[,] { { i }, { j }, { fixedCoord } };
			return permutationMatrixes [0] [normal].MatrixProduct (matrix);
		}
		private int GetLineLength (Axis axis)
		{
			int[,] matrix = new int[,] { { Rows }, { Columns }, { Layers } };
			return permutationMatrixes [1] [axis].MatrixProduct (matrix) [1, 0];
		}
		private int[,] ArrangeLineCoord (int i, int fixedCoord1, int fixedCoord2, Axis normal)
		{
			int[,] matrix = new int[,] { { i }, { fixedCoord1 }, { fixedCoord2 } };
			return permutationMatrixes [1] [normal].MatrixProduct (matrix);
		}

		private void InitPermutationMatrixes ()
		{

			// Permutation matrixes used to extract a plane.
			permutationMatrixes [0] = new Dictionary<Axis, int[,]> ();
			permutationMatrixes [0].Add (Axis.Row, new int[,] { 
                { 0, 1, 0 }, 
                { 0, 0, 1 }, 
                { 1, 0, 0 } }); // pM[0][Row] * {R, C, L} = {C, L, R}
			permutationMatrixes [0].Add (Axis.Column, new int[,] { 
                { 1, 0, 0 }, 
                { 0, 0, 1 }, 
                { 0, 1, 0 } }); // pM[0][Column] * {R, C, L} = {R, L, C}
			permutationMatrixes [0].Add (Axis.Layer, new int[,] { 
                { 1, 0, 0 }, 
                { 0, 1, 0 }, 
                { 0, 0, 1 } }); // pM[0][Layer] * {R, C, L} = {R, C, L}

            // Permutation matrixes used to extract a line.
            permutationMatrixes [1] = new Dictionary<Axis, int[,]> ();
			permutationMatrixes [1].Add (Axis.Row, new int[,] { 
                { 1, 0, 0 }, 
                { 0, 1, 0 }, 
                { 0, 0, 1 } });
			permutationMatrixes [1].Add (Axis.Column, new int[,] { 
                { 0, 1, 0 }, 
                { 1, 0, 0 }, 
                { 0, 0, 1 } });
			permutationMatrixes [1].Add (Axis.Layer, new int[,] { 
                { 0, 1, 0 }, 
                { 0, 0, 1 }, 
                { 1, 0, 0 } });
		}
	}
}
