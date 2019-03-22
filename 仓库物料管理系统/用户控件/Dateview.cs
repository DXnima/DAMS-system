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

    /***************************
     * 
     * 查询数据库转为datatable; sqlStr为SQL查询语句
     * DataTable dt = operSQL.select(sqlStr).Tables[0];
     * 
     * 将datatable某一列转为字符数组
     * string[] arrRate = dt.AsEnumerable().Select(d => d.Field<string>("arry")).ToArray(); 
     * 
     * 将datatable某一列转为十进制数组
     * Decimal[] arrRate = dt.AsEnumerable().Select(d => d.Field<Decimal>("arry")).ToArray();
     * 
    *************************/


    public partial class Dateview : UserControl
    {
        方法类.operateSQL operSQL = new 方法类.operateSQL();
        public Dateview()
        {
            InitializeComponent();
        }

        public string selectImge()
        {
            if (comboBox1.Text == "入库物料类型数量统计") return "exec ImgeInputSum";
            else if (comboBox1.Text == "出库物料类型数量统计") return "exec ImgeOutputSum";
            else if (comboBox1.Text == "各仓库现有库存情况统计") return "exec ImageKuCun";
            else return "";
        }

        public string ValueImage()
        {
            if (comboBox1.Text == "入库物料类型数量统计") return "物料类型";
            else if (comboBox1.Text == "出库物料类型数量统计") return "物料类型";
            else if (comboBox1.Text == "各仓库现有库存情况统计") return "仓库名称";
            else return "";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //GetColumnImage 绘制柱状图
            if (comboBox1.Text == "")
            {
                MessageBox.Show("请选取数据！", "提示");
            }
            else
            {
                string title = comboBox1.Text;
                string[] strName = operSQL.imgeNeedstring(selectImge(), ValueImage());
                Decimal[] deNum = operSQL.imgeDecimal(selectImge(), "总数量");
                Bitmap bt = 方法类.Countimage.GetColumnImage(title, strName, deNum);
                pictureBox1.Image = Image.FromHbitmap(bt.GetHbitmap());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //GetPieImage  绘制饼图
            if (comboBox1.Text == "")
            {
                MessageBox.Show("请选取数据！", "提示");
            }
            else
            {
                string title = comboBox1.Text;
                string[] strName = operSQL.imgeNeedstring(selectImge(), ValueImage());
                Decimal[] deNum = operSQL.imgeDecimal(selectImge(), "总数量");
                Bitmap bt = 方法类.Countimage.GetPieImage(title, strName, deNum);
                pictureBox1.Image = Image.FromHbitmap(bt.GetHbitmap());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //绘制曲线图  GetLineImage
            if (comboBox1.Text == "")
            {
                MessageBox.Show("请选取数据！", "提示");
            }
            else
            {
                string title = comboBox1.Text;
                string[] strName = operSQL.imgeNeedstring(selectImge(), ValueImage());
                Decimal[] deNum = operSQL.imgeDecimal(selectImge(), "总数量");
                Bitmap bt = 方法类.Countimage.GetLineImage(title, strName, deNum);
                pictureBox1.Image = Image.FromHbitmap(bt.GetHbitmap());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //GetMulLineImage 绘制多组数据的曲线图
            if (comboBox1.Text == "")
            {
                MessageBox.Show("请选取数据！", "提示");
            }
            else
            {
                string title = "出入库物料类型数量统计";
                string[] strName = operSQL.imgeNeedstring("exec ImgeInputSum", "物料类型");//横轴多组如
                Decimal[] deNum1, deNum2;
                deNum1 = operSQL.imgeDecimal("exec ImgeInputSum", "总数量");
                deNum2 = operSQL.imgeDecimal("exec ImgeOutputSum", "总数量");
                Decimal[][] deNum = new Decimal[2][];
                deNum[0] = deNum1;
                deNum[1] = deNum2;
                string[] strName1 = { "入库", "出库" }; //不同类型
                Bitmap bt = 方法类.Countimage.GetMulLineImage(title, strName, deNum, strName1);
                pictureBox1.Image = Image.FromHbitmap(bt.GetHbitmap());
            }
        }
    }
}
