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
        bool showItems = false;
        bool shouldDelete = false;
        bool storable = false;


        public Wall(bool collidable) : base(new Sprite(Color.SaddleBrown), new Location(0,0))
        {
            Collidable = collidable;
        }

        public bool Collidable { get => collidable; set => collidable = value; }
        public bool ShowItems { get => showItems; set => showItems = value; }
        public bool ShouldDelete { get => shouldDelete; set => shouldDelete = value; }
        public bool Storable { get => storable; set => storable = value; }

        public virtual void Update()
        {

        }

        public virtual List<string> GetStorageTypes()
        {
            return new List<string>();
        }

        public override void Render(Graphics graphics)
        {
            Sprite.Render(graphics, Location.X, Location.Y, 50, 50, 0);
            if (ShowItems && Items.Count > 0)
            {
                Items[0].Render(graphics, Location);
            }
        }
    }
}
