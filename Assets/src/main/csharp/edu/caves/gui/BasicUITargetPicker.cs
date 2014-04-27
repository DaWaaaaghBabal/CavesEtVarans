using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    public class BasicUITargetPicker : TargetPicker {

        public BasicUITargetPicker(string targetKey)
            : base(targetKey) {
        }

        public override void AddTarget(Character character) {
            MainGUI.DisplayCharacter(character);
        }

        public override void MouseOverCharacter(Character character) {
            
        }
    }
}
