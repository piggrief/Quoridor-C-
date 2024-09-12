using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Random;

namespace SimulateAnneal
{
    /// <summary>
    /// 退火类
    /// </summary>
    public class Annealing
    {
        public double Temp_Init;//初始温度
        public double alpha;//
        public double L;//最大迭代长度
        public double FSA_LD;//FSA算法中L的指数递减系数0.5或1
        public double FSA_h;//FSA算法中的h参数
        /// <summary>
        /// 构造函数,初始化模拟退火算法参数
        /// </summary>
        public Annealing(double Temp_Init_set, double alpha_set, double L_set, double FSA_h_set)
        {
            Temp_Init = Temp_Init_set;
            alpha = alpha_set;
            L = L_set;
            FSA_h = FSA_h_set;
            FSA_LD = 1;
        }
        /// <summary>
        /// SA算法温度衰减函数
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public double SA_Ann_fun(double T)
        {
            return T * alpha;
        }
        /// <summary>
        /// FSA算法温度衰减函数
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public double FSA_Ann_fun(double T)
        {
            return T * Math.Pow(alpha, Math.Pow(L, FSA_LD));
        }
        public enum SAMode
        {
            FastSA,
            SA
        }
        /// <summary>
        /// 接受函数，参数为“FSA”则为快速模拟退火，“SA”为普通模拟退火,E为能量,T为当前温度
        /// </summary>
        /// <returns></returns>
        public double P_rec(double E, double T, SAMode mode)
        {
            if (mode == SAMode.FastSA)
            {
                return Math.Pow((1 - (1 - FSA_h) * E / T), (1 / (1 - FSA_h)));
            }
            else
            {
                return Math.Exp(-E / (T));
            }
        }

        #region 以下是模拟退火的核心框架按具体情况实现，暂时没有很方便的接口
        //public void SimulateAnnealFramework()
        //{
        //    double T = Temp_Init;
        //    int l = 0;//初始迭代变量
        //    double E = 0;//能量
        //    double P = 0;//接受概率
        //    int result_num_pre = Part_result_num;//前一次解的质量  

        //    //纯模拟退火框架
        //    while (T > 1)//外循环，退火终止条件
        //    {
        //        while(l<=L)//内循环，迭代终止条件
        //        {
        //            Node newS = S_pre;
        //            ///产生新的领域解空间
        //            newS = Create_Children_Tree(Part_result, k);

        //            ///搜出最优解要记录保存
        //            if (Part_result_num > Best_result_num)
        //            {
        //                Best_result_num = Part_result_num;
        //                Best_result = Part_result;
        //                BestN = newS;
        //            }

        //            ///模拟退火核心
        //            E = Part_result_num-result_num_pre;
        //            P = P_rec(E,T,"SA");
        //            CryptoRandomSource rnd = new CryptoRandomSource();
        //            double[] randnum = new double[] { 1 };
        //            randnum = rnd.NextDoubles(1);

        //            if (E > 0)//局部更优必然接受
        //            {
        //                result_num_pre = Part_result_num;
        //                S_pre = newS;
        //            }
        //            else if (randnum[0] < P)//按概率接受
        //            {
        //                result_num_pre = Part_result_num;
        //                S_pre = newS;
        //            }
        //            else
        //                break;

        //            l = l + 1;//继续迭代
        //        }
        //        T = SA_Ann_fun(T);//退火
        //    }
        //}
        #endregion
    }
}
