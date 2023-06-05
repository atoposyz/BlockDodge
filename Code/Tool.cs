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

        public static double BULLETSPEED = 12.5;
        //public static double BULLETSPEED = 5;

        public static Player block;
        public static Form1 form;
        public static Transmitter transmitter;
        public static Timer MainTimer;
        public static Timer TransmitterTimer;

        public static Timer MagnetTimer;
        public static Timer TimeslackTimer;
        public static Timer InvincibilityTimer;
        public static Timer SprintTimer;
        public static Timer FearlessTimer;

        public static int TransmitterTime = 0;
        public static int MagnetTime = 0;
        public static int TimeslackTime = 0;
        public static int InvincibilityTime = 0;
        public static int SprintTime = 0;
        public static int FearlessTime = 0;


        public Tool(Form1 form1, Player block, Transmitter transmitter)
        {
            Tool.block = block;
            Tool.form = form1;
            Tool.transmitter = transmitter;

            MainTimer = new Timer(500);
            MainTimer.Elapsed += new System.Timers.ElapsedEventHandler(MainTimerCount);
            MainTimer.AutoReset = true;
            MainTimer.Start();

            TransmitterTimer = new Timer(50);
            TransmitterTimer.Elapsed += new System.Timers.ElapsedEventHandler(transmitter.TransmitterCheck);
            TransmitterTimer.AutoReset = true;

            Tool.MagnetTimer = new Timer(10000);
            Tool.MagnetTimer.Elapsed += new System.Timers.ElapsedEventHandler(MAGNET.LoseEfficacy);
            Tool.MagnetTimer.AutoReset = false;
            Tool.TimeslackTimer = new Timer(5000);
            Tool.TimeslackTimer.Elapsed += new System.Timers.ElapsedEventHandler(TIMESLACK.LoseEfficacy);
            Tool.TimeslackTimer.AutoReset = false;
            InvincibilityTimer = new Timer(5000);
            InvincibilityTimer.Elapsed += new System.Timers.ElapsedEventHandler(INVINCIBILITY.LoseEfficacy);
            InvincibilityTimer.AutoReset = false;
            SprintTimer = new Timer(4000);
            SprintTimer.Elapsed += new System.Timers.ElapsedEventHandler(SPRINT.LoseEfficacy);
            SprintTimer.AutoReset = false;
            FearlessTimer = new Timer(6000);
            FearlessTimer.Elapsed += new System.Timers.ElapsedEventHandler(FEARLESS.LoseEfficacy);
            SprintTimer.AutoReset = false;
        }
        public static void MainTimerCount(object source, System.Timers.ElapsedEventArgs e)
        {
            if(block.Magnet == true && MagnetTime > 0)
            {
                MagnetTime -= 500;
            }
            else
            {
                MagnetTime = 0;
            }
            if(form.BulletSpeed < Tool.BULLETSPEED && TimeslackTime > 0)
            {
                TimeslackTime -= 500;
            } 
            else
            {
                TimeslackTime = 0;
            }
            if(block.EffectIgnore == true && block.BulletIgnore == true && InvincibilityTime > 0)
            {
                InvincibilityTime -= 500;
            }
            else
            {
                InvincibilityTime = 0;
            }
            if(form.BulletSpeed > Tool.BULLETSPEED && block.EffectIgnore == true && block.BulletIgnore == true && SprintTime > 0)
            {
                SprintTime -= 500;
            }
            else
            {
                SprintTime = 0;
            }
        }
        /*public static void TransmitterCheck(object source, System.Timers.ElapsedEventArgs e)
        {
            TransmitterTime += 50;

        }*/
    }
}
