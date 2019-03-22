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

namespace 仓库物料管理系统
{
    public partial class Information : Form
    {
        方法类.operateFile operFile = new 方法类.operateFile();
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        public Findin findin;
        public Datainput datainput;
        public 用户控件.Dseting dseting;
        public 用户控件.Cnumber cnumber;
        public 用户控件.Dateview dateview;
        public 窗体.Ainput ainput;
        public 窗体.Aget aget;
        public 窗体.Dinput dinput;
        public 窗体.DAchange dachange;
        public 窗体.Changepwd changepwd;
        public 窗体.Changeskin changeskin;
        public Information()
        {
            InitializeComponent();
        }
        private void Information_Load(object sender, EventArgs e)
        {
            findin = new Findin();
            datainput = new Datainput();
            dseting = new 用户控件.Dseting();
            cnumber = new 用户控件.Cnumber();
            dateview = new 用户控件.Dateview();
            ainput = new 窗体.Ainput();
            aget = new 窗体.Aget();
            dinput = new 窗体.Dinput();
            dachange = new 窗体.DAchange();
            changepwd = new 窗体.Changepwd();
            changeskin = new 窗体.Changeskin();
        }
        private void 库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dinput.ShowDialog();
        }

        private void 查询信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dachange.ShowDialog();
        }

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findin.Show();//显示窗体findin
            panel1.Controls.Clear();//清空之前加载的窗体控件
            panel1.Controls.Add(findin);//加载findin窗体控件
        }

        private void 数据导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            datainput.Show();
            panel1.Controls.Clear();
            panel1.Controls.Add(datainput);
        }

        private void 物料录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ainput.ShowDialog();
        }


        private void 信息修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dseting.Show();
            panel1.Controls.Clear();
            panel1.Controls.Add(dseting);
        }

        private void 计算器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cnumber.Show();
            panel1.Controls.Clear();
            panel1.Controls.Add(cnumber);
        }

        private void 数据可视化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dateview.Show();
            panel1.Controls.Clear();
            panel1.Controls.Add(dateview);
        }

        private void 物料领取ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aget.ShowDialog();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changepwd.ShowDialog();
        }

        private void 更换皮肤ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeskin.ShowDialog();
        }

        private void Information_Activated(object sender, EventArgs e)
        {
            Log.Text = operFile.ReadTxtFile("Log.txt");
        }

        private void 退出登陆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Information_FormClosed(object sender, FormClosedEventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(operSQL.SQLstr);
            sqlconn.Close();
            Application.Exit();
        }
    }
}
