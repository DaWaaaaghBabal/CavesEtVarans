using CavesEtVarans.rules;
using CavesEtVarans.skills.core;
using CavesEtVarans.utils;
using System.Collections.Generic;

namespace CavesEtVarans.character.statistics {
	// The role of the statistics manager is to keep track of all the character's statistics.
	// It also dispatches stat modifiers to the relevant statistics.
	public class StatisticsManager
	{
	
		private Dictionary<string, Statistic> statistics;

		public StatisticsManager ()
		{
			statistics = new Dictionary<string, Statistic> ();

			InitializeSpecificStats ();

			InitializeConstantStats ();
		}
		// These statistics are specific to each character. Their value should be read in the save file.
		private void InitializeSpecificStats ()
		{
			statistics.Add(Statistic.HEALTH, new Statistic (24.0));
			statistics.Add(Statistic.DEFENSE, new Statistic (8.0));
			statistics.Add(Statistic.ATTACK, new Statistic(8.0));
			statistics.Add(Statistic.DAMAGE, new Statistic(8.0));
			statistics.Add(Statistic.WILLPOWER, new Statistic (8.0));
			statistics.Add(Statistic.DODGE, new Statistic (8.0));
			statistics.Add(Statistic.SPECIAL, new Statistic (8.0));
			statistics.Add(Statistic.INITIATIVE, new Statistic (8.0));
			statistics.Add(Statistic.ITERATION_MALUS, new Statistic (25.0));
			statistics.Add(Statistic.JUMP, new Statistic (2.0));
			statistics.Add(Statistic.MAX_ENERGY, new Statistic (10));
			statistics.Add(Statistic.MOVEMENT_COST, new Statistic(0));
		}

		// These statistics always have the same value for every character. They are defined in the game rules.
		private void InitializeConstantStats ()
		{
			statistics.Add (Statistic.MAX_AP, new Statistic (RulesConstants.MAX_AP));
			statistics.Add(Statistic.ACTION_AP, new Statistic(RulesConstants.ACTION_AP));
		}

		// Returns the value of the statistic referenced by the string given as a key.
		public int GetValue (string key, Context context)
		{
			return statistics [key].GetValue (context);
		}

		// Adds a modifier to the statistic referenced by the string given as a key.
		public void AddModifier (string key, IStatModifier modifier)
		{
			statistics [key].AddModifier (modifier);
		}
		// Removes a modifier from the statistic referenced by the string given as a key.
		public void RemoveModifier (string key, IStatModifier modifier)
		{
			statistics [key].RemoveModifier (modifier);
		}

		public void AddStatObserver(string key, Observer<StatChange> observer) {
			
		}
	}
}
