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
