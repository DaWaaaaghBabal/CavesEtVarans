using CavesEtVarans.rules;
using CavesEtVarans.skills.core;
using CavesEtVarans.utils;
using System.Collections.Generic;
using System;

namespace CavesEtVarans.character.statistics {
	// The role of the statistics manager is to keep track of all the character's statistics.
	// It also dispatches stat modifiers to the relevant statistics.
	public class StatisticsManager
	{
	
		private Dictionary<string, Statistic> statistics;

		public StatisticsManager ()
		{
			statistics = new Dictionary<string, Statistic> ();
            InitConstantStats ();
		}

        public void InitClassStats(CharacterClass clazz) {
            InitStat(Statistic.HEALTH, clazz.Health);
            InitStat(Statistic.DEFENSE, clazz.Defense);
            InitStat(Statistic.ATTACK, clazz.Attack);
            InitStat(Statistic.DAMAGE, clazz.Damage);
            InitStat(Statistic.WILLPOWER, clazz.Willpower);
            InitStat(Statistic.DODGE, clazz.Dodge);
            InitStat(Statistic.SPECIAL, clazz.Special);
            InitStat(Statistic.INITIATIVE, clazz.Initiative);
            InitStat(Statistic.ITERATION_MALUS, clazz.Iterative);
            InitStat(Statistic.JUMP, clazz.Jump);
            InitStat(Statistic.MAX_ENERGY, clazz.MaxEnergy);
        }

        // These statistics always have the same value for every character. They are defined in the game rules.
        private void InitConstantStats ()
		{
			InitStat(Statistic.MAX_AP, RulesConstants.MAX_AP);
			InitStat(Statistic.ACTION_AP, RulesConstants.ACTION_AP);
            InitStat(Statistic.MOVEMENT_COST, 0);
		}
        
        private void InitStat(string key, int value) {
            statistics.Add(key, new Statistic(value));
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
