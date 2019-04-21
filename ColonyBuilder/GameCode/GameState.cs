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
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    AddTile(50 * i, 50 * j);
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

        public void AddTile(int x, int y)
        {
            Tile newTile = new Tile(new Location(x, y));
            Tiles.Add("" + x + "-" + y, newTile);
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
