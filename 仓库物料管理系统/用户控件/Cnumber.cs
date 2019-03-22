using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 仓库物料管理系统.用户控件
{
    public partial class Cnumber : UserControl
    {
        bool isSecStart;
        方法类.MyCalcu objCalcu;
        public Cnumber()
        {
            InitializeComponent();
            isSecStart = false;
            objCalcu = new 方法类.MyCalcu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isSecStart)
            {
                objCalcu.A = Convert.ToDouble(textBox1.Text);
                textBox1.Text = "";
                isSecStart = false;
            }
            Button obj = (Button)sender;
            if (obj.Text == ".")
            {
                if (textBox1.Text.IndexOf('.') < 0)
                {
                    textBox1.Text += obj.Text;
                }
                else
                { }
            }
            else
                textBox1.Text += obj.Text;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            isSecStart = true;
            objCalcu.Flag = 1;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            isSecStart = true;
            objCalcu.Flag = 2;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            isSecStart = true;
            objCalcu.Flag = 3;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            isSecStart = true;
            objCalcu.Flag = 4;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            objCalcu.B = Convert.ToDouble(textBox1.Text);
            textBox1.Text = objCalcu.Calcu().ToString();
        }

        private void button12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isSecStart)
            {
                textBox1.Text = "";
                isSecStart = false;
            }
            if (e.KeyChar == '+')
            {
                objCalcu.A = Convert.ToDouble(textBox1.Text);
                objCalcu.Flag = 1;
                isSecStart = true;
                e.Handled = true;
            }
            else if (e.KeyChar == '\r')
            {
                objCalcu.B = Convert.ToDouble(textBox1.Text);
                textBox1.Text = objCalcu.Calcu().ToString();
            }
            else if (!Char.IsNumber(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.')
            {
                if (textBox1.Text.IndexOf('.') >= 0)
                {
                    e.Handled = true;
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isSecStart)
            {
                textBox1.Text = "";
                isSecStart = false;
            }
            if (e.KeyChar == '+')
            {
                objCalcu.A = Convert.ToDouble(textBox1.Text);
                objCalcu.Flag = 1;
                isSecStart = true;
                e.Handled = true;
            }
            else if (e.KeyChar == '\r')
            {
                objCalcu.B = Convert.ToDouble(textBox1.Text);
                textBox1.Text = objCalcu.Calcu().ToString();
            }
            else if (!Char.IsNumber(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.')
            {
                if (textBox1.Text.IndexOf('.') >= 0)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
