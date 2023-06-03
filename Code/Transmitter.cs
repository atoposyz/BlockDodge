using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Code
{
    class Transmitter
    {
        private int interval;
        private int speed;
        public Bullet[] bullets;
        private string? path;
        //private string[] track = new string[3];
        private List<bool>[] track;
        private int tracklength;
        private int bulletnumber;
        public Transmitter(int tracknumber)
        {
            bulletnumber = 0;
            track = new List<bool>[tracknumber];
            for (int i = 0; i < tracknumber; i++)
            {
                track[i] = new List<bool> { };
            }
        }
        public Bullet[] Bullets
        {
            get { return bullets; }
            set { bullets = value; }
        }
        private void Reset()
        {
            tracklength = 100000;
            bulletnumber = 0;
        }
        public void LoadTrack(string docname)
        {
            path = "../../../config/" + docname;
            Reset();
            StreamReader sr = new StreamReader(path);
            string? line;
            for (int i = 0; i < track.Length; i++)
            {
                line = sr.ReadLine();
                tracklength = Math.Min(tracklength, line.Length);
                foreach (char ch in line)
                {
                    if (ch == '0')
                    {
                        track[i].Add(false);
                    }
                    else
                    {
                        track[i].Add(true);
                        bulletnumber++;
                    }
                }
            }
            sr.Close();
            bullets = new Bullet[bulletnumber];
        }
        public int BulletNumber
        {
            get { return bulletnumber; }
        }
        public void Fire(int startX)
        {
            int numtmp = 0;
            for (int i = 0; i < tracklength; i++)
            {
                for (int j = 0; j < track.Length; j++)
                {
                    if (track[j][i] == true)
                    {
                        bullets[numtmp] = new Bullet(new Point(startX + i * 200, Form1.Points[j].Y), Form1.BulletWidth, Form1.BulletHeight, GameImg.Bullet);
                        numtmp++;
                    }

                }
            }
        }
    }
}
