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
    public partial class login : Form
    {
        string User, Pwd;//用户名，密码
        bool flagshow = false;
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        方法类.operateFile operFile = new 方法类.operateFile();
        public login()
        {
            InitializeComponent();
            textBox1.Text = operFile.ReadTxtFile("UserName.txt");
            //if (textBox1.Text != "")
            //{
            //    string strPwd = "select UserPed from Table_User where UserName='" + textBox1.Text + "'";
            //    textBox2.Text = operSQL.select(strPwd, "UserPed");
            //}
        }

        //记录登陆日志
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

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Register reg = new Register();
            reg.ShowDialog();
        }//显示注册界面

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            string a = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd = "select UserName,UserPed from Table_User";
                SqlDataReader reader = operSQL.readSQL(cmd);
                while (reader.Read())//从数据库读取用户信息
                {
                    User = reader["UserName"].ToString();
                    Pwd = reader["UserPed"].ToString();
                    if (User.Trim() == textBox1.Text & Pwd.Trim() == textBox2.Text)
                    {
                        flagshow = true;//已经存在
                    }
                }
                reader.Close();
                if (flagshow == true)
                {
                    string strLog = "登录用户：" + textBox1.Text + "    登录时间：" + DateTime.Now;
                    string strName = textBox1.Text;
                    MessageBox.Show("登陆成功,欢迎你" + textBox1.Text + "!");
                    Rfile("Log.txt", strLog);
                    Rfile("UserName.txt", strName);
                    this.Hide();
                    Information infor = new Information();
                    infor.ShowDialog();
                }
                else
                {
                    MessageBox.Show("用户不存在或密码错误！", "提示");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告");
            }
        }
    }
}
