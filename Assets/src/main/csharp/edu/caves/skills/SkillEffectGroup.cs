using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    public class SkillEffectGroup {

        private List<SkillEffect> criticalEffects;
        private List<SkillEffect> hitEffects;
        private List<SkillEffect> missEffects;
        private string targetKey;

        public SkillEffectGroup(string key) {
            criticalEffects = new List<SkillEffect>();
            hitEffects = new List<SkillEffect>();
            missEffects = new List<SkillEffect>();
            targetKey = key;
        }

        public void Apply() {
            Dictionary<Character, ConditionResult> targetResults = (Dictionary<Character, ConditionResult>)Context.Get(targetKey);
            foreach (KeyValuePair<Character, ConditionResult> entry in targetResults) {
                entry.Value.Dispatch(this, entry.Key);
            }
        }

        public void ApplyHit(Character character) {
            foreach (SkillEffect effect in hitEffects) {
                effect.Apply(character);
            }
        }

        public void ApplyMiss(Character character) {
            foreach (SkillEffect effect in missEffects) {
                effect.Apply(character);
            }
        }

        public void ApplyCritical(Character character) {
            foreach (SkillEffect effect in criticalEffects) {
                effect.Apply(character);
            }
        }

        public void AddHit(SkillEffect effect) {
            hitEffects.Add(effect);
        }
        public void AddMiss(SkillEffect effect) {
            missEffects.Add(effect);
        }
        public void AddCritical(SkillEffect effect) {
            criticalEffects.Add(effect);
        }
    }
}
