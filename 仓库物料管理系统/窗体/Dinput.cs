using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 仓库物料管理系统.窗体
{
    public partial class Dinput : Form
    {
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        方法类.operateFile operFile = new 方法类.operateFile();
        public Dinput()
        {
            InitializeComponent();
        }


        public void inputAD()
        {
            operSQL.tranSQL("exec bulidTable @table='AD'");
            string input_SQL;
            List<string> list;
            DataTable dt;
            DataSet dset;
            dset = operFile.getData();
            dt = dset.Tables[0];
            input_SQL = "insert into tableWNM(DNo,ANo,Num,ADTime) values('{0}','{1}','{2}','" + DateTime.Now.ToString() + "')";
            list = (from DataRow row in dt.Rows
                    select String.Format(input_SQL, row[0], row[1], row[2])).ToList();
            foreach (string item in list)
            {
                operSQL.tranSQL(item);
            }
            string AD = "exec buffet_##AD";
            DataTable dtAD = operSQL.select(AD).Tables[0];
            input_SQL = "insert into AD(DNo,ANo,Num) values('{0}','{1}','{2}')";
            list = (from DataRow row in dtAD.Rows
                    select String.Format(input_SQL, row[0], row[1], row[2])).ToList();
            foreach (string item in list)
            {
                operSQL.tranSQL(item);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            operSQL.tranSQL("exec bulidTable @table='AD'");
            if ((textBox1.Text == "") || (comboBox1.Text == "") || (comboBox2.Text == ""))
            {
                MessageBox.Show("不能有空！", "提示");
            }
            else
            {
                try
                {
                    string str = "insert into tableWNM(DNo,ANo,Num,ADTime) values ('" + comboBox1.SelectedValue.ToString().Trim() + "'," + "'" + comboBox2.SelectedValue.ToString().Trim() + "'," + "'" + textBox1.Text + "','" + DateTime.Now.ToString() + "') ";
                    operSQL.tranSQL(str);
                    string AD = "exec buffet_##AD";
                    DataTable dtAD = operSQL.select(AD).Tables[0];
                    string input_SQL = "insert into AD(DNo,ANo,Num) values('{0}','{1}','{2}')";
                    List<string> list = (from DataRow row in dtAD.Rows
                                         select String.Format(input_SQL, row[0], row[1], row[2])).ToList();
                    foreach (string item in list)
                    {
                        operSQL.tranSQL(item);
                    }
                    MessageBox.Show("入库成功！", "提示");
                }
                catch
                {
                    MessageBox.Show("入库失败,可能该物料已经入库了！", "提示");
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            operFile.Open_File();
            try
            {
                inputAD();
                MessageBox.Show("导入数据库成功！", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告");
            }

        }

        private void Dinput_Load(object sender, EventArgs e)
        {
            DataTable dt1 = operSQL.select("select * from D").Tables[0];
            DataTable dt2 = operSQL.select("select * from A").Tables[0];
            comboBox1.DataSource = dt1;
            comboBox2.DataSource = dt2;
            comboBox1.DisplayMember = "DN";
            comboBox1.ValueMember = "DNo";
            comboBox2.DisplayMember = "AN";
            comboBox2.ValueMember = "ANo";
        }
    }
}
