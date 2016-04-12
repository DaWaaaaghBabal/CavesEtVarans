using CavesEtVarans.skills.core;
using System.Collections.Generic;

namespace CavesEtVarans.character
{
	public class CharacterClass {
		public string Name { set; get; }
		public int Health { set; get; }
		public int Defense { set; get; }
		public int Attack { set; get; }
		public int Damage { get; set; }
		public int Willpower { set; get; }
		public int Dodge { set; get; }
		public string SpecialName { set; get; }
		public int Special { get; set; }
		public int Initiative { set; get; }
		public int Iterative { set; get; }
		public int Energy { set; get; }
		public string EnergyName { set; get; }
		public int Jump { set; get; }
		public List<Skill> Skills { set; get; }
	}
}
