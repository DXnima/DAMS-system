using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 仓库物料管理系统.用户控件
{
    
    public partial class Dseting : UserControl
    {
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        public bool DsetFlag = true;
        public Dseting()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Dseting_Load(object sender, EventArgs e)
        {
            string str = "exec stock_Num";
            DataTable dt = operSQL.select(str).Tables[0];
            dataGridView1.DataSource = dt;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            窗体.Seting seting = new 窗体.Seting();
            seting.buttonType = 2;
            int index = dataGridView1.CurrentRow.Index; //获取选中行的行号
            seting.textBox1.Text = dataGridView1.Rows[index].Cells["物料编号"].Value.ToString();
            seting.textBox2.Text = dataGridView1.Rows[index].Cells["仓库编号"].Value.ToString();
            seting.textBox3.Text = dataGridView1.Rows[index].Cells["库存数量"].Value.ToString();
            seting.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            窗体.Seting seting = new 窗体.Seting();
            seting.buttonType = 3;
            int index = dataGridView1.CurrentRow.Index; //获取选中行的行号
            seting.textBox1.Text = dataGridView1.Rows[index].Cells["物料编号"].Value.ToString();
            seting.textBox2.Text = dataGridView1.Rows[index].Cells["仓库编号"].Value.ToString();
            seting.textBox3.Text = dataGridView1.Rows[index].Cells["库存数量"].Value.ToString();
            seting.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "exec stock_Num";
            DataTable dt = operSQL.select(str).Tables[0];
            dataGridView1.DataSource = dt;
        }
    }
}
