using System;

namespace CavesEtVarans.skills.core {
	public abstract class ContextDependent : IContextDependent {

		private Context context;
		private Context PrivateContext {
			set { context = value; }
			get {
				if (context == null)
					context = Context.ProvidePrivateContext(this);
				if(Parent != null) {
					Parent.PrivateContext.CopyInto(context);
				}
				return context;
			}
		}
		[YamlDotNet.Serialization.YamlIgnore]
		public ContextDependent Parent { set; get; }

		protected object ReadContext(Context c, string key) {
			return c.Get(key, PrivateContext);
		}
		public void SetLocalContext(string key, object value) {
			PrivateContext.Set(key, value);
		}

		public IContextDependent Clone() {
			ContextDependent obj = MemberwiseClone() as ContextDependent;
			obj.PrivateContext = Context.ProvidePrivateContext(this);
			return obj;
		}
		/// <summary>
		/// Clones the context given as argument, then copies the private contaxt into it.
		/// </summary>
		protected Context CopyContext(Context original) {
			Context copy = original.Duplicate();
			PrivateContext.CopyInto(copy);
			return copy;
		}
	}
}
