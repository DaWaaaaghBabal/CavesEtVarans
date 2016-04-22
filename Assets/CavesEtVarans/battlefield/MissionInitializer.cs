using System.Collections.Generic;
using CavesEtVarans.character;
using CavesEtVarans.character.factions;
using CavesEtVarans.graphics;
using UnityEngine;

public delegate void CreateTile(int row, int column, int layer, int cost);

namespace CavesEtVarans.battlefield {
	public class MissionInitializer : MonoBehaviour{
		public GameObject tilePrefab;
		public GameObject wallPrefab;
		public GameObject characterPrefab;
		public bool hexagonal;
		public TextAsset MissionDescriptor;
		public TextAsset ClassDescriptor;
		//@TODO replace with mission config file
		public Texture2D heightMap;

		private HexOrSquare hexOrSquare;
		private	PlacementStrategy placementStrategy;
		private GenerationStrategy generationStrategy;
		void Start() {
			if (hexagonal) {
				hexOrSquare = new HexGridProvider(wallPrefab);
			} else {
				hexOrSquare = new SquareGridProvider(wallPrefab);
			}
			HexOrSquare.SetSingleton(hexOrSquare);

			generationStrategy = HexOrSquare.ProvideGridGenerator();
			Battlefield.Init();
			GraphicBattlefield.Init();
			placementStrategy = HexOrSquare.ProvidePlacement();
			generationStrategy.GenerateMap(heightMap, CreateTile);
			ClassManager classMgr = ClassManager.Instance;
            classMgr.ParseTextResource("classes");
			Faction factionA = new Faction("Faction A");
            PlaceCharacter(4, 11, 0, "Character C", factionA);
			Faction factionB = new Faction("Faction B");
			factionB.TreatAs(factionA, FriendOrFoe.Foe);
			factionA.TreatAs(factionB, FriendOrFoe.Foe);
			PlaceCharacter(1,  2, 0, "Character B", factionB);
            CharacterManager.Get().ActivateNext();
        }
		
		private void CreateTile(int row, int column, int layer, int cost) {
			Tile tile = new Tile(row, column, layer, cost);
			Battlefield.AddTile(tile);

			GameObject tileObject = (GameObject) Instantiate(tilePrefab, transform.position, transform.rotation);
			GraphicTile graphicTile = tileObject.GetComponent<GraphicTile>();
			float size = graphicTile.size;

			GraphicBattlefield.Associate(tile, graphicTile);

			Vector3 offset = placementStrategy.Place(row, column, layer) * size;
			tileObject.transform.position += offset;
		}

		private void PlaceCharacter(int row, int column, int layer, string name, Faction faction) {
			Character character = new Character {
				Name = name,
				CharacterClass = "Paladin",
				Faction = faction
			};
			CharacterManager.Get().Add(character);
			Tile t = Battlefield.Get(row, column, layer);
			character.Tile = t;
			t.Character = character;
            Orientation orientation = HexOrSquare.ProvideOrientation().AllDirections()[0];
            character.Orientation = orientation;

            GraphicTile gt = GraphicBattlefield.GetSceneTile(t);
			Vector3 position = gt.transform.position;
			int angle = HexOrSquare.ProvidePlacement().AngleForOrientation(orientation);
            GameObject charObject = (GameObject) Instantiate(characterPrefab, position, Quaternion.Euler(0, angle + 90, 0) * transform.rotation);
			
			GraphicCharacter charComponent = charObject.GetComponent<GraphicCharacter>();
			charComponent.Character = character;
			CharacterAnimator anim = charObject.GetComponent<CharacterAnimator>();
			anim.Character = character;
            GraphicBattlefield.Associate(character, charComponent);
		}
	}
}
