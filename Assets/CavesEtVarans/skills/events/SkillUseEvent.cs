using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.events {
	public class SkillUseEvent : GameEvent<SkillUseEvent> {

        public SkillUseEvent(Skill skill, Character source, TargetGroup targets) : base() {
            EventData[ContextKeys.TRIGGERING_SKILL] = skill;
            EventData[ContextKeys.TRIGGERING_CHARACTER] = source;
            EventData[ContextKeys.TRIGGERING_TARGETS] = targets;
        }

		public override TriggerType TriggerType() {
			return events.TriggerType.SkillUse;
		}
	}
}
