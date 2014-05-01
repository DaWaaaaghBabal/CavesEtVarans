using System;
using System.Collections.Generic;
using UnityEngine;

namespace CavesEtVarans {
    public class TileManager {
        

        private static TileManager instance;
        public static TileManager Get() {
            if (instance == null) {
                instance = new TileManager();
            }
            return instance;
        }

        private CubeArray<Tile> tiles;

        public TileManager() {
            tiles = new CubeArray<Tile>(15, 15, 15);
        }

        // Returns the distance between two tiles from the point of view of the game,
        // i.e ignoring Pythagoras' theorem : game distance = dX + dY + dH/3.
        // This distance is used everywhere a range is required.
        public int GameDistance(Tile from, Tile to) {
            int[] fromPos = tiles.GetPositionOf(from);
            int[] toPos = tiles.GetPositionOf(to);
            int dX = Math.Abs(toPos[0] - fromPos[0]);
            int dY = Math.Abs(toPos[1] - fromPos[1]);
            int dH = Math.Abs(toPos[2] - fromPos[2]) / 3;
            return dX + dY + dH;
        }

        public void AddTile(Tile t, int x, int y, int h) {
            tiles.Add(t, x, y, h);
        }

        public int[] GetPosition(Tile t) {
            return tiles.GetPositionOf(t);
        }

    }
}
