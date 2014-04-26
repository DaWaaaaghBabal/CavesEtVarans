using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    class Critical : ConditionResult{

        public override void Dispatch(SkillEffectGroup effect, Character character) {
            effect.ApplyCritical(character);
        }
    }
}
