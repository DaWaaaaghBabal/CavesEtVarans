using CavesEtVarans.battlefield;
using CavesEtVarans.gui;
using System.Collections.Generic;
using System;
using CavesEtVarans.character;

namespace CavesEtVarans.graphics
{
	public class GraphicBattlefield {
		private static GraphicBattlefield instance;
		public static GraphicBattlefield Instance {
			set { }
			get {
				if (instance == null) {
					instance = new GraphicBattlefield();
				}
				return instance;
			}
		}

        private Dictionary<Tile, GraphicTile> tileMapping;
        private Dictionary<Character, GraphicCharacter> characterMapping;

        private HighlightStrategy highlightStrategy;
		private PlacementStrategy placementStrategy;

		public static void Init() {
			Instance.tileMapping = new Dictionary<Tile, GraphicTile>();
            Instance.characterMapping = new Dictionary<Character, GraphicCharacter>();

			Instance.highlightStrategy = HexOrSquare.ProvideHighlight();
            Instance.placementStrategy = HexOrSquare.ProvidePlacement();
		}

		public static void HighlightArea(HashSet<Tile> area) {
			Instance.highlightStrategy.HighlightArea(area);
		}

		public static void ClearHighlightedArea() {
			Instance.highlightStrategy.ClearHighlightedArea();
        }

        public static void Associate(Tile tile, GraphicTile graphicTile)
        {
            graphicTile.Tile = tile;
            Instance.tileMapping.Add(tile, graphicTile);
        }

        public static GraphicTile GetSceneTile(Tile tile) {
			if (Instance.tileMapping.ContainsKey(tile))
				return Instance.tileMapping[tile];
			return null;
        }

        public static void Associate(Character character, GraphicCharacter graphicCharacter)
        {
            graphicCharacter.Character = character;
            Instance.characterMapping.Add(character, graphicCharacter);
        }

        public static GraphicCharacter GetSceneCharacter(Character character)
        {
            if (Instance.characterMapping.ContainsKey(character))
                return Instance.characterMapping[character];
            return null;
        }

        public static int AngleForDirection(Orientation newOrientation) {
			return Instance.placementStrategy.AngleForOrientation(newOrientation);
		}

        public static int[][] EdgeDirections()
        {
            return Instance.highlightStrategy.NeighbourDirections();
        }
	}
}
