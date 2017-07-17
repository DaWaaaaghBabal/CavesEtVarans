using CavesEtVarans.graphics;
using CavesEtVarans.gui;
using UnityEngine;

namespace CavesEtVarans.battlefield {
    public abstract class HexOrSquare {
        private static HexOrSquare instance;

        private readonly Grid<Tile> tileGrid;
        //@TODO creates a dependency between model and graphics. FIX THAT.
        //Note : might not be fixable : hex/square variation affects both graphics and model.
        private readonly HighlightStrategy highlight;
        private readonly PlacementStrategy placement;
        private readonly OrientationStrategy orientation;
        private readonly LineOfSightStrategy lineOfSight;
        private readonly int[][] ringDirections;

        public GameObject EdgePrefab { set; get; }

        public HexOrSquare() {
            tileGrid = InitGrid<Tile>();
            highlight = InitHighlight();
            placement = InitPlacement();
            orientation = InitOrientation();
            placement.InitAngles(orientation);
            lineOfSight = InitLineOfSight();
            ringDirections = InitRingDirections();
        }

        public static LineOfSightStrategy ProvideLineOfSight() {
            return instance.lineOfSight;
        }

        public static void SetSingleton(HexOrSquare singleton) {
            instance = singleton;
        }

        public static Grid<Tile> ProvideGrid() {
            return instance.tileGrid;
        }
        public static HighlightStrategy ProvideHighlight() {
            return instance.highlight;
        }

        public static PlacementStrategy ProvidePlacement() {
            return instance.placement;
        }

        public static OrientationStrategy ProvideOrientation() {
            return instance.orientation;
        }


        /// <summary>
        /// Provides an array of vectors corresponding to the successive directions one must face when going clockwise along a circle,
        /// starting from 3 o'clock.
        /// </summary>
        /// <returns></returns>
        public static int[][] ProvideRingDirections() {
            return instance.ringDirections;
        }

        protected abstract Grid<T> InitGrid<T>() where T : ICoordinates;
        protected abstract HighlightStrategy InitHighlight();
        protected abstract PlacementStrategy InitPlacement();
        protected abstract OrientationStrategy InitOrientation();
        protected abstract LineOfSightStrategy InitLineOfSight();
        protected abstract int[][] InitRingDirections();

    }
}
