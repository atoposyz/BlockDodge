﻿using System;
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
        public static Point[,] Points = new Point[3, 3] {
            { new Point(50, 100), new Point(50, 200), new Point(50, 300) },
            { new Point(350, 100), new Point(350, 200), new Point(350, 300) },
            { new Point(650, 100), new Point(650, 200), new Point(650, 300) } };
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

        public Player(Point position, int width, int height, Image image) : base(position, width, height, image)
        {
            this.width = width;
            this.height = height;
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
        public override void Draw(Graphics g)
        {
            g.DrawImage(image, position.X, position.Y, width, height);
        }
    }
}
