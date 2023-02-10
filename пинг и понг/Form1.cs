using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace пинг_и_понг
{
    public partial class Form1 : Form
    {

        public int speed_left = 4; //скорость мячика
        public int speed_top = 4;
        public int point = 0;      //Scored point 

        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            Cursor.Hide();        //скрываем курсор

            this.FormBorderStyle = FormBorderStyle.None;  //удаляем границы формы
            this.TopMost = true;                          //форма на переднем плане, что мешает выглядывать программам
            this.Bounds = Screen.PrimaryScreen.Bounds;    // на весь экран

            racket.Top = playground.Bottom - (playground.Bottom / 10);   //ракетка

            Boom.BackColor = Color.Transparent;
           // System.IO.FileStream fs = new System.IO.FileStream("C: \\Users\\Wait4n\\Desktop\\занятие на каждый день\\Borland Delphi c#\\no.gif");
            System.Drawing.Image img = System.Drawing.Image.FromFile(Directory.GetCurrentDirectory() + "\\no.gif");
            Boom.Image = img;
            Boom.Visible = false;

            GO.BackColor = Color.Transparent;
            System.Drawing.Image img1 = System.Drawing.Image.FromFile(Directory.GetCurrentDirectory() + "\\GO.gif");
            GO.SizeMode = PictureBoxSizeMode.StretchImage;
            GO.Image = img1;
            GO.Visible = false;
            GO.Left = (playground.Width / 2) - (GO.Width / 2);
            GO.Top = (playground.Height / 2) - (GO.Height / 2);

            gameover_lbl.Left = (playground.Width / 2) - (gameover_lbl.Width / 2);
            gameover_lbl.Top = GO.Top + GO.Height;
            gameover_lbl.Visible = false;

            ball.BackColor = Color.Transparent;
            //string fn = Directory.GetCurrentDirectory();
            //string p = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "\\512.png"));
            //System.Drawing.Image img2 = System.Drawing.Image.FromFile("C: \\Users\\Wait4n\\Desktop\\занятие на каждый день\\Borland Delphi c#\\512.png");
            System.Drawing.Image img2 = System.Drawing.Image.FromFile(Directory.GetCurrentDirectory() + "\\512.png");
            ball.SizeMode = PictureBoxSizeMode.StretchImage;
            ball.Image = img2;
        }

        private void playground_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            racket.Left = Cursor.Position.X - (racket.Width/2);  //позиция курсора 

            ball.Left += speed_left;  //move the ball
            ball.Top += speed_top;
            Random r;
            if(ball.Bottom >= racket.Top && ball.Bottom <= racket.Bottom && ball.Left >= racket.Left && ball.Right <= racket.Right )   //racket collision
            {
                speed_top += 1;
                speed_left += 1;
                speed_top = -speed_top;  //change direction
                point += 1;
                points_lbl.Text = point.ToString();
                r = new Random();
                playground.BackColor = Color.FromArgb(r.Next(150, 255), r.Next(150, 255), r.Next(150, 255));
            }

            if (ball.Left <= playground.Left || ball.Right >= playground.Right)
            {
                speed_left = -speed_left;
            }
            //if (ball.Right >= playground.Right)
            //{
            //    speed_left = -speed_left;
            //}
            if(ball.Top <= playground.Top)
            {
                speed_top = -speed_top;
            }

            if (ball.Bottom >= playground.Bottom)
            {
                Boom.Left = ball.Left - (Boom.Width / 2);
                Boom.Top = ball.Top - (Boom.Height / 2) + 50;
                Boom.Visible = true;
                timer1.Enabled = false;  //game over
                timer2.Enabled = true;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape) { this.Close(); } //чтобы уметь закрывать игру
            if(e.KeyCode == Keys.Enter)
            {
                ball.Top = 50;
                ball.Left = 50;
                speed_left = 4;
                speed_top = 4;
                point = 0;
                points_lbl.Text = "0";
                Boom.Visible = false;
                timer1.Enabled = true;
                timer2.Enabled = false;
                gameover_lbl.Visible = false;
                GO.Visible = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            GO.Visible = true;
            gameover_lbl.Visible = true;
        }
    }
}
