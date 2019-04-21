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
    }
}
