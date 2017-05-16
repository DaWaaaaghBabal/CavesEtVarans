using CavesEtVarans.exceptions;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {

    public class SkillFlags: AbstractFilter {
        public FlagsList<SkillFlag> Flags { get; set; }
        public FilterType Operation { set; get; }
        protected override bool FilterContext() {
            Skill skill = ReadContext(ContextKeys.TRIGGERING_SKILL) as Skill;
            switch(Operation) {
                case FilterType.And:
                    return And(skill.Flags);
                case FilterType.Or:
                    return Or(skill.Flags);
                default:
                    throw new UndefinedValueException("Property \"Operation\" of type SkillFlagsFilter must be set (Or / And).");
            }
        }

        private bool Or(FlagsList<SkillFlag> flags) {
            foreach (SkillFlag flag in Flags)
                if (flags.Contains(flag)) return true;
            return false;
        }

        private bool And(FlagsList<SkillFlag> flags) {
            foreach (SkillFlag flag in Flags)
                if(!flags.Contains(flag)) return false;
            return true;
        }
    }
}
