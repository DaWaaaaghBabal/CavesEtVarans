using System;
using System.Collections.Generic;

namespace CavesEtVarans
{
    public class SkillCost
    {
        private Dictionary<string, int> elements;

        public Dictionary<string, int> GetElements()
        {
            if (elements == null)
            {
                elements = new Dictionary<string, int>();
            }
            return elements;
        }

        public void Add(string key, int value) 
        {
            GetElements().Add(key, value);
        }

        public int Get(string key)
        {
            if (GetElements().ContainsKey(key))
            {
                return elements[key];
            }
            return 0;
        }
    }
}

