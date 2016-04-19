using System;
using CavesEtVarans.character;
using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public class StatIncrement : BuffEffect, IStatModifier {
		public ModifierType Type {
			get { return ModifierType.Increment; }
			private set { }
		}
		private IValueCalculator val;
		public IValueCalculator Value {
			get { return val; }
			set {
				val = value;
				val.Parent = this;
			}
		}
		public string StatKey { get; set; }

		public double GetValue(double originalValue, Context context) {
			Context localContext = CopyContext(context);
			return originalValue + Value.Value(localContext);
		}

		public override void ApplyOn(Character target, Context context) {
			target.ApplyStatModifier(StatKey, this);
		}

		public override void RemoveFrom(Character target) {
			target.RemoveStatModifier(StatKey, this);
		}
	}
}
