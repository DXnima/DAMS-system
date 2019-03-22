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
    public partial class DAchange : Form
    {
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        方法类.operateFile operFile = new 方法类.operateFile();
        public DAchange()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string num = "exec ADnum @an='" + comboBox1.Text + "',@dn='" + comboBox2.SelectedValue.ToString().Trim() + "'";
            DataSet set = operSQL.select(num);

            if ((set == null) || (set.Tables.Count == 0) || (set.Tables.Count == 1 && set.Tables[0].Rows.Count == 0)) //判断有无数据
            {
                MessageBox.Show("转出仓库没有此物料！", "提示");
            }
            else
            {
                string textB = textBox1.Text;
                string sNum = operSQL.select(num, "num");
                if (comboBox2.Text == comboBox3.Text)
                {
                    MessageBox.Show("相同仓库不能转库！", "提示");
                }
                else if (Convert.ToDouble(textB) > Convert.ToDouble(sNum))
                {
                    MessageBox.Show("转出仓库的此物料库存不足！", "提示");
                }
                else
                {
                    try
                    {
                        string str = "insert into RD(ANo,DNo1,DNo2,remNum) values ('" + comboBox1.SelectedValue.ToString().Trim() + "'," + "'" + comboBox2.Text.Trim() + "'," + "'" + comboBox3.Text.Trim() + "'," + "'" + textBox1.Text + "') ";
                        operSQL.tranSQL(str);
                        MessageBox.Show("转库成功！", "提示");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "警告");
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string input_SQL;
            List<string> list;
            DataTable dt;
            DataSet dset;
            operFile.Open_File();
            try
            {
                dset = operFile.getData();
                dt = dset.Tables[0];
                input_SQL = "insert into RD(ANo,DNo1,DNo2,remNum) values('{0}','{1}','{2}','{3}')";
                list = (from DataRow row in dt.Rows
                        select String.Format(input_SQL, row[0], row[1], row[2], row[3])).ToList();
                foreach (string item in list)
                {
                    operSQL.tranSQL(item);
                }
                MessageBox.Show("导入数据库成功！", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告");
            }

        }

        private void DAchange_Load(object sender, EventArgs e)
        {
            DataTable dt1 = operSQL.select("select * from A").Tables[0];
            DataTable dt2 = operSQL.select("select * from D").Tables[0];
            DataTable dt3 = operSQL.select("select * from D").Tables[0];
            comboBox1.DataSource = dt1;
            comboBox2.DataSource = dt2;
            comboBox3.DataSource = dt3;
            comboBox1.DisplayMember = "AN";
            comboBox1.ValueMember = "ANo";
            comboBox2.DisplayMember = "DNo";
            comboBox2.ValueMember = "DN";
            comboBox3.DisplayMember = "DNo";
        }
    }
}
