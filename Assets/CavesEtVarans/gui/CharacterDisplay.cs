using UnityEngine;
using UnityEngine.UI;

namespace CavesEtVarans.gui {

	public class CharacterDisplay : MonoBehaviour {
		// Used to know which stats should be displayed
		public string[] displayedStats;
		// Static display fields, initialized through Unity's editor (dependency injection-like);
		public Text characterName;

	}
}