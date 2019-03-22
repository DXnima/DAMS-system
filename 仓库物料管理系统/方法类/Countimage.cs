using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 仓库物料管理系统.方法类
{
    class Countimage
    {
        #region // 颜色，画框，
        /// <summary>
        /// 生成随机颜色
        /// </summary>
        /// <returns></returns>
        private static Color GetRandomColor(int seed)
        {
            Random random = new Random(seed);
            int r = 0;
            int g = 0;
            int b = 0;
            r = random.Next(0, 230);
            g = random.Next(0, 230);
            b = random.Next(0, 235);
            Color randomcolor = Color.FromArgb(r, g, b);
            return randomcolor;
        }
        /// <summary>
        /// 绘制区域框，框何其阴影
        /// </summary>
        /// <param name="image">图形</param>
        /// <param name="rect">矩形框对象</param>
        /// <returns>图形</returns>
        private static Bitmap DrawRectangle(Bitmap image, Rectangle rect)
        {
            Bitmap Image = image;
            Graphics g = Graphics.FromImage(Image);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try
            {
                Rectangle rn = new Rectangle(rect.X + 3, rect.Y + 3, rect.Width, rect.Height);
                SolidBrush brush1 = new SolidBrush(Color.FromArgb(233, 234, 249));
                SolidBrush brush2 = new SolidBrush(Color.FromArgb(221, 213, 215));
                //
                g.FillRectangle(brush2, rn);
                g.FillRectangle(brush1, rect);
                return Image;
            }
            finally
            {
                g.Dispose();
            }
        }
        #endregion
        #region //绘制图例框，绘制扇形
        /// <summary>
        /// 绘制图例信息
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="rect">第一个矩形框</param>
        /// <param name="c">颜色</param>
        /// <param name="DesStr">图例说明文字</param>
        /// <param name="f">文字样式</param>
        /// <param name="i">图例说明序号</param>
        /// <returns>图形</returns>
        private static Bitmap DrawDes(Bitmap image, Rectangle rect, Color c, string DesStr, Font f, int i)
        {
            Bitmap Image = image;
            Graphics g = Graphics.FromImage(Image);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try
            {
                SolidBrush brush = new SolidBrush(c);
                //
                Rectangle R = new Rectangle(rect.X, rect.Y + 18 * i, rect.Width, rect.Height);
                Point p = new Point(rect.X + 12, rect.Y + 18 * i);
                //❀颜色矩形框
                g.FillRectangle(brush, R);
                //文字说明
                g.DrawString(DesStr, f, new SolidBrush(Color.Black), p);
                return Image;
            }
            finally
            {
                g.Dispose();
            }
        }
        //绘制扇形
        private static Bitmap DrawPie(Bitmap image, Rectangle rect, Color c, int Angle1, int Angle2)
        {
            Bitmap Image = image;
            Graphics g = Graphics.FromImage(Image);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try
            {
                SolidBrush brush = new SolidBrush(c);
                //
                Rectangle R = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
                g.FillPie(brush, R, Angle1, Angle2);
                return Image;
            }
            finally
            {
                g.Dispose();
            }
        }
        #endregion
        #region//绘制基本图形
        /// <summary>
        /// 生成图片，统一设置图片大小、背景色,图片布局，及标题
        /// </summary>
        /// <returns>图片</returns>
        private static Bitmap GenerateImage(string Title)
        {
            //图片大小：宽度、高度
            int width = 750;
            int height = 435;
            //标题
            Point PTitle = new Point(30, 20);
            Font f1 = new Font("宋体", 10, FontStyle.Bold);
            //线
            Point PLine1 = new Point(40, 40);
            Point PLine2 = new Point(390, 40);
            Pen pen = new Pen(new SolidBrush(Color.FromArgb(8, 34, 231)), 1.5f);
            //主区域,主区域图形
            Rectangle RMain1 = new Rectangle(20, 55, 515, 345);
            Rectangle RMain2 = new Rectangle(25, 60, 510, 335);
            //图例区域
            Rectangle RDes1 = new Rectangle(545, 55, 200, 345);
            //图例说明
            string Des = "图例说明：";
            Font f2 = new Font("新宋体", 9, FontStyle.Bold);
            Point PDes = new Point(545, 65);
            //图例信息，后面的x坐标上累加20
            Rectangle RDes2 = new Rectangle(545, 90, 10, 10);
            Bitmap image = new Bitmap(width, height);
            //
            Graphics g = Graphics.FromImage(image);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try
            {
                //设置背景色、绘制边框
                g.Clear(Color.White);
                g.DrawRectangle(new Pen(Color.Black), 0, 0, width - 1, height - 1);
                //绘制标题、线
                g.DrawString(Title, f1, new SolidBrush(Color.Black), PTitle);
                g.DrawLine(pen, PLine1, PLine2);

                //主区域
                image = DrawRectangle(image, RMain1);
                //图例区域
                image = DrawRectangle(image, RDes1);
                //“图例说明”
                g.DrawString(Des, f2, new SolidBrush(Color.Black), PDes);
                //return 
                return image;
            }
            finally
            {
                g.Dispose();
            }
        }
        #endregion
        #region //绘制饼状图************************
        /// <summary>
        /// 计算数值综合
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        private static decimal Sum(decimal[] Value)
        {
            decimal t = 0;
            foreach (decimal d in Value)
            {
                t += d;
            }
            return t;
        }
        /// <summary>
        /// 计算各项比例
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        private static decimal[] GetItemRate(decimal[] Value)
        {
            decimal sum = Sum(Value);
            decimal[] ItemRate = new decimal[Value.Length];
            for (int i = 0; i < Value.Length; i++)
            {
                ItemRate[i] = Value[i] / sum;
            }
            return ItemRate;
        }
        /// <summary>
        /// 根据比例，计算各项角度值
        /// </summary>
        /// <param name="ItemRate"></param>
        /// <returns></returns>
        private static int[] GetItemAngle(decimal[] ItemRate)
        {
            int[] ItemAngel = new int[ItemRate.Length];
            for (int i = 0; i < ItemRate.Length; i++)
            {
                decimal t = 360 * ItemRate[i];
                ItemAngel[i] = Convert.ToInt32(t);
            }
            return ItemAngel;
        }
        /// <summary>
        /// 绘制饼图(主要是分析不同类型的数值所占比例)，参数有图的标题，项目名称，项目的数值，名称和数值都是长度对应的
        /// </summary>
        /// <param name="Title">图的标题</param>
        /// <param name="ItemName">项目名称</param>
        /// <param name="ItemValue">项目的数值</param>
        /// <returns>Bitmap图形</returns>
        public static Bitmap GetPieImage(string Title, string[] ItemName, decimal[] ItemValue)
        {
            Bitmap image = GenerateImage(Title);
            //
            //主区域图形
            Rectangle RMain = new Rectangle(40, 80, 380, 300);
            //图例信息
            Rectangle RDes = new Rectangle(550, 90, 10, 10);
            Font f = new Font("新宋体", 9, FontStyle.Bold);
            Graphics g = Graphics.FromImage(image);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try
            {
                //分析数据，绘制饼图和图例说明
                decimal[] ItemRate = GetItemRate(ItemValue);
                int[] ItemAngle = GetItemAngle(ItemRate);
                int Angle1 = 0;
                int Angle2 = 0;
                int len = ItemValue.Length;
                Color c = new Color();
                //3D
                g.DrawPie(new Pen(Color.Black), RMain, 0F, 360F);
                g.DrawPie(new Pen(Color.Black), new Rectangle(RMain.X, RMain.Y + 10, RMain.Width, RMain.Height), 0F, 360F);
                g.FillPie(new SolidBrush(Color.Black), new Rectangle(RMain.X, RMain.Y + 10, RMain.Width, RMain.Height), 0F, 360F);
                //绘制
                for (int i = 0; i < len; i++)
                {
                    Angle2 = ItemAngle[i];
                    //if (c != GetRandomColor(i))
                    c = GetRandomColor(i);
                    SolidBrush brush = new SolidBrush(c);
                    string DesStr = ItemName[i] + "(" + (ItemRate[i] * 100).ToString(".00") + "%" + ")" + ItemValue[i].ToString(".00");
                    //
                    DrawPie(image, RMain, c, Angle1, Angle2);
                    Angle1 += Angle2;
                    DrawDes(image, RDes, c, DesStr, f, i);
                }
                return image;
            }
            finally
            {
                g.Dispose();
            }
        }
        #endregion
        #region //获取Y轴坐标数据
        /*
        坐标轴实现算法描述：
         * X轴坐标根据项目数量把X轴均等分，有效长度350，
         * Y轴有效长度280，平分为10个等分，即有十个点；
         * Y轴的数值算法：第一个点位最小值，然后每个等分所对应的值是（最大值-最小值）/9，
        */
        /// <summary>
        /// 获取Y轴坐标的点分布值
        /// </summary>
        /// <param name="ItemValue">项目数值</param>
        /// <param name="YCount">Y轴点的数量</param>
        /// <returns>图形</returns>
        private static int[] GetYValue(decimal[] ItemValue, int YCount)
        {
            int len = ItemValue.Length;
            int[] Value = new int[YCount];
            int Max = Convert.ToInt32(ItemValue.Max());
            int Min = Convert.ToInt32(ItemValue.Min());
            int Distance = Convert.ToInt32((Max - Min) / (YCount - 1));
            for (int i = 0; i < YCount; i++)
            {
                Value[i] = Min + Distance * i;
            }
            //Value[YCount - 1] = Max;
            return Value;
        }
        #endregion
        #region //建立坐标轴
        /// <summary>
        /// 绘制坐标轴，X、Y轴的坐标值
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="ItemName">项目名称</param>
        /// <param name="ItemValue">项目数值</param>
        /// <returns>图像</returns>
        private static Bitmap DrawCoordinate(Bitmap image, string[] ItemName, decimal[] ItemValue)
        {
            //坐标轴
            Point P0 = new Point(60, 360);
            Point Px = new Point(520, 360);
            Point Py = new Point(60, 65);
            Pen pen = new Pen(Color.Black);
            //箭头
            Point Py1 = new Point(58, 70);
            Point Py2 = new Point(62, 70);
            Point Px1 = new Point(515, 358);
            Point Px2 = new Point(515, 362);
            //Y,X Value
            //y 280-10
            int YCount = 10;//Y轴点的数量
            int YDistance = Convert.ToInt32(280 / YCount);//Y轴点击的距离
            int[] YValue = GetYValue(ItemValue, YCount);
            int len = 3;//短线的长度
            int XCount = ItemName.Length;//X轴点的数量
            int XDistance = Convert.ToInt32(450 / XCount);//X轴点间的距离
            //
            Font f = new Font("新宋体", 8, FontStyle.Bold);
            //Image
            Graphics g = Graphics.FromImage(image);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try
            {
                //绘制坐标轴线
                g.DrawLine(pen, P0, Px);
                g.DrawLine(pen, P0, Py);
                //箭头
                g.DrawLine(pen, Py, Py1);
                g.DrawLine(pen, Py, Py2);
                g.DrawLine(pen, Px, Px1);
                g.DrawLine(pen, Px, Px2);
                //X
                for (int i = 1; i <= XCount; i++)
                {
                    Point pl1 = new Point(P0.X + i * XDistance, P0.Y);
                    Point pl2 = new Point(P0.X + i * XDistance, P0.Y - len);
                    string str = ItemName[i - 1];
                    Point ps = new Point(pl1.X - (str.Length * 8), pl1.Y + 5);
                    g.DrawLine(pen, pl1, pl2);
                    g.DrawString(str, f, new SolidBrush(Color.Black), ps);
                }
                //Y
                for (int i = 1; i <= YCount; i++)
                {
                    Point pl1 = new Point(P0.X, P0.Y - YDistance * i);
                    Point pl2 = new Point(pl1.X + len, pl1.Y);
                    string str = YValue[i - 1].ToString();
                    Point ps = new Point(pl1.X - str.Length * 8, pl1.Y - 5);
                    g.DrawLine(pen, pl1, pl2);
                    g.DrawString(str, f, new SolidBrush(Color.Black), ps);
                }
                //0
                g.DrawString("0", f, new SolidBrush(Color.Black), new Point(P0.X - 10, P0.Y - 10));
                //return
                return image;
            }
            finally
            {
                g.Dispose();
            }
        }
        #endregion

        #region //获取某个数值在坐标系中的位置
        /// <summary>
        /// 获取某个数值在坐标系中的位置
        /// </summary>
        /// <param name="Value">当前数值</param>
        /// <param name="ItemValue">所有数值</param>
        /// <returns>位置</returns>
        private static int GetCoordinateValue(decimal Value, decimal[] ItemValue)
        {
            //y 280-10
            //get y value
            int[] YValue = GetYValue(ItemValue, 10);
            int Max = YValue.Max();
            int Min = YValue.Min();
            float AvgDis = (Max - Min) / 9;
            float tt = (Convert.ToSingle(Value) - Min) / AvgDis;
            int m = Convert.ToInt32(tt * 28);
            m = 360 - 28 - m;
            //if((Convert.ToInt32(Value)) >= Max)
            //    m=80;
            //else if (Convert.ToInt32(Value) <= Min)
            //    m=332;
            return m;
        }
        #endregion
        #region //绘制曲线图****************************
        /// <summary>
        /// 绘制曲线图(主要是分析不同类型的数值所占比例，或者同意项目不同状态下的值和趋势)，参数有图的标题，项目名称，项目的数值，名称和数值都是长度对应的
        /// </summary>
        /// <param name="Title">图的标题</param>
        /// <param name="ItemName">项目名称</param>
        /// <param name="ItemValue">项目的数值</param>
        /// <returns>Bitmap图形</returns>
        public static Bitmap GetLineImage(string Title, string[] ItemName, decimal[] ItemValue)
        {
            Bitmap image = GenerateImage(Title);
            image = DrawCoordinate(image, ItemName, ItemValue);
            Graphics g = Graphics.FromImage(image);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try
            {
                int PNum = ItemName.Length;
                Point[] Pts = new Point[PNum];
                //坐标轴
                Point P0 = new Point(60, 360);//60  360 
                Point Px = new Point(420, 360);// 420  360
                Point Py = new Point(60, 65);//60  65
                Pen pen = new Pen(Color.Black);
                //
                int XCount = ItemName.Length;//X轴点的数量
                int XDistance = Convert.ToInt32(450 / XCount);//X轴点间的距离
                //图例
                Rectangle RDes = new Rectangle(550, 90, 10, 10);
                Font f = new Font("新宋体", 9, FontStyle.Bold);
                //
                for (int i = 0; i < PNum; i++)
                {
                    int x = P0.X + (i + 1) * XDistance;
                    int y = GetCoordinateValue(ItemValue[i], ItemValue);
                    Pts[i] = new Point(x, y);
                }
                //把点连起来
                for (int i = 1; i < PNum; i++)
                {
                    g.DrawLine(pen, Pts[i - 1], Pts[i]);
                }
                //画图例说明
                Color c = GetRandomColor(3);
                for (int i = 0; i < PNum; i++)
                {
                    string str = ItemName[i] + ":" + ItemValue[i].ToString();
                    DrawDes(image, RDes, c, str, f, i);
                }
                //return
                return image;
            }
            finally
            {
                g.Dispose();
            }
        }
        #endregion
        #region //绘制柱状图*********************
        /// <summary>
        /// 绘制柱状图(主要是分析某一个项目在不同状态下的值，获取其发展趋势)，参数有图的标题，项目名称，项目的数值，名称和数值都是长度对应的
        /// </summary>
        /// <param name="Title">图的标题</param>
        /// <param name="ItemName">项目名称</param>
        /// <param name="ItemValue">项目的数值</param>
        /// <returns>Bitmap图形</returns>
        public static Bitmap GetColumnImage(string Title, string[] ItemName, decimal[] ItemValue)
        {
            Bitmap image = GenerateImage(Title);
            DrawCoordinate(image, ItemName, ItemValue);
            Graphics g = Graphics.FromImage(image);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try
            {
                int PNum = ItemName.Length;
                Point[] Pts = new Point[PNum];
                //坐标轴
                Point P0 = new Point(60, 360);
                Point Px = new Point(420, 360);
                Point Py = new Point(60, 65);
                Pen pen = new Pen(Color.Black);
                //
                int XCount = ItemName.Length;//X轴点的数量
                int XDistance = Convert.ToInt32(450 / XCount);//X轴点间的距离
                //图例
                Rectangle RDes = new Rectangle(550, 90, 10, 10);
                Font f = new Font("新宋体", 9, FontStyle.Bold);
                //获取值所对应的点
                for (int i = 0; i < PNum; i++)
                {
                    int x = P0.X + (i + 1) * XDistance;
                    int y = GetCoordinateValue(ItemValue[i], ItemValue);
                    Pts[i] = new Point(x, y);
                }
                //绘制条形框图
                Color c = GetRandomColor(0);
                for (int i = 0; i < PNum; i++)
                {
                    Rectangle rect = new Rectangle(Pts[i].X - 7, Pts[i].Y, 10, P0.Y - Pts[i].Y);
                    Font ff = new Font("新宋体", 8, FontStyle.Regular);
                    g.DrawString(ItemValue[i].ToString(".00"), f, new SolidBrush(Color.Black), new Point(Pts[i].X - 15, Pts[i].Y - 12));
                    g.DrawRectangle(new Pen(Color.Black), rect);
                    g.FillRectangle(new SolidBrush(c), rect);
                }
                //画图例说明
                for (int i = 0; i < PNum; i++)
                {
                    string str = ItemName[i] + ":" + ItemValue[i].ToString();
                    DrawDes(image, RDes, c, str, f, i);
                }
                //return
                return image;
            }
            finally
            {
                g.Dispose();
            }
        }
        #endregion
        #region //绘制多组数据曲线图********************
        /// <summary>
        /// 获取二维数组中的最大值
        /// </summary>
        /// <param name="ItemValues"></param>
        /// <returns></returns>
        private static int GetMax(decimal[][] ItemValues)
        {
            int Max = 0;
            for (int i = 0; i < ItemValues.Length; i++)
            {
                int t = Convert.ToInt32(ItemValues[i].Max());
                if (i == 0)
                    Max = t;
                if (Max <= t)
                    Max = t;
            }
            return Max;
        }
        /// <summary>
        /// 获取二维数组中的最小值
        /// </summary>
        /// <param name="ItemValues"></param>
        /// <returns></returns>
        private static int GetMin(decimal[][] ItemValues)
        {
            int Min = 0;
            for (int i = 0; i < ItemValues.Length; i++)
            {
                int t = Convert.ToInt32(ItemValues[i].Min());
                if (i == 0)
                    Min = t;
                if (Min >= t)
                    Min = t;
            }
            return Min;
        }
        /// <summary>
        /// 获取Y轴上点的数值
        /// </summary>
        /// <param name="ItemValues"></param>
        /// <param name="YCount"></param>
        /// <returns></returns>
        private static int[] GetYValue(decimal[][] ItemValues, int YCount)
        {
            int[] Value = new int[YCount];
            int Max = GetMax(ItemValues);
            int Min = GetMin(ItemValues);
            int Distance = Convert.ToInt32((Max - Min) / (YCount - 1));
            for (int i = 0; i < YCount; i++)
            {
                Value[i] = Min + Distance * i;
            }
            //Value[YCount - 1] = Max;
            return Value;
        }
        /// <summary>
        /// 获取某个数值在坐标系中的位置
        /// </summary>
        /// <param name="Value">当前数值</param>
        /// <param name="ItemValue">所有数值</param>
        /// <returns>位置</returns>
        private static int GetCoordinateValue(decimal Value, decimal[][] ItemValues)
        {
            //y 280-10
            //get y value
            int[] YValue = GetYValue(ItemValues, 10);
            int Max = GetMax(ItemValues);
            int Min = GetMin(ItemValues);
            float AvgDis = (Max - Min) / 9;
            float tt = (Convert.ToSingle(Value) - Min) / AvgDis;
            int m = Convert.ToInt32(tt * 28);
            m = 360 - 28 - m;
            return m;
        }
        /// <summary>
        /// 绘制坐标轴，X、Y轴的坐标值
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="ItemName">项目名称</param>
        /// <param name="ItemValue">多组项目数值</param>
        /// <returns>图像</returns>
        private static Bitmap DrawCoordinate(Bitmap image, string[] ItemName, decimal[][] ItemValues)
        {
            //坐标轴
            Point P0 = new Point(60, 360);
            Point Px = new Point(520, 360);
            Point Py = new Point(60, 65);
            Pen pen = new Pen(Color.Black);
            //箭头
            Point Py1 = new Point(58, 70);
            Point Py2 = new Point(62, 70);
            Point Px1 = new Point(515, 358);
            Point Px2 = new Point(515, 362);
            //Y,X Value
            //y 280-10
            int YCount = 10;//Y轴点的数量
            int YDistance = Convert.ToInt32(280 / YCount);//Y轴点击的距离
            int[] YValue = GetYValue(ItemValues, YCount);
            int len = 3;//短线的长度
            int XCount = ItemName.Length;//X轴点的数量
            int XDistance = Convert.ToInt32(450 / XCount);//X轴点间的距离
            //
            Font f = new Font("新宋体", 8, FontStyle.Bold);
            //Image
            Graphics g = Graphics.FromImage(image);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try
            {
                //绘制坐标轴线
                g.DrawLine(pen, P0, Px);
                g.DrawLine(pen, P0, Py);
                //箭头
                g.DrawLine(pen, Py, Py1);
                g.DrawLine(pen, Py, Py2);
                g.DrawLine(pen, Px, Px1);
                g.DrawLine(pen, Px, Px2);
                //X
                for (int i = 1; i <= XCount; i++)
                {
                    Point pl1 = new Point(P0.X + i * XDistance, P0.Y);
                    Point pl2 = new Point(P0.X + i * XDistance, P0.Y - len);
                    string str = ItemName[i - 1];
                    Point ps = new Point(pl1.X - (str.Length * 8), pl1.Y + 5);
                    g.DrawLine(pen, pl1, pl2);
                    g.DrawString(str, f, new SolidBrush(Color.Black), ps);
                }
                //Y
                for (int i = 1; i <= YCount; i++)
                {
                    Point pl1 = new Point(P0.X, P0.Y - YDistance * i);
                    Point pl2 = new Point(pl1.X + len, pl1.Y);
                    string str = YValue[i - 1].ToString();
                    Point ps = new Point(pl1.X - str.Length * 8, pl1.Y - 5);
                    g.DrawLine(pen, pl1, pl2);
                    g.DrawString(str, f, new SolidBrush(Color.Black), ps);
                }
                //0
                g.DrawString("0", f, new SolidBrush(Color.Black), new Point(P0.X - 10, P0.Y - 10));
                //return
                return image;
            }
            finally
            {
                g.Dispose();
            }
        }
        /// <summary>
        /// 绘制多组数据的曲线图
        /// </summary>
        /// <param name="Title">主标题</param>
        /// <param name="ItemName">数据项名称</param>
        /// <param name="ItemValues">多组数据集合</param>
        /// <param name="ItemsName">各组的名称</param>
        /// <returns></returns>
        public static Bitmap GetMulLineImage(string Title, string[] ItemName, decimal[][] ItemValues, string[] ItemsName)
        {
            Bitmap image = GenerateImage(Title);
            image = DrawCoordinate(image, ItemName, ItemValues);
            Graphics g = Graphics.FromImage(image);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try
            {
                //绘制多条线
                for (int m = 0; m < ItemsName.Length; m++)
                {
                    int PNum = ItemName.Length;
                    Point[] Pts = new Point[PNum];
                    //坐标轴
                    Point P0 = new Point(60, 360);
                    Point Px = new Point(420, 360);
                    Point Py = new Point(60, 65);
                    //color
                    Color c = GetRandomColor(m);
                    Pen pen = new Pen(c);
                    //
                    int XCount = ItemName.Length;//X轴点的数量
                    int XDistance = Convert.ToInt32(450 / XCount);//X轴点间的距离
                    //图例
                    Rectangle RDes = new Rectangle(550, 90, 10, 10);
                    Font f = new Font("新宋体", 9, FontStyle.Bold);
                    //
                    for (int i = 0; i < PNum; i++)
                    {
                        int x = P0.X + (i + 1) * XDistance;
                        int y = GetCoordinateValue(ItemValues[m][i], ItemValues);
                        Pts[i] = new Point(x, y);
                    }
                    //把点连起来
                    for (int i = 1; i < PNum; i++)
                    {
                        g.DrawLine(pen, Pts[i - 1], Pts[i]);
                    }
                    //画图例说明
                    DrawDes(image, RDes, c, ItemsName[m], f, m);
                }
                //return
                return image;
            }
            finally
            {
                g.Dispose();
            }
        }
        #endregion
        //end
    }
}

