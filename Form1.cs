using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Timer = System.Windows.Forms.Timer;

namespace demo
{
    public partial class Form1 : Form
    {
        private Player? tmp;
        int posnum = 1;
        Point[] Points = new Point[3] { new Point(200, 100), new Point(200, 200), new Point(200, 300) };
        private Bullet[] bullets;

        private List<DrawableObject> dos = new List<DrawableObject>();
        private Timer timer;

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

            //panel2.Resize += new EventHandler(panel2_Resize);
            buffer = BufferedGraphicsManager.Current.Allocate(panel2.CreateGraphics(), panel2.DisplayRectangle);

        }

        //private void InitializeGame()
        //{
        //    //buffer.Graphics.Clear(BackColor);
        //    tmp = new Player(Points[posnum], BlockSize, BlockSize, GameImg.Square);
        //    dos.Add(tmp);
        //    timer = new Timer();
        //    timer.Interval = 20;
        //    timer.Tick += new EventHandler(timer_Tick);
        //    timer.Start();

        //}

        private void timer_Tick(object sender, EventArgs e)
        {
            if (bullets != null)
            {
                foreach (Bullet bullet in bullets)
                {
                    bullet.Move();
                    //bullet.Draw(panel2.CreateGraphics());
                    if (bullet.Position.X + BulletWidth < 0)
                    {
                        bullet.Position = new Point(panel2.Width, random.Next(panel2.Height - BulletHeight));
                    }

                    if (bullet.CollidesWith(tmp))
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
            //tmp.Draw(panel2.CreateGraphics());
        }

        private void DrawGame()
        {
            buffer.Graphics.Clear(BackColor);
            tmp.Draw(buffer.Graphics);

            foreach (Bullet bullet in bullets)
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
            tmp = new Player(Points[posnum], BlockSize, BlockSize, GameImg.Square);
            dos.Add(tmp);


            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            

            random = new Random();
            bullets = new Bullet[5];
            GenerateBullets();
            label1.Text = "start on" + Points[posnum].ToString();
        }

        private void GenerateBullets()
        {
            int startY = random.Next(panel2.Height - BulletHeight);
            for (int i = 0; i < bullets.Length; i++)
            {
                int startX = random.Next(panel2.Width, panel2.Width + 200);
                bullets[i] = new Bullet(new Point(startX, startY), BulletWidth, BulletHeight, GameImg.Bullet);
                startY += BulletHeight * 2;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            label1.Text = e.KeyChar.ToString();
            if (e.KeyChar == 'w' && tmp != null)
            {
                if (posnum > 0)
                {
                    posnum--;
                }
                tmp.changepos(Points[posnum]);
                //UpdateDrawableObject();
                label1.Text = "change to pos" + posnum + "on" + Points[posnum].ToString();
            }
            else if (e.KeyChar == 's' && tmp != null)
            {
                if (posnum < 2)
                {
                    posnum++;
                }
                tmp.changepos(Points[posnum]);
                //UpdateDrawableObject();
                label1.Text = "change to pos" + posnum + "on" + tmp.position.ToString();
            }
        }

        //private void panel2_MouseClick(object sender, MouseEventArgs e)
        //{
        //    Bullet bullet = new Bullet(panel1.Width, e.Y, 5);
        //    bullet.Draw(panel2.CreateGraphics());
        //    bullets.Add(bullet);
        //    dos.Add(bullet);
        //}

    }
    public abstract class DrawableObject
    {
        public Point position;
        protected Image image;

        public DrawableObject(Point position, int width, int height, Image image)
        {
            this.position = position;
            this.image = new Bitmap(image, width, height);
        }

        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        public abstract void Draw(Graphics g);
    }
    public class Player : DrawableObject
    {
        //public Point PlayerPosition;
        //double Width;
        //Image img;
        private int width;
        private int height;

        public Player(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            //PlayerPosition = pos;
            //    img = GameImg.Square;
            //    Width = img.Width;
            this.width = width;
            this.height = height;
        }

        public void changepos(Point pos)
        {
            position = pos;
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(image, position.X, position.Y, width, height);
        }
    }

    class Bullet : DrawableObject
    {
        private int width;
        private int height;

        public Bullet(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
        }


        public void Move()
        {
            position.X -= Form1.BulletSpeed;
        }

        public bool CollidesWith(Player block)
        {
            Rectangle bulletRect = new Rectangle(position, new Size(Form1.BulletWidth, Form1.BulletHeight));
            Rectangle blockRect = new Rectangle(block.Position, new Size(Form1.BlockSize, Form1.BlockSize));
            return bulletRect.IntersectsWith(blockRect);
        }

        public override void Draw(Graphics g)
        {
            //g.FillEllipse(Brushes.Red, this.X, this.Y, 10, 10);
            g.DrawImage(image, position.X, position.Y, width, height);
        }

    }

}