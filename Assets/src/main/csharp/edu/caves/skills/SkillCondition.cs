using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans {
    class SkillCondition {

        private string targetKey;
        private string resultKey;
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
