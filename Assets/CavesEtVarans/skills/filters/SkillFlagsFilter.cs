using CavesEtVarans.exceptions;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.filters;

namespace CavesEtVarans.skills.filters {

    public class SkillFlagsFilter : AbstractFilter {
        public FlagsList<SkillFlag> Flags { get; set; }
        public FilterType Operation { set; get; }
        public override bool Filter(Context c) {
            Skill skill = ReadContext(c, Context.TRIGGERING_SKILL) as Skill;
            FlagsList<SkillFlag> flags = skill.Flags;
            switch(Operation) {
                case FilterType.And:
                    return And(flags);
                case FilterType.Or:
                    return Or(flags);
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
