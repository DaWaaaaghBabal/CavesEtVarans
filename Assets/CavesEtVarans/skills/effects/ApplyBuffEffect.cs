using CavesEtVarans.skills.core;
using CavesEtVarans.character;
using CavesEtVarans.skills.effects.buffs;

namespace CavesEtVarans.skills.effects {
	public class ApplyBuffEffect : TargetedEffect {

		public Duration Duration { set; get; }
		public BuffEffect Effect;

		public override void Apply(Character target, Context context) {
			BuffInstance instance = Effect.Instantiate(context);
			instance.Duration = Duration.Instantiate(context); 
			instance.ApplyOn(target);
		}
	}
}
