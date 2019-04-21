using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects.AI
{
    class Interaction
    {
        public enum InteractionType
        {
            Take,
            Give
        }


        private GameObject source;
        private GameObject target;
        private InteractionType type;



    }
}
