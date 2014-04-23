using System;

namespace CavesEtVarans
{

	public class Statistic {

		public static readonly Properties statProperties = new Properties ("statProperties");

		private double value;
		private string displayName;


		// Initializes a string with an initial value and a display name.
		public Statistic (string dispName, double initValue) {
			displayName = dispName;
			value = initValue;


		}
	}
}

