using CavesEtVarans.skills.core;
using System.Collections.Generic;
using CavesEtVarans.skills.triggers;

namespace CavesEtVarans.character {
	public class SkillManager {
		public TriggerManager TriggerManager { get; set; }

		public List<Skill> Skills {
			private set { }
			get {
				return skills;
			}
		}
		private List<Skill> commonSkills;
		private List<Skill> skills;

		public void InitClassSkills(CharacterClass clazz) {
			skills = new List<Skill>(commonSkills);
			skills.AddRange(clazz.Skills);
			foreach(TriggeredSkill triggered in clazz.TriggeredSkills) {
				TriggerManager.Register(triggered);
			}
		}

		public void InitCommonSkills() {
			commonSkills = new List<Skill>(); 
		}


	}
}