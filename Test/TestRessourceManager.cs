using CavesEtVarans;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCavesEtVarans
{
    [TestClass]
    class TestRessourceManager
    {
        RessourceManager rm;
        Ressource r;

        [TestInitialize]
        public void SetUp()
        {
            rm = new RessourceManager();
            r = new Ressource(0, 100);
            rm.Add("mana", r);
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

    }
}


