using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects.AI
{
    class MappingNode
    {
        public Tile previousTile;
        public Constants.Direction previousMove;

        public MappingNode(Tile previousTile, Constants.Direction previousMove)
        {
            this.previousTile = previousTile;
            this.previousMove = previousMove;
        }
    }
}
