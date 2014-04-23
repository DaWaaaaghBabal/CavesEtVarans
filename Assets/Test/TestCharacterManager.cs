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
            cm = CavesEtVarans.CharacterManager.get();
            c1 = new Character();
            c2 = new Character();
            cm.add(c1);
            cm.add(c2);
        }

        [TestCleanup]
        public void tearDown()
        {

        }

        [TestMethod]
        public void TestActive()
        {
            Assert.Equals(cm.getActiveCharacter(), c1);
        }

        [TestMethod]
        public void TestActiveNext()
        {
            cm.activateNext();
            Assert.Equals(cm.getActiveCharacter(), c2);
        }

        [TestMethod]
        public void TestActiveRotate()
        {
            cm.activateNext();
            cm.activateNext();
            Assert.Equals(cm.getActiveCharacter(), c1);
        }

    }
}
