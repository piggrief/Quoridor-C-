﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quoridor_With_C;
using System.Drawing;

namespace Quoridor
{

    /// <summary>
    /// 棋盘格类，定义了棋盘的一个格子这个对象
    /// </summary>
    public class Grid
    {
        public enum GridInsideStatus
        {
            Have_Player1,
            Have_Player2,
            Empty
        }
        public GridInsideStatus GridStatus = GridInsideStatus.Empty;
        public bool IfUpBoard = false;
        public bool IfLeftBoard = false;

        public Grid(){}
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Gs">格子状态</param>
        /// <param name="UpBoard">是否有上挡板</param>
        /// <param name="DownBoard">是否有下挡板</param>
        /// <param name="LeftBoard">是否有左挡板</param>
        /// <param name="RightBoard">是否有右挡板</param>
        public Grid(GridInsideStatus Gs, bool UpBoard, bool LeftBoard)
        {
            GridStatus = Gs;
            IfUpBoard = UpBoard;
            IfLeftBoard = LeftBoard;
        }
    }
    /// <summary>
    /// 棋盘类
    /// </summary>
    public class ChessBoard
    {
        public Grid[,] ChessBoardAll = new Grid[7, 7];
        public static Color Player1ChessColor = Color.White;
        public static Color Player2ChessColor = Color.Black;
        public Point Player1Location = new Point(0, 3);
        public Point Player2Location = new Point(6, 3);


