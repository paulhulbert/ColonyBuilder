using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace ColonyBuilder
{
    public partial class Form1 : Form
    {
        private GameCode.GameLoop gameLoop;

        public Form1()
        {
            InitializeComponent();
            gameLoop = new GameCode.GameLoop(this);
            Thread thread = new Thread(new ThreadStart(gameLoop.Loop));
            thread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;  Uncomment this to make full screen
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphicsObj  = e.Graphics;

            gameLoop.Render(graphicsObj);
        }
    }
}
