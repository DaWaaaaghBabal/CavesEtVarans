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
		private int value;

		public Gauge (int newMin, int newMax)
		{
			min = newMin;
			max = newMax;
			value = min;
		}

		public int GetValue ()
		{
			return value;
		}

		public bool CanBeIncreased (int qty)
		{
			return GetValue () + qty <= max; 
		}

		public bool CanBeDecreased (int qty)
		{
			return GetValue () - qty >= min;
		}

		public int Increment (int value)
		{
			return SetValue (GetValue () + value);
		}

		public int Decrement (int value)
		{
			return SetValue (GetValue () - value);
		}

		public int SetValue (int newValue)
		{
			if (newValue < min) {
				value = min;
			} else if (newValue > max) {
				value = max;
			} else {
				value = newValue;
			}
			return value;
		}

		public double getPercentage ()
		{
			return (value - min) / (max - min);
		}

		override public string ToString ()
		{
			return value + "/" + max;
		}

		private void NotifyGaugeChange () {
			Notify(new GaugeChange() {
				Min = min,
				Max = max,
				Value = value,
				Percentage = getPercentage()
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


