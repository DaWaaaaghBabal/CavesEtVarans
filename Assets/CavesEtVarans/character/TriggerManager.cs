using System;
using System.Collections.Generic;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;
using CavesEtVarans.skills.triggers;

namespace CavesEtVarans.character {
	public class TriggerManager : ContextDependent {
		private Character character;
		private Dictionary<TriggerType, HashSet<TriggeredSkill>> triggeredSkills;

		public TriggerManager(Character character) {
			this.character = character;
			triggeredSkills = new Dictionary<TriggerType, HashSet<TriggeredSkill>>();
			foreach (TriggerType t in Enum.GetValues(typeof(TriggerType))) {
				triggeredSkills.Add(t, new HashSet<TriggeredSkill>());
			}
		}

		public void Register(TriggeredSkill skill) {
			triggeredSkills[skill.TriggerType].Add(skill);
		}

		public void Register() {
			SkillUseEvent.Listeners += SkillUseTrigger;
			MovementEvent.Listeners += MovementTrigger;
            StartTurnEvent.Listeners += StartTurnTrigger;
		}

	    public void SkillUseTrigger(SkillUseEvent e) {
            TargetGroup targets = (TargetGroup) ReadContext(ContextKeys.TARGETS);
            Dictionary<string, object> reactionData = new Dictionary<string, object>();
			reactionData[ContextKeys.TRIGGERING_TARGETS] = targets;
			reactionData[ContextKeys.TRIGGERING_SKILL] = e.Skill;
			reactionData[ContextKeys.TRIGGERING_CHARACTER] = e.Source;
			reactionData[ContextKeys.SOURCE] = character;
			foreach (TriggeredSkill skill in triggeredSkills[e.TriggerType()]) {
                skill.Trigger(reactionData);
			}
		}

		public void MovementTrigger(MovementEvent e) {
            Dictionary<string, object> reactionData = new Dictionary<string, object>();
            reactionData[ContextKeys.TRIGGERING_CHARACTER] = e.Source;
			reactionData[ContextKeys.START_TILE] = e.StartTile;
			reactionData[ContextKeys.END_TILE] = e.EndTile;
            reactionData[ContextKeys.SOURCE] = character;
			foreach (TriggeredSkill skill in triggeredSkills[e.TriggerType()]) {
				skill.Trigger(reactionData);
			}
		}

        public void StartTurnTrigger(StartTurnEvent e) {
            if (e.Source == character) {
                Dictionary<string, object> reactionData = new Dictionary<string, object>();
                reactionData[ContextKeys.TRIGGERING_CHARACTER] = e.Source;
                reactionData[ContextKeys.SOURCE] = character;
                foreach (TriggeredSkill skill in triggeredSkills[e.TriggerType()]) {
                    skill.Trigger(reactionData);
                }
            }
        }
	}
}