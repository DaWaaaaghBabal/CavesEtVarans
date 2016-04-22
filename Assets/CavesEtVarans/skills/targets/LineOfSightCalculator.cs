using CavesEtVarans.battlefield;
using CavesEtVarans.rules;

namespace CavesEtVarans.skills.targets {
    public abstract class LineOfSightCalculator {
        public static LineOfSightCalculator ProvideLoSFilter(bool checkLoS) {
            if (checkLoS) return new CheckLoS();
            else return new IgnoreLoS();
        }
        
        public virtual double Cover(ICoordinates source, int sourceHeight, Tile targetTile, bool groundTarget) {
            int targetHeight = 1;
            if (!groundTarget & targetTile.Character != null)
                targetHeight = targetTile.Character.Size;
            double cover = 0;
            for (int i = 1 ; i <= targetHeight ; i++) {
                Line<ICoordinates> line = Battlefield.LineOfSight(source, sourceHeight, targetTile, targetHeight);
                double lineCover = 0;
                foreach (Obstacle obs in line) {
                    if (obs != null && obs.Cover > lineCover)
                        lineCover = obs.Cover;
                }
                cover += lineCover / targetHeight;
            }
            return cover;
        }

        public abstract bool LoS(ICoordinates source, int sourceHeight, Tile targetTile, bool groundTarget);
 
        private class IgnoreLoS : LineOfSightCalculator {
            public override bool LoS(ICoordinates source, int sourceHeight, Tile targetTile, bool groundTarget) {
                // Ignoring LoS means we always have LoS.
                return true;
            }
            public override double Cover(ICoordinates source, int sourceHeight, Tile targetTile, bool groundTarget) {
                return 0;
            }
        }
        private class CheckLoS : LineOfSightCalculator {
            public override bool LoS(ICoordinates source, int sourceHeight, Tile targetTile, bool groundTarget) {
                double cover = Cover(source, sourceHeight, targetTile, groundTarget);
                return cover < RulesConstants.LOS_THRESHOLD;
            }
        }
    }
}