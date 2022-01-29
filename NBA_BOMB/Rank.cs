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

namespace NBA_BOMB
{
    public partial class Form1 : Form
    {
        struct ranktype
        {
            public string name;
            public int playnum;
            public int life;
            public int bomb;
            public int lefttime;
        };
        ranktype[] data = new ranktype[30];//可存30筆資料
        public void fileopen(int playi, int life, int bomb, int lefttime, string namepass)
        {
            if (namepass == "")
                return;
            BinaryWriter outFile = new BinaryWriter(File.Open("..//..//Rank//Rank.dat", FileMode.Append));
            outFile.Write(namepass); // 寫入 字串
            outFile.Write(playi);  // 寫入 整數
            outFile.Write(life);   // 寫入 整數
            outFile.Write(bomb);   // 寫入 整數
            outFile.Write(lefttime);   // 寫入 整數

            outFile.Close(); // 關閉檔案   
        }
        public void fileread()
        {
            Ranktitle.Text = "Name          playernumber    life     bomb       usetime\n\n";// 文字方塊 先清空
            Rankname.Text = "";
            ranklabel.Text = "";
            if (!File.Exists("..//..//Rank//Rank.dat"))
                return;

            BinaryReader inFile = new BinaryReader(File.Open("..//..//Rank//Rank.dat", FileMode.Open));

            int rankindex = 0;
            /*String my_name;                        
            int my_playi;
            int my_life;
            int my_bomb;
            int my_lefttime;*/

            while (inFile.BaseStream.Position < inFile.BaseStream.Length) // 傳回下一個可供使用的字元，但不消耗它
            {
                data[rankindex].name = inFile.ReadString(); // 讀出 字串                
                data[rankindex].playnum = inFile.ReadInt32();
                data[rankindex].life = inFile.ReadInt32();
                data[rankindex].bomb = inFile.ReadInt32();
                data[rankindex].lefttime = inFile.ReadInt32();

                rankindex++;
            }

            ranksort(rankindex);
            ranklabel.Text = "";
            Rankname.Text = "";
            int min;
            int sec;
            for (int i = 0; i < rankindex; i++)
            {
                Rankname.Text = Rankname.Text + data[i].name + "\n";

                ranklabel.Text = ranklabel.Text + (data[i].playnum + 1).ToString() + "            ";
                ranklabel.Text = ranklabel.Text + data[i].life.ToString() + "           ";
                ranklabel.Text = ranklabel.Text + data[i].bomb.ToString() + "          ";
                min = data[i].lefttime / 60;
                sec = data[i].lefttime % 60;
                if (min < 10)
                    ranklabel.Text = ranklabel.Text + "0";
                ranklabel.Text = ranklabel.Text + min.ToString() + " ";
                ranklabel.Text = ranklabel.Text + ": ";
                if(sec<10)
                    ranklabel.Text = ranklabel.Text + "0";
                ranklabel.Text = ranklabel.Text + sec.ToString();

                ranklabel.Text = ranklabel.Text + Environment.NewLine;
            }

            inFile.Close(); // 關閉檔案
        }
        public void ranksort(int n)
        {
            ranktype tmp;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (data[j].lefttime < data[i].lefttime)
                    {
                        tmp = data[i];
                        data[i] = data[j];
                        data[j] = tmp;
                    }
                }
            }
        }
    }
}