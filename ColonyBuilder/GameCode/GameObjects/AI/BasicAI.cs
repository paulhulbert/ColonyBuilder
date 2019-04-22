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
            //foreach (Constants.Direction dir in FindPathFromTo(testGoal1, testGoal2))
            //{
            //    Console.Write(dir + ", ");
            //}
            //Console.WriteLine();

            if (character.Location.Equals(testGoal1) && goingToTestGoal1)
            {
                goingToTestGoal1 = false;
            }
            if (character.Location.Equals(testGoal2) && !goingToTestGoal1)
            {
                goingToTestGoal1 = true;
            }


            Location goal = goingToTestGoal1 ? testGoal1 : testGoal2;
            
            CurrentOrder = new Order(FindPathFromTo(character.Location, goal)[0], "Moving East");
            return;

            if (goal.X > character.Location.X)
            {
                CurrentOrder = new Order(Constants.Direction.East, "Moving East");
                return;
            }
            if (goal.X < character.Location.X)
            {
                CurrentOrder = new Order(Constants.Direction.West, "Moving West");
                return;
            }
            if (goal.Y < character.Location.Y)
            {
                CurrentOrder = new Order(Constants.Direction.North, "Moving North");
                return;
            }
            if (goal.Y > character.Location.Y)
            {
                CurrentOrder = new Order(Constants.Direction.South, "Moving South");
                return;
            }
        }

        private List<Constants.Direction> FindPathFromTo(Location from, Location to)
        {
            List<Constants.Direction> directions = new List<Constants.Direction>();
            MappingNode lastNode = null;
            Dictionary<Tile, MappingNode> paths = new Dictionary<Tile, MappingNode>();
            paths.Add(gameState.GetTile(from), new MappingNode(null, Constants.Direction.East));
            List<Tile> tilesToSearch = new List<Tile>();
            tilesToSearch.Add(gameState.GetTile(from));

            while (lastNode == null || GetPathDistance(paths, lastNode) > tilesToSearch.Select(tile =>
            {
                if (paths.ContainsKey(tile)) {
                    return GetPathDistance(paths, paths[tile]);
                } else
                {
                    return 99999999;
                }
            }).Min())
            {
                List<Tile> newTilesToSearch = new List<Tile>();
                foreach (Tile tile in tilesToSearch)
                {
                    if (tile.Location.Equals(to))
                    {
                        lastNode = paths[tile];
                        break;
                    }
                    foreach (Constants.Direction adjacentDirection in tile.AdjacentTiles.Keys)
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
                tilesToSearch = newTilesToSearch;
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
