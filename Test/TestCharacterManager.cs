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
            c1 = new Character("Character A");
            c1.SetAP(100);
            c2 = new Character("Character B");
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
            Assert.AreEqual(cm.GetActiveCharacter(), c1);
        }

        [TestMethod]
        public void TestActiveNext()
        {
            c1.SetAP(50);
            cm.ActivateNext();
            Assert.AreEqual(cm.GetActiveCharacter(), c2);
        }

        [TestMethod]
        public void TestActiveRotate()
        {
            cm.ActivateNext();
            cm.ActivateNext();
            Assert.AreEqual(cm.GetActiveCharacter(), c1);
        }

    }
}
