using System;

namespace CavesEtVarans
{
	public class Character
	{
        public Random rand = new Random();
        private Gauge AP;
        private StatisticsManager statManager;

		public Character (string newName) {
            statManager = new StatisticsManager();
            AP = new Gauge(0, statManager.GetValue(Statistic.MAX_AP));
            name = newName;
		}
		
		public void Activate() {
            // TODO do other stuff...
            MainGUI.DisplayCharacter(this);
		}
        public void EndTurn() {
            SetAP(GetAP() / 2);
            CharacterManager.Get().ActivateNext();
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

        public int GetStatValue(string key) {
            return statManager.GetValue(key);
        }
    }
}

