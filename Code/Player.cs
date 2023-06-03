using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Code
{
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
}
