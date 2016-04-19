namespace CavesEtVarans.rules
{
	class RulesConstants
	{
		public static readonly double STAT_PER_LEVEL = 1.04;
		public static readonly int MAX_AP = 120;
		public static readonly int ACTION_AP = 100;
		public static readonly int BASE_MOVEMENT_COST = 12;
		public static readonly int HEIGHT_DIVISOR = 3;
        public static readonly double LOS_THRESHOLD = 0.9;
		public static readonly double[] FLANKING_BONUSES = new double[] {0, .075, .15, .225, .3};

	}
}
