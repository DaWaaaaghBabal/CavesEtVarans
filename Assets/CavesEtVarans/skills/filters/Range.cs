﻿using System.Collections.Generic;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.values;

namespace CavesEtVarans.skills.filters {
	public class Range : AbstractFilter {
        
		public string CenterKey { set; get; }
        public IValueCalculator MinRange {
            set { minRange = value; }
            get {
                if (minRange == null)
                    minRange = new FixedValue(0);
                return minRange;
            }
        }
        public IValueCalculator MaxRange {
            set { maxRange = value; }
            get {
                if (maxRange == null)
                    maxRange = new FixedValue(0);
                return maxRange;
            }
        }

        private IValueCalculator minRange;
        private IValueCalculator maxRange;
        protected override bool FilterContext() {
			Character source = ReadContext(CenterKey) as Character;
			ITargetable triggeringCharacter = ReadContext(TargetKey) as ITargetable;
            int min = (int) MinRange.Value();
            int max = (int) MaxRange.Value();
            return GetArea(source.Tile, min, max).Contains(triggeringCharacter.Tile);
		}

        private HashSet<Tile> GetArea(ITargetable center, int minRange, int maxRange) {
            return Battlefield.GetArea(center.Tile, minRange, maxRange);
        }
    }
}
