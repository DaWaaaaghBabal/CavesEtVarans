using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;
using CavesEtVarans.skills.filters;
using System.Collections.Generic;

namespace CavesEtVarans.skills.triggers {
	public class TriggeredSkill : ContextDependent {
		public Skill Skill { set; get; }
		public FiltersList TriggerFilters { set; get; }
		public TriggerType TriggerType { set; get; }
		public void Trigger(Dictionary<string, object> reactionData) {
            UnityEngine.Debug.Log("Triggering skill : " + Skill.Attributes.Name);
            TriggerFilters.SetLocalContext(reactionData);
			if (TriggerFilters.Filter()) {
				if (Skill.Flags[SkillFlag.Reaction])
					ReactionCoordinator.FileReaction(Skill, reactionData);
				else
					Skill.InitSkill(reactionData);
			}
		}
	}
}
