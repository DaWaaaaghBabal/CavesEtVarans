using System;

namespace CavesEtVarans
{
	public class Character
	{
        private Gauge AP;
        private StatisticsManager statManager;

		public Character () {
            statManager = new StatisticsManager();
            AP = new Gauge(0, statManager.GetValue(Statistic.MAX_AP));
		}
		
		public void Activate() {

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
	}
}

