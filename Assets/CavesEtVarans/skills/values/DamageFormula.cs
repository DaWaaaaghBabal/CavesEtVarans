using System;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.values
{
    /// <summary>
    /// The standard formula for calculating damage is :
    /// 1 + 0.5 x tanh(4.5 x (atk - def) / (atk + def))
    /// where atk and def are two stats defined by the skill
    /// (attack and defense for physical attacks, willpower against willpower 
    /// for magical attacks, or any other stats).
    /// Flanking also has an impact : +7.5% per flanking step.
    /// (attacking from front = +0%, front flank = +7.5%, etc up to 30% for a back attack.
    /// </summary>
	public class DamageFormula : ContextDependent, IValueCalculator
	{
        private static double[] flankingBonuses = new double[] {0, .075, .15, .225, .3};
		public IValueCalculator Attack { set; get; }
		public IValueCalculator Defense { set; get; }

		public double Value(Context context)
		{
			double diff = Attack.Value(context) - Defense.Value(context);
			double sum = Attack.Value(context) + Defense.Value(context);
            double ratio = 4.5 * diff / sum;
            Character source = ReadContext(context, Context.SOURCE) as Character;
            Character target = ReadContext(context, Context.CURRENT_TARGET) as Character;
            Flanking flanking = Battlefield.Flanking(source, target);
            return 1.0 + 0.5 * Math.Tanh(ratio) + flankingBonuses[(int) flanking];
		}
	}
}
