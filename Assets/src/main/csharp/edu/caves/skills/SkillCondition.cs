using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans {
    public abstract class SkillCondition {

        private string targetKey;
        private string resultKey;

        public SkillCondition(string target, string result) {
            targetKey = target;
            resultKey = result;
        }

        public void Test() {
            HashSet<Character> targets = (HashSet<Character>)Context.Get(targetKey);
            Dictionary<Character, ConditionResult> result = new Dictionary<Character, ConditionResult>();
            foreach (Character target in targets) {
                result.Add(target, TestTarget(target));
            }
            Context.Set(resultKey, result);
        }

        protected abstract ConditionResult TestTarget(Character target);
    }
}
