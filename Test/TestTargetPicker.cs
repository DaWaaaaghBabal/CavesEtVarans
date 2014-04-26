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
            characterA = new Character("Character A");
            characterA.SetAP(0);
            characterB = new Character("Character B");
            characterB.SetAP(0);

            picker = new TargetPicker(1, "target");
            List<TargetPicker> targetPickers = new List<TargetPicker>();
            targetPickers.Add(picker);

            SkillCost cost = new SkillCost();
            List<SkillEffectGroup> effectGroups = new List<SkillEffectGroup>();
            List<SkillCondition> conditions = new List<SkillCondition>();
            
            new Skill(cost, targetPickers, effectGroups, conditions).InitSkill(characterA);

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

            picker = new TargetPicker(2, "target");
            picker.Activate();
            picker.AddTarget(characterA);
            Assert.AreEqual(characterA.GetAP(), 0);
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
