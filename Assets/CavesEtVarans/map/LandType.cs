using CavesEtVarans.rules;

namespace CavesEtVarans.map {
    public class LandType {
        private LandType() { }
        public static readonly LandType FOREST = new LandType() { MovementCost = 2 * RulesConstants.BASE_MOVEMENT_COST };
        public static readonly LandType SEA = new LandType() { MovementCost = 5 * RulesConstants.BASE_MOVEMENT_COST };
        public static readonly LandType PLAIN = new LandType() { MovementCost = RulesConstants.BASE_MOVEMENT_COST };

        public int MovementCost { get; internal set; }
    }
}