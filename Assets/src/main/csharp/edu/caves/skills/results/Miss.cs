using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    class Miss : ConditionResult{

        public override void Dispatch(SkillEffectGroup effect, Character character) {
            effect.ApplyMiss(character);
        }
    }
}
