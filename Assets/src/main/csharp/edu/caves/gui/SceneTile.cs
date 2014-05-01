using System;
using UnityEngine;

namespace CavesEtVarans {
    /* This is the representation, in the Unity world, of a battlefield Tile. It handles nothing more than position and click management.
     */
    public class SceneTile : MonoBehaviour {
        
        private Tile tile;
        public void SetTile(Tile newTile) {
            tile = newTile;
        }
        public Tile GetTile() {
            return tile;
        }
       
        void OnMouseDown() {
            GUIEventHandler.HandleClickOnTile(this);
        }
    }
}
