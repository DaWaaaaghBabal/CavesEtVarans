using CavesEtVarans.battlefield;
using CavesEtVarans.utils;
using System;
using System.Collections.Generic;

namespace CavesEtVarans.map {

    public class HexMapGeneratorPen : Pen {
        private readonly Hasher hasher;
        private Region currentRegion;
        private Queue<Region> regions;
        private readonly int regionSize;
        private readonly Dictionary<Region, int> currentSizes;
        private List<GenerationOrder> generationOrders;
        public int MinRow { get; private set; }
        public int MinColumn { get; private set; }
        public DualKeyDictionary<int, int, Region> Grid { private set; get; }

        public HexMapGeneratorPen(int seed, int regionSize, Queue<Region> regions) : base() {
            this.regionSize = regionSize;
            this.regions = regions;
            currentSizes = new Dictionary<Region, int>();
            hasher = new Hasher(seed);
            NextRegion();
            MinRow = 0;
            MinColumn = 0;
        }

        public List<GenerationOrder> GenerateMap() {
            Grid = new DualKeyDictionary<int, int, Region>();
            generationOrders = new List<GenerationOrder>();
            while (regions.Count > 0 || currentRegion != null) {
                Step();
            }
            return generationOrders;
        }

        private void Step() {
            // First step : create a tile here.
            // During this part of the generation, we don't actually create tiles : we only take note of where tiles will go and which region they will be assigned to.
            // Substep : check that we have room. If not, we don't place a tile.
            Region r = GetRegion(new int[] { row, column });
            if (r == null) {
                // Substep : figure out which region it belongs to.
                // If the current region is full, we switch to the next, except if we are filling a hole 
                // (i.e. tiles behind and in front are taken) in which case we keep growing the region
                // (which can make bigger than normal).
                if (currentSizes[currentRegion] >= regionSize) {
                    Region behind = GetRegion(GetNeighbour(Turn(-3)));
                    Region front = GetRegion(GetNeighbour(Turn(0)));
                    if (front == null || behind == null) // There is room to go away from here after filling this space, it's safe to switch to the next region.
                        NextRegion();
                }
                // Last substep : create a tile in the appropriate region.
                if (currentRegion != null) // It's possible we have already finished.
                    PlaceRegion();
            }
            // Second step : orient ourselves, based on where we are.
            Orient();
            /* Third step : figure out where to go next. The pen can move within the current region, but not to another. 
             *      At any point in the movement, the pen will be either in the current region or in a free spot.
             * - Priority is given to filling holes : if the space behind on the left is free, go to it.
             */
            int[] backwardLeftSpace = GetNeighbour(Turn(-2));
            r = GetRegion(backwardLeftSpace);
            if (r == null) {
                MoveTo(backwardLeftSpace);
            } else {
                /* If no hole needs to be filled, we can advance. The advance is random : there is a 50% chance
                 * that the pen will stick to a spiral pattern, and 50% that it will diverge from it.
                 * If the spot we wish to diverge to is forbidden (i.e. taken by a tile belonging to another region), 
                 * we default to the spiral pattern.
                 */
                byte rand = hasher.Hash(row, column, 0);
                if (rand < 148) {
                    FollowSpiral();
                } else if (rand < 202) {
                    // Stray from the spiral path by going sideways and forward.
                    if (!TryMovingInDirection(1)) FollowSpiral();
                } else {
                    // Stray from the spiral path by going sideways and backwards.
                    if (!TryMovingInDirection(2)) FollowSpiral();
                }

            }
        }

        private void FollowSpiral() {
            // To follow the spiral pattern, we go as much to the left as possible.
            // We go to the space forward left if it's free, else we go to the leftmost space that is accessible.
            int[] forwardLeftSpace = GetNeighbour(Turn(-1));
            Region r = GetRegion(forwardLeftSpace);
            if (r == null) {
                MoveTo(forwardLeftSpace);
            } else {
                bool moved = false;
                for (int i = 0; i < 4 && !moved; i++)
                    moved = TryMovingInDirection(i);
                // The conditions required to get stuck should make it impossible. This is just in case.
                if (!moved) throw new Exception("WoOps, the pen got stuck !");
            }
        }

        /// <summary>
        /// If the space in the direction given is accesible (i.e. either empty or belonging to the same region),
        /// move to it and returns true. Else, doesn't move and returns false.
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        private bool TryMovingInDirection(int steps) {
            int[] forwardSpace = GetNeighbour(Turn(steps));
            Region r = GetRegion(forwardSpace);
            if (r == null || currentRegion == r) {
                MoveTo(forwardSpace);
                return true;
            }
            return false;
        }

        private void NextRegion() {
            if (regions.Count > 0) {
                currentRegion = regions.Dequeue();
                currentSizes.Add(currentRegion, 0);
            } else
                currentRegion = null;
        }

        private void PlaceRegion() {
            Grid.Add(row, column, currentRegion);
            if (row < MinRow)
                MinRow = row;
            if (column < MinColumn)
                MinColumn = column;
            currentSizes[currentRegion]++;
            generationOrders.Add(new GenerationOrder(currentRegion, row, column));
        }

        private Region GetRegion(int[] space) {
            if (Grid.ContainsKeys(space[0], space[1]))
                return Grid[space[0]][space[1]];
            return null;
        }
    }

    public class GenerationOrder {
        public readonly Region region;
        public readonly int row, column;

        public GenerationOrder(Region region, int row, int column) {
            this.region = region;
            this.row = row;
            this.column = column;
        }
    }
}
