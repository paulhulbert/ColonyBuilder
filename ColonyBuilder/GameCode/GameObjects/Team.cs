using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects
{
    class Team : Renderable
    {
        private String name;
        private List<Character> characters = new List<Character>();

        public Team(String name)
        {
            this.Name = name;
        }

        public List<Character> Characters { get => characters; set => characters = value; }
        public string Name { get => name; set => name = value; }

        public void Update(int timeDifference)
        {
            foreach (Character character in Characters)
            {
                character.Update(timeDifference);
            }
        }

        public void Render(Graphics graphics)
        {
            foreach (Character character in Characters)
            {
                character.Render(graphics);
            }
        }

    }
}
