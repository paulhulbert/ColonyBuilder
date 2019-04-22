using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColonyBuilder.GameCode.Util;

namespace ColonyBuilder.GameCode.GameObjects
{
    class Wall : GameObject
    {
        bool collidable;
        List<Item> items = new List<Item>();
        bool showItems = false;


        public Wall(bool collidable) : base(new Sprite(Color.SaddleBrown), new Location(0,0))
        {
            Collidable = collidable;
        }

        public bool Collidable { get => collidable; set => collidable = value; }
        public List<Item> Items { get => items; set => items = value; }
        public bool ShowItems { get => showItems; set => showItems = value; }

        public override void Render(Graphics graphics)
        {
            Sprite.Render(graphics, Location.X, Location.Y, 50, 50, 0);
            if (ShowItems && items.Count > 0)
            {
                items[0].Render(graphics, Location);
            }
        }
    }
}
