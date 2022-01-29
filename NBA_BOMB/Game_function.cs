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
        public static string name;
        Pen myPen = new Pen(Color.Black, 1);
        Bitmap scr = new Bitmap(1000, 700);
        PictureBox[] play = new PictureBox[4];

        const int num = 15;//17*17 地圖
        const int topy = 30;//左上角y值
        const int leftx = 350;//左上角x值
        const int mapwidth = 40;//寬度
        const int mapheight = 40;//高度
        const int step = 4;//控制移動速度(40的因數)
        const int aistep = 4;//控制移動速度(40的因數)
        const int bombnum = 10;
        const int bombattacknum = 2;
        const int mutekitimerload = 20; //被炸彈炸到的無敵時間長度
        const int pushtimerload = 50;
        const int ainum = 2;
        const int playnum = 2;
        const int warnwidth = 272;
        const int warnheight = 112;
        const int warnt = 1200;//warning倒數時間 120秒
        const int aiwaymax = 300;//aiway陣列大小
        Bitmap scr1 = new Bitmap(warnwidth, warnheight);
        int t = 1800; //遊戲時間
        int warindex = 0;
        bool[] movingflag = new bool[ainum];
        int[] aiindex = new int[ainum];
        int btomi, btomj;
        int[] aidirection = new int[ainum];
        int warni, warnj, warndirection;
        //int player1_x = 1;
        //int player1_y = 1;

        Image pass = new Bitmap(Properties.Resources.brownground);
        Image trash = new Bitmap(Properties.Resources.tree4);
        Image notpass = new Bitmap(Properties.Resources.icewall);
        Image bomber = new Bitmap(Properties.Resources.bomb01);
        Image fire = new Bitmap(Properties.Resources.onfire);
        Image warnwall = new Bitmap(Properties.Resources.redground);
        Image[] tool = new Image[10];
        Image[] war = new Image[6];

        Random fixRand = new Random();

        public struct maptype
        {
            public int x;
            public int y;
            public int tag;
            public int item;
            public bool fire;
            public int firetoroadflag;
            public int bombplayer;
            public int bomblocationindex;
            public bool warn;
        };

        public struct bombtype
        {
            public int x;
            public int y;
            public int timesec;
            public int distance;
            public bool exist;
            public int onfire;
            public int bombdirection; //上：1 ; 左：2 ; 下：3 ; 右：4            
        };

        public struct playertype
        {
            public int playernum;
            public int x;
            public int y;
            public int bombindex;
            public int bombdistance;
            public int bombcount;
            public int mutekitime;
            public int life;
            public int direction; //上：1 ; 左：2 ; 下：3 ; 右：4
            public int pushtime;
        };

        public struct aimaptype
        {
            public int pass;
            public int value;
            public int ratvalue;
            public int timesec;
        };

        public struct aiwaytype
        {
            public int valuex;
            public int valuey;
        };

        public struct wayindextype
        {
            public int index;
            public int success;
            public int direct;
            public int back;
        };

        maptype[,] map = new maptype[num, num];
        bombtype[,] bomb = new bombtype[4, bombnum];
        playertype[] player = new playertype[4];
        aimaptype[,,] aimap = new aimaptype[ainum, num, num];
        aiwaytype[,] aiway = new aiwaytype[2, aiwaymax];
        wayindextype[] wayindex = new wayindextype[2];
        private void setimage()
        {
            tool[0] = new Bitmap(Properties.Resources.brownground);
            tool[1] = new Bitmap(Properties.Resources.bomb14);
            tool[2] = new Bitmap(Properties.Resources.love);
            tool[3] = new Bitmap(Properties.Resources.push);
            tool[4] = new Bitmap(Properties.Resources.distance);
            war[0] = new Bitmap(Properties.Resources.war1);
            war[1] = new Bitmap(Properties.Resources.war2);
            war[2] = new Bitmap(Properties.Resources.war3);
            war[3] = new Bitmap(Properties.Resources.war4);
            war[4] = new Bitmap(Properties.Resources.war5);
            war[5] = new Bitmap(Properties.Resources.war6);
        }

        private void setvalue()
        {
            warni = 13;
            warnj = 0;
            warndirection = 4;//1:上,2:左,3:下,4:右
            t = 1800;
            play[0] = player1;
            play[1] = player2;
            play[2] = player3;
            play[3] = player4;
            for (int i = 0; i < 4; i++)
                play[i].Visible = true;

            for (int i = 0; i < num; i++)
                for (int j = 0; j < num; j++)
                {
                    map[i, j].x = leftx + j * mapwidth;
                    map[i, j].y = topy + i * mapheight;
                    map[i, j].tag = 0;
                    map[i, j].item = 0; //沒有特殊道具
                    map[i, j].fire = false;
                    map[i, j].firetoroadflag = 0;
                    map[i, j].warn = false;
                }

            for (int i = 0; i < 4; i++)
            {
                player[i].playernum = i + 1;
                player[i].bombindex = 0;
                player[i].bombdistance = 1;
                if (i >= 2)
                    player[i].bombcount = 1;
                else
                    player[i].bombcount = bombattacknum;
                
                player[i].life = 5;
                player[i].mutekitime = mutekitimerload;
                player[i].pushtime = 0;
                switch (i)
                {
                    case 0:
                        player[i].x = 1;
                        player[i].y = 1;
                        break;
                    case 1:
                        player[i].x = 13;
                        player[i].y = 13;
                        break;
                    case 2:
                        player[i].x = 13;
                        player[i].y = 1;
                        break;
                    case 3:
                        player[i].x = 1;
                        player[i].y = 13;
                        break;
                    default:
                        break;
                }
            }

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < bombnum; j++)
                {
                    bomb[i, j].x = -1;
                    bomb[i, j].y = -1;
                    bomb[i, j].distance = 1;
                    bomb[i, j].timesec = -1;
                    bomb[i, j].exist = false;
                    bomb[i, j].onfire = -1;
                    bomb[i, j].bombdirection = 0;
                }

            for (int i = 0; i < ainum; i++)
                for (int j = 0; j < num; j++)
                    for (int k = 0; k < num; k++)
                    {
                        aimap[i, j, k].pass = 0;
                        aimap[i, j, k].value = 0;
                        aimap[i, j, k].ratvalue = 0;
                    }
            for (int i = 0; i < 2; i++)
            {
                wayindex[i].index = 0;
                wayindex[i].success = 0;
                wayindex[i].direct = 0;
                wayindex[i].back = 0;
                for (int j = 0; j < aiwaymax; j++)
                {
                    aiway[i, j].valuex = 0;
                    aiway[i, j].valuey = 0;
                }
            }
            for (int i = 0; i < ainum; i++)
            {
                aiindex[i] = 0;
                movingflag[i] = false;
            }
            for (int i = 0; i < ainum; i++)
                aidirection[i] = 0;
        }
        private void generate_map()
        {
            //起始=0, 通(沙路)=-1, 不通(冰牆)=-2, 樹=1, 玩家起始位置四角=-3 ,炸彈=2            
            int[,] x = new int[num, num];

            for (int a = 0; a < num; a++)
                for (int b = 0; b < num; b++)
                {
                    if (a == 0 || a == num - 1 || b == 0 || b == num - 1 || (a == (num - 1) / 2 && b == (num - 1) / 2)) //不通=-2
                        x[a, b] = -2;
                    else if ((a == (num - 1) / 2 - 1 || a == (num - 1) / 2 || a == (num - 1) / 2 + 1) && (b == (num - 1) / 2 - 1 || b == (num - 1) / 2 || b == (num - 1) / 2 + 1)) //通=-1
                        x[a, b] = -1;
                    else
                        x[a, b] = 0; //起始=0
                }

            int tmp, rc, dc, lc, indexx, indexy;
            for (int i = 0; i < 4; i++)
            {
                if (i == 0 || i == 1)
                {
                    if (i == 0)
                    {
                        indexx = 1;
                        indexy = 1;
                        rc = (num - 1) / 2 - 2;
                        dc = (num - 1) / 2 - 2;
                    }
                    else
                    {
                        indexx = 8;
                        indexy = 8;
                        rc = (num - 1) / 2 - 2;
                        dc = (num - 1) / 2 - 2;
                    }
                    while (rc != 0 || dc != 0)
                    {
                        tmp = fixRand.Next(0, 4);
                        switch (tmp)
                        {
                            case 0: //right
                                if (rc > 0)
                                {
                                    x[indexy, indexx + 1] = -1;
                                    indexx++;
                                    rc--;
                                }
                                break;
                            case 1: //down
                                if (dc > 0)
                                {
                                    x[indexy + 1, indexx] = -1;
                                    indexy++;
                                    dc--;
                                }
                                break;
                            case 2: //left
                                if (dc > 0 && rc > 0 && rc != 5)
                                {
                                    x[indexy, indexx - 1] = -1;
                                    indexx--;
                                    rc++;
                                }
                                break;
                            case 3: //up
                                if (dc > 0 && rc > 0 && dc != 5)
                                {
                                    x[indexy - 1, indexx] = -1;
                                    indexy--;
                                    dc++;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    if (i == 2)
                    {
                        indexx = 13;
                        indexy = 1;
                        lc = (num - 1) / 2 - 2;
                        dc = (num - 1) / 2 - 2;
                    }
                    else
                    {
                        indexx = 6;
                        indexy = 8;
                        lc = (num - 1) / 2 - 2;
                        dc = (num - 1) / 2 - 2;
                    }
                    while (lc != 0 || dc != 0)
                    {
                        tmp = fixRand.Next(0, 4);
                        switch (tmp)
                        {
                            case 0: //left
                                if (lc > 0)
                                {
                                    x[indexy, indexx - 1] = -1;
                                    indexx--;
                                    lc--;
                                }
                                break;
                            case 1: //down
                                if (dc > 0)
                                {
                                    x[indexy + 1, indexx] = -1;
                                    indexy++;
                                    dc--;
                                }
                                break;
                            case 2: //right
                                if (dc > 0 && lc > 0 && lc != 5)
                                {
                                    x[indexy, indexx + 1] = -1;
                                    indexx++;
                                    lc++;
                                }
                                break;
                            case 3: //up
                                if (dc > 0 && lc > 0 && dc != 5)
                                {
                                    x[indexy - 1, indexx] = -1;
                                    indexy--;
                                    dc++;
                                }
                                break;
                        }
                    }
                }

            }

            x[1, 1] = -3; x[1, 13] = -3; x[13, 1] = -3; x[13, 13] = -3; x[1, 2] = -3; x[2, 2] = -3; x[13, 2] = -3; x[12, 2] = -3; x[1, 12] = -3; x[2, 12] = -3; x[13, 12] = -3; x[12, 12] = -3;//玩家起始位置四角=-3


            int count = 0;
            for (int i = 1; i < num - 1; i++)
                for (int j = 1; j < num - 1; j++)
                    if (x[i, j] == 0)
                        count++;
            int m;
            int n;
            for (int i = 0; i < Convert.ToInt64(count / 2); i++)
            {
                do
                {
                    m = fixRand.Next(1, num - 1);
                    n = fixRand.Next(1, num - 1);
                }
                while (x[m, n] != 0);
                x[m, n] = -2;
            }

            count = 0;
            for (int i = 1; i < num - 1; i++)
                for (int j = 1; j < num - 1; j++)
                    if (x[i, j] == 0 || x[i, j] == -1)
                        count++;

            for (int i = 0; i < Convert.ToInt64(count / 5*3); i++)
            {
                do
                {
                    m = fixRand.Next(1, num - 1);
                    n = fixRand.Next(1, num - 1);
                }
                while (x[m, n] != 0 && x[m, n] != -1);
                x[m, n] = 1;
            }

            for (int i = 1; i < num - 1; i++)
                for (int j = 1; j < num - 1; j++)
                    if (x[i, j] == 0)
                        x[i, j] = -1;

            x[1, 1] = -1; x[1, 13] = -1; x[13, 1] = -1; x[13, 13] = -1; x[1, 2] = -1; x[2, 2] = -1; x[13, 2] = -1; x[12, 2] = -1; x[1, 12] = -1; x[2, 12] = -1; x[13, 12] = -1; x[12, 12] = -1;//玩家起始位置四角=-1

            int randnum;
            for (int i = 1; i < num - 1; i++)
                for (int j = 1; j < num - 1; j++)
                {
                    if (x[i, j] == 1)//樹的地方放特殊道具
                    {
                        randnum = fixRand.Next(0, 1001);
                        if (randnum >= 0 && randnum <= 300)
                            map[i, j].item = 0;//沒有特殊道具                        
                        else if (randnum > 300 && randnum <= 400)
                            map[i, j].item = 1;//炸彈數增加                        
                        else if (randnum > 400 && randnum <= 550)
                            map[i, j].item = 2;//補血
                        else if (randnum > 550 && randnum <= 700)
                            map[i, j].item = 3;//推炸彈
                        else
                            map[i, j].item = 4;//增加炸彈範圍
                    }
                }

            for (int a = 0; a < num; a++)
                for (int b = 0; b < num; b++)
                    map[a, b].tag = x[a, b];

        }
        public void bombvalue(int playernum, int x, int y, ref int index)
        {
            /*int countexitst = 0 ;
            for(int i=0;i<bombnum;i++)
                if
            */


            bombtimer.Enabled = true;
            if (map[(y - topy) / mapheight, (x - leftx) / mapwidth].tag != 2)
            {
                bomb[playernum - 1, index].exist = true;
                bomb[playernum - 1, index].distance = player[playernum - 1].bombdistance;
                bomb[playernum - 1, index].x = x;
                bomb[playernum - 1, index].y = y;
                bomb[playernum - 1, index].timesec = 30;
                map[(y - topy) / mapheight, (x - leftx) / mapwidth].bombplayer = playernum;
                map[(y - topy) / mapheight, (x - leftx) / mapwidth].bomblocationindex = index;
                index = (index + 1) % bombnum;

                map[(y - topy) / mapheight, (x - leftx) / mapwidth].tag = 2;

                if (player[playernum - 1].bombcount > 0)
                    player[playernum - 1].bombcount--;

                uirefresh();

                Game.Invalidate();
            }
        }
        public void bombsplash(int i, int j, int mapi, int mapj)
        {
            map[mapi, mapj].fire = true;
            map[mapi, mapj].tag = -1;

            map[mapi, mapj].bombplayer = -1;
            map[mapi, mapj].bomblocationindex = -1;

            for (int l = mapj - 1; l >= mapj - bomb[i, j].distance; l--)
            {
                if (map[mapi, l].tag == -2)
                    break;
                if (map[mapi, l].tag == 1)
                {
                    map[mapi, l].tag = -1;
                    map[mapi, l].fire = true;
                    break;
                }
                else
                {
                    map[mapi, l].tag = -1;
                    map[mapi, l].fire = true;
                }
            }


            for (int r = mapj + 1; r <= mapj + bomb[i, j].distance; r++)
            {
                if (map[mapi, r].tag == -2)
                    break;
                if (map[mapi, r].tag == 1)
                {
                    map[mapi, r].tag = -1;
                    map[mapi, r].fire = true;
                    break;
                }
                else
                {
                    map[mapi, r].tag = -1;
                    map[mapi, r].fire = true;
                }
            }

            for (int u = mapi - 1; u >= mapi - bomb[i, j].distance; u--)
            {
                if (map[u, mapj].tag == -2)
                    break;
                if (map[u, mapj].tag == 1)
                {
                    map[u, mapj].tag = -1;
                    map[u, mapj].fire = true;
                    break;
                }
                else
                {
                    map[u, mapj].fire = true;
                    map[u, mapj].tag = -1;
                }
            }

            for (int d = mapi + 1; d <= mapi + bomb[i, j].distance; d++)
            {
                if (map[d, mapj].tag == -2)
                    break;
                if (map[d, mapj].tag == 1)
                {
                    map[d, mapj].tag = -1;
                    map[d, mapj].fire = true;
                    break;
                }
                else
                {
                    map[d, mapj].tag = -1;
                    map[d, mapj].fire = true;
                }
            }
        }

        public void firetoroad(int i, int j, int mapi, int mapj)
        {
            map[mapi, mapj].fire = false;
            map[mapi, mapj].firetoroadflag = 1;

            for (int l = mapj - 1; l >= mapj - bomb[i, j].distance; l--)
            {
                if (map[mapi, l].tag == -2)
                    break;
                if (map[mapi, l].fire == true)
                {
                    map[mapi, l].fire = false;
                    map[mapi, l].firetoroadflag = 1;
                }
            }

            for (int r = mapj + 1; r <= mapj + bomb[i, j].distance; r++)
            {
                if (map[mapi, r].tag == -2)
                    break;
                if (map[mapi, r].fire == true)
                {
                    map[mapi, r].fire = false;
                    map[mapi, r].firetoroadflag = 1;
                }
            }
        

            for (int u = mapi - 1; u >= mapi - bomb[i, j].distance; u--)
            {
                if (map[u, mapj].tag == -2)
                    break;
                if (map[u, mapj].fire == true)
                {
                    map[u, mapj].fire = false;
                    map[u, mapj].firetoroadflag = 1;
                }
            }

            for (int d = mapi + 1; d <= mapi + bomb[i, j].distance; d++)
            {
                if (map[d, mapj].tag == -2)
                    break;
                if (map[d, mapj].fire == true)
                {
                    map[d, mapj].fire = false;
                    map[d, mapj].firetoroadflag = 1;
                }
                    
            }
        }

        public void uirefresh()
        {
            player1_life.Text = player[0].life.ToString();
            player2_life.Text = player[1].life.ToString();
            player3_life.Text = player[2].life.ToString();
            player4_life.Text = player[3].life.ToString();
            player1_bomb.Text = player[0].bombcount.ToString();
            player2_bomb.Text = player[1].bombcount.ToString();
            player3_bomb.Text = player[2].bombcount.ToString();            
            player4_bomb.Text = player[3].bombcount.ToString();
            if (t % 10 == 0)
            {
                minute.Text = "0" + ((t / 10) / 60).ToString();
                if ((t / 10) % 60 < 10)
                    second.Text = "0" + ((t / 10) % 60).ToString();
                else
                    second.Text = ((t / 10) % 60).ToString();
            }
        }

        public void player1_specialitem(int playernum, int item)
        {
            switch (item)
            {
                case 1://增加炸彈數量
                    player[playernum - 1].bombcount++;
                    uirefresh();
                    break;
                case 2://增加血量
                    if (player[playernum - 1].life < 5)
                        player[playernum - 1].life++;
                    uirefresh();
                    break;
                case 3://推炸彈效果timer啟動，開始倒數
                    player[playernum - 1].pushtime += 50;
                    //pushtimer.Enabled = true;
                    player1_push.Visible = true;
                    break;
                case 4://增加炸彈範圍                    
                    player[playernum - 1].bombdistance += 1;
                    break;
            }
        }

        public void player2_specialitem(int playernum, int item)
        {
            switch (item)
            {
                case 1://增加炸彈數量
                    player[playernum - 1].bombcount++;
                    uirefresh();
                    break;
                case 2://增加血量
                    player[playernum - 1].life++;
                    uirefresh();
                    break;
                case 3://推炸彈效果timer啟動，開始倒數，每吃到道具延長效果時間5秒
                    player[playernum - 1].pushtime += pushtimerload;
                    //pushtimer2.Enabled = true;
                    player2_push.Visible = true;
                    break;
                case 4://增加炸彈範圍                    
                    player[playernum - 1].bombdistance += 1;
                    break;
            }
        }        

        public bool iswaymove(int playno, int index, int direction)//上：1 ; 左：2 ; 下：3 ; 右：4
        {
            bool wayflag = false;
            int mapi = (bomb[playno, index].y - topy) / mapheight;
            int mapj = (bomb[playno, index].x - leftx) / mapwidth;
            //mapi , mapj為炸彈所在地圖座標
            if (bomb[playno, index].timesec > 0)
            {
                switch (direction)
                {
                    case 1://上                    
                        if (map[mapi - 1, mapj].tag == -1)
                            wayflag = true;
                        break;
                    case 2://左
                        if (map[mapi, mapj - 1].tag == -1)
                            wayflag = true;
                        break;
                    case 3://下
                        if (map[mapi + 1, mapj].tag == -1)
                            wayflag = true;
                        break;
                    case 4://右
                        if (map[mapi, mapj + 1].tag == -1)
                            wayflag = true;
                        break;
                }
            }
            if (wayflag == false)
                bomb[playno, index].bombdirection = 0;
            else
            {
                bomb[playno, index].bombdirection = direction;
                map[mapi, mapj].tag = -1;
            }

            return wayflag;//true->有路  false->沒路
        }
        private void AI(int aino)
        {
            aiindex[aino] = 0;
            
            for (int i = 0; i < num; i++)
                for (int j = 0; j < num; j++)
                    aimap[aino,i,j].pass = 0;                      

            aimapanalyze(aino, player[playnum+aino].y, player[playnum + aino].x);//建立aimap 1 為 電腦玩家可到地方 0->不可到
           
            bombdanger(aino);//偵測炸彈
            aimapvalue(aino);//寫入aimap.value值
            
            int besti = 0, bestj = 0,comparevalue = -100000;
            for(int i=0;i< num;i++)            
                for(int j = 0;j< num;j++)
                    if(aimap[aino,i,j].value> comparevalue)
                    {
                        comparevalue = aimap[aino, i, j].value;
                        besti = i;
                        bestj = j;
                    }

            if (map[player[aino + playnum].y - 1, player[aino + playnum].x].tag != -1 && map[player[aino + playnum].y + 1, player[aino + playnum].x].tag != -1 && map[player[aino + playnum].y, player[aino + playnum].x - 1].tag != -1 && map[player[aino + playnum].y, player[aino + playnum].x + 1].tag != -1)//如果四周都被鎖起來
            {
                besti = player[aino + playnum].y;
                bestj = player[aino + playnum].x;
            }
            
            textBox1.Text = " ";
            textBox1.Text = besti.ToString() + ","+bestj.ToString() + "  ";                                    
            movingflag[aino] = true;

            ratwalk(aino, player[aino + playnum].y, player[aino + playnum].x,besti,bestj);
        }

        public void aimapanalyze(int aino,int i,int j)
        {           
            if (map[i, j].tag == -1 || (player[aino + playnum].y == i && player[aino + playnum].x == j))//通(沙路)
            {
                if ((player[aino + playnum].y == i && player[aino + playnum].x == j) && map[i, j].tag != -1)                                    
                    aimap[aino, i, j].pass = 0;                
                else
                    aimap[aino, i, j].pass = 1;
                if (aimap[aino, i - 1, j].pass == 0)
                    aimapanalyze(aino, i - 1, j);//上
                if (aimap[aino, i, j - 1].pass == 0)
                    aimapanalyze(aino, i, j - 1);//左
                if (aimap[aino, i + 1, j].pass == 0)
                    aimapanalyze(aino, i + 1, j);//下
                if (aimap[aino, i, j + 1].pass == 0)
                    aimapanalyze(aino, i, j + 1);//右
            }
            else                           
                return;
            /*
            Console.Write("\n******\n");
            for(int y = 0;y< num;y++)
            {
                for (int k = 0; k < num; k++)
                    Console.Write(aimap[aino,y,k].pass.ToString()+" ");
                Console.Write("\n");
            }
            */
        }

        public void bombdanger(int aino)
        {
            int bombi, bombj;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < bombnum; j++)
                    if (bomb[i, j].exist = true && bomb[i, j].timesec > 0)
                    {
                        bombi = ((bomb[i, j].y) - topy) / mapheight;
                        bombj = ((bomb[i, j].x) - leftx) / mapwidth;

                        aimap[aino, bombi, bombj].pass = 2;

                        for (int l = bombj - 1; l >= bombj - bomb[i, j].distance; l--)
                        {
                            if (map[bombi, l].tag == -2 || map[bombi, l].tag == 1)
                                break;
                            else if(aimap[aino, bombi, l].pass == 1)
                                aimap[aino, bombi, l].pass = 2;//有被炸的危險                                                                
                            
                        }

                        for (int r = bombj + 1; r <= bombj + bomb[i, j].distance; r++)
                        {
                            if (map[bombi, r].tag == -2|| map[bombi, r].tag == 1)
                                break;                            
                            else if(aimap[aino, bombi, r].pass == 1)
                                aimap[aino,bombi,r].pass = 2;//有被炸的危險                                                                                                                        
                        }

                        for (int u = bombi - 1; u >= bombi - bomb[i, j].distance; u--)
                        {
                            if (map[u, bombj].tag == -2|| map[u, bombj].tag == 1)
                                break;                                  
                            else if(aimap[aino, u, bombj].pass == 1)
                                aimap[aino, u, bombj].pass = 2;//有被炸的危險                                                        
                        }

                        for (int d = bombi + 1; d <= bombi + bomb[i, j].distance; d++)
                        {
                            if (map[d, bombj].tag == -2|| map[d, bombj].tag == 1)
                                break;
                            else if(aimap[aino, d, bombj].pass == 1)
                                aimap[aino, d, bombj].pass = 2;//有被炸的危險                                                                                    
                        }
                    }

            for (int i = 0; i < num; i++)
                for (int j = 0; j < num; j++)
                    if (map[i, j].fire == true && aimap[aino, i, j].pass == 1)
                        aimap[aino, i, j].pass = 2;
            /*
            Console.Write("\n******\n");
            for (int y = 0; y < num; y++)
            {
                for (int k = 0; k < num; k++)
                    Console.Write(aimap[aino, y, k].pass.ToString() + " ");
                Console.Write("\n");
            }
            */
        }        

        public void aimapvalue(int aino)
        {
            int ran = fixRand.Next(0, 100);            
            int i, j, x, y,aii,aij;
            int value;
            for(i=0;i< num;i++)
            {
                for (j = 0; j < num; j++)
                {                    
                    if (i == 0 || j == 0 || i == 14 || j == 14 || aimap[aino, i, j].pass == 0)
                        value = -100000;
                    else
                    {
                        value = 30000;
                        if (ran <= 30 && t >= warnt && aino==1)
                            value += fixRand.Next(0, 1000);
                        //判斷四周炸彈數
                        if (aimap[aino, i, j].pass == 2)//炸彈炸的到                                                    
                            value = value - 50000;
                        //周圍有人可以炸
                        for (x = 0; x < playnum; x++)
                            if (Math.Abs(i - player[x].y) < 2 && Math.Abs(j - player[x].x) < 2)
                            {
                                value += 30000;
                                if (i == player[x].y)
                                    value += 30000;
                                if (j == player[x].x)
                                    value += 30000;
                            }
                        //周遭可能會被炸到
                        for (x = -1; x <= 1; x++)
                            for (y = -1; y <= 1; y++)
                                if (aimap[aino, i + x, j + y].pass == 2)
                                    value -= 300;
                                    
                        //周遭有樹可以炸
                        for (x = -1; x <= 1; x++)
                            for (y = -1; y <= 1; y++)
                                if (map[i + x, j + y].tag == 1)
                                {
                                    value += 10000;
                                    break;
                                }
                        //周遭為冰牆
                        for (x = -1; x <= 1; x++)
                            for (y = -1; y <= 1; y++)
                                if (map[i + x, j + y].tag == -2)
                                    value -= 100;

                        aij = player[playnum + aino].x;
                        aii = player[playnum + aino].y;

                        int countdanger = 0;
                        for (x = -1; x <= 1; x++)
                            for (y = -1; y <= 1; y++)
                                if (map[i + x, j + y].tag == -2)
                                    countdanger++;
                        if (countdanger == 3)
                            value -= 1000;

                        if (t < warnt)
                        {
                            value -= (warnt - t) / 400 * Math.Abs(i - 7) * Math.Abs(i - 7);
                            value -= (warnt - t) / 400 * Math.Abs(j - 7) * Math.Abs(j - 7);
                        }

                        if (aino == 0 && (Math.Abs(i - player[3].y) < 2 || Math.Abs(j - player[3].x) < 2))
                            value -= 10000;
                        if ((i < 7 && j < 7) || (i > 7 && j > 7))
                            value += 3000;
                        else
                            value += (Convert.ToInt32(Math.Sqrt((aii - i) * (aii - i) + (aij - j) * (aij - j)))) * 2;                        
                    }
                    aimap[aino, i, j].value = value;                    
                }
            }
            /*
            Console.Write("\n****** value ******\n");
            for(i=0;i< num;i++)
            {
                for (j = 0; j < num; j++)
                    Console.Write((aimap[aino,i,j].value/1000).ToString()+" ");
                Console.Write("\n");
            }
            */
        }        

        public bool ratwalk(int aino,int aiy,int aix,int goaly,int goalx)//老鼠走迷宮(aix,aiy)目前座標,(goalx,goaly)目的地坐標
        {
            int t = 0,minvalue,direction;//計算次數
            wayindex[aino].index = 0;//index歸零

            for(int i=0;i<aiwaymax;i++)
            {
                aiway[aino, i].valuex = num;//將座標都設成15初始化(方便DEBUG)
                aiway[aino, i].valuey = num;//將座標都設成15初始化(方便DEBUG)
            }

            for(int i=0;i< num;i++)//建立一開始老鼠走迷宮的ratvalue
                for(int j=0;j< num;j++)
                {
                    if (map[i, j].tag != -1)//走不過去
                        aimap[aino, i, j].ratvalue = -1;
                    else if (i == aiy && j == aix)//沙路
                        aimap[aino, i, j].ratvalue = 1;
                    else
                        aimap[aino, i, j].ratvalue = 0;
                }

            do
            {                               
                for(int i=0;i<wayindex[aino].index;i++)
                {
                    if (aiway[aino, i].valuex == aix && aiway[aino, i].valuey == aiy)
                    {
                        wayindex[aino].index = i;
                        break;
                    }
                }

                aiway[aino, wayindex[aino].index].valuex = aix;
                aiway[aino, wayindex[aino].index].valuey = aiy;

                if ((aix == goalx && aiy == goaly) || t > 200)//判斷是否到達終點
                    break;

                direction = 1;
                minvalue = aimap[aino, aiy - 1, aix].ratvalue;//上
                if ((minvalue == -1 || minvalue > aimap[aino, aiy + 1, aix].ratvalue) && aimap[aino, aiy + 1, aix].ratvalue != -1)//下
                {
                    direction = 2;
                    minvalue = aimap[aino, aiy + 1, aix].ratvalue;
                }
                if ((minvalue == -1 || minvalue > aimap[aino, aiy, aix - 1].ratvalue) && aimap[aino, aiy, aix - 1].ratvalue != -1)//左
                {
                    direction = 3;
                    minvalue = aimap[aino, aiy, aix - 1].ratvalue;
                }
                if (minvalue == -1 || minvalue > aimap[aino, aiy, aix + 1].ratvalue && aimap[aino, aiy, aix + 1].ratvalue != -1)//右
                {
                    direction = 4;
                    minvalue = aimap[aino, aiy, aix + 1].ratvalue;
                }

                if (minvalue == -1)
                    break;
                else
                {
                    aimap[aino, aiy, aix].ratvalue += 1;
                    switch (direction)
                    {
                        case 1://上
                            aiy -= 1;
                            break;
                        case 2://下
                            aiy += 1;
                            break;
                        case 3://左
                            aix -= 1;
                            break;
                        case 4://右
                            aix += 1;
                            break;
                        default:
                            break;
                    }
                }

                t++;//計算次數
                wayindex[aino].index++;
            }
            while (true);
            /*
            if (aino == 1)
            {
                Console.Write("\n");
                for (int i = 0; i <= wayindex[aino].index; i++)
                    Console.Write(aiway[aino, i].valuey.ToString() + " " + aiway[aino, i].valuex.ToString() + "\n");
            }
            */
            if (aiy == goaly && aix == goalx)
                return true;
            else
                return false;
        }

        public void detectbomb(int aino)
        {
            for (int i = 0; i < num; i++)
                for (int j = 0; j < num; j++)
                    aimap[aino, i, j].timesec = 30;//初始化

            int mapi, mapj;
            for(int i=0;i< 4;i++)
                for(int j=0;j<bombnum;j++)
                {
                    if(bomb[i,j].exist = true && bomb[i,j].timesec>0)
                    {
                        mapi = (bomb[i, j].y - topy) / mapheight;
                        mapj = (bomb[i, j].x - leftx) / mapwidth;
                        aimap[aino, mapi, mapj].timesec = bomb[i, j].timesec;
                    }
                }
        }

        public bool setbomb(int aino)
        {
            int aiy = player[playnum + aino].y, aix = player[playnum + aino].x;
            bool set = false;

            for (int l = 1; l <= player[playnum + aino].bombdistance; l++)
                if (map[aiy, aix - l].tag == -2)
                    break;
                else if (map[aiy, aix - l].tag == 1)
                {
                    set = true;
                    break;
                }

            for (int r = 1; r <= player[playnum + aino].bombdistance; r++)
                if (map[aiy, aix + r].tag == -2)
                    break;
                else if (map[aiy, aix + r].tag == 1)
                {
                    set = true;
                    break;
                }

            for (int u = 1; u <= player[playnum + aino].bombdistance; u++)
                if (map[aiy - u, aix].tag == -2)
                    break;
                else if (map[aiy - u, aix].tag == 1)
                {
                    set = true;
                    break;
                }
            for (int d = 1; d <= player[playnum + aino].bombdistance; d++)
                if (map[aiy + d, aix].tag == -2)
                    break;
                else if (map[aiy + d, aix].tag == 1)
                {
                    set = true;
                    break;
                }

            return set;
        }

        public void isdied()
        {
            if (gametimer.Enabled == true)
                for (int i = 0; i < 4; i++)
                    if (player[i].life == 0)
                        play[i].Visible = false;
            if (player[0].life == 0 && player[1].life == 0 && (player[2].life != 0 || player[3].life != 0))
            {
                gametimer.Enabled = false;
                MessageBox.Show("Lose!!!");
                Main.Location = new Point(0, 0);
                gameclose();
            }
            else if (player[2].life == 0 && player[3].life == 0)
            {
                if ((player[0].life == 0 && player[1].life != 0) || (player[1].life == 0 && player[0].life != 0))
                {
                    int playernumber;
                    if (player[0].life == 0)
                        playernumber = 1;
                    else
                        playernumber = 0;
                    gametimer.Enabled = false;
                    if (InputBox.Show("請輸入名字", "名字：", true, ref name))
                    {
                        if (name != "")
                            fileopen(playernumber, player[playernumber].life, player[playernumber].bombcount, (1800 - t) / 10, name);
                        Main.Location = new Point(0, 0);
                        //输入成功后的操作                                
                    }
                    gameclose();
                }
            }
        }

        public void gameclose()
        {
            gametimer.Enabled = false;
            aitimer.Enabled = false;
            uptimer.Enabled = false;
            downtimer.Enabled = false;
            lefttimer.Enabled = false;
            righttimer.Enabled = false;
            uptimer2.Enabled = false;
            downtimer2.Enabled = false;
            lefttimer2.Enabled = false;
            righttimer2.Enabled = false;
            timer1.Enabled = false;
            timer2.Enabled = false;
            mutekitimer.Enabled = false;
            mutekitimer2.Enabled = false;
            mutekitimer3.Enabled = false;
            mutekitimer4.Enabled = false;
            bombmovetimer.Enabled = false;
            bombtimer.Enabled = false;
            Game.Enabled = false;
            Game.Visible = false;
            Rank.Enabled = false;
            Rank.Visible = false;
            Character.Enabled = false;
            Character.Visible = false;
            Ready.Enabled = false;
            Ready.Visible = false;
            button1.Enabled = true;
            button2.Enabled = true;
            Main.Enabled = true;
            Main.Visible = true;
        }
        public void fillmap()
        {
            switch (warndirection)
            {
                case 1://上
                    warni -= 1;
                    map[warni, warnj].warn = true;
                    map[warni, warnj].tag = -2;
                    if (warnj + warni == 14)
                        warndirection = 2;
                    break;
                case 2://左
                    warnj -= 1;
                    map[warni, warnj].warn = true;
                    map[warni, warnj].tag = -2;
                    if (warnj == warni)
                        warndirection = 3;
                    break;
                case 3://下
                    warni += 1;
                    map[warni, warnj].warn = true;
                    map[warni, warnj].tag = -2;
                    if (warni - warnj == 11)
                        warndirection = 4;
                    break;
                case 4://右                                                            
                    warnj += 1;
                    map[warni, warnj].warn = true;
                    map[warni, warnj].tag = -2;
                    if (warnj == warni)
                        warndirection = 1;
                    break;
            }

            if (map[warni, warnj].warn == true)
            {
                map[warni, warnj].item = 0; //沒有特殊道具
                map[warni, warnj].fire = false;
                map[warni, warnj].firetoroadflag = 0;
                Game.Invalidate();
            }
        }
    }
}