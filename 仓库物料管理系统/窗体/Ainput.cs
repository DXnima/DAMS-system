using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 仓库物料管理系统.窗体
{
    public partial class Ainput : Form
    {
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        方法类.operateFile operFile = new 方法类.operateFile();
        public Ainput()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            operSQL.tranSQL("exec bulidTable @table='A'");
            string cmd = "select ANo from A";
            bool Flag = operSQL.judgeCopy(cmd, "ANo", textBox1.Text); //判断重复
            if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") || (comboBox1.Text == ""))
            {
                MessageBox.Show("不能有空！", "提示");
            }
            else
            {
                if (Flag == true)
                {
                    MessageBox.Show("该物料已经存在！", "提示");
                }
                else
                {
                    string str = "insert into tableWNM(ANo,AN,Class,Prince) values ('" + textBox1.Text + "'," + "'" + textBox2.Text + "'," + "'" + comboBox1.Text + "'," + "'" + textBox3.Text + "') ";
                    operSQL.tranSQL(str);
                    string s = "exec buffet_##A";
                    operSQL.tranSQL(s);
                    MessageBox.Show("录入成功！", "提示");
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            operSQL.tranSQL("exec bulidTable @table='A'");
            string input_SQL;
            List<string> list;
            DataTable dt;
            DataSet dset;
            operFile.Open_File();
            try
            {
                dset = operFile.getData();
                dt = dset.Tables[0];
                input_SQL = "insert into tableWNM(ANo,AN,Class,Prince) values('{0}','{1}','{2}','{3}')";
                list = (from DataRow row in dt.Rows
                        select String.Format(input_SQL, row[0], row[1], row[2], row[3])).ToList();
                foreach (string item in list)
                {
                    operSQL.tranSQL(item);
                }
                string s = "exec buffet_##A";
                operSQL.tranSQL(s);
                MessageBox.Show("导入数据库成功！", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告");
            }

        }
    }
}
