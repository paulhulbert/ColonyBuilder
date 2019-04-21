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
            return (Location.X % 50 == 0 && Location.Y % 50 == 0);
        }

        private void LockToTile()
        {

        }

        private void FollowMoveOrder(int timeDifference)
        {
            Constants.Direction direction = basicAI.CurrentOrder.Move;

            Location startingLocation = new Location(Location.X, Location.Y);

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


            if (startingLocation.X % 50 != 0 || startingLocation.Y % 50 != 0)
            {
                if ((Math.Abs(startingLocation.X % 50 - Location.X % 50) > 25) || (Math.Abs(startingLocation.Y % 50 - Location.Y % 50) > 25))
                {
                    Location.X = ((int)Math.Round(Location.X / 50.0)) * 50;
                    Location.Y = ((int)Math.Round(Location.Y / 50.0)) * 50;
                    Console.WriteLine(startingLocation + " --- " + Location + " -- Snapped XY");
                }
            }


            
        }

        public void Update(int timeDifference)
        {
            if (IsCharacterOnTile())
            {
                basicAI.Evaluate();
                if (basicAI.CurrentOrder.Move == Constants.Direction.East)
                {
                    Sprite.ImageColor = Color.Blue;
                }
                if (basicAI.CurrentOrder.Move == Constants.Direction.West)
                {
                    Sprite.ImageColor = Color.Red;
                }
                if (basicAI.CurrentOrder.Move == Constants.Direction.North)
                {
                    Sprite.ImageColor = Color.Yellow;
                }
                if (basicAI.CurrentOrder.Move == Constants.Direction.South)
                {
                    Sprite.ImageColor = Color.Purple;
                }
                if (basicAI.CurrentOrder.Move == Constants.Direction.NorthEast)
                {
                    Sprite.ImageColor = Color.Gray;
                }
                if (basicAI.CurrentOrder.Move == Constants.Direction.NorthWest)
                {
                    Sprite.ImageColor = Color.Brown;
                }
                if (basicAI.CurrentOrder.Move == Constants.Direction.SouthEast)
                {
                    Sprite.ImageColor = Color.Aqua;
                }
                if (basicAI.CurrentOrder.Move == Constants.Direction.SouthWest)
                {
                    Sprite.ImageColor = Color.Pink;
                }
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
