using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Spire.Xls;

namespace 仓库物料管理系统
{
    public partial class Findin : UserControl
    {
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        方法类.operateFile operFile = new 方法类.operateFile();
        public Findin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //清空查询条件按钮
            dataGridView1.DataSource=null;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "";
            checkBox1.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SerOrder();
        }

        private void SerOrder()
        {
            dataGridView1.DataSource=null;//初始化datagridview
            //查询语句参数值
            string[] paras = { textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim(), textBox6.Text.Trim(), textBox7.Text.Trim(), comboBox1.Text.Trim()};
            //数据表列名
            string[] columns = { "物料编号", "物料名称", "物料类型", "单价", "仓库编号", "仓库名称", "出入库数量", "出入库标志"};
            string sqlStr = "select row_number() over(order by 物料编号)as ID ,* from ioDnum";
            string[] condition = { };//查询语句条件
            List<string> condition2 = condition.ToList();//数组转列表
            for (int i = 0; i < paras.Length; i++)
            {
                //生成查询语句&查询条件
                if (paras[i] == "")
                {
                    continue;
                }
                condition2.Add(columns[i]+" like '%" + paras[i] + "%'");
            }
            condition = condition2.ToArray();
            if (condition.Length > 0)
            {//有查询条件
                string result = String.Join(" and ", condition);
                string timeStr = "convert(varchar,出入库时间,120) like '" + dateTimePicker1.Text + "'";
                if (checkBox1.Checked == false) //该控件状态为未选中
                {
                    sqlStr += (" where " + result + ";");
                }
                else
                    sqlStr += (" where " + result + " and convert(varchar,出入库时间,120) like '%" + dateTimePicker1.Text.Trim() + "%';");
            }
            else
            {
                if (checkBox1.Checked == false) //该控件状态为未选中
                {
                    sqlStr = "select row_number() over(order by 物料编号)as ID ,* from ioDnum";
                }
                else
                    sqlStr = "select row_number() over(order by 物料编号)as ID ,* from ioDnum where convert(varchar,出入库时间,120) like '%" + dateTimePicker1.Text.Trim() + "%';";
            }
            //显示数据库数据
            DataTable dt = operSQL.select(sqlStr).Tables[0];
            dataGridView1.DataSource = dt;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dataGridView1.DataSource=operSQL.select("exec stock_Num").Tables[0];
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("数据为空,无法导出！", "提示");
                return;
            }
            else
            {
                DataTable dt = this.dataGridView1.DataSource as DataTable;
                Workbook workbook = new Workbook();//创建一个Excel文档
                Worksheet sheet = workbook.Worksheets[0];//获取第一个工作表
                sheet.InsertDataTable(dt, true, 1, 1);//将datatable导入到工作表，数据从工作表的第一行第一列开始写入
                sheet.AllocatedRange.AutoFitColumns(); //设置自适应列宽
                sheet.Rows[0].Style.Color = Color.Yellow; //设置第一行的填充颜色
                operFile.OutputFile(workbook,dt);//保存文档
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dataGridView1.DataSource = operSQL.select("exec select_BSMeber").Tables[0];
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dataGridView1.DataSource = operSQL.select("exec select_SA").Tables[0];
        }
    }
}
