namespace CavesEtVarans.skills.core {
	public interface IEffect : IContextDependent
	{
		void Apply (Context context);
	}
}
