using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.events {
    public class EndTurnEvent : GameEvent<EndTurnEvent> {
        
        public EndTurnEvent (Character character) : base() {
            EventData[ContextKeys.TRIGGERING_CHARACTER] = character;
        }

        public override TriggerType TriggerType() {
            return events.TriggerType.StartTurn;
        }
    }
}
