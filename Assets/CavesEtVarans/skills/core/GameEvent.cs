using System.Collections.Generic;
using CavesEtVarans.skills.events;

namespace CavesEtVarans.skills.core {
	public delegate void EventBroadcaster<T>(T e) where T : GameEvent<T>;
	public abstract class GameEvent<T> where T : GameEvent<T> {
		public abstract TriggerType TriggerType();
        public Dictionary<string, object> EventData { private set; get; }
        public static event EventBroadcaster<T> Listeners;
		public void Trigger() {
			if(Listeners != null)
                Listeners((T)this);
		}
        public GameEvent() {
            EventData = new Dictionary<string, object>();
        }
	}
}
