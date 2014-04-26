using System;
using System.Collections.Generic;
using CavesEtVarans;

namespace CavesEtVarans
{
    // The role of the statistics manager is to keep track of all the character's statistics.
    // It also dispatches stat modifiers to the relevant statistics.
	public class StatisticsManager {
	
		private Dictionary<string, Statistic> statistics;

        public StatisticsManager() {
            statistics = new Dictionary<string, Statistic>();
            statistics.Add(Statistic.BASE_HIT_CHANCE, new Statistic(50.0));
            statistics.Add(Statistic.BONUS_TO_DEF, new Statistic(0.0));
            statistics.Add(Statistic.BONUS_TO_HIT, new Statistic(50.0));
            statistics.Add(Statistic.DEFENSE, new Statistic(8.0));
            statistics.Add(Statistic.DODGE, new Statistic(8.0));
            statistics.Add(Statistic.HEALTH, new Statistic(24.0));
            statistics.Add(Statistic.INITIATIVE, new Statistic(8.0));
            statistics.Add(Statistic.ITERATION_MALUS, new Statistic(25.0));
            statistics.Add(Statistic.JUMP, new Statistic(2.0));
            statistics.Add(Statistic.MAX_AP, new Statistic(120.0));
            statistics.Add(Statistic.ACTION_AP, new Statistic(100.0));
            statistics.Add(Statistic.MELEE_ATTACK, new Statistic(8.0));
            statistics.Add(Statistic.MELEE_DAMAGE, new Statistic(8.0));
            statistics.Add(Statistic.RANGE_ATTACK, new Statistic(8.0));
            statistics.Add(Statistic.RANGE_DAMAGE, new Statistic(8.0));
            statistics.Add(Statistic.SPECIAL, new Statistic(8.0));
            statistics.Add(Statistic.SPELL_POWER, new Statistic(8.0));
            statistics.Add(Statistic.WILLPOWER, new Statistic(8.0));
        }

        // Returns the value of the statistic referenced by the string given as a key.
        public int GetValue(string key) {
            return statistics[key].GetValue();
        }

        // Adds a modifier to the statistic referenced by the string given as a key.
        public void AddModifier(string key, StatModifier modifier) {
            statistics[key].AddModifier(modifier);
        }
        // Removes a modifier from the statistic referenced by the string given as a key.
        public void RemoveModifier(string key, StatModifier modifier) {
            statistics[key].RemoveModifier(modifier);
        }

    }
}
