using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slalom
{
    // Class which will hold the values of the player.
    class Racer
    {
        public int x, y;
        public int xSpeed = 0, ySpeed = 2;
        public Racer(int startingX, int startingY)
        {
            x = startingX;
            y = startingY;
        }

        public void Reset(int startingX, int startingY)
        {
            x = startingX;
            y = startingY;
            xSpeed = 0;
        }
    }
}
