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

        int boardSize = 8;
        int squareSize = 100;

        bool leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;

        Ball food;
        List<Ball> ball = new List<Ball>();
        Player hero;

        Random randGen = new Random();
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush redBrush = new SolidBrush(Color.Red);
            
        Brush lightBrush = Brushes.LightGray;
        Brush darkBrush = Brushes.Gray;

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
            int x = randGen.Next(0, 8);
            int y = randGen.Next(0, 8);

            food = new Ball(x, y); // Create food ball at random position

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

            // Check for collision with the food ball
            if (hero.Collision(food))
            {
                // After the player eats the food ball, generate a new food ball at a random position
                int x = randGen.Next(0, 8);
                int y = randGen.Next(0, 8);
                food = new Ball(x, y); // Reset food ball with new random coordinates
            }

            // Refresh the screen to update the display
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < boardSize + 1; i++) //Draws board
            {
                for (int j = 0; j < boardSize + 1; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        e.Graphics.FillRectangle(lightBrush, squareSize * (j - 1), squareSize * (i - 1), squareSize, squareSize);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(darkBrush, squareSize * (j - 1), squareSize * (i - 1), squareSize, squareSize);
                    }
                }
            }

            // Update labels
            pointsLabel.Text = $"Points: {points}";

            // Draw food ball
            e.Graphics.FillEllipse(redBrush, food.row, food.column, 100, 100);


            // Draw the player's body
            foreach (Body segment in hero.bodyParts)
            {
                e.Graphics.FillRectangle(greenBrush, segment.x, segment.y, segment.width, segment.height);
            }

            // Draw the player's head
            e.Graphics.FillRectangle(greenBrush, hero.x, hero.y, hero.width, hero.height);
        }

    }
}