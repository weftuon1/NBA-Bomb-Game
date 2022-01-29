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
    public partial class InputBox : Form
    {   
        private InputBox()
        {
            InitializeComponent();
        }
        public String getValue()
        {
            return textBox1.Text;
        }
        public static bool Show(String title, String inputTips, bool isPassword, ref String value)
        {
            InputBox ib = new InputBox();            
            if (title != null)            
                ib.Text = title;          
            if (inputTips != null)            
                ib.label1.Text = inputTips;            
            
            if (ib.ShowDialog() == DialogResult.OK)
            {
                value = ib.getValue();
                ib.Dispose();
                return true;
            }
            else
            {
                ib.Dispose();
                return true;
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {                        
            Form1.name = getValue();            
            if (Form1.name != "")                            
                this.Close();            
            else
                MessageBox.Show("空");            
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Form1.name = "";
            this.Close();
        }
    }
}
