using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 仓库物料管理系统.窗体
{
    public partial class Changepwd : Form
    {
        string User, Pwd;//用户名，密码
        bool flagshow = false;
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        public Changepwd()
        {
            InitializeComponent();
        }

        private void Changepwd_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
            textBox4.PasswordChar = '*';
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {        //复选框被勾选，明文显示
                textBox2.PasswordChar = new char();
                textBox3.PasswordChar = new char();
                textBox4.PasswordChar = new char();
            }
            else
            {
                //复选框被取消勾选，密文显示        
                textBox2.PasswordChar = '*';
                textBox3.PasswordChar = '*';
                textBox4.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("信息填写不完整！", "提示");
                return;
            }
            string cmd = "select UserName,UserPed from Table_User";
            SqlDataReader reader = operSQL.readSQL(cmd);
            while (reader.Read())//从数据库读取用户信息
            {
                User = reader["UserName"].ToString();
                Pwd = reader["UserPed"].ToString();
                if (User.Trim() == textBox1.Text & Pwd.Trim() == textBox2.Text)
                {
                    flagshow = true;//用户名与密码匹配
                }
            }
            reader.Close();
            if (flagshow == true)
            {
                if ((textBox3.Text != textBox4.Text) || textBox3.Text.Length < 6)
                {
                    MessageBox.Show("两次输入密码不一致或密码长度不足6位以上!");
                    return;
                }
                else
                {
                    string strUpdte = "update Table_User set UserPed='" + textBox4.Text + "' where UserName='" + textBox1.Text + "'";
                    operSQL.tranSQL(strUpdte);
                    MessageBox.Show("密码修改成功,请重新登陆!");
                    this.Close();
                    Application.Exit();
                    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    login log = new login();
                    log.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("用户不存在或旧密码错误！", "提示");
                return;
            }
        }
    }
}
