using System;
using System.Collections.Generic;

namespace CavesEtVarans
{
    class SkillCost
    {
        private Dictionary<string, int> elements;

        protected Dictionary<string, int> getElements()
        {
            if (elements == null)
            {
                elements = new Dictionary<string, int>();
            }
            return elements;
        }

        public void Add(string key, int value) 
        {
            getElements().Add(key, value);
        }

        public int Get(string key)
        {
            if (getElements().ContainsKey(key))
            {
                return elements[key];
            }
            return 0;
        }
    }
}

