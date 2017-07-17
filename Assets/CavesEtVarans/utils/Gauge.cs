namespace CavesEtVarans.utils
{
	/**
     *  A Gauge is an integer value between a min and a max value. Incrementing
     *  or decrementing this value does not make it to go beyond its limits.
     */
	public class Gauge : Observable<GaugeChange>
	{
		private int min;
		private int max;
		public int Value {
            get;
            private set; }
        public double Percentage {
            get {
                return (Value - min) / (double)(max - min);
            }
            private set { }
        }

        public Gauge (int newMin, int newMax)
		{
			min = newMin;
			max = newMax;
			Value = min;
		}

		public bool CanBeIncreased (int qty)
		{
			return Value + qty <= max; 
		}

		public bool CanBeDecreased (int qty)
		{
			return Value - qty >= min;
		}

        /// <summary>
        /// Increases a gauge while staying within its limits. Returns the amount by which the gauge was actually increased.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
		public int Increase (int amount)
		{
            if (amount < 0 && Value + amount < min)
                amount = Value;
            else if (amount > 0 && Value + amount > max)
                amount = max - Value;
			Value += amount;
            return amount;
		}

		public int SetValue (int newValue)
		{
			if (newValue < min) {
				Value = min;
			} else if (newValue > max) {
				Value = max;
			} else {
				Value = newValue;
			}
			return Value;
		}

		override public string ToString ()
		{
			return Value + "/" + max;
		}

		private void NotifyGaugeChange () {
			Notify(new GaugeChange() {
				Min = min,
				Max = max,
				Value = Value,
				Percentage = Percentage
			});
		}
	}

	public class GaugeChange {
		public int Min { set; get; }
		public int Max { set; get; }
		public int Value { set; get; }
		public double Percentage { set; get; }
	}
}


