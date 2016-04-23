using CavesEtVarans.skills.core;
using System.Collections.Generic;
using CavesEtVarans.skills.triggers;

namespace CavesEtVarans.data
{
	public class CharacterClass {
		public string Name { set; get; }
		public int Health { set; get; }
		public int Defense { set; get; }
		public int Attack { set; get; }
		public int Damage { get; set; }
		public int Willpower { set; get; }
		public int Stability { set; get; }
		public string SpecialName { set; get; }
		public int Special { get; set; }
		public int Initiative { set; get; }
		public int Iterative { set; get; }
		public int Jump { set; get; }
		public List<Skill> Skills { set; get; }
		public List<TriggeredSkill> TriggeredSkills { set; get; }
		public int MaxEnergy { get; set; }
        public int EnergyPerLevel { set; get; }
        public string EnergyKey { set; get; }
        public string EnergyName { set; get; }
        public Dictionary<string, int> HiddenResources { set; get; }
    }
}
