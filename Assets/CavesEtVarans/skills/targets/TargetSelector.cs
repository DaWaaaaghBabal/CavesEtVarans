using System;
using System.Collections.Generic;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.graphics;
using CavesEtVarans.gui;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.target;

namespace CavesEtVarans.skills.targets {
    public class TargetSelector : ContextDependent, ITargetSelector {
        public string CenterKey { set; get; }
        public string TargetKey { set; get; }
        public IValueCalculator MinRange { set; get; }
        public IValueCalculator MaxRange { set; get; }
        public IValueCalculator AoEMinRadius { set; get; }
        public IValueCalculator AoEMaxRadius { set; get; }
        public int NumberOfTargets { get; set; }
        public TargetFilter TargetFilter { set; get; }
        public bool TargetGround { set; get; }
        public bool Optional { set; get; }
        public PlayerChoiceType PlayerChoice {
            set {
                playerChoiceStrategy = PlayerChoiceStrategy.ProvidePlayerChoice(value);
                playerChoiceStrategy.BatchSelectionCallback = SelectArea;
                playerChoiceStrategy.SelectionCallback = SelectTile;
                playerChoiceType = value;
            }
            get {
                return playerChoiceType;
            }
        }
        public bool LineOfSight {
            set {
                LoSFilter = LineOfSightFilter.ProvideLoSFilter(value);
                LoS = value;
            }
            get {
                return LoS;
            }
        }
        public bool AoELineOfSight {
            set {
                AoELoSFilter = LineOfSightFilter.ProvideLoSFilter(value);
                AoELoS = value;
            }
            get {
                return AoELoS;
            }
        }


        private PlayerChoiceType playerChoiceType;
        private bool LoS, AoELoS;
        private int aoeMinRadius, aoeMaxRadius;
        private LineOfSightFilter LoSFilter;
        private LineOfSightFilter AoELoSFilter;
        private PlayerChoiceStrategy playerChoiceStrategy;

        private TargetGroup targets;
        private Context context;
        private int count;

        public void Activate(Context context) {
            count = 0;
            this.context = context;
            GUIEventHandler.Get().ActivePicker = this;
            int minRange = (int) MinRange.Value(context);
            int maxRange = (int) MaxRange.Value(context);
            aoeMinRadius = (int) AoEMinRadius.Value(context);
            aoeMaxRadius = (int) AoEMaxRadius.Value(context);
            ITargetable center = ExtractCenter(context);
            targets = new TargetGroup();
            playerChoiceStrategy.Activate(
                GetArea(center, LoSFilter, minRange, maxRange)
            );
        }

        public void TargetCharacter(Character character) {
            TargetTile(character.Tile);
        }

        public void TargetTile(Tile tile) {
            playerChoiceStrategy.TargetTile(tile);
        }

        private HashSet<Tile> GetArea(ITargetable center, LineOfSightFilter LoSFilter, int minRange, int maxRange) {
            HashSet<Tile> result = new HashSet<Tile>();
            HashSet<Tile> area = Battlefield.GetArea(center.Tile, minRange, maxRange);
            foreach (Tile t in area) {
                if (LoSFilter.LoS(center.Tile, center.Size, t, TargetGround)) {
                    result.Add(t);
                }
            }
            return result;
        }

        private void SelectArea(HashSet<Tile> area) {
            bool count = false;
            foreach (Tile t in area) {
                count |= FinalizeSelection(t);
            }
            if(count) Count();
        }

        private void SelectTile(Tile tile) {
            ITargetable center = TileToTarget(tile);
            HashSet<Tile> aoe = GetArea (tile, AoELoSFilter, aoeMinRadius, aoeMaxRadius);
            SelectArea(aoe);
        }

        private bool FinalizeSelection(Tile tile) {
            ITargetable target = TileToTarget(tile);
            if (target != null && TargetFilter.Filter(target)) {
                targets.Add(target);
                return true;
            }
            return false;
        }

        private ITargetable TileToTarget(Tile tile) {
            ITargetable target = null;
            if (TargetGround) {
                target = tile;
            } else {
                if (tile.Character != null)
                    target = tile.Character;
            }
            return target;
        }

        private void Count() {
            if (++count == NumberOfTargets) {
                EndPicking();
            }
        }

        private ITargetable ExtractCenter(Context context) {
            ITargetable center = ReadContext(context, CenterKey) as ITargetable;
            return center;
        }

        private void EndPicking() {
            string compoundKey = Context.TARGETS + TargetKey;
            context.Set(compoundKey, targets);
            //@TODO decouple from graphics (use events);
            GraphicBattlefield.ClearHighlightedArea();
            ((Skill) ReadContext(context, Context.SKILL)).NextTargetPicker(context);
        }

        public void Cancel() {
            if (Optional)
                EndPicking();
            else
                GUIEventHandler.Get().Reset();
        }
    }
}
