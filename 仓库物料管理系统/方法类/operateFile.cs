using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 仓库物料管理系统.方法类
{
    class operateFile
    {
        public OpenFileDialog file = new OpenFileDialog();
        //手动选取打开文件
        public void Open_File()
        {
            //打开文件
            file.Filter = "文件类型(*.xlsx;*.xls;*.txt)|*.xlsx;*.xls;*.txt";
            file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.Cancel)
            {
                MessageBox.Show("导入失败！", "提示");
            }
        }

        //保存文件
        public void OutputFile(Workbook workbook = null, DataTable dt = null)
        {
            string outputName = null;
            //打开保存文件对话框
            SaveFileDialog saveTifFileDialog = new SaveFileDialog();
            saveTifFileDialog.OverwritePrompt = true;//询问是否覆盖
            saveTifFileDialog.Filter = "Excel表格(*.xls)|*.xls|Excel表格(*.xlsx)|*.xlsx|文本文件(*.txt)|*.txt";
            if (saveTifFileDialog.ShowDialog() == DialogResult.OK)
            {
                outputName = saveTifFileDialog.FileName;
                string localFilePath = saveTifFileDialog.FileName.ToString(); //获得文件路径
                string fileSuffix = System.IO.Path.GetExtension(localFilePath);
                if (fileSuffix == ".txt") SavaTxt(dt, outputName);
                else SavaXls(workbook, outputName);
            }
        }

        //datatable数据保存到txt
        public void SavaTxt(DataTable dt, string name)
        {
            StreamWriter sw = new StreamWriter(name);
            string col_txt = "";
            string row_txt = "";
            foreach (DataColumn item in dt.Columns)// dt为DataTable      
            {
                col_txt += item.ToString() + "       "; // 循环得到列名          
            }
            col_txt = col_txt.Substring(0, col_txt.Length - 1);
            sw.WriteLine(col_txt);  //写入文件                 
            foreach (DataRow item in dt.Rows)
            {
                row_txt = ""; //此处容易遗漏，导致数据的重复添加           
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row_txt += item[i].ToString() + "       "; //循环得到行数据  
                }
                row_txt = row_txt.Substring(0, row_txt.Length - 1);
                sw.WriteLine(row_txt, Encoding.UTF8);//写入文件
            }
            sw.Close();
        }


        //datatable数据转为excel
        public void SavaXls(Workbook workbook, string FileName = "")
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                workbook.SaveToFile(FileName, ExcelVersion.Version2010);
            }
            else
            {
                workbook.SaveToFile(DateTime.Now.ToString("yyyyMMddhhmmssfff"), ExcelVersion.Version2010);
            }
        }

        //读取txt文本内容
        public string ReadTxtFile(string Path)
        {
            StreamReader sr = new StreamReader(Path);
            string str = "";
            while (!sr.EndOfStream)
            {
                str = sr.ReadLine().ToString();//读取每行数据

            }
            sr.Close();
            return str;
        }

        //获取文件数据
        public DataSet getData()
        {
            //判断文件后缀
            var path = file.FileName;
            string fileSuffix = System.IO.Path.GetExtension(path);
            if (string.IsNullOrEmpty(fileSuffix))
                return null;
            else if (fileSuffix == ".txt")//txt数据转为datatable
            {
                DataSet ds = new DataSet();
                // 读出文本文件的所有行
                string[] lines = File.ReadAllLines(path, Encoding.Default);
                DataTable dt = new DataTable();
                Regex.Split(lines[0], @"\s+").Where(t => t.Trim() != "").ToList().ForEach(t => dt.Columns.Add(t.Trim()));
                for (int i = 1; i < lines.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    Regex.Split(lines[i], @"\s+").Where(t => t.Trim() != "").Select((t, index) => dr[index] = t).ToArray();
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
                return ds;
            }
            else//excel数据转为datatable
            {
                using (DataSet ds = new DataSet())
                {
                    //判断Excel文件是2003版本还是2007版本
                    string connString = "";
                    if (fileSuffix == ".xls")
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                    else
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
                    //读取excel文件
                    string sql_select = " SELECT * FROM [Sheet1$]";
                    using (OleDbConnection conn = new OleDbConnection(connString))
                    using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql_select, conn))
                    {
                        conn.Open();
                        cmd.Fill(ds);
                    }
                    if (ds == null || ds.Tables.Count <= 0) return null;
                    return ds;
                }
            }
        }
    }
}
