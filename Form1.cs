using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Timer = System.Windows.Forms.Timer;
using demo.Code;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Text;
using System.Media;
using System.Reflection.Emit;
using System.Diagnostics;

namespace demo
{
    public partial class Form1 : Form
    {

        private Player block;
        private Point startpoint;

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

        //以下为UI部分的变量
        Panel mainpanel;
        PictureBox backgroundpic;
        System.Windows.Forms.Label helppara;
        System.Windows.Forms.Label helppara2;
        Button helpbutton;
        Button levelbutton;
        Button randombutton;
        Button quitbutton;
        int ifhelp;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(1254, 777);
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
            shieldlabel.Font = new Font("Microsoft YaHei UI", 12);
            magnetlabel.Font = new Font("Microsoft YaHei UI", 12);
            defenselabel.Font = new Font("Microsoft YaHei UI", 12);
            timeslacklabel.Font = new Font("Microsoft YaHei UI", 12);
            invincibilitylabel.Font = new Font("Microsoft YaHei UI", 12);
            purelabel.Font = new Font("Microsoft YaHei UI", 12);
            sprintlabel.Font = new Font("Microsoft YaHei UI", 12);
            bravelabel.Font = new Font("Microsoft YaHei UI", 12);
            fearlesslabel.Font = new Font("Microsoft YaHei UI", 12);
            goodlucklabel.Font = new Font("Microsoft YaHei UI", 12);
            quicklabel.Font = new Font("Microsoft YaHei UI", 12);
            nightwalklabel.Font = new Font("Microsoft YaHei UI", 12);
            shieldtext.Font = new Font("Microsoft YaHei UI", 12);
            magnetlimit.Font = new Font("Microsoft YaHei UI", 12);
            timeslacklimit.Font = new Font("Microsoft YaHei UI", 12);
            invincibilitylimit.Font = new Font("Microsoft YaHei UI", 12);
            sprintlimit.Font = new Font("Microsoft YaHei UI", 12);
            fearlesslimit.Font = new Font("Microsoft YaHei UI", 12);
            quicklimit.Font = new Font("Microsoft YaHei UI", 12);
            nightwalklimit.Font = new Font("Microsoft YaHei UI", 12);
            label2.Font = new Font("Microsoft YaHei UI", 12);

            pause = true;
            firststart = true;

            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += new EventHandler(timer_Tick);

            transmitter.LoadTrack("demo2.txt");

