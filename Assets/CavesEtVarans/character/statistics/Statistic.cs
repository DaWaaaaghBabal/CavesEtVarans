using CavesEtVarans.skills.core;
using CavesEtVarans.utils;
using System.Collections.Generic;

namespace CavesEtVarans.character.statistics
{

	// A statistic is a value that is modified by a number of effects. Each statistic stores its base value and all effects that alter it.
	public class Statistic : Observable<StatChange>
	{
		public static readonly string ATTACK = "ATK";
		public static readonly string DEFENSE = "DEF";
		public static readonly string DAMAGE = "DMG";
		public static readonly string WILLPOWER = "WP";
		public static readonly string HEALTH = "HP";
		public static readonly string INITIATIVE = "INI";
		public static readonly string STABILITY = "STA";
		public static readonly string JUMP = "JMP";
		public static readonly string ITERATION_MALUS = "ITER";
		public static readonly string SPECIAL = "SPE";
		public static readonly string MELEE_RANGE = "MELEE";

		public static readonly string MAX_AP = "MAP";
		public static readonly string ACTION_AP = "AAP";
		public static readonly string MAX_ENERGY = "ME";
		public static readonly string MOVEMENT_COST = "MC";

		private List<IStatModifier> statModifiers;

		private double value;

		// Initializes a statistic with an initial value.
		public Statistic (double initValue)
		{
			value = initValue;
			statModifiers = new List<IStatModifier> ();
		}

		/* Returns the modified value of the statistic. It takes into account all effects that modify
         this statistic in the current context. Effects that increase the value by a percentage are applied first,
         then effects that add a fixed value, then effects that replace the value by another.*/
		public int GetValue (Context context)
		{
			double val = value;
			foreach (IStatModifier mod in statModifiers) {
				val = mod.GetValue (val, context);
			}
			// Rounded down.
			return (int)val;
		}

		public void RemoveModifier (IStatModifier mod)
		{
			statModifiers.Remove (mod);
		}

		public void AddModifier (IStatModifier mod)
		{
			// Each modifier is either a multiplier (stat +X%), an incrementer (stat +X) or an overrider (stat =X).
			// To add a new modifier, it must be dispatched to the right list.
			int i = 0;
			foreach (IStatModifier m in statModifiers) {
				if (mod.Type >= m.Type)
					i++;
			}
			statModifiers.Insert(i, mod);
			NotifyStatChange();
		}

		private void NotifyStatChange() {
			Context context = Context.ProvideDisplayContext();
			int newValue = GetValue(context);
			Notify (new StatChange { NewValue = newValue });
		}
	}
	// Event data used when a character stat is modified
	public class StatChange {
		public int NewValue { set; get; }
	}
}

