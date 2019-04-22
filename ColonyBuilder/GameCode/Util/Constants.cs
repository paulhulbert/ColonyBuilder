using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode
{
    class Constants
    {
        public static int FRAME_RATE = 30;

        public static Random random = new Random();

        public enum Direction
        {
            North,
            South,
            East, 
            West,
            NorthEast,
            NorthWest,
            SouthEast,
            SouthWest
        }

        public static bool IsDiagonal(Direction direction)
        {
            return direction == Direction.NorthEast || direction == Direction.NorthWest || direction == Direction.SouthEast || direction == Direction.SouthWest;
        }

        public static Direction oppositeDirection(Direction direction)
        {
            if (direction == Direction.North)
            {
                return Direction.South;
            }
            if (direction == Direction.NorthEast)
            {
                return Direction.SouthWest;
            }
            if (direction == Direction.NorthWest)
            {
                return Direction.SouthEast;
            }
            if (direction == Direction.South)
            {
                return Direction.North;
            }
            if (direction == Direction.SouthEast)
            {
                return Direction.NorthWest;
            }
            if (direction == Direction.SouthWest)
            {
                return Direction.NorthEast;
            }
            if (direction == Direction.East)
            {
                return Direction.West;
            }
            if (direction == Direction.West)
            {
                return Direction.East;
            }
            return Direction.East;
        }
    }
}
