using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BallDodgeTemplate
{
    public partial class GameScreen : UserControl
    {
        public static int lives = 3;
        public static int points = 0;

        public static int screenWidth;
        public static int screenHeight;

        bool leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;

        Ball chaseBall;
        List<Ball> balls = new List<Ball>();
        Player hero;

        Random randGen = new Random();
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush redBrush = new SolidBrush(Color.Red);

        public GameScreen()
        {
            InitializeComponent();

            screenWidth = this.Width;
            screenHeight = this.Height;

            InitializeGame();
        }

        public void InitializeGame()
        {
            hero = new Player();

            int x = randGen.Next(0, 39);
            int y = randGen.Next(0, 39);

            chaseBall = new Ball(x, y, 1, 1);

            for (int i = 0; i < 5; i++)
            {
                CreateBall();
            }
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (hero.heading != "right")  // Prevent reverse direction
                    {
                        hero.ChangeDirection("left");
                    }
                    break;
                case Keys.Right:
                    if (hero.heading != "left")  // Prevent reverse direction
                    {
                        hero.ChangeDirection("right");
                    }
                    break;
                case Keys.Up:
                    if (hero.heading != "down")  // Prevent reverse direction
                    {
                        hero.ChangeDirection("up");
                    }
                    break;
                case Keys.Down:
                    if (hero.heading != "up")  // Prevent reverse direction
                    {
                        hero.ChangeDirection("down");
                    }
                    break;
            }
        }


        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Continuously move the player in the current direction
            hero.Move(hero.heading); // The player will keep moving in the last direction chosen

            // Check for collision with the chase ball
            if (hero.Collision(chaseBall))
            {
                // Increase points for eating the chase ball
                GameScreen.points++;

                // After the player eats the chase ball, generate a new chase ball at a random position
                int x = randGen.Next(0, 39);
                int y = randGen.Next(0, 39);
                chaseBall = new Ball(x, y, 1, 1); // Reset chase ball with new random coordinates
            }

            // Refresh the screen to update the display
            Refresh();
        }

        private void CreateBall()
        {
            int x = randGen.Next(0, 40);
            int y = randGen.Next(0, 40);

            Ball b = new Ball(x, y, 20, 20);
            balls.Add(b);
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // Update labels
            liveLabel.Text = $"Lives: {lives}";
            pointsLabel.Text = $"Points: {points}";

            // Draw chase ball
            e.Graphics.FillEllipse(greenBrush, chaseBall.row, chaseBall.column, chaseBall.size, chaseBall.size);

            // Draw balls to avoid
            foreach (Ball b in balls)
            {
                e.Graphics.FillEllipse(redBrush, b.row, b.column, b.size, b.size);
            }

            // Draw the player's body
            foreach (Body segment in hero.bodyParts)
            {
                e.Graphics.FillRectangle(greenBrush, segment.x, segment.y, segment.width, segment.height);
            }

            // Draw the player's head
            e.Graphics.FillRectangle(greenBrush, hero.x, hero.y, hero.width, hero.height);

            pointsLabel.Text = $"Points: {points}";
        }

    }
}