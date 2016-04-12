using System;

namespace CavesEtVarans.utils
{
	public static class MatrixCalculus
	{
		public static int[,] MatrixProduct (this Array matrix, int[,] matrixB)
		{
			int[,] matrixA = (int[,])matrix;
			if (matrixA.GetLength (1) != matrixB.GetLength (0)) {
				throw new ArgumentException ("The sizes of given matrixes don't match");
			}
			int numberOfLines = matrixA.GetLength (0);
			int numberOfColumns = matrixB.GetLength (1);
			int numberOfProducts = matrixA.GetLength (1);
			int[,] result = new int[numberOfLines, numberOfColumns];

			for (int i = 0; i < numberOfLines; i++) {
				for (int j = 0; j < numberOfColumns; j++) {
					result [i, j] = 0;
					for (int k = 0; k < numberOfProducts; k++) {
						result [i, j] += matrixA [i, k] * matrixB [k, j];
					}
				}
			}
			return result;
		}
	}
}
