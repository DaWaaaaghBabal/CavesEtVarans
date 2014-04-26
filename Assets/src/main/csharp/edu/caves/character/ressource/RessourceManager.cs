using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans
{
    /*
     * The role of the RessourceManager is to track the behavior of all 
     * ressource components of a character, and to inform any observer of
     * any change.
     * The filling of ressources from a character is done when the 
     * character's data is parsed
     */ 
    class RessourceManager
    {
        private Dictionary<string, Ressource> ressources;

        protected Dictionary<string, Ressource> GetRessources() 
        {
            if(ressources == null) 
            {
                ressources = new Dictionary<string, Ressource>();
            }
            return ressources;
        }

        public Ressource Get(string key) 
        {
            if (GetRessources().ContainsKey(key)) 
            {
                return ressources[key];
            }
            return null;
        }

        public void Add(string key, Ressource ress)
        {
            GetRessources().Add(key, ress);
        }

    }

}
