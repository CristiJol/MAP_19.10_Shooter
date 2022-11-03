using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAP_19._10
{
    public class Enemy
    {
        public double health, speed, damage , sizeX , sizeY , positionX;
        public int spawnTime;
        public Point position;
        

        public Enemy(double health, double speed, double damage,double sizeX , double sizeY , int spawnTime)
        {
            this.health = health;
            this.speed = speed;
            this.damage = damage;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.spawnTime = spawnTime;
            position = Engine.GetRandomPoint((int)sizeX,(int)sizeY);
            positionX = position.X;
        }

        public void Move()
        {
            position.Y += (int) speed;
            sizeX += speed/8;
            sizeY += speed/4;
            positionX -=1.0/8;
            position.X=(int)positionX;
        }

        public void GetShot(Point click)
        {
            if(click.X>position.X && click.X<position.X+sizeX && click.Y>position.Y && click.Y<position.Y+sizeY)
            {
                int x = click.X - position.X;
                int y = click.Y - position.Y;
                Bitmap zombie = new Bitmap((int)sizeX,( int)sizeY);
                Graphics grp = Graphics.FromImage(zombie);
                grp.DrawImage(Engine.form.normalzombie,0,0,(int)sizeX, (int)sizeY);
                if (zombie.GetPixel(x, y).ToArgb()==0)
                    return;
                health -= 20;
                Engine.graphics.DrawString("20", new Font("Arial", 12), new SolidBrush(Color.White), click.X,click.Y-20);
                Engine.form.pictureBox1.Image =Engine.bitmap;

            }
        }

    }
}
