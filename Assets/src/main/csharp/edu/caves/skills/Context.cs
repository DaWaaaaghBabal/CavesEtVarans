using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans
{
    public class Context
    {
        private static Dictionary<string, Object> context;


        public static void InitSkillContext(Skill skill, Character source) {
            context = new Dictionary<string, object>();
            context.Add("source", source);
            context.Add("skill", skill);
        }
    }
}
