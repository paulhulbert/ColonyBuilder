using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects.Walls
{
    class ResourcePile : Wall
    {
        public ResourcePile(List<Item> items) : base(false)
        {
            Items = items;
            ShowItems = true;
        }

        public override void Update()
        {
            if (Items.Count == 0)
            {
                ShouldDelete = true;
            }
        }
    }
}
