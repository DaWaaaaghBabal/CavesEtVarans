using System.Collections.Generic;
using CavesEtVarans.character;
using CavesEtVarans.character.factions;
using CavesEtVarans.data;
using CavesEtVarans.graphics;
using UnityEngine;
using CavesEtVarans.map;

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
		private IMapGenerator mapGenerator;
		void Start() {
			if (hexagonal) {
				hexOrSquare = new HexGridProvider();
			} else {
				hexOrSquare = new SquareGridProvider();
			}
			HexOrSquare.SetSingleton(hexOrSquare);
            hexOrSquare.WallPrefab = wallPrefab;
			mapGenerator = HexOrSquare.ProvideGridGenerator();
			Battlefield.Init();
			GraphicBattlefield.Init();
			placementStrategy = HexOrSquare.ProvidePlacement();
            ((GenerationStrategy)mapGenerator).HeightMap = heightMap;
			mapGenerator.GenerateMap(CreateTile);
			ClassManager classMgr = ClassManager.Instance;
            /**/
			Faction factionA = new Faction("Faction A");
            PlaceCharacter(2, 10, 0, "Paladin 1", factionA, "Paladin");
            PlaceCharacter(1, 11, 0, "Stalker", factionA, "Stalker");

            Faction factionB = new Faction("Faction B");
			factionB.TreatAs(factionA, FriendOrFoe.Foe);
			factionA.TreatAs(factionB, FriendOrFoe.Foe);
            PlaceCharacter(1, 10, 0, "Berserker", factionB, "Berserker");
            PlaceCharacter(6, 6, 6, "Evoker", factionB, "Evoker");
            PlaceCharacter(5, 13, 6, "Shadowmancer", factionB, "Shadowmancer");
            
            CharacterManager.Get().ActivateNext();
        }
		
		private void CreateTile(int row, int column, int layer, int cost) {
			Tile tile = new Tile(row, column, layer, cost);
			Battlefield.AddTile(tile);

			GameObject tileObject = Instantiate(tilePrefab, transform.position, transform.rotation);
			GraphicTile graphicTile = tileObject.GetComponent<GraphicTile>();
			float size = graphicTile.size;

			GraphicBattlefield.Associate(tile, graphicTile);

			Vector3 offset = placementStrategy.Place(row, column, layer) * size;
			tileObject.transform.position += offset;
		}

		private void PlaceCharacter(int row, int column, int layer, string name, Faction faction, string clazz) {
			Character character = new Character {
				Name = name,
				CharacterClass = clazz,
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
