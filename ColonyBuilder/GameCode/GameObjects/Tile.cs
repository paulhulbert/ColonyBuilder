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
        public Tile(Location location) : base(new Sprite(Color.DarkGreen), location)
        {
        }

        public override void Render(Graphics graphics)
        {
            Sprite.Render(graphics, Location.X, Location.Y, 50, 50, 0);
        }
    }
}
