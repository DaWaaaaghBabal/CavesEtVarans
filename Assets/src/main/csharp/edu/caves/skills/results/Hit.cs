using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    class Hit : ConditionResult{

        public override void Dispatch(SkillEffectGroup effect, Character character) {
            effect.ApplyHit(character);
        }
    }
}
