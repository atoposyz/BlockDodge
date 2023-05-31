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
        private List<Bullet> bullets = new List<Bullet>();
        private List<DrawableObject> dos = new List<DrawableObject>();
        private Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();

            timer.Interval = 20;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        
        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.Move();
                //bullet.Draw(panel2.CreateGraphics());
            }
            UpdateDrawableObject();
            //panel2.Invalidate();
            //tmp.Draw(panel2.CreateGraphics());
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
            tmp = new Player(Points[posnum]);
            tmp.Draw(panel2.CreateGraphics());
            dos.Add(tmp);
            label1.Text = "start on" + Points[posnum].ToString();
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
                UpdateDrawableObject();
                label1.Text = "change to pos" + posnum + "on" + Points[posnum].ToString();
            }
            else if (e.KeyChar == 's' && tmp != null)
            {
                if (posnum < 2)
                {
                    posnum++;
                }
                tmp.changepos(Points[posnum]);
                UpdateDrawableObject();
                label1.Text = "change to pos" + posnum + "on" + tmp.PlayerPosition.ToString();
            }
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            Bullet bullet = new Bullet(panel1.Width, e.Y, 5);
            bullet.Draw(panel2.CreateGraphics());
            bullets.Add(bullet);
            dos.Add(bullet);
        }

    }
    public abstract class DrawableObject
    {
        public abstract void Draw(Graphics g);
    }
    public class Player: DrawableObject
    {
        public Point PlayerPosition;
        double Width;
        Image img;
        public Player(Point pos)
        {
            PlayerPosition = pos;
            img = GameImg.Square;
            Width = img.Width;
        }
        public void changepos(Point pos)
        {
            PlayerPosition = pos;
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(img, PlayerPosition);
        }
    }

    class Bullet: DrawableObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }

        //public Point PlayerPosition;
        double Width;
        Image img;
        public Bullet(int x, int y, int speed)
        {
            X = x;
            Y = y;
            Speed = speed;
            img = GameImg.Bullet;
            Width = img.Width;
        }

        public void Move()
        {
            X -= Speed;
        }
        public override void Draw(Graphics g)
        {
            //g.FillEllipse(Brushes.Red, this.X, this.Y, 10, 10);
            g.DrawImage(img, new Point(this.X, this.Y));
        }
    }

}