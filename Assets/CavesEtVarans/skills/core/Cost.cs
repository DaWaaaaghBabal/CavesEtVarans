using CavesEtVarans.character.resource;
using System.Collections.Generic;

namespace CavesEtVarans.skills.core
{
	public class Cost
	{
		public Dictionary<string, int> Elements{get; set;}

		public Cost() {
			Elements = new Dictionary<string, int>();
		}

		public void Add (string key, int value)
		{
			Elements.Add (key, value);
		}

		public void Pay(ResourceManager ressManager) {
			foreach (KeyValuePair<string, int> elem in Elements) {
				ressManager.Decrement(elem.Key, elem.Value);
			}
		}

		public bool CanBePaid(ResourceManager ressManager) {
			foreach (KeyValuePair<string, int> elem in Elements) {
				if (!ressManager.CanBePaid(elem.Key, elem.Value))
					return false;
			}
			return true;
		}
	}
}

