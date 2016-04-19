namespace CavesEtVarans.skills.core
{
	public interface IValueCalculator : IContextDependent
	{
		double Value (Context context);
	}
}
