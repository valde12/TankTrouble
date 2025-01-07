using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TankTrouble
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer1;

        private float playerX = 100f;
        private float playerY = 100f;
        private float playerAngle = 0f;
        private float playerDeltaX = 1f;
        private float playerDeltaY = 0f;
        private int playerSpeed = 15;

        private HashSet<Keys> keyStates = new HashSet<Keys>();  // Track key presses

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 16;
            timer1.Tick += timer1_Tick;
            timer1.Start();

            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Update movement based on key states
            if (keyStates.Contains(Keys.W)) MovePlayer(playerSpeed);
            if (keyStates.Contains(Keys.S)) MovePlayer(-playerSpeed);
            if (keyStates.Contains(Keys.A)) TurnPlayer(5);  // Turn left
            if (keyStates.Contains(Keys.D)) TurnPlayer(-5);   // Turn right

            this.Invalidate();  // Redraw the form
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw the elongated tank body
            int bodyWidth = 100;
            int bodyHeight = 150;
            Point[] bodyPoints = {
                new Point((int)(playerX + playerDeltaY * (bodyWidth / 2) - playerDeltaX * bodyHeight / 2), 
                          (int)(playerY - playerDeltaX * (bodyWidth / 2) - playerDeltaY * bodyHeight / 2)),

                new Point((int)(playerX - playerDeltaY * (bodyWidth / 2) - playerDeltaX * bodyHeight / 2), 
                          (int)(playerY + playerDeltaX * (bodyWidth / 2) - playerDeltaY * bodyHeight / 2)),

                new Point((int)(playerX - playerDeltaY * (bodyWidth / 2) + playerDeltaX * bodyHeight / 2), 
                          (int)(playerY + playerDeltaX * (bodyWidth / 2) + playerDeltaY * bodyHeight / 2) ),

                new Point((int)(playerX + playerDeltaY * (bodyWidth / 2) + playerDeltaX * bodyHeight / 2), 
                          (int)(playerY - playerDeltaX * (bodyWidth / 2) + playerDeltaY * bodyHeight / 2) )
            };

            g.FillPolygon(Brushes.MediumAquamarine, bodyPoints);  // Draw tank body

            // Draw the directional line as a rectangle (tank barrel)
            int barrelWidth = 40;
            int barrelLength = 70;
            Point[] barrelPoints = {
                new Point((int)(playerX + playerDeltaY * (barrelWidth / 2)),
                          (int)(playerY - playerDeltaX * (barrelWidth / 2))),

                new Point((int)(playerX - playerDeltaY * (barrelWidth / 2)),
                          (int)(playerY + playerDeltaX * (barrelWidth / 2))),

                new Point((int)(playerX + playerDeltaX * (bodyHeight / 2 + barrelLength) - playerDeltaY * (barrelWidth / 2)),
                          (int)(playerY + playerDeltaY * (bodyHeight / 2 + barrelLength) + playerDeltaX * (barrelWidth / 2))),

                new Point((int)(playerX + playerDeltaX * (bodyHeight / 2 + barrelLength) + playerDeltaY * (barrelWidth / 2)),
                          (int)(playerY + playerDeltaY * (bodyHeight / 2 + barrelLength) - playerDeltaX * (barrelWidth / 2)))
            };

            g.FillPolygon(Brushes.Aquamarine, barrelPoints);  // Draw tank barrel
        }

        private void MovePlayer(float direction)
        {
            float newX = playerX + playerDeltaX * direction;
            float newY = playerY + playerDeltaY * direction;

            // Assuming map array and map bounds check here if needed
            playerX = newX;
            playerY = newY;
        }

        private void TurnPlayer(float angleChange)
        {
            playerAngle += angleChange;
            playerAngle = FixAngle(playerAngle);

            playerDeltaX = (float)Math.Cos(DegToRad(playerAngle));
            playerDeltaY = (float)-Math.Sin(DegToRad(playerAngle));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            keyStates.Add(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keyStates.Remove(e.KeyCode);
        }

        private float DegToRad(float degrees) => degrees * (float)Math.PI / 180f;

        private float FixAngle(float angle)
        {
            if (angle > 359f) return angle - 360f;
            if (angle < 0f) return angle + 360f;
            return angle;
        }
    }
}

