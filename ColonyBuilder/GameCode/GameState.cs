using ColonyBuilder.GameCode.GameObjects;
using ColonyBuilder.GameCode.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode
{
    class GameState : Renderable
    {
        private Dictionary<String, Tile> Tiles = new Dictionary<String, Tile>();
        private Dictionary<String, Team> Teams = new Dictionary<string, Team>();

        public GameState()
        {

            int testCounter = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    AddTile(50 * i, 50 * j);
                    if (Constants.random.Next() % 4 == 0)
                    {
                        testCounter++;
                        GetTile(50 * i, 50 * j).Wall = new Wall(true);
                        if (testCounter == 10 || testCounter == 70) 
                        {
                            GetTile(50 * i, 50 * j).Wall.Items.Add(new Item(new Sprite(Color.Red), "Food"));
                            GetTile(50 * i, 50 * j).Wall.ShowItems = true;
                            Console.WriteLine("Food: " + GetTile(50 * i, 50 * j).Location);
                        }
                        if (testCounter == 50 || testCounter == 90)
                        {
                            GetTile(50 * i, 50 * j).Wall.Items.Add(new Item(new Sprite(Color.Yellow), "Gold"));
                            GetTile(50 * i, 50 * j).Wall.ShowItems = true;
                        }
                    }
                }
            }
            

            Character testCharacter = new Character(new Location(500, 300), this);
            AddCharacter(testCharacter);
        }

        public void AddCharacter(Character character)
        {
            if (Teams.ContainsKey(character.TeamName))
            {
                Teams[character.TeamName].Characters.Add(character);
            }
            else
            {
                Team newTeam = new Team(character.TeamName);
                newTeam.Characters.Add(character);
                Teams.Add(character.TeamName, newTeam);
            }
        }

        public Tile GetTile(int x, int y)
        {
            return GetTile(new Location(x, y));
        }
        public Tile GetTile(Location location)
        {
            if (Tiles.ContainsKey(location.ToString()))
            {
                return Tiles[location.ToString()];
            }
            return null;
        }

        public void AddTile(int x, int y)
        {
            AddTile(new Location(x, y));
        }
        public void AddTile(Location location)
        {
            Tile newTile = new Tile(location);
            Tiles.Add(newTile.Location.ToString(), newTile);
            newTile.AdjacentTiles = GetAdjacentTiles(location);
            
            foreach (Constants.Direction direction in newTile.AdjacentTiles.Keys)
            {
                newTile.AdjacentTiles[direction].AdjacentTiles.Add(Constants.oppositeDirection(direction), newTile);
            }
        }

        private Dictionary<Constants.Direction, Tile> GetAdjacentTiles(Location center)
        {
            Dictionary<Constants.Direction, Tile> tiles = new Dictionary<Constants.Direction, Tile>();

            Tile northTile = GetTile(new Location(center.X, center.Y - 50));
            Tile northEastTile = GetTile(new Location(center.X + 50, center.Y - 50));
            Tile eastTile = GetTile(new Location(center.X + 50, center.Y));
            Tile southEastTile = GetTile(new Location(center.X + 50, center.Y + 50));
            Tile southTile = GetTile(new Location(center.X, center.Y + 50));
            Tile southWestTile = GetTile(new Location(center.X - 50, center.Y + 50));
            Tile westTile = GetTile(new Location(center.X - 50, center.Y));
            Tile northWestTile = GetTile(new Location(center.X - 50, center.Y - 50));

            if (northTile != null)
            {
                tiles.Add(Constants.Direction.North, northTile);
            }
            if (northEastTile != null)
            {
                tiles.Add(Constants.Direction.NorthEast, northEastTile);
            }
            if (northWestTile != null)
            {
                tiles.Add(Constants.Direction.NorthWest, northWestTile);
            }
            if (southTile != null)
            {
                tiles.Add(Constants.Direction.South, southTile);
            }
            if (southEastTile != null)
            {
                tiles.Add(Constants.Direction.SouthEast, southEastTile);
            }
            if (southWestTile != null)
            {
                tiles.Add(Constants.Direction.SouthWest, southWestTile);
            }
            if (eastTile != null)
            {
                tiles.Add(Constants.Direction.East, eastTile);
            }
            if (westTile != null)
            {
                tiles.Add(Constants.Direction.West, westTile);
            }

            return tiles;
        }

        public void Update(int timeDifference)
        {
            foreach (Team team in Teams.Values)
            {
                team.Update(timeDifference);
            }
        }

        public void Render(Graphics graphics)
        {
            foreach (Tile tile in Tiles.Values)
            {
                tile.Render(graphics);
            }

            foreach (Team team in Teams.Values)
            {
                team.Render(graphics);
            }
        }
    }
}
