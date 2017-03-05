using CavesEtVarans.skills.core;
using CavesEtVarans.character;
using CavesEtVarans.skills.effects.buffs;
using System.Collections.Generic;

namespace CavesEtVarans.skills.effects {
	public class ApplyBuffEffect : TargetedEffect {

        private Duration duration;
		public Duration Duration {
            set { duration = value; }
            get {
                if (duration == null) duration = new Constant();
                return duration;
            }
        }
		public List<BuffEffect> Effects { set; get; }

		public StackingType StackingType {
			set { Stacking = StackingStrategy.ProvideInstance(value); }
			get { return Stacking.StackingType; }
		}
		[YamlDotNet.Serialization.YamlIgnore]
		public StackingStrategy Stacking { get; set; }

		public ApplyBuffEffect() {
			Effects = new List<BuffEffect>();
		}

		public override void Apply(Character target, Context context) {
			BuffInstance instance = new BuffInstance();
			instance.SetLocalContext(Context.BUFF_SOURCE, ReadContext(context, Context.SOURCE));
			instance.SetLocalContext(Context.BUFF_TARGET, ReadContext(context, Context.CURRENT_TARGET));

			foreach (BuffEffect effect in Effects) {
				BuffEffect effectInstance = effect.Clone() as BuffEffect;
				effectInstance.SetLocalContext(Context.BUFF_SOURCE, ReadContext(context, Context.SOURCE));
				effectInstance.SetLocalContext(Context.BUFF_TARGET, ReadContext(context, Context.CURRENT_TARGET));
				instance.AddEffect(effectInstance);
            }
			instance.SourceEffect = this;
            instance.Duration = Duration.Instantiate(context);
			target.ApplyBuff(instance, context);
		}
	}
}
