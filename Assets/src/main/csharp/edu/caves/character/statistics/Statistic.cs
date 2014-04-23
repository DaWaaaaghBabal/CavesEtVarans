using System;
using System.Collections.Generic;
using CavesEtVarans;

namespace CavesEtVarans
{

	public class Statistic {
		private List<StatModifier> statOverriders;
		private List<StatModifier> statMultipliers;
		private List<StatModifier> statIncrementers;

		private double value;

		// Initializes a string with an initial value and a display name.
		public Statistic (double initValue) {
			value = initValue;
			statOverriders = new List<StatModifier> ();
			statMultipliers = new List<StatModifier> ();
			statIncrementers = new List<StatModifier> ();
		}

		public double GetValue () {
			double val = value;
			foreach (StatModifier mod in statMultipliers) {
				val = mod.getValue (val);
			}
			foreach (StatModifier mod in statIncrementers) {
				val = mod.getValue (val);
			}
			foreach (StatModifier mod in statOverriders) {
				val = mod.getValue (val);
			}
			return val;
		}

		public void RemoveModifier (StatModifier mod) {
			statOverriders.Remove (mod);
			statMultipliers.Remove (mod);
			statIncrementers.Remove (mod);
		}

		public void addModifier (StatModifier mod) {
			mod.dispatch (this);
		}
		
		public void addOverrider (StatModifier mod) {
			statOverriders.Add (mod);
		}
		public void addMultiplier (StatModifier mod) {
			statMultipliers.Add (mod);
		}
		public void addIncrementer (StatModifier mod) {
			statIncrementers.Add (mod);
		}
	}
}

