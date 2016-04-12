﻿using CavesEtVarans.skills.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans.skills.core {
	public delegate void EventBroadcaster<T>(T e, Context context) where T : GameEvent<T>;
	public abstract class GameEvent<T> where T : GameEvent<T> {
		public static event EventBroadcaster<T> Listeners = Mock;
        private static void Mock(T e, Context c)
        {
            //This method is here to provide a default listener so Listeners is correctly initialised.
        }
		public void Trigger(Context c) {
			Listeners((T)this, c);
		}
	}
}