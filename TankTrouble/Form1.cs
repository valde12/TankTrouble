using System;
using System.Drawing;
using System.Windows.Forms;

namespace TankTrouble
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer1;   // Timer for game updates
        private int playerX = 50;  // Example player position
        private int playerY = 100;
        private int playerSpeed = 10;  // Speed for movement

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;  // Reduces flickering

            // Initialize and start the timer
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 16;  // ~60 frames per second
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();

            // Register event handlers
            this.Paint += new PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(this.Form1_KeyUp);
        }

        // Timer event for game loop
        private void timer1_Tick(object sender, EventArgs e)
        {
            playerX += 1;
            this.Invalidate();  // Redraws the form
        }

        // Paint event for drawing
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Blue, new Rectangle(playerX, playerY, 50, 50));  // Draws a blue rectangle
        }

        // KeyDown event for movement
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                playerX -= playerSpeed;
            else if (e.KeyCode == Keys.Right)
                playerX += playerSpeed;
        }

        // KeyUp event for future handling (optional in this example)
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // Stop or handle other key events here
        }
    }
}
