using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.events {
	public class SkillUseEvent : GameEvent<SkillUseEvent> {
		public Skill Skill { get; private set; }
		public Character Source { get; private set; }

		public SkillUseEvent(Skill skill, Character source) {
			Skill = skill;
			Source = source;
		}

		public override TriggerType TriggerType() {
			return events.TriggerType.SkillUse;
		}
	}
}
