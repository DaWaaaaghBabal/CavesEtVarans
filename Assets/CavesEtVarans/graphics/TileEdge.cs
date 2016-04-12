using System;
using UnityEngine;

namespace CavesEtVarans.graphics
{
    public class TileEdge : MonoBehaviour
    {
        public Material restMaterial;
        public Material highlightMaterial;
        void Start() {
            Clear();
        }
        public void Highlight()  {
            GetComponent<Renderer>().material = highlightMaterial;
        }

        public void Clear() {
            GetComponent<Renderer>().material = restMaterial;
        }
    }
}