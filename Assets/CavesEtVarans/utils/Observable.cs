using System.Collections.Generic;

namespace CavesEtVarans.utils
{
	public abstract class Observable<T> {
		private HashSet<Observer<T>> observers = new HashSet<Observer<T>>();

		public void register(Observer<T> observer) {
			if (!observers.Contains(observer))
				observers.Add(observer);
		}

		public void remove(Observer<T> observer) {
			if (observers.Contains(observer))
				observers.Remove(observer);
		}

		public void Notify(T data) {
			foreach(Observer<T> observer in observers) observer.Update(data);
		}
	}
}
