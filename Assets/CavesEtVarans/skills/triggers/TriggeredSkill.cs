using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.triggers {
	public class TriggeredSkill : ContextDependent {
		public Skill Skill { set; get; }
		public TriggerFiltersList TriggerFilters { set; get; }
		public void Trigger(Context c) {
			if (TriggerFilters.Filter(c))
				Skill.InitSkill(ReadContext(c, Context.SOURCE) as Character);
		}
	}
}
