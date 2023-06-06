using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;


/*
空地: 0
子弹: 1
buff: 2
debuff: 3
effect: 4
shield: 5
magnet: 6
defense: 7
timeslack: 8
invincibility:9
pure: a
sprint: b
*/

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
    enum DebuffProbability
    {
        brave = 25,
        fearless = 50,
        goodluck = 75,
        quick = 100
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
        private bool goodluck;
        private bool endtransmit;
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
        public bool Goodluck
        {
            get { return goodluck; } 
            set { goodluck = value; }
        }
        public void Reset()
        {
            tracklength = 100000;
            bulletnumber = 0;
            timecount = 0;
            goodluck = false;
            endtransmit = false;
            for (int i = 0; i < track.Length; i++)
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
                    else if(ch > '0' && ch <= '9')
                    {
                        track[i].Add((int)(ch - '0'));
                        bulletnumber++;
                    } else
                    {
                        track[i].Add(ch - 'a' + 10);
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
            if(Tool.form.pause == true)
            {
                return;
            }
            timecount += 50;
            int i = timecount / interval;    //interval秒发射一次
            if(i >= tracklength && endtransmit == false)
            {
                endtransmit = true;
                bullets2.Add(new Final(new Point(startX, Tool.trackposY[0]), Form1.BulletWidth, Form1.BulletHeight, GameImg.goal));
                bullets2.Add(new Final(new Point(startX, Tool.trackposY[1]), Form1.BulletWidth, Form1.BulletHeight, GameImg.goal));
                bullets2.Add(new Final(new Point(startX, Tool.trackposY[2]), Form1.BulletWidth, Form1.BulletHeight, GameImg.goal));
                return;
            }
            if(endtransmit == true)
            {
                return;
            }
            if (timecount % interval == 0)
            {
                if(goodluck == true)
                {
                    goodluck = false;

                    bullets2.Add(RandomEffect(startX, i, 0, GameImg.RandomEffect)); 
                    bullets2.Add(RandomEffect(startX, i, 1, GameImg.RandomEffect)); 
                    bullets2.Add(RandomEffect(startX, i, 2, GameImg.RandomEffect));
                }
                else
                {
                    for(int j = 0; j < track.Length; j++)
                    {
                        if (track[j][i] == 0) continue;
                        if (track[j][i] == 1)           //普通子弹
                        {
                            bullets2.Add(new Bullet(
                                new Point(startX, Tool.trackposY[j]),
                                Form1.BulletWidth, Form1.BulletHeight, GameImg.sword));
                        }
                        else if (track[j][i] == 2)    //BUFF
                        {
                            bullets2.Add(RandomBuff(startX, i, j, GameImg.BUFF));
                        }
                        else if (track[j][i] == 3)      //DEBUFF
                        {
                            bullets2.Add(RandomDebuff(startX, i, j, GameImg.DEBUFF));
                        } 
                        else if(track[j][i] == 4)
                        {

                        }
                        else if (track[j][i] > 4 && track[j][i] <= 11)
                        {
                            bullets2.Add(CertainBUFF(track[j][i] - 4, j));
                        }
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
                            Form1.BulletWidth, Form1.BulletHeight, GameImg.sword);
                        numtmp++;
                    } else if (track[j][i] == 2)    //BUFF
                    {
                        bullets[numtmp] = RandomBuff(startX, i, j, GameImg.BUFF);
                        /*bullets[numtmp] = new TIMESLACK(
                            new Point(startX + i * interval, Tool.trackposY[j]),
                            Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);*/
                        numtmp++;
                    }
                    else if (track[j][i] == 3)      //DEBUFF
                    {
                        bullets[numtmp] = RandomDebuff(startX, i, j, GameImg.DEBUFF);
                        /*bullets[numtmp] = new FEARLESS(
                            new Point(startX + i * interval, Tool.trackposY[j]),
                            Form1.BulletWidth, Form1.BulletHeight, GameImg.DEBUFF);*/
                        numtmp++;
                    }

                }
            }
        }
        public BUFF RandomBuff(int startX, int i, int j, Bitmap bm) {
            Random rd = new Random();
            int tmp = rd.Next(0, 100);
            if(tmp < (int)BuffProbability.shield) {
                return new SHIELD(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, bm);
            } else if(tmp < (int)BuffProbability.magnet) {
                return new MAGNET(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, bm);
            } else if(tmp < (int)BuffProbability.defense) {
                return new DEFENSE(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, bm);
            } else if(tmp < (int)BuffProbability.timeslack) {
                return new TIMESLACK(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, bm);
            } else if(tmp < (int)BuffProbability.invincibility) {
                return new INVINCIBILITY(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, bm);
            } else if(tmp < (int)BuffProbability.pure) {
                return new PURE(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, bm);
            } else {
                return new SPRINT(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, bm);
            }
        }
        public DEBUFF RandomDebuff(int startX, int i, int j, Bitmap bm)
        {
            Random rd = new Random();
            int tmp = rd.Next(0, 100);
            if (tmp < (int)DebuffProbability.brave)
            {
                return new BRAVE(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, bm);
            }
            else if (tmp < (int)DebuffProbability.fearless)
            {
                return new FEARLESS(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, bm);
            }
            else if (tmp < (int)DebuffProbability.goodluck)
            {
                return new GOODLUCK(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, bm);
            }
            else if (tmp < (int)DebuffProbability.quick)
            {
                return new QUICK(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, bm);
            }
            else
            {
                return new NIGHTWALK(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, bm);
            }
        }
        public Effect RandomEffect(int startX, int i, int j, Bitmap bm)
        {
            Random rd = new Random();
            int tmp = rd.Next(0, 100);
            if(tmp < 30)
            {
                return RandomBuff(startX, i, j, bm);
            }
            else
            {
                return RandomDebuff(startX, i, j, bm);
            }
        }
        public BUFF CertainBUFF(int number, int j)
        {
            if (number == 1)
            {
                return new SHIELD(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            }
            else if (number == 2)
            {
                return new MAGNET(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            }
            else if (number == 3)
            {
                return new DEFENSE(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            }
            else if (number == 4)
            {
                return new TIMESLACK(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            }
            else if (number == 5)
            {
                return new INVINCIBILITY(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            }
            else if (number == 6)
            {
                return new PURE(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            }
            else
            {
                return new SPRINT(
                    new Point(startX, Tool.trackposY[j]),
                    Form1.BulletWidth, Form1.BulletHeight, GameImg.BUFF);
            }
        }
    }
}
