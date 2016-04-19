namespace CavesEtVarans.skills.core {
	public interface IContextDependent {
		void SetLocalContext(string key, object value);
		IContextDependent Clone();
		ContextDependent Parent { set; get; }
	}
}