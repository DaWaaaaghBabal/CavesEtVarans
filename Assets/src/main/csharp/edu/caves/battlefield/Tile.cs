using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    public class Tile : IDisposable {

        // Position in the game grid.
        private List<StatModifier> movementCostMultipliers;
        private List<StatModifier> movementCostIncrementers;
        private List<StatModifier> movementCostOverriders;
        public Tile() {

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

        public void Dispose() {
            // Do nothing yet.
        }
    }
}
