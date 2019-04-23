using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects.Walls
{
    class Storage : Wall
    {
        public Storage() : base(false)
        {
            Storable = true;
            Sprite = new Sprite(Color.Gray);
            ShowItems = true;
        }

        public override List<string> GetStorageTypes()
        {
            HashSet<String> itemNames = new HashSet<string>();

            if (Items == null || Items.Count == 0)
            {
                return null;
            }

            foreach (Item item in Items)
            {
                itemNames.Add(item.Name);
            }

            return itemNames.ToList();
        }

        public override void Render(Graphics graphics)
        {
            base.Render(graphics);
        }
    }
}
