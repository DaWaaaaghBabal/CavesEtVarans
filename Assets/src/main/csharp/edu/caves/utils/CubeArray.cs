using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    /* A CubeArray is a 3-dimensional array of elements.
     * An element can be stored at any (x,y,z) tuple. 
     * The array can return a plane, a line or a column, depending on the number of parameters provided.
     * int x, String axis : plane normal to the specified axis, with coordinate on that axis = x.
     * int x, int y, String axis : line on the specified axis, with the two other coordinates = x and y.
     * int x, int y, int z : element at the specified coordinates.
     */ 
    public class CubeArray<T> where T : IDisposable{
        private T[,,] content;

        private int length, height, width;
        public enum Axis {
            X, Y, Z
        }
        private Dictionary<Axis, int[,]>[] permutationMatrixes;

        // Initializes the cube with initial sizes on all directions. These dimensions chould be as close as possible
        // to the actual dimensions of the data to insert.
        public CubeArray(int initLength, int initWidth, int initHeight) {
            length = initLength;
            height = initHeight;
            width = initWidth;
            InitCube();

            permutationMatrixes = new Dictionary<Axis, int[,]>[2];
            InitPermutationMatrixes();
        }

        private void InitCube() {
            content = new T[length, width, height];
        }

        // Returns {x,y,z} of the given element.
        public int[] GetPositionOf(T item) {
            for (int i = 0; i < length; i++) {
                for (int j = 0; j < width; j++) {
                    for (int k = 0; k < height; k++) {
                        if (item.Equals(content[i, j, k])) {
                            return new int[3]{i,j,k};
                        }
                    }
                }
            }
            return null;
        }

        // Will dynamically extend the cube if needed.
        public void Add(T t, int x, int y, int z) {

            if (x >= length || y >= width || z >= height) {
                int oldLength = length, oldWidth = width, oldHeight = height;
                length = Math.Max(x+1, length);
                width = Math.Max(y+1, width);
                height = Math.Max(z + 1, height);
                T[, ,] oldContent = content;
                InitCube();
                CopyContent(oldContent, oldLength, oldWidth, oldHeight);
            }
            
            if (content[x,y,z] != null) {
                content[x,y,z].Dispose();
            }

            content[x, y, z] = t;
        }

        // Copies all elements from the specified cube in this one. Will extend the cube if needed.
        public void Add(CubeArray<T> cube) {
            T[,,] newContent = cube.content;
            CopyContent(newContent, cube.length, cube.width, cube.height);
        }

        private void CopyContent(T[, ,] newContent, int contentLength, int contentWidth, int contentHeight) {
            // Iterate backwards. This way, we add the highest index first, which allows us to limit the number of reallocations.
            for (int i = contentLength - 1; i >= 0; i--) {
                for (int j = contentWidth - 1; j >= 0; j--) {
                    for (int k = contentHeight - 1; k >= 0; k--) {
                        Add(newContent[i, j, k], i, j, k);
                    }
                }
            }
        }

        // Returns a cell.
        public T Get(int x, int y, int z) {
            if (x >= length || y >= width || z >= height) {
                return default(T);
            }
            return content[x, y, z];
        }

        // Returns a plane. The provided Axis is used as the normal to the plane:
        // If Axis.X is provided, the plane will be the subset of the cube where x = index.
        public T[,] Get(int index, Axis normal) {
            int[,] planeDimensions = GetPlaneDimensions(normal);
            int planeLength = planeDimensions[0,0], planeWidth = planeDimensions[1,0];
            int[,]coord;
            T[,] result = new T[planeLength, planeWidth];
            for (int i = 0; i < planeLength ; i++) {
                for (int j = 0; j < planeWidth; j++) {
                    coord = ArrangePlaneCoord(i, j, index, normal);
                    result[i, j] = content[coord[0,0], coord[1,0], coord[2,0]];
                }
            }
            return result;
        }
        // Returns a line. The provided Axis is used as the axis of the line:
        // If Axis.X is provided, the plane will be the subset of the cube where y = index1 and z = index2.
        public T[] Get(int index1, int index2, Axis normal) {
            int planeDimensions = GetLineLength(normal);
            int lineLength = planeDimensions;
            int[,] coord;
            T[] result = new T[lineLength];
            for (int i = 0; i < lineLength; i++) {
                coord = ArrangeLineCoord(i, index1, index2, normal);
                result[i] = content[coord[0, 0], coord[1, 0], coord[2, 0]];
            }
            return result;
        }
        private int[,] GetPlaneDimensions(Axis normal) {
            int[,] matrix = new int[3, 1] { { length }, { width }, { height } };
            return permutationMatrixes[0][normal].MatrixProduct(matrix);
        }
        private int[,] ArrangePlaneCoord(int i, int j, int fixedCoord, Axis normal) {
            int[,] matrix = new int[3, 1] { { i }, { j }, { fixedCoord } };
            return permutationMatrixes[0][normal].MatrixProduct(matrix);
        }
        private int GetLineLength(Axis axis) {
            int[,] matrix = new int[3, 1] { { length }, { width }, { height } };
            return permutationMatrixes[1][axis].MatrixProduct(matrix)[0,0];
        }
        private int[,] ArrangeLineCoord(int i, int fixedCoord1, int fixedCoord2, Axis normal) {
            int[,] matrix = new int[3, 1] { { i }, { fixedCoord1 }, { fixedCoord2 } };
            return permutationMatrixes[1][normal].MatrixProduct(matrix);
        }

        private void InitPermutationMatrixes() {

            // Permutation matrixes used to extract a plane.
            permutationMatrixes[0] = new Dictionary<Axis, int[,]>();
            permutationMatrixes[0].Add(Axis.X, new int[3, 3] { 
                { 0, 0, 1 }, 
                { 1, 0, 0 }, 
                { 0, 1, 0 } });
            permutationMatrixes[0].Add(Axis.Y, new int[3, 3] { 
                { 1, 0, 0 }, 
                { 0, 0, 1 }, 
                { 0, 1, 0 } });
            permutationMatrixes[0].Add(Axis.Z, new int[3, 3] { 
                { 1, 0, 0 }, 
                { 0, 1, 0 }, 
                { 0, 0, 1 } });

            // Permutation matrixes used to extract a line.
            permutationMatrixes[1] = new Dictionary<Axis, int[,]>();
            permutationMatrixes[1].Add(Axis.X, new int[3, 3] { 
                { 1, 0, 0 }, 
                { 0, 1, 0 }, 
                { 0, 0, 1 } });
            permutationMatrixes[1].Add(Axis.Y, new int[3, 3] { 
                { 0, 1, 0 }, 
                { 1, 0, 0 }, 
                { 0, 0, 1 } });
            permutationMatrixes[1].Add(Axis.Z, new int[3, 3] { 
                { 0, 1, 0 }, 
                { 0, 0, 1 }, 
                { 1, 0, 0 } });
        }
    }
}
