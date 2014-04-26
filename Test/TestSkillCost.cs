using CavesEtVarans;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCavesEtVarans
{
    [TestClass]
    public class TestSkillCost
    {
        private SkillCost cost = null;

        [TestInitialize]
        public void SetUp()
        {
            cost = new SkillCost();
            cost.Add("mana", 100);
        }

        [TestCleanup]
        public void TearDown()
        {

        }

        [TestMethod]
        public void TestCostIsPresent()
        {
            Assert.AreEqual(cost.Get("mana"), 100);
        }

        [TestMethod]
        public void TestCostIsNotPresent()
        {
            Assert.AreEqual(cost.Get("energy"), 0);
        }

    }
}

