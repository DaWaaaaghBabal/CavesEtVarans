using CavesEtVarans.battlefield;
using System.Collections.Generic;

namespace CavesEtVarans.map {
    public class Region {
        public HashSet<Tile> Tiles { private set; get; }
        public UnityEngine.Color Color { set; get; }
        public Region() {
            Tiles = new HashSet<Tile>();
        }
    }
}