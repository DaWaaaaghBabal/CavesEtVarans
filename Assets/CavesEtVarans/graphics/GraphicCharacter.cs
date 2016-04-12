using CavesEtVarans.character;
using CavesEtVarans.gui;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CavesEtVarans.graphics {
	/* A GraphicCharacter is the representation in the Unity world of a Character in the game logic. 
     * Basically, it's only a visual representation that will manage clicks, animation, position and stuff
     * but no game logic.
     */
	public class GraphicCharacter : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
		public Character Character { get; set; }
		public CharacterAnimator Animator;

		public void Init(Character character) {
			Character = character;
			Animator.Character = character;
		}

		public void OnPointerEnter(PointerEventData eventData) {
			GUIEventHandler.Get().HandleMouseEnterCharacter(this);
		}

		public void OnPointerExit(PointerEventData eventData) {
			GUIEventHandler.Get().HandleMouseExitCharacter(this);
		}

		public void OnPointerClick(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left)
                GUIEventHandler.Get().HandleClickOnCharacter(this);
		}
	}
}
