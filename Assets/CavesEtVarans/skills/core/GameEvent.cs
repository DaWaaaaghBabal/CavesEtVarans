using CavesEtVarans.skills.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CavesEtVarans.skills.events;

namespace CavesEtVarans.skills.core {
	public delegate void EventBroadcaster<T>(T e) where T : GameEvent<T>;
	public abstract class GameEvent<T> where T : GameEvent<T> {
		public abstract TriggerType TriggerType();
		public static event EventBroadcaster<T> Listeners = Mock;
        private static void Mock(T e)
        {
            //This method is here to provide a default listener so the Listeners delegate is correctly initialised.
        }
		public void Trigger() {
			Listeners((T)this);
		}
	}
}
