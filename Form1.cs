using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Timer = System.Windows.Forms.Timer;
using demo.Code;

namespace demo
{
    public partial class Form1 : Form
    {

        private Player block;
        //private int posY;
        //private int posX;
        //private Point startpoint = new Point(50, 200);
        private Point startpoint;
        /*public static Point[,] Points = new Point[3, 3] { 
            { new Point(50, 100), new Point(50, 200), new Point(50, 300) },
            { new Point(350, 100), new Point(350, 200), new Point(350, 300) },
            { new Point(650, 100), new Point(650, 200), new Point(650, 300) } };*/
        private int timelast;

        private List<DrawableObject> dos = new List<DrawableObject>();
        private Transmitter transmitter = new Transmitter(TrackNumber);
        private Timer timer;

        public const int TrackNumber = 3;
        public const int BlockSize = 60;
        public const int BlockSpeed = 10;
        public const int BulletSize = 60;
        public const int BulletWidth = 60;
        public const int BulletHeight = 60;
        public int BulletSpeed = 5;

        private Random random;
        private BufferedGraphics buffer;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            //InitializeGame();
            //DoubleBuffered = true;
            //posY = 1;
            //posX = 0;
            startpoint = Tool.points[0, 1];
            block = new Player(startpoint, BlockSize, BlockSize, GameImg.Square);
            block.CoordinateX = 0;
            block.CoordinateY = 1;
            Tool tool = new Tool(this, block);
            //panel2.Resize += new EventHandler(panel2_Resize);
            buffer = BufferedGraphicsManager.Current.Allocate(panel2.CreateGraphics(), panel2.DisplayRectangle);
        }

        //private void InitializeGame()
        //{
        //    //buffer.Graphics.Clear(BackColor);
        //    block = new Player(Points[posY], BlockSize, BlockSize, GameImg.Square);
        //    dos.Add(block);
        //    timer = new Timer();
        //    timer.Interval = 20;
        //    timer.Tick += new EventHandler(timer_Tick);
        //    timer.Start();

        //}
        private bool CollidesJudge(int No_, Bullet bullet)
        {
            if (bullet.DamageType == 0)  //如果是子弹
            {
                if (block.BulletIgnore == true)
                {
                    transmitter.Bullets[No_] = null;
                    UpdateStatus();
                }
                else if (block.ShieldCapacity > 0)
                {
                    block.ShieldCapacity--;
                    transmitter.Bullets[No_] = null;
                    UpdateStatus();
                }
                else
                {
                    return true;
                }
            }
            else if (bullet.DamageType == 1) //如果是效果类
            {
                if (block.EffectIgnore == true)
                {
                    transmitter.Bullets[No_] = null;
                }
                else
                {
                    ((BUFF)bullet).CauseEffect(block);
                    UpdateStatus();
                    transmitter.Bullets[No_] = null;
                }
            }
            else if (bullet.DamageType == 2)
            {
                if (block.EffectIgnore == true)
                {
                    transmitter.Bullets[No_] = null;
                }
                else
                {
                    ((DEBUFF)bullet).CauseEffect(block);
                    UpdateStatus();
                    transmitter.Bullets[No_] = null;
                }
            }
            return false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (transmitter.Bullets != null)
            {
                for (int i = 0; i < transmitter.Bullets.Length; i++)
                {
                    Bullet bullet = transmitter.Bullets[i];
                    if (bullet == null) continue;
                    bullet.Move();
                    //bullet.Draw(panel2.CreateGraphics());
                    if (bullet.LeaveScreen())
                    {
                        bullet = null;
                        continue;
                        //bullet.Position = new Point(panel2.Width, random.Next(panel2.Height - BulletHeight));
                    }
                    //有磁铁效果时判断一下有没有经过BUFF，经过就自动吃掉BUFF
                    if (block.Magnet == true && bullet.MeetWith(block) && bullet.DamageType == 1)
                    {
                        ((BUFF)bullet).CauseEffect(block);
                        UpdateStatus();
                        transmitter.Bullets[i] = null;
                        continue;
                    }
                    if (bullet.CollidesWith(block))
                    {
                        bool flag = CollidesJudge(i, bullet);
                        if (flag == true)
                        {
                            //InitializeGame();
                            timer.Stop();
                            MessageBox.Show("Game Over");
                            break;
                        }
                    }
                }

                DrawGame();
            }
            if (block.Magnet == true && timelast > 0)
            {
                timelimit.Text = (timelast / 1000).ToString() + 's';
                timelast -= 20;
            }
            else
            {
                timelimit.Text = "0s";
                timelast = 0;
            }
            //UpdateDrawableObject();
            //panel2.Invalidate();
            //block.Draw(panel2.CreateGraphics());
        }

