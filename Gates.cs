using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slalom
{
    class Gates
    {
        public int x;
        public int y;
        public Brush colorGate;
        public Gates(int X, int Y, Brush brush)
        {
            x = X;
            y = Y;
            brush = colorGate;
        }

        // If the gate is inclinig to the left or is right in the middle, the function returns true, otherwise it returns false.
        // So left is defined as "true" and right as "false".
        public bool LRChecker(int x, int widthSkiSlope)
        {
            return (x <= widthSkiSlope / 2) ? true : false;
        }
    }
}
