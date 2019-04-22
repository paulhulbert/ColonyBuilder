using ColonyBuilder.GameCode.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects
{
    class Item
    {
        private Sprite sprite;
        private String name;

        public Item(Sprite sprite, string name)
        {
            this.sprite = sprite;
            this.Name = name;
        }

        public string Name { get => name; set => name = value; }

        public void Render(Graphics graphics, Location location)
        {
            sprite.Render(graphics, location.X, location.Y, 50, 50, 0);
        }
    }
}
