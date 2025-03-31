using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BallDodgeTemplate
{
    internal class Ball
    {
        public int row, column;
        public int size = 100;

        public Ball(int _x, int _y)
        {
            row = _x * 100;
            column = _y * 100; 
        }
    }
}
