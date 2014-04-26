using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    /* About everything in the game, every action and every specificity of a character, is a Skill.
     * A skill is defined by a cost in various resources, targets, effects applied to these targets, and tests to check whether each target is affected.

     * The flow for using a skill is :
     * - activate the first target picker and wait for it to call back
     * - when the target picker calls back, activate the next one and wait
     * - when all target pickers have called back, pay the cost
     * - apply each test to a set of targets. For each target, the skill can be a miss, a hit, or a critical hit.
     * - apply each effect to a set of targets.
     * In fact, all targets and test results will be stored in the Context.
     */ 

    public abstract class Skill {
        private SkillCost cost;
        private List<TargetPicker> targetPickers;
        private List<SkillEffectGroup> effects;
        private List<SkillCondition> conditions;

        public Skill(SkillCost newCost, List<TargetPicker> newTargetPickers,
        List<SkillEffectGroup> newEffects, List<SkillCondition> newConditions) {
            targetPickers = newTargetPickers;
            effects = newEffects;
            cost = newCost;
            conditions = newConditions;
        }

        public void InitSkill(Character source) {
            targetPickerIndex = 0;
            Context.InitSkillContext(this, source);
        }

        int targetPickerIndex;
        public void NextTargetPicker() {
            if (targetPickerIndex < targetPickers.Count) {
                targetPickers[targetPickerIndex].Activate();
                targetPickerIndex++;
            } else {
                UseSkill();
            }
        }

        // Called when all targets have been picked and stored in the Context.
        private void UseSkill() {
            PayCosts();
            TestConditions();
            ApplyEffects();
        }

        private void PayCosts() {
            ((Character)Context.Get("source")).Pay(cost);
        }

        private void TestConditions() {
            foreach (SkillCondition condition in conditions) {
                condition.Test();
            }
        }

        private void ApplyEffects() {
            foreach (SkillEffectGroup effect in effects) {
                effect.Apply();
            }
        }

    }
}
