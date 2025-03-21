using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BallDodgeTemplate
{
    internal class Ball
    {
        public int row, column;
        public int size = 8;
        public int xSpeed, ySpeed;

        public Ball(int _x, int _y, int _xSpeed, int _ySpeed)
        {
            row = _x * 20;
            column = _y * 20;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
        }

        //public void Move()
        //{
        //    x += xSpeed;
        //    y += ySpeed;

        //    if (x < 0 || x > GameScreen.screenWidth - size)
        //    {
        //        xSpeed = -xSpeed;
        //    }

        //    if (y < 0 || y > GameScreen.screenHeight - size)
        //    {
        //        ySpeed = -ySpeed;
        //    }
        //}
    }
}
