using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans
{
    [TestClass]
    public class TestResource
    {
        private Resource r = null;
        private int max = 100;
        private int min = 10;

        [TestInitialize]
        public void setUp()
        {
            r = new Resource(min, max);
        }

        [TestCleanup]
        public void tearDown()
        {
        
        }

        [TestMethod]
        public void TestCanBePaid()
        {
            r.Give((max-min)/2);
            Assert.IsTrue(r.CanBePaid((max-min)/2));
        }

        [TestMethod]
        public void TestCanBeGiven()
        {
            r.Give(2*max);
            Assert.AreEqual(max, r.GetValue());
        }

        [TestMethod]
        public void TestCantBePaid()
        {
            r.Give((max-min)/2-1);
            Assert.IsFalse(r.CanBePaid((max-min)/2));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestDontPay()
        {
            r.Give((max-min)/2-1);
            r.Pay((max-min)/2);
        }

    }
}
