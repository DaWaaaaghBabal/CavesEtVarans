using System;
using System.Collections.Generic;
using UnityEngine;

namespace CavesEtVarans {
    class BattleFieldInitializer : MonoBehaviour{
        public GameObject tilePrefab;
        public GameObject characterPrefab;

        void Start() {
            InitializeMap();
            InitializeCharacters();
        }

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

        public void InitializeMap() {
            for (int i = 0; i < 15; i++) {
                for (int j = 0; j < 15; j++) {
                    Tile tile = new Tile(i, j);
                    PlaceTile(tile, i, j);
                }
            }
        }

        private void PlaceTile(Tile tile, int i, int j) {
            Vector3 tilePosition = transform.position + new Vector3(2 * i, -0.55f, 2 * j);
            GameObject tileObject = (GameObject) Instantiate(tilePrefab, tilePosition, transform.rotation);
            SceneTile tileComponent = tileObject.GetComponent<SceneTile>();
            tileComponent.SetTile(tile);
        }


        private void PlaceCharacter(Character character, int i, int j) {
            Vector3 charPosition = transform.position + new Vector3(2 * i, 1, 2 * j);
            GameObject charObject = (GameObject)Instantiate(characterPrefab, charPosition, transform.rotation);
            SceneCharacter charComponent = charObject.GetComponent<SceneCharacter>();
            charComponent.SetCharacter(character);
        }
    }
}
