using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    public class Tile {
        // Position in the game grid.
        private int x, y, h;
        private List<StatModifier> movementCostMultipliers;
        private List<StatModifier> movementCostIncrementers;
        private List<StatModifier> movementCostOverriders;
        public Tile(int i, int j, int height) {
            x = i;
            y = j;
            h = height;

            movementCostOverriders = new List<StatModifier>();
            movementCostMultipliers = new List<StatModifier>();
            movementCostIncrementers = new List<StatModifier>();
        }

        public int GetMovementCost(int originalValue) {
            double value = originalValue;
            foreach (StatModifier modifier in movementCostMultipliers) {
                value = modifier.GetValue(value);
            }
            foreach (StatModifier modifier in movementCostIncrementers) {
                value = modifier.GetValue(value);
            }
            foreach (StatModifier modifier in movementCostOverriders) {
                value = modifier.GetValue(value);
            }
            return originalValue;
        }
    }
}
