using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects.AI
{
    class BasicAI
    {
        private Character character;
        private GameState gameState;
        private Order currentOrder;

        private int testCounter = 0;

        public BasicAI(Character character, GameState gameState)
        {
            this.character = character;
            this.gameState = gameState;
            CurrentOrder = new Order(Constants.Direction.East, "Moving East");
        }

        public Order CurrentOrder { get => currentOrder; set => currentOrder = value; }

        public void Evaluate()
        {
            testCounter++;

            if (CurrentOrder.Move == Constants.Direction.East && testCounter > 1)
            {
                CurrentOrder = new Order(Constants.Direction.SouthEast, "Moving SouthEast");
                testCounter = 0;
            }
            if (CurrentOrder.Move == Constants.Direction.SouthEast && testCounter > 1)
            {
                CurrentOrder = new Order(Constants.Direction.South, "Moving South");
                testCounter = 0;
            }
            if (CurrentOrder.Move == Constants.Direction.South && testCounter > 1)
            {
                CurrentOrder = new Order(Constants.Direction.SouthWest, "Moving SouthWest");
                testCounter = 0;
            }
            if (CurrentOrder.Move == Constants.Direction.SouthWest && testCounter > 1)
            {
                CurrentOrder = new Order(Constants.Direction.West, "Moving West");
                testCounter = 0;
            }
            if (CurrentOrder.Move == Constants.Direction.West && testCounter > 1)
            {
                CurrentOrder = new Order(Constants.Direction.NorthWest, "Moving NorthWest");
                testCounter = 0;
            }
            if (CurrentOrder.Move == Constants.Direction.NorthWest && testCounter > 1)
            {
                CurrentOrder = new Order(Constants.Direction.North, "Moving North");
                testCounter = 0;
            }
            if (CurrentOrder.Move == Constants.Direction.North && testCounter > 1)
            {
                CurrentOrder = new Order(Constants.Direction.NorthEast, "Moving NorthEast");
                testCounter = 0;
            }
            if (CurrentOrder.Move == Constants.Direction.NorthEast && testCounter > 1)
            {
                CurrentOrder = new Order(Constants.Direction.East, "Moving East");
                testCounter = 0;
            }
        }
    }
}
