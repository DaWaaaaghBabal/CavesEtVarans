using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CavesEtVarans;

namespace TestCavesEtVarans
{
    [TestClass]
    public class TestCharacterManager
    {

        private CharacterManager cm = null;

        private Character c1 = null;
        private Character c2 = null;

        [TestInitialize]
        public void setUp()
        {
            cm = CharacterManager.Get();
            c1 = new Character("player1");
            c1.SetAP(100);
            c2 = new Character("player2");
            c2.SetAP(80);
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
            Assert.AreEqual(c1, cm.GetActiveCharacter());

        }

        [TestMethod]
        public void TestActiveNext()
        {
            Assert.AreEqual(c1, cm.GetActiveCharacter());
            cm.ActivateNext();
            Assert.AreEqual(c2, cm.GetActiveCharacter());
        }

        [TestMethod]
        public void TestActiveRotate()
        {
            Assert.AreEqual(c1, cm.GetActiveCharacter());
            cm.ActivateNext();
            cm.ActivateNext();
            Assert.AreEqual(c1, cm.GetActiveCharacter());
        }

    }
}
