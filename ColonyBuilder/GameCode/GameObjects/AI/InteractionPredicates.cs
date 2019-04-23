using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects.AI
{
    class InteractionPredicates
    {
        public static Action<GameState, GameObject, GameObject> TakeFirstItem()
        {
            return delegate (GameState state, GameObject source, GameObject target)
            {
                Console.WriteLine("Taking item: " + target.Items[0].Name);
                source.Items.Add(target.Items[0]);
                target.Items.RemoveAt(0);
            };
        }

        public static Action<GameState, GameObject, GameObject> GiveItem(String itemName)
        {
            return delegate (GameState state, GameObject source, GameObject target)
            {
                for (int i = 0; i < source.Items.Count; i++)
                {
                    if (source.Items[i].Name.Equals(itemName))
                    {
                        Console.WriteLine("Giving item: " + source.Items[i].Name);
                        target.Items.Add(source.Items[i]);
                        source.Items.RemoveAt(i);
                        return;
                    }
                }
            };
        }
    }
    
}
