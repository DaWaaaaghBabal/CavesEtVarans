using System;
using UnityEngine;

namespace CavesEtVarans {
    class Tile : MonoBehaviour {


        void OnClick() {
            TargetHandler.HandleClickOnTile(this);

        }
    }
}
