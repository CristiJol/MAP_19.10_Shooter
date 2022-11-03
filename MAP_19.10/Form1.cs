using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace MAP_19._10
{
    public partial class Form1 : Form
    {
        public Image background = Image.FromFile("../../Images/OV1c2ZB.jpg");
        public Image normalzombie = Image.FromFile("../../Images/zombie.png");
        public Image sabie = Image.FromFile("../../Images/Sword.png");
        public SoundPlayer backgroundSound = new SoundPlayer("../../Sounds/Panzerkampf.wav");
        Bitmap bm = new Bitmap(new Bitmap("../../Images/Sword.png"), 80, 160);


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Width = this.Width;
            pictureBox1.Height = this.Height;
            

            TimeLabel.Parent =WaveLabel.Parent = HealthLabel.Parent = pictureBox1;
            

            //Cursor.Current = new Cursor(GetType(), "../../Images/Sword.png");
            pictureBox1.Cursor=new Cursor(bm.GetHicon());
            this.Cursor = Cursors.WaitCursor;
            backgroundSound.PlayLooping();
            Engine.Init(this);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                timer1.Enabled = false;
                backgroundSound.Stop();
                pictureBox1.Enabled = false;
                var option = MessageBox.Show("Vrei sa scapi drace","Exit", MessageBoxButtons.OKCancel);

                if (option == DialogResult.OK)
                { 
                    Close();
                }
                backgroundSound.Play();
                pictureBox1.Enabled=true;
                timer1.Enabled = true;

            }
        }
        
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
            Engine.Shoot(new Point(e.X, e.Y));
            pictureBox1.Cursor = new Cursor(bm.GetHicon());

        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Bitmap bm1 = new Bitmap(new Bitmap("../../Images/Sword2.png"), 80, 160);
            pictureBox1.Cursor=new Cursor(bm1.GetHicon());
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           Engine.Tick();
        }

        private void TimeLabel_Click(object sender, EventArgs e)
        {

        }

        
    }
}
