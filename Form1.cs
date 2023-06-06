using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Timer = System.Windows.Forms.Timer;
using demo.Code;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Text;
using System.Media;

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

        private int mode = 0;
        private List<DrawableObject> dos = new List<DrawableObject>();
        private Transmitter transmitter;
        private Timer timer;
        System.Media.SoundPlayer sp = new SoundPlayer();
        public bool pause;
        private bool firststart;

        public const int TrackNumber = 3;
        public const int BlockSize = 60;

        public const int BlockWidth = 49;
        public const int BlockHeight = 80;

        public const int BlockSpeed = 10;
        public const int BulletSize = 60;
        public const int BulletWidth = 48;
        public const int BulletHeight = 48;



        public double BulletSpeed = Tool.BULLETSPEED;
        public const int TrackLength = 200;

        private Random random;
        private BufferedGraphics buffer;
        private int _score = 100; // 记录用户得分
        private string userName;
        private bool ifRecord = false;
        private int cnt = 3;
        private int xPos = 0;
        private Thread uiThread;
        public Form1()
        {
            InitializeComponent();
            uiThread = new Thread(DrawGame);
            this.KeyPreview = true;
            //InitializeGame();
            //DoubleBuffered = true;
            //posY = 1;
            //posX = 0;
            startpoint = Player.Points[0, 1];
            block = new Player(startpoint, BlockWidth, BlockHeight, GameImg.Square);
            block.CoordinateX = 0;
            block.CoordinateY = 1;
            transmitter = new Transmitter(TrackNumber, panel2.Width);
            Tool tool = new Tool(this, block, transmitter);

            pause = true;
            firststart = true;

            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += new EventHandler(timer_Tick);

            transmitter.LoadTrack("demo2.txt");

            sp.SoundLocation = @"../../../Resources/music.wav"; //音乐文件


            //panel2.Resize += new EventHandler(panel2_Resize);
            buffer = BufferedGraphicsManager.Current.Allocate(panel2.CreateGraphics(), panel2.DisplayRectangle);
            uiThread.Start();
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
                    transmitter.Bullets2[No_] = null;
                    UpdateTimesEffect();
                }
                else if (block.ShieldCapacity > 0)
                {
                    block.ShieldCapacity--;
                    transmitter.Bullets2[No_] = null;
                    UpdateTimesEffect();
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
                    transmitter.Bullets2[No_] = null;
                }
                else
                {
                    ((BUFF)bullet).CauseEffect(block);
                    UpdateTimesEffect();
                    transmitter.Bullets2[No_] = null;
                }
            }
            else if (bullet.DamageType == 2)
            {
                if (block.EffectIgnore == true)
                {
                    transmitter.Bullets2[No_] = null;
                }
                else
                {
                    ((DEBUFF)bullet).CauseEffect(block);
                    UpdateTimesEffect();
                    transmitter.Bullets2[No_] = null;
                }
            }
            return false;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (block.Win == true)
            {
                reset();
                timer.Stop();
                MessageBox.Show("You Win!");
                if (ifRecord == true)    //如果开始的时候输入了用户名，就更新排行榜
                {
                    Form2.AddOrUpdateRankItem(userName, _score);
                }
                return;
            }
            if (transmitter.Bullets2 != null)
            {
                for (int i = 0; i < transmitter.Bullets2.Count; i++)
                {
                    Bullet bullet = transmitter.Bullets2[i];
                    if (bullet == null) continue;
                    bullet.Move();
                    //bullet.Draw(panel2.CreateGraphics());


                    if (bullet.LeaveScreen())
                    {
                        transmitter.Bullets2[i] = null;
                        continue;
                        //bullet.Position = new Point(panel2.Width, random.Next(panel2.Height - BulletHeight));
                    }
                    //有磁铁效果时判断一下有没有经过BUFF，经过就自动吃掉BUFF
                    if (block.Magnet == true && bullet.MeetWith(block) && bullet.DamageType == 1)
                    {
                        ((BUFF)bullet).CauseEffect(block);
                        UpdateTimesEffect();
                        transmitter.Bullets2[i] = null;
                        continue;
                    }
                    if (bullet.CollidesWith(block))
                    {
                        bool flag = CollidesJudge(i, bullet);
                        if (flag == true)
                        {
                            reset();
                            timer.Stop();
                            MessageBox.Show("Game Over");
                            break;
                        }
                    }
                }

                //DrawGame();
            }
            UpdateContinuousEffect();
        }
        /*
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
                    if (bullet.Pass(block) && i == transmitter.Bullets.Length - 1)
                    {//TODO   BUG!!!!!!!
                        timer.Stop();
                        MessageBox.Show("You Win!");
                        if (ifRecord == true)    //如果开始的时候输入了用户名，就更新排行榜
                        {
                            Form2.AddOrUpdateRankItem(userName, _score);
                        }
                        break;
                    }
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
                        UpdateTimesEffect();
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
            UpdateContinuousEffect();
            //UpdateDrawableObject();
            //panel2.Invalidate();
            //block.Draw(panel2.CreateGraphics());
        }*/

        public void UpdateTimesEffect()
        {
            shieldtext.Text = block.ShieldCapacity.ToString();

        }
        public void UpdateContinuousEffect()
        {
            magnetlimit.Text = (Tool.MagnetTime / 1000).ToString() + 's';
            timeslacklimit.Text = (Tool.TimeslackTime / 1000).ToString() + "s";
            invincibilitylimit.Text = (Tool.InvincibilityTime / 1000).ToString() + "s";
            sprintlimit.Text = (Tool.SprintTime / 1000).ToString() + "s";
            fearlesslimit.Text = (Tool.FearlessTime / 1000).ToString() + "s";
            quicklimit.Text = (Tool.QuickTime / 1000).ToString() + "s";
        }
        private void DrawGame()
        {
            while (true)
            {
                if (firststart == false && pause == true)
                {
                    buffer.Graphics.DrawString("PAUSE", new Font("Segoe UI", 80), new SolidBrush(Color.Black), new Point(200, 100));
                    buffer.Render();
                }
                else if (firststart == false)
                {
                    xPos -= 5;
                    if (xPos < -4154)
                    {
                        xPos = 0;
                    }
                    buffer.Graphics.Clear(BackColor);
                    buffer.Graphics.DrawImage(GameImg.background_all, xPos, 0);
                    string imagePath = @"../../../Resources/mod_00" + cnt.ToString() + ".png";
                    cnt = (cnt - 3 + 1) % 23 + 3;
                    //string imagePath = "Resources\\mod_000" + cnt.ToString() + ".png";
                    using (Image image = Image.FromFile(imagePath))
                    {
                        Image imageTMP = new Bitmap(image, BlockWidth, BlockHeight);
                        block.Draw(buffer.Graphics, imageTMP);
                    }
                    //Image image = Image.FromFile(imagePath);
                    //Image imageTMP = new Bitmap(image, BlockWidth, BlockHeight);
                    //block.Draw(buffer.Graphics, imageTMP);

                    for (int i = transmitter.Bullets2.Count - 1; i >= 0; i--)
                    {
                        if (transmitter.Bullets2[i] != null)
                        {
                            transmitter.Bullets2[i].Draw(buffer.Graphics);
                        }
                    }
                    transmitter.Bullets2.RemoveAll(s => s == null);
                    buffer.Render();
                }
                Thread.Sleep(8);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(GameImg.background_all, 0, 0);
            string imagePath = @"../../../Resources/mod_003.png";
            using (Image image = Image.FromFile(imagePath))
            {
                // 游戏绘制逻辑
                Image imageTMP = new Bitmap(image, BlockWidth, BlockHeight);
                block.Draw(e.Graphics, imageTMP);
            }
            //buffer.Graphics.Clear(BackColor);
            //buffer.Graphics.DrawImage(GameImg.background_all, 0, 0);
            //ControlPaint.DrawBorder(e.Graphics, panel2.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        //private void panel2_Paint2(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    g.DrawImage(new Bitmap(GameImg.tmp), 0, 0);
        //    // 
        //    // label1
        //    // 
        //    label1.AutoSize = true;
        //}

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
            if (pause == true)
            {
                pause = false;
                timer.Start();
                button1.Text = "暂停";

            }
            else if (pause == false)
            {
                pause = true;
                timer.Stop();

                button1.Text = "继续";
            }
            if (firststart == true)
            {
                if (mode == 0)
                {
                    button1.Enabled = false;
                }
                button3.Enabled = false;
                firststart = false;
                sp.PlayLooping();
                dos.Add(block);
                random = new Random();

                Tool.TransmitterTimer.Start();
                //GenerateBullets();
            }
            //bullets = new Bullet[5];
            //transmitter.Bullets = new Bullet[transmitter.BulletNumber];
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
            userName = ShowUserNameDialog();
            if (!string.IsNullOrEmpty(userName))
            {
                ifRecord = true;
            }
        }
        private string ShowUserNameDialog()
        {
            return Microsoft.VisualBasic.Interaction.InputBox("请输入您的用户名：", "提示", "");
        }

        //private void panel2_MouseClick(object sender, MouseEventArgs e)
        //{
        //    Bullet bullet = new Bullet(panel1.Width, e.Y, 5);
        //    bullet.Draw(panel2.CreateGraphics());
        //    bullets.Add(bullet);
        //    dos.Add(bullet);
        //}
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (pause == true)
            {
                return;
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 rankForm = new Form2();
            rankForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mode == 0)
            {
                mode = 1;
                label1.Text = "无尽模式";
                Tool.BULLETSPEED = 5;
                BulletSpeed = 5;
                transmitter.Interval = 1500;
                sp.SoundLocation = @"../../../Resources/endless.wav";
                transmitter.LoadRandomTrack(TrackLength);
            }
            else if (mode == 1)
            {
                mode = 0;
                label1.Text = "闯关模式";
                Tool.BULLETSPEED = 12.5;
                BulletSpeed = 12.5;
                transmitter.Interval = 500;
                sp.SoundLocation = @"../../../Resources/music.wav";
                transmitter.LoadTrack("demo2.txt");
            }
        }

        public void reset()
        {
            sp.Stop();
            panel2.CreateGraphics().Clear(BackColor);
            firststart = true;
            button1.Text = "开始游戏";
            button3.Enabled = true;
            transmitter.Reset();
            Tool.reset();
            block.reset();
            pause = true;
            UpdateTimesEffect();
            UpdateContinuousEffect();
            if (mode == 0)
            {
                button1.Enabled = true;
                transmitter.Interval = 500;
                transmitter.LoadTrack("demo2.txt");
            }
            else if (mode == 1)
            {
                transmitter.Interval = 1500;
                transmitter.LoadRandomTrack(TrackLength);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

