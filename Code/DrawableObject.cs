using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Code
{
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
}
