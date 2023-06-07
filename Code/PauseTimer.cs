using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace demo.Code
{
    internal class PauseTimer: Timer
    {
        private double remainingtime;
        private Stopwatch stopwatch;
        private int initinterval;
        bool resumed;
        public PauseTimer(int interval): base(interval)
        {
            this.initinterval = interval;
            stopwatch = new Stopwatch();
            Elapsed += OnElapsed;
            remainingtime = 0;
        }
        public new void Start()
        {
            resetStopwatch();
            base.Start();
        }
        private void OnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (resumed)
            {
                resumed = false;
                Stop();
                Interval = initinterval;
                Start();
            }

            stopwatch.Reset();
        }
        public void Pause()
        {
            Stop();
            stopwatch.Stop();
            remainingtime = Interval - stopwatch.Elapsed.TotalMilliseconds;
            if(remainingtime <= 0)
            {
                remainingtime = 1;
            }
        }
        public void Resume()
        {
            resumed = true;
            Interval = remainingtime;
            remainingtime = 0;
            Start();
        }
        private void resetStopwatch()
        {
            stopwatch.Reset();
            stopwatch.Start();
        }
    }
}
