using CavesEtVarans.character.statistics;
using CavesEtVarans.utils;
using UnityEngine;
using UnityEngine.UI;

namespace CavesEtVarans.gui {

	public class StatDisplay : MonoBehaviour, Observer<StatChange> {
		public string Key { get; set; }
		public Text statName;
		public Text statValue;
		void Observer<StatChange>.Update(StatChange data) {
			statValue.text = "" + data.NewValue;
		}
	}
}