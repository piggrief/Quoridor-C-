﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MathNet.Numerics.Random;
using SimulateAnneal;
using Quoridor_With_C;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Text;
using System.Collections.Concurrent;

namespace Queen
{
    /// <summary>
    /// 八皇后求解类
    /// </summary>
    public class QueenSolve
    {
        public List<Point> ChessLocationList = new List<Point>();//棋子位置列表
        public List<Point> QueenLocationList = new List<Point>();//八皇后结果列表
        public static int SelectedQueenResultNum = 92;//只搜索SelectedQueenResultNum组八皇后的解
        public enum DistanceCalMethod
        {
            ManhattanDistance,
            EuclideanDistance,
            ManhattanDistance_Diagonal
        }
        public DistanceCalMethod UsedCalMethod = DistanceCalMethod.EuclideanDistance;//TSP问题使用的距离公式
        double DiagonalWalkTimeRate = Math.Sqrt(2);//斜走一个距离放大系数
        double StraightWalkTimeRate = 1;//直走一个距离放大系数

        /// <summary>
        /// 计算棋子和皇后位置之间的距离
        /// </summary>
        /// <param name="Chess">棋子位置点</param>
        /// <param name="Queen">皇后位置点</param>
        /// <param name="CalMethod">距离计算方式（曼哈顿、欧式、带斜走的曼哈顿）</param>
        /// <returns>两点间的距离</returns>
        public double CalDistance_QueenToChess(Point Chess, Point Queen, DistanceCalMethod CalMethod)
        {
            int x1 = Chess.X, y1 = Chess.Y;
            int x2 = Queen.X, y2 = Queen.Y;
            switch (CalMethod)
            {
                case DistanceCalMethod.ManhattanDistance:
                    return Convert.ToDouble(Math.Abs(x1 - x2) + Math.Abs(y1 - y2));
                case DistanceCalMethod.EuclideanDistance:
                    double sum_pingfang = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
                    return Math.Sqrt(Convert.ToDouble(sum_pingfang));
                case DistanceCalMethod.ManhattanDistance_Diagonal:
                    int x0 = Math.Abs(x1 - x2);
                    int y0 = Math.Abs(y1 - y2);
                    double distancebuff = Convert.ToDouble(x0 + y0);
                    if (x0 == y0)
                    {
                        return DiagonalWalkTimeRate * Convert.ToDouble(x0);
                    }
                    else if (x0 < y0)
                    {
                        double DiagonalDisbuff = x0 * DiagonalWalkTimeRate;
                        return StraightWalkTimeRate * (distancebuff - 2 * x0) + DiagonalDisbuff;
                    }
                    else
                    {
                        double DiagonalDisbuff = y0 * DiagonalWalkTimeRate;
                        return StraightWalkTimeRate * (distancebuff - 2 * y0) + DiagonalDisbuff; 
                    }
                default:
                    return 999;
            }
        }
        /// <summary>
        /// 对初始解进行预处理，不优化棋子就在八皇后位置上的情况
        /// </summary>
        /// <param name="ChessList">棋子位置列表</param>
        /// <param name="QueenList">皇后位置列表</param>
        public void ResultPretreatment(ref List<Point> ChessList, ref List<Point> QueenList)
        {
            for (int i = ChessList.Count - 1; i >= 0; i--)
            {
                foreach (Point QueenPoint in QueenList)
                {
                    if (QueenPoint.X == ChessList[i].X && QueenPoint.Y == ChessList[i].Y)
                    {
                        QueenList.Remove(QueenPoint);
                        ChessList.Remove(ChessList[i]);
                        break;
                    }
                }                
            }
        }
        List<int> InitResult = new List<int>();//其中10~17代表第0~7个棋子，20~27代表第0~7个皇后

        public enum InitResultMethod///初始解计算方法
        {
            Dijkstra,//迪杰斯特拉算法
            Other
        }
        public InitResultMethod UsedInitResultMethod = InitResultMethod.Dijkstra;
        /// <summary>
        /// 创建初始解(贪婪算法)
        /// </summary>
        /// <param name="ChessList">棋子位置列表</param>
        /// <param name="QueenList">皇后位置列表</param>
        /// <param name="Method">计算初始解的方法</param>
        /// <param name="Distance_All">总距离</param>
        /// <returns>路径移动序列,其中百位数0、1代表棋子或皇后，十位代表所在行，个位数代表所在列</returns>
        public List<int> CreateInitResult(List<Point> ChessList, List<Point> QueenList, ref double Distance_All)
        {
            List<int> MoveSequence = new List<int>();
            List<Point> ChessListBuff = new List<Point>();
            List<Point> QueenListBuff = new List<Point>();
            for (int i = 0; i < ChessList.Count; i++)
            {
                ChessListBuff.Add(ChessList[i]);
            }
            for (int i = 0; i < QueenList.Count; i++)
            {
                QueenListBuff.Add(QueenList[i]);
            }
            ResultPretreatment(ref ChessListBuff, ref QueenListBuff);
            if (UsedInitResultMethod == InitResultMethod.Dijkstra)
            {
                for (int i = 0; i < ChessListBuff.Count; i++)
                {
                    double disbuff = 0;
                    double dismin = 999;
                    int minindex = 0;
                    for (int j = 0; j < QueenListBuff.Count; j++)
                    {
                        disbuff = CalDistance_QueenToChess(ChessListBuff[i], QueenListBuff[j], UsedCalMethod);
                        if (disbuff < dismin && !MoveSequence.Contains(200 + QueenListBuff[j].X * 10 + QueenListBuff[j].Y)) 
                        {
                            dismin = disbuff;
                            minindex = j;
                        }
                    }
                    MoveSequence.Add(100 + ChessListBuff[i].X * 10 + ChessListBuff[i].Y);
                    MoveSequence.Add(200 + QueenListBuff[minindex].X * 10 + QueenListBuff[minindex].Y);
                }
            }

            Distance_All = CalMoveSequenceDistance(MoveSequence);

            return MoveSequence;
        }
        /// <summary>
        /// 用逆转策略改变解序列，如(1,2,3,4,5,6)随机选出2，5两个点，逆转之后新序列（1,5,4,3,2,6）
        /// </summary>
        /// <param name="Sequence_last">待随机逆转的序列</param>
        /// <returns>逆转完成的序列</returns>
        public List<int> ChangeResult_Reverse(List<int> Sequence_last)
        {
            #region 随机选择两个可交换的点
            bool RandomSuccess = false;
            CryptoRandomSource rnd = new CryptoRandomSource();
            int[] RandomNumber = new int[2];
            do//保证随机出来的数全奇或全偶且不相等
            {
                RandomNumber = rnd.NextInt32s(2, 0, Sequence_last.Count);
                int buff = RandomNumber[0] + RandomNumber[1];
                if ((buff % 2 == 0) && (RandomNumber[0] != RandomNumber[1]))
                    RandomSuccess = true;
            } while (!RandomSuccess);
            if (RandomNumber[0] > RandomNumber[1])//保证第一个数一定小
            {
                int t = RandomNumber[0];
                RandomNumber[0] = RandomNumber[1];
                RandomNumber[1] = t;
            }
            #endregion

            #region 将这两个点之间的序列倒序
            List<int> NewSequence = new List<int>();
            for (int i = 0; i < Sequence_last.Count; i++)
			{
			    NewSequence.Add(Sequence_last[i]); 
			}
            for (int i = RandomNumber[0], j = 0; i <= RandomNumber[1]; i++, j++)
            {
                NewSequence[i] = Sequence_last[RandomNumber[1] - j];
            }
            #endregion
            return NewSequence;
        }
        /// <summary>
        /// 计算一个移动序列的总距离
        /// </summary>
        /// <param name="MoveSequence">移动序列列表（其中百位数0、1代表棋子或皇后，十位代表所在行，个位数代表所在列）</param>
        /// <returns>移动序列的总距离</returns>
        public double CalMoveSequenceDistance(List<int> MoveSequence)
        {
            double dis_all = 0;
            int x0 = (MoveSequence[0] / 10) % 10;
            int y0 = MoveSequence[0] % 10;

            if (x0 < y0)
            {
                dis_all = x0 + 1;
            }
            else
            {
                dis_all = y0 + 1;
            }
            for (int i = 0; i < MoveSequence.Count - 1; i++)
            {
                dis_all += CalDistance_QueenToChess(
                            new Point((MoveSequence[i] / 10) % 10, MoveSequence[i] % 10)
                            , new Point((MoveSequence[i + 1] / 10) % 10, MoveSequence[i + 1] % 10)
                            , UsedCalMethod);
            }
            int xe = (MoveSequence[MoveSequence.Count - 1] / 10) % 10;
            int ye = MoveSequence[MoveSequence.Count - 1] % 10;

            if (xe < ye)
            {
                dis_all += (xe + 1);
            }
            else
            {
                dis_all += (ye + 1);
            }
            return dis_all;
        }
        public QueenSolve(DistanceCalMethod SetCalMethod, InitResultMethod InitMethod, int SelectNum = 92)
        {
            UsedCalMethod = SetCalMethod;
            UsedInitResultMethod = InitMethod;
            SelectedQueenResultNum = SelectNum;
        }
        /// <summary>
        /// 在控制台上输出解序列（会显示棋子坐标，皇后坐标以及移动序列以及总长度）
        /// </summary>
        /// <param name="MoveSequence">移动序列</param>
        /// <param name="ChessList">皇后位置列表</param>
        /// <param name="QueenList">棋子位置列表</param>
        public void PrintMoveSequence(List<int> MoveSequence, List<Point> ChessList, List<Point> QueenList)
        {
            for (int i = 0; i < MoveSequence.Count; i++)
            {
                Console.Write("地点" + (i+1).ToString() + ": ");
                if (i % 2 == 0)
                {
                    Console.Write("Chess----(" + ((MoveSequence[i] / 10) % 10).ToString()
                        + "," + (MoveSequence[i] % 10).ToString() + ")");
                }
                else
                {
                    Console.Write("Queen----(" + ((MoveSequence[i] / 10) % 10).ToString()
                        + "," + (MoveSequence[i] % 10).ToString() + ")");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// 打印解序列，主要用于真实的序列信息
        /// </summary>
        /// <param name="MoveSequence">移动序列</param>
        public void PrintResultSequence(List<int> MoveSequence)
        {
            Console.Write("棋子序列:");
            for (int i = 0; i < MoveSequence.Count; i+=2)
            {
                Console.Write(MoveSequence[i].ToString() + " ");
            }
            Console.WriteLine();
            Console.Write("皇后序列:");
            for (int i = 1; i < MoveSequence.Count; i += 2)
            {
                Console.Write(MoveSequence[i].ToString() + " ");
            }
            Console.WriteLine();
        }
        public Annealing ThisSA;//退火引擎
        public Annealing.SAMode ThisSAMode = Annealing.SAMode.SA; //退火模式
        /// <summary>
        /// 初始化模拟退火（主要为了设定SA算法的初始参数）
        /// </summary>
        /// <param name="InitTemp">初始温度</param>
        /// <param name="Alpha">衰减常数alpha</param>
        /// <param name="L">最大迭代次数</param>
        /// <param name="FSA_H">快速模拟退火中所需的H参数</param>
        /// <param name="WhichSA">使用哪种模拟退火算法（如SA、FSA）</param>
        public void InitSA(double InitTemp, double Alpha, double L, double FSA_H, Annealing.SAMode WhichSA)
        {
            ThisSA = new Annealing(InitTemp, Alpha, L, FSA_H);
            ThisSAMode = WhichSA;
        }
        /// <summary>
        /// 搜索最短路径问题的解
        /// </summary>
        /// <param name="BestDistance">最优的路径的总距离</param>
        /// <returns>最优路径序列</returns>
        public List<int> SearchResult_ForMinDistance(ref double BestDistance)
        {
            //if (ChessLocationList.Count == 0 || QueenLocationList.Count == 0)
            //    return new List<int>();
            #region 创建初始解
            List<int> Init_Sequence = new List<int>();
            double Init_Distance = 999999;

            Init_Sequence = CreateInitResult(ChessLocationList, QueenLocationList, ref Init_Distance);
            Init_Distance = CalMoveSequenceDistance(Init_Sequence);

            #endregion

            double OverallBest_Distance = Init_Distance;//全局最优解的距离
            List<int> OverallBest_Sequence = Init_Sequence;//全局最优解

            double PartBest_Distance = Init_Distance;//局部最优解的距离
            List<int> PartBest_Sequence = Init_Sequence;//局部最优解

            #region 模拟退火
            double T = ThisSA.Temp_Init;
            int l = 0;//初始迭代变量
            double E = 0;//能量
            double P = 0;//接受概率
            double result_distance_pre = Init_Distance;//前一次解的质量  
            double result_distance_new = Init_Distance;//新的解的质量

            List<int> Last_Sequence = Init_Sequence;
            List<int> New_Sequence = Init_Sequence;
            //纯模拟退火框架
            while (T > 1)//外循环，退火终止条件
            {
                l = 0;
                while (l <= ThisSA.L)//内循环，迭代终止条件
                {
                    //Last_Sequence = New_Sequence;///随机搜索策略
                    Last_Sequence = PartBest_Sequence;//改进搜索策略
                    result_distance_pre = result_distance_new;
                    ///产生新的随机解
                    New_Sequence = new List<int>();
                    New_Sequence = ChangeResult_Reverse(Last_Sequence);
                    result_distance_new = CalMoveSequenceDistance(New_Sequence);

                    //PrintResultSequence(New_Sequence);

                    ///搜出最优解要记录保存
                    if (result_distance_new < OverallBest_Distance)
                    {
                        OverallBest_Distance = result_distance_new;
                        OverallBest_Sequence = New_Sequence;
                    }

                    ///模拟退火核心
                    E = result_distance_new - result_distance_pre;
                    P = ThisSA.P_rec(E, T, ThisSAMode);
                    CryptoRandomSource rnd = new CryptoRandomSource();
                    double[] randnum = new double[] { 1 };
                    randnum = rnd.NextDoubles(1);

                    if (E < 0)//局部更优必然接受 7.24日前是大于号
                    {
                        PartBest_Distance = result_distance_new;
                        PartBest_Sequence = New_Sequence;
                    }
                    else if (randnum[0] < P)//按概率接受
                    {
                        PartBest_Distance = result_distance_new;
                        PartBest_Sequence = New_Sequence;
                    }

                    l = l + 1;//继续迭代
                }
                if (ThisSAMode == Annealing.SAMode.SA)
                {
                    T = ThisSA.SA_Ann_fun(T);//退火                    
                }
                else
                {
                    T = ThisSA.FSA_Ann_fun(T);
                }
            }
            #endregion
            BestDistance = OverallBest_Distance;
            return OverallBest_Sequence;
        }
        /// <summary>
        /// 搜索最短路径问题的解,加入了图表刷新程序
        /// </summary>
        /// <param name="BestDistance">最优的路径的总距离</param>
        /// <returns>最优路径序列</returns>
        public List<int> SearchResult_ForMinDistance(ref double BestDistance, DataPointCollection DataPoint)
        {
            long PointIndex = 0;
            DataPoint.Clear();

            #region 创建初始解
            List<int> Init_Sequence = new List<int>();
            double Init_Distance = 999999;

            Init_Sequence = CreateInitResult(ChessLocationList, QueenLocationList, ref Init_Distance);
            Init_Distance = CalMoveSequenceDistance(Init_Sequence);

            #endregion

            double OverallBest_Distance = Init_Distance;//全局最优解的距离
            List<int> OverallBest_Sequence = Init_Sequence;//全局最优解

            double PartBest_Distance = Init_Distance;//局部最优解的距离
            List<int> PartBest_Sequence = Init_Sequence;//局部最优解

            #region 模拟退火
            double T = ThisSA.Temp_Init;
            int l = 0;//初始迭代变量
            double E = 0;//能量
            double P = 0;//接受概率
            double result_distance_pre = Init_Distance;//前一次解的质量  
            double result_distance_new = Init_Distance;//新的解的质量

            List<int> Last_Sequence = Init_Sequence;
            List<int> New_Sequence = Init_Sequence;
            //纯模拟退火框架
            while (T > 1)//外循环，退火终止条件
            {
                l = 0;
                while (l <= ThisSA.L)//内循环，迭代终止条件
                {
                    Last_Sequence = New_Sequence;
                    result_distance_pre = result_distance_new;
                    ///产生新的随机解
                    New_Sequence = new List<int>();
                    New_Sequence = ChangeResult_Reverse(Last_Sequence);
                    result_distance_new = CalMoveSequenceDistance(New_Sequence);

                    ///搜出最优解要记录保存
                    if (result_distance_new < OverallBest_Distance)
                    {
                        OverallBest_Distance = result_distance_new;
                        OverallBest_Sequence = New_Sequence;
                    }

                    ///模拟退火核心
                    E = result_distance_new - result_distance_pre;
                    P = ThisSA.P_rec(E, T, ThisSAMode);
                    CryptoRandomSource rnd = new CryptoRandomSource();
                    double[] randnum = new double[] { 1 };
                    randnum = rnd.NextDoubles(1);

                    if (E > 0)//局部更优必然接受
                    {
                        PartBest_Distance = result_distance_new;
                        PartBest_Sequence = New_Sequence;
                    }
                    else if (randnum[0] < P)//按概率接受
                    {
                        PartBest_Distance = result_distance_new;
                        PartBest_Sequence = New_Sequence;
                    }

                    l = l + 1;//继续迭代
                }
                if (ThisSAMode == Annealing.SAMode.SA)
                {
                    T = ThisSA.SA_Ann_fun(T);//退火                    
                }
                else
                {
                    T = ThisSA.FSA_Ann_fun(T);
                }
                DataPoint.Add(new DataPoint(PointIndex, PartBest_Distance));
                PointIndex++;
            }
            #endregion
            BestDistance = OverallBest_Distance;
            return OverallBest_Sequence;
        }
        /// <summary>
        /// 在92个八皇后的解中搜索最短路径问题的解
        /// </summary>
        /// <param name="BestDistance">最优的路径的总距离</param>
        /// <returns>最优路径序列</returns>
        public List<int> SearchResult_ForOverall(ref double BestDistance, ref List<Point> QueenLocation)
        {
            if (ChessLocationList.Count == 0)
                return new List<int>();
            #region 生成待搜索的八皇后解列表
            SelectQueenResult(SelectedQueenResultNum);
            #endregion
            double OverallBest_Distance = 999999;//全局最优解的距离
            List<int> OverallBest_Sequence = new List<int>();//全局最优解

            List<Point> QueenLocationBuff = new List<Point>();

            for (int i = 0; i < SelectedQueenResultNum; i++)
            {
                int index = EightQueenResult_Effective[i];
                QueenLocationList = new List<Point>();
                for (int j = 0; j < 8; j++)
                {
                    QueenLocationList.Add(new Point(j, EightQueenResult[index, j] - 1));
                }

                double PartBest_Distance = 999999;//全局最优解的距离
                List<int> PartBest_Sequence = new List<int>();//全局最优解

                PartBest_Sequence = SearchResult_ForMinDistance(ref PartBest_Distance);

                if (PartBest_Distance < OverallBest_Distance)
                {
                    OverallBest_Distance = PartBest_Distance;
                    OverallBest_Sequence = PartBest_Sequence;
                    QueenLocation = QueenLocationList;
                }
            }

            BestDistance = OverallBest_Distance;
            return OverallBest_Sequence;
        }
        /// <summary>
        /// 在92个八皇后的解中搜索最短路径问题的解(加入了进度条刷新)
        /// </summary>
        /// <param name="BestDistance">最优的路径的总距离</param>
        /// <returns>最优路径序列</returns>
        public List<int> SearchResult_ForOverall(ref double BestDistance, ref List<Point> QueenLocation, CCWin.SkinControl.SkinProgressBar SPBar)
        {
            if (ChessLocationList.Count == 0)
                return new List<int>();
            #region 生成待搜索的八皇后解列表
            SelectQueenResult(SelectedQueenResultNum);
            #endregion
            SPBar.Value = 0;
            SPBar.Maximum = SelectedQueenResultNum;
            double OverallBest_Distance = 999999;//全局最优解的距离
            List<int> OverallBest_Sequence = new List<int>();//全局最优解

            List<Point> QueenLocationBuff = new List<Point>();

            for (int i = 0; i < SelectedQueenResultNum; i++)
            {
                int index = EightQueenResult_Effective[i];
                QueenLocationList = new List<Point>();
                for (int j = 0; j < 8; j++)
                {
                    QueenLocationList.Add(new Point(j, EightQueenResult[index, j] - 1));
                }

                double PartBest_Distance = 999999;//全局最优解的距离
                List<int> PartBest_Sequence = new List<int>();//全局最优解

                PartBest_Sequence = SearchResult_ForMinDistance(ref PartBest_Distance);

                if (PartBest_Distance < OverallBest_Distance)
                {
                    OverallBest_Distance = PartBest_Distance;
                    OverallBest_Sequence = PartBest_Sequence;
                    QueenLocation = QueenLocationList;
                }
                SPBar.Value++;
            }

            BestDistance = OverallBest_Distance;
            return OverallBest_Sequence;
        }
        public List<double> Queen92Dis = new List<double>();
        /// <summary>
        /// 在92个八皇后的解中搜索最短路径问题的解(加入了图表控件点刷新)
        /// </summary>
        /// <param name="BestDistance">最优的路径的总距离</param>
        /// <returns>最优路径序列</returns>
        public List<int> SearchResult_ForOverall(ref double BestDistance, ref List<Point> QueenLocation, DataPointCollection DataPoint)
        {
            long PointIndex = 0;
            if (ChessLocationList.Count == 0)
                return new List<int>();
            #region 生成待搜索的八皇后解列表
            SelectQueenResult(SelectedQueenResultNum);
            #endregion
            DataPoint.Clear();
            double OverallBest_Distance = 999999;//全局最优解的距离
            List<int> OverallBest_Sequence = new List<int>();//全局最优解

            List<Point> QueenLocationBuff = new List<Point>();

            Queen92Dis.Clear();
            for (int i = 0; i < SelectedQueenResultNum; i++)
            {
                int index = EightQueenResult_Effective[i];
                QueenLocationList = new List<Point>();
                for (int j = 0; j < 8; j++)
                {
                    QueenLocationList.Add(new Point(j, EightQueenResult[index, j] - 1));
                }

                double PartBest_Distance = 999999;//全局最优解的距离
                List<int> PartBest_Sequence = new List<int>();//全局最优解

                PartBest_Sequence = SearchResult_ForMinDistance(ref PartBest_Distance);

                if (PartBest_Distance < OverallBest_Distance)
                {
                    OverallBest_Distance = PartBest_Distance;
                    OverallBest_Sequence = PartBest_Sequence;
                    QueenLocation = QueenLocationList;
                }

                Queen92Dis.Add(PartBest_Distance);
                DataPoint.Add(new DataPoint(index, PartBest_Distance));
                PointIndex++;
            }

            BestDistance = OverallBest_Distance;
            return OverallBest_Sequence;
        }

        public double MeanDistanceResult = 0;
        public double MinDistanceResult = 0;
        public double SolveUsedTime = 0;
        public ConcurrentBag<double> TestDisList = new ConcurrentBag<double>();
        public ConcurrentBag<double> TestUsedTime = new ConcurrentBag<double>();
        /// <summary>
        /// 测试模拟退火的某组参数的性能,最终的结果会被保存在MeanDistanceResult，MinDistanceResult，SolveUsedTime
        /// </summary>
        /// <param name="InitTemp">初始温度</param>
        /// <param name="Alpha">衰减常数</param>
        /// <param name="L">迭代长度</param>
        /// <param name="FSA_H">FSA的h参数</param>
        /// <param name="WhichSA">使用SA还是FSA</param>
        /// <param name="Test_Num">测试次数</param>
        public string Test_SAParameter(double InitTemp, double Alpha, double L, double FSA_H, Annealing.SAMode WhichSA, int Test_Num)
        {
            InitSA(InitTemp, Alpha, L, FSA_H, WhichSA);

            List<int> MoveSequence = new List<int>();
            ConcurrentBag<double> disall = new ConcurrentBag<double>();
            ConcurrentBag<double> RunTime_ms = new ConcurrentBag<double>();
            //var obj = new Object();
            //Parallel.For(0, Test_Num, i =>
            //{
            //    double disbuff = 0;
            //    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //    stopwatch.Start(); //  开始监视代码运行时间
            //    MoveSequence = SearchResult_ForMinDistance(ref disbuff);
            //    stopwatch.Stop(); //  停止监视
            //    TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
            //    double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数

            //    disall.Add(disbuff);
            //    RunTime_ms.Add(milliseconds);

            //    if ((i + 1) % 100 == 0)
            //        Console.WriteLine("第" + (i + 1).ToString() + "次测试已完成！");
            //});
            
            for (int i = 0; i < Test_Num; i++)
            {
                double disbuff = 0;
                List<Point> QueenLocation = new List<Point>();
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start(); //  开始监视代码运行时间
                MoveSequence = SearchResult_ForOverall(ref disbuff,ref QueenLocation);
                //MoveSequence = SearchResult_ForMinDistance(ref disbuff);
                stopwatch.Stop(); //  停止监视
                TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
                double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数

                disall.Add(disbuff);
                RunTime_ms.Add(milliseconds);
                if ((i + 1) % 100 == 0)
                    Console.WriteLine("第" + (i + 1).ToString() + "次测试已完成！");
            }

            MeanDistanceResult = disall.AsParallel().Average();
            MinDistanceResult = disall.AsParallel().Min();
            SolveUsedTime = RunTime_ms.AsParallel().Average();
            string buff = PrintSAParameterTest(Test_Num);
            TestDisList = disall;
            TestUsedTime = RunTime_ms;

            return buff;
        }
        public enum WhichSAParameter
        {
            InitTemp,
            Alpha,
            Lenght,
            FSA_h
        }
        /// <summary>
        /// 写入一个txt文件
        /// </summary>
        /// <param name="path">文件目录</param>
        public void Write(string path, string FileStr)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(FileStr);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
        public void Test_SAParameter_Auto(Annealing.SAMode WhichSA, Annealing InitSAPara, WhichSAParameter SelectedPara, double Start, double Step, double Stop, int Test_num = 50)
        {
            string ResultText = "";
            //InitSA(InitSAPara.Temp_Init, InitSAPara.alpha, InitSAPara.L, InitSAPara.FSA_h, WhichSA);
            #region 生成参数调节列表
            List<double> ParaList = new List<double>();

            for (double i = Start; i <= Stop; i += Step)
            {
                ParaList.Add(i);
            }            
            #endregion
            foreach (double NowPara in ParaList)
            {
                double InitTemp = InitSAPara.Temp_Init;
                double Alpha = InitSAPara.alpha;
                double L = InitSAPara.L;
                double FSA_H = InitSAPara.FSA_h;

                switch (SelectedPara)
                {
                    case WhichSAParameter.InitTemp:
                        InitTemp = NowPara;
                        break;
                    case WhichSAParameter.Alpha:
                        Alpha = NowPara;
                        break;
                    case WhichSAParameter.Lenght:
                        L = NowPara;
                        break;
                    case WhichSAParameter.FSA_h:
                        FSA_H = NowPara;
                        break;
                    default:
                        break;
                }

                string SAResultStrBuff = Test_SAParameter(InitTemp, Alpha, L, FSA_H, WhichSA, Test_num);

                ResultText += SAResultStrBuff;
            }

            #region 读写记录
            DirectoryInfo source = new DirectoryInfo(Environment.CurrentDirectory + "\\Record");
            List<int> RecordIndexList = new List<int>();
            foreach (FileInfo diSourceSubDir in source.GetFiles())
            {
                //这里进行处理，用diSourceSubDir.Name
                int count = diSourceSubDir.Name.Count();
                string newstr = diSourceSubDir.Name.Remove(count - 4);
                int RecordIndex = 0;
                try
                {
                    RecordIndex = Convert.ToInt32(newstr);
                    RecordIndexList.Add(RecordIndex);
                }
                catch (Exception)
                {                                       
                }
            }
            int FinalRecordIndex = RecordIndexList.Max();

            ResultText = ResultText.Insert(0, DateTime.Now.ToLongDateString().ToString() + "  " +DateTime.Now.ToLongTimeString().ToString() + System.Environment.NewLine);

            Write(Environment.CurrentDirectory + "\\Record\\" + (FinalRecordIndex + 1).ToString() + ".txt", ResultText);

            #endregion
        }
        public string PrintSAParameterTest(int TestNum)
        {
            string RecordData = "";
            for (int i = 0; i < 30; i++)
                Console.Write("*");
            Console.WriteLine();
            Console.WriteLine("模拟退火参数：");
            Console.WriteLine("模拟退火方式：" + ThisSAMode.ToString());
            Console.WriteLine("初始温度：" + ThisSA.Temp_Init);
            Console.WriteLine("衰减常数：" + ThisSA.alpha);
            Console.WriteLine("迭代长度：" + ThisSA.L);
            if (ThisSAMode == Annealing.SAMode.FastSA)
            {
                Console.WriteLine("FSA-h：" + ThisSA.FSA_h); 
            }
            Console.WriteLine("进行了" + TestNum.ToString() + "次测试,其结果为:");
            Console.WriteLine("平均值：" + MeanDistanceResult.ToString());
            Console.WriteLine("最小值：" + MinDistanceResult.ToString());
            Console.WriteLine("均用时：" + SolveUsedTime.ToString()+"ms");
            for (int i = 0; i < 30; i++)
                Console.Write("*");
            Console.WriteLine();

            if (ThisSAMode == Annealing.SAMode.SA)
                RecordData += "\"SA\" ";
            else
                RecordData += "\"FSA\" ";

            RecordData += ThisSA.Temp_Init.ToString() + " ";
            RecordData += ThisSA.alpha.ToString() + " ";
            RecordData += ThisSA.L.ToString() + " ";

            if (ThisSAMode != Annealing.SAMode.SA)
                RecordData += ThisSA.FSA_h.ToString() + " ";

            RecordData += TestNum.ToString() + " ";
            RecordData += MeanDistanceResult.ToString("F2") + " ";
            RecordData += MinDistanceResult.ToString() + " ";
            RecordData += SolveUsedTime.ToString("F2") + " ";

            RecordData += System.Environment.NewLine;

            return RecordData;
        }
        /// <summary>
        /// 用初始解质量评估92组解
        /// </summary>
        /// <returns>评分列表</returns>
        public List<double> QueenResultEvaluation()
        {
            List<double> ResultEvaluation = new List<double>();

            for (int i = 0; i < 92; i++)
            {
                List<int> Init_Sequence = new List<int>();
                double Init_Distance = 999999;
                QueenLocationList = new List<Point>();
                for (int j = 0; j < 8; j++)
                {
                    QueenLocationList.Add(new Point(j, EightQueenResult[i, j] - 1));
                }

                Init_Sequence = CreateInitResult(ChessLocationList, QueenLocationList, ref Init_Distance);

                ResultEvaluation.Add(Init_Sequence.Count);
            }

            return ResultEvaluation;
        }
        /// <summary>
        /// 选择待寻优的八皇后的解，也就是在92组解中找SelectNum个最有可能是最优解的解
        /// </summary>
        /// <param name="SelectNum">要选出几个解</param>
        public void SelectQueenResult(int SelectNum)
        {
            Queen92Dis.Clear();

            Queen92Dis = QueenResultEvaluation();
            List<double> Queen92Buff = new List<double>();
            for (int i = 0; i < Queen92Dis.Count; i++)
            {
                Queen92Buff.Add(Queen92Dis[i]);
            }
            Queen92Buff.Sort();
            double disGateValue = Queen92Buff[SelectNum - 1];
            EightQueenResult_Effective.Clear();

            for (int i = 0, ResultNum = 0; ResultNum < SelectNum || i < 92 ; i++)
            {
                if (Queen92Dis[i] <= disGateValue)
                {
                    EightQueenResult_Effective.Add(i);
                    ResultNum++;
                }
            }
            int a = 0;
        }
        #region 有用的八皇后的解索引
        public List<int> EightQueenResult_Effective = new List<int>();
        #endregion
        #region 八皇后所有的解
        public int[,] EightQueenResult = new int[92, 8]  {{1,5,8,6,3,7,2,4},
                                                                {1,6,8,3,7,4,2,5},
                                                                {1,7,4,6,8,2,5,3},
                                                                {1,7,5,8,2,4,6,3},
                                                                {2,4,6,8,3,1,7,5},
                                                                {2,5,7,1,3,8,6,4},
                                                                {2,5,7,4,1,8,6,3},
                                                                {2,6,1,7,4,8,3,5},
                                                                {2,6,8,3,1,4,7,5},
                                                                {2,7,3,6,8,5,1,4},
                                                                {2,7,5,8,1,4,6,3},
                                                                {2,8,6,1,3,5,7,4},
                                                                {3,1,7,5,8,2,4,6},
                                                                {3,5,2,8,1,7,4,6},
                                                                {3,5,2,8,6,4,7,1},
                                                                {3,5,7,1,4,2,8,6},
                                                                {3,5,8,4,1,7,2,6},
                                                                {3,6,2,5,8,1,7,4},
                                                                {3,6,2,7,1,4,8,5},
                                                                {3,6,2,7,5,1,8,4},
                                                                {3,6,4,1,8,5,7,2},
                                                                {3,6,4,2,8,5,7,1},
                                                                {3,6,8,1,4,7,5,2},
                                                                {3,6,8,1,5,7,2,4},
                                                                {3,6,8,2,4,1,7,5},
                                                                {3,7,2,8,5,1,4,6},
                                                                {3,7,2,8,6,4,1,5},
                                                                {3,8,4,7,1,6,2,5},
                                                                {4,1,5,8,2,7,3,6},
                                                                {4,1,5,8,6,3,7,2},
                                                                {4,2,5,8,6,1,3,7},
                                                                {4,2,7,3,6,8,1,5},
                                                                {4,2,7,3,6,8,5,1},
                                                                {4,2,7,5,1,8,6,3},
                                                                {4,2,8,5,7,1,3,6},
                                                                {4,2,8,6,1,3,5,7},
                                                                {4,6,1,5,2,8,3,7},
                                                                {4,6,8,2,7,1,3,5},
                                                                {4,6,8,3,1,7,5,2},
                                                                {4,7,1,8,5,2,6,3},
                                                                {4,7,3,8,2,5,1,6},
                                                                {4,7,5,2,6,1,3,8},
                                                                {4,7,5,3,1,6,8,2},
                                                                {4,8,1,3,6,2,7,5},
                                                                {4,8,1,5,7,2,6,3},
                                                                {4,8,5,3,1,7,2,6},
                                                                {5,1,4,6,8,2,7,3},
                                                                {5,1,8,4,2,7,3,6},
                                                                {5,1,8,6,3,7,2,4},
                                                                {5,2,4,6,8,3,1,7},
                                                                {5,2,4,7,3,8,6,1},
                                                                {5,2,6,1,7,4,8,3},
                                                                {5,2,8,1,4,7,3,6},
                                                                {5,3,1,6,8,2,4,7},
                                                                {5,3,1,7,2,8,6,4},
                                                                {5,3,8,4,7,1,6,2},
                                                                {5,7,1,3,8,6,4,2},
                                                                {5,7,1,4,2,8,6,3},
                                                                {5,7,2,4,8,1,3,6},
                                                                {5,7,2,6,3,1,4,8},
                                                                {5,7,2,6,3,1,8,4},
                                                                {5,7,4,1,3,8,6,2},
                                                                {5,8,4,1,3,6,2,7},
                                                                {5,8,4,1,7,2,6,3},
                                                                {6,1,5,2,8,3,7,4},
                                                                {6,2,7,1,3,5,8,4},
                                                                {6,2,7,1,4,8,5,3},
                                                                {6,3,1,7,5,8,2,4},
                                                                {6,3,1,8,4,2,7,5},
                                                                {6,3,1,8,5,2,4,7},
                                                                {6,3,5,7,1,4,2,8},
                                                                {6,3,5,8,1,4,2,7},
                                                                {6,3,7,2,4,8,1,5},
                                                                {6,3,7,2,8,5,1,4},
                                                                {6,3,7,4,1,8,2,5},
                                                                {6,4,1,5,8,2,7,3},
                                                                {6,4,2,8,5,7,1,3},
                                                                {6,4,7,1,3,5,2,8},
                                                                {6,4,7,1,8,2,5,3},
                                                                {6,8,2,4,1,7,5,3},
                                                                {7,1,3,8,6,4,2,5},
                                                                {7,2,4,1,8,5,3,6},
                                                                {7,2,6,3,1,4,8,5},
                                                                {7,3,1,6,8,5,2,4},
                                                                {7,3,8,2,5,1,6,4},
                                                                {7,4,2,5,8,1,3,6},
                                                                {7,4,2,8,6,1,3,5},
                                                                {7,5,3,1,6,8,2,4},
                                                                {8,2,4,1,7,5,3,6},
                                                                {8,2,5,3,1,7,4,6},
                                                                {8,3,1,6,2,5,7,4},
                                                                {8,4,1,3,6,2,7,5}};
        #endregion
        public enum RunCMDMode
        {
            Forward,
            Back,
            Left,
            Right,
            Stop,
            HoldChess,
            DownChess
        }
        /// <summary>
        /// 绝对方向转换成相对方向的字典，绝对Forward方向为从0号AprilTag码到9号码方向
        /// </summary>
        Dictionary<RunCMDMode, RunCMDMode> DirectionConversionDic = new Dictionary<RunCMDMode, RunCMDMode>();
        Dictionary<RunCMDMode, int> DirectionToSendIntDic = new Dictionary<RunCMDMode, int>();
        /// <summary>
        /// 一个0~15的十进制数转成16进制字符
        /// </summary>
        /// <param name="DecNum">0~15的十进制</param>
        /// <returns>16进制字符</returns>
        string DecCharToHexChar(int DecNum)
        {
            try
            {
                if (DecNum < 10 && DecNum >= 0)
                {
                    return DecNum.ToString();
                }
                else if (DecNum == 10)
                {
                    return "A";
                }
                else if (DecNum == 11)
                {
                    return "B";
                }
                else if (DecNum == 12)
                {
                    return "C";
                }
                else if (DecNum == 13)
                {
                    return "D";
                }
                else if (DecNum == 14)
                {
                    return "E";
                }
                else if (DecNum == 15)
                {
                    return "F";
                }
                else
                {
                    throw new Exception("函数输入的数字不是0~15内的整数");                
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 一个8位二进制存储的0~255的十进制转换成16进制字符串，如“A2”
        /// </summary>
        /// <param name="DecNum">0~255的十进制</param>
        /// <returns>16进制字符串</returns>
        string DecToHexStr(int DecNum)
        {
            string HexStr = "";
            int Num1 = DecNum / 16;
            int Num2 = DecNum % 16;
            HexStr += DecCharToHexChar(Num1);
            HexStr += DecCharToHexChar(Num2);
            return HexStr;
        }
        public string CreateSendCMDStr(List<int> MoveSequence)
        {
            #region 初始化字典
            DirectionConversionDic = new Dictionary<RunCMDMode, RunCMDMode>();
            DirectionToSendIntDic = new Dictionary<RunCMDMode, int>();
            DirectionToSendIntDic.Add(RunCMDMode.Forward, 0);
            DirectionToSendIntDic.Add(RunCMDMode.Back, 1);
            DirectionToSendIntDic.Add(RunCMDMode.Left, 2);
            DirectionToSendIntDic.Add(RunCMDMode.Right, 3);
            DirectionToSendIntDic.Add(RunCMDMode.Stop, 4);
            DirectionToSendIntDic.Add(RunCMDMode.HoldChess, 5);
            DirectionToSendIntDic.Add(RunCMDMode.DownChess, 6);
            #endregion
            string CMDBuff = "FF ";
            int index = 0;
            for (int i = 0; i < MoveSequence.Count; i++)//10 23
            {
                int ChessQueenIndex = MoveSequence[i] / 100;
                int LocationRow = (MoveSequence[i] / 10) % 10;
                int LocationCol = MoveSequence[i] % 10;

                int LastLocationRow = LocationRow;
                int LastLocationCol = LocationCol;
                
                int TaskNum = 0;

                if (i == 0)
                {
                    # region 确定入场的方向
                    int[] DistanceArr = new int[4];
                    DistanceArr[0] = LocationRow;
                    DistanceArr[1] = 7 - LocationRow;
                    DistanceArr[2] = LocationCol;
                    DistanceArr[3] = 7 - LocationCol;
                    int MinDistance = 900;
                    int MinIndex = 0;
                    for (int j = 0; j < 4; j++)
                    {
                        if (DistanceArr[j] < MinDistance)
                        {
                            MinIndex = j;
                            MinDistance = DistanceArr[j];
                        }
                    }
                    if (MinIndex == 0)
                    {
                        DirectionConversionDic.Add(RunCMDMode.Forward, RunCMDMode.Forward);
                        DirectionConversionDic.Add(RunCMDMode.Back, RunCMDMode.Back);
                        DirectionConversionDic.Add(RunCMDMode.Left, RunCMDMode.Left);
                        DirectionConversionDic.Add(RunCMDMode.Right, RunCMDMode.Right);
                    }
                    else if (MinIndex == 1)
                    {
                        DirectionConversionDic.Add(RunCMDMode.Forward, RunCMDMode.Back);
                        DirectionConversionDic.Add(RunCMDMode.Back, RunCMDMode.Forward);
                        DirectionConversionDic.Add(RunCMDMode.Left, RunCMDMode.Right);
                        DirectionConversionDic.Add(RunCMDMode.Right, RunCMDMode.Left);                                                
                    }
                    else if (MinIndex == 2)
                    {
                        DirectionConversionDic.Add(RunCMDMode.Forward, RunCMDMode.Right);
                        DirectionConversionDic.Add(RunCMDMode.Back, RunCMDMode.Left);
                        DirectionConversionDic.Add(RunCMDMode.Left, RunCMDMode.Forward);
                        DirectionConversionDic.Add(RunCMDMode.Right, RunCMDMode.Back);
                    }
                    else
                    {
                        DirectionConversionDic.Add(RunCMDMode.Forward, RunCMDMode.Left);
                        DirectionConversionDic.Add(RunCMDMode.Back, RunCMDMode.Right);
                        DirectionConversionDic.Add(RunCMDMode.Left, RunCMDMode.Back);
                        DirectionConversionDic.Add(RunCMDMode.Right, RunCMDMode.Forward); 
                    }
                    TaskNum = DirectionToSendIntDic[RunCMDMode.Forward] * 10 + MinDistance + 1;

                    CMDBuff += DecToHexStr(TaskNum);
                    CMDBuff += " ";
                    CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.Stop] * 10 + 1);
                    CMDBuff += " ";
                    CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.HoldChess] * 10 + 1);
                    CMDBuff += " ";
                    # endregion
                }
                else
                {
                    LastLocationRow = (MoveSequence[i - 1] / 10) % 10;
                    LastLocationCol = MoveSequence[i - 1] % 10;
                    int LocationCha = 0;
                    if (LastLocationRow == LocationRow)//同行，只左右动
                    {
                        LocationCha = LocationCol - LastLocationCol;
                        if (LocationCha > 0)//左移
                        {
                            TaskNum = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Left]] * 10 + Math.Abs(LocationCha);
                        }
                        else
                        {
                            TaskNum = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Right]] * 10 + Math.Abs(LocationCha); 
                        }
                        CMDBuff += DecToHexStr(TaskNum);
                        CMDBuff += " ";
                        CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.Stop] * 10 + 1);
                        CMDBuff += " ";
                        if (ChessQueenIndex == 1)//棋子
                        {
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.HoldChess] * 10 + 1);
                        }
                        else
                        {
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.DownChess] * 10 + 1); 
                        }
                        CMDBuff += " ";
                    }
                    else if (LastLocationCol == LocationCol)//同列，只前后动
                    {
                        LocationCha = LocationRow - LastLocationRow;
                        if (LocationCha > 0)//前移
                        {
                            TaskNum = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Forward]] * 10 + Math.Abs(LocationCha);
                        }
                        else
                        {
                            TaskNum = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Back]] * 10 + Math.Abs(LocationCha);
                        }
                        CMDBuff += DecToHexStr(TaskNum);
                        CMDBuff += " ";
                        CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.Stop] * 10 + 1);
                        CMDBuff += " ";
                        if (ChessQueenIndex == 1)//棋子
                        {
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.HoldChess] * 10 + 1);
                        }
                        else
                        {
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.DownChess] * 10 + 1);
                        }
                        CMDBuff += " ";
                    }
                    else//不同行且不同列
                    {
                        int RowCha = LocationRow - LastLocationRow;
                        int ColCha = LocationCol - LastLocationCol;
                        #region 四方位路径解算
                        if (RowCha > 0 && ColCha > 0)//右下,需要前左移动
                        {
                            TaskNum = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Forward]] * 10 + Math.Abs(RowCha);
                            CMDBuff += DecToHexStr(TaskNum);
                            CMDBuff += " ";
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.Stop] * 10 + 1);
                            CMDBuff += " ";
                            TaskNum = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Left]] * 10 + Math.Abs(ColCha);
                            CMDBuff += DecToHexStr(TaskNum);
                            CMDBuff += " ";
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.Stop] * 10 + 1);
                            CMDBuff += " ";
                        }
                        else if (RowCha > 0 && ColCha < 0)//左下，需要前右移动
                        {
                            TaskNum = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Forward]] * 10 + Math.Abs(RowCha);
                            CMDBuff += DecToHexStr(TaskNum);
                            CMDBuff += " ";
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.Stop] * 10 + 1);
                            CMDBuff += " ";
                            TaskNum = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Right]] * 10 + Math.Abs(ColCha);
                            CMDBuff += DecToHexStr(TaskNum);
                            CMDBuff += " ";
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.Stop] * 10 + 1);
                            CMDBuff += " ";
                        }
                        else if (RowCha < 0 && ColCha > 0)//右上，需要后左移动
                        {
                            TaskNum = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Back]] * 10 + Math.Abs(RowCha);
                            CMDBuff += DecToHexStr(TaskNum);
                            CMDBuff += " ";
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.Stop] * 10 + 1);
                            CMDBuff += " ";
                            TaskNum = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Left]] * 10 + Math.Abs(ColCha);
                            CMDBuff += DecToHexStr(TaskNum);
                            CMDBuff += " ";
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.Stop] * 10 + 1);
                            CMDBuff += " ";
                        }
                        else if (RowCha < 0 && ColCha < 0)//左上，需要后右移动
                        {
                            TaskNum = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Back]] * 10 + Math.Abs(RowCha);
                            CMDBuff += DecToHexStr(TaskNum);
                            CMDBuff += " ";
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.Stop] * 10 + 1);
                            CMDBuff += " ";
                            TaskNum = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Right]] * 10 + Math.Abs(ColCha);
                            CMDBuff += DecToHexStr(TaskNum);
                            CMDBuff += " ";
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.Stop] * 10 + 1);
                            CMDBuff += " ";
                        }
                        #endregion
                        if (ChessQueenIndex == 1)//棋子
                        {
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.HoldChess] * 10 + 1);
                        }
                        else
                        {
                            CMDBuff += DecToHexStr(DirectionToSendIntDic[RunCMDMode.DownChess] * 10 + 1);
                        }
                        CMDBuff += " ";
                    }
                }
            }
            # region 出场解算
            int LocationRow_End = (MoveSequence.Last() / 10) % 10;
            int LocationCol_End = MoveSequence.Last() % 10;
            int[] DistanceArr2 = new int[4];
            DistanceArr2[0] = 7 - LocationRow_End;
            DistanceArr2[1] = LocationRow_End;
            DistanceArr2[2] = 7 - LocationCol_End;
            DistanceArr2[3] = LocationCol_End;
            int MinDistance2 = 900;
            int MinIndex2 = 0;
            for (int j = 0; j < 4; j++)
            {
                if (DistanceArr2[j] < MinDistance2)
                {
                    MinIndex2 = j;
                    MinDistance2 = DistanceArr2[j];
                }
            }
            int TaskNum2 = 0;
            if (MinIndex2 == 0)
            {
                TaskNum2 = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Forward]] * 10 + MinDistance2 + 1;
            }
            else if (MinIndex2 == 1)
            {
                TaskNum2 = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Back]] * 10 + MinDistance2 + 1;
            }
            else if (MinIndex2 == 2)
            {
                TaskNum2 = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Left]] * 10 + MinDistance2 + 1;
            }
            else
            {
                TaskNum2 = DirectionToSendIntDic[DirectionConversionDic[RunCMDMode.Right]] * 10 + MinDistance2 + 1;
            }

            CMDBuff += DecToHexStr(TaskNum2);
            CMDBuff += " ";
            # endregion
            CMDBuff += "FE";

            return CMDBuff;
        }
    }
}
