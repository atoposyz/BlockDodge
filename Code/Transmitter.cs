using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace demo.Code
{
    enum BulletProbability
    {
        space = 30,     //0
        buff = 35,      //2
        debuff = 40,    //3
        bullet = 100    //1
    }
    enum BuffProbability
    {
        shield = 15,
        magnet = 30,
        defense = 45,
        timeslack = 60,
        invincibility = 75,
        pure = 90,
        sprint = 100
    }
    class Transmitter
    {
        private int interval;
        private int speed;
        private int startX;
        public Bullet[] bullets;
        public List<Bullet> bullets2 = new List<Bullet>();
        private string? path;
        //private string[] track = new string[3];
        private List<int>[] track; //赛道，track.length是赛道的个数，目前为3
        private int tracklength;    //赛道的长度
        private int bulletnumber;   //发射物的数量
        //private int[] trackposY = new int[3] { 100, 200, 300 };
        private int timecount = 0;
        public Transmitter(int tracknumber, int startX, int interval = 500)
        {
            bulletnumber = 0;
            this.startX = startX;
            this.interval = interval;
            track = new List<int>[tracknumber];
            for (int i = 0; i < tracknumber; i++)
            {
                track[i] = new List<int> { };
            }
        }
        public Bullet[] Bullets
        {
            get { return bullets; }
            set { bullets = value; }
        }
        public List<Bullet> Bullets2 
        { 
            get { return bullets2; } 
            set { bullets2 = value; }
        }
        public int Interval
        { 
            get { return interval; } 
            set {  interval = value; } 
        }
        public void Reset()
        {
            tracklength = 100000;
            bulletnumber = 0;
            for(int i = 0; i < track.Length; i++)
            {
                track[i] = new List<int> { };
            }
            bullets = null;
            bullets2 = new List<Bullet> { };
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
                        track[i].Add(0);
                    }
                    else
                    {
                        track[i].Add(1);
                        bulletnumber++;
                    }
                }
            }
            sr.Close();
            bullets = new Bullet[bulletnumber];
        }
        public void LoadRandomTrack(int timelength)
        {
            Reset();
            tracklength = timelength;
            Random rd = new Random();
            int flag, tmp;
            for(int i = 0; i < tracklength; i++)//注意，这是赛道的长度
            {
                flag = 0;
                for(int j = 0; j < track.Length; j++)//这是赛道的个数
                {
                    tmp = rd.Next(0, 100);
                    if(tmp < (int)BulletProbability.space)
                    {
                        flag = 1;
                        track[j].Add(0);
                    } else if(tmp < (int)BulletProbability.buff)
                    {
                        flag = 1;
                        track[j].Add(2);
                        bulletnumber++;
                    } else if(tmp < (int)BulletProbability.debuff)
                    {
                        flag = 1;
                        track[j].Add(3);
                        bulletnumber++;
                    } else if(tmp < (int)BulletProbability.bullet)
                    {
                        track[j].Add(1);
                        bulletnumber++;
                    }
                }
                if(flag == 0)
                {
                    for(int j = 0; j < track.Length; j++)
                    {
                        track[j].RemoveAt(track[j].Count - 1);
                    }
                    i--;
                    bulletnumber -= track.Length;
                }
            }
            bullets = new Bullet[bulletnumber];
        }
        public int BulletNumber
        {
            get { return bulletnumber; }
        }
        public void TransmitterCheck(object source, System.Timers.ElapsedEventArgs e)
        {
            timecount += 50;
            int i = timecount / interval;    //interval秒发射一次
            if(i >= tracklength)
            {
                return;
            }
            if (timecount % interval == 0)
            {
                for(int j = 0; j < track.Length; j++)
                {
                    if (track[j][i] == 0) continue;
                    if (track[j][i] == 1)           //普通子弹
                    {
                        bullets2.Add(new Bullet(
                            new Point(startX, Tool.trackposY[j]),
                            Form1.BulletWidth, Form1.BulletHeight, GameImg.bullet_blue));
                    }
                    else if (track[j][i] == 2)    //BUFF
                    {
                        bullets2.Add(RandomBuff(startX, i, j));
                    }
                    else if (track[j][i] == 3)      //DEBUFF
                    {
                        bullets2.Add(new FEARLESS(
                            new Point(startX, Tool.trackposY[j]),
                            Form1.BulletWidth, Form1.BulletHeight, GameImg.bullet_blue));
                    }
                }
            }
        }
        public void Fire(int startX, int blockX)
        {
            int numtmp = 0;
            for (int i = 0; i < tracklength; i++)
            {
                for (int j = 0; j < track.Length; j++)
                {
                    if (track[j][i] == 1)           //普通子弹
                    {
                        bullets[numtmp] = new Bullet(
                            new Point(startX + i * interval, Tool.trackposY[j]), 
                            Form1.BulletWidth, Form1.BulletHeight, GameImg.bullet_blue);
                        numtmp++;
                    } else if (track[j][i] == 2)    //BUFF
                    {
                        bullets[numtmp] = RandomBuff(startX, i, j);
                        /*bullets[numtmp] = new TIMESLACK(
                            new Point(startX + i * interval, Tool.trackposY[j]),
                            Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);*/
                        numtmp++;
                    }
                    else if (track[j][i] == 3)      //DEBUFF
                    {
                        bullets[numtmp] = new FEARLESS(
                            new Point(startX + i * interval, Tool.trackposY[j]),
                            Form1.BulletWidth, Form1.BulletHeight, GameImg.DEBUFF);
                        numtmp++;
                    }

                }
            }
        }
        public BUFF RandomBuff(int startX, int i, int j) {
            Random rd = new Random();
            int tmp = rd.Next(0, 100);
            if(tmp < (int)BuffProbability.shield) {
                return new SHIELD(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            } else if(tmp < (int)BuffProbability.magnet) {
                return new MAGNET(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            } else if(tmp < (int)BuffProbability.defense) {
                return new DEFENSE(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            } else if(tmp < (int)BuffProbability.timeslack) {
                return new TIMESLACK(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            } else if(tmp < (int)BuffProbability.invincibility) {
                return new INVINCIBILITY(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            } else if(tmp < (int)BuffProbability.pure) {
                return new PURE(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            } else {
                return new SPRINT(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            }
        }
    }
}
