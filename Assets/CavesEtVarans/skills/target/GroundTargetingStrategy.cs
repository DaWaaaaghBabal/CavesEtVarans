using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using System.Collections.Generic;
using System;

namespace CavesEtVarans.skills.target {
	public abstract class GroundTargetingStrategy {
		public abstract bool TargetTile(Tile tile, HashSet<ITargetable> targets);
		public abstract bool TargetCharacter(Character character, HashSet<ITargetable> targets);
		public abstract bool IsGroundTargeting();

		public GroundTargetingStrategy(TargetFilter filter) { targetFilter = filter; }

		private TargetFilter targetFilter;

		protected bool FilterTarget(ITargetable target, HashSet<ITargetable> targets) {
			if (targets.Contains(target))
				return false;
			if (targetFilter.Filter(target)) {
				targets.Add(target);
				return true;
			}
			return false;
		}
	}

	public class TileTargeting : GroundTargetingStrategy {

		public TileTargeting(TargetFilter filter) : base(filter) { }
	
		public override bool IsGroundTargeting() {
			return true;
		}

		public override bool TargetCharacter(Character character, HashSet<ITargetable> targets) {
			return FilterTarget(character.Tile, targets);
        }

		public override bool TargetTile(Tile tile, HashSet<ITargetable> targets) {
			return FilterTarget(tile, targets);
		}
	}

	public class CharacterTargeting : GroundTargetingStrategy {

		public CharacterTargeting(TargetFilter filter) : base(filter) { }
	
			public override bool IsGroundTargeting() {
			return false;
		}

		public override bool TargetCharacter(Character character, HashSet<ITargetable> targets) {
			return FilterTarget(character, targets);
		}

		public override bool TargetTile(Tile tile, HashSet<ITargetable> targets) {
			return tile.Character == null ? false : FilterTarget(tile.Character, targets);
		}
	}
}