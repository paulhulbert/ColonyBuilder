using ColonyBuilder.GameCode.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects.AI
{
    class BasicAI
    {
        private Character character;
        private GameState gameState;
        private Order currentOrder;
        private bool storing = false;

        Location testGoal1 = new Location(200, 200);
        Location testGoal2 = new Location(800, 400);

        public BasicAI(Character character, GameState gameState)
        {
            this.character = character;
            this.gameState = gameState;
            CurrentOrder = new Order(Constants.Direction.East, "Moving East");
        }

        public Order CurrentOrder { get => currentOrder; set => currentOrder = value; }

        public void Evaluate()
        {
            if (character.GetFreeInventorySpace() == character.MaxInventory)
            {
                storing = false;
            }
            Predicate<Tile> isTargetTile = null;

            List<Constants.Direction> path = null;
            if (!storing)
            {
                isTargetTile = AIPredicates.CheckClosestNonStorageTileWithItemsSizeLimit(new List<string>() { "Food", "Gold" }, character.GetFreeInventorySpace());

                path = FindPathFromTo(character.Location, isTargetTile);
            }
            

            if (path == null)
            {
                isTargetTile = AIPredicates.CheckClosestStorageForItems(character.Items.Select(item => item.Name).ToList());

                path = FindPathFromTo(character.Location, isTargetTile);
                storing = true;
            }

            if (path != null && path.Count > 0)
            {
                CurrentOrder = new Order(path[0], "Moving");
            } else
            {
                if (path != null)
                {
                    if (storing)
                    {
                        Tile targetTile = FindAdjacentTileThatMeetsRequirements(delegate (Tile adjacentTile)
                        {
                        if (adjacentTile.Wall != null && ( adjacentTile.Wall.GetStorageTypes() == null || adjacentTile.Wall.GetStorageTypes().Contains("Food") || adjacentTile.Wall.GetStorageTypes().Contains("Gold") ))
                            {
                                return true;
                            }
                            return false;
                        });

                        if (targetTile != null)
                        {
                            string itemName = character.Items[0].Name;
                            if (targetTile.Wall.GetStorageTypes() != null)
                            {
                                itemName = targetTile.Wall.GetStorageTypes()[0];
                            }
                            CurrentOrder = new Order(new Interaction(character, targetTile.Wall, gameState, "Giving item", InteractionPredicates.GiveItem(itemName), 1000), "Giving item");
                            return;
                        }
                    }
                    else
                    {
                        Tile targetTile = FindAdjacentTileThatMeetsRequirements(delegate (Tile adjacentTile)
                        {
                            if (adjacentTile.Wall != null && !adjacentTile.Wall.Storable && adjacentTile.Wall.Items.Count > 0)
                            {
                                foreach (Item item in adjacentTile.Wall.Items)
                                {
                                    if (item.Name.Equals("Food") || item.Name.Equals("Gold"))
                                    {
                                        return true;
                                    }
                                }
                            }
                            return false;
                        });

                        if (targetTile != null)
                        {
                            CurrentOrder = new Order(new Interaction(character, targetTile.Wall, gameState, "Taking item", InteractionPredicates.TakeFirstItem(), 1000), "Taking item");
                            return;
                        }
                    }
                }
                CurrentOrder = new Order("Wait");
            }
        }

        private Tile FindAdjacentTileThatMeetsRequirements(Predicate<Tile> requirements)
        {
            Tile tile = gameState.GetTile(character.Location);
            foreach (Constants.Direction adjacentDirection in tile.AdjacentTiles.Keys)
            {
                if (tile.CanInteractWithAdjacentDirection(adjacentDirection))
                {
                    Tile adjacentTile = tile.AdjacentTiles[adjacentDirection];
                    if (requirements(adjacentTile))
                    {
                        return adjacentTile;
                    }
                }
            }
            return null;
        }

        private List<Constants.Direction> FindPathFromTo(Location from, Predicate<Tile> toPredicate)
        {
            List<Constants.Direction> directions = new List<Constants.Direction>();
            MappingNode lastNode = null;
            Dictionary<Tile, MappingNode> paths = new Dictionary<Tile, MappingNode>();
            paths.Add(gameState.GetTile(from), new MappingNode(null, Constants.Direction.East));
            List<Tile> tilesToSearch = new List<Tile>();
            tilesToSearch.Add(gameState.GetTile(from));

            while (tilesToSearch.Count > 0 && (lastNode == null || GetPathDistance(paths, lastNode) > tilesToSearch.Select(tile =>
            {
                if (paths.ContainsKey(tile)) {
                    return GetPathDistance(paths, paths[tile]);
                } else
                {
                    return 99999999;
                }
            }).Min()))
            {
                List<Tile> newTilesToSearch = new List<Tile>();
                foreach (Tile tile in tilesToSearch)
                {
                    if (toPredicate(tile))
                    {
                        if (lastNode == null || GetPathDistance(paths, lastNode) > GetPathDistance(paths, paths[tile]))
                        lastNode = paths[tile];
                        continue;
                    }
                    foreach (Constants.Direction adjacentDirection in tile.AdjacentTiles.Keys)
                    {
                        if (tile.CanMoveAdjacentDirection(adjacentDirection))
                        {
                            if (paths.ContainsKey(tile.AdjacentTiles[adjacentDirection]))
                            {
                                if (GetPathDistance(paths, paths[tile]) + (Constants.IsDiagonal(adjacentDirection) ? 1.4 : 1.0) < GetPathDistance(paths, paths[tile.AdjacentTiles[adjacentDirection]]))
                                {
                                    paths[tile.AdjacentTiles[adjacentDirection]].previousTile = tile;
                                    paths[tile.AdjacentTiles[adjacentDirection]].previousMove = adjacentDirection;
                                }
                            }
                            else
                            {
                                paths.Add(tile.AdjacentTiles[adjacentDirection], new MappingNode(tile, adjacentDirection));
                                newTilesToSearch.Add(tile.AdjacentTiles[adjacentDirection]);
                            }
                        }
                    }
                }
                tilesToSearch = newTilesToSearch;
            }

            if (lastNode == null)
            {
                return null;
            }
            
            while (lastNode.previousTile != null)
            {
                directions.Add(lastNode.previousMove);
                lastNode = paths[lastNode.previousTile];
            }
            directions.Reverse();
            return directions;
        }

        private double GetPathDistance(Dictionary<Tile, MappingNode> paths, MappingNode node)
        {
            double total = 0;

            while (node.previousTile != null)
            {
                total += Constants.IsDiagonal(node.previousMove) ? 1.4 : 1.0;
                node = paths[node.previousTile];
            }

            return total;
        }
    }
}
