using CavesEtVarans.character;
using CavesEtVarans.graphics;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;
using UnityEngine;

namespace CavesEtVarans.gui
{
    public class FloatingTextHandler : MonoBehaviour
    {
        public GameObject floatingTextPrefab;
        public Color damageColor;
        public Color healColor;
        public Canvas canvas;
        void Start() {
            DamageEvent.Listeners += HandleDamageEvent;
            HealEvent.Listeners += HandleHealEvent;
        }

        public void HandleDamageEvent(DamageEvent e, Context c) {
            FloatingText dmgText = SpawnOverCharacter(e.Target);
            dmgText.DisplayedText = "-" + e.Amount;
            dmgText.Color = damageColor;
        }
        public void HandleHealEvent(HealEvent e, Context c) {
            FloatingText healText = SpawnOverCharacter(e.Target);
            healText.DisplayedText = "+" + e.Amount;
            healText.Color = healColor;
        }

        private FloatingText SpawnOverCharacter(Character target) {
            GraphicCharacter graphicCharacter = GraphicBattlefield.GetSceneCharacter(target);
            Vector3 textLocation = Camera.main.WorldToScreenPoint(graphicCharacter.transform.position + new Vector3(0, 2.5f, 0));
            textLocation.z = 0;
            GameObject obj = Instantiate(floatingTextPrefab, textLocation, Quaternion.identity) as GameObject;
            FloatingText floatingText = obj.GetComponent<FloatingText>();
            floatingText.transform.SetParent(canvas.transform, false);
            return floatingText;
        }
    }
}
