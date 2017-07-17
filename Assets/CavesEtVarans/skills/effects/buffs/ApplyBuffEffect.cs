using CavesEtVarans.skills.core;
using CavesEtVarans.character;
using CavesEtVarans.skills.effects.buffs;
using System.Collections.Generic;
using CavesEtVarans.battlefield;
using System;

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

		public override EffectResult Apply(Character target, int suffix) {
			BuffInstance instance = new BuffInstance();
			instance.SetLocalContext(ContextKeys.BUFF_SOURCE, ReadContext(ContextKeys.SOURCE));
			instance.SetLocalContext(ContextKeys.BUFF_TARGET, ReadContext(ContextKeys.CURRENT_TARGET));

			foreach (BuffEffect effect in Effects) {
				BuffEffect effectInstance = effect.Clone() as BuffEffect;
				effectInstance.SetLocalContext(ContextKeys.BUFF_SOURCE, ReadContext(ContextKeys.SOURCE));
				effectInstance.SetLocalContext(ContextKeys.BUFF_TARGET, ReadContext(ContextKeys.CURRENT_TARGET));
				instance.AddEffect(effectInstance);
            }
			instance.SourceEffect = this;
            instance.Duration = Duration.Instantiate();
			target.ApplyBuff(instance);
            return EffectResult.Void; // @TODO Modify to apply a variable number of charges and return that number;
		}

        public override EffectResult Apply(Tile tile, int suffix) {
            throw new NotImplementedException("Trying to apply a buff to a tile ! Please check YAML for inconsistencies.");
        }
    }
}
