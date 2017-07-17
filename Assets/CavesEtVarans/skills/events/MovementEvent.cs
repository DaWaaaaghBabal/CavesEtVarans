using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.events {
	public class MovementEvent : GameEvent<MovementEvent> {
		public MovementEvent(Character source, Tile start, Tile end) : base() {
			EventData[ContextKeys.TRIGGERING_CHARACTER] = source;
            EventData[ContextKeys.START_TILE] = start;
            EventData[ContextKeys.END_TILE] = end;
		}

		public override TriggerType TriggerType(){
			return events.TriggerType.Movement;
		}
	}
}