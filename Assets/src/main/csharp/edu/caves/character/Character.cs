using System;

namespace CavesEtVarans
{
	public class Character
	{
        private Gauge AP;
        private StatisticsManager statManager;

		public Character (string newName) {
            statManager = new StatisticsManager();
            AP = new Gauge(0, statManager.GetValue(Statistic.MAX_AP));
            name = newName;
		}
		
		public void Activate() {
            MainGUI.DisplayCharacter(this);
		}

        private string name;
        public string GetName() {
            return name;
        }

        public int GetAP() {
            return AP.GetValue();
        }
        public void SetAP(int newValue) {
            AP.SetValue(newValue);
        }
        public void IncrementAP(int newValue) {
            AP.SetValue(AP.GetValue() + newValue);
        }

        public int getStatValue(string key) {
            return statManager.GetValue(key);
        }
	}
}

