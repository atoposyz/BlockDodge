﻿using System;
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
        public static int INTERVAL;
        public static int BULLETTOTAL = 60;
        public static int PERSCORE = 5;

        public static Player block;
        public static Form1 form;
        public static Transmitter transmitter;
        public static Timer MainTimer;
        public static Timer TransmitterTimer;

        public static PauseTimer MagnetTimer;
        public static PauseTimer TimeslackTimer;
        public static PauseTimer InvincibilityTimer;
        public static PauseTimer SprintTimer;
        public static PauseTimer FearlessTimer;
        public static PauseTimer QuickTimer;
        public static PauseTimer NightwalkTimer;
        public static Timer DefenseTimer;
        public static Timer PureTimer;
        public static Timer BraveTimer;
        public static Timer GoodluckTimer;
        public static Timer ShieldTimer;

        public static int TransmitterTime = 0;
        public static int MagnetTime = 0;
        public static int TimeslackTime = 0;
        public static int InvincibilityTime = 0;
        public static int SprintTime = 0;
        public static int FearlessTime = 0;
        public static int QuickTime = 0;
        public static int NightwalkTime = 0;
        public static int DefenseTime = 0;
        public static int PureTime = 0;
        public static int BraveTime = 0;
        public static int GoodluckTime = 0;
        public static int ShieldTime = 0;

        public static int score = 0;
        public static int activebullets = 0;

        //public static string debugdot = "../../../";
        public static string debugdot = "";



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

            Tool.MagnetTimer = new PauseTimer(10000);
            Tool.MagnetTimer.Elapsed += new System.Timers.ElapsedEventHandler(MAGNET.LoseEfficacy);
            Tool.MagnetTimer.AutoReset = false;
            Tool.TimeslackTimer = new PauseTimer(5000);
            Tool.TimeslackTimer.Elapsed += new System.Timers.ElapsedEventHandler(TIMESLACK.LoseEfficacy);
            Tool.TimeslackTimer.AutoReset = false;
            InvincibilityTimer = new PauseTimer(5000);
            InvincibilityTimer.Elapsed += new System.Timers.ElapsedEventHandler(INVINCIBILITY.LoseEfficacy);
            InvincibilityTimer.AutoReset = false;
            SprintTimer = new PauseTimer(4000);
            SprintTimer.Elapsed += new System.Timers.ElapsedEventHandler(SPRINT.LoseEfficacy);
            SprintTimer.AutoReset = false;
            FearlessTimer = new PauseTimer(6000);
            FearlessTimer.Elapsed += new System.Timers.ElapsedEventHandler(FEARLESS.LoseEfficacy);
            FearlessTimer.AutoReset = false;
            QuickTimer = new PauseTimer(5000);
            QuickTimer.Elapsed += new System.Timers.ElapsedEventHandler(QUICK.LoseEfficacy);
            QuickTimer.AutoReset = false;
            NightwalkTimer = new PauseTimer(4000);
            NightwalkTimer.Elapsed += new System.Timers.ElapsedEventHandler(NIGHTWALK.LoseEfficacy);
            NightwalkTimer.AutoReset = false;
            DefenseTimer = new Timer(500);
            DefenseTimer.Elapsed += new System.Timers.ElapsedEventHandler(DEFENSE.LoseEfficacy);
            DefenseTimer.AutoReset = false;
            PureTimer = new Timer(500);
            PureTimer.Elapsed += new System.Timers.ElapsedEventHandler(PURE.LoseEfficacy);
            PureTimer.AutoReset= false;
            BraveTimer = new Timer(500);
            BraveTimer.Elapsed += new System.Timers.ElapsedEventHandler(BRAVE.LoseEfficacy);
            BraveTimer.AutoReset = false;
            GoodluckTimer = new Timer(500);
            GoodluckTimer.Elapsed += new System.Timers.ElapsedEventHandler(GOODLUCK.LoseEfficacy);
            GoodluckTimer.AutoReset = false;
            ShieldTimer = new Timer(500);
            ShieldTimer.Elapsed += new System.Timers.ElapsedEventHandler(SHIELD.LoseEfficacy);
            ShieldTimer.AutoReset = false;
        }
        public static void reset ()
        {
            MainTimer.Stop();
            TransmitterTimer.Stop();
            MagnetTimer.Stop();
            TimeslackTimer.Stop();
            InvincibilityTimer.Stop();
            SprintTimer.Stop();
            FearlessTimer.Stop();
            QuickTimer.Stop();
            NightwalkTimer.Stop();

            TransmitterTime = 0;
            MagnetTime = 0;
            TimeslackTime = 0;
            InvincibilityTime = 0;
            SprintTime = 0;
            FearlessTime = 0;
            QuickTime = 0;
            NightwalkTime = 0;
            score = 0;
            Tool.PERSCORE = 5;

        }

        public static void EffectPause()
        {
            MagnetTimer.Pause();
            TimeslackTimer.Pause();
            InvincibilityTimer.Pause();
            SprintTimer.Pause();
            FearlessTimer.Pause();
            QuickTimer.Pause();
            NightwalkTimer.Pause();
        }

        public static void EffectResume()
        {
            MagnetTimer.Resume();
            TimeslackTimer.Resume();
            InvincibilityTimer.Resume();
            SprintTimer.Resume();
            FearlessTimer.Resume();
            QuickTimer.Resume();
            NightwalkTimer.Resume();
        }

        public static void EffectStop() 
        {
            block.EffectIgnore = false;
            block.BulletIgnore = false;
            block.Magnet = false;
            block.Fearless = false;
            block.Timeslack = false;
            block.Quick = false;
            Tool.form.BulletSpeed = Tool.BULLETSPEED;
            Tool.transmitter.Interval = Tool.INTERVAL;
            Tool.MagnetTimer.Stop();
            Tool.FearlessTimer.Stop();
            Tool.InvincibilityTimer.Stop();
            Tool.TimeslackTimer.Stop();
            Tool.SprintTimer.Stop();
            Tool.PERSCORE = 5;
        }

        public static void MainTimerCount(object source, System.Timers.ElapsedEventArgs e)
        {
            if(form.pause == true)
            {
                return;
            }
            score += PERSCORE;
            if(block.Magnet == true && MagnetTime > 0)
            {
                MagnetTime -= 500;
            }
            else
            {
                MagnetTime = 0;
            }
            if(form.BulletSpeed < Tool.BULLETSPEED && TimeslackTime > 0 && block.Timeslack == true)
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
            if(block.Fearless == true && FearlessTime > 0) 
            { 
                FearlessTime -= 500; 
            }
            else
            {
                FearlessTime = 0;
            }
            if(block.Quick == true && QuickTime > 0)
            {
                QuickTime -= 500;
            }
            else
            {
                QuickTime = 0;
            }
            if(DefenseTime > 0)
            {
                DefenseTime -= 250;
            }
            else
            {
                DefenseTime = 0;
            }
            if(PureTime > 0)
            {
                PureTime -= 250;
            }
            else
            {
                PureTime = 0;
            }
            if(BraveTime > 0)
            {
                BraveTime -= 250;
            }
            else
            {
                BraveTime = 0;
            }
            if(GoodluckTime > 0)
            {
                GoodluckTime -= 250;
            }
            else
            {
                GoodluckTime = 0;
            }
            if(ShieldTime > 0)
            {
                ShieldTime -= 250;
            }
            else
            {
                 ShieldTime = 0;
            }
            activebullets = 0;
            foreach (var bullet in transmitter.bullets)
            {
                if(bullet != null)
                {
                    activebullets++;
                }
            }
        }
        
    }
}
