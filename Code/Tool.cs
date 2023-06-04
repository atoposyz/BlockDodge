using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

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
        public static Player block;
        public static Form1 form;
        public static Timer MagnetTimer;
        public static Timer TimeslackTimer;
        public static Timer InvincibilityTimer;
        public static Timer SprintTimer;
        public Tool(Form1 form1, Player block)
        {
            Tool.block = block;
            Tool.form = form1;
            Tool.MagnetTimer = new Timer(13500);//大概有3.5s延迟
            Tool.MagnetTimer.Elapsed += new System.Timers.ElapsedEventHandler(MAGNET.LoseEfficacy);
            Tool.MagnetTimer.AutoReset = false;
            Tool.TimeslackTimer = new Timer(8500);
            Tool.TimeslackTimer.Elapsed += new System.Timers.ElapsedEventHandler(TIMESLACK.LoseEfficacy);
            Tool.TimeslackTimer.AutoReset = false;
            InvincibilityTimer = new Timer(8500);
            InvincibilityTimer.Elapsed += new System.Timers.ElapsedEventHandler(INVINCIBILITY.LoseEfficacy);
            InvincibilityTimer.AutoReset = false;
            SprintTimer = new Timer(4000);
            SprintTimer.Elapsed += new System.Timers.ElapsedEventHandler(SPRINT.LoseEfficacy);
            SprintTimer.AutoReset = false;
        }
    }
}
