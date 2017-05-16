using CavesEtVarans.character;
using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.filters;

namespace CavesEtVarans.skills.effects.buffs {
    public abstract class StatModifier : BuffEffect, IStatModifier {
        [YamlDotNet.Serialization.YamlIgnore]
        public abstract ModifierType Type { get; protected set; }
        private IValueCalculator val;
        public IValueCalculator Value {
            get { return val; }
            set {
                val = value;
                val.Parent = this;
            }
        }
        private FiltersList filters;
        public FiltersList Filters {
            get {
                if (filters == null)
                    filters = new FiltersList();
                return filters;
            }
            set {
                filters = value;
            }
        }
        public string StatKey { get; set; }

        public double GetValue(double originalValue) {
            return Filters.Filter() ? GetModifiedValue(originalValue) : originalValue;
        }

        protected abstract double GetModifiedValue(double originalValue);

        public override void ApplyOn(Character target) {
            target.ApplyStatModifier(StatKey, this);
        }

        public override void RemoveFrom(Character target) {
            target.RemoveStatModifier(StatKey, this);
        }
    }
}