using CavesEtVarans.skills.core;
using System.Collections.Generic;

namespace CavesEtVarans.character {
	public class SkillManager {
		private List<Skill> commonSkills;
		private List<Skill> skills;
		public void InitClassSkills(CharacterClass clazz) {
			skills = new List<Skill>(commonSkills);
			skills.AddRange(clazz.Skills);
		}

		public List<Skill> Skills {
			private set { }
			get {
				return skills;
			}
		}

		public void InitCommonSkills() {
			commonSkills = new List<Skill>(); 
		}
	}
}