using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 仓库物料管理系统.方法类
{
    class operateSQL
    {
        static string SQL_str1 = "server=localhost;database=WNM;integrated security=SSPI";
        public string SQLstr
        {
            get { return SQL_str1; }
        }

        //数据库查询方法
        public DataSet select(string select)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(SQLstr);
                SqlDataAdapter sqlda = new SqlDataAdapter(select, sqlconn);
                DataSet dset = new DataSet();
                sqlda.Fill(dset);
                return dset;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告");
                return null;
            }
        }

        //数据库语句查询返回查询结果 ，字符串形式输出 
        //selectSQL 查询语句   name列名
        public string select(string selectSQL,string name)
        {
            try
            {
                string s;
                SqlConnection sqlconn = new SqlConnection(SQLstr);
                SqlDataAdapter sqlda = new SqlDataAdapter(selectSQL, sqlconn);
                DataSet dset = new DataSet();
                sqlconn.Open();
                sqlda.Fill(dset);
                s = dset.Tables[0].Rows[0][name].ToString().Trim();
                sqlconn.Close();
                return s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告");
                return null;
            }
        }

        //数据库语句操作方法
        public void tranSQL(string str)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(SQLstr);
                SqlCommand sqlcomm = new SqlCommand(str, sqlconn);
                sqlconn.Open();
                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告");
            }
        }

        //读取数据库数据
        public SqlDataReader readSQL(string str)
        {
            
            try
            {
                SqlConnection sqlconn = new SqlConnection(SQLstr);
                SqlCommand sqlcomm = new SqlCommand(str, sqlconn);
                sqlconn.Open();
                SqlDataReader sdr = sqlcomm.ExecuteReader();
                return sdr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告");
                return null;
            }
        }

        //判断数据库重复真重复 假不存在
        public bool judgeCopy(string sdrSQL,string strSQL,string strCON)
        {
            bool Flag = false;
            SqlDataReader reader =readSQL(sdrSQL);
            while (reader.Read())
            {
                if (strCON == reader[strSQL].ToString().Trim())
                {
                    Flag = true;
                    break;
                }
                else
                {
                    Flag = false;
                }
            }
            reader.Close();
            return Flag;
        }

        //数据库某一列数据存入字符数组
        //sqlStr 数据库语句 rowStr列名
        public string[] imgeNeedstring(string sqlStr,string rowStr)
        {
            DataTable dt = select(sqlStr).Tables[0];
            //string dt = select(sqlStr,rowStr);
            string[] abc = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string row = select(sqlStr).Tables[0].Rows[i][rowStr].ToString().Trim();
                abc[i] = row.ToString().Trim();
            }
            return abc;
        }


        //字符数组转decimal类型
        public Decimal[] imgeDecimal(string sqlStr, string rowStr)
        {
            string[] Content = imgeNeedstring(sqlStr,rowStr);
            Decimal[] d = new Decimal[Content.Length];
            for (int i = 0; i < Content.Length; i++)
            {
                d[i] = Convert.ToInt32(Content[i].ToString().Trim());
            }
            return d;
        }

        ///<summary>
        ///附加数据库
        ///</summary>
        public void AttachDB()
        {
            SqlConnection conn = new SqlConnection("server = localhost; database = master; integrated security = SSPI");
            SqlCommand  comm = new SqlCommand();
            conn.Open();
            comm.Connection = conn;
            comm.CommandText = "select count(*) from sys.databases where name = 'WNA'";
            int a = Convert.ToInt32(comm.ExecuteScalar());
            if (a == 1)
            {
                return;
            }
            else
            {
                try
                {
                    string Path = @"../../obj/Debug/DataBase/";
                    FileInfo file = new FileInfo(Path);
                    string mdfPath = file.FullName + "WNMData.mdf";//mdf文件的路径
                    string ldfPath = file.FullName + "WNMData.ldf";
                    comm.CommandText = "sp_attach_db";//系统数据库master 中的一个附加数据库存储过程。
                    comm.Parameters.Add(new SqlParameter(@"dbname", SqlDbType.NVarChar));
                    comm.Parameters[@"dbname"].Value = "WNM";
                    comm.Parameters.Add(new SqlParameter(@"filename1", SqlDbType.NVarChar));  //一个主文件mdf，一个或者多个日志文件ldf，或次要文件ndf
                    comm.Parameters[@"filename1"].Value = mdfPath;
                    comm.Parameters.Add(new SqlParameter(@"filename2", SqlDbType.NVarChar));
                    comm.Parameters[@"filename2"].Value = ldfPath;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.ExecuteNonQuery();

                   //MessageBox.Show("附加数据库成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch //(Exception ex)
                {
                   // MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
