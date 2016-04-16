using System;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.rules;

namespace CavesEtVarans.skills.target {
    public abstract class LineOfSightFilter {
        public Character Source { get; internal set; }
        public bool GroundTarget { private get; set; }

        private class IgnoreLoS : LineOfSightFilter {
            public override bool LoS(Tile tile) {
                // Ignoring LoS means we always have LoS.
                return true;
            }
        }
        private class CheckLoS : LineOfSightFilter {

            public override bool LoS(Tile tile) {
                double cover = Cover(tile);
                return cover < RulesConstants.LOS_THRESHOLD;
            }
        }

        public static LineOfSightFilter ProvideLoSFilter(bool ignoreLoS) {
            if (ignoreLoS) return new IgnoreLoS();
            else return new CheckLoS();
        }

        public double Cover(Tile tile) {
            int targetHeight = 1;
            if (!GroundTarget & tile.Character != null)
                targetHeight = tile.Character.Size;
            double cover = 0;
            for (int i = 1 ; i <= targetHeight ; i++) {
                Line<ICoordinates> line = Battlefield.LineOfSight(tile, targetHeight, Source.Tile, Source.Size);
                double lineCover = 0;
                foreach (Obstacle obs in line) {
                    if (obs != null && obs.Cover > lineCover)
                        lineCover = obs.Cover;
                }
                cover += lineCover / targetHeight;
            }
            return cover;
        }

        public abstract bool LoS(Tile target);
    }
}