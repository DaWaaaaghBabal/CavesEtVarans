using System;
using System.Collections.Generic;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using UnityEngine;

namespace CavesEtVarans.gui {
    //@TODO refactor to use panels and layout instead of attaching this directly to the canvas.
    public class SkillBar : MonoBehaviour {
        public GameObject skillIconPrefab;
        public int size;
        private HashSet<SkillIcon> icons;
        void Start() {
            icons = new HashSet<SkillIcon>();
        }
        public void DisplaySkills(Character character) {
            Clear();
            float offset = character.Skills.Count / 2.0f;
            Vector3 iconLocation = new Vector3(-offset * size, size / 2, 0);
            foreach (Skill skill in character.Skills) {
                InitIcon(iconLocation, skill);
                iconLocation += new Vector3(size, 0, 0);
            }
        }

        private void InitIcon(Vector3 iconLocation, Skill skill) {
            GameObject obj =  Instantiate(skillIconPrefab, iconLocation, Quaternion.identity) as GameObject;
            SkillIcon icon = obj.GetComponent<SkillIcon>();
            icon.transform.SetParent(transform, false);
            icon.Skill = skill;
            icons.Add(icon);
        }

        public void Clear() {
            HashSet<SkillIcon> copy = new HashSet<SkillIcon>();
            copy.UnionWith(icons);
            foreach(SkillIcon icon in copy) {
                GameObject obj = icon.gameObject;
                Destroy(obj);
                icons.Remove(icon);
            }
        }
    }
}
