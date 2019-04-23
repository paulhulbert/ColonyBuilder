using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects.AI
{
    class AIPredicates
    {
        public static Predicate<Tile> CheckClosestStorageForItems(List<string> items)
        {
            return CheckClosestTileAdjacentToRequirements(delegate (Tile adjacentTile)
            {
                if (adjacentTile.Wall != null && adjacentTile.Wall.Storable)
                {
                    if (adjacentTile.Wall.GetStorageTypes() == null || items.Any(item => adjacentTile.Wall.GetStorageTypes().Contains(item)))
                    {
                        return true;
                    }
                }
                return false;
            });
        }

        public static Predicate<Tile> CheckClosestNonStorageTileWithItems(List<string> items)
        {
            return CheckClosestNonStorageTileWithItemsSizeLimit(items, 9999999);
        }

        public static Predicate<Tile> CheckClosestNonStorageTileWithItemsSizeLimit(List<string> items, int sizeLimit)
        {
            return CheckClosestTileAdjacentToRequirements(delegate (Tile adjacentTile)
            {
                if (adjacentTile.Wall != null && !adjacentTile.Wall.Storable && adjacentTile.Wall.Items.Count > 0)
                {
                    foreach (Item item in adjacentTile.Wall.Items)
                    {
                        if (items.Contains(item.Name) && item.Weight <= sizeLimit)
                        {
                            return true;
                        }
                    }
                }
                return false;
            });
        }

        public static Predicate<Tile> CheckClosestTileAdjacentToRequirements(Predicate<Tile> adjacentTileRequirements)
        {
            return delegate (Tile tile)
            {
                foreach (Constants.Direction adjacentDirection in tile.AdjacentTiles.Keys)
                {
                    if (tile.CanInteractWithAdjacentDirection(adjacentDirection))
                    {
                        Tile adjacentTile = tile.AdjacentTiles[adjacentDirection];
                        if (adjacentTileRequirements(adjacentTile))
                        {
                            return true;
                        }
                    }
                }
                return false;
            };
        }
    }
}
