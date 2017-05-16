using System;
using System.Collections.Generic;

namespace CavesEtVarans.skills.core {
	public abstract class ContextDependent : IContextDependent {
        private class Context {

            private Stack<Dictionary<string, Object>> data;
            private Dictionary<string, Object> currentData;
            private static Context instance;
            public static Context Current() {
                if (instance == null)
                    instance = new Context();
                return instance;
            }

            private Context() {
                data = new Stack<Dictionary<string, object>>();
                CreateEmptyContext();
            }

            private void CreateEmptyContext() {
                Dictionary<string, object> empty = new Dictionary<string, object>();
                data.Push(empty);
                currentData = empty;
            }

            public void NewLayer() {
                Dictionary<string, Object> dataLayer = new Dictionary<string, object>();
                dataLayer[ContextKeys.TARGETS] = new TargetGroup();
                data.Push(dataLayer);
                currentData = dataLayer;
            }

            public void EndLayer() {
                data.Pop();
                if (data.Count == 0)
                    CreateEmptyContext();
                else 
                    currentData = data.Peek();
            }

            public void Set(string key, Object value) {
                currentData.Remove(key);
                currentData.Add(key, value);
            }

            // Retrieves whatever is stored in the Context under a given key. Returns null if there is nothing.
            public Object Get(string key) {
                return currentData.ContainsKey(key) ? currentData[key] : null;
            }

            public void Duplicate() {
                Dictionary<string, Object> old = currentData;
                NewLayer();
                foreach (KeyValuePair<string, object> kv in old) {
                    Set(kv.Key, kv.Value);
                }
            }
        }

		private Dictionary<string, object> privateContext;
		[YamlDotNet.Serialization.YamlIgnore]
		public ContextDependent Parent { set; get; }

        public void SetLocalContext(string key, object value) {
            privateContext.Remove(key);
            privateContext.Add(key, value);
        }
        public void SetLocalContext(Dictionary<string, object> contextData) {
            foreach (KeyValuePair<string, object> entry in contextData) {
                SetLocalContext(entry.Key, entry.Value);
            }
        }

        public ContextDependent() {
            privateContext = new Dictionary<string, object>();
        }
		public ContextDependent Clone() {
			ContextDependent obj = MemberwiseClone() as ContextDependent;
            obj.privateContext = new Dictionary<string, object>();
			return obj;
		}
		protected object ReadContext(string key) {
            if (privateContext.ContainsKey(key))
                return privateContext[key];
            else if (Parent == null)
                return Context.Current().Get(key);
            return Parent.ReadContext(key);
		}

        protected void SetContext(string key, object value) {
            Context.Current().Set(key, value);
        }

        protected void StartNewContext() {
            Context.Current().NewLayer();
        }
        protected void CloneContext() {
            Context.Current().Duplicate();
        }
        protected void EndContext() {
            Context.Current().EndLayer();
        }


	}
}
