using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    [TestClass]
    public class TestSkillStructure {
        private Character character1;
        private Character character2;
        private TargetPicker picker;
        private Skill skill;
        [TestInitialize]
        public void SetUp() {
            character1 = new Character("Character 1");
            character1.SetAP(25);
            character2 = new Character("Character 2");
            character2.SetAP(0);

            SkillCost cost = new SkillCost();
            cost.Add("AP", 25);

            picker = new TargetPicker(1, "target1");
            List<TargetPicker> targetPickers = new List<TargetPicker>();
            targetPickers.Add(picker);

            SkillEffectGroup effectGroup = new SkillEffectGroup("hitResults1");
            SkillEffect effect = new TestSkillEffect();
            effectGroup.AddHit(effect);
            List<SkillEffectGroup> effectGroups = new List<SkillEffectGroup>();
            effectGroups.Add(effectGroup);

            SkillCondition condition = new TestSkillCondition("target1", "hitResults1");
            List<SkillCondition> conditions = new List<SkillCondition>();
            conditions.Add(condition);
            skill = new Skill(cost, targetPickers, effectGroups, conditions);

        }

        [TestCleanup]
        public void TearDown() {

        }

        [TestMethod]
        public void TestSkillUse() {
            skill.InitSkill(character1);
            picker.AddTarget(character2);
            Assert.AreEqual(character2.GetAP(), 100);
        }
    }
}
