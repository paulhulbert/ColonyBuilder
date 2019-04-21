using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects.AI
{
    class Order
    {
        public enum OrderType
        {
            Move,
            Interact
        }

        private Constants.Direction move;
        private Interaction interaction = null;
        private String infoText;
        private OrderType type;

        public Constants.Direction Move { get => move; }
        public Interaction Interaction { get => interaction; }
        public String InfoText { get => infoText; }
        public OrderType Type { get => type; }

        public Order(Interaction interaction, string infoText)
        {
            this.interaction = interaction;
            this.infoText = infoText;
            type = OrderType.Interact;
        }

        public Order(Constants.Direction move, string infoText)
        {
            this.move = move;
            this.infoText = infoText;
            type = OrderType.Move;
        }
    }
}
