using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CavesEtVarans;

namespace CavesEtVarans {
    [TestClass]
    public class TestCubeArray {
        private class TestData : IDisposable {
            public int value;
            public TestData(int i, int j, int k) {
                value = 9 * i + 3 * j + k;
            }
        
            public void Dispose(){}
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
        public void TestInitialContent() {
            Assert.AreEqual(39, cube.Get(3, 3, 3).value);
        }
        
    }
}
