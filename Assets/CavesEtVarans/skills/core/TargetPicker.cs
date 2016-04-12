using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.gui;
using System.Collections.Generic;
using CavesEtVarans.skills.target;
using CavesEtVarans.graphics;

namespace CavesEtVarans.skills.core {
	public class TargetPicker : ContextDependent, ITargetPicker {

        public string TargetKey { set; get; }
        public string SourceKey { set; get; }
        public int AoeRadius { set; get; }
        public int TargetNumber { set; get; }
        public int Range { get; internal set; }
        public FlagsList<TargetFlag> Flags { set; get; }
		/// <summary>
		/// If true, will target tiles instead of characters.
		/// </summary>
		public bool GroundTarget { 
			set {
				if (value)
					groundTargeting = new TileTargeting(new TargetFilter());
				else groundTargeting = new CharacterTargeting(new TargetFilter());
			}
			get { return groundTargeting.IsGroundTargeting(); }
		}

		/// <summary> Validating targets comes in three flavours :
		///	1/ No validation : targets are selected automatically and the skill is used without confirmation.
		/// 2/ User validation : targets are selected automatically, but the player can confirm or cancel.
		/// 3/ User choice : targets are chosen by the player.
		/// </summary>
		public PlayerChoiceType PlayerChoice {
			get { return playerChoice.ChoiceType(); }
			set {
				playerChoice = PlayerChoiceStrategy.GetStrategy(value, TargetCallback);
			}
		}

        private GroundTargetingStrategy groundTargeting;
        private PlayerChoiceStrategy playerChoice;
        private Context context;
		private HashSet<ITargetable> targets;
		private int targetCount;

        public TargetPicker() {
            Flags = new FlagsList<TargetFlag>();
        }

		public void Activate (Context context)
		{
			this.context = context;
			targetCount = 0;
			targets = new HashSet<ITargetable>();
			GUIEventHandler.Get().ActivePicker = this;
			//@TODO decouple from graphics (use events);
			GraphicBattlefield.ClearHighlightedArea();
			playerChoice.Activate(this, context);
		}

		public void EndPicking ()
		{
			string compoundKey = Context.TARGETS + TargetKey;
			context.Set(compoundKey, targets);
			//@TODO decouple from graphics (use events);
			GraphicBattlefield.ClearHighlightedArea();
			((Skill)ReadContext(context, Context.SKILL)).NextTargetPicker (context);
		}

		public void TargetTile(Tile tile) {
			if (playerChoice.TargetTile(tile)) CountTargets();
		}

		public void TargetCharacter (Character character) {
			if(playerChoice.TargetCharacter(character)) CountTargets();
		}

		private void CountTargets() {
			if (++targetCount == TargetNumber)
				EndPicking();
		}

		private bool TargetCallback(Tile t) {
			return groundTargeting.TargetTile(t, targets);
		}

        public void Cancel() {
            if (Flags[TargetFlag.Optional])
                EndPicking();
            else
                GUIEventHandler.Get().Reset();
        }
    }
}