        public void UpdateStatus()
        {
            shieldtext.Text = block.ShieldCapacity.ToString();

        }
        private void DrawGame()
        {
            buffer.Graphics.Clear(BackColor);
            block.Draw(buffer.Graphics);

            foreach (Bullet bullet in transmitter.Bullets)
            {
                if (bullet != null)
                {
                    bullet.Draw(buffer.Graphics);
                }
            }

            buffer.Render();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            buffer.Graphics.Clear(BackColor);
            foreach (DrawableObject drawable in dos)
            {
                if (drawable != null)
                {
                    drawable.Draw(buffer.Graphics);
                }

            }
            buffer.Render(e.Graphics);

        }
        private void UpdateDrawableObject()
        {
            panel2.CreateGraphics().Clear(BackColor);
            foreach (DrawableObject drawable in dos)
            {
                drawable.Draw(panel2.CreateGraphics());
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            dos.Add(block);


            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();


            random = new Random();
            //bullets = new Bullet[5];
            //transmitter.Bullets = new Bullet[transmitter.BulletNumber];
            GenerateBullets();
            //label1.Text = "start on " + block.Position.ToString();
        }

        private void GenerateBullets()
        {
            /*
            int startY = random.Next(panel2.Height - BulletHeight);
            for (int i = 0; i < bullets.Length; i++)
            {
                int startX = random.Next(panel2.Width, panel2.Width + 200);
                bullets[i] = new Bullet(new Point(startX, startY), BulletWidth, BulletHeight, GameImg.Bullet);
                startY += BulletHeight * 2;
            }*/
            transmitter.Fire(panel2.Width, block.CoordinateX);
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            //transmitter.LoadTrack("demo1.txt");
            transmitter.LoadRandomTrack(50);
        }

        //private void panel2_MouseClick(object sender, MouseEventArgs e)
        //{
        //    Bullet bullet = new Bullet(panel1.Width, e.Y, 5);
        //    bullet.Draw(panel2.CreateGraphics());
        //    bullets.Add(bullet);
        //    dos.Add(bullet);
        //}
        public int Timelast
        {
            get { return timelast; }
            set { timelast = value; }
        }
        public void setlabel1(string text)
        {
            //label1.Text = text;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            label1.Text = ((char)e.KeyValue).ToString();
            if ((char)e.KeyValue == 'W' && block != null)
            {
                if (block.CoordinateY > 0)
                {
                    block.CoordinateY--;
                }
                block.changepos(block.CoordinateX, block.CoordinateY);
                //UpdateDrawableObject();
                //label1.Text = "change to pos " + block.CoordinateY + " on" + block.Position.ToString();
            }
            else if ((char)e.KeyValue == 'S' && block != null)
            {
                if (block.CoordinateY < 2)
                {
                    block.CoordinateY++;
                }
                block.changepos(block.CoordinateX, block.CoordinateY);
                //UpdateDrawableObject();
                //label1.Text = "change to pos " + block.CoordinateY + " on" + block.Position.ToString();
            }
        }
    }
}