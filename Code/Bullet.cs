using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Code
{
    class Bullet : DrawableObject
    {
        private int width;
        private int height;
        protected int damagetype;
        protected double posX;

        public Bullet(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
            damagetype = 0;
            posX = position.X;
        }

        public int DamageType
        {
            get { return damagetype; } 
            set {  damagetype = value; }
        }
        public void Move()
        {
            //position.X -= Form1.BulletSpeed;
            posX -= Tool.form.BulletSpeed;
            position.X = (int)posX;
        }

        public bool CollidesWith(Player block)
        {
            Rectangle bulletRect = new Rectangle(position, new Size(Form1.BulletWidth, Form1.BulletHeight));
            Rectangle blockRect = new Rectangle(block.Position, new Size(Form1.BlockSize, Form1.BlockSize));
            return bulletRect.IntersectsWith(blockRect);
        }

        public bool LeaveScreen()
        {
            return position.X + Form1.BulletWidth < 10;
        }

        public bool MeetWith(Player block)
        {
            return block.Position.X < position.X + Form1.BulletWidth 
                && block.Position.X + Form1.BlockSize > position.X;
        }
        public bool Pass(Player block)
        {
            return block.Position.X > position.X + Form1.BulletWidth;
        }

        public override void Draw(Graphics g)
        {
            //g.FillEllipse(Brushes.Red, this.X, this.Y, 10, 10);
            g.DrawImage(image, position.X, position.Y, width, height);
            Tool.form.drawstringonlabel(position.ToString());
        }

        public override void Draw(Graphics g, Image image)
        {
            g.DrawImage(image, position.X, position.Y, width, height);
        }

    }
}
