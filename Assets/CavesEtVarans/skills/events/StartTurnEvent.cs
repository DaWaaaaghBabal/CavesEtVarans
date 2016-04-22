using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.events {
    public class StartTurnEvent : GameEvent<StartTurnEvent> {

        public Character Source { get; private set; }

        public StartTurnEvent (Character character) {
            Source = character;
        }

        public override TriggerType TriggerType() {
            return events.TriggerType.StartTurn;
        }
    }
}
