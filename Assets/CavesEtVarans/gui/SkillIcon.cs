using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using UnityEngine;
using UnityEngine.UI;

namespace CavesEtVarans.gui
{
	public class SkillIcon : MonoBehaviour {
        public Image image;
        public Text description;

        private Skill skill;
        public Skill Skill {
            set {
                skill = value;
                description.text = skill.Attributes.Description;
                image.sprite = Resources.Load<Sprite>(skill.Attributes.Icon);
            }
            get {
                return skill;
            }
        }
        public void Click() {
			Character source =  CharacterManager.Get().ActiveCharacter;
            source.InitSkill(Skill);
		}

	}
}