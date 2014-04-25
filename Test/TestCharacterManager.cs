using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CavesEtVarans;

namespace TestCavesEtVarans
{
    [TestClass]
    public class TestCharacterManager
    {

        private CavesEtVarans.CharacterManager cm = null;

        private Character c1 = null;
        private Character c2 = null;

        [TestInitialize]
        public void setUp()
        {
            cm = CavesEtVarans.CharacterManager.Get();
            c1 = new Character();
            c1.SetAP(100);
            c2 = new Character();
            c2.SetAP(90);
            cm.Add(c1);
            cm.Add(c2);
        }

        [TestCleanup]
        public void tearDown()
        {
            cm.Clear();
        }

        [TestMethod]
        public void TestActive()
        {
            Assert.AreEqual(cm.getActiveCharacter(), c1);
        }

        [TestMethod]
        public void TestActiveNext()
        {
            cm.activateNext();
            Assert.AreEqual(cm.getActiveCharacter(), c2);
        }

        [TestMethod]
        public void TestActiveRotate()
        {
            cm.activateNext();
            cm.activateNext();
            Assert.AreEqual(cm.getActiveCharacter(), c1);
        }

    }
}
