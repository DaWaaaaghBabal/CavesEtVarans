using System;
using System.Collections.Generic;
using UnityEngine;

namespace CavesEtVarans {
    // This class is responsible with instantiating the playing field (terrain and NPC's).
    class BattleFieldInitializer : MonoBehaviour{
        public GameObject tilePrefab;
        public GameObject characterPrefab;
        public int width, height;
        void Start() {
            InitializeMap();
            InitializeCharacters();
        }

        // Places characters on the map. Should, read them from the mission descriptor file.
        private void InitializeCharacters() {
            Character charA = new Character("Character A");
            CharacterManager.Get().Add(charA);
            Character charB = new Character("Character B");
            CharacterManager.Get().Add(charB);
            Character charC = new Character("Character C");
            CharacterManager.Get().Add(charC);
            PlaceCharacter(charA, 2, 8);
            PlaceCharacter(charB, 1, 12);
            PlaceCharacter(charC, 9, 3);
            CharacterManager.Get().ActivateNext();
        }

        // Places tiles on the map. Should read them from the mission descriptor file.
        public void InitializeMap() {
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    PlaceTile(i, j, Dice.Roll(2,2,-2));
                }
            }
        }

        private void PlaceTile(int i, int j, int h) {
            Tile tile = new Tile();
            TileManager.Get().AddTile(tile, i, j, h);
            Vector3 tilePosition = transform.position + new Vector3(2 * i, h/3f, 2 * j);
            GameObject tileObject = (GameObject) Instantiate(tilePrefab, tilePosition, transform.rotation);
            SceneTile tileComponent = tileObject.GetComponent<SceneTile>();
            tileComponent.SetTile(tile);
        }


        private void PlaceCharacter(Character character, int i, int j) {
            Vector3 charPosition = transform.position + new Vector3(2 * i, 0, 2 * j);
            GameObject charObject = (GameObject)Instantiate(characterPrefab, charPosition, Quaternion.Euler(0, 180, 0) * transform.rotation);
            SceneCharacter charComponent = charObject.GetComponent<SceneCharacter>();
            charComponent.SetCharacter(character);
        }
    }
}
