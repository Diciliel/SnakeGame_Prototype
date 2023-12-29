using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame_Prototype
{
    public abstract class Shapes
    {
        public int posx;
        public int posy;
        public int width;
        public int height;
        public Color color;

        public abstract void Draw(Graphics g);
    }

    public class Head : Shapes
    {
        public int speed;
        public enum direction {right, left, up, down};
        public Point[] points;
        direction direct;
        public Head()
        {
            this.posx = 0;
            this.posy = 0;
            this.width = 20;
            this.height = 20;
            this.speed = 20;
            this.color = Color.DarkGreen;
        }

        public override void Draw(Graphics g)
        {
            Point[] points = new Point[3];

            switch (direct)
            {
                case direction.right:
                    points[0] = new Point(posx + width, posy + height / 2);
                    points[1] = new Point(posx, posy);
                    points[2] = new Point(posx, posy + height);
                    break;

                case direction.left:
                    points[0] = new Point(posx, posy + height / 2);
                    points[1] = new Point(posx + width, posy);
                    points[2] = new Point(posx + width, posy + height);
                    break;

                case direction.up:
                    points[0] = new Point(posx + width / 2, posy);
                    points[1] = new Point(posx, posy + height);
                    points[2] = new Point(posx + width, posy + height);
                    break;

                case direction.down:
                    points[0] = new Point(posx + width / 2, posy + height);
                    points[1] = new Point(posx, posy);
                    points[2] = new Point(posx + width, posy);
                    break;
            }

            g.FillPolygon(new SolidBrush(color), points);
        }

        public void GoRight(int maxWidth) 
        {
            if (direct != direction.left && posx <= maxWidth) { direct = direction.right; }
        }

        public void GoLeft()
        {
            if (direct != direction.right && posx >= 0) { direct = direction.left; }
        }

        public void GoUp()
        {
            if (direct != direction.down && posy >= 0) { direct = direction.up; }
        }

        public void GoDown(int maxHeight)
        {
            if (direct != direction.up && posy <= maxHeight) { direct = direction.down; }
        }
        public void Move(int maxWidth, int maxHeight)
        {
            switch (direct)
            {
                case direction.right:
                    posx += speed;
                    if (posx > maxWidth) posx = 0;
                    break;
                case direction.left:
                    posx -= speed;
                    if (posx < 0) posx = maxWidth;
                    break;
                case direction.up:
                    posy -= speed;
                    if (posy + height < 0) posy = maxHeight;                    
                    break;
                case direction.down:
                    posy += speed;
                    if (posy > maxHeight) posy = 0;
                    break;
            }
        }
    }

    public class Body : Shapes
    {
        public Body()
        {
            this.width = 20;
            this.height = 20;
            this.color = Color.ForestGreen;
        }

        public override void Draw(Graphics g) 
        {
            g.FillRectangle(new SolidBrush(color), posx, posy, width, height);
        }        
    }

    public class Food : Shapes 
    {
        public Food(int posx, int posy)
        {
            this.posx = posx;
            this.posy = posy;
            this.width = 20;
            this.height = 20;
            this.color = Color.Yellow;
        }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), posx, posy, width, height);
        }
    }
}
