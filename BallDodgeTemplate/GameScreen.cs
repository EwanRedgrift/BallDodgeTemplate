using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

            int x = randGen.Next(20, this.Width - 50);
            int y = randGen.Next(20, this.Height - 50);

            chaseBall = new Ball(x, y, 8, 8);

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
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
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
            #region move code
            // moving the hero
            if (rightArrowDown == true)
            {
                hero.Move("right");
            }

            if (leftArrowDown == true)
            {
                hero.Move("left");
            }

            if (upArrowDown == true)
            {
                hero.Move("up");
            }

            if (downArrowDown == true)
            {
                hero.Move("down");
            }

            //moving the chase ball
            chaseBall.Move();

            //moving enemies
            foreach (Ball b in balls)
            {
                b.Move();
            }
            #endregion

            if (hero.Collision(chaseBall))
            {
                points++;

                CreateBall();
            }

            foreach (Ball b in balls)
            {
                if (hero.Collision(b))
                {
                    lives--;
                }
            }

            if (lives == 0)
            {
                gameTimer.Stop();
            }

            Refresh();
        }

        private void CreateBall()
        {
            int x = randGen.Next(20, this.Width - 50);
            int y = randGen.Next(20, this.Height - 50);

            Ball b = new Ball(x, y, 8, 8);
            balls.Add(b);
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //update labels
            liveLabel.Text = $"Lives: {lives}";
            pointsLabel.Text = $"Points: {points}";


            //chaseball
            e.Graphics.FillEllipse(greenBrush, chaseBall.x, chaseBall.y, chaseBall.size, chaseBall.size);

            //balls to avoid
            foreach (Ball b in balls)
            {
                e.Graphics.FillEllipse(redBrush, b.x, b.y, b.size, b.size);
            }

            //hero
            e.Graphics.FillRectangle(greenBrush, hero.x, hero.y, hero.width, hero.height);
        }
    }
}
