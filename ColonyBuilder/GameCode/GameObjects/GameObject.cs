using ColonyBuilder.GameCode.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects
{
    abstract class GameObject : Renderable
    {
        private Sprite sprite;
        private Location location;
        private List<Item> items = new List<Item>();


        internal Sprite Sprite { get => sprite; set => sprite = value; }
        internal Location Location { get => location; set => location = value; }
        public List<Item> Items { get => items; set => items = value; }

        protected GameObject(Sprite sprite, Location location)
        {
            Sprite = sprite;
            Location = location;
        }

        public abstract void Render(Graphics graphics);
    }
}
