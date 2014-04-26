using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans {
    public abstract class ConditionResult {
        public abstract void Dispatch(SkillEffectGroup effect, Character character);
    }
}
