using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CavesEtVarans;

namespace TestCavesEtVarans {
    [TestClass]
    public class TestStatisticsManager {
        StatisticsManager manager;
        StatModifier multiplier;
        StatIncrementer incrementer;
        StatOverrider overrider;
        [TestInitialize]
        public void SetUp() {
            manager = new StatisticsManager();
            multiplier = new StatMultiplier(2.0);
            incrementer = new StatIncrementer(10.0);
            overrider = new StatOverrider(43.0);
        }

        [TestMethod]
        public void TestBaseStatistics() {
            Assert.AreEqual(manager.GetValue(Statistic.HEALTH), 24);
        }

        [TestMethod]
        public void TestModifiedStatistics() {
            string key = Statistic.HEALTH;
            Assert.AreEqual(manager.GetValue(key), 24);
            manager.AddModifier(key, incrementer);
            Assert.AreEqual(manager.GetValue(key), 34);
            manager.RemoveModifier(key, incrementer);
            manager.AddModifier(key, multiplier);
            Assert.AreEqual(manager.GetValue(key), 48);
            manager.RemoveModifier(key, multiplier);
            manager.AddModifier(key, overrider);
            Assert.AreEqual(manager.GetValue(key), 43);
        }

        [TestMethod]
        public void TestModifierPriority() {
            string key = Statistic.HEALTH;
            Assert.AreEqual(manager.GetValue(key), 24);
            manager.AddModifier(key, incrementer);
            Assert.AreEqual(manager.GetValue(key), 34);
            manager.AddModifier(key, multiplier);
            Assert.AreEqual(manager.GetValue(key), 58);
            manager.AddModifier(key, overrider);
            Assert.AreEqual(manager.GetValue(key), 43);
        }
    }
}
