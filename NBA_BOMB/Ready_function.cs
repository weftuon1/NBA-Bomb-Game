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
        int readyload; //5秒
        private void readyset()
        {
            Ready.BackgroundImage = new Bitmap(Properties.Resources.Ready_1000);
            readyload = 5;
            readytime.Text = readyload.ToString();
            readytimer.Enabled = true;
        }
    }

}
