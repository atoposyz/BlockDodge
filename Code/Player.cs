using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Code
{
    public class Player : DrawableObject
    {
        private int width;
        private int height;
        private int coordinateX;
        private int coordinateY;
        private int shieldcapacity = 0;
        private bool bulletignore;
        private bool magnet;
        private bool effectignore;
        private bool fearless;
        private bool timeslack;
        private bool quick;
        private bool win;
        public static Point[,] Points = new Point[3, 3] {
            { new Point(50, 100 - 10), new Point(50, 200 - 10), new Point(50, 300 - 10) },
            { new Point(350, 100 - 10), new Point(350, 200 - 10), new Point(350, 300 - 10) },
            { new Point(650, 100 - 10), new Point(650, 200 - 10), new Point(650, 300 - 10) } };
        public int CoordinateX
        {
            get { return coordinateX; }
            set { coordinateX = value; }
        }
        public int CoordinateY
        {
            get { return coordinateY; }
            set { coordinateY = value; }
        }
        public int ShieldCapacity
        {
            get { return shieldcapacity; }
            set { shieldcapacity = value; }
        }
        public bool BulletIgnore
        {
            get { return bulletignore; }
            set { bulletignore = value; }
        }
        public bool Magnet
        {
            get { return magnet; }
            set { magnet = value; }
        }
        public bool EffectIgnore
        {
            get { return effectignore;}
            set { effectignore = value; }
        }
        public bool Fearless
        {
            get { return fearless; }
            set { fearless = value; }
        }
        public bool Timeslack
        {
            get { return timeslack; }
            set { timeslack = value; }
        }
        public bool Quick
        {
            get { return quick; }
            set { quick = value; }
        }
        public bool Win
        {
            get { return win; }
            set { win = value; }
        }
        public Player(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
        }
        public void reset()
        {
            coordinateX = 0; 
            coordinateY = 1;
            position = Points[0, 1];
            shieldcapacity = 0;
            bulletignore = false;
            magnet = false;
            effectignore = false;
            fearless = false;
            timeslack = false;
            quick = false;
            win = false;
        }
        public void changepos(Point pos)
        {
            position = pos;
        }
        public void changepos(int x, int y)
        {
            coordinateX = x;
            coordinateY = y;
            position = Points[x, y];
        }
        public override void Draw(Graphics g, Image image)
        {
            g.DrawImage(image, position.X, position.Y, width, height);
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(image, position.X, position.Y, width, height);
        }
    }
}
