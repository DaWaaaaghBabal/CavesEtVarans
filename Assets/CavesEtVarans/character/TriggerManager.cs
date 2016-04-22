using System;
using System.Collections.Generic;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;
using CavesEtVarans.skills.triggers;

namespace CavesEtVarans.character {
	public class TriggerManager {
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

		public void SkillUseTrigger(SkillUseEvent e, Context context) {
			foreach (TriggeredSkill skill in triggeredSkills[e.TriggerType()]) {
				Context c = context.Duplicate();
				TargetGroup targets = new TargetGroup();
				foreach (TargetGroup group in c.AllKeys(Context.TARGETS)) {
					targets.Add(group);
				}
				c.Set(Context.TRIGGERING_TARGETS, targets);
				c.Set(Context.TRIGGERING_SKILL, e.Skill);
				c.Set(Context.TRIGGERING_CHARACTER, e.Source);
				c.Set(Context.SOURCE, character);
				skill.Trigger(c);
			}
		}

		public void MovementTrigger(MovementEvent e, Context context) {
			foreach (TriggeredSkill skill in triggeredSkills[e.TriggerType()]) {
				Context c = context.Duplicate();
				c.Set(Context.TRIGGERING_CHARACTER, e.Source);
				c.Set(Context.START_TILE, e.StartTile);
				c.Set(Context.END_TILE, e.EndTile);
				c.Set(Context.SOURCE, character);
				skill.Trigger(c);
			}
		}

        public void StartTurnTrigger(StartTurnEvent e, Context context) {
            if (e.Source == character) {
                foreach (TriggeredSkill skill in triggeredSkills[e.TriggerType()]) {
                    Context c = context.Duplicate();
                    c.Set(Context.TRIGGERING_CHARACTER, e.Source);
                    c.Set(Context.SOURCE, character);
                    skill.Trigger(c);
                }
            }
        }
	}
}