using System;
using CavesEtVarans.battlefield;
using CavesEtVarans.graphics;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.gui
{
	public class GUIEventHandler
	{

		private static GUIEventHandler instance;
		public static GUIEventHandler Get() {
			if (instance == null) {
				instance = new GUIEventHandler();
				instance.ActivePicker = new BasicUITargetPicker();
			}
			return instance;
		}

		private GUIEventHandler(){}

		public ITargetPicker ActivePicker { set; get; }

		public void HandleClickOnTile (GraphicTile graphicTile)
		{
            Tile tile = graphicTile.Tile;
            /*foreach (Tile t in Battlefield.Line(tile, CharacterManager.Get().GetActiveCharacter().Tile)) {
                if (t != null)
                {
                    GraphicTile gt = GraphicBattlefield.GetSceneTile(t);
                    gt.GetComponent<Renderer>().material.color = Color.red;
                }
            }*/
            ActivePicker.TargetTile (tile);
		}

        public void HandleRightClick() {
            ActivePicker.Cancel();
        }

        public void HandleClickOnCharacter (GraphicCharacter sceneCharacter)
		{
			ActivePicker.TargetCharacter (sceneCharacter.Character);
		}

		public void HandleMouseEnterCharacter (GraphicCharacter sceneCharacter)
		{
			MainGUI.DisplayCharacter(sceneCharacter.Character);
		}

		public void HandleMouseExitCharacter (GraphicCharacter sceneCharacter)
		{
			MainGUI.DisplayCharacter(null);
		}

        public void Reset() {
            GraphicBattlefield.ClearHighlightedArea();
            new BasicUITargetPicker().Activate(Context.Init(null, null));
        }
    }
}
