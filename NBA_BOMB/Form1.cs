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
        public Form1()
        {
            InitializeComponent();

            Main.BackgroundImage = new Bitmap(Properties.Resources.Main_1000);
            Main.Location = new Point(0, 0);
            Main.Enabled = true;
            Main.Visible = true;
            Game.Enabled = false;
            Game.Visible = false;
            Rank.Enabled = false;
            Rank.Visible = false;
            Character.Enabled = false;
            Character.Visible = false;
            Ready.Enabled = false;
            Ready.Visible = false;

            setvalue();

            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(Character, true, null);
            aProp.SetValue(Ready, true, null);
            aProp.SetValue(Game, true, null);
            aProp.SetValue(warning, true, null);           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1000, 700);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); //双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* togame
            GC.Collect();
            setimage();
            generate_map();
            Game.Location = new Point(0, 0);
            Game.Enabled = true;
            Game.Visible = true;
            Main.Enabled = false;
            Main.Visible = false;
            Rank.Enabled = false;
            Rank.Visible = false;
            character.Enabled = false;
            character.Visible = false;
            Ready.Enabled = false;
            Ready.Visible = false;
            button2.Enabled = false;
            //Game.Invalidate(); 系統自動重載

            player1.Location = new Point(map[player[0].y, player[0].x].x, map[player[0].y, player[0].x].y);
            player2.Location = new Point(map[player[1].y, player[1].x].x, map[player[1].y, player[1].x].y);
            player3.Location = new Point(map[player[2].y, player[2].x].x, map[player[2].y, player[2].x].y);
            player4.Location = new Point(map[player[3].y, player[3].x].x, map[player[3].y, player[3].x].y);

            player1_push.Visible = false;
            player2_push.Visible = false;

            button1.Enabled = false;
            button1.TabStop = false;
            button2.TabStop = false;

            aitimer.Enabled = true;
            gametimer.Enabled = true;
            */
            setvalue();
            Character.Location = new Point(0, 0);
            Character.Enabled = true;
            Character.Visible = true;
            Main.Enabled = false;
            Main.Visible = false;
            Game.Enabled = false;
            Game.Visible = false;
            Rank.Enabled = false;
            Rank.Visible = false;
            Ready.Enabled = false;
            Ready.Visible = false;

            characterset();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Rank.BackgroundImage = new Bitmap(Properties.Resources.Rank_1000);
            Rank.Location = new Point(0, 0);
            Rank.Enabled = true;
            Rank.Visible = true;
            Main.Enabled = false;
            Main.Visible = false;
            Game.Enabled = false;
            Game.Visible = false;
            Character.Enabled = false;
            Character.Visible = false;
            Ready.Enabled = false;
            Ready.Visible = false;
            fileread();
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            //   MessageBox.Show("test");
            if (Game.Enabled == true)
            {                
                Graphics dr1 = Graphics.FromImage(scr);// 圖層1
                Graphics dr2 = Graphics.FromImage(scr);// 圖層1
                Graphics dr3 = Graphics.FromImage(scr);// 圖層3 火焰
                Graphics dr4 = Graphics.FromImage(scr);// 圖層4 特殊道具
                Graphics dr5 = Graphics.FromImage(scr);
                //====================== 撿起道具 =======================
                if (player1.Location.X == map[player[0].y, player[0].x].x && player1.Location.Y == map[player[0].y, player[0].x].y && map[player[0].y, player[0].x].item > 0 && player[0].life > 0)
                {
                    player1_specialitem(1, map[player[0].y, player[0].x].item);
                    map[player[0].y, player[0].x].item = 0;
                }
                if (player2.Location.X == map[player[1].y, player[1].x].x && player2.Location.Y == map[player[1].y, player[1].x].y && map[player[1].y, player[1].x].item > 0 && player[1].life > 0)
                {
                    player2_specialitem(2, map[player[1].y, player[1].x].item);
                    map[player[1].y, player[1].x].item = 0;
                }
                //====================== End ============================
                //====================== 人物1部分重劃  =================
                if (timer1.Enabled == true || uptimer.Enabled == true || downtimer.Enabled == true || righttimer.Enabled == true || lefttimer.Enabled == true)                    
                {
                    
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            switch (map[player[0].y + i - 1, player[0].x + j - 1].tag)
                            {
                                case -1:
                                    dr2.DrawImage(pass, map[player[0].y + i - 1, player[0].x + j - 1].x, map[player[0].y + i - 1, player[0].x + j - 1].y, mapwidth, mapheight);
                                    break;
                                case -2:
                                    if (map[player[0].y + i - 1, player[0].x + j - 1].warn == false)
                                        dr2.DrawImage(notpass, map[player[0].y + i - 1, player[0].x + j - 1].x, map[player[0].y + i - 1, player[0].x + j - 1].y, mapwidth, mapheight);
                                    break;
                                case 1:
                                    dr2.DrawImage(pass, map[player[0].y + i - 1, player[0].x + j - 1].x, map[player[0].y + i - 1, player[0].x + j - 1].y, mapwidth, mapheight);
                                    dr2.DrawImage(trash, map[player[0].y + i - 1, player[0].x + j - 1].x, map[player[0].y + i - 1, player[0].x + j - 1].y, mapwidth, mapheight);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    
                }
                //===================== End ======================
                //===================== 人物2部分重劃 ============
                if (timer2.Enabled == true || uptimer2.Enabled == true || downtimer2.Enabled == true || righttimer2.Enabled == true || lefttimer2.Enabled == true)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            switch (map[player[1].y + i - 1, player[1].x + j - 1].tag)
                            {
                                case -1:
                                    dr2.DrawImage(pass, map[player[1].y + i - 1, player[1].x + j - 1].x, map[player[1].y + i - 1, player[1].x + j - 1].y, mapwidth, mapheight);
                                    break;
                                case -2:
                                    if (map[player[1].y + i - 1, player[1].x + j - 1].warn == false)
                                        dr2.DrawImage(notpass, map[player[1].y + i - 1, player[1].x + j - 1].x, map[player[1].y + i - 1, player[1].x + j - 1].y, mapwidth, mapheight);
                                    break;
                                case 1:
                                    dr2.DrawImage(pass, map[player[1].y + i - 1, player[1].x + j - 1].x, map[player[1].y + i - 1, player[1].x + j - 1].y, mapwidth, mapheight);
                                    dr2.DrawImage(trash, map[player[1].y + i - 1, player[1].x + j - 1].x, map[player[1].y + i - 1, player[1].x + j - 1].y, mapwidth, mapheight);
                                    break;
                                default:
                                    break;
                            }                            
                        }
                    }
                }
                //====================== End ==============================
                //====================== AI(0)部分重劃 ====================
                if (movingflag[0]==true)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            switch (map[player[2].y + i - 1, player[2].x + j - 1].tag)
                            {
                                case -1:
                                    dr2.DrawImage(pass, map[player[2].y + i - 1, player[2].x + j - 1].x, map[player[2].y + i - 1, player[2].x + j - 1].y, mapwidth, mapheight);
                                    break;
                                case -2:
                                    if (map[player[2].y + i - 1, player[2].x + j - 1].warn == false)
                                        dr2.DrawImage(notpass, map[player[2].y + i - 1, player[2].x + j - 1].x, map[player[2].y + i - 1, player[2].x + j - 1].y, mapwidth, mapheight);
                                    break;
                                case 1:
                                    dr2.DrawImage(pass, map[player[2].y + i - 1, player[2].x + j - 1].x, map[player[2].y + i - 1, player[2].x + j - 1].y, mapwidth, mapheight);
                                    dr2.DrawImage(trash, map[player[2].y + i - 1, player[2].x + j - 1].x, map[player[2].y + i - 1, player[2].x + j - 1].y, mapwidth, mapheight);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                //====================== End ==============================
                //====================== AI(1)部分重劃 ====================
                if (movingflag[1] == true)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            switch (map[player[3].y + i - 1, player[3].x + j - 1].tag)
                            {
                                case -1:
                                    dr2.DrawImage(pass, map[player[3].y + i - 1, player[3].x + j - 1].x, map[player[3].y + i - 1, player[3].x + j - 1].y, mapwidth, mapheight);
                                    break;
                                case -2:
                                    if (map[player[3].y + i - 1, player[3].x + j - 1].warn == false)
                                        dr2.DrawImage(notpass, map[player[3].y + i - 1, player[3].x + j - 1].x, map[player[3].y + i - 1, player[3].x + j - 1].y, mapwidth, mapheight);
                                    break;
                                case 1:
                                    dr2.DrawImage(pass, map[player[3].y + i - 1, player[3].x + j - 1].x, map[player[3].y + i - 1, player[3].x + j - 1].y, mapwidth, mapheight);
                                    dr2.DrawImage(trash, map[player[3].y + i - 1, player[3].x + j - 1].x, map[player[3].y + i - 1, player[3].x + j - 1].y, mapwidth, mapheight);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                //====================== End ==============================

                if ((timer1.Enabled == false && uptimer.Enabled == false && downtimer.Enabled == false && righttimer.Enabled == false && lefttimer.Enabled == false) &&
                    (timer2.Enabled == false && uptimer2.Enabled == false && downtimer2.Enabled == false && righttimer2.Enabled == false && lefttimer2.Enabled == false)&&(movingflag[0]==false)&&movingflag[1]==false && t > 1700)
                {
                    for (int i = 0; i < num; i++)
                    {
                        for (int j = 0; j < num; j++)
                        {
                            if (map[i, j].tag == -1)
                                dr1.DrawImage(pass, map[i, j].x, map[i, j].y, mapwidth, mapheight);
                            else if (map[i, j].tag == -2)
                                dr1.DrawImage(notpass, map[i, j].x, map[i, j].y, mapwidth, mapheight);
                            else if (map[i, j].tag == 1)
                            {
                                dr1.DrawImage(pass, map[i, j].x, map[i, j].y, mapwidth, mapheight);
                                dr1.DrawImage(trash, map[i, j].x, map[i, j].y, mapwidth, mapheight);
                            }

                            if(map[i, j].item >= 1 && map[i, j].tag == -1)
                                dr4.DrawImage(tool[map[i, j].item], map[i, j].x, map[i, j].y, mapwidth, mapheight);                            
                        }
                    }
                }

                //=============== 炸彈移動重劃 =============================
                for(int a=0;a<4;a++)
                    for(int b=0;b<bombnum;b++)
                        if(bomb[a,b].bombdirection>0)
                        {
                            btomi = (bomb[a, b].y - topy) / mapheight;
                            btomj = (bomb[a, b].x - leftx) / mapwidth;
                            for (int i = 0; i < 3; i++)                            
                                for (int j = 0; j < 3; j++)                                
                                    if(map[btomi + i - 1, btomj + j - 1].tag==-1)                                                                            
                                            dr2.DrawImage(pass, map[btomi + i - 1, btomj + j - 1].x, map[btomi + i - 1, btomj + j - 1].y, mapwidth, mapheight);                                                                                                                                                                                                                              
                        }
                //================ 化火焰 特殊道具 炸彈 ====================
                for (int i = 0; i < num; i++)
                    for (int j = 0; j < num; j++)
                    {
                        if (map[i, j].firetoroadflag == 1)
                        {
                            dr1.DrawImage(pass, map[i, j].x, map[i, j].y, mapwidth, mapheight);
                            map[i, j].firetoroadflag = 0;
                        }
                        if (map[i, j].fire == true)                        
                            dr1.DrawImage(pass, map[i, j].x, map[i, j].y, mapwidth, mapheight);                                                    
                        if (map[i, j].item >= 1 && map[i, j].tag == -1)                        
                            dr4.DrawImage(tool[map[i,j].item], map[i, j].x, map[i, j].y, mapwidth, mapheight);
                        if (map[i, j].fire == true)
                            dr3.DrawImage(fire, map[i, j].x, map[i, j].y, mapwidth, mapheight);                                                   
                    }

                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < bombnum; j++)
                    {
                        if (bomb[i, j].exist == true)
                            dr3.DrawImage(bomber, bomb[i, j].x, bomb[i, j].y, mapwidth, mapheight);
                    }

                //====================== End ===================
                //====================== 劃線 ==================
                for (int i = 0; i < num; i++)
                    dr1.DrawLine(myPen, map[i, 0].x, map[i, 0].y, map[i, num - 1].x + mapwidth, map[i, 0].y); //橫線
                for (int j = 0; j < num; j++)
                    dr1.DrawLine(myPen, map[0, j].x, map[0, j].y, map[0, j].x, map[num - 1, j].y + mapheight); //直線

                dr1.DrawLine(myPen, map[0, num - 1].x + mapwidth, map[0, 0].y, map[0, num - 1].x + mapwidth, map[num - 1, 0].y + mapheight); //最右
                dr1.DrawLine(myPen, map[0, 0].x, map[num - 1, 0].y + mapheight, map[0, num - 1].x + mapwidth, map[num - 1, 0].y + mapheight); //最下
                //====================== End ===================

                //=================== 偵測player1 被燒到 =======
                if (map[player[0].y, player[0].x].fire == true && mutekitimer.Enabled == false && player[0].life > 0)
                {
                    player[0].life--;
                    player1_life.Text = player[0].life.ToString();
                    if(player[0].life>0)
                        mutekitimer.Enabled = true;
                    else
                        isdied();
                }
                //====================== End ===================
                //=================== 偵測player2 被燒到 =======
                if (map[player[1].y, player[1].x].fire == true && mutekitimer2.Enabled == false && player[1].life > 0)
                {
                    player[1].life--;
                    player2_life.Text = player[1].life.ToString();
                    if (player[1].life > 0)
                        mutekitimer2.Enabled = true;
                    else
                        isdied();
                }
                //====================== End ===================

                //=================== 偵測player3 (AI 1) 被燒到 =======
                if (map[player[2].y, player[2].x].fire == true && mutekitimer3.Enabled == false && player[2].life > 0)
                {
                    player[2].life--;
                    player3_life.Text = player[2].life.ToString();
                    if(player[2].life>0)
                        mutekitimer3.Enabled = true;
                    else
                        isdied();
                }
                //====================== End ===================
                //=================== 偵測player4 (AI 2) 被燒到 =======
                if (map[player[3].y, player[3].x].fire == true && mutekitimer4.Enabled == false && player[3].life > 0)
                {
                    player[3].life--;
                    player4_life.Text = player[3].life.ToString();
                    if (player[3].life > 0)
                        mutekitimer4.Enabled = true;
                    else
                        isdied();
                }
                //====================== End ===================
                if (map[warni, warnj].warn == true && t > 300 && gametimer.Enabled == true)
                {
                    dr5.DrawImage(warnwall, map[warni, warnj].x, map[warni, warnj].y, mapwidth, mapheight);
                    for (int i = 0; i < 4; i++)
                        if (player[i].x == warnj && player[i].y == warni)
                        {
                            player[i].life = 0;
                            isdied();
                        }
                }
                e.Graphics.DrawImage(scr, 0, 0);
            }
            //GC.Collect();
        }

        private void uptimer_Tick(object sender, EventArgs e)
        {
            if (player[0].life <= 0)
                uptimer.Enabled = false;
            while (player1.Location.Y < map[player[0].y, player[0].x].y)
                player[0].y--;
            if (player[0].y == 0)
                uptimer.Enabled = false;
            else if (map[player[0].y - 1, player[0].x].tag == -1)
                player1.Location = new Point(player1.Location.X, player1.Location.Y - step);
            //============================= 撞到炸彈 =========================
            else if(player1.Location.Y == map[player[0].y, player[0].x].y && map[player[0].y - 1, player[0].x].tag == 2 && player[0].pushtime > 0)//碰到炸彈
            {
                if(iswaymove(0, map[player[0].y - 1, player[0].x].bomblocationindex, 1)==true) //炸彈有路可以走 ; (int playno,int index,int direction) 上：1 ; 左：2 ; 下：3 ; 右：4
                {
                    bombmovetimer.Enabled = true;
                    map[player[0].y - 1, player[0].x].bomblocationindex = -1;
                    //bombmove(0, map[player[0].y - 1, player[0].x].bomblocationindex, 1);
                }
                else
                {
                    timer1.Enabled = true;
                    uptimer.Enabled = false;
                }
            }
            //============================= End ==============================
            else
            {
                timer1.Enabled = true;
                uptimer.Enabled = false;
            }
        }

        private void downtimer_Tick(object sender, EventArgs e)
        {
            if (player[0].life <= 0)
                downtimer.Enabled = false;
            while (player1.Location.Y > map[player[0].y, player[0].x].y)
                player[0].y++;
            if (player[0].y == 14)
                downtimer.Enabled = false;
            else if (map[player[0].y + 1, player[0].x].tag == -1)
                player1.Location = new Point(player1.Location.X, player1.Location.Y + step);
            //============================= 撞到炸彈 =========================
            else if (player1.Location.Y == map[player[0].y, player[0].x].y && map[player[0].y + 1, player[0].x].tag == 2 && player[0].pushtime > 0)//碰到炸彈
            {
                if (iswaymove(0, map[player[0].y + 1, player[0].x].bomblocationindex, 3) == true) //炸彈有路可以走 ; (int playno,int index,int direction) 上：1 ; 左：2 ; 下：3 ; 右：4
                {
                    bombmovetimer.Enabled = true;
                    map[player[0].y + 1, player[0].x].bomblocationindex = -1;
                    //bombmove(0, map[player[0].y - 1, player[0].x].bomblocationindex, 1);
                }
                else
                {
                    timer1.Enabled = true;
                    downtimer.Enabled = false;
                }
            }
            //============================= End ==============================
            else
            {
                timer1.Enabled = true;
                downtimer.Enabled = false;
            }
        }

        private void righttimer_Tick(object sender, EventArgs e)
        {
            if (player[0].life <= 0)
                righttimer.Enabled = false;
            while (player1.Location.X > map[player[0].y, player[0].x].x)
                player[0].x++;
            if (player[0].x == 14)
                righttimer.Enabled = false;
            else if (map[player[0].y, player[0].x + 1].tag == -1)
                player1.Location = new Point(player1.Location.X + step, player1.Location.Y);
            //============================= 撞到炸彈 =========================
            else if (player1.Location.X == map[player[0].y, player[0].x].x && map[player[0].y, player[0].x + 1].tag == 2 && player[0].pushtime > 0)//碰到炸彈
            {
                if (iswaymove(0, map[player[0].y, player[0].x + 1].bomblocationindex, 4) == true) //炸彈有路可以走 ; (int playno,int index,int direction) 上：1 ; 左：2 ; 下：3 ; 右：4
                {
                    bombmovetimer.Enabled = true;
                    map[player[0].y, player[0].x + 1].bomblocationindex = -1;
                    //bombmove(0, map[player[0].y - 1, player[0].x].bomblocationindex, 1);
                }
                else
                {
                    timer1.Enabled = true;
                    righttimer.Enabled = false;
                }
            }
            //============================= End ==============================
            else
            {
                timer1.Enabled = true;
                righttimer.Enabled = false;
            }
        }

        private void lefttimer_Tick(object sender, EventArgs e)
        {
            if (player[0].life <= 0)
                lefttimer.Enabled = false;
            while (player1.Location.X < map[player[0].y, player[0].x].x)
                player[0].x--;
            if (player[0].x == 0)
                lefttimer.Enabled = false;
            else if (map[player[0].y, player[0].x - 1].tag == -1)
                player1.Location = new Point(player1.Location.X - step, player1.Location.Y);
            //============================= 撞到炸彈 =========================
            else if (player1.Location.X == map[player[0].y, player[0].x].x && map[player[0].y, player[0].x - 1].tag == 2 && player[0].pushtime > 0)//碰到炸彈
            {
                if (iswaymove(0, map[player[0].y, player[0].x - 1].bomblocationindex, 2) == true) //炸彈有路可以走 ; (int playno,int index,int direction) 上：1 ; 左：2 ; 下：3 ; 右：4
                {
                    bombmovetimer.Enabled = true;
                    map[player[0].y, player[0].x - 1].bomblocationindex = -1;
                    //bombmove(0, map[player[0].y - 1, player[0].x].bomblocationindex, 1);
                }
                else
                {
                    timer1.Enabled = true;
                    lefttimer.Enabled = false;
                }
            }
            //============================= End ==============================
            else
            {
                timer1.Enabled = true;
                lefttimer.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (player1.Location.Y < map[player[0].y, player[0].x].y)
                player1.Location = new Point(player1.Location.X, player1.Location.Y + step);
            else if (player1.Location.Y > map[player[0].y, player[0].x].y)
                player1.Location = new Point(player1.Location.X, player1.Location.Y - step);
            else if (player1.Location.X > map[player[0].y, player[0].x].x)
                player1.Location = new Point(player1.Location.X - step, player1.Location.Y);
            else if (player1.Location.X < map[player[0].y, player[0].x].x)
                player1.Location = new Point(player1.Location.X + step, player1.Location.Y);
            else
                timer1.Enabled = false;
        }

        private void uptimer2_Tick(object sender, EventArgs e)
        {
            if (player[1].life <= 0)
                uptimer2.Enabled = false;
            while (player2.Location.Y < map[player[1].y, player[1].x].y)
                player[1].y--;
            if (player[1].y == 0)
                uptimer2.Enabled = false;
            else if (map[player[1].y - 1, player[1].x].tag == -1)
                player2.Location = new Point(player2.Location.X, player2.Location.Y - step);
            //============================= 撞到炸彈 =========================
            else if (player2.Location.Y == map[player[1].y, player[1].x].y && map[player[1].y - 1, player[1].x].tag == 2 && player[1].pushtime > 0)//碰到炸彈
            {
                if (iswaymove(1, map[player[1].y - 1, player[1].x].bomblocationindex, 1) == true) //炸彈有路可以走 ; (int playno,int index,int direction) 上：1 ; 左：2 ; 下：3 ; 右：4
                {
                    bombmovetimer.Enabled = true;
                    map[player[1].y - 1, player[1].x].bomblocationindex = -1;
                    //bombmove(0, map[player[0].y - 1, player[0].x].bomblocationindex, 1);
                }
                else
                {
                    timer2.Enabled = true;
                    uptimer2.Enabled = false;
                }
            }
            //============================= End ==============================
            else
            {
                timer2.Enabled = true;
                uptimer2.Enabled = false;
            }
        }

        private void downtimer2_Tick(object sender, EventArgs e)
        {
            if (player[1].life <= 0)
                downtimer2.Enabled = false;
            while (player2.Location.Y > map[player[1].y, player[1].x].y)
                player[1].y++;
            if (player[1].y == 14)
                downtimer2.Enabled = false;
            else if (map[player[1].y + 1, player[1].x].tag == -1)
                player2.Location = new Point(player2.Location.X, player2.Location.Y + step);
            //============================= 撞到炸彈 =========================
            else if (player2.Location.Y == map[player[1].y, player[1].x].y && map[player[1].y + 1, player[1].x].tag == 2 && player[1].pushtime > 0)//碰到炸彈
            {
                if (iswaymove(1, map[player[1].y + 1, player[1].x].bomblocationindex, 3) == true) //炸彈有路可以走 ; (int playno,int index,int direction) 上：1 ; 左：2 ; 下：3 ; 右：4
                {
                    bombmovetimer.Enabled = true;
                    map[player[1].y + 1, player[1].x].bomblocationindex = -1;
                    //bombmove(0, map[player[0].y - 1, player[0].x].bomblocationindex, 1);
                }
                else
                {
                    timer2.Enabled = true;
                    downtimer2.Enabled = false;
                }
            }
            //============================= End ==============================
            else
            {
                timer2.Enabled = true;
                downtimer2.Enabled = false;
            }
        }

        private void righttimer2_Tick(object sender, EventArgs e)
        {
            if (player[1].life <= 0)
                righttimer2.Enabled = false;
            while (player2.Location.X > map[player[1].y, player[1].x].x)
                player[1].x++;
            if (player[1].x == 14)
                righttimer2.Enabled = false;
            else if (map[player[1].y, player[1].x + 1].tag == -1)
                player2.Location = new Point(player2.Location.X + step, player2.Location.Y);
            //============================= 撞到炸彈 =========================
            else if (player2.Location.X == map[player[1].y, player[1].x].x && map[player[1].y, player[1].x + 1].tag == 2 && player[1].pushtime > 0)//碰到炸彈
            {
                if (iswaymove(1, map[player[1].y, player[1].x + 1].bomblocationindex, 4) == true) //炸彈有路可以走 ; (int playno,int index,int direction) 上：1 ; 左：2 ; 下：3 ; 右：4
                {
                    bombmovetimer.Enabled = true;
                    map[player[1].y, player[1].x + 1].bomblocationindex = -1;
                    //bombmove(0, map[player[0].y - 1, player[0].x].bomblocationindex, 1);
                }
                else
                {
                    timer2.Enabled = true;
                    righttimer2.Enabled = false;
                }
            }
            //============================= End ==============================
            else
            {
                timer2.Enabled = true;
                righttimer2.Enabled = false;
            }
        }

        private void lefttimer2_Tick(object sender, EventArgs e)
        {
            if (player[1].life <= 0)
                lefttimer2.Enabled = false;
            while (player2.Location.X < map[player[1].y, player[1].x].x)
                player[1].x--;
            if (player[1].x == 0)
                lefttimer2.Enabled = false;
            else if (map[player[1].y, player[1].x - 1].tag == -1)
                player2.Location = new Point(player2.Location.X - step, player2.Location.Y);
            //============================= 撞到炸彈 =========================
            else if (player2.Location.X == map[player[1].y, player[1].x].x && map[player[1].y, player[1].x - 1].tag == 2 && player[1].pushtime > 0)//碰到炸彈
            {
                if (iswaymove(1, map[player[1].y, player[1].x - 1].bomblocationindex, 2) == true) //炸彈有路可以走 ; (int playno,int index,int direction) 上：1 ; 左：2 ; 下：3 ; 右：4
                {
                    bombmovetimer.Enabled = true;
                    map[player[1].y, player[1].x - 1].bomblocationindex = -1;
                    //bombmove(0, map[player[0].y - 1, player[0].x].bomblocationindex, 1);
                }
                else
                {
                    timer2.Enabled = true;
                    lefttimer2.Enabled = false;
                }
            }
            //============================= End ==============================
            else
            {
                timer2.Enabled = true;
                lefttimer2.Enabled = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (player2.Location.Y < map[player[1].y, player[1].x].y)
                player2.Location = new Point(player2.Location.X, player2.Location.Y + step);
            else if (player2.Location.Y > map[player[1].y, player[1].x].y)
                player2.Location = new Point(player2.Location.X, player2.Location.Y - step);
            else if (player2.Location.X > map[player[1].y, player[1].x].x)
                player2.Location = new Point(player2.Location.X - step, player2.Location.Y);
            else if (player2.Location.X < map[player[1].y, player[1].x].x)
                player2.Location = new Point(player2.Location.X + step, player2.Location.Y);
            else
                timer2.Enabled = false;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (Game.Enabled == true && player[0].life > 0)
            {
                if (downtimer.Enabled == true && e.KeyCode == Keys.S)
                {
                    timer1.Enabled = true;
                    downtimer.Enabled = false;
                }
                else if (uptimer.Enabled == true && e.KeyCode == Keys.W)
                {
                    timer1.Enabled = true;
                    uptimer.Enabled = false;
                }
                else if (righttimer.Enabled == true && e.KeyCode == Keys.D)
                {
                    timer1.Enabled = true;
                    righttimer.Enabled = false;
                }
                else if (lefttimer.Enabled == true && e.KeyCode == Keys.A)
                {
                    timer1.Enabled = true;
                    lefttimer.Enabled = false;
                }
            }

            if (Game.Enabled == true && player[1].life > 0)
            {
                if (downtimer2.Enabled == true && e.KeyCode == Keys.Down)
                {
                    timer2.Enabled = true;
                    downtimer2.Enabled = false;
                }
                else if (uptimer2.Enabled == true && e.KeyCode == Keys.Up)
                {
                    timer2.Enabled = true;
                    uptimer2.Enabled = false;
                }
                else if (righttimer2.Enabled == true && e.KeyCode == Keys.Right)
                {
                    timer2.Enabled = true;
                    righttimer2.Enabled = false;
                }
                else if (lefttimer2.Enabled == true && e.KeyCode == Keys.Left)
                {
                    timer2.Enabled = true;
                    lefttimer2.Enabled = false;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Game.Enabled == true && uptimer.Enabled == false && downtimer.Enabled == false && righttimer.Enabled == false && lefttimer.Enabled == false && timer1.Enabled == false && player[0].life > 0)
            {
                // int direction; 上：1 ; 左：2 ; 下：3 ; 右：4
                if (e.KeyCode == Keys.S)
                {
                    downtimer.Enabled = true;
                    player[0].direction = 3;
                }
                else if (e.KeyCode == Keys.W)
                {
                    uptimer.Enabled = true;
                    player[0].direction = 1;
                }
                else if (e.KeyCode == Keys.D)
                {
                    righttimer.Enabled = true;
                    player[0].direction = 4;
                }
                else if (e.KeyCode == Keys.A)
                {
                    lefttimer.Enabled = true;
                    player[0].direction = 2;
                }
                else if (e.KeyCode == Keys.Space && player[0].bombcount > 0)
                    bombvalue(1, player1.Location.X, player1.Location.Y, ref player[0].bombindex);
            }

            if (Game.Enabled == true && uptimer2.Enabled == false && downtimer2.Enabled == false && righttimer2.Enabled == false && lefttimer2.Enabled == false && timer2.Enabled == false && player[1].life > 0)
            {
                if (e.KeyCode == Keys.Down)
                {
                    downtimer2.Enabled = true;
                    player[1].direction = 3;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    uptimer2.Enabled = true;
                    player[1].direction = 1;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    righttimer2.Enabled = true;
                    player[1].direction = 4;
                }
                else if (e.KeyCode == Keys.Left)
                {
                    lefttimer2.Enabled = true;
                    player[1].direction = 2;
                }
                else if (e.KeyCode == Keys.L && player[1].bombcount > 0)
                    bombvalue(2, player2.Location.X, player2.Location.Y, ref player[1].bombindex);
            }

            //===============Character===============
            if (Character.Enabled == true)
            {
                if (e.KeyCode == Keys.S && Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) != 3)
                {
                    character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag = 0;

                    selectindex1 += 5;
                    if (selectindex1 > 9)
                        selectindex1 -= 10;
                    if (Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) == 2 || Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) == 4)
                        selectindex1 += 5;
                    if (selectindex1 > 9)
                        selectindex1 -= 10;

                    character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag = 1;
                }
                else if (e.KeyCode == Keys.W && Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) != 3)
                {
                    character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag = 0;

                    selectindex1 -= 5;
                    if (selectindex1 < 0)
                        selectindex1 += 10;
                    if (Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) == 2 || Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) == 4)
                        selectindex1 -= 5;
                    if (selectindex1 < 0)
                        selectindex1 += 10;

                    character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag = 1;
                }
                else if (e.KeyCode == Keys.D && Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) != 3)
                {
                    character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag = 0;

                    int flag = 0;
                    selectindex1 += 1;
                    if (selectindex1 == 5)
                        selectindex1 = 0;
                    else if (selectindex1 == 10)
                        selectindex1 = 5;
                    if (Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) == 2 || Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) == 4)
                    {
                        selectindex1 += 1;
                        flag = 1;
                    }
                    if (selectindex1 == 5 && flag == 1)
                        selectindex1 = 0;
                    else if (selectindex1 == 10 && flag == 1)
                        selectindex1 = 5;

                    character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag = 1;
                }
                else if (e.KeyCode == Keys.A && Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) != 3)
                {
                    character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag = 0;

                    int flag = 0;
                    selectindex1 -= 1;
                    if (selectindex1 == -1)
                        selectindex1 = 4;
                    else if (selectindex1 == 4)
                        selectindex1 = 9;
                    if (Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) == 2 || Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) == 4)
                    {
                        selectindex1 -= 1;
                        flag = 1;
                    }
                    if (selectindex1 == -1 && flag == 1)
                        selectindex1 = 4;
                    else if (selectindex1 == 4 && flag == 1)
                        selectindex1 = 9;

                    character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag = 1;
                }
                else if (e.KeyCode == Keys.Space)
                {
                    if (Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) == 1)
                    {
                        character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag = 3;
                        p1_ready.Text = "Press Space to cancel";
                    }
                    else if (Convert.ToInt64(character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag) == 3)
                    {
                        character[selectindex1 > 4 ? 1 : 0, selectindex1 > 4 ? selectindex1 - 5 : selectindex1].Tag = 1;
                        p1_ready.Text = "Press Space to be ready";
                    }
                        
                }

                if (e.KeyCode == Keys.Down && Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) != 4)
                {
                    character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag = 0;

                    selectindex2 += 5;
                    if (selectindex2 > 9)
                        selectindex2 -= 10;
                    if (Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) == 1 || Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) == 3)
                        selectindex2 += 5;
                    if (selectindex2 > 9)
                        selectindex2 -= 10;

                    character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag = 2;
                }
                else if (e.KeyCode == Keys.Up && Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) != 4)
                {
                    character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag = 0;

                    selectindex2 -= 5;
                    if (selectindex2 < 0)
                        selectindex2 += 10;
                    if (Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) == 1 || Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) == 3)
                        selectindex2 -= 5;
                    if (selectindex2 < 0)
                        selectindex2 += 10;

                    character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag = 2;
                }
                else if (e.KeyCode == Keys.Right && Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) != 4)
                {
                    character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag = 0;

                    int flag = 0;
                    selectindex2 += 1;
                    if (selectindex2 == 5)
                        selectindex2 = 0;
                    else if (selectindex2 == 10)
                        selectindex2 = 5;
                    if (Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) == 1 || Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) == 3)
                    {
                        selectindex2 += 1;
                        flag = 1;
                    }
                    if (selectindex2 == 5 && flag == 1)
                        selectindex2 = 0;
                    else if (selectindex2 == 10 && flag == 1)
                        selectindex2 = 5;

                    character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag = 2;
                }
                else if (e.KeyCode == Keys.Left && Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) != 4)
                {
                    character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag = 0;

                    int flag = 0;
                    selectindex2 -= 1;
                    if (selectindex2 == -1)
                        selectindex2 = 4;
                    else if (selectindex2 == 4)
                        selectindex2 = 9;
                    if (Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) == 1 || Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) == 3)
                    {
                        selectindex2 -= 1;
                        flag = 1;
                    }
                    if (selectindex2 == -1 && flag == 1)
                        selectindex2 = 4;
                    else if (selectindex2 == 4 && flag == 1)
                        selectindex2 = 9;

                    character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag = 2;
                }
                else if (e.KeyCode == Keys.L)
                {
                    if (Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) == 2)
                    {
                        character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag = 4;
                        p2_ready.Text = "Press L to cancel";
                    }
                    else if (Convert.ToInt64(character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag) == 4)
                    {
                        character[selectindex2 > 4 ? 1 : 0, selectindex2 > 4 ? selectindex2 - 5 : selectindex2].Tag = 2;
                        p2_ready.Text = "Press L to be ready";
                    }                
                }

                //Character.Invalidate();

            }
                e.SuppressKeyPress = true; //讓鍵盤wasd按下時沒有聲音
        }

        private void bombtimer_Tick(object sender, EventArgs e)
        {
           // bool enableflag = false;
            //bool fireflag = false;

            int checkcount;//計算被爆炸的炸彈個數

            do
            {
                checkcount = 0;
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < bombnum; j++)
                    {
                        if (bomb[i, j].exist == true)
                        {
                            //enableflag = true;

                            if (bomb[i, j].timesec > 0)
                                bomb[i, j].timesec--;
                            //================================被爆炸的炸彈=================================
                            if (map[(bomb[i, j].y - topy) / mapheight, (bomb[i, j].x - leftx) / mapwidth].fire == true && bomb[i, j].timesec > 0)
                            {
                                bomb[i, j].timesec = 0;
                                checkcount++;
                            }
                            //====================================爆炸=========================================
                            if (bomb[i, j].timesec == 0 && bomb[i,j].bombdirection<=0)
                            {
                               // MessageBox.Show("bomb");
                                //map[(bomb[i, j].y - topy) / mapheight, (bomb[i, j].x - leftx) / mapwidth].tag = -1;
                                bombsplash(i, j, (bomb[i, j].y - topy) / mapheight, (bomb[i, j].x - leftx) / mapwidth);//爆炸
                                bomb[i, j].onfire = 10; //火焰持續 0.5秒
                                bomb[i, j].timesec = -1;
                                bomb[i, j].exist = false;

                                //if(player[i].bombcount<bombattacknum)
                                player[i].bombcount++;

                                uirefresh();
                            }
                        }
                    }
            }
            while (checkcount>0);

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < bombnum; j++)
                {
                    if (bomb[i, j].onfire >= 0)
                    {
                        //fireflag = true;

                        if (bomb[i, j].onfire > 0)
                            bomb[i, j].onfire--;

                        if (bomb[i, j].onfire == 0)
                        {
                            firetoroad(i, j, (bomb[i, j].y - topy) / mapheight, (bomb[i, j].x - leftx) / mapwidth);
                            bomb[i, j].onfire = -1;
                            bomb[i, j].x = -1;
                            bomb[i, j].y = -1;
                        }
                    }
                }

            Game.Invalidate();

            //if (enableflag == false && fireflag == false)
             //   bombtimer.Enabled = false;
        }

        private void mutekitimer_Tick(object sender, EventArgs e)
        {
            player[0].mutekitime--;
            if(player[0].mutekitime == 0)
            {
                player[0].mutekitime = mutekitimerload;
                mutekitimer.Enabled = false;
            }
            if (player1.Visible == true)
                player1.Visible = false;
            else
                player1.Visible = true; 
        }

        private void mutekitimer2_Tick(object sender, EventArgs e)
        {
            player[1].mutekitime--;
            if (player[1].mutekitime == 0)
            {
                player[1].mutekitime = mutekitimerload;
                mutekitimer2.Enabled = false;
            }
            if (player2.Visible == true)
                player2.Visible = false;
            else
                player2.Visible = true;
        }

        private void mutekitimer3_Tick(object sender, EventArgs e)
        {
            player[2].mutekitime--;
            if (player[2].mutekitime == 0)
            {
                player[2].mutekitime = mutekitimerload;
                mutekitimer3.Enabled = false;
            }
            if (player3.Visible == true)
                player3.Visible = false;
            else
                player3.Visible = true;
        }

        private void mutekitimer4_Tick(object sender, EventArgs e)
        {
            player[3].mutekitime--;
            if (player[3].mutekitime == 0)
            {
                player[3].mutekitime = mutekitimerload;
                mutekitimer4.Enabled = false;
            }
            if (player4.Visible == true)
                player4.Visible = false;
            else
                player4.Visible = true;
        }

        private void bombmovetimer_Tick(object sender, EventArgs e)
        {
            int countbombmove = 0;
            int mapi;
            int mapj;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < bombnum; j++)
                {
                    if (bomb[i, j].bombdirection > 0)
                    {
                        countbombmove++;
                        mapi = (bomb[i, j].y - topy) / mapheight;
                        mapj = (bomb[i, j].x - leftx) / mapwidth;
                        switch (bomb[i, j].bombdirection)
                        {
                            case 1://上                                   
                                if ((bomb[i, j].y - topy) % mapheight == 0)
                                {
                                    if (map[(mapi - 1) < 0 ? 0 : mapi - 1, mapj].tag != -1 || bomb[i, j].timesec == 0)
                                    {
                                        bomb[i, j].bombdirection = 0;
                                        map[mapi, mapj].tag = 2;
                                        map[mapi, mapj].bomblocationindex = j;
                                        break;
                                    }
                                }
                                bomb[i, j].y -= step;
                                break;
                            case 2://左
                                if ((bomb[i, j].x - leftx) % mapwidth == 0)
                                {
                                    if (map[mapi, (mapj - 1) < 0 ? 0 : mapj - 1].tag != -1 || bomb[i, j].timesec == 0)
                                    {
                                        bomb[i, j].bombdirection = 0;
                                        map[mapi, mapj].tag = 2;
                                        map[mapi, mapj].bomblocationindex = j;
                                        break;
                                    }
                                }
                                bomb[i, j].x -= step;
                                break;
                            case 3://下
                                if ((bomb[i, j].y - topy) % mapheight == 0)
                                {
                                    if (map[(mapi + 1) > 14 ? 14 : mapi + 1, mapj].tag != -1 || bomb[i, j].timesec == 0)
                                    {
                                        bomb[i, j].bombdirection = 0;
                                        map[mapi, mapj].tag = 2;
                                        map[mapi, mapj].bomblocationindex = j;
                                        break;
                                    }
                                }
                                bomb[i, j].y += step;
                                break;
                            case 4://右
                                if ((bomb[i, j].x - leftx) % mapwidth == 0)
                                {
                                    if (map[mapi, (mapj + 1) > 14 ? 14 : mapj + 1].tag != -1 || bomb[i, j].timesec == 0)
                                    {
                                        bomb[i, j].bombdirection = 0;
                                        map[mapi, mapj].tag = 2;
                                        map[mapi, mapj].bomblocationindex = j;
                                        break;
                                    }
                                }
                                bomb[i, j].x += step;
                                break;
                        }
                    }
                }
            }

            Game.Invalidate();

            if (countbombmove == 0)
                bombmovetimer.Enabled = false;

        }
        
        private void aitimer_Tick(object sender, EventArgs e)
        {
            //右上角ai 移動    
            if (player[playnum].life > 0)
            {
                if (movingflag[0] == false)
                    AI(0);//呼叫右上角ai     
                else
                {
                    detectbomb(0);
                    if ((player3.Location.X - leftx) % mapwidth == 0 && (player3.Location.Y - topy) % mapheight == 0)
                    {
                        if (aiindex[0] == wayindex[0].index)
                        {
                            movingflag[0] = false;
                            aiindex[0] = 0;
                            if (player[2].bombcount > 0)
                                if (setbomb(0) == true)
                                    bombvalue(3, player3.Location.X, player3.Location.Y, ref player[2].bombindex);
                        }
                        else if (player[playnum].y == aiway[0, aiindex[0]].valuey && player[playnum].x == aiway[0, aiindex[0]].valuex)
                            aiindex[0]++;
                        if ((Math.Abs(player[2].y - aiway[0, aiindex[0]].valuey) + Math.Abs(player[2].x - aiway[0, aiindex[0]].valuex)) >= 2)
                            movingflag[0] = false;
                        if (aimap[0, aiway[0, aiindex[0]].valuey, aiway[0, aiindex[0]].valuex].timesec < 20)
                            movingflag[0] = false;
                        if (map[aiway[0, aiindex[0]].valuey, aiway[0, aiindex[0]].valuex].fire == true)
                            movingflag[0] = false;
                        if (map[aiway[0, aiindex[0]].valuey, aiway[0, aiindex[0]].valuex].tag != -1)
                            movingflag[0] = false;
                    }

                    if (movingflag[0] == true)
                    {
                        aidirection[0] = 0;
                        if (player3.Location.Y > aiway[0, aiindex[0]].valuey * mapheight + topy)
                            aidirection[0] = 1;//上
                        else if (player3.Location.X > aiway[0, aiindex[0]].valuex * mapwidth + leftx)
                            aidirection[0] = 2;//左
                        else if (player3.Location.Y < aiway[0, aiindex[0]].valuey * mapheight + topy)
                            aidirection[0] = 3;//下
                        else if (player3.Location.X < aiway[0, aiindex[0]].valuex * mapwidth + leftx)
                            aidirection[0] = 4;//右                    
                        switch (aidirection[0])
                        {
                            case 1:
                                player3.Location = new Point(player3.Location.X, player3.Location.Y - aistep);
                                player[2].y = (player3.Location.Y - topy) / mapheight;
                                break;
                            case 2:
                                player3.Location = new Point(player3.Location.X - aistep, player3.Location.Y);
                                player[2].x = (player3.Location.X - leftx) / mapwidth;
                                break;
                            case 3:
                                player3.Location = new Point(player3.Location.X, player3.Location.Y + aistep);
                                player[2].y = (player3.Location.Y - topy) / mapheight;
                                break;
                            case 4:
                                player3.Location = new Point(player3.Location.X + aistep, player3.Location.Y);
                                player[2].x = (player3.Location.X - leftx) / mapwidth;
                                break;
                            default:
                                movingflag[0] = false;
                                break;
                        }
                    }

                }
            }

            if (player[playnum + 1].life > 0)
            {
                if (movingflag[1] == false)
                    AI(1);//呼叫右上角ai     
                else
                {
                    detectbomb(1);
                    if (player[playnum + 1].y == aiway[1, aiindex[1]].valuey && player[playnum + 1].x == aiway[1, aiindex[1]].valuex && (player4.Location.X - leftx) % mapwidth == 0 && (player4.Location.Y - topy) % mapheight == 0)
                    {
                        if (aiindex[1] == wayindex[1].index)
                        {
                            movingflag[1] = false;
                            aiindex[1] = 0;
                            if (player[3].bombcount > 0)
                                bombvalue(4, player4.Location.X, player4.Location.Y, ref player[3].bombindex);
                        }
                        else
                            aiindex[1]++;
                    }
                    else if ((player4.Location.Y - topy) % mapheight != 0 || (player4.Location.X - leftx) % mapwidth != 0 || (map[aiway[1, aiindex[1]].valuey, aiway[1, aiindex[1]].valuex].fire == false && aimap[1, aiway[1, aiindex[1]].valuey, aiway[1, aiindex[1]].valuex].timesec > 20))
                    {
                        //Console.Write(aiway[0, aiindex[0]].valuey.ToString() + "\n");                     
                        if ((aiway[1, aiindex[1]].valuey * mapheight + topy) < player4.Location.Y && map[aiway[1, aiindex[1]].valuey, aiway[1, aiindex[1]].valuex].tag == -1)
                        {//上
                            player4.Location = new Point(player4.Location.X, player4.Location.Y - aistep);
                            player[3].y = (player4.Location.Y - topy) / mapheight;
                        }
                        else if ((aiway[1, aiindex[1]].valuey * mapheight + topy) > player4.Location.Y && map[aiway[1, aiindex[1]].valuey, aiway[1, aiindex[1]].valuex].tag == -1)
                        {//下
                            player4.Location = new Point(player4.Location.X, player4.Location.Y + aistep);
                            player[3].y = (player4.Location.Y - topy) / mapheight;
                            // if ((player3.Location.Y - topy) % mapheight != 0)
                            //  player[2].y += 1;
                        }
                        else if ((aiway[1, aiindex[1]].valuex * mapwidth + leftx) < player4.Location.X && map[aiway[1, aiindex[1]].valuey, aiway[1, aiindex[1]].valuex].tag == -1)
                        {
                            player4.Location = new Point(player4.Location.X - aistep, player4.Location.Y);
                            player[3].x = (player4.Location.X - leftx) / mapwidth;
                        }
                        else if ((aiway[1, aiindex[1]].valuex * mapwidth + leftx) > player4.Location.X && map[aiway[1, aiindex[1]].valuey, aiway[1, aiindex[1]].valuex].tag == -1)
                        {
                            player4.Location = new Point(player4.Location.X + aistep, player4.Location.Y);
                            player[3].x = (player4.Location.X - leftx) / mapwidth;
                            // if ((player3.Location.X - leftx) % mapwidth != 0)
                            //  player[2].x += 1;
                        }
                        else if (aiindex[1] < wayindex[1].index)
                            aiindex[1]++;
                       // else
                           // movingflag[1] = false;
                    }

                }
            }
        }

        private void gametimer_Tick(object sender, EventArgs e)
        {
            if (player[0].pushtime > 0)
                player[0].pushtime--;
            if (player[0].pushtime == 0)
                player1_push.Visible = false;
            if (player[1].pushtime > 0)
                player[1].pushtime--;
            if (player[1].pushtime == 0)
                player2_push.Visible = false;
            if (t < warnt)
            {
                warindex = (warindex + 1) % 6;
                warning.Invalidate();
                if (t % 10 == 0 && t > 300)
                    fillmap();
            }
            if (t > 0)
            {
                t--;
                if (t % 10 == 0)
                    uirefresh();
            }
            else
            {
                gametimer.Enabled = false;
                MessageBox.Show("Lose!!!");
                Main.Location = new Point(0, 0);
                gameclose();
            }
        }

        private void warning_Paint(object sender, PaintEventArgs e)
        {
            if (t < warnt)
            {
                Graphics dr1 = Graphics.FromImage(scr1);// 圖層1
                dr1.DrawImage(war[warindex], 0, 0,warnwidth,warnheight);
                e.Graphics.DrawImage(scr1, 0, 0);
            }
        }

        private void readytimer_Tick(object sender, EventArgs e)
        {
            readyload--;
            readytime.Text = readyload.ToString();
            
            if(readyload == 0)
            {
                GC.Collect();
                setimage();
                generate_map();
                Game.Location = new Point(0, 0);
                Game.Enabled = true;
                Game.Visible = true;
                Main.Enabled = false;
                Main.Visible = false;
                Rank.Enabled = false;
                Rank.Visible = false;
                Character.Enabled = false;
                Character.Visible = false;
                Ready.Enabled = false;
                Ready.Visible = false;
                button2.Enabled = false;
                //Game.Invalidate(); 系統自動重載

                player1.Location = new Point(map[player[0].y, player[0].x].x, map[player[0].y, player[0].x].y);
                player2.Location = new Point(map[player[1].y, player[1].x].x, map[player[1].y, player[1].x].y);
                player3.Location = new Point(map[player[2].y, player[2].x].x, map[player[2].y, player[2].x].y);
                player4.Location = new Point(map[player[3].y, player[3].x].x, map[player[3].y, player[3].x].y);

                player1_push.Visible = false;
                player2_push.Visible = false;

                button1.Enabled = false;
                button1.TabStop = false;
                button2.TabStop = false;

                aitimer.Enabled = true;
                gametimer.Enabled = true;
                //setvalue();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Main.BackgroundImage = new Bitmap(Properties.Resources.Main_1000);
            Main.Location = new Point(0, 0);
            Main.Enabled = true;
            Main.Visible = true;
            Game.Enabled = false;
            Game.Visible = false;
            Rank.Enabled = false;
            Rank.Visible = false;
            Character.Enabled = false;
            Character.Visible = false;
            Ready.Enabled = false;
            Ready.Visible = false;
        }
    }
}
