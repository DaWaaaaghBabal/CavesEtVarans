using System;

namespace CavesEtVarans.skills.core {
	public abstract class ContextDependent {
		private Context context;
		private Context PrivateContext {
			set { context = value; }
			get {
				if (context == null)
					context = Context.ProvidePrivateContext(this);
				return context;
			}
		}
		protected Object ReadContext(Context c, string key) {
			return c.Get(key, PrivateContext);
		}
		public void SetLocalContext(string key, object value) {
			PrivateContext.Set(key, value);
		}
	}
}
