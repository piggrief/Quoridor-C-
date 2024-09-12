using System;
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
            Gr.Clear(Color.Transparent);
            Gr.DrawImage(Resource1.qipan, 0, 0, Resource1.qipan2019.Width, Resource1.qipan2019.Height);
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
    }
    /// <summary>
    /// Quoridor规则引擎类
    /// </summary>
    public class QuoridorRuleEngine
    {
        public static int NumPlayer1Board = 16;
        public static int NumPlayer2Board = 16;
        LookupRoadAlgorithm AstarEngine = new LookupRoadAlgorithm();

        public string CheckMove_NoChange(ChessBoard ChessBoard_ToCheck, int row, int col, Form1.NowAction NA)
        {
            Grid[,] ChessBoardAll = ChessBoard_ToCheck.ChessBoardAll;

            if (ChessBoardAll[row, col].GridStatus != Grid.GridInsideStatus.Empty) return "This Not Empty";

            Grid.GridInsideStatus ActionPlayer = Grid.GridInsideStatus.Empty;
            Grid.GridInsideStatus AnotherPlayer = Grid.GridInsideStatus.Empty;

            if (NA != Form1.NowAction.Action_Move_Player1
                && NA != Form1.NowAction.Action_Move_Player2)
                return "Error";

            if (NA == Form1.NowAction.Action_Move_Player1)
            {
                ActionPlayer = Grid.GridInsideStatus.Have_Player1;
                AnotherPlayer = Grid.GridInsideStatus.Have_Player2;
            }
            else
            {
                ActionPlayer = Grid.GridInsideStatus.Have_Player2;
                AnotherPlayer = Grid.GridInsideStatus.Have_Player1;
            }

            //前扫一格
            if (row >= 1
                && !(ChessBoardAll[row, col].IfUpBoard))//上扫
            {
                if (ChessBoardAll[row - 1, col].GridStatus == ActionPlayer)
                {
                    return "OK";
                }
                else if (ChessBoardAll[row - 1, col].GridStatus == AnotherPlayer)
                {
                    if (col >= 1
                        && ChessBoardAll[row - 1, col - 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row - 1, col].IfLeftBoard))//左扫
                    {
                        return "OK";
                    }
                    if (col <= 5
                        && ChessBoardAll[row - 1, col + 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row - 1, col + 1].IfLeftBoard))//右扫
                    {
                        return "OK";
                    }
                    if (row >= 2
                        && ChessBoardAll[row - 2, col].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row - 1, col].IfUpBoard))//上扫
                    {
                        return "OK";
                    }
                }
            }
            if (row <= 5
                && !(ChessBoardAll[row + 1, col].IfUpBoard))//下扫
            {
                if (ChessBoardAll[row + 1, col].GridStatus == ActionPlayer)
                {
                    return "OK";
                }
                else if (ChessBoardAll[row + 1, col].GridStatus == AnotherPlayer)
                {
                    if (col >= 1
                        && ChessBoardAll[row + 1, col - 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row + 1, col].IfLeftBoard))//左扫
                    {
                        return "OK";
                    }
                    if (col <= 5
                        && ChessBoardAll[row + 1, col + 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row + 1, col + 1].IfLeftBoard))//右扫
                    {
                        return "OK";
                    }

                    if (row <= 4
                        && ChessBoardAll[row + 2, col].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row + 2, col].IfUpBoard))//下扫
                    {
                        return "OK";
                    }
                }
            }
            if (col >= 1
                && !(ChessBoardAll[row, col].IfLeftBoard))//左扫
            {
                if (ChessBoardAll[row, col - 1].GridStatus == ActionPlayer)
                {
                    return "OK";
                }
                else if (ChessBoardAll[row, col - 1].GridStatus == AnotherPlayer)
                {
                    if (col >= 2
                        && ChessBoardAll[row, col - 2].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row, col - 1].IfLeftBoard))//左扫
                    {
                        return "OK";
                    }
                    if (row <= 5
                        && ChessBoardAll[row + 1, col - 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row + 1, col - 1].IfUpBoard))//下扫
                    {
                        return "OK";
                    }
                    if (row >= 1
                        && ChessBoardAll[row - 1, col - 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row, col - 1].IfUpBoard))//上扫
                    {
                        return "OK";
                    }
                }
            }
            if (col <= 5
                && !(ChessBoardAll[row, col + 1].IfLeftBoard))//右扫
            {
                if (ChessBoardAll[row, col + 1].GridStatus == ActionPlayer)
                {
                    return "OK";
                }
                else if (ChessBoardAll[row, col + 1].GridStatus == AnotherPlayer)
                {
                    if (col <= 4
                        && ChessBoardAll[row, col + 2].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row, col + 2].IfLeftBoard))//右扫
                    {
                        return "OK";
                    }
                    if (row <= 5
                        && ChessBoardAll[row + 1, col + 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row + 1, col + 1].IfUpBoard))//下扫
                    {
                        return "OK";
                    }
                    if (row >= 1
                        && ChessBoardAll[row - 1, col + 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row, col + 1].IfUpBoard))//上扫
                    {
                        return "OK";
                    }
                }
            }

            return "MoveError";
        }

        public string CheckMove_New(ref ChessBoard ChessBoard_ToCheck, int row, int col, Form1.NowAction NA)
        {
            Grid[,] ChessBoardAll = ChessBoard_ToCheck.ChessBoardAll;
            if (ChessBoard_ToCheck.ChessBoardAll[row, col].GridStatus != Grid.GridInsideStatus.Empty) return "This Not Empty";

            Grid.GridInsideStatus ActionPlayer = Grid.GridInsideStatus.Empty;
            Grid.GridInsideStatus AnotherPlayer = Grid.GridInsideStatus.Empty;

            if (NA != Form1.NowAction.Action_Move_Player1
                && NA != Form1.NowAction.Action_Move_Player2)
                return "Error";

            if (NA == Form1.NowAction.Action_Move_Player1)
            {
                ActionPlayer = Grid.GridInsideStatus.Have_Player1;
                AnotherPlayer = Grid.GridInsideStatus.Have_Player2;
            }
            else
            {
                ActionPlayer = Grid.GridInsideStatus.Have_Player2;
                AnotherPlayer = Grid.GridInsideStatus.Have_Player1;
            }

            //前扫一格
            if (row >= 1
                && !(ChessBoardAll[row, col].IfUpBoard))//上扫
            {
                if (ChessBoardAll[row - 1, col].GridStatus == ActionPlayer)
                {
                    ChessBoardAll[row - 1, col].GridStatus = Grid.GridInsideStatus.Empty;
                    ChessBoardAll[row, col].GridStatus = ActionPlayer;
                    if (NA == Form1.NowAction.Action_Move_Player1)
                        ChessBoard_ToCheck.Player1Location = new Point(row, col);
                    else
                        ChessBoard_ToCheck.Player2Location = new Point(row, col);
                    return "OK";
                }
                else if (ChessBoardAll[row - 1, col].GridStatus == AnotherPlayer)
                {
                    if (col >= 1
                        && ChessBoardAll[row - 1, col - 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row - 1, col].IfLeftBoard))//左扫
                    {
                        ChessBoardAll[row - 1, col - 1].GridStatus = Grid.GridInsideStatus.Empty;
                        ChessBoardAll[row, col].GridStatus = ActionPlayer;
                        if (NA == Form1.NowAction.Action_Move_Player1)
                            ChessBoard_ToCheck.Player1Location = new Point(row, col);
                        else
                            ChessBoard_ToCheck.Player2Location = new Point(row, col);
                        return "OK";
                    }
                    if (col <= 5
                        && ChessBoardAll[row - 1, col + 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row - 1, col + 1].IfLeftBoard))//右扫
                    {
                        ChessBoardAll[row - 1, col + 1].GridStatus = Grid.GridInsideStatus.Empty;
                        ChessBoardAll[row, col].GridStatus = ActionPlayer;
                        if (NA == Form1.NowAction.Action_Move_Player1)
                            ChessBoard_ToCheck.Player1Location = new Point(row, col);
                        else
                            ChessBoard_ToCheck.Player2Location = new Point(row, col);
                        return "OK";
                    }
                    if (row >= 2
                        && ChessBoardAll[row - 2, col].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row - 1, col].IfUpBoard))//上扫
                    {
                        ChessBoardAll[row - 2, col].GridStatus = Grid.GridInsideStatus.Empty;
                        ChessBoardAll[row, col].GridStatus = ActionPlayer;
                        if (NA == Form1.NowAction.Action_Move_Player1)
                            ChessBoard_ToCheck.Player1Location = new Point(row, col);
                        else
                            ChessBoard_ToCheck.Player2Location = new Point(row, col);
                        return "OK";
                    }
                }
            }
            if (row <= 5
                && !(ChessBoardAll[row + 1, col].IfUpBoard))//下扫
            {
                if (ChessBoardAll[row + 1, col].GridStatus == ActionPlayer)
                {
                    ChessBoardAll[row + 1, col].GridStatus = Grid.GridInsideStatus.Empty;
                    ChessBoardAll[row, col].GridStatus = ActionPlayer;
                    if (NA == Form1.NowAction.Action_Move_Player1)
                        ChessBoard_ToCheck.Player1Location = new Point(row, col);
                    else
                        ChessBoard_ToCheck.Player2Location = new Point(row, col);
                    return "OK";
                }
                else if (ChessBoardAll[row + 1, col].GridStatus == AnotherPlayer)
                {
                    if (col >= 1
                        && ChessBoardAll[row + 1, col - 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row + 1, col].IfLeftBoard))//左扫
                    {
                        ChessBoardAll[row + 1, col - 1].GridStatus = Grid.GridInsideStatus.Empty;
                        ChessBoardAll[row, col].GridStatus = ActionPlayer;
                        if (NA == Form1.NowAction.Action_Move_Player1)
                            ChessBoard_ToCheck.Player1Location = new Point(row, col);
                        else
                            ChessBoard_ToCheck.Player2Location = new Point(row, col);
                        return "OK";
                    }
                    if (col <= 5
                        && ChessBoardAll[row + 1, col + 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row + 1, col + 1].IfLeftBoard))//右扫
                    {
                        ChessBoardAll[row + 1, col + 1].GridStatus = Grid.GridInsideStatus.Empty;
                        ChessBoardAll[row, col].GridStatus = ActionPlayer;
                        if (NA == Form1.NowAction.Action_Move_Player1)
                            ChessBoard_ToCheck.Player1Location = new Point(row, col);
                        else
                            ChessBoard_ToCheck.Player2Location = new Point(row, col);
                        return "OK";
                    }

                    if (row <= 4
                        && ChessBoardAll[row + 2, col].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row + 2, col].IfUpBoard))//下扫
                    {
                        ChessBoardAll[row + 2, col].GridStatus = Grid.GridInsideStatus.Empty;
                        ChessBoardAll[row, col].GridStatus = ActionPlayer;
                        if (NA == Form1.NowAction.Action_Move_Player1)
                            ChessBoard_ToCheck.Player1Location = new Point(row, col);
                        else
                            ChessBoard_ToCheck.Player2Location = new Point(row, col);
                        return "OK";
                    }
                }
            }
            if (col >= 1
                && !(ChessBoardAll[row, col].IfLeftBoard))//左扫
            {
                if (ChessBoardAll[row, col - 1].GridStatus == ActionPlayer)
                {
                    ChessBoardAll[row, col - 1].GridStatus = Grid.GridInsideStatus.Empty;
                    ChessBoardAll[row, col].GridStatus = ActionPlayer;
                    if (NA == Form1.NowAction.Action_Move_Player1)
                        ChessBoard_ToCheck.Player1Location = new Point(row, col);
                    else
                        ChessBoard_ToCheck.Player2Location = new Point(row, col);
                    return "OK";
                }
                else if (ChessBoardAll[row, col - 1].GridStatus == AnotherPlayer)
                {
                    if (col >= 2
                        && ChessBoardAll[row, col - 2].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row, col - 1].IfLeftBoard))//左扫
                    {
                        ChessBoardAll[row, col - 2].GridStatus = Grid.GridInsideStatus.Empty;
                        ChessBoardAll[row, col].GridStatus = ActionPlayer;
                        if (NA == Form1.NowAction.Action_Move_Player1)
                            ChessBoard_ToCheck.Player1Location = new Point(row, col);
                        else
                            ChessBoard_ToCheck.Player2Location = new Point(row, col);
                        return "OK";
                    }
                    if (row <= 5
                        && ChessBoardAll[row + 1, col - 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row + 1, col - 1].IfUpBoard))//下扫
                    {
                        ChessBoardAll[row + 1, col - 1].GridStatus = Grid.GridInsideStatus.Empty;
                        ChessBoardAll[row, col].GridStatus = ActionPlayer;
                        if (NA == Form1.NowAction.Action_Move_Player1)
                            ChessBoard_ToCheck.Player1Location = new Point(row, col);
                        else
                            ChessBoard_ToCheck.Player2Location = new Point(row, col);
                        return "OK";
                    }
                    if (row >= 1
                        && ChessBoardAll[row - 1, col - 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row, col - 1].IfUpBoard))//上扫
                    {
                        ChessBoardAll[row - 1, col - 1].GridStatus = Grid.GridInsideStatus.Empty;
                        ChessBoardAll[row, col].GridStatus = ActionPlayer;
                        if (NA == Form1.NowAction.Action_Move_Player1)
                            ChessBoard_ToCheck.Player1Location = new Point(row, col);
                        else
                            ChessBoard_ToCheck.Player2Location = new Point(row, col);
                        return "OK";
                    }
                }
            }
            if (col <= 5
                && !(ChessBoardAll[row, col + 1].IfLeftBoard))//右扫
            {
                if (ChessBoardAll[row, col + 1].GridStatus == ActionPlayer)
                {
                    ChessBoardAll[row, col + 1].GridStatus = Grid.GridInsideStatus.Empty;
                    ChessBoardAll[row, col].GridStatus = ActionPlayer;
                    if (NA == Form1.NowAction.Action_Move_Player1)
                        ChessBoard_ToCheck.Player1Location = new Point(row, col);
                    else
                        ChessBoard_ToCheck.Player2Location = new Point(row, col);
                    return "OK";
                }
                else if (ChessBoardAll[row, col + 1].GridStatus == AnotherPlayer)
                {
                    if (col <= 4
                        && ChessBoardAll[row, col + 2].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row, col + 2].IfLeftBoard))//右扫
                    {
                        ChessBoardAll[row, col + 2].GridStatus = Grid.GridInsideStatus.Empty;
                        ChessBoardAll[row, col].GridStatus = ActionPlayer;
                        if (NA == Form1.NowAction.Action_Move_Player1)
                            ChessBoard_ToCheck.Player1Location = new Point(row, col);
                        else
                            ChessBoard_ToCheck.Player2Location = new Point(row, col);
                        return "OK";
                    }
                    if (row <= 5
                        && ChessBoardAll[row + 1, col + 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row + 1, col + 1].IfUpBoard))//下扫
                    {
                        ChessBoardAll[row + 1, col + 1].GridStatus = Grid.GridInsideStatus.Empty;
                        ChessBoardAll[row, col].GridStatus = ActionPlayer;
                        if (NA == Form1.NowAction.Action_Move_Player1)
                            ChessBoard_ToCheck.Player1Location = new Point(row, col);
                        else
                            ChessBoard_ToCheck.Player2Location = new Point(row, col);
                        return "OK";
                    }
                    if (row >= 1
                        && ChessBoardAll[row - 1, col + 1].GridStatus == ActionPlayer
                        && !(ChessBoardAll[row, col + 1].IfUpBoard))//上扫
                    {
                        ChessBoardAll[row - 1, col + 1].GridStatus = Grid.GridInsideStatus.Empty;
                        ChessBoardAll[row, col].GridStatus = ActionPlayer;
                        if (NA == Form1.NowAction.Action_Move_Player1)
                            ChessBoard_ToCheck.Player1Location = new Point(row, col);
                        else
                            ChessBoard_ToCheck.Player2Location = new Point(row, col);
                        return "OK";
                    }
                }
            }

            return "MoveError";
        }

        /// <summary>
        /// 检测该挡板是否能被放下
        /// </summary>
        /// <param name="WhichBoard">放置哪种挡板</param>
        /// <param name="Player">检测哪个玩家会被堵死</param>
        /// <param name="Location_row">玩家的位置行</param>
        /// <param name="Location_col">玩家的位置列</param>
        /// <returns>错误提示符，能被放下就会返回“OK”</returns>
        public string CheckBoard(ChessBoard ChessBoard_ToCheck, Form1.NowAction WhichBoard, Form1.EnumNowPlayer Player, int Location_row, int Location_col)
        {
            ChessBoard ThisChessBoard = ChessBoard_ToCheck;
            if (WhichBoard == Form1.NowAction.Action_Move_Player1 || WhichBoard == Form1.NowAction.Action_Move_Player2)
                return "OK";
            if (Player == Form1.EnumNowPlayer.Player1 && NumPlayer1Board <= 0)
                return "Player1 No Board";
            else if (Player == Form1.EnumNowPlayer.Player2 && NumPlayer2Board <= 0)
                return "Player2 No Board";

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
            string Hint = Action(ref ThisChessBoard,Location_row, Location_col, WhichBoard);
            if (Hint != "OK")
            {
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

                return Hint;
            }

            int disbuff = 0;
            List<LookupRoadAlgorithm.AstarList> InitAList = new List<LookupRoadAlgorithm.AstarList>();
            AstarEngine.Astar_Stop = false;
            AstarEngine.Min_DistanceLength = 0;
            if (Player == Form1.EnumNowPlayer.Player1)
            {
                //disbuff = LookupRoad_Greedy(Player
                //    , ThisChessBoard.Player1Location.X, ThisChessBoard.Player1Location.Y
                //    , Moved);
                LookupRoadAlgorithm.AstarList InitGrid = new LookupRoadAlgorithm.AstarList(6, 0, 6, ThisChessBoard.Player1Location.X, ThisChessBoard.Player1Location.Y);

                InitAList.Add(InitGrid);
                disbuff = AstarEngine.LookupRoad_Astar(ChessBoard_ToCheck,Player
                    , InitGrid
                    , 1, new List<LookupRoadAlgorithm.AstarList>()
                    , InitAList);
            }
            else
            {
                //disbuff = LookupRoad_Greedy(Player
                //    , ThisChessBoard.Player2Location.X, ThisChessBoard.Player2Location.Y
                //    , Moved); 
                LookupRoadAlgorithm.AstarList InitGrid = new LookupRoadAlgorithm.AstarList(6, 0, 6, ThisChessBoard.Player2Location.X, ThisChessBoard.Player2Location.Y);

                InitAList.Add(InitGrid);
                disbuff = AstarEngine.LookupRoad_Astar(ChessBoard_ToCheck,Player
                    , InitGrid
                    , 1, new List<LookupRoadAlgorithm.AstarList>()
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
                return "No Road";
            }
            else
            {
                return "OK";
            }
        }
        /// <summary>
        /// 检测游戏是否结束
        /// </summary>
        /// <returns>代表游戏状态</returns>
        public string CheckResult(ChessBoard ThisChessBoard)
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
        /// 行动操作，主要是用来改变棋盘状态数组
        /// </summary>
        /// <param name="row">行动的行</param>
        /// <param name="col">行动的列</param>
        /// <param name="NA">行动操作</param>
        /// <returns>行动结果，可不可行</returns>
        public string Action(ref ChessBoard ChessBoard_ToAction, int row, int col, Form1.NowAction NA)
        {
            Grid[,] ChessBoardAll = ChessBoard_ToAction.ChessBoardAll;
            switch (NA)
            {
                case Form1.NowAction.Action_PlaceVerticalBoard:
                    if (col <= 0 || col >= 7 || row >= 6) return "VerticalBoardPlaceError!";
                    if (ChessBoardAll[row, col].IfLeftBoard || ChessBoardAll[row + 1, col].IfLeftBoard) return "This has a VerticalBoard!";
                    if (ChessBoardAll[row + 1, col].IfUpBoard && ChessBoardAll[row + 1, col - 1].IfUpBoard)
                        return "十字交叉违规！";
                    ChessBoardAll[row, col].IfLeftBoard = true;
                    ChessBoardAll[row + 1, col].IfLeftBoard = true;
                    return "OK";
                case Form1.NowAction.Action_PlaceHorizontalBoard:
                    if (row <= 0 || row >= 7 || col >= 6) return "HorizontalBoardPlaceError!";
                    if (ChessBoardAll[row, col].IfUpBoard || ChessBoardAll[row, col + 1].IfUpBoard) return "This has a HorizontalBoard!";
                    if (ChessBoardAll[row, col + 1].IfLeftBoard && ChessBoardAll[row - 1, col + 1].IfLeftBoard)
                        return "十字交叉违规！";
                    ChessBoardAll[row, col].IfUpBoard = true;
                    ChessBoardAll[row, col + 1].IfUpBoard = true;
                    return "OK";
                case Form1.NowAction.Action_Move_Player1:
                    return CheckMove_New(ref ChessBoard_ToAction,row, col, Form1.NowAction.Action_Move_Player1);
                case Form1.NowAction.Action_Move_Player2:
                    return CheckMove_New(ref ChessBoard_ToAction,row, col, Form1.NowAction.Action_Move_Player2);
                case Form1.NowAction.Action_Wait:
                    return "OK";
                default:
                    break;
            }
            return "未知……";
        }
        
    }
    /// <summary>
    /// 寻路算法类
    /// </summary>
    public class LookupRoadAlgorithm
    {
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
            public AstarList Father;
        }
        /// <summary>
        /// 重启A*寻路
        /// </summary>
        /// <param name="Player">寻路玩家</param>
        /// <param name="Location_row">该玩家所在位置行</param>
        /// <param name="Location_col">该玩家所在位置列</param>
        /// <returns></returns>
        public int AstarRestart(ChessBoard ToAstarSearch, Form1.EnumNowPlayer Player, int Location_row, int Location_col)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start(); //  开始监视代码运行时间
            /***************待测代码段****************/

            Min_DistanceLength = 0;
            List<AstarList> InitAList = new List<AstarList>();
            Astar_Stop = false;

            AstarList InitGrid = new AstarList(6, 0, 6, Location_row, Location_col);
            InitAList.Add(InitGrid);

            int distance = LookupRoad_Astar(ToAstarSearch, Player, InitGrid, 1,
                new List<AstarList>(), InitAList);

            /***************待测代码段****************/
            stopwatch.Stop(); //  停止监视
            TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间

            if (QuoridorAI.AIRunTime.Astar_s == 0)
            { 
                QuoridorAI.AIRunTime.Astar_s = timespan.TotalMilliseconds;
            }
            else
            {
                QuoridorAI.AIRunTime.Astar_s += timespan.TotalMilliseconds;
                QuoridorAI.AIRunTime.Astar_s /= 2;
            }
            QuoridorAI.AIRunTime.AstarNum++;

            return Min_DistanceLength;

        }
        public List<Point> Player1MinRoad = new List<Point>();
        public List<Point> Player2MinRoad = new List<Point>();
        /// <summary>
        /// A*寻路，最短路径保存在Player1MinRoad或Player2MinRoad中，返回最短路径长度
        /// </summary>
        /// <param name="Player">寻路玩家</param>
        /// <param name="NowGrid">当前寻路的格子</param>
        /// <param name="num_renew">迭代次数</param>
        /// <param name="OpenList">Open列表</param>
        /// <param name="CloseList">Close列表</param>
        /// <returns></returns>
        public int LookupRoad_Astar(ChessBoard ThisChessBoard, Form1.EnumNowPlayer Player, AstarList NowGrid, int num_renew, List<AstarList> OpenList, List<AstarList> CloseList)
        {
            int Location_row = NowGrid.Grid_row;
            int Location_col = NowGrid.Grid_col;

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

            #region 上下扫描是否有木板，用来减少搜索空间
            bool flag_NoBoard = true;
            bool flag_UpNowBoard = true;
            bool flag_DownNowBoard = true;

            for (int k = Location_row + 1; k <= Row_Destination; k++)//下扫
            {
                if (ThisChessBoard.ChessBoardAll[k, Location_col].IfUpBoard)
                {
                    flag_DownNowBoard = false;
                    break;
                }
            }
            for (int k = Location_row - 1; k >= Row_Destination; k--)//上扫
            {
                if (ThisChessBoard.ChessBoardAll[k + 1, Location_col].IfUpBoard)
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
                Min_DistanceLength = Math.Abs((Row_Destination - Location_row)) + CloseList.Last().G;

                #region 迭代寻找最短路径
                List<Point> MinRoad;
                if (Player == Form1.EnumNowPlayer.Player1)
                {
                    Player1MinRoad = new List<Point>();
                    MinRoad = Player1MinRoad;
                }
                else
                {
                    Player2MinRoad = new List<Point>();
                    MinRoad = Player2MinRoad;
                }

                if (Location_row < Row_Destination)
                {
                    for (int i = Row_Destination; i >= Location_row; i--)
                    {
                        MinRoad.Add(new Point(i, Location_col));
                    }
                }
                else
                {
                    for (int i = Row_Destination; i <= Location_row; i++)
                    {
                        MinRoad.Add(new Point(i, Location_col));
                    } 
                }
                AstarList ALBuff = CloseList.Last();
                while (true)
                {
                    if (ALBuff.Father != null)
                    {
                        MinRoad.Add(new Point(ALBuff.Father.Grid_row, ALBuff.Father.Grid_col));
                        ALBuff = ALBuff.Father;
                    }
                    else
                        break;
                }
                #endregion
                return Min_DistanceLength;
            }

            #endregion

            #region 搜索树搜索策略——A*算法
            List<int> P_Dis = new List<int>();
            for (int i = 0; i < P_List_Enable.Count; i++)
            {
                P_Dis.Add(999);    
            }
            int minF = 9999;
            int minindex = 0;
            for (int i = 0; i < P_List_Enable.Count && i >= 0; i++)
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
                        P_List_Enable.Remove(P_List_Enable[i]);
                        P_Dis.Remove(P_Dis[i]);
                        i--;
                        flag_InClose = true;
                        break;
                    }
                }
                if (flag_InClose)
                {
                    continue;
                }

                bool flag_InOpen = false;
                //检测是否在Open列表里
                for (int j = 0; j < OpenList.Count; j++)
                {
                    if (P_List_Enable[i].X == OpenList[j].Grid_row && P_List_Enable[i].Y == OpenList[j].Grid_col)
                    {
                        P_List_Enable.Remove(P_List_Enable[i]);
                        P_Dis.Remove(P_Dis[i]);
                        i--;
                        flag_InOpen = true;

                        if (Gbuff < OpenList[j].G)
                        {
                            OpenList[j].G = Gbuff;
                            OpenList[j].F = Fbuff;
                            OpenList[j].H = Hbuff;
                            OpenList[j].Father = NowGrid;
                        }
                        break;
                    }
                }

                if (!flag_InOpen && !flag_InClose)
                {
                    AstarList NewGrid = new AstarList(Hbuff, Gbuff, Fbuff, P_List_Enable[i].X, P_List_Enable[i].Y);
                    NewGrid.Father = NowGrid;
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
                    dislengthbuff = LookupRoad_Astar(ThisChessBoard, Player, MinFGrid, MinFGrid.G + 1, OpenList, CloseList);
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
        public int LookupRoad_Greedy(ChessBoard ThisChessBoard, Form1.EnumNowPlayer Player, int Location_row, int Location_col, List<Point>MovedPoint)
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
                P_Dis[i] = CalDistance(ThisChessBoard, Player, P_List_Enable[i].X, P_List_Enable[i].Y);
                if (P_Dis[i] < mindis)
                { 
                    mindis = P_Dis[i];
                    minindex = i;
                }
            }

            MovedPoint.Add(new Point(P_List_Enable[minindex].X, P_List_Enable[minindex].Y));
            int dissum = 0;
            int disbuff = LookupRoad_Greedy(ThisChessBoard, Player, P_List_Enable[minindex].X, P_List_Enable[minindex].Y, MovedPoint);

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
        public int CalDistance(ChessBoard ThisChessBoard, Form1.EnumNowPlayer Player, int Location_row, int Location_col)
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

    }
    /// <summary>
    /// 动作类，包含双方评分
    /// </summary>
    public class QuoridorAction
    {
        public Form1.NowAction PlayerAction = Form1.NowAction.Action_Move_Player1;

        public Point ActionPoint = new Point(-1, -1);

        public double SelfScore = 50;
        public double OpponentScore = 50;
        public double WholeScore = 50;

        public QuoridorAction(Form1.NowAction PA, Point AP)
        {
            PlayerAction = PA;
            ActionPoint.X = AP.X;
            ActionPoint.Y = AP.Y;
        }
    }
    /// <summary>
    /// 步步为营AI类 
    /// </summary>
    public class QuoridorAI
    {
        public ChessBoard ThisChessBoard = new ChessBoard();
        public LookupRoadAlgorithm AstarEngine = new LookupRoadAlgorithm();
        public QuoridorRuleEngine QuoridorRule = new QuoridorRuleEngine();
        public List<QuoridorAction> ActionList = new List<QuoridorAction>();
        public Form1.EnumNowPlayer Player_Now = Form1.EnumNowPlayer.Player1;

        public class RunTime
        {
            public bool IfMeassureTime = false;
            public double Astar_s = 0;
            public long AstarNum = 0;
        }
        public static RunTime AIRunTime = new RunTime();
        /// <summary>
        /// 检测该挡板对该玩家有何影响
        /// </summary>
        /// <param name="WhichBoard">放置哪种挡板</param>
        /// <param name="Player">检测哪个玩家会被堵死</param>
        /// <param name="Location_row">玩家的位置行</param>
        /// <param name="Location_col">玩家的位置列</param>
        /// <returns>影响了该玩家多少步数</returns>
        public int CheckBoardEffect(Form1.NowAction WhichBoard, Form1.EnumNowPlayer Player, int Location_row, int Location_col)
        {
            if (WhichBoard == Form1.NowAction.Action_Move_Player1 || WhichBoard == Form1.NowAction.Action_Move_Player2)
                return 993;
            if (Player == Form1.EnumNowPlayer.Player1 && QuoridorRuleEngine.NumPlayer1Board <= 0)
                return 993;
            else if (Player == Form1.EnumNowPlayer.Player2 && QuoridorRuleEngine.NumPlayer2Board <= 0)
                return 993;

            //int dis_pre = 0;
            //if (Player == Form1.EnumNowPlayer.Player1)
            //    dis_pre = AstarRestart(Form1.EnumNowPlayer.Player1
            //        , ThisChessBoard.Player1Location.X, ThisChessBoard.Player1Location.Y);
            //else
            //    dis_pre = AstarRestart(Form1.EnumNowPlayer.Player2
            //        , ThisChessBoard.Player2Location.X, ThisChessBoard.Player2Location.Y);

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
            string Hint = QuoridorRule.Action(ref ThisChessBoard, Location_row, Location_col, WhichBoard);
            if (Hint != "OK")
            {
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

                return 995;
            }

            int disbuff = 0;
            List<LookupRoadAlgorithm.AstarList> InitAList = new List<LookupRoadAlgorithm.AstarList>();
            AstarEngine.Astar_Stop = false;
            AstarEngine.Min_DistanceLength = 0;
            if (Player == Form1.EnumNowPlayer.Player1)
            {
                //disbuff = LookupRoad_Greedy(Player
                //    , ThisChessBoard.Player1Location.X, ThisChessBoard.Player1Location.Y
                //    , Moved);
                LookupRoadAlgorithm.AstarList InitGrid = new LookupRoadAlgorithm.AstarList(6, 0, 6, ThisChessBoard.Player1Location.X, ThisChessBoard.Player1Location.Y);

                InitAList.Add(InitGrid);
                disbuff = AstarEngine.LookupRoad_Astar(ThisChessBoard,Form1.EnumNowPlayer.Player1
                    , InitGrid
                    , 1, new List<LookupRoadAlgorithm.AstarList>()
                    , InitAList);
            }
            else
            {
                //disbuff = LookupRoad_Greedy(Player
                //    , ThisChessBoard.Player2Location.X, ThisChessBoard.Player2Location.Y
                //    , Moved); 
                LookupRoadAlgorithm.AstarList InitGrid = new LookupRoadAlgorithm.AstarList(6, 0, 6, ThisChessBoard.Player2Location.X, ThisChessBoard.Player2Location.Y);

                InitAList.Add(InitGrid);
                disbuff = AstarEngine.LookupRoad_Astar(ThisChessBoard,Form1.EnumNowPlayer.Player2
                    , InitGrid
                    , 1, new List<LookupRoadAlgorithm.AstarList>()
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

            if (disbuff >= 50)
            {
                return 994;
            }
            else
            {
                return disbuff; //- dis_pre;
            }
        }

        /// <summary>
        /// 计算一个动作的评分
        /// </summary>
        /// <param name="Action_Once">本次动作</param>
        /// <param name="Player">做该动作的玩家</param>
        public void ActionEvaluation(QuoridorAction Action_Once, Form1.EnumNowPlayer Player)
        {
            if (Action_Once.PlayerAction == Form1.NowAction.Action_Move_Player1)
            {
                Action_Once.SelfScore = Convert.ToDouble(AstarEngine.AstarRestart(ThisChessBoard,Form1.EnumNowPlayer.Player1, Action_Once.ActionPoint.X, Action_Once.ActionPoint.Y));
            }
            else if (Action_Once.PlayerAction == Form1.NowAction.Action_Move_Player2)
            {
                Action_Once.SelfScore = Convert.ToDouble(AstarEngine.AstarRestart(ThisChessBoard,Form1.EnumNowPlayer.Player2, Action_Once.ActionPoint.X, Action_Once.ActionPoint.Y)); 
            }
            else if (Action_Once.PlayerAction == Form1.NowAction.Action_PlaceHorizontalBoard
                || Action_Once.PlayerAction == Form1.NowAction.Action_PlaceVerticalBoard)
            {
                Point PlayerLocation = new Point();
                Form1.EnumNowPlayer CheckPlayer = Form1.EnumNowPlayer.Player1;
                if(Player == Form1.EnumNowPlayer.Player1)
                {
                    PlayerLocation.X = ThisChessBoard.Player2Location.X;
                    PlayerLocation.Y = ThisChessBoard.Player2Location.Y;
                    CheckPlayer = Form1.EnumNowPlayer.Player2;
                }
                else
                {
                    PlayerLocation.X = ThisChessBoard.Player1Location.X;
                    PlayerLocation.Y = ThisChessBoard.Player1Location.Y;
                    CheckPlayer = Form1.EnumNowPlayer.Player1;

                }
                int distance_effect = CheckBoardEffect(Action_Once.PlayerAction
                                                        , CheckPlayer
                                                        , Action_Once.ActionPoint.X
                                                        , Action_Once.ActionPoint.Y);

                Action_Once.OpponentScore = Convert.ToDouble(distance_effect);

                distance_effect = CheckBoardEffect(Action_Once.PlayerAction
                                                        , Player
                                                        , Action_Once.ActionPoint.X
                                                        , Action_Once.ActionPoint.Y);

                Action_Once.SelfScore = Convert.ToDouble(distance_effect);
            }
        }
        /// <summary>
        /// 对整个动作列表的每个动作评分
        /// </summary>
        public void ActionListEvaluation(ChessBoard ThisChessBoard, ref List<QuoridorAction> ActionList)
        {
            int dis_player1 = 0, dis_player2 = 0;
            dis_player1 = AstarEngine.AstarRestart(ThisChessBoard,Form1.EnumNowPlayer.Player1
                        , ThisChessBoard.Player1Location.X, ThisChessBoard.Player1Location.Y);
            dis_player2 = AstarEngine.AstarRestart(ThisChessBoard,Form1.EnumNowPlayer.Player2
                        , ThisChessBoard.Player2Location.X, ThisChessBoard.Player2Location.Y);

            for (int i = ActionList.Count - 1; i >= 0 ; i--)
            {
                QuoridorAction Action = ActionList[i];
                ActionEvaluation(Action, Player_Now);

                if (Action.SelfScore >= 100 || Action.OpponentScore >= 100)
                {
                    ActionList.Remove(ActionList[i]);
                    continue;
                }

                # region 计算整体评分
                //移动
                if (Action.PlayerAction == Form1.NowAction.Action_Move_Player1
                    || Action.PlayerAction == Form1.NowAction.Action_Move_Player2)
                {
                    if (Action.PlayerAction == Form1.NowAction.Action_Move_Player1)
                        Action.WholeScore = Action.SelfScore - dis_player2;
                    else
                        Action.WholeScore = Action.SelfScore - dis_player1;

                    //检测四周是否有别人棋子
                    if( Action.ActionPoint.X >= 1 &&
                        ThisChessBoard.ChessBoardAll[Action.ActionPoint.X - 1,Action.ActionPoint.Y].GridStatus != Grid.GridInsideStatus.Empty)//上
                       Action.WholeScore++; 
                    else if(Action.ActionPoint.X <= 5 &&                       
                        ThisChessBoard.ChessBoardAll[Action.ActionPoint.X + 1,Action.ActionPoint.Y].GridStatus != Grid.GridInsideStatus.Empty)//下
                       Action.WholeScore++;
                    if (Action.ActionPoint.Y >= 1 &&
                        ThisChessBoard.ChessBoardAll[Action.ActionPoint.X, Action.ActionPoint.Y - 1].GridStatus != Grid.GridInsideStatus.Empty)//左
                        Action.WholeScore++;
                    else if (Action.ActionPoint.Y <= 5 &&
                        ThisChessBoard.ChessBoardAll[Action.ActionPoint.X, Action.ActionPoint.Y + 1].GridStatus != Grid.GridInsideStatus.Empty)//右
                        Action.WholeScore++; 
                    
                    //检测是否能跳过对方
                    if(ThisChessBoard.ChessBoardAll[Action.ActionPoint.X,Action.ActionPoint.Y].GridStatus != Grid.GridInsideStatus.Empty)
                        Action.WholeScore--;

                    //检测是前走还是后走
                    if (Action.PlayerAction == Form1.NowAction.Action_Move_Player1)
                    {
                        if (Action.ActionPoint.X < ThisChessBoard.Player1Location.X)
                            Action.WholeScore -= 0.5;
                    }
                    else
                    {
                        if (Action.ActionPoint.X > ThisChessBoard.Player1Location.X)
                            Action.WholeScore -= 0.5; 
                    }
                    Action.WholeScore += 1;
                }
                //放挡板
                if (Action.PlayerAction == Form1.NowAction.Action_PlaceHorizontalBoard
                || Action.PlayerAction == Form1.NowAction.Action_PlaceVerticalBoard)
                {
                    double disbuff_1 = 0, disbuff_2 = 0, disbuff_3 = 0, disbuff_4 = 0;
                    if (Player_Now == Form1.EnumNowPlayer.Player1)
                    { 
                        disbuff_1 = dis_player2 - 1 - Action.OpponentScore;
                        disbuff_2 = dis_player1 - 1 - Action.SelfScore;
                        if (Action.PlayerAction == Form1.NowAction.Action_PlaceHorizontalBoard)
                        {
                            disbuff_3 = Math.Abs(Action.ActionPoint.X - ThisChessBoard.Player1Location.X);

                            if (Action.ActionPoint.X > ThisChessBoard.Player1Location.X)//下方
                            {
                                disbuff_3 = 6 - ThisChessBoard.Player1Location.X - disbuff_3;
                            }

                            disbuff_4 = Math.Abs(Action.ActionPoint.Y - ThisChessBoard.Player1Location.Y);
                        }
                        else
                        {
                            disbuff_3 = Math.Abs(Action.ActionPoint.Y - ThisChessBoard.Player1Location.Y);
                            disbuff_4 = Math.Abs(Action.ActionPoint.X - ThisChessBoard.Player1Location.X);
                        }


                        Action.WholeScore = disbuff_1 - disbuff_2 + disbuff_3 + disbuff_4; 
                    }
                    else
                    {
                        disbuff_1 = dis_player1 - 1 - Action.OpponentScore;
                        disbuff_2 = dis_player2 - 1 - Action.SelfScore;
                        if (Action.PlayerAction == Form1.NowAction.Action_PlaceHorizontalBoard)
                        {
                            disbuff_3 = Math.Abs(Action.ActionPoint.X - ThisChessBoard.Player2Location.X);

                            if (Action.ActionPoint.X <= ThisChessBoard.Player2Location.X)//上方
                            {
                                disbuff_3 = ThisChessBoard.Player2Location.X - disbuff_3;
                            }

                            disbuff_4 = Math.Abs(Action.ActionPoint.Y - ThisChessBoard.Player2Location.Y);
                        }
                        else
                        {
                            disbuff_3 = Math.Abs(Action.ActionPoint.Y - ThisChessBoard.Player2Location.Y);
                            disbuff_4 = Math.Abs(Action.ActionPoint.X - ThisChessBoard.Player2Location.X);
                        }

                        Action.WholeScore = disbuff_1 - disbuff_2 + disbuff_3 + disbuff_4;
                    }
                }

                #endregion


            }
        }
        /// <summary>
        /// 创建可选动作列表（目前只是用挡住对手的最短路径上的挡板动作为主）
        /// </summary>
        public List<QuoridorAction> CreateActionList(ChessBoard ThisChessBoard)
        {
            List<QuoridorAction> ActionListBuff = new List<QuoridorAction>();
            ActionList = new List<QuoridorAction>();

            Point PlayerLocation = new Point();
            if(Player_Now == Form1.EnumNowPlayer.Player1)
            {
                PlayerLocation.X = ThisChessBoard.Player1Location.X;
                PlayerLocation.Y = ThisChessBoard.Player1Location.Y;
            }
            else
            {
                PlayerLocation.X = ThisChessBoard.Player2Location.X;
                PlayerLocation.Y = ThisChessBoard.Player2Location.Y;
            }

            #region 创建移动Action
            Form1.NowAction MoveAction = Form1.NowAction.Action_Move_Player1;
            if(Player_Now == Form1.EnumNowPlayer.Player2)
                MoveAction = Form1.NowAction.Action_Move_Player2;

            int row = PlayerLocation.X, col = PlayerLocation.Y;

            //if (row >= 1 && !(ThisChessBoard.ChessBoardAll[row, col].IfUpBoard))//上扫
            //    ActionList.Add(new QuoridorAction(MoveAction, new Point(row - 1, col)));
            //if (row <= 5 && !(ThisChessBoard.ChessBoardAll[row + 1, col].IfUpBoard))//下扫
            //    ActionList.Add(new QuoridorAction(MoveAction, new Point(row + 1, col)));
            //if (col <= 5 && !(ThisChessBoard.ChessBoardAll[row, col + 1].IfLeftBoard))//右扫
            //    ActionList.Add(new QuoridorAction(MoveAction, new Point(row, col + 1)));
            //if (col >= 1 && !(ThisChessBoard.ChessBoardAll[row, col].IfLeftBoard))//左扫
            //    ActionList.Add(new QuoridorAction(MoveAction, new Point(row, col - 1)));

            ///检测附近的12个可能点
            if (row >= 2 && QuoridorRule.CheckMove_NoChange(ThisChessBoard, row - 2, col, MoveAction) == "OK")
                ActionListBuff.Add(new QuoridorAction(MoveAction, new Point(row - 2, col)));

            if (row >= 1 && col >= 1 && QuoridorRule.CheckMove_NoChange(ThisChessBoard, row - 1, col - 1, MoveAction) == "OK")
                ActionListBuff.Add(new QuoridorAction(MoveAction, new Point(row - 1, col - 1)));
            if (row >= 1 && QuoridorRule.CheckMove_NoChange(ThisChessBoard, row - 1, col, MoveAction) == "OK")
                ActionListBuff.Add(new QuoridorAction(MoveAction, new Point(row - 1, col)));
            if (row >= 1 && col <= 5 && QuoridorRule.CheckMove_NoChange(ThisChessBoard, row - 1, col + 1, MoveAction) == "OK")
                ActionListBuff.Add(new QuoridorAction(MoveAction, new Point(row - 1, col + 1)));

            if (col >= 2 && QuoridorRule.CheckMove_NoChange(ThisChessBoard, row, col - 2, MoveAction) == "OK")
                ActionListBuff.Add(new QuoridorAction(MoveAction, new Point(row, col - 2)));
            if (col >= 1 && QuoridorRule.CheckMove_NoChange(ThisChessBoard, row, col - 1, MoveAction) == "OK")
                ActionListBuff.Add(new QuoridorAction(MoveAction, new Point(row, col - 1)));
            if (col <= 5 && QuoridorRule.CheckMove_NoChange(ThisChessBoard, row, col + 1, MoveAction) == "OK")
                ActionListBuff.Add(new QuoridorAction(MoveAction, new Point(row, col + 1)));
            if (col <= 4 && QuoridorRule.CheckMove_NoChange(ThisChessBoard, row, col + 2, MoveAction) == "OK")
                ActionListBuff.Add(new QuoridorAction(MoveAction, new Point(row, col + 2)));

            if (row <= 5 && col >= 1 && QuoridorRule.CheckMove_NoChange(ThisChessBoard, row + 1, col - 1, MoveAction) == "OK")
                ActionListBuff.Add(new QuoridorAction(MoveAction, new Point(row + 1, col - 1)));
            if (row <= 5 && QuoridorRule.CheckMove_NoChange(ThisChessBoard, row + 1, col, MoveAction) == "OK")
                ActionListBuff.Add(new QuoridorAction(MoveAction, new Point(row + 1, col)));
            if (row <= 5 && col <= 5 && QuoridorRule.CheckMove_NoChange(ThisChessBoard, row + 1, col + 1, MoveAction) == "OK")
                ActionListBuff.Add(new QuoridorAction(MoveAction, new Point(row + 1, col)));

            if (row <= 4 && QuoridorRule.CheckMove_NoChange(ThisChessBoard, row + 2, col, MoveAction) == "OK")
                ActionListBuff.Add(new QuoridorAction(MoveAction, new Point(row + 2, col)));

            #endregion

            #region 创建放置挡板Action
            List<Point> MinRoad = new List<Point>();
            if(Player_Now == Form1.EnumNowPlayer.Player1)
            {
                AstarEngine.AstarRestart(ThisChessBoard, Form1.EnumNowPlayer.Player2, ThisChessBoard.Player2Location.X, ThisChessBoard.Player2Location.Y);
                MinRoad = AstarEngine.Player2MinRoad;
            }
            else
            {
                AstarEngine.AstarRestart(ThisChessBoard, Form1.EnumNowPlayer.Player1, ThisChessBoard.Player1Location.X, ThisChessBoard.Player1Location.Y);

                MinRoad = AstarEngine.Player1MinRoad;
            }

            for (int i = 0; i < MinRoad.Count - 1; i++)
            {
                int row_1 = MinRoad[i].X, col_1 = MinRoad[i].Y;
                int row_2 = MinRoad[i + 1].X, col_2 = MinRoad[i + 1].Y;


                /*判断下一格在这一格的哪个方位*/
                if (col_1 == col_2 && row_1 != row_2)//上或下
                {
                    if (row_1 < row_2)//下
                    {
                        if (col_2 <= 5 && !ThisChessBoard.ChessBoardAll[row_2, col_2 + 1].IfUpBoard)
                            ActionListBuff.Add(new QuoridorAction(
                                                            Form1.NowAction.Action_PlaceHorizontalBoard
                                                            ,new Point(row_2, col_2)));
                        if(col_2 >= 1 && !ThisChessBoard.ChessBoardAll[row_2, col_2 - 1].IfUpBoard)
                            ActionListBuff.Add(new QuoridorAction(
                                                            Form1.NowAction.Action_PlaceHorizontalBoard
                                                            , new Point(row_2, col_2 - 1)));
                    }
                    else//上
                    {
                        if (col_1 <= 5 && !ThisChessBoard.ChessBoardAll[row_1, col_1 + 1].IfUpBoard)
                            ActionListBuff.Add(new QuoridorAction(
                                                            Form1.NowAction.Action_PlaceHorizontalBoard
                                                            , new Point(row_1, col_1)));
                        if(col_1 >= 1 && !ThisChessBoard.ChessBoardAll[row_1,col_1 - 1].IfUpBoard)
                            ActionListBuff.Add(new QuoridorAction(
                                                            Form1.NowAction.Action_PlaceHorizontalBoard
                                                            , new Point(row_1, col_1 - 1)));

                    }
                }
                else if (row_1 == row_2 && col_1 != col_2)//左或右
                {
                    if (col_1 < col_2)//右
                    {
                        if(row_2 <= 5 && !ThisChessBoard.ChessBoardAll[row_2 + 1,col_2].IfLeftBoard)
                            ActionListBuff.Add(new QuoridorAction(
                                                            Form1.NowAction.Action_PlaceVerticalBoard
                                                            , new Point(row_2, col_2)));
                        if(row_2 >= 1 && !ThisChessBoard.ChessBoardAll[row_2 - 1,col_2].IfLeftBoard)
                            ActionListBuff.Add(new QuoridorAction(
                                                            Form1.NowAction.Action_PlaceVerticalBoard
                                                            , new Point(row_2 - 1, col_2)));
                    }
                    else//上
                    {
                        if (row_1 <= 5 && !ThisChessBoard.ChessBoardAll[row_1 + 1, col_1].IfLeftBoard)
                            ActionListBuff.Add(new QuoridorAction(
                                                            Form1.NowAction.Action_PlaceVerticalBoard
                                                            , new Point(row_1, col_1)));
                        if (row_1 >= 1 && !ThisChessBoard.ChessBoardAll[row_1 - 1, col_1].IfLeftBoard)
                            ActionListBuff.Add(new QuoridorAction(
                                                            Form1.NowAction.Action_PlaceVerticalBoard
                                                            , new Point(row_1 - 1, col_1)));
                    } 
                }
            }
            #endregion
            #region 校验生成的ActionList是否合法

            foreach (QuoridorAction QA in ActionListBuff)
            {
                if (QuoridorRule.CheckBoard(ThisChessBoard, QA.PlayerAction, Player_Now,
                    QA.ActionPoint.X, QA.ActionPoint.Y) == "OK")
                {
                    ActionList.Add(QA);
                }
            }

            #endregion
            return ActionList;
        }
        /// <summary>
        /// 测试该动作列表的评分
        /// </summary>
        public void TestEvaluation()
        {
            List<QuoridorAction> QABuff = ActionList;
            QABuff = CreateActionList(ThisChessBoard);
            ActionListEvaluation(ThisChessBoard,ref QABuff);

            Console.WriteLine("/**********显示"+ (Player_Now).ToString()+"动作评分***********/");
            foreach (QuoridorAction AL in ActionList)
            {
                Console.Write("在(" + AL.ActionPoint.X.ToString() + "," + AL.ActionPoint.Y.ToString() + ")处");
                string actionstr = "";
                switch (AL.PlayerAction)
                {
                    case Form1.NowAction.Action_PlaceVerticalBoard:
                        actionstr = "放置竖挡板";break;
                    case Form1.NowAction.Action_PlaceHorizontalBoard:
                        actionstr = "放置横挡板"; break;
                    case Form1.NowAction.Action_Move_Player1:
                        actionstr = "移动玩家1"; break;
                    case Form1.NowAction.Action_Move_Player2:
                        actionstr = "移动玩家2"; break;
                    default:
                        actionstr = "异常"; break;
                }
                Console.WriteLine(actionstr);
                Console.Write("该动作评分为：");
                Console.WriteLine(AL.OpponentScore.ToString() + "," + AL.SelfScore.ToString() + "," + AL.WholeScore.ToString());

            }
            Console.WriteLine("/****************************************/");
        }
        /// <summary>
        /// AI落子，使用贪婪算法
        /// </summary>
        /// <param name="AIPlayer"></param>
        /// <returns></returns>
        public QuoridorAction AIAction_Greedy(Form1.EnumNowPlayer AIPlayer)
        {
            List<QuoridorAction> QABuff = ActionList;

            ///暂存一些量以便恢复
            Form1.EnumNowPlayer PlayerSave = Player_Now;

            Player_Now = AIPlayer;

            QABuff = CreateActionList(ThisChessBoard);
            ActionListEvaluation(ThisChessBoard, ref QABuff);

            #region 贪婪思想，找最好的一步
            QuoridorAction BestAction = ActionList.First();
            double MinScore = 99;
            foreach (QuoridorAction AL in ActionList)
            {
                if (MinScore > AL.WholeScore)
                {
                    BestAction = AL;
                    MinScore = AL.WholeScore;
                }
            }
            #endregion

            Player_Now = PlayerSave;
            return BestAction;
        }
        public enum SearchLevel
        {
            MaxLevel,
            MinLevel
        }
        public Grid[,] ChessBoardBuff = new Grid[7, 7];
        public Form1.EnumNowPlayer PlayerBuff = Form1.EnumNowPlayer.Player2;
        public void AlphaBetaPruningInit(Grid[,] ChessBoard_Init, Form1.EnumNowPlayer ToAction_Player)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    ChessBoardBuff[i, j] = new Grid();
                }
            }
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    ChessBoardBuff[i, j].GridStatus = ChessBoard_Init[i, j].GridStatus;
                    ChessBoardBuff[i, j].IfLeftBoard = ChessBoard_Init[i, j].IfLeftBoard;
                    ChessBoardBuff[i, j].IfUpBoard = ChessBoard_Init[i, j].IfUpBoard;
                }
            }
            PlayerBuff = ToAction_Player;
        }
        public Form1.EnumNowPlayer ReversePlayer(Form1.EnumNowPlayer NowPlayer)
        {
            if (NowPlayer == Form1.EnumNowPlayer.Player1)
                return Form1.EnumNowPlayer.Player2;
            else
                return Form1.EnumNowPlayer.Player1;
        }
        public QuoridorAction AlphaBetaPruning(ChessBoard ChessBoardStatus, Form1.EnumNowPlayer NowPlayer, int depth, double alpha, double beta, ref double alphabetabuff)
        {
            //#region 输出当前棋盘状态
            //ThisChessBoard.DrawNowChessBoard(ref Form1.Gr, Form1.ChessWhitePB, Form1.ChessBlackPB);
            //Form1.ChessBoardPB.Refresh();
            //#endregion

            if (QuoridorRule.CheckResult(ChessBoardStatus) != "No success")
            {
                alphabetabuff = 9999;
                return new QuoridorAction(Form1.NowAction.Action_Move_Player1,new Point(0,0));
            }
            if (depth == 0)
            {
                ///暂存一些量以便恢复
                Form1.EnumNowPlayer PlayerSave = Player_Now;
                Player_Now = NowPlayer;
                List<QuoridorAction> QABuff = ActionList;

                QABuff = CreateActionList(ThisChessBoard);
                ActionListEvaluation(ThisChessBoard, ref QABuff);

                if (ActionList.Count <= 0)
                {
                    Player_Now = PlayerSave;
                    alphabetabuff = 999999;
                    return new QuoridorAction(Form1.NowAction.Action_Wait,new Point(-1,-1));
                }
                #region 贪婪思想，找最好的一步
                QuoridorAction BestAction = QABuff.First();
                double MaxScore = -100;
                foreach (QuoridorAction AL in QABuff)
                {
                    if (MaxScore < 100 - AL.WholeScore)
                    {
                        BestAction = AL;
                        MaxScore = 100 - AL.WholeScore;
                    }
                }
                #endregion
                Player_Now = PlayerSave;
                alphabetabuff = MaxScore;

                return BestAction;
            }
            if (NowPlayer == PlayerBuff)
            {
                ///暂存一些量以便恢复
                Form1.EnumNowPlayer PlayerSave = Player_Now;
                Player_Now = NowPlayer;

                List<QuoridorAction> QABuff = ActionList;

                QABuff = CreateActionList(ThisChessBoard);

                QuoridorAction BestAction = QABuff.First();

                foreach (QuoridorAction Action in QABuff)
                {
                    #region 保存棋盘状态
                    ChessBoard ChessBoardBuff = new ChessBoard();
                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            ChessBoardBuff.ChessBoardAll[i, j].IfLeftBoard = ChessBoardStatus.ChessBoardAll[i, j].IfLeftBoard;
                            ChessBoardBuff.ChessBoardAll[i, j].IfUpBoard = ChessBoardStatus.ChessBoardAll[i, j].IfUpBoard;
                            ChessBoardBuff.ChessBoardAll[i, j].GridStatus = ChessBoardStatus.ChessBoardAll[i, j].GridStatus;
                        }
                    }
                    ChessBoardBuff.Player1Location.X = ChessBoardStatus.Player1Location.X;
                    ChessBoardBuff.Player1Location.Y = ChessBoardStatus.Player1Location.Y;
                    ChessBoardBuff.Player2Location.X = ChessBoardStatus.Player2Location.X;
                    ChessBoardBuff.Player2Location.Y = ChessBoardStatus.Player2Location.Y;
                    #endregion
                    #region 模拟落子
                    string Hint = QuoridorRule.Action(ref ChessBoardStatus, Action.ActionPoint.X, Action.ActionPoint.Y, Action.PlayerAction);
                    try
                    {
                        if (Hint != "OK")
                        {
                            Exception e = new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    #endregion
                    //#region 输出当前棋盘状态
                    //ThisChessBoard.DrawNowChessBoard(ref Form1.Gr, Form1.ChessWhitePB, Form1.ChessBlackPB);
                    //Form1.ChessBoardPB.Refresh();
                    //#endregion

                    double alphabuff = 0;

                    QuoridorAction PartAction = Action;
                    PartAction = AlphaBetaPruning(ChessBoardStatus, ReversePlayer(NowPlayer), depth - 1, alpha, beta, ref alphabuff);
                    #region 恢复棋盘状态
                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            ChessBoardStatus.ChessBoardAll[i, j].IfLeftBoard = ChessBoardBuff.ChessBoardAll[i, j].IfLeftBoard;
                            ChessBoardStatus.ChessBoardAll[i, j].IfUpBoard = ChessBoardBuff.ChessBoardAll[i, j].IfUpBoard;
                            ChessBoardStatus.ChessBoardAll[i, j].GridStatus = ChessBoardBuff.ChessBoardAll[i, j].GridStatus;
                        }
                    }
                    ChessBoardStatus.Player1Location.X = ChessBoardBuff.Player1Location.X;
                    ChessBoardStatus.Player1Location.Y = ChessBoardBuff.Player1Location.Y;
                    ChessBoardStatus.Player2Location.X = ChessBoardBuff.Player2Location.X;
                    ChessBoardStatus.Player2Location.Y = ChessBoardBuff.Player2Location.Y;
                    #endregion

                    if (alphabuff > alpha)
                    { 
                        alpha = alphabuff;
                        BestAction = Action;
                    }

                    if (beta <= alpha)
                        break;

                }

                Player_Now = PlayerSave;

                return BestAction;
            }
            else
            {
                ///暂存一些量以便恢复
                Form1.EnumNowPlayer PlayerSave = Player_Now;
                Player_Now = NowPlayer;
                List<QuoridorAction> QABuff = ActionList;

                QABuff = CreateActionList(ThisChessBoard);

                QuoridorAction BestAction = QABuff.First();

                foreach (QuoridorAction Action in QABuff)
                {
                    #region 保存棋盘状态
                    ChessBoard ChessBoardBuff = new ChessBoard();
                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            ChessBoardBuff.ChessBoardAll[i, j].IfLeftBoard = ChessBoardStatus.ChessBoardAll[i, j].IfLeftBoard;
                            ChessBoardBuff.ChessBoardAll[i, j].IfUpBoard = ChessBoardStatus.ChessBoardAll[i, j].IfUpBoard;
                            ChessBoardBuff.ChessBoardAll[i, j].GridStatus = ChessBoardStatus.ChessBoardAll[i, j].GridStatus;
                        }
                    }
                    ChessBoardBuff.Player1Location.X = ChessBoardStatus.Player1Location.X;
                    ChessBoardBuff.Player1Location.Y = ChessBoardStatus.Player1Location.Y;
                    ChessBoardBuff.Player2Location.X = ChessBoardStatus.Player2Location.X;
                    ChessBoardBuff.Player2Location.Y = ChessBoardStatus.Player2Location.Y;
                    #endregion
                    #region 模拟落子
                    string Hint = QuoridorRule.Action(ref ChessBoardStatus, Action.ActionPoint.X, Action.ActionPoint.Y, Action.PlayerAction);
                    try
                    {
                        if (Hint != "OK")
                        {
                            Exception e = new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    #endregion
                    //#region 输出当前棋盘状态
                    //ThisChessBoard.DrawNowChessBoard(ref Form1.Gr, Form1.ChessWhitePB, Form1.ChessBlackPB);
                    //Form1.ChessBoardPB.Refresh();
                    //#endregion

                    double betabuff = 0;

                    QuoridorAction PartAction = Action;

                    PartAction = AlphaBetaPruning(ChessBoardStatus, ReversePlayer(NowPlayer), depth - 1, alpha, beta, ref betabuff);
                    #region 恢复棋盘状态
                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            ChessBoardStatus.ChessBoardAll[i, j].IfLeftBoard = ChessBoardBuff.ChessBoardAll[i, j].IfLeftBoard;
                            ChessBoardStatus.ChessBoardAll[i, j].IfUpBoard = ChessBoardBuff.ChessBoardAll[i, j].IfUpBoard;
                            ChessBoardStatus.ChessBoardAll[i, j].GridStatus = ChessBoardBuff.ChessBoardAll[i, j].GridStatus;
                        }
                    }
                    ChessBoardStatus.Player1Location.X = ChessBoardBuff.Player1Location.X;
                    ChessBoardStatus.Player1Location.Y = ChessBoardBuff.Player1Location.Y;
                    ChessBoardStatus.Player2Location.X = ChessBoardBuff.Player2Location.X;
                    ChessBoardStatus.Player2Location.Y = ChessBoardBuff.Player2Location.Y;
                    #endregion
                    if (betabuff < beta)
                    { 
                        beta = betabuff;
                        BestAction = Action;
                    }

                    if (beta <= alpha)
                        break;

                }
                Player_Now = PlayerSave;

                return BestAction; 
            }
        }
    }
    /// <summary>
    /// 博弈树节点类
    /// </summary>
    public class GameTreeNode
    {
        public Form1.NowAction NodeAction;///当前节点的动作
        public Form1.EnumNowPlayer NodePlayer;///当前节点的动作的执行玩家
        public Point ActionLocation = new Point(-1, -1);///当前节点动作的执行位置
        public List<GameTreeNode> SonNode = new List<GameTreeNode>();///子节点列表

        public int depth = 0;///该节点深度

        public double alpha = -10000;///该节点的alpha值
        public double beta = 10000;///该节点的beta值
        public double score = 10000;///该节点的评分值

        public static QuoridorAI NowQuoridor = new QuoridorAI();
        public static long NodeNum = 0;
        public GameTreeNode() { }
        /// <summary>
        /// 构造函数,用来设定该博弈树节点的信息
        /// </summary>
        public GameTreeNode(Form1.NowAction Action_set, Point ActionLocation_set, Form1.EnumNowPlayer Player_set, int depth_set, double alpha_set, double beta_set, double score_set)
        {
            NodeAction = Action_set;
            NodePlayer = Player_set;
            depth = depth_set;
            alpha = alpha_set;
            beta = beta_set;
            score = score_set;
            ActionLocation = ActionLocation_set;
        }
        public GameTreeNode(GameTreeNode ThisNode)
        {
 
        }
        /// <summary>
        /// 给该节点添加新的子节点
        /// </summary>
        /// <param name="NewNode">待添加的子节点</param>
        public void CreateNewSon(GameTreeNode FatherNode, GameTreeNode NewNode)
        {
            FatherNode.SonNode.Add(NewNode);
        }
        public static int DepthMax = 1000;///博弈树最大深度
        /// <summary>
        /// 以极大极小搜索框架生成博弈树
        /// </summary>
        /// <param name="ThisChessBoard">当前棋盘状态</param>
        /// <param name="ThisNode">当前博弈树节点</param>
        public void ExpandNode_MinMax(ChessBoard ThisChessBoard, GameTreeNode ThisNode)
        {
            ///暂存一些量以便恢复
            Form1.EnumNowPlayer PlayerSave = NowQuoridor.ReversePlayer(ThisNode.NodePlayer);
            NowQuoridor.Player_Now = PlayerSave;

            List<QuoridorAction> QABuff = NowQuoridor.ActionList;

            QABuff = NowQuoridor.CreateActionList(ThisChessBoard);

            if (ThisNode.depth > DepthMax)
            {
                NowQuoridor.ActionListEvaluation(ThisChessBoard, ref QABuff);

                if (NowQuoridor.ActionList.Count <= 0)
                {
                    NowQuoridor.Player_Now = PlayerSave;
                    double score = 999999;
                    ThisNode.CreateNewSon(ThisNode, new GameTreeNode(Form1.NowAction.Action_Wait, new Point(-1, -1)
                        , PlayerSave, ThisNode.depth + 1, score, score, score));
                    ThisNode.score = score;
                    return;
                }
                #region 贪婪思想，找最好的一步
                QuoridorAction BestAction = new QuoridorAction(Form1.NowAction.Action_Wait, new Point(-1, -1));
                BestAction = QABuff.First();
                double MaxScore = -100;
                foreach (QuoridorAction AL in QABuff)
                {
                    if (MaxScore < 100 - AL.WholeScore)
                    {
                        BestAction = AL;
                        MaxScore = 100 - AL.WholeScore;
                    }
                }
                #endregion
                NowQuoridor.Player_Now = PlayerSave;
                double alphabetabuff = MaxScore;

                ThisNode.CreateNewSon(ThisNode,new GameTreeNode(BestAction.PlayerAction, BestAction.ActionPoint
                , PlayerSave, ThisNode.depth + 1, -alphabetabuff, alphabetabuff, alphabetabuff));
                ThisNode.score = alphabetabuff;
                return;
            }

            foreach (QuoridorAction QA in QABuff)
            {
                #region 保存棋盘状态
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
                #endregion
                #region 模拟落子
                string Hint = NowQuoridor.QuoridorRule.Action(ref ThisChessBoard, QA.ActionPoint.X, QA.ActionPoint.Y, QA.PlayerAction);
                try
                {
                    if (Hint != "OK")
                    {
                        Exception e = new Exception();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                #endregion

                CreateNewSon(ThisNode, new GameTreeNode(QA.PlayerAction, QA.ActionPoint
                    , NowQuoridor.ReversePlayer(ThisNode.NodePlayer), ThisNode.depth + 1, ThisNode.alpha, ThisNode.beta, ThisNode.beta));

                ExpandNode_MinMax(ThisChessBoard, ThisNode.SonNode.Last());
                #region 恢复棋盘状态
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
                #endregion
            }
            if (ThisNode.NodePlayer == NowQuoridor.PlayerBuff)//MIN层
            {
                double minvalue = 99999;
                foreach (GameTreeNode Son in ThisNode.SonNode)
                {
                    if (Son.score < minvalue)
                    {
                        minvalue = Son.score;
                        ThisNode.score = minvalue;
                    }
                }
            }
            else //MAX层
            {
                double maxvalue = -10000;
                foreach (GameTreeNode Son in ThisNode.SonNode)
                {
                    if (Son.score > maxvalue)
                    {
                        maxvalue = Son.score;
                        ThisNode.score = maxvalue;
                        if (depth == 0)//根节点层
                        {
                            ThisNode.ActionLocation = Son.ActionLocation;
                            ThisNode.NodeAction = Son.NodeAction;
                            ThisNode.NodePlayer = Son.NodePlayer;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 以Alpha-Beta剪枝框架生成博弈树
        /// </summary>
        /// <param name="ThisChessBoard">当前棋盘状态</param>
        /// <param name="ThisNode">当前博弈树节点</param>
        public void ExpandNode_ABPruning(ChessBoard ThisChessBoard, GameTreeNode ThisNode)
        {
            ///暂存一些量以便恢复
            
            Form1.EnumNowPlayer PlayerSave = NowQuoridor.ReversePlayer(ThisNode.NodePlayer);
            NowQuoridor.Player_Now = PlayerSave;

            List<QuoridorAction> QABuff = NowQuoridor.ActionList;

            QABuff = NowQuoridor.CreateActionList(ThisChessBoard);

            if (ThisNode.depth > DepthMax)
            {
                NowQuoridor.ActionListEvaluation(ThisChessBoard, ref QABuff);

                if (NowQuoridor.ActionList.Count <= 0)
                {
                    NowQuoridor.Player_Now = PlayerSave;
                    double score = 999999;
                    ThisNode.CreateNewSon(ThisNode, new GameTreeNode(Form1.NowAction.Action_Wait, new Point(-1, -1)
                        , PlayerSave, ThisNode.depth + 1, score, score, score));
                    ThisNode.score = score;
                    return;
                }
                #region 贪婪思想，找最好的一步
                QuoridorAction BestAction = new QuoridorAction(Form1.NowAction.Action_Wait, new Point(-1, -1));
                BestAction = QABuff.First();
                double MaxScore = -100;
                foreach (QuoridorAction AL in QABuff)
                {
                    if (MaxScore < 100 - AL.WholeScore)
                    {
                        BestAction = AL;
                        MaxScore = 100 - AL.WholeScore;
                    }
                }
                #endregion
                NowQuoridor.Player_Now = PlayerSave;
                double alphabetabuff = MaxScore;

                ThisNode.CreateNewSon(ThisNode, new GameTreeNode(BestAction.PlayerAction, BestAction.ActionPoint
                , PlayerSave, ThisNode.depth + 1, -alphabetabuff, alphabetabuff, alphabetabuff));
                ThisNode.score = alphabetabuff;
                return;
            }

            foreach (QuoridorAction QA in QABuff)
            {
                #region 保存棋盘状态
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
                #endregion
                #region 模拟落子
                string Hint = NowQuoridor.QuoridorRule.Action(ref ThisChessBoard, QA.ActionPoint.X, QA.ActionPoint.Y, QA.PlayerAction);
                try
                {
                    if (Hint != "OK")
                    {
                        Exception e = new Exception();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                #endregion

                CreateNewSon(ThisNode, new GameTreeNode(QA.PlayerAction, QA.ActionPoint
                    , NowQuoridor.ReversePlayer(ThisNode.NodePlayer), ThisNode.depth + 1, ThisNode.alpha, ThisNode.beta, ThisNode.score));

                ExpandNode_ABPruning(ThisChessBoard, ThisNode.SonNode.Last());

                if (ThisNode.NodePlayer == NowQuoridor.PlayerBuff)//MIN层
                {
                    if (ThisNode.SonNode.Last().score < ThisNode.beta)
                    {
                        ThisNode.beta = ThisNode.SonNode.Last().score;
                        ThisNode.score = ThisNode.SonNode.Last().score;
                    }

                    if (ThisNode.beta <= ThisNode.alpha)
                        break;
                }
                else
                {
                    if (ThisNode.SonNode.Last().score > ThisNode.alpha)
                    {
                        ThisNode.alpha = ThisNode.SonNode.Last().score;
                        ThisNode.score = ThisNode.SonNode.Last().score;
                        if (depth == 0)//根节点层
                        {
                            ThisNode.ActionLocation = ThisNode.SonNode.Last().ActionLocation;
                            ThisNode.NodeAction = ThisNode.SonNode.Last().NodeAction;
                            //ThisNode.NodePlayer = ThisNode.SonNode.Last().NodePlayer;
                        }
                    }

                    if (ThisNode.beta <= ThisNode.alpha)
                    { 
                        break; 
                    }
                }
                #region 恢复棋盘状态
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
                #endregion
            }
        }

        public static void CreateGameTree(GameTreeNode RootNode, ChessBoard ChessBoard_Init, int DepthMax_Set, bool IfShowDebugLog = false)
        {
            try
            {
                Exception E = new Exception("最大深度设定错误！请设置为偶数！");
                //if (DepthMax_Set % 2 != 1)
                //{
                //    throw E; 
                //}
            }
            catch (Exception e)
            {
                throw;
            }
            DepthMax = DepthMax_Set;

            //RootNode.ExpandNode_MinMax(ChessBoard_Init, RootNode);//9W数量级节点数
            RootNode.ExpandNode_ABPruning(ChessBoard_Init, RootNode);//500数量级节点数

            if(IfShowDebugLog)
                PrintGameTree(RootNode);
        }

        public static void CalGameTreeNodeNum(GameTreeNode NowNode)
        {
            if (NowNode.SonNode.Count <= 0)
            {
                return;
            }
            foreach (GameTreeNode Son in NowNode.SonNode)
            {
                NodeNum++;
                CalGameTreeNodeNum(Son);
            }
        }
        /// <summary>
        /// 控制台输出博弈树调试日志（向下遍历）
        /// </summary>
        /// <param name="NowNode"></param>
        public static void PrintGameTree(GameTreeNode NowNode)
        {
            if (NowNode.SonNode.Count <= 0)
            {
                //Console.Write(("第" + NowNode.depth.ToString() + "层 "));
                //Console.Write(NowNode.NodePlayer.ToString());
                //Console.Write((" a = " + NowNode.alpha.ToString()));
                //Console.Write((",b = " + NowNode.beta.ToString()));
                //Console.Write("动作：");
                //Console.Write(NowNode.NodeAction.ToString());
                //Console.Write(("位置：" + NowNode.ActionLocation.ToString()));

                //Console.WriteLine();
                return;
            }
            foreach (GameTreeNode Son in NowNode.SonNode)
            {
                PrintGameTree(Son);
            }
            if (NowNode.depth >= 0 && NowNode.depth <= DepthMax + 1)
            {
                Console.Write(("第" + NowNode.depth.ToString() + "层 "));
                Console.Write(NowNode.NodePlayer.ToString());
                Console.Write((" a = " + NowNode.alpha.ToString()));
                Console.Write((",b = " + NowNode.beta.ToString()));
                Console.Write((",s = " + NowNode.score.ToString()));
                Console.Write("动作：");
                Console.Write(NowNode.NodeAction.ToString());
                Console.Write(("位置：" + NowNode.ActionLocation.ToString()));

                Console.WriteLine();
            }
        }

    }
}
