using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects.AI
{
    class Interaction
    {
        private GameObject source;
        private GameObject target;
        private GameState gameState;
        private string infoText;
        private int interactionLength;
        private int timePassed = 0;
        private Action<GameState, GameObject, GameObject> interact;
        public bool InteractionFinished { get => timePassed > interactionLength; }

        public Interaction(GameObject source, GameObject target, GameState gameState, string infoText, Action<GameState, GameObject, GameObject> interact, int interactionLength)
        {
            this.source = source;
            this.target = target;
            this.gameState = gameState;
            this.infoText = infoText;
            this.interact = interact;
            this.interactionLength = interactionLength;
        }

        public void Update(int timeSinceUpdate)
        {
            timePassed += timeSinceUpdate;

            if (InteractionFinished)
            {
                interact(gameState, source, target);
            }
        }
    }
}
