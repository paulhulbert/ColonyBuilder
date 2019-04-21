using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode
{
    class GameLoop
    {
        private int testCounter = 0;
        private Form1 form;
        private DateTime lastPaint = DateTime.UtcNow;
        private DateTime lastUpdate = DateTime.UtcNow;

        private GameState gameState = new GameState();

        public GameLoop(Form1 form)
        {
            this.form = form;
        }

        public void Loop()
        {
            while (true)
            {
                testCounter++;


                int timeDifference = (DateTime.UtcNow - lastUpdate).Milliseconds;
                lastUpdate = DateTime.UtcNow;

                gameState.Update(timeDifference);


                if ((DateTime.UtcNow - lastPaint).TotalMilliseconds > (1000 / Constants.FRAME_RATE))
                {
                    lastPaint = DateTime.UtcNow;
                    form.Invalidate();
                }
                Thread.Sleep(5);
            }
        }

        public void Render(Graphics graphics)
        {
            Pen myPen = new Pen(System.Drawing.Color.Green, 5);
            Rectangle myRectangle = new Rectangle(20 + testCounter / 2, 20, 450, 450);
            graphics.DrawEllipse(myPen, myRectangle);

            gameState.Render(graphics);
        }
    }
}
