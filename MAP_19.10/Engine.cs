﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAP_19._10
{
    public static class Engine
    {
        public static Form1 form;
        public static int horizon = 280 , wave = 1;
        public static double time = 0;
        public static double fortHealth = 100;
        public static Random random = new Random();
        public static List<Enemy> enemies = new List<Enemy>() , currentWave = new List<Enemy>() ;
        public static List<List<Enemy>> waves = new List<List<Enemy>>();    
        public static Graphics graphics;
        public static Bitmap bitmap;
        public static void Init(Form1 f)
        {
            form = f;
            bitmap=new Bitmap(form.Width, form.Height);
            graphics= Graphics.FromImage(bitmap);

            var wave1 =new List<Enemy>();
            wave1.Add(new Enemy(100, random.Next(1,10), 20, 70, 140, 0));
            wave1.Add(new Enemy(100, random.Next(1,10), 20, 70, 140, 20));
            wave1.Add(new Enemy(100, random.Next(1,10), 20, 70, 140, 35));
            wave1.Add(new Enemy(100, random.Next(1,10), 20, 70, 140, 45));
            wave1.Add(new Enemy(100, random.Next(1,10), 20, 70, 140, 55));
            

            var wave2 = new List<Enemy>();
            wave2.Add(new Enemy(100, random.Next(1, 10), 20, 70, 140, 0));
            wave2.Add(new Enemy(100, random.Next(1, 10), 20, 70, 140, 10));
            wave2.Add(new Enemy(100, random.Next(1, 10), 20, 70, 140, 15));
            wave2.Add(new Enemy(100, random.Next(1, 10), 20, 70, 140, 20));
            wave2.Add(new Enemy(100, random.Next(1, 10), 20, 70, 140, 25));
            wave2.Add(new Enemy(100, random.Next(1, 10), 20, 70, 140, 30));
            wave2.Add(new Enemy(100, random.Next(1, 10), 20, 70, 140, 35));
            wave2.Add(new Enemy(100, random.Next(1, 10), 20, 70, 140, 40));
            wave2.Add(new Enemy(100, random.Next(1, 10), 20, 70, 140, 45));
            wave2.Add(new Enemy(100, random.Next(1, 10), 20, 70, 140, 50));

            waves.Add(wave1);
            waves.Add(wave2);
            currentWave = wave1;
        }
        public static void Tick()
        {
            time++;
            form.TimeLabel.Text = $"{(float)time /10}";

            if (!currentWave.Any() && !enemies.Any())
            {
                if (wave < waves.Count)
                    NextWave();
                else
                    Win();
               
            }
            if (currentWave.Any() && currentWave[0].spawnTime <= time)
            {
                enemies.Add(currentWave[0]);
                currentWave.RemoveAt(0);
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy =enemies[i];
                enemy.Move();
                if(enemy.position.Y >= form.Height)
                {
                    fortHealth -= enemy.damage;
                    form.HealthLabel.Text = $"Health {fortHealth}";
                    enemies.Remove(enemies[i]);
                    i--;
                }
            }
            if(fortHealth <= 0)
            {
                form.timer1.Enabled = false;
                MessageBox.Show("AI PIEDUT !!! AHHAHAHAAA!");
                form.Close();
            }

            UpdateDisplay();
        }
        public static void Shoot(Point click)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].GetShot(click);

                if (enemies[i].health<=0)
                {
                    enemies.Remove(enemies[i]);
                    i--;
                }
            }
            
        }
        public static void NextWave()
        {
            wave++;
            currentWave = waves[wave-1];
            time = 0;
            form.WaveLabel.Text = $"Wave {wave}";

        }
        public static Point GetRandomPoint(int sizeX , int sizeY)
        {
            return new Point(random.Next(form.Width - sizeX), horizon -sizeY);
        }

        public static void UpdateDisplay()
        {
            graphics.DrawImage(form.background,0,0,form.Width,form.Height);
            enemies.Sort((e1,e2)=>e1.position.Y-e2.position.Y);
            foreach (Enemy enemy in enemies)
            {
                graphics.DrawImage(form.normalzombie, enemy.position.X, enemy.position.Y, (int)enemy.sizeX, (int)enemy.sizeY);
            }
            form.pictureBox1.Image=bitmap;
        }
        public static void Win()
        {
            form.timer1.Enabled = false;
            form.backgroundSound.Stop();
            MessageBox.Show("N-AI MAI LOAT TZEAPA FRAERE ?", "CICA AI CASTIGAT...");
            form.Close();
        }
        
    }
}
