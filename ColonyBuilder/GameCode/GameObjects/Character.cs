using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColonyBuilder.GameCode.GameObjects.AI;
using ColonyBuilder.GameCode.Util;

namespace ColonyBuilder.GameCode.GameObjects
{
    class Character : GameObject
    {
        private String teamName = "Player";  //Change this later to be set by constructor
        private BasicAI basicAI;
        private double speed = 100; // 50 == 1 tile per second

        public string TeamName { get => teamName; set => teamName = value; }

        public Character(Location location, GameState gameState) : base(new Sprite(Color.Blue), location)
        {
            basicAI = new BasicAI(this, gameState);
        }

        private bool IsCharacterOnTile()
        {
            int currentX = (int)Location.X;
            int currentY = (int)Location.Y;

            return (currentX % 50 == 0 && currentY % 50 == 0);
        }

        private void FollowMoveOrder(int timeDifference)
        {
            Constants.Direction direction = basicAI.CurrentOrder.Move;

            if (direction == Constants.Direction.East)
            {
                Location.X += ((double)timeDifference / 1000) * 100;
            }
            if (direction == Constants.Direction.West)
            {
                Location.X -= ((double)timeDifference / 1000) * 100;
            }
            if (direction == Constants.Direction.North)
            {
                Location.Y -= ((double)timeDifference / 1000) * 100;
            }
            if (direction == Constants.Direction.South)
            {
                Location.Y += ((double)timeDifference / 1000) * 100;
            }
            if (direction == Constants.Direction.NorthEast)
            {
                Location.X += ((double)timeDifference / 1000) * 100 / 1.4;
                Location.Y -= ((double)timeDifference / 1000) * 100 / 1.4;
            }
            if (direction == Constants.Direction.NorthWest)
            {
                Location.X -= ((double)timeDifference / 1000) * 100 / 1.4;
                Location.Y -= ((double)timeDifference / 1000) * 100 / 1.4;
            }
            if (direction == Constants.Direction.SouthEast)
            {
                Location.X += ((double)timeDifference / 1000) * 100 / 1.4;
                Location.Y += ((double)timeDifference / 1000) * 100 / 1.4;
            }
            if (direction == Constants.Direction.SouthWest)
            {
                Location.X -= ((double)timeDifference / 1000) * 100 / 1.4;
                Location.Y += ((double)timeDifference / 1000) * 100 / 1.4;
            }

        }

        public void Update(int timeDifference)
        {
            if (IsCharacterOnTile())
            {
                basicAI.evaluate();
            }

            if (basicAI.CurrentOrder.Type == Order.OrderType.Move)
            {
                FollowMoveOrder(timeDifference);
            }
        }

        public override void Render(Graphics graphics)
        {
            Sprite.Render(graphics, Location.X, Location.Y, 50, 50, 0);
        }
    }
}
