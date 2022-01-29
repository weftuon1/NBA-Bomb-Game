using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBA_BOMB
{
    public partial class Form1 : Form
    {
        PictureBox[,] character = new PictureBox[2, 5];
        Image[,] nba = new Image[2, 5];
        Image[,] nbashow = new Image[2, 5];
        Image[,] nbaplayer = new Image[2, 5];
        String[,] nbaname = new String[2, 5];
        const int characterx = 320;
        const int charactery = 500;
        int selectindex1 = -1;
        int selectindex2 = -1;
        int flash = 1;


        private void characterset()
        {
            Character.BackgroundImage = new Bitmap(Properties.Resources.Character_1000);

            character[0, 0] = charactera1; character[0, 1] = charactera2; character[0, 2] = charactera3; character[0, 3] = charactera4; character[0, 4] = charactera5;
            character[1, 0] = characterb1; character[1, 1] = characterb2; character[1, 2] = characterb3; character[1, 3] = characterb4; character[1, 4] = characterb5;

            nba[0, 0] = new Bitmap(Properties.Resources.Jeremy_60); nba[0, 1] = new Bitmap(Properties.Resources.Michael_60); nba[0, 2] = new Bitmap(Properties.Resources.LeBron_60); nba[0, 3] = new Bitmap(Properties.Resources.Wade_60); nba[0, 4] = new Bitmap(Properties.Resources.Iverson_60);
            nba[1, 0] = new Bitmap(Properties.Resources.McGee_60); nba[1, 1] = new Bitmap(Properties.Resources.Kobe_60); nba[1, 2] = new Bitmap(Properties.Resources.Curry_60); nba[1, 3] = new Bitmap(Properties.Resources.Duncan_60); nba[1, 4] = new Bitmap(Properties.Resources.Yao_60);

            nbashow[0, 0] = new Bitmap(Properties.Resources.Jeremy_275x275); nbashow[0, 1] = new Bitmap(Properties.Resources.Michael_300x250); nbashow[0, 2] = new Bitmap(Properties.Resources.LeBron_250x300); nbashow[0, 3] = new Bitmap(Properties.Resources.Wade_275x275); nbashow[0, 4] = new Bitmap(Properties.Resources.Iverson_300x250);
            nbashow[1, 0] = new Bitmap(Properties.Resources.McGee_300x250); nbashow[1, 1] = new Bitmap(Properties.Resources.Kobe_300x250); nbashow[1, 2] = new Bitmap(Properties.Resources.Curry_275x275); nbashow[1, 3] = new Bitmap(Properties.Resources.Duncan_300x250); nbashow[1, 4] = new Bitmap(Properties.Resources.Yao_275x275);

            nbaplayer[0, 0] = new Bitmap(Properties.Resources.Jeremy_40); nbaplayer[0, 1] = new Bitmap(Properties.Resources.Michael_40); nbaplayer[0, 2] = new Bitmap(Properties.Resources.LeBron_40); nbaplayer[0, 3] = new Bitmap(Properties.Resources.Wade_40); nbaplayer[0, 4] = new Bitmap(Properties.Resources.Iverson_40);
            nbaplayer[1, 0] = new Bitmap(Properties.Resources.McGee_40); nbaplayer[1, 1] = new Bitmap(Properties.Resources.Kobe_40); nbaplayer[1, 2] = new Bitmap(Properties.Resources.Curry_40); nbaplayer[1, 3] = new Bitmap(Properties.Resources.Duncan_40); nbaplayer[1, 4] = new Bitmap(Properties.Resources.Yao_40);

            nbaname[0, 0] = "Jeremy Lin"; nbaname[0, 1] = "Michael Jordan"; nbaname[0, 2] = "LeBron James"; nbaname[0, 3] = "Dwyane Wade"; nbaname[0, 4] = "Allen Iverson";
            nbaname[1, 0] = "JaVale McGee"; nbaname[1, 1] = "Kobe Bryant"; nbaname[1, 2] = "Stephen Curry"; nbaname[1, 3] = "Tim Duncan"; nbaname[1, 4] = "Yao Ming";

            p1_label.Location = new Point(50, 20);
            p2_label.Location = new Point(685, 20);
            verus.Location = new Point(368, 20);
            p1_character.Location = new Point(40, 100);
            p2_character.Location = new Point(660, 100);
            p1_name.Location = new Point(60, 400);
            p2_name.Location = new Point(675, 400);
            p1_howtoplay.Location = new Point(95, 470);
            p1_howtoplay.Image = new Bitmap(Properties.Resources.howtoplayp1);
            p2_howtoplay.Location = new Point(720, 470);
            p2_howtoplay.Image = new Bitmap(Properties.Resources.howtoplayp2);
            p1_ready.Location = new Point(40, 565);
            p1_ready.Text = "Press Space to be ready";
            p2_ready.Location = new Point(680, 565);
            p2_ready.Text = "Press L to be ready";


            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 5; j++)
                {
                    character[i, j].BackColor = Color.LightBlue;
                    character[i, j].Image = nba[i, j];
                    character[i, j].Tag = 0;
                    character[i, j].Location = new Point(characterx + j * 70, charactery + i * 70);
                }

            character[0, 2].Tag = 1;
            character[1, 2].Tag = 2;

            character[0, 2].Invalidate();
            character[1, 2].Invalidate();

            selectindex1 = 2; //第一列中間
            selectindex2 = 7; //第二列中間

            selecttimer.Enabled = true;

        }

        private void selecttimer_Tick(object sender, EventArgs e)
        {
            if (Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) == 3 && Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) == 4)
            {
                selecttimer.Enabled = false;
                p1_start.Size = p1_character.Size - new Size(50, 50); ;
                p1_start.Image = nbashow[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1];
                p2_start.Size = p2_character.Size - new Size(50, 50); ;
                p2_start.Image = nbashow[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2];
                
                player1.Image = nbaplayer[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1]; //輸入player1圖片
                player2.Image = nbaplayer[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2]; //輸入player2圖片
                //==========隨機選擇電腦玩家圖片==========
                int p3, p4;
                do
                {
                    p3 = fixRand.Next(0, 10);
                    if (p3 != selectindex1 && p3 != selectindex2)
                    {
                        switch (p3)
                        {
                            case 0:
                            case 3:
                            case 7:
                            case 9:
                                p3_start.Size = new Size(275, 275);
                                break;
                            case 1:
                            case 4:
                            case 5:
                            case 6:
                            case 8:
                                p3_start.Size = new Size(300, 250);
                                break;
                            case 2:
                                p3_start.Size = new Size(250, 300);
                                break;
                        }
                        p3_start.Size =  new Size(225, 225);
                        p3_start.Image = nbashow[p3 > 4 ? 1 : 0, p3 > 4 ? p3 - 5 : p3];
                        player3.Image = nbaplayer[p3 > 4 ? 1 : 0, p3 > 4 ? p3 - 5 : p3];                      
                        break;
                    }
                }
                while (true);

                do
                {
                    p4 = fixRand.Next(0, 10);
                    if (p4 != selectindex1 && p4 != selectindex2 && p4 != p3)
                    {
                        switch(p4)
                        {
                            case 0 :
                            case 3 :
                            case 7 :
                            case 9 :
                                p4_start.Size = new Size(275, 275);
                                break;
                            case 1 :
                            case 4 :
                            case 5 :
                            case 6 :
                            case 8 :
                                p4_start.Size = new Size(300, 250);
                                break;
                            case 2 :
                                p4_start.Size = new Size(250, 300);
                                break;
                        }
                        p4_start.Size = new Size(225, 225);
                        p4_start.Image = nbashow[p4 > 4 ? 1 : 0, p4 > 4 ? p4 - 5 : p4];
                        player4.Image = nbaplayer[p4 > 4 ? 1 : 0, p4 > 4 ? p4 - 5 : p4];
                        break;
                    }
                }
                while (true);

                //==========切換到Ready panel==========
                Ready.Location = new Point(0, 0);
                Ready.Enabled = true;
                Ready.Visible = true;
                Main.Enabled = false;
                Main.Visible = false;
                Game.Enabled = false;
                Game.Visible = false;
                Rank.Enabled = false;
                Rank.Visible = false;
                Character.Enabled = false;
                Character.Visible = false;
                readyset();
            }


            if (flash == 1)
                flash = 0;
            else if (flash == 0)
                flash = 1;

            Character.Invalidate();
        }

        // Tag 0:沒事 ; 1 : p1正在選 ; 2 : p2正在選 ; 3 : p1已選 ; 4 : p2已選
        private void Character_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 5; j++)
                {
                    character[i, j].Invalidate();
                }
        }

        private void charactera3_Paint(object sender, PaintEventArgs e)  //LeBron_250x300
        {
            PictureBox pb = (PictureBox)sender;
            if ((Convert.ToInt64(pb.Tag) == 1 && flash == 1) || (Convert.ToInt64(pb.Tag) == 3))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 8), 0, 0, pb.Width, pb.Height);
                p1_character.Size = new Size(250, 300);
                p1_character.Image = nbashow[0, 2];
                p1_name.Text = nbaname[0, 2];
            }
            else if (Convert.ToInt64(pb.Tag) == 2 && flash == 1 || (Convert.ToInt64(pb.Tag) == 4))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 8), 0, 0, pb.Width, pb.Height);
                p2_character.Size = new Size(250, 300);
                p2_character.Image = nbashow[0, 2];
                p2_name.Text = nbaname[0, 2];
            }
        }

        private void characterb3_Paint(object sender, PaintEventArgs e) //Curry_275x275
        {
            PictureBox pb = (PictureBox)sender;
            if (Convert.ToInt64(pb.Tag) == 1 && flash == 1 || (Convert.ToInt64(pb.Tag) == 3))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 8), 0, 0, pb.Width, pb.Height);
                p1_character.Size = new Size(275, 275);
                p1_character.Image = nbashow[1, 2];
                p1_name.Text = nbaname[1, 2];
            }
            else if (Convert.ToInt64(pb.Tag) == 2 && flash == 1 || (Convert.ToInt64(pb.Tag) == 4))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 8), 0, 0, pb.Width, pb.Height);
                p2_character.Size = new Size(275, 275);
                p2_character.Image = nbashow[1, 2];
                p2_name.Text = nbaname[1, 2];
            }
        }

        private void charactera1_Paint(object sender, PaintEventArgs e) //Jeremy_275x275
        {
            PictureBox pb = (PictureBox)sender;
            if (Convert.ToInt64(pb.Tag) == 1 && flash == 1 || (Convert.ToInt64(pb.Tag) == 3))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 8), 0, 0, pb.Width, pb.Height);
                p1_character.Size = new Size(275, 275);
                p1_character.Image = nbashow[0, 0];
                p1_name.Text = nbaname[0, 0];
            }
            else if (Convert.ToInt64(pb.Tag) == 2 && flash == 1 || (Convert.ToInt64(pb.Tag) == 4))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 8), 0, 0, pb.Width, pb.Height);
                p2_character.Size = new Size(275, 275);
                p2_character.Image = nbashow[0, 0];
                p2_name.Text = nbaname[0, 0];
            }
        }

        private void charactera2_Paint(object sender, PaintEventArgs e) //Michael_300x250
        {
            PictureBox pb = (PictureBox)sender;
            if (Convert.ToInt64(pb.Tag) == 1 && flash == 1 || (Convert.ToInt64(pb.Tag) == 3))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 8), 0, 0, pb.Width, pb.Height);
                p1_character.Size = new Size(300, 250);
                p1_character.Image = nbashow[0, 1];
                p1_name.Text = nbaname[0, 1];
            }
            else if (Convert.ToInt64(pb.Tag) == 2 && flash == 1 || (Convert.ToInt64(pb.Tag) == 4))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 8), 0, 0, pb.Width, pb.Height);
                p2_character.Size = new Size(300, 250);
                p2_character.Image = nbashow[0, 1];
                p2_name.Text = nbaname[0, 1];
            }
        }

        private void charactera4_Paint(object sender, PaintEventArgs e) //Wade_275x275
        {
            PictureBox pb = (PictureBox)sender;
            if (Convert.ToInt64(pb.Tag) == 1 && flash == 1 || (Convert.ToInt64(pb.Tag) == 3))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 8), 0, 0, pb.Width, pb.Height);
                p1_character.Size = new Size(275, 275);
                p1_character.Image = nbashow[0, 3];
                p1_name.Text = nbaname[0, 3];
            }
            else if (Convert.ToInt64(pb.Tag) == 2 && flash == 1 || (Convert.ToInt64(pb.Tag) == 4))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 8), 0, 0, pb.Width, pb.Height);
                p2_character.Size = new Size(275, 275);
                p2_character.Image = nbashow[0, 3];
                p2_name.Text = nbaname[0, 3];
            }
        }

        private void charactera5_Paint(object sender, PaintEventArgs e) //Iverson_300x250
        {
            PictureBox pb = (PictureBox)sender;
            if (Convert.ToInt64(pb.Tag) == 1 && flash == 1 || (Convert.ToInt64(pb.Tag) == 3))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 8), 0, 0, pb.Width, pb.Height);
                p1_character.Size = new Size(300, 250);
                p1_character.Image = nbashow[0, 4];
                p1_name.Text = nbaname[0, 4];            
            }
            else if (Convert.ToInt64(pb.Tag) == 2 && flash == 1 || (Convert.ToInt64(pb.Tag) == 4))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 8), 0, 0, pb.Width, pb.Height);
                p2_character.Size = new Size(300, 250);
                p2_character.Image = nbashow[0, 4];
                p2_name.Text = nbaname[0, 4];
            }
        }

        private void characterb1_Paint(object sender, PaintEventArgs e) //McGee_300x250
        {
            PictureBox pb = (PictureBox)sender;
            if (Convert.ToInt64(pb.Tag) == 1 && flash == 1 || (Convert.ToInt64(pb.Tag) == 3))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 8), 0, 0, pb.Width, pb.Height);
                p1_character.Size = new Size(300, 250);
                p1_character.Image = nbashow[1, 0];
                p1_name.Text = nbaname[1, 0];              
            }
            else if (Convert.ToInt64(pb.Tag) == 2 && flash == 1 || (Convert.ToInt64(pb.Tag) == 4))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 8), 0, 0, pb.Width, pb.Height);
                p2_character.Size = new Size(300, 250);
                p2_character.Image = nbashow[1, 0];
                p2_name.Text = nbaname[1, 0];
            }
        }

        private void characterb2_Paint(object sender, PaintEventArgs e) //Kobe_300x250
        {
            PictureBox pb = (PictureBox)sender;
            if (Convert.ToInt64(pb.Tag) == 1 && flash == 1 || (Convert.ToInt64(pb.Tag) == 3))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 8), 0, 0, pb.Width, pb.Height);
                p1_character.Size = new Size(300, 250);
                p1_character.Image = nbashow[1, 1];
                p1_name.Text = nbaname[1, 1];
            }
            else if (Convert.ToInt64(pb.Tag) == 2 && flash == 1 || (Convert.ToInt64(pb.Tag) == 4))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 8), 0, 0, pb.Width, pb.Height);
                p2_character.Size = new Size(300, 250);
                p2_character.Image = nbashow[1, 1];
                p2_name.Text = nbaname[1, 1];
            }
        }

        private void characterb4_Paint(object sender, PaintEventArgs e) //Duncan_300x250
        {
            PictureBox pb = (PictureBox)sender;
            if (Convert.ToInt64(pb.Tag) == 1 && flash == 1 || (Convert.ToInt64(pb.Tag) == 3))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 8), 0, 0, pb.Width, pb.Height);
                p1_character.Size = new Size(300, 250);
                p1_character.Image = nbashow[1, 3];
                p1_name.Text = nbaname[1, 3];
            }
            else if (Convert.ToInt64(pb.Tag) == 2 && flash == 1 || (Convert.ToInt64(pb.Tag) == 4))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 8), 0, 0, pb.Width, pb.Height);
                p2_character.Size = new Size(300, 250);
                p2_character.Image = nbashow[1, 3];
                p2_name.Text = nbaname[1, 3];
            }
        }

        private void characterb5_Paint(object sender, PaintEventArgs e) //Yao_275x275
        {
            PictureBox pb = (PictureBox)sender;
            if (Convert.ToInt64(pb.Tag) == 1 && flash == 1 || (Convert.ToInt64(pb.Tag) == 3))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 8), 0, 0, pb.Width, pb.Height);
                p1_character.Size = new Size(275, 275);
                p1_character.Image = nbashow[1, 4];
                p1_name.Text = nbaname[1, 4];
            }
            else if (Convert.ToInt64(pb.Tag) == 2 && flash == 1 || (Convert.ToInt64(pb.Tag) == 4))
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 8), 0, 0, pb.Width, pb.Height);
                p2_character.Size = new Size(275, 275);
                p2_character.Image = nbashow[1, 4];
                p2_name.Text = nbaname[1, 4];
            }
        }
    }
}