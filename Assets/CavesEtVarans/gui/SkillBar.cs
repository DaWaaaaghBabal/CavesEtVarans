using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using UnityEngine;

namespace CavesEtVarans.gui {
    public class SkillBar : MonoBehaviour {
        public GameObject skillIconPrefab;
        public int size;
        public void DisplaySkills(Character character) {
            float offset = character.Skills.Count / 2.0f;
            Vector3 iconLocation = new Vector3(-offset * size, size / 2, 0);
            foreach (Skill skill in character.Skills) {
                GameObject obj =  Instantiate(skillIconPrefab, iconLocation, Quaternion.identity) as GameObject;
                SkillIcon icon = obj.GetComponent<SkillIcon>();
                icon.transform.SetParent(transform, false);
                iconLocation += new Vector3(size, 0, 0);
                icon.Skill = skill;
            }
        }
    }
}
