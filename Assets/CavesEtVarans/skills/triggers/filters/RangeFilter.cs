﻿using System.Collections.Generic;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.target;
using CavesEtVarans.skills.targets;

namespace CavesEtVarans.skills.triggers.filters {
	public class RangeFilter : TriggerFilter {

        public IValueCalculator MinRange { set; get; }
        public IValueCalculator MaxRange { set; get; }
		public string CenterKey { set; get; }
		public string TargetKey { set; get; }

		public override bool Filter(Context c) {
			Character source = ReadContext(c, CenterKey) as Character;
			ITargetable triggeringCharacter = ReadContext(c, TargetKey) as ITargetable;
            int min = (int) MinRange.Value(c);
            int max = (int) MaxRange.Value(c);
            return GetArea(source.Tile, min, max).Contains(triggeringCharacter.Tile);
		}

        private HashSet<Tile> GetArea(ITargetable center, int minRange, int maxRange) {
            return Battlefield.GetArea(center.Tile, minRange, maxRange);
        }
    }
}
