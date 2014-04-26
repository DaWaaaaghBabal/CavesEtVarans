using CavesEtVarans;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCavesEtVarans
{
    [TestClass]
    public class TestResourceManager
    {
        ResourceManager rm;
        Resource r;
        Resource r2;
        SkillCost sc;

        [TestInitialize]
        public void SetUp()
        {
            rm = new ResourceManager();
            r = new Resource(0, 100);
            r.Give(50);
            rm.Add("mana", r);
            r2 = new Resource(0, 120);
            r2.Give(60);
            rm.Add("hatred", r2);
            sc = new SkillCost();
        }

        [TestCleanup]
        public void tearDown()
        {
            
        }

        [TestMethod]
        public void TestPresenceRessource()
        {
            Assert.AreEqual(r, rm.Get("mana"));
        }

        [TestMethod]
        public void TestIsAbleToPaySimpleCost()
        {
            sc.Add("mana", 50);
            Assert.IsTrue(rm.CanBePaid(sc));
        }

        [TestMethod]
        public void TestIsNotAbleToPaySimpleCost()
        {
            sc.Add("mana", 75);
            Assert.IsFalse(rm.CanBePaid(sc));
        }

        [TestMethod]
        public void TestIsNotAbleToPayUnknownCost()
        {
            sc.Add("valor", 50);
            Assert.IsFalse(rm.CanBePaid(sc));
        }

        [TestMethod]
        public void TestIsAbleToPayComplexCost()
        {
            sc.Add("mana", 50);
            sc.Add("hatred", 60);
            Assert.IsTrue(rm.CanBePaid(sc));
        }

        [TestMethod]
        public void TestIsNotAbleToPayComplexCost()
        {
            sc.Add("mana", 75);
            sc.Add("hatred", 10);
            Assert.IsFalse(rm.CanBePaid(sc));
        }

        [TestMethod]
        public void TestIsNotAbleToPayUnknownComplexCost()
        {
            sc.Add("mana", 50);
            sc.Add("valor", 40);
            Assert.IsFalse(rm.CanBePaid(sc));
        }



        [TestMethod]
        public void TestCanPaySimpleCost()
        {
            sc.Add("mana", 50);
            rm.Pay(sc);
            Assert.AreEqual(0, rm.Get("mana").GetValue());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestCantPaySimpleCost()
        {
            sc.Add("mana", 75);
            rm.Pay(sc);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestCantPayUnknownCost()
        {
            sc.Add("valor", 50);
            rm.Pay(sc);
        }

        [TestMethod]
        public void TestCanPayComplexCost()
        {
            sc.Add("mana", 30);
            sc.Add("hatred", 60);
            rm.Pay(sc);
            Assert.AreEqual(20, rm.Get("mana").GetValue());
            Assert.AreEqual(0, rm.Get("hatred").GetValue());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestCantPayComplexCost()
        {
            sc.Add("mana", 75);
            sc.Add("hatred", 10);
            rm.Pay(sc);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestCantPayUnknownComplexCost()
        {
            sc.Add("mana", 50);
            sc.Add("valor", 40);
            rm.Pay(sc);
        }

    }
}



