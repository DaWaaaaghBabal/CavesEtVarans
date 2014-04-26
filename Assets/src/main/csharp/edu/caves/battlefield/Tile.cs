using System;
using UnityEngine;

namespace CavesEtVarans {
    public class Tile : MonoBehaviour {


        void OnClick() {
            TargetHandler.HandleClickOnTile(this);

        }
    }
}
