using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 仓库物料管理系统.方法类
{
    class MyCalcu
    {
        double a;
        double b;
        int flag;
        public double A
        {
            get { return a; }
            set { a = value; }
        }
        public double B
        {
            get { return b; }
            set { b = value; }
        }
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public double Calcu()
        {
            double c;
            if (flag == 1)
            {
                c = a + b;
            }
            else if (flag == 2)
            {
                c = a - b;
            }
            else if (flag == 3)
            {
                c = a * b;
            }
            else
            {
                c = a / b;
            }
            return c;
        }
    }
}
