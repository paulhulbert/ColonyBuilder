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
