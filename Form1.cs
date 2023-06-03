using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Timer = System.Windows.Forms.Timer;
using demo.Code;

namespace demo
{
    public partial class Form1 : Form
    {
        private Player block;
        private int posY;
        private int posX;
        //private Point startpoint = new Point(50, 200);
        private Point startpoint;
        /*public static Point[,] Points = new Point[3, 3] { 
            { new Point(50, 100), new Point(50, 200), new Point(50, 300) },
            { new Point(350, 100), new Point(350, 200), new Point(350, 300) },
            { new Point(650, 100), new Point(650, 200), new Point(650, 300) } };*/
        

        private List<DrawableObject> dos = new List<DrawableObject>();
        private Transmitter transmitter = new Transmitter(TrackNumber);
        private Timer timer;

        public const int TrackNumber = 3;
        public const int BlockSize = 60;
        public const int BlockSpeed = 10;
        public const int BulletSize = 60;
        public const int BulletWidth = 60;
        public const int BulletHeight = 60;
        public const int BulletSpeed = 5;

        private Random random;
        private BufferedGraphics buffer;

        public Form1()
        {
            InitializeComponent();
            //InitializeGame();
            //DoubleBuffered = true;
            posY = 1;
            posX = 0;
            startpoint = Tool.points[posX, posY];
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

        private void timer_Tick(object sender, EventArgs e)
        {
            if (transmitter.Bullets != null)
            {
                foreach (Bullet bullet in transmitter.Bullets)
                {
                    bullet.Move();
                    //bullet.Draw(panel2.CreateGraphics());
                    if (bullet.Position.X + BulletWidth < 0)
                    {
                        //bullet.Position = new Point(panel2.Width, random.Next(panel2.Height - BulletHeight));
                    }

                    if (bullet.CollidesWith(block))
                    {
                        timer.Stop();
                        MessageBox.Show("Game Over");
                        //InitializeGame();
                        break;
                    }
                }

                DrawGame();
            }
            //UpdateDrawableObject();
            //panel2.Invalidate();
            //block.Draw(panel2.CreateGraphics());
        }

        private void DrawGame()
        {
            buffer.Graphics.Clear(BackColor);
            block.Draw(buffer.Graphics);

            foreach (Bullet bullet in transmitter.Bullets)
            {
                bullet.Draw(buffer.Graphics);
            }

            buffer.Render();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            buffer.Graphics.Clear(BackColor);
            foreach (DrawableObject drawable in dos)
            {
                drawable.Draw(buffer.Graphics);
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
            block = new Player(startpoint, BlockSize, BlockSize, GameImg.Square);
            dos.Add(block);


            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();


            random = new Random();
            //bullets = new Bullet[5];
            //transmitter.Bullets = new Bullet[transmitter.BulletNumber];
            GenerateBullets();
            label1.Text = "start on " + block.Position.ToString();
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
            transmitter.Fire(panel2.Width, posX);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            label1.Text = e.KeyChar.ToString();
            if (e.KeyChar == 'w' && block != null)
            {
                if (posY > 0)
                {
                    posY--;
                }
                block.changepos(posX, posY);
                //UpdateDrawableObject();
                label1.Text = "change to pos " + posY + " on" + block.Position.ToString();
            }
            else if (e.KeyChar == 's' && block != null)
            {
                if (posY < 2)
                {
                    posY++;
                }
                block.changepos(posX, posY);
                //UpdateDrawableObject();
                label1.Text = "change to pos " + posY + " on" + block.Position.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            transmitter.LoadTrack("demo1.txt");
        }

        //private void panel2_MouseClick(object sender, MouseEventArgs e)
        //{
        //    Bullet bullet = new Bullet(panel1.Width, e.Y, 5);
        //    bullet.Draw(panel2.CreateGraphics());
        //    bullets.Add(bullet);
        //    dos.Add(bullet);
        //}

    }
}