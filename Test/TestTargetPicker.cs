using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CavesEtVarans;
using System.Collections.Generic;

namespace TestCavesEtVarans {
    [TestClass]
    public class TestTargetPicker {

        TargetPicker picker;
        Character characterA;
        Character characterB;

        [TestInitialize]
        public void SetUp() {

            picker = new TargetPicker(1, "target");
            characterA = new Character("Character A");
            characterA.SetAP(0);

            characterB = new Character("Character B");
            characterB.SetAP(0);
            Context.InitSkillContext(null, characterB);

        }

        [TestMethod]
        public void TestSingleTargetPicking() {
            picker.Activate();
            picker.AddTarget(characterA);
            fillAP();
            Assert.AreEqual(characterA.GetAP(), 100);
            Assert.AreEqual(characterB.GetAP(), 0);
        }

        [TestMethod]
        public void TestMultiTargetPicking() {
            picker.Activate();
            picker.AddTarget(characterA);
            picker.AddTarget(characterB);
            fillAP();
            Assert.AreEqual(characterA.GetAP(), 100);
            Assert.AreEqual(characterB.GetAP(), 100);
        }

        private void fillAP() {
            foreach (Character character in (HashSet<Character>)Context.Get("target")) {
                character.SetAP(100);
            }
        }

    }
}
