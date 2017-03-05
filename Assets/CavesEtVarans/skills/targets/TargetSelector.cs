using System.Collections.Generic;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.graphics;
using CavesEtVarans.gui;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.filters;
using CavesEtVarans.skills.values;

namespace CavesEtVarans.skills.targets {
    public class TargetSelector : ContextDependent, ITargetSelector {

        public string CenterKey { set { centerKey = value; }
            get {
                if (centerKey == null)
                    centerKey = Context.SOURCE;
                return centerKey;
            }
        }
        public string TargetKey { set; get; }
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
        public IValueCalculator AoEMinRadius {
            set { aoeMinRadius = value; }
            get {
                if (aoeMinRadius == null)
                    aoeMinRadius = new FixedValue(0);
                return aoeMinRadius;
            }
        }
        public IValueCalculator AoEMaxRadius {
            set { aoeMaxRadius = value; }
            get {
                if (aoeMaxRadius == null)
                    aoeMaxRadius = new FixedValue(0);
                return aoeMaxRadius;
            }
        }
        public int NumberOfTargets { get; set; }
        public FiltersList TargetFilters { set; get; }
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
                LoSFilter = LineOfSightCalculator.ProvideLoSFilter(value);
                LoS = value;
            }
            get {
                return LoS;
            }
        }
        public bool AoELineOfSight {
            set {
                AoELoSFilter = LineOfSightCalculator.ProvideLoSFilter(value);
                AoELoS = value;
            }
            get {
                return AoELoS;
            }
        }

        private string centerKey;
        private IValueCalculator minRange;
        private IValueCalculator maxRange;
        private IValueCalculator aoeMinRadius;
        private IValueCalculator aoeMaxRadius;
        private PlayerChoiceType playerChoiceType;
        private bool LoS, AoELoS;
        private int aoeMin, aoeMax;
        private LineOfSightCalculator LoSFilter;
        private LineOfSightCalculator AoELoSFilter;
        private PlayerChoiceStrategy playerChoiceStrategy;

        private TargetGroup targets;
        private Context context;
        private int count;

        public TargetSelector() {
            NumberOfTargets = 1;
            Optional = false;
            LineOfSight = true;
            AoELineOfSight = true;
        }

        public void Activate(Context context) {
            count = 0;
            this.context = context;
            GUIEventHandler.Get().ActivePicker = this;
            int minRange = (int) MinRange.Value(context);
            int maxRange = (int) MaxRange.Value(context);
            aoeMin = (int) AoEMinRadius.Value(context);
            aoeMax = (int) AoEMaxRadius.Value(context);
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

        private HashSet<Tile> GetArea(ITargetable center, LineOfSightCalculator LoSFilter, int minRange, int maxRange) {
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
            ITargetable center = tile;
            if (!TargetGround && tile.Character != null) { 
                    center = tile.Character;
            }
            HashSet<Tile> aoe = GetArea (center, AoELoSFilter, aoeMin, aoeMax);
            SelectArea(aoe);
        }

        private bool FinalizeSelection(Tile tile) {
            Context c = context.Duplicate();
            ITargetable target = TileToTarget(tile);
            c.Set(Context.FILTER_TARGET, target);
            if (target != null && TargetFilters.Filter(c)) {
                targets.Add(target);
                return true;
            }
            return playerChoiceStrategy.CanBeEmpty; // @TODO could be done more elegantly. Refactor.
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
            if (++count >= NumberOfTargets) {
                EndPicking();
            }
        }

        private ITargetable ExtractCenter(Context context) {
            object obj = ReadContext(context, CenterKey);
            ITargetable center = obj as ITargetable;
            if (center == null) {
                TargetGroup group = obj as TargetGroup;
                center = group.PickOne; 
            }
                
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
