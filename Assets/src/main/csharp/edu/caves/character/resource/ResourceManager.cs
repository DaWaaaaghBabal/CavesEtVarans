using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans
{
    /*
     * The role of the ResourceManager is to track the behavior of all 
     * resource components of a character, and to inform any observer of
     * any change.
     * The filling of resources from a character is done when the 
     * character's data is parsed
     */ 
    class ResourceManager
    {
        private Dictionary<string, Resource> resources;

        protected Dictionary<string, Resource> GetResources() 
        {
            if(resources == null) 
            {
                resources = new Dictionary<string, Resource>();
            }
            return resources;
        }

        public Resource Get(string key) 
        {
            if (GetResources().ContainsKey(key)) 
            {
                return resources[key];
            }
            return null;
        }

        public void Add(string key, Resource res)
        {
            GetResources().Add(key, res);
        }

        public Boolean CanBePaid(SkillCost cost)
        {

            foreach (string name in cost.GetElements().Keys)
            {
                if (!GetResources().ContainsKey(name)
                    || !resources[name].CanBePaid(cost.Get(name))
                ) 
                {
                    return false;
                }
            }
            return true;
        }

        public void Pay(SkillCost cost)
        {
            foreach (string name in cost.GetElements().Keys)
            {
                resources[name].Pay(cost.Get(name));
            }
        }

    }

}
