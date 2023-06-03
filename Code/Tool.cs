using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Code
{
    internal class Tool
    {
        public static int[] trackposY = { 100, 200, 300 };
        public static int[] blockposX = { 50, 350, 650 };
        public static Point[,] points = new Point[3, 3]{
            { new Point(blockposX[0], trackposY[0]), new Point(blockposX[0], trackposY[1]), new Point(blockposX[0], trackposY[2]) },
            { new Point(blockposX[1], trackposY[0]), new Point(blockposX[1], trackposY[1]), new Point(blockposX[1], trackposY[2]) },
            { new Point(blockposX[2], trackposY[0]), new Point(blockposX[2], trackposY[1]), new Point(blockposX[2], trackposY[2]) } };
    }
}
