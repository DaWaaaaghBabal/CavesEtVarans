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


        private void Trigger <T> (GameEvent<T> e) where T : GameEvent<T> {
            Dictionary<string, object> reactionData = new Dictionary<string, object>(e.EventData);
            reactionData[ContextKeys.SOURCE] = character;
            foreach (TriggeredSkill skill in triggeredSkills[e.TriggerType()]) {
                skill.Trigger(reactionData);
            }
        }

	    private void SkillUseTrigger(SkillUseEvent e) {
            Trigger(e);
        }
        private void MovementTrigger(MovementEvent e) {
            Trigger(e);
		}
        private void StartTurnTrigger(StartTurnEvent e) {
            if (e.EventData[ContextKeys.TRIGGERING_CHARACTER] == character) {
                Trigger(e);
            }
        }
	}
}