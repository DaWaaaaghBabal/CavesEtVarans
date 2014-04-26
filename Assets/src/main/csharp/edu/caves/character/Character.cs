using System;

namespace CavesEtVarans
{
	public class Character
	{
        private StatisticsManager statManager;
        private ResourceManager ressManager;

		public Character (string newName) 
        {
            statManager = new StatisticsManager();
            ressManager = new ResourceManager();
            ressManager.Add(Statistic.AP, new Resource(0, statManager.GetValue(Statistic.MAX_AP)));
            name = newName;
		}
		
		public void Activate() 
        {
            // TODO do other stuff...
            MainGUI.ActivateCharacter(this);
		}

        public void EndTurn() 
        {
            SetAP(GetAP() / 2);
        }

        private string name;

        public string GetName() 
        {
            return name;
        }

        public int GetAP() 
        {
            return ressManager.GetAP();
        }

        public void SetAP(int newValue) 
        {
            ressManager.SetAP(newValue);
        }

        public void IncrementAP(int newValue) 
        {
            ressManager.IncrementAP(newValue);
        }

        public Boolean CanBePaid(SkillCost cost)
        {
            return ressManager.CanBePaid(cost);
        }

        public void Pay(SkillCost cost)
        {
            ressManager.Pay(cost);
        }

        public int GetStatValue(string key)
        {
            return statManager.GetValue(key);
        }

        override public string ToString()
        {
            return base.ToString() + " " + GetName();
        }

    }

}


