using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColonyBuilder.GameCode.Util;

namespace ColonyBuilder.GameCode.GameObjects
{
    class Tile : GameObject
    {
        private Wall wall;
        private Dictionary<Constants.Direction, Tile> adjacentTiles = new Dictionary<Constants.Direction, Tile>();
        public Tile(Location location) : base(new Sprite(Color.DarkGreen), location)
        {
        }
        

        public Wall Wall
        {
            get => wall; set
            {
                wall = value;
                wall.Location = Location;
            }
        }

        public Dictionary<Constants.Direction, Tile> AdjacentTiles { get => adjacentTiles; set => adjacentTiles = value; }


        public bool CanMoveAdjacentDirection(Constants.Direction adjacentDirection)
        {
            bool diagonalsSafe = true;
            if (Constants.IsDiagonal(adjacentDirection))
            {
                if (adjacentDirection == Constants.Direction.NorthEast)
                {
                    diagonalsSafe = CanMoveAdjacentDirection(Constants.Direction.North) && CanMoveAdjacentDirection(Constants.Direction.East);
                }
                if (adjacentDirection == Constants.Direction.NorthWest)
                {
                    diagonalsSafe = CanMoveAdjacentDirection(Constants.Direction.North) && CanMoveAdjacentDirection(Constants.Direction.West);
                }
                if (adjacentDirection == Constants.Direction.SouthEast)
                {
                    diagonalsSafe = CanMoveAdjacentDirection(Constants.Direction.South) && CanMoveAdjacentDirection(Constants.Direction.East);
                }
                if (adjacentDirection == Constants.Direction.SouthWest)
                {
                    diagonalsSafe = CanMoveAdjacentDirection(Constants.Direction.South) && CanMoveAdjacentDirection(Constants.Direction.West);
                }
            }
            return (AdjacentTiles[adjacentDirection].Wall == null || !AdjacentTiles[adjacentDirection].Wall.Collidable) && diagonalsSafe;
        }

        public bool CanInteractWithAdjacentDirection(Constants.Direction adjacentDirection)
        {
            if (Constants.IsDiagonal(adjacentDirection))
            {
                if (adjacentDirection == Constants.Direction.NorthEast)
                {
                    return CanMoveAdjacentDirection(Constants.Direction.North) || CanMoveAdjacentDirection(Constants.Direction.East);
                }
                if (adjacentDirection == Constants.Direction.NorthWest)
                {
                    return CanMoveAdjacentDirection(Constants.Direction.North) || CanMoveAdjacentDirection(Constants.Direction.West);
                }
                if (adjacentDirection == Constants.Direction.SouthEast)
                {
                    return CanMoveAdjacentDirection(Constants.Direction.South) || CanMoveAdjacentDirection(Constants.Direction.East);
                }
                if (adjacentDirection == Constants.Direction.SouthWest)
                {
                    return CanMoveAdjacentDirection(Constants.Direction.South) || CanMoveAdjacentDirection(Constants.Direction.West);
                }
            }
            return true;
        }

        public void Update()
        {
            if (wall != null)
            {
                wall.Update();
                if (wall.ShouldDelete)
                {
                    wall = null;
                }
            }
        }

        public override void Render(Graphics graphics)
        {
            Sprite.Render(graphics, Location.X, Location.Y, 50, 50, 0);
            if (Wall != null)
            {
                Wall.Render(graphics);
            }
        }
    }
}
