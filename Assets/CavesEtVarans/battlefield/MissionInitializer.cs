using System.Collections.Generic;
using CavesEtVarans.character;
using CavesEtVarans.character.factions;
using CavesEtVarans.data;
using CavesEtVarans.graphics;
using UnityEngine;
using CavesEtVarans.map;
using RNG = UnityEngine.Random;
using System;

public delegate void CreateTile(int row, int column, int layer, LandType landType);

namespace CavesEtVarans.battlefield {
	public class MissionInitializer : MonoBehaviour {
		public GameObject plainPrefab, seaPrefab, forestPrefab;
		public GameObject edgePrefab;
		public GameObject characterPrefab;
		public bool hexagonal, island;
		public int radius, seed;
		public TextAsset MissionDescriptor;
		public TextAsset ClassDescriptor;
		//@TODO replace with mission config file
		public Texture2D heightMap;

		private HexOrSquare hexOrSquare;
		private	PlacementStrategy placementStrategy;
		private IMapGenerator mapGenerator;
		private Dictionary<LandType, GameObject> landPrefabs;
		private Dictionary<Region, ICoordinates> regions;
		void Start() {
			if (hexagonal) {
				hexOrSquare = new HexGridProvider();
			}			else {
				hexOrSquare = new SquareGridProvider();
			}

			HexOrSquare.SetSingleton(hexOrSquare);
			hexOrSquare.EdgePrefab = edgePrefab;
			landPrefabs = new Dictionary<LandType, GameObject>();
			landPrefabs[LandType.PLAIN] = plainPrefab;
			landPrefabs[LandType.SEA] = seaPrefab;
			landPrefabs[LandType.FOREST] = forestPrefab;
			//mapGenerator = HexOrSquare.ProvideGridGenerator();
			//((GenerationStrategy)mapGenerator).HeightMap = heightMap;
			mapGenerator = new PerlinMapGenerator() {
				Seed = seed,
                Depth = 3,
                PlainToForest = -0,
                SeaToLand = 0.3,
                Island = island,
                MaxAltitude = 10,
                NoiseSize = 10,
                Radius = radius,
			};
			Battlefield.Init();
			GraphicBattlefield.Init();
			placementStrategy = HexOrSquare.ProvidePlacement();
			regions = InitRegions();
			mapGenerator.GenerateMap(CreateTile);
			ClassManager classMgr = ClassManager.Instance;

			Faction factionA = new Faction("Faction A");
			PlaceCharacter(2, 10, "Paladin 1", factionA, "Paladin");
			PlaceCharacter(1, 11, "Stalker", factionA, "Stalker");

			Faction factionB = new Faction("Faction B");
			factionB.TreatAs(factionA, FriendOrFoe.Foe);
			factionA.TreatAs(factionB, FriendOrFoe.Foe);
			PlaceCharacter(1, 10, "Berserker", factionB, "Berserker");
			PlaceCharacter(6, 7, "Evoker", factionB, "Evoker");
			PlaceCharacter(5, 12, "Shadowmancer", factionB, "Shadowmancer");

			CharacterManager.Get().ActivateNext();
		}

		private Dictionary<Region, ICoordinates> InitRegions() {
			Dictionary<Region, ICoordinates> result = new Dictionary<Region, ICoordinates>();
			for (int i = 0; i < radius; i++) {
				int row = RNG.Range(-radius, radius);
				int minColumn = Mathf.Max(-radius, row - radius);
				int maxColumn = Mathf.Min(radius, row + radius);
				int column = RNG.Range(minColumn, maxColumn);
				ICoordinates coord = new Tile(radius + row, radius + column, 0, 0);
				Region region = new Region() {
					Color = RNG.ColorHSV(0, 1, 0.15f, 0.2f, 0.9f, 1)
				};
				result[region] = coord;
			}

			return result;
		}

		private void CreateTile(int row, int column, int layer, LandType landType) {
			Tile tile = new Tile(row, column, layer, landType.MovementCost);
			Battlefield.AddTile(tile);
			GameObject prefab = landPrefabs[landType];
			GameObject tileObject = Instantiate(prefab, transform.position, transform.rotation);
			GraphicTile graphicTile = tileObject.GetComponent<GraphicTile>();
			float size = graphicTile.size;

			GraphicBattlefield.Associate(tile, graphicTile);

			Vector3 offset = placementStrategy.Place(row, column, layer) * size;
			tileObject.transform.position += offset;

			if (landType != LandType.SEA) {
				int minDistance = 2 * radius; // 2* radius is the max distance between two tiles in a circular map
                Region closest = null;
				foreach(KeyValuePair<Region, ICoordinates> entry in regions) {
					int distance = Battlefield.GameDistance(tile, (Tile)entry.Value);
					if (distance < minDistance) {
						minDistance = distance;
						closest = entry.Key;
					}
				}
				AssignTileToRegion(tile, closest);
			}
		}

		private void AssignTileToRegion(Tile tile, Region region) {
			region.Tiles.Add(tile);
			GraphicBattlefield.GetSceneTile(tile).GetComponent<Renderer>().material.color = region.Color;
		}

		private void PlaceCharacter(int row, int column, string name, Faction faction, string clazz) {
			Character character = new Character {
				Name = name,
				CharacterClass = clazz,
				Faction = faction
			};
			CharacterManager.Get().Add(character);
			foreach (Tile t in Battlefield.GetStack(row, column)) {
				if (t != null) {
					character.Tile = t;
					t.Character = character;
				}
			}

			Orientation orientation = HexOrSquare.ProvideOrientation().AllDirections()[0];
			character.Orientation = orientation;

			GraphicTile gt = GraphicBattlefield.GetSceneTile(character.Tile);
			Vector3 position = gt.transform.position;
			int angle = HexOrSquare.ProvidePlacement().AngleForOrientation(orientation);
			GameObject charObject = Instantiate(characterPrefab, position, Quaternion.Euler(0, angle + 90, 0) * transform.rotation);

			GraphicCharacter charComponent = charObject.GetComponent<GraphicCharacter>();
			charComponent.Character = character;
			CharacterAnimator anim = charObject.GetComponent<CharacterAnimator>();
			anim.Character = character;
			GraphicBattlefield.Associate(character, charComponent);
		}
	}
}