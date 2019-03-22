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
    public partial class Seting : Form
    {
        public int buttonType = 0;
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        string str_sql = "";
        public Seting()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("不能为空！", "提示");
                return;
            }
            try
            {
                 if (buttonType == 2)
                {
                    str_sql = "delete from AD where ANo='" + textBox1.Text + "' and DNo='" + textBox2.Text + "'";
                }
                else if (buttonType == 3)
                {
                    str_sql = "update AD  set Num = '"+textBox3.Text+"' where ANo = '"+textBox1.Text+"' and DNo = '"+textBox2.Text+"'";
                }
                operSQL.tranSQL(str_sql);
                MessageBox.Show("编辑成功！", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告");
            }
        }
    }
}
