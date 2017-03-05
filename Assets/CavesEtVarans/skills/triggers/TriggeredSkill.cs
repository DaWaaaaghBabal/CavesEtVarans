using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;
using CavesEtVarans.skills.filters;

namespace CavesEtVarans.skills.triggers {
	public class TriggeredSkill : ContextDependent {
		public Skill Skill { set; get; }
		public FiltersList TriggerFilters { set; get; }
		public TriggerType TriggerType { set; get; }
		public void Trigger(Context c) {
            UnityEngine.Debug.Log("Triggering skill : " + Skill.Attributes.Name);
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
