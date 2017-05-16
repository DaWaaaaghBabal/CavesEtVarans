namespace CavesEtVarans.skills.core
{
	public interface IValueCalculator
	{
		double Value ();
        ContextDependent Parent { set; get; }
	}
}
