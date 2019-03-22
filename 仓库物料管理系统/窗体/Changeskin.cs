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
    public partial class Changeskin : Form
    {
        Sunisoft.IrisSkin.SkinEngine SkinEngine = new Sunisoft.IrisSkin.SkinEngine();
        List<string> Skins;
        public Changeskin()
        {
            InitializeComponent();
        }

        private void Changeskin_Load(object sender, EventArgs e)
        {
            //加载所有皮肤列表
            Skins = Directory.GetFiles(Application.StartupPath + @"\Skins\", "*.ssk").ToList();
            Skins.ForEach(x =>
            {
                dataGridView1.Rows.Add(Path.GetFileNameWithoutExtension(x));
            });
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                //加载皮肤
                SkinEngine.SkinFile = Skins[dataGridView1.CurrentRow.Index];
                SkinEngine.Active = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SkinEngine.Active = false;
        }

        private void dataGridView1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();                 //实例化
            toolTip1.Show("提示内容", dataGridView1);   //绑定控件和提示信息
        }
    }
}
