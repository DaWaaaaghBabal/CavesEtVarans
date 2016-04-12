using System;

namespace CavesEtVarans.skills.core
{
	public class SkillAttributes {
		public String Name { set; get; } // The name of the skill.
		public String Icon { set; get; } // The icon used for the skill button.
		public string Animation { set; get; } // The character animation to use when the character uses the skill.
		public string Description { set; get; } // The description used in the skill's tooltip.
	}
}
