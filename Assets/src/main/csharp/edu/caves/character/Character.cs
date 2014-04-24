using System;

namespace CavesEtVarans
{
	public class Character
	{
        public Random rand = new Random();
        private Gauge AP;
        private StatisticsManager statManager;

		public Character () {
            statManager = new StatisticsManager();
            AP = new Gauge(0, statManager.GetValue(Statistic.MAX_AP));
		}
		
		public void Activate() {
            // TODO do other stuff...
            SetAP(GetAP() / 2);
		}

        internal bool runAction()
        {
            IncrementAP(statManager.GetValue(Statistic.INITIATIVE) + Dice.Roll(1,3));
            return GetAP() >= statManager.GetValue(Statistic.ACTION_AP);
        }

        public int GetAP() {
            return AP.GetValue();
        }
        public void SetAP(int newValue) {
            AP.SetValue(newValue);
        }
        public void IncrementAP(int newValue) {
            AP.SetValue(AP.GetValue() + newValue);
            System.Diagnostics.Debug.WriteLine("incrementing value to " + AP.GetValue());
        }

    }
}

