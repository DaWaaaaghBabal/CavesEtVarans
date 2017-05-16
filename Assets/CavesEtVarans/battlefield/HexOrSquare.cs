﻿using System;
using CavesEtVarans.graphics;
using CavesEtVarans.gui;
using UnityEngine;
using CavesEtVarans.map;
using System.Collections.Generic;

namespace CavesEtVarans.battlefield {
	public abstract class HexOrSquare {
		private static HexOrSquare instance;
		
		private Grid<Tile> tileGrid;
        //@TODO creates a dependency between model and graphics. FIX THAT.
        //Note : might not be fixable : hex/square variation affects both graphics and model.
		private HighlightStrategy highlight;
		private PlacementStrategy placement;
		private GenerationStrategy gridGenerator;
		private OrientationStrategy orientation;
        private LineOfSightStrategy lineOfSight;
        private IEnumerable<int[]> ringDirections;

        public GameObject EdgePrefab { set; get; }

        public HexOrSquare() {
			tileGrid = InitGrid<Tile>();
			highlight = InitHighlight();
			placement = InitPlacement();
			gridGenerator = InitGridGenerator();
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

		public static IMapGenerator ProvideGridGenerator() {
			return instance.gridGenerator;
		}

        public static IEnumerable<int[]> ProvideRingDirections() {
            return instance.ringDirections;
        }

        protected abstract Grid<T> InitGrid<T>() where T: ICoordinates;
		protected abstract GenerationStrategy InitGridGenerator();
		protected abstract HighlightStrategy InitHighlight();
		protected abstract PlacementStrategy InitPlacement();
		protected abstract OrientationStrategy InitOrientation();
        protected abstract LineOfSightStrategy InitLineOfSight();
        protected abstract IEnumerable<int[]> InitRingDirections();

    }
}
