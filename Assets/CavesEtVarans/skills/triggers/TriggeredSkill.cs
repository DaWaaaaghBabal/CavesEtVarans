using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;

namespace CavesEtVarans.skills.triggers {
	public class TriggeredSkill : ContextDependent {
		public Skill Skill { set; get; }
		public TriggerFiltersList TriggerFilters { set; get; }
		public TriggerType TriggerType { set; get; }
		public void Trigger(Context c) {
			if (TriggerFilters.Filter(c)) {
				c.Set(Context.SKILL, Skill);
				if (Skill.Flags[SkillFlag.Reaction])
					ReactionCoordinator.FileReaction(Skill, c);
				else
					Skill.InitSkill(c);
			}
		}
	}
}