        public ChessBoard()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    ChessBoardAll[i, j] = new Grid();    
                }        
            }
            ChessBoardAll[0, 3].GridStatus = Grid.GridInsideStatus.Have_Player1;
            ChessBoardAll[6, 3].GridStatus = Grid.GridInsideStatus.Have_Player2;

        }
        /// <summary>
        /// 根据棋盘类的棋盘数组画整个棋盘
        /// </summary>
        /// <param name="Gr"></param>
        public void DrawNowChessBoard(ref Graphics Gr, CCWin.SkinControl.SkinPictureBox ChessWhite, CCWin.SkinControl.SkinPictureBox ChessBlack)
        {
            Form1._FormDraw FD = new Form1._FormDraw();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (ChessBoardAll[i, j].GridStatus == Grid.GridInsideStatus.Have_Player1)
                    {
                        ChessWhite.Location = FD.DrawChess(Gr, i, j, Player1ChessColor);
                    }
                    else if (ChessBoardAll[i, j].GridStatus == Grid.GridInsideStatus.Have_Player2)
                    {
                        ChessBlack.Location = FD.DrawChess(Gr, i, j, Player2ChessColor);
                    }
                    if (ChessBoardAll[i, j].IfUpBoard)
                    {
                        FD.DrawBoard(Gr, Form1.NowAction.Action_PlaceHorizontalBoard, i, j, Color.Red);
                    }
                    if (ChessBoardAll[i, j].IfLeftBoard)
                    {
                        FD.DrawBoard(Gr, Form1.NowAction.Action_PlaceVerticalBoard, i, j, Color.Red);
                    }
                }
            } 
        }
        /// <summary>
        /// 行动操作，主要是用来改变棋盘状态数组
        /// </summary>
        /// <param name="row">行动的行</param>
        /// <param name="col">行动的列</param>
        /// <param name="NA">行动操作</param>
        /// <returns>行动结果，可不可行</returns>
        public string Action(int row, int col, Form1.NowAction NA)
        {
            switch (NA)
            {
                case Form1.NowAction.Action_PlaceVerticalBoard:
                    if (col <= 0 || col >= 7 || row >= 6) return "VerticalBoardPlaceError!";
                    if (ChessBoardAll[row, col].IfLeftBoard || ChessBoardAll[row+1, col].IfLeftBoard) return "This has a VerticalBoard!";
                    ChessBoardAll[row, col].IfLeftBoard = true;
                    ChessBoardAll[row+1, col].IfLeftBoard = true;
                    return "OK";
                case Form1.NowAction.Action_PlaceHorizontalBoard:
                    if (row <= 0 || row >= 7 || col >= 6) return "HorizontalBoardPlaceError!";
                    if (ChessBoardAll[row, col].IfUpBoard || ChessBoardAll[row, col+1].IfUpBoard) return "This has a HorizontalBoard!";
                    ChessBoardAll[row, col].IfUpBoard = true;
                    ChessBoardAll[row, col+1].IfUpBoard = true;
                    return "OK";
                case Form1.NowAction.Action_Move_Player1:
                    if (ChessBoardAll[row, col].GridStatus != Grid.GridInsideStatus.Empty) return "This Not Empty";
                    //前扫一格
                    if (row >= 1  
                        && !(ChessBoardAll[row, col].IfUpBoard))//上扫
                    {
                        if (ChessBoardAll[row - 1, col].GridStatus == Grid.GridInsideStatus.Have_Player1)
                        {
                            ChessBoardAll[row - 1, col].GridStatus = Grid.GridInsideStatus.Empty;
                            ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player1;
                            Player1Location = new Point(row,col);
                            return "OK";
                        }
                        else if (ChessBoardAll[row - 1, col].GridStatus == Grid.GridInsideStatus.Have_Player2)
                        {
                            if (row >= 2
                                && ChessBoardAll[row - 2, col].GridStatus == Grid.GridInsideStatus.Have_Player1
                                && !(ChessBoardAll[row - 2, col].IfUpBoard))
                            {
                                ChessBoardAll[row - 2, col].GridStatus = Grid.GridInsideStatus.Empty;
                                ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player1;
                                Player1Location = new Point(row, col);
                                return "OK"; 
                            }
                        }
                    }
                    if (row <= 5
                        && !(ChessBoardAll[row + 1, col].IfUpBoard))//下扫
                    {
                        if (ChessBoardAll[row + 1, col].GridStatus == Grid.GridInsideStatus.Have_Player1)
                        {
                            ChessBoardAll[row + 1, col].GridStatus = Grid.GridInsideStatus.Empty;
                            ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player1;
                            Player1Location = new Point(row, col);
                            return "OK";
                        }
                        else if (ChessBoardAll[row + 1, col].GridStatus == Grid.GridInsideStatus.Have_Player2)
                        {
                            if (row <= 4
                                && ChessBoardAll[row + 2, col].GridStatus == Grid.GridInsideStatus.Have_Player1
                                && !(ChessBoardAll[row + 2, col].IfUpBoard))
                            {
                                ChessBoardAll[row + 2, col].GridStatus = Grid.GridInsideStatus.Empty;
                                ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player1;
                                Player1Location = new Point(row, col);
                                return "OK"; 
                            }
                        }
                    }
                    if (col >= 1 
                        && !(ChessBoardAll[row, col].IfLeftBoard))//左扫
                    {
                        if(ChessBoardAll[row, col - 1].GridStatus == Grid.GridInsideStatus.Have_Player1)
                        { 
                            ChessBoardAll[row, col - 1].GridStatus = Grid.GridInsideStatus.Empty;
                            ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player1;
                            Player1Location = new Point(row, col);
                            return "OK";
                        }
                        else if (ChessBoardAll[row, col - 1].GridStatus == Grid.GridInsideStatus.Have_Player2)
                        {
                            if (col >= 2
                                && ChessBoardAll[row, col - 2].GridStatus == Grid.GridInsideStatus.Have_Player1
                                && !(ChessBoardAll[row, col - 1].IfLeftBoard))
                            {
                                ChessBoardAll[row, col - 2].GridStatus = Grid.GridInsideStatus.Empty;
                                ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player1;
                                Player1Location = new Point(row, col);
                                return "OK"; 
                            }
                        }
                    }
                    if (col <= 5 
                        && !(ChessBoardAll[row, col + 1].IfLeftBoard))//右扫
                    {
                        if (ChessBoardAll[row, col + 1].GridStatus == Grid.GridInsideStatus.Have_Player1)
                        {
                            ChessBoardAll[row, col + 1].GridStatus = Grid.GridInsideStatus.Empty;
                            ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player1;
                            Player1Location = new Point(row, col);
                            return "OK";
                        }
                        else if (ChessBoardAll[row, col + 1].GridStatus == Grid.GridInsideStatus.Have_Player2)
                        {
                            if(col <= 4
                                && ChessBoardAll[row, col + 2].GridStatus == Grid.GridInsideStatus.Have_Player1
                                && !(ChessBoardAll[row, col + 2].IfLeftBoard))
                            {
                                ChessBoardAll[row, col + 2].GridStatus = Grid.GridInsideStatus.Empty;
                                ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player1;
                                Player1Location = new Point(row, col);
                                return "OK";
                            }
                        }
                    }

                    return "MoveError";
                case Form1.NowAction.Action_Move_Player2:
                    if (ChessBoardAll[row, col].GridStatus != Grid.GridInsideStatus.Empty) return "This Not Empty";
                    if (row >= 1  
                        && !(ChessBoardAll[row, col].IfUpBoard))//上扫
                    {
                        if (ChessBoardAll[row - 1, col].GridStatus == Grid.GridInsideStatus.Have_Player2)
                        {
                            ChessBoardAll[row - 1, col].GridStatus = Grid.GridInsideStatus.Empty;
                            ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player2;
                            Player2Location = new Point(row, col);
                            return "OK";
                        }
                        else if (ChessBoardAll[row - 1, col].GridStatus == Grid.GridInsideStatus.Have_Player1)
                        {
                            if (row >= 2
                                && ChessBoardAll[row - 2, col].GridStatus == Grid.GridInsideStatus.Have_Player2
                                && !(ChessBoardAll[row - 2, col].IfUpBoard))
                            {
                                ChessBoardAll[row - 2, col].GridStatus = Grid.GridInsideStatus.Empty;
                                ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player2;
                                Player2Location = new Point(row, col);
                                return "OK"; 
                            }
                        }
                    }
                    if (row <= 5
                        && !(ChessBoardAll[row + 1, col].IfUpBoard))//下扫
                    {
                        if (ChessBoardAll[row + 1, col].GridStatus == Grid.GridInsideStatus.Have_Player2)
                        {
                            ChessBoardAll[row + 1, col].GridStatus = Grid.GridInsideStatus.Empty;
                            ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player2;
                            Player2Location = new Point(row, col);
                            return "OK";
                        }
                        else if (ChessBoardAll[row + 1, col].GridStatus == Grid.GridInsideStatus.Have_Player1)
                        {
                            if (row <= 4
                                && ChessBoardAll[row + 2, col].GridStatus == Grid.GridInsideStatus.Have_Player2
                                && !(ChessBoardAll[row + 2, col].IfUpBoard))
                            {
                                ChessBoardAll[row + 2, col].GridStatus = Grid.GridInsideStatus.Empty;
                                ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player2;
                                Player2Location = new Point(row, col);
                                return "OK"; 
                            }
                        }
                    }
                    if (col >= 1 
                        && !(ChessBoardAll[row, col].IfLeftBoard))//左扫
                    {
                        if(ChessBoardAll[row, col - 1].GridStatus == Grid.GridInsideStatus.Have_Player2)
                        { 
                            ChessBoardAll[row, col - 1].GridStatus = Grid.GridInsideStatus.Empty;
                            ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player2;
                            Player2Location = new Point(row, col);
                            return "OK";
                        }
                        else if (ChessBoardAll[row, col - 1].GridStatus == Grid.GridInsideStatus.Have_Player1)
                        {
                            if (col >= 2
                                && ChessBoardAll[row, col - 2].GridStatus == Grid.GridInsideStatus.Have_Player2
                                && !(ChessBoardAll[row, col - 1].IfLeftBoard))
                            {
                                ChessBoardAll[row, col - 2].GridStatus = Grid.GridInsideStatus.Empty;
                                ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player2;
                                Player2Location = new Point(row, col);
                                return "OK"; 
                            }
                        }
                    }
                    if (col <= 5 
                        && !(ChessBoardAll[row, col + 1].IfLeftBoard))//右扫
                    {
                        if (ChessBoardAll[row, col + 1].GridStatus == Grid.GridInsideStatus.Have_Player2)
                        {
                            ChessBoardAll[row, col + 1].GridStatus = Grid.GridInsideStatus.Empty;
                            ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player2;
                            Player2Location = new Point(row, col);
                            return "OK";
                        }
                        else if (ChessBoardAll[row, col + 1].GridStatus == Grid.GridInsideStatus.Have_Player1)
                        {
                            if(col <= 4
                                && ChessBoardAll[row, col + 2].GridStatus == Grid.GridInsideStatus.Have_Player2
                                && !(ChessBoardAll[row, col + 2].IfLeftBoard))
                            {
                                ChessBoardAll[row, col + 2].GridStatus = Grid.GridInsideStatus.Empty;
                                ChessBoardAll[row, col].GridStatus = Grid.GridInsideStatus.Have_Player2;
                                Player2Location = new Point(row, col);
                                return "OK";
                            }
                        }
                    }
                    return "MoveError";
                case Form1.NowAction.Action_Wait:
                    return "OK";
                default:
                    break;
            }
            return "未知……";
        }


    }
    /// <summary>
    /// 步步为营游戏类
    /// </summary>
    public class QuoridorGame
    {
        public ChessBoard ThisChessBoard = new ChessBoard();
        /// <summary>
        /// 检测游戏是否结束
        /// </summary>
        /// <returns>代表游戏状态</returns>
        public string CheckResult()
        {
            for (int i = 0; i < 7; i++)
            {
                if (ThisChessBoard.ChessBoardAll[0, i].GridStatus == Grid.GridInsideStatus.Have_Player2)
                    return "Player2 Success!";
            }
            for (int i = 0; i < 7; i++)
            {
                if (ThisChessBoard.ChessBoardAll[6, i].GridStatus == Grid.GridInsideStatus.Have_Player1)
                    return "Player1 Success!";
            }

            return "No success";
        }
        /// <summary>
        /// 计算两点间的曼哈顿距离
        /// </summary>
        /// <param name="P0">第一个点</param>
        /// <param name="P1">第二个点</param>
        /// <returns>曼哈顿距离</returns>
        public int CalDistance_Manhattan(Point P0, Point P1)
        {
            return Math.Abs(P0.X - P1.X) + Math.Abs(P0.Y - P1.Y);
        }
        public int Min_DistanceLength = 0;
        public bool Astar_Stop = false;
        public class AstarList
        {
            public int H = 0;
            public int G = 0;
            public int F = 999;
            public int Grid_row = -1;
            public int Grid_col = -1;
            public AstarList(int HH, int GG, int FF, int row, int col)
            { 
                H = HH;
                G = GG;
                F = FF;
                Grid_row = row;
                Grid_col = col;
            }
        }
        public int LookupRoad_Astar(Form1.EnumNowPlayer Player, int Location_row, int Location_col, int num_renew, List<AstarList> OpenList, List<AstarList> CloseList)
        {
            if (Astar_Stop == true)
                return Min_DistanceLength;

            int Row_Destination = 0;
            #region 设置目的地行
            switch (Player)
            {
                case Form1.EnumNowPlayer.Player1:
                    Row_Destination = 6;
                    break;
                case Form1.EnumNowPlayer.Player2:
                    Row_Destination = 0;
                    break;
                default:
                    break;
            }
            #endregion
            #region 上下扫描是否有木板，用来减少搜索空间
            bool flag_NoBoard = true;
            bool flag_UpNowBoard = true;
            bool flag_DownNowBoard = true;

            for (int i = Location_row + 1; i <= Row_Destination; i++)//下扫
            {
                if (ThisChessBoard.ChessBoardAll[i, Location_col].IfUpBoard)
                {
                    flag_DownNowBoard = false;
                    break;
                }
            }
            for (int i = Location_row - 1; i >= Row_Destination; i--)//上扫
            {
                if (ThisChessBoard.ChessBoardAll[i + 1, Location_col].IfUpBoard)
                {
                    flag_UpNowBoard = false;
                    break;
                }
            }
            if (flag_DownNowBoard && flag_UpNowBoard)
                flag_NoBoard = true;
            else
                flag_NoBoard = false;

            if (flag_NoBoard)
            {
                Astar_Stop = true;
                Min_DistanceLength += Math.Abs((Row_Destination - Location_row)); 
                return Math.Abs((Row_Destination - Location_row));
            }

            #endregion

            #region 检查四周能移动的位置添加进P_List_Enable列表
            //计算四周能移动的位置的距离
            List<Point> P_List_Enable = new List<Point>();
            //左
            if (Location_col > 0
                && !(ThisChessBoard.ChessBoardAll[Location_row, Location_col].IfLeftBoard))
                P_List_Enable.Add(new Point(Location_row, Location_col - 1));
            //右
            if (Location_col < 6
                && !(ThisChessBoard.ChessBoardAll[Location_row, Location_col + 1].IfLeftBoard))
                P_List_Enable.Add(new Point(Location_row, Location_col + 1));
            //上
            if (Location_row > 0
                && !(ThisChessBoard.ChessBoardAll[Location_row, Location_col].IfUpBoard))
                P_List_Enable.Add(new Point(Location_row - 1, Location_col));
            //下
            if (Location_row < 6
                && !(ThisChessBoard.ChessBoardAll[Location_row + 1, Location_col].IfUpBoard))
                P_List_Enable.Add(new Point(Location_row + 1, Location_col));
            #endregion


            #region 搜索树搜索策略——A*算法
            List<int> P_Dis = new List<int>();
            for (int i = 0; i < P_List_Enable.Count; i++)
            {
                P_Dis.Add(999);    
            }
            int minF = 9999;
            int minindex = 0;
            for (int i = 0; i < P_List_Enable.Count; i++)
            {
                int Hbuff = Math.Abs(P_List_Enable[i].X - Row_Destination);
                P_Dis[i] = Hbuff;
                int Gbuff = num_renew;
                int Fbuff = Hbuff + Gbuff;
                bool flag_InClose = false;
                //检测是否在Close列表里
                for (int j = 0; j < CloseList.Count; j++)
                {
                    if (P_List_Enable[i].X == CloseList[j].Grid_row && P_List_Enable[i].Y == CloseList[j].Grid_col)
                    {
                        if (Gbuff < CloseList[j].G)
                        {
                            CloseList[j].Grid_row = P_List_Enable[i].X;
                            CloseList[j].Grid_col = P_List_Enable[i].Y;
                            CloseList[j].H = Hbuff;
                            CloseList[j].G = Gbuff;
                            CloseList[j].F = Fbuff;
                        }
                        P_List_Enable.Remove(P_List_Enable[i]);
                        P_Dis.Remove(P_Dis[i]);
                        i--;
                        flag_InClose = true;
                        break;
                    }
                }

                if (!flag_InClose)
                {
                    AstarList NewGrid = new AstarList(Hbuff, Gbuff, Fbuff, P_List_Enable[i].X, P_List_Enable[i].Y);
                    OpenList.Add(NewGrid); 
                }

            }
            AstarList MinFGrid = new AstarList(-1, -1, -1, -1, -1);
            for (int i = 0; i < OpenList.Count; i++)
            {
                int Fbuff = OpenList[i].F;
                if (Fbuff < minF)
                {
                    minF = Fbuff;
                    minindex = i;
                    MinFGrid = OpenList[i];
                }                
            }
            CloseList.Add(MinFGrid);

            int dislengthbuff = 0;
            if (MinFGrid.Grid_row == Row_Destination && Astar_Stop == false)
            {
                Min_DistanceLength += MinFGrid.G;
                Astar_Stop = true;
                return Min_DistanceLength;
            }
            else
            {
                if (OpenList.Count > 0)
                {
                    OpenList.Remove(MinFGrid);
                    dislengthbuff = LookupRoad_Astar(Player, MinFGrid.Grid_row, MinFGrid.Grid_col, num_renew + 1, OpenList, CloseList);
                }
                else
                    return 999;
            }
            #endregion

            return dislengthbuff;
        }
        /// <summary>
        /// 寻找路径长度——贪婪方法
        /// </summary>
        /// <param name="Player"></param>
        /// <param name="Location_row"></param>
        /// <param name="Location_col"></param>
        /// <returns></returns>
        public int LookupRoad_Greedy(Form1.EnumNowPlayer Player, int Location_row, int Location_col, List<Point>MovedPoint)
        {
            int Row_Destination = 0;
            #region 设置目的地行
            switch (Player)
            {
                case Form1.EnumNowPlayer.Player1:
                    Row_Destination = 6;
                    break;
                case Form1.EnumNowPlayer.Player2:
                    Row_Destination = 0;
                    break;
                default:
                    break;
            }
            #endregion
            #region 上下扫描是否有木板，用来减少搜索空间
            bool flag_NoBoard = true;
            bool flag_UpNowBoard = true;
            bool flag_DownNowBoard = true;

            for (int i = Location_row + 1; i <= Row_Destination; i++)//下扫
            {
                if (ThisChessBoard.ChessBoardAll[i, Location_col].IfUpBoard)
                {
                    flag_DownNowBoard = false;
                    break;
                }
            }
            for (int i = Location_row - 1; i >= Row_Destination; i--)//上扫
            {
                if (ThisChessBoard.ChessBoardAll[i + 1, Location_col].IfUpBoard)
                {
                    flag_UpNowBoard = false;
                    break;
                }
            }
            if (flag_DownNowBoard && flag_UpNowBoard)
                flag_NoBoard = true;
            else
                flag_NoBoard = false;
            
            if(flag_NoBoard)
                return Math.Abs((Row_Destination - Location_row));

            #endregion
            #region 检查四周能移动的位置添加进P_List_Enable列表
            //计算四周能移动的位置的距离
            List<Point> P_List_Enable = new List<Point>();
            //左
            if (Location_col > 0 
                && !(ThisChessBoard.ChessBoardAll[Location_row, Location_col].IfLeftBoard)
                && !MovedPoint.Contains(new Point(Location_row, Location_col - 1)))
                P_List_Enable.Add(new Point(Location_row, Location_col - 1));
            //右
            if (Location_col < 6 
                && !(ThisChessBoard.ChessBoardAll[Location_row, Location_col + 1].IfLeftBoard)
                && !MovedPoint.Contains(new Point(Location_row, Location_col + 1)))
                P_List_Enable.Add(new Point(Location_row, Location_col + 1));
            //上
            if (Location_row > 0 
                && !(ThisChessBoard.ChessBoardAll[Location_row, Location_col].IfUpBoard)
                && !MovedPoint.Contains(new Point(Location_row - 1, Location_col)))
                P_List_Enable.Add(new Point(Location_row - 1, Location_col));
            //下
            if (Location_row < 6 
                && !(ThisChessBoard.ChessBoardAll[Location_row + 1, Location_col].IfUpBoard)
                && !MovedPoint.Contains(new Point(Location_row + 1, Location_col)))
                P_List_Enable.Add(new Point(Location_row + 1, Location_col));
            if (P_List_Enable.Count == 0)
                return 999;
            #endregion

            #region 搜索树搜索策略——贪婪
            int[] P_Dis = new int[P_List_Enable.Count];
            int mindis = 999;
            int minindex = 0;
            //选择距离最近的点尝试
            for (int i = 0; i < P_List_Enable.Count; i++)
            {
                P_Dis[i] = CalDistance(Player, P_List_Enable[i].X, P_List_Enable[i].Y);
                if (P_Dis[i] < mindis)
                { 
                    mindis = P_Dis[i];
                    minindex = i;
                }
            }

            MovedPoint.Add(new Point(P_List_Enable[minindex].X, P_List_Enable[minindex].Y));
            int dissum = 0;
            int disbuff = LookupRoad_Greedy(Player, P_List_Enable[minindex].X, P_List_Enable[minindex].Y, MovedPoint);

            #endregion

            if (disbuff != 999)
            { 
                dissum += disbuff;
                return dissum;
            }
          
            return 999;
        }
        /// <summary>
        /// 计算某点的距离
        /// </summary>
        /// <param name="Player">要检测哪个玩家会被这步挡板堵死</param>
        /// <param name="Location_row">某点的行</param>
        /// <param name="Location_col">某点的列</param>
        /// <returns></returns>
        public int CalDistance(Form1.EnumNowPlayer Player, int Location_row, int Location_col)
        {
            int Row_Destination = 0;

            switch (Player)
            {
                case Form1.EnumNowPlayer.Player1:
                    Row_Destination = 6;
                    break;
                case Form1.EnumNowPlayer.Player2:
                    Row_Destination = 0;
                    break;
                default:
                    break;
            }

            int Num_VBoard = 0, Num_HBoard = 0;
            for (int i = Location_row + 1; i <= Row_Destination; i++)//下扫
            {
                if (ThisChessBoard.ChessBoardAll[i, Location_col].IfUpBoard)
                    Num_HBoard++;
            }
            for (int i = Location_row - 1; i >= Row_Destination; i--)//上扫
            {
                if (ThisChessBoard.ChessBoardAll[i + 1, Location_col].IfUpBoard)
                    Num_HBoard++;
            }
            for (int i = Location_col - 1; i >= 0; i--)//左扫
            {
                if (ThisChessBoard.ChessBoardAll[Location_row, i + 1].IfLeftBoard)
                    Num_VBoard++;
            }
            for (int i = Location_col + 1; i <= 6; i++)//右扫
            {
                if (ThisChessBoard.ChessBoardAll[Location_row, i - 1].IfLeftBoard)
                    Num_VBoard++;
            }

            return Num_HBoard + Num_VBoard;
        }
        /// <summary>
        /// 检测该挡板是否能被放下
        /// </summary>
        /// <param name="WhichBoard">放置哪种挡板</param>
        /// <param name="Player">检测哪个玩家会被堵死</param>
        /// <param name="Location_row">玩家的位置行</param>
        /// <param name="Location_col">玩家的位置列</param>
        /// <returns></returns>
        public int CheckBoard(Form1.NowAction WhichBoard, Form1.EnumNowPlayer Player, int Location_row, int Location_col)
        {
            ///为了不改变原状态而暂存原状态以便后续恢复
            ChessBoard ChessBoardBuff = new ChessBoard();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    ChessBoardBuff.ChessBoardAll[i, j].IfLeftBoard = ThisChessBoard.ChessBoardAll[i, j].IfLeftBoard;
                    ChessBoardBuff.ChessBoardAll[i, j].IfUpBoard = ThisChessBoard.ChessBoardAll[i, j].IfUpBoard;
                    ChessBoardBuff.ChessBoardAll[i, j].GridStatus = ThisChessBoard.ChessBoardAll[i, j].GridStatus;
                }
            }
            ChessBoardBuff.Player1Location.X = ThisChessBoard.Player1Location.X;
            ChessBoardBuff.Player1Location.Y = ThisChessBoard.Player1Location.Y;
            ChessBoardBuff.Player2Location.X = ThisChessBoard.Player2Location.X;
            ChessBoardBuff.Player2Location.Y = ThisChessBoard.Player2Location.Y;

            //假设能放挡板
            ThisChessBoard.Action(Location_row, Location_col, WhichBoard);

            int disbuff = 0;
            List<AstarList> InitAList = new List<AstarList>();
            Astar_Stop = false;
            Min_DistanceLength = 0;
            if (Player == Form1.EnumNowPlayer.Player1)
            {
                //disbuff = LookupRoad_Greedy(Player
                //    , ThisChessBoard.Player1Location.X, ThisChessBoard.Player1Location.Y
                //    , Moved);
                InitAList.Add(new AstarList(6,0,6,ThisChessBoard.Player1Location.X,ThisChessBoard.Player1Location.Y));
                disbuff = LookupRoad_Astar(Player
                    , ThisChessBoard.Player1Location.X
                    , ThisChessBoard.Player1Location.Y
                    , 1, new List<AstarList>()
                    , InitAList);
            }
            else
            {
                //disbuff = LookupRoad_Greedy(Player
                //    , ThisChessBoard.Player2Location.X, ThisChessBoard.Player2Location.Y
                //    , Moved); 
                InitAList.Add(new AstarList(6, 0, 6, ThisChessBoard.Player2Location.X, ThisChessBoard.Player2Location.Y));
                disbuff = LookupRoad_Astar(Player
                    , ThisChessBoard.Player2Location.X
                    , ThisChessBoard.Player2Location.Y
                    , 1, new List<AstarList>()
                    , InitAList);
            }

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    ThisChessBoard.ChessBoardAll[i, j].IfLeftBoard = ChessBoardBuff.ChessBoardAll[i, j].IfLeftBoard;
                    ThisChessBoard.ChessBoardAll[i, j].IfUpBoard = ChessBoardBuff.ChessBoardAll[i, j].IfUpBoard;
                    ThisChessBoard.ChessBoardAll[i, j].GridStatus = ChessBoardBuff.ChessBoardAll[i, j].GridStatus;
                }
            }
            ThisChessBoard.Player1Location.X = ChessBoardBuff.Player1Location.X;
            ThisChessBoard.Player1Location.Y = ChessBoardBuff.Player1Location.Y;
            ThisChessBoard.Player2Location.X = ChessBoardBuff.Player2Location.X;
            ThisChessBoard.Player2Location.Y = ChessBoardBuff.Player2Location.Y;

            if (disbuff >= 999)
            {
                return 999;
            }
            else
            {
                return disbuff;
            }
        }
    }
}
