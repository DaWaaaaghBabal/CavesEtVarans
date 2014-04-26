using System;
using UnityEngine;

namespace CavesEtVarans {
    /* This is the representation, in the Unity world, of a battlefield Tile. It handles nothing more than position and click management.
     */
    public class SceneTile : MonoBehaviour {
        
        private Tile tile;
       
        void OnClick() {
            TargetHandler.HandleClickOnTile(this);

        }
    }
}
