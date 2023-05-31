namespace demo
{
    public partial class Form1 : Form
    {
        private Player? tmp;
        int posnum = 1;
        Point[] Points = new Point[3] { new Point(200, 100), new Point(200, 200), new Point(200, 300) };
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tmp = new Player(Points[posnum]);
            tmp.Draw(panel2.CreateGraphics());
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
                tmp.Draw(panel2.CreateGraphics());
                label1.Text = "change to pos" + posnum + "on" + Points[posnum].ToString();
            }
            else if (e.KeyChar == 's' && tmp != null)
            {
                if (posnum < 2)
                {
                    posnum++;
                }
                tmp.changepos(Points[posnum]);
                tmp.Draw(panel2.CreateGraphics());
                label1.Text = "change to pos" + posnum + "on" + tmp.PlayerPosition.ToString();
            }
        }
    }
    public class Player
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
        public void Draw(Graphics g)
        {
            g.Clear(Color.White);
            g.DrawImage(img, PlayerPosition);
        }
    }


}