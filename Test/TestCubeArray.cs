using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CavesEtVarans;

namespace CavesEtVarans {
    [TestClass]
    public class TestCubeArray {
        private class TestData : IDisposable {
            public string value;
            public TestData(int i, int j, int k) {
                value = "" + i + "" + j + "" + k;
            }
        
            public void Dispose(){}
            public override string ToString() {
                return value;
            }
        }

        private CubeArray<TestData> cube;

        [TestInitialize]
        public void SetUp() {
            cube = new CubeArray<TestData>(1,1,1);
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    for (int k = 0; k < 3; k++) {
                        cube.Add(new TestData(i, j, k), i, j, k);
                    }
                }
            }
        }

        [TestMethod]
        public void TestGetCell() {
            Assert.AreEqual("222", cube.Get(2, 2, 2).value);
            Assert.AreEqual("210", cube.Get(2, 1, 0).value);
            Assert.AreEqual(null, cube.Get(2, 2, 3));
        }

        [TestMethod]
        public void TestGetLine() {
            string expectedResult = " 210 211 212";
            string actualResult = "";
            TestData[] result = cube.Get(2, 1, CubeArray<TestData>.Axis.Z);
            for (int i = 0; i < 3; i++) {
                actualResult += " " + result[i].value;
            }
            Assert.AreEqual(expectedResult, actualResult);

            expectedResult = " 021 121 221";
            actualResult = "";
            result = cube.Get(2, 1, CubeArray<TestData>.Axis.X);
            for (int i = 0; i < 3; i++) {
                actualResult += " " + result[i].value;
            }
            Assert.AreEqual(expectedResult, actualResult);

            expectedResult = " 201 211 221";
            actualResult = "";
            result = cube.Get(2, 1, CubeArray<TestData>.Axis.Y);
            for (int i = 0; i < 3; i++) {
                actualResult += " " + result[i].value;
            }
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetPlane() {
            string expectedResult = " 200 201 202 210 211 212 220 221 222";
            string actualResult = "";
            TestData[,] result = cube.Get(2, CubeArray<TestData>.Axis.X);
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    actualResult += " " + result[i,j].value;
                }
            }
            Assert.AreEqual(expectedResult, actualResult);

            expectedResult = " 020 021 022 120 121 122 220 221 222";
            actualResult = "";
            result = cube.Get(2, CubeArray<TestData>.Axis.Y);
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    actualResult += " " + result[i, j].value;
                }
            }
            Assert.AreEqual(expectedResult, actualResult);

            expectedResult = " 002 012 022 102 112 122 202 212 222";
            actualResult = "";
            result = cube.Get(2, CubeArray<TestData>.Axis.Z);
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    actualResult += " " + result[i, j].value;
                }
            }
            Assert.AreEqual(expectedResult, actualResult);
        }
        
    }
}
