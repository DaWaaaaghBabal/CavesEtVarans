using System;
using System.Collections.Generic;
using CavesEtVarans;

namespace CavesEtVarans
{
    // A statistic is a value that is modified by a number of effects. Each statistic stores its base value and all effects that alter it.
	public class Statistic {
        public static readonly string MELEE_ATTACK = "MATK";
        public static readonly string MELEE_DAMAGE = "MDMG";
        public static readonly string RANGE_ATTACK = "RATK";
        public static readonly string RANGE_DAMAGE = "RDMG";
        public static readonly string WILLPOWER = "WP";
        public static readonly string SPELL_POWER = "SPP";
        public static readonly string HEALTH = "HP";
        public static readonly string DEFENSE = "DEF";
        public static readonly string INITIATIVE = "INI";
        public static readonly string DODGE = "DDG";
        public static readonly string JUMP = "JMP";
        public static readonly string ITERATION_MALUS = "ITER";
        public static readonly string SPECIAL = "SPE";

        public static readonly string BASE_HIT_CHANCE = "BHC";
        public static readonly string BONUS_TO_HIT = "BTH";
        public static readonly string BONUS_TO_DEF = "BTD";
        public static readonly string MAX_AP = "MAP";

		private List<StatModifier> statOverriders;
		private List<StatModifier> statMultipliers;
		private List<StatModifier> statIncrementers;

		private double value;

		// Initializes a statistic with an initial value.
		public Statistic (double initValue) {
			value = initValue;
			statOverriders = new List<StatModifier> ();
			statMultipliers = new List<StatModifier> ();
			statIncrementers = new List<StatModifier> ();
		}

        /* Returns the modified value of the statistic. It takes into account all effects that modify
         this statistic in the current context. Effects that increase the value by a percentage are applied first,
         then effects that Add a fixed value, then effects that replace the value by another.*/
		public int GetValue () {
			double val = value;
			foreach (StatModifier mod in statMultipliers) {
				val = mod.getValue (val);
			}
			foreach (StatModifier mod in statIncrementers) {
                val = mod.getValue(val);
			}
			foreach (StatModifier mod in statOverriders) {
                val = mod.getValue(val);
			}
            // Rounded down.
			return (int) val;
		}

		public void RemoveModifier (StatModifier mod) {
			statOverriders.Remove (mod);
			statMultipliers.Remove (mod);
			statIncrementers.Remove (mod);
		}

        // Each modifier is either a multiplier (stat +X%), an incrementer (stat +X) or an overrider (stat =X).
        // To Add a new modifier, it must be dispatched to the right list.
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

