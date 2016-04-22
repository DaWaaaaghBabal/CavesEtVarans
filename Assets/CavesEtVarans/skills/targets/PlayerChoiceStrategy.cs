using System;
using System.Collections.Generic;
using CavesEtVarans.battlefield;
using CavesEtVarans.exceptions;
using CavesEtVarans.graphics;

namespace CavesEtVarans.skills.targets { 
    /// <summary>
    /// Validating targets comes in three flavours :
    ///	1/ No validation : targets are selected automatically and the skill is used without confirmation.
    /// 2/ User validation : targets are selected automatically, but the player can confirm or cancel.
    /// 3/ User choice : targets are chosen by the player.
    /// </summary>
    public enum PlayerChoiceType {
        NoValidation, PlayerValidation, PlayerChoice
    }
    
    public abstract class PlayerChoiceStrategy {
        protected TargetSelector targetSelector;
        public Action<HashSet<Tile>> BatchSelectionCallback { get; set; }
        public Action<Tile> SelectionCallback { get; set; }
        protected HashSet<Tile> area;
        public static PlayerChoiceStrategy ProvidePlayerChoice(PlayerChoiceType type) {
            switch (type) {
                case PlayerChoiceType.NoValidation:
                    return new NoValidation();
                case PlayerChoiceType.PlayerValidation:
                    return new PlayerValidation();
                case PlayerChoiceType.PlayerChoice:
                    return new PlayerChoice();
                default:
                    throw new UndefinedValueException("Property \"PlayerChoiceType\" of type TargetPicker must be set (NoValidation, PlayerValidation, PlayerChoice).");
            }
        }
        public abstract void Activate(HashSet<Tile> area);
        public abstract void TargetTile(Tile tile);

        protected void Highlight(HashSet<Tile> area) {
            GraphicBattlefield.ClearHighlightedArea();
            GraphicBattlefield.HighlightArea(area);
        }
            
        private class NoValidation : PlayerChoiceStrategy {
            public override void Activate(HashSet<Tile> area) {
                BatchSelectionCallback(area);
            }

            public override void TargetTile(Tile tile) {
                throw new TargetPickerExecutionException("A no-validation target selector should never have to call TargetTile");
            }
        }

        private class PlayerChoice : PlayerChoiceStrategy {
            public override void Activate(HashSet<Tile> area) {
                Highlight(area);
                this.area = area;
            }

            public override void TargetTile(Tile tile) {
                if (area.Contains(tile)) SelectionCallback(tile);
            }
        }

        private class PlayerValidation : PlayerChoiceStrategy {
            public override void Activate(HashSet<Tile> area) {
                Highlight(area);
                this.area = area;
            }

            public override void TargetTile(Tile tile) {
                if (area.Contains(tile)) BatchSelectionCallback(area);
            }
        }

    }
}
