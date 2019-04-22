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

        Location testGoal1 = new Location(200, 200);
        Location testGoal2 = new Location(800, 400);
        bool goingToTestGoal1 = false;

        public BasicAI(Character character, GameState gameState)
        {
            this.character = character;
            this.gameState = gameState;
            CurrentOrder = new Order(Constants.Direction.East, "Moving East");
        }

        public Order CurrentOrder { get => currentOrder; set => currentOrder = value; }

        public void Evaluate()
        {

            if (character.Location.Equals(testGoal1) && goingToTestGoal1)
            {
                //goingToTestGoal1 = false;
            }
            if (character.Location.Equals(testGoal2) && !goingToTestGoal1)
            {
                //goingToTestGoal1 = true;
            }


            Location goal = goingToTestGoal1 ? testGoal1 : testGoal2;

            //List<Constants.Direction> path = FindPathFromTo(character.Location, delegate (Tile tile) { return tile.Location.Equals(goal); });

            List<Constants.Direction> path = FindPathFromTo(character.Location, delegate (Tile tile) {
                foreach (Tile adjacentTile in tile.AdjacentTiles.Values)
                {
                    if (adjacentTile.Wall != null && adjacentTile.Wall.Items.Count > 0)
                    {
                        foreach (Item item in adjacentTile.Wall.Items)
                        {
                            String targetItem = goingToTestGoal1 ? "Food" : "Gold";
                            if (item.Name.Equals(targetItem))
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            });

            if (path != null && path.Count > 0)
            {
                CurrentOrder = new Order(path[0], "Moving East");
            } else
            {
                CurrentOrder = new Order("Wait");
                goingToTestGoal1 = !goingToTestGoal1;
            }
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
                        if (CanMoveAdjacentDirection(tile, adjacentDirection))
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

        private bool CanMoveAdjacentDirection(Tile tile, Constants.Direction adjacentDirection)
        {
            bool diagonalsSafe = true;
            if (Constants.IsDiagonal(adjacentDirection))
            {
                if (adjacentDirection == Constants.Direction.NorthEast)
                {
                    diagonalsSafe = CanMoveAdjacentDirection(tile, Constants.Direction.North) && CanMoveAdjacentDirection(tile, Constants.Direction.East);
                }
                if (adjacentDirection == Constants.Direction.NorthWest)
                {
                    diagonalsSafe = CanMoveAdjacentDirection(tile, Constants.Direction.North) && CanMoveAdjacentDirection(tile, Constants.Direction.West);
                }
                if (adjacentDirection == Constants.Direction.SouthEast)
                {
                    diagonalsSafe = CanMoveAdjacentDirection(tile, Constants.Direction.South) && CanMoveAdjacentDirection(tile, Constants.Direction.East);
                }
                if (adjacentDirection == Constants.Direction.SouthWest)
                {
                    diagonalsSafe = CanMoveAdjacentDirection(tile, Constants.Direction.South) && CanMoveAdjacentDirection(tile, Constants.Direction.West);
                }
            }
            return (tile.AdjacentTiles[adjacentDirection].Wall == null || !tile.AdjacentTiles[adjacentDirection].Wall.Collidable) && diagonalsSafe;
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
