using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    class TestSkillEffect : SkillEffect {

        public override void Apply(Character character) {
            character.SetAP(100);
        }
    }
}
