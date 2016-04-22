using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.events {
	public class OrientationEvent : GameEvent<OrientationEvent> {
		public Orientation NewOrientation { set; get; }
		public Orientation OldOrientation { set; get; }
		public Character Source { set; get; }

		public OrientationEvent(Character target, Orientation oldOrientation, Orientation newOrientation) {
			Source = target;
			OldOrientation = oldOrientation;
			NewOrientation = newOrientation;
		}

		public override TriggerType TriggerType() {
			return events.TriggerType.Orientation;
		}
	}
}
