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
            this.imageColor = imageColor;
        }

        public void Render(Graphics graphics, double x, double y, int width, int height, double rotation)
        {
            Brush brush = new SolidBrush(imageColor);
            Rectangle myRectangle = new Rectangle((int)x, (int)y, width, height);
            graphics.FillRectangle(brush, myRectangle);
        }
    }
}