            sp.SoundLocation = Tool.debugdot + @"Resources/music.wav"; //音乐文件


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
            Debug.Assert(bullet != null);
            if (bullet.DamageType == 0)  //如果是子弹
            {
                if (block.BulletIgnore == true)
                {
                    //transmitter.Bullets2[No_] = null;
                    transmitter.Bullets[No_] = null;
                    UpdateTimesEffect();
                }
                else if (block.ShieldCapacity > 0)
                {
                    block.ShieldCapacity--;
                    //transmitter.Bullets2[No_] = null;
                    transmitter.Bullets[No_] = null;
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
                    //transmitter.Bullets2[No_] = null;
                    transmitter.Bullets[No_] = null;
                }
                else
                {
                    ((BUFF)bullet).CauseEffect(block);
                    UpdateTimesEffect();
                    //transmitter.Bullets2[No_] = null;
                    transmitter.Bullets[No_] = null;
                }
            }
            else if (bullet.DamageType == 2)
            {
                if (block.EffectIgnore == true)
                {
                    //transmitter.Bullets2[No_] = null;
                    transmitter.Bullets[No_] = null;
                }
                else
                {
                    ((DEBUFF)bullet).CauseEffect(block);
                    UpdateTimesEffect();
                    //transmitter.Bullets2[No_] = null;
                    transmitter.Bullets[No_] = null;
                }
            }
            else if(bullet.DamageType == 3)
            {
                ((BUFF)bullet).CauseEffect(block);
                block.Win = true;
            }
            return false;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (block.Win == true)
            {
                if (ifRecord == true)    //如果开始的时候输入了用户名，就更新排行榜
                {
                    Form2.AddOrUpdateRankItem(userName, Tool.score);
                }
                block.Win = false;
                reset();
                timer.Stop();
                MessageBox.Show("You Win!");
                return;
            }
            //if (transmitter.Bullets2 != null)
            if (transmitter.Bullets != null)
            {//Bullets!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                for (int i = 0; i < transmitter.Bullets.Length; i++)
                {
                    Bullet bullet = transmitter.Bullets[i];
                    if (bullet == null) continue;
                    bullet.Move();
                    //bullet.Draw(panel2.CreateGraphics());
                    Debug.Assert(bullet != null);

                    if (bullet.LeaveScreen())
                    {
                        //Debug.Assert(false);
                        transmitter.Bullets[i] = null;
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
                    Debug.Assert(bullet != null);
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
                _score = block.ShieldCapacity;

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
            if (Tool.MagnetTime / 1000 > 0)
            {
                magnetlabel.BackColor = Color.LightGreen;
            }
            else
            {
                magnetlabel.BackColor = BackColor;
            }
            if (Tool.TimeslackTime / 1000 > 0)
            {
                timeslacklabel.BackColor = Color.LightGreen;
            }
            else
            {
                timeslacklabel.BackColor = BackColor;
            }
            if (Tool.InvincibilityTime / 1000 > 0)
            {
                invincibilitylabel.BackColor = Color.LightGreen;
            }
            else
            {
                invincibilitylabel.BackColor = BackColor;
            }
            if (Tool.SprintTime / 1000 > 0)
            {
                sprintlabel.BackColor = Color.LightGreen;
            }
            else
            {
                sprintlabel.BackColor = BackColor;
            }
            if (Tool.FearlessTime / 1000 > 0)
            {
                fearlesslabel.BackColor = Color.OrangeRed;
            }
            else
            {
                fearlesslabel.BackColor = BackColor;
            }
            if (Tool.QuickTime / 1000 > 0)
            {
                quicklabel.BackColor = Color.OrangeRed;
            }
            else
            {
                quicklabel.BackColor = BackColor;
            }
            if (Tool.DefenseTime / 100 > 0)
            {
                defenselabel.BackColor = Color.LightGreen;
            }
            else
            {
                defenselabel.BackColor = BackColor;
            }
            if (Tool.PureTime / 100 > 0)
            {
                purelabel.BackColor = Color.LightGreen;
            }
            else
            {
                purelabel.BackColor = BackColor;
            }
            if (Tool.BraveTime / 100 > 0)
            {
                bravelabel.BackColor = Color.OrangeRed;
            }
            else
            {
                bravelabel.BackColor = BackColor;
            }
            if (Tool.GoodluckTime / 100 > 0)
            {
                goodlucklabel.BackColor = Color.OrangeRed;
            }
            else
            {
                goodlucklabel.BackColor = BackColor;
            }
            if(Tool.ShieldTime / 100 > 0)
            {
                shieldlabel.BackColor = Color.LightGreen;
            }
            else
            {
                shieldlabel.BackColor = BackColor;
            }
            magnetlimit.Text = (Tool.MagnetTime / 1000).ToString() + 's';
            timeslacklimit.Text = (Tool.TimeslackTime / 1000).ToString() + "s";
            invincibilitylimit.Text = (Tool.InvincibilityTime / 1000).ToString() + "s";
            sprintlimit.Text = (Tool.SprintTime / 1000).ToString() + "s";
            fearlesslimit.Text = (Tool.FearlessTime / 1000).ToString() + "s";
            quicklimit.Text = (Tool.QuickTime / 1000).ToString() + "s";
            scores.Text = Tool.score.ToString();
        }
        private void DrawGame()
        {
            while (true)
            {
                if (firststart == false && pause == true)
                {
                    buffer.Graphics.DrawString("PAUSE", new Font("Segoe UI", 80), new SolidBrush(Color.Black), new Point(300, 200));
                    buffer.Render();
                }
                else if (firststart == false)
                {
                    xPos += 5;
                    if (xPos > 4156)
                    {
                        xPos = 0;
                    }
                    buffer.Graphics.Clear(BackColor);

                    using (FileStream stream = new FileStream(Tool.debugdot + @"Resources/background_all.png", FileMode.Open, FileAccess.Read))
                    {
                        using (Image image = Image.FromStream(stream))
                        {
                            Rectangle cropArea = new Rectangle(xPos, 0, 1039, 640);
                            using (Bitmap croppedBitmap = new Bitmap(cropArea.Width, cropArea.Height))
                            {
                                using (Graphics g = Graphics.FromImage(croppedBitmap))
                                {
                                    g.DrawImage(image, new Rectangle(0, 0, cropArea.Width, cropArea.Height), cropArea, GraphicsUnit.Pixel);
                                }

                                buffer.Graphics.DrawImage(croppedBitmap, 0, 0);
                            }
                        }
                    }


                    //buffer.Graphics.DrawImage(GameImg.background_all, xPos, 0);
                    string imagePath = Tool.debugdot + @"Resources/mod_00" + cnt.ToString() + ".png";
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
                    //foreach (var bullet in transmitter.Bullets2)
                    foreach (var bullet in transmitter.Bullets)
                    {
                        if (bullet != null)
                        {
                            bullet.Draw(buffer.Graphics);
                        }
                    }
                    /*for (int i = transmitter.Bullets2.Count - 1; i >= 0; i--)
                    {

                        if (transmitter.Bullets2[i] != null)
                        {
                            transmitter.Bullets2[i].Draw(buffer.Graphics);
                        }
                    }*/

                    //transmitter.Bullets2.RemoveAll(s => s == null);这里注释掉了！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
                    buffer.Render();
                }
                Thread.Sleep(8);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(GameImg.background_all, 0, 0);
            string imagePath = Tool.debugdot + @"Resources/mod_003.png";
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
                if(firststart == false)
                {
                    Tool.EffectResume();
                }
                
            }
            else if (pause == false)
            {
                pause = true;
                timer.Stop();
                Tool.EffectPause();
                button1.Text = "继续";
            }
            if (firststart == true)
            {
                if (mode == 0)
                {
                    button1.Enabled = false;
                }
                //button3.Enabled = false;
                firststart = false;
                sp.PlayLooping();
                dos.Add(block);
                random = new Random();
                Tool.MainTimer.Start();
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
            tableLayoutPanel1.Visible = false;

            mainpanel = new Panel()
            {
                Size = new Size(1254, 777),
                BackColor = Color.White
            };

            backgroundpic = new PictureBox()
            {
                Size = new Size(1254, 777),
                Image = GameImg.MainpagePicture,
                Location = new Point(0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            levelbutton = new Button()
            {
                Text = "关卡模式",
                Font = new Font("方正粗雅宋_GBK", 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(100, 200),
                //AutoSize = true,
                Width = 200,
                Height = 70,/*
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent*/
            };

            randombutton = new Button()
            {
                Text = "随机模式",
                Font = new Font("方正粗雅宋_GBK", 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(100, 300),
                //AutoSize = true,
                Width = 200,
                Height = 70,/*
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent*/
            };

            helpbutton = new Button()
            {
                Text = "帮助",
                Font = new Font("方正粗雅宋_GBK", 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(100, 400),
                //AutoSize = true,
                Width = 200,
                Height = 70,/*
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent*/
            };

            quitbutton = new Button()
            {
                Text = "退出",
                Font = new Font("方正粗雅宋_GBK", 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(100, 500),
                //AutoSize = true,
                Width = 200,
                Height = 70,/*
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.Red*/
            };

            helppara = new System.Windows.Forms.Label()
            {
                Text = "\t又是平常的一天，德克萨斯身为企鹅物流的员工，又要去派送快递了！\n\n但是今天的路上依旧不那么太平啊，就像是误入了什么游戏一样，一把把刀剑从前面飞来！\n\n作为一名尖兵，躲过这些自然是易如反掌，但是有时候也会遇到不明确的BUFF与DEBUFF，让人措手不及！\n\n请你和德克萨斯合作，将快递安全送到目的地吧！\r\n\n\t使用W向上移动，S向下移动。\n\n作者：Aidan, Xuanfly\n\nSpecial Thanks：GGN_2015",
                Font = new Font("黑体", 13),
                Location = new Point(100, 100),
                BackColor = Color.FromArgb(200, 255, 255, 255),
                AutoSize = true,
                MaximumSize = new Size(600, 0),
                Visible = false,
            };
            helppara2 = new System.Windows.Forms.Label()
            {
                Text = "BUFF\n\n1. 护盾SHIELD。具有层数，可抵消一次子弹。\r\n2. 磁铁MAGNET。具有持续时间，在持续时间内可以吸收经过的 3 条赛道的 BUFF。\r\n3. 回防DEFENSE。一次性效果，若方块后方有位置，则退到当前赛道的后一个位置，并无敌小段时间。\r\n4. 缓时TIMESLACK。具有持续时间，持续时间内发射物速度变慢。\r\n5. 无敌INVINCIBILITY。具有持续时间，持续时间内抵消所有子弹和特殊道具。\r\n6. 净化PURE。一次性效果，清空所有已获得的特殊效果。\r\n7. 冲刺SPRINT。具有持续时间，持续时间内获得磁铁和无敌并加速游戏时间。\n\nDEBUFF\n\n1. 勇猛BRAVE。一次性效果，若方块前方有位置，则前进到当前赛道的前一个位置，并无敌小段时间。\r\n2. 无畏FEARLESS。具有持续时间，持续时间内子弹数量变多。\r\n3. 强运GOODLUCK。一次性效果，立刻发射 3 个并排的未知效果。\r\n4. 迅捷QUICK。具有持续时间，持续时间内发射物速度变快。\r\n5. 夜行NIGHTWALK。具有持续时间，持续时间内游戏画面黑白闪烁。",
                Font = new Font("黑体", 13),
                Location = new Point(50, 20),
                BackColor = Color.FromArgb(200, 255, 255, 255),
                AutoSize = true,
                MaximumSize = new Size(950, 0),
                Visible = false,
            };
            ifhelp = 0;

            levelbutton.Click += levelbutton_click;
            randombutton.Click += randombutton_click;
            helpbutton.Click += helpbutton_click;
            quitbutton.Click += quitbutton_click;

            backgroundpic.Controls.Add(helppara);
            backgroundpic.Controls.Add(helppara2);
            backgroundpic.Controls.Add(levelbutton);
            backgroundpic.Controls.Add(randombutton);
            backgroundpic.Controls.Add(quitbutton);
            backgroundpic.Controls.Add(helpbutton);
            mainpanel.Controls.Add(backgroundpic);

            this.Controls.Add(mainpanel);

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
            mainpanel.Visible = true;
            tableLayoutPanel1.Visible = false;
            reset();
            /*if (mode == 0)
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
            }*/
        }

        public void reset()
        {
            sp.Stop();
            panel2.CreateGraphics().Clear(BackColor);
            panel2.Invalidate();
            firststart = true;
            button1.Text = "开始游戏";
            transmitter.Reset();
            Tool.reset();
            block.reset();
            pause = true;
            block.Win = false;
            UpdateTimesEffect();
            UpdateContinuousEffect();
            timer.Stop();
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
            System.Environment.Exit(0);
        }
        private void levelbutton_click(object sender, EventArgs e)
        {
            mode = 0;
            Tool.BULLETSPEED = 12.5;
            BulletSpeed = 12.5;
            transmitter.Interval = 500;
            Tool.INTERVAL = 500;
            sp.SoundLocation = Tool.debugdot + @"Resources/music.wav";
            transmitter.LoadTrack("demo2.txt");
            mainpanel.Visible = false;
            tableLayoutPanel1.Visible = true;
        }
        private void randombutton_click(object sender, EventArgs e)
        {
            mode = 1;
            Tool.BULLETSPEED = 5;
            BulletSpeed = 5;
            transmitter.Interval = 1500;
            Tool.INTERVAL = 1500;
            sp.SoundLocation = Tool.debugdot + @"Resources/endless.wav";
            transmitter.LoadRandomTrack(TrackLength);
            mainpanel.Visible = false;
            tableLayoutPanel1.Visible = true;
        }
        private void helpbutton_click(object sender, EventArgs e)
        {
            if (ifhelp == 0)
            {
                ifhelp = 1;
                helpbutton.Text = "下一页";
                helpbutton.Location = new Point(100, 600);
                helppara.Visible = true;
                helppara2.Visible = false;
                levelbutton.Visible = false;
                randombutton.Visible = false;
                quitbutton.Visible = false;
            }
            else if (ifhelp == 1)
            {
                ifhelp = 2;
                helpbutton.Text = "返回";
                helppara.Visible = false;
                helppara2.Visible = true;
            }
            else
            {
                ifhelp = 0;
                helpbutton.Text = "帮助";
                helpbutton.Location = new Point(100, 400);
                helppara.Visible = false;
                helppara2.Visible = false;
                levelbutton.Visible = true;
                randombutton.Visible = true;
                quitbutton.Visible = true;
            }

        }
        private void quitbutton_click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

    }
}

