using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 仓库物料管理系统
{
    public partial class Register : Form
    {
        bool flagRegister;
        public bool UserFlag; //定义标志位，来确认用户是否存在
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        public Register()
        {
            InitializeComponent();
        }


        private void Rfile(string FileName, string str)
        {
            if (!File.Exists(FileName))                    //判断日志文件是否存在
            {
                File.Create(FileName);                     //创建日志文件
            }
            //string strLog = "登录用户：" + textBox1.Text + "    登录时间：" + DateTime.Now;
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                //创建StreamWriter对象
                StreamWriter sw = new StreamWriter(FileName);
                sw.WriteLine(str);              //写入日志
                sw.Close();
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string cmd = "select UserName from Table_User";
            bool UserFlag;
            UserFlag=operSQL.judgeCopy(cmd, "UserName", textBox1.Text);
            if (UserFlag == true)
                label3.Text = "*用户已存在，请重新输入！";
            else label3.Text = "*恭喜你，该用户名可以使用。";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Length >= 3) && (textBox1.Text.Length <= 12) && (textBox2.Text.Length >= 6))
            {
                flagRegister = true;
            }
            else
            {
                MessageBox.Show("用户名长度范围为3～12之间,密码长度6位以上！", "范围提示");
                return;
            }
            if (UserFlag == true)
            {
                MessageBox.Show("用户已经存在！");
                return;
            }
            if (flagRegister == true)
            {
                string cmd = "insert into Table_User(UserName,UserPed) values ('" + textBox1.Text + "'," + "'" + textBox2.Text + "') ";
                operSQL.tranSQL(cmd);
                Rfile("UserName.txt", textBox1.Text);
                MessageBox.Show("注册成功！点击确定，返回登录界面。", "提示");
                this.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            login  log = new login();
            log.Show();
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            label4.Text = "密码长度6位以上";
        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            label4.Text = "";
        }
    }
}
