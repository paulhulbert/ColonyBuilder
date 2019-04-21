using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.GameObjects
{
    class Sprite
    {
        private Color imageColor;

        public Sprite(Color imageColor)
        {
            this.ImageColor = imageColor;
        }

        public Color ImageColor { get => imageColor; set => imageColor = value; }

        public void Render(Graphics graphics, double x, double y, int width, int height, double rotation)
        {
            Pen brush = new Pen(ImageColor, 2);
            Rectangle myRectangle = new Rectangle((int)x, (int)y, width, height);
            graphics.DrawRectangle(brush, myRectangle);
        }
    }
}
