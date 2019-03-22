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
    public partial class Aget : Form
    {
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        方法类.operateFile operFile = new 方法类.operateFile();
        public Aget()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string num = "exec ADnum @an='" + comboBox2.Text + "',@dn='" + comboBox3.Text + "'";
            DataSet set = operSQL.select(num);
            if ((set == null) || (set.Tables.Count == 0) || (set.Tables.Count == 1 && set.Tables[0].Rows.Count == 0)) //判断有无数据
            {
                MessageBox.Show("该仓库没有此物料！", "提示");
            }
            else
            {
                string textB = textBox1.Text;
                string sNum = operSQL.select(num, "num");
                if (Convert.ToDouble(textB) > Convert.ToDouble(sNum))
                {
                    MessageBox.Show("该仓库此物料库存不足！", "提示");
                }
                else
                {
                    try
                    {
                        string str = "insert into SA(SNo,ANo,DNo,getNum) values ('" + comboBox1.Text.Trim() + "'," + "'" + comboBox2.SelectedValue.ToString().Trim() + "'," + "'" + comboBox3.SelectedValue.ToString().Trim() + "'," + "'" + textBox1.Text + "') ";
                        operSQL.tranSQL(str);
                        MessageBox.Show("领取成功！", "提示");
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
                input_SQL = "insert into SA(SNo,ANo,DNo,getNum) values('{0}','{1}','{2}','{3}')";
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

        private void Aget_Load(object sender, EventArgs e)
        {
            DataTable dt1 = operSQL.select("select SNo from S").Tables[0];
            DataTable dt2 = operSQL.select("select * from A").Tables[0];
            DataTable dt3 = operSQL.select("select * from D").Tables[0];
            comboBox1.DataSource = dt1;
            comboBox2.DataSource = dt2;
            comboBox3.DataSource = dt3;
            comboBox1.DisplayMember = "SNo";
            comboBox2.DisplayMember = "AN";
            comboBox2.ValueMember = "ANo";
            comboBox3.DisplayMember = "DN";
            comboBox3.ValueMember = "DNo";
        }
    }
}
