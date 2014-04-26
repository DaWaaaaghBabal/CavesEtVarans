using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    class TestSkillCondition : SkillCondition {


        public TestSkillCondition(string target, string result) : base(target, result) {
            
        }

        protected override ConditionResult TestTarget(Character target) {
            return new Hit();
        }
    }
}
