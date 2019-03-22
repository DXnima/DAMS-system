using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 仓库物料管理系统
{
    public partial class Datainput : UserControl
    {
        方法类.operateFile operFile = new 方法类.operateFile();
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        public Datainput()
        {
            InitializeComponent();
        }

        //导入数据到数据库
        public void insertSQL()
        {
            DataTable dt = operFile.getData().Tables[0];
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("请先导入文件！");
                return;
            }
            else
            {
                string input_SQL, str_SQL;
                List<string> list;
                if (comboBox1.Text == "员工信息录入")
                {
                    operSQL.tranSQL("exec bulidTable @table='S'");
                    input_SQL = "insert into tableWNM(SNo,SN,Sex,TNo,Dept) values('{0}','{1}','{2}','{3}','{4}')";
                    list = (from DataRow row in dt.Rows
                            select String.Format(input_SQL, row[0], row[1], row[2], row[3], row[4])).ToList();
                    str_SQL = "exec buffet_##S";
                }
                else if (comboBox1.Text == "物料信息录入")
                {
                    operSQL.tranSQL("exec bulidTable @table='A'");
                    input_SQL = "insert into tableWNM(ANo,AN,Class,Prince) values('{0}','{1}','{2}','{3}')";
                    list = (from DataRow row in dt.Rows
                            select String.Format(input_SQL, row[0], row[1], row[2], row[3])).ToList();
                    str_SQL = "exec buffet_##A";
                }
                else if (comboBox1.Text == "仓库信息录入")
                {
                    operSQL.tranSQL("exec bulidTable @table='D'");
                    input_SQL = "insert into tableWNM(DNo,DN) values('{0}','{1}')";
                    list = (from DataRow row in dt.Rows
                            select String.Format(input_SQL, row[0], row[1])).ToList();
                    str_SQL = "exec buffet_##D";
                }
                else if (comboBox1.Text == "部门信息录入")
                {
                    operSQL.tranSQL("exec bulidTable @table='B'");
                    input_SQL = "insert into tableWNM(BNo,BN) values('{0}','{1}')";
                    list = (from DataRow row in dt.Rows
                            select String.Format(input_SQL, row[0], row[1])).ToList();
                    str_SQL = "exec buffet_##B";
                }
                else if (comboBox1.Text == "物料入库管理")
                {
                    operSQL.tranSQL("exec bulidTable @table='AD'");
                    input_SQL = "insert into tableWNM(DNo,ANo,Num,ADTime) values('{0}','{1}','{2}','"+DateTime.Now.ToString()+"')";
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
                    str_SQL = "";
                }
                else if (comboBox1.Text == "部门员工管理")
                {
                    operSQL.tranSQL("exec bulidTable @table='BS'");
                    input_SQL = "insert into tableWNM(BNo,SNo) values('{0}','{1}')";
                    list = (from DataRow row in dt.Rows
                            select String.Format(input_SQL, row[0], row[1])).ToList();
                    str_SQL = "exec buffet_##BS";
                }
                else
                {
                    MessageBox.Show("请选择导入哪种数据！", "提示");
                    return;
                }
                try
                {
                    if (str_SQL != "")
                    {
                        foreach (string item in list)
                        {
                            operSQL.tranSQL(item);
                        }
                        operSQL.tranSQL(str_SQL);
                    }
                    MessageBox.Show("导入数据库成功！", "提示");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "警告");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            operFile.Open_File();
            dataGridView1.DataSource = null; //每次打开清空内容
            DataTable dt = operFile.getData().Tables[0];
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            insertSQL();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
