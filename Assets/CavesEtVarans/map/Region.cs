using CavesEtVarans.battlefield;
using System.Collections.Generic;

namespace CavesEtVarans.map {
    public class Region {
        public HashSet<Tile> Tiles { private set; get; }
        public UnityEngine.Color Color { set; get; }
        public string Name { set; get; }
        public Region() {
            Tiles = new HashSet<Tile>();
        }
        public bool Contains(Tile t) {
            return Tiles.Contains(t);
        }

        public int Count {
            get { return Tiles.Count; }
            private set { }
        }

        public override string ToString() {
            return Name;
        }
    }
}