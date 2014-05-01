using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CavesEtVarans;

namespace Test {
    [TestClass]
    public class TestMatrix {
        [TestMethod]
        public void TestMatrixProduct() {
            int[,] matrixA = new int[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            int[,] matrixB = new int[2, 3] { { 1, 1, 1 }, { 2, 2, 2 } };
            int[,] expectedResult = new int[3, 3] { { 5, 5, 5 }, { 11, 11, 11 }, { 17, 17, 17 } };

            int[,] result = matrixA.MatrixProduct(matrixB);
            EqualMatrixes(expectedResult, result);
        }

        private static void EqualMatrixes(int[,] expectedResult, int[,] result) {
            if (expectedResult.GetLength(0) != result.GetLength(0) || expectedResult.GetLength(1) != result.GetLength(1)) {
                Assert.Fail();
            }
            for (int i = 0; i < expectedResult.GetLength(0); i++) {
                for (int j = 0; j < expectedResult.GetLength(1); j++) {
                    Assert.AreEqual(expectedResult[i, j], result[i, j]);
                }
            }
        }
    }
}
