﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using System.Runtime.InteropServices;
using Quoridor;

namespace Quoridor_With_C
{
    public partial class Form1 : Skin_Metro
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole(); 

        Graphics Gr;
        Bitmap bmp = new Bitmap(1000, 900);
        public enum GameModeStatus
        {
            SinglePlay,
            DoublePlay,
            Queen8
        }
        public GameModeStatus GameMode;
        public enum EnumNowPlayer
        {
            Player1,
            Player2
        }
        EnumNowPlayer NowPlayer = EnumNowPlayer.Player1;
        public enum NowAction
        {
            Action_PlaceVerticalBoard,
            Action_PlaceHorizontalBoard,
            Action_Move_Player1,
            Action_Move_Player2,
            Action_Wait
        }

        NowAction PlayerNowAction = NowAction.Action_Move_Player1;

        public class _FormDraw
        {
            public static float CB_LineWidth = 0;//棋盘框线宽度
            public static float CB_BlockWidth = 0;//棋盘每格的宽度
            public static int CB_size_width = 0;//棋盘宽的像素大小
            public static int CB_size_height = 0;//棋盘高的像素大小
            public static int StartLocation_X = 16;//棋盘框线的起始位置X坐标
            public static int StartLocation_Y = 16;//棋盘框线的起始位置Y坐标

            /// <summary>
            /// 画棋盘框线函数
            /// </summary>
            /// <param name="Gr">绘画类</param>
            /// <param name="size_width">棋盘宽度</param>
            /// <param name="size_height">棋盘高度</param>
            /// <param name="LineColor">框线颜色</param>
            /// <param name="LineWidth">框线宽度</param>
            public void DrawChessBoard(Graphics Gr, int size_width, int size_height, float LineWidth, float BlockWidth)
            {
                CB_LineWidth = LineWidth;
                CB_size_width = size_width;
                CB_size_height = size_height;
                CB_BlockWidth = BlockWidth;
            }
            /// <summary>
            /// 画挡板
            /// </summary>
            /// <param name="Gr">绘画类</param>
            /// <param name="NA">当前动作状态</param>
            /// <param name="row">第row行挡板</param>
            /// <param name="col">第col列挡板</param>
            /// <param name="BoardColor">挡板颜色</param>
            /// <param name="BoardWidth">挡板宽度，最好和棋盘框长度一样</param>
            public void DrawBoard(Graphics Gr, NowAction NA, int row, int col, Color BoardColor)
            {
                int BlockWidth = _FormDraw.CB_size_width / 8;
                if (NA == NowAction.Action_PlaceVerticalBoard)
                {
                    Pen pen = new Pen(BoardColor, CB_LineWidth);//定义画笔，里面的参数为画笔的颜色

                    int x0 = StartLocation_X, y0 = StartLocation_Y;
                    int x1 = StartLocation_X, y1 = StartLocation_Y;

                    x0 = Convert.ToInt16(x0 + CB_LineWidth / 2 + col * CB_BlockWidth);
                    y0 = Convert.ToInt16(y0 + CB_LineWidth / 2 + row * CB_BlockWidth);
                    x1 = x0;
                    y1 = Convert.ToInt16(y0 + CB_BlockWidth);

                    Gr.DrawLine(pen, x0, y0, x1, y1);
                }
                else if(NA == NowAction.Action_PlaceHorizontalBoard)
                {
                    Pen pen = new Pen(BoardColor, CB_LineWidth);//定义画笔，里面的参数为画笔的颜色

                    int x0 = StartLocation_X, y0 = StartLocation_Y;
                    int x1 = StartLocation_X, y1 = StartLocation_Y;

                    x0 = Convert.ToInt16(x0 + CB_LineWidth / 2 + col * CB_BlockWidth);
                    y0 = Convert.ToInt16(y0 + CB_LineWidth / 2 + row * CB_BlockWidth);
                    x1 = Convert.ToInt16(x0 + CB_BlockWidth);
                    y1 = y0;

                    Gr.DrawLine(pen, x0, y0, x1, y1);
                }
            }
            /// <summary>
            /// 画棋子圆
            /// </summary>
            /// <param name="Gr">绘画类</param>
            /// <param name="row">第row行棋格</param>
            /// <param name="col">第col行棋格</param>
            /// <param name="ChessColor">棋子颜色</param>
            /// <param name="LineWidth">棋盘线宽</param>
            public Point DrawChess(Graphics Gr, int row, int col, Color ChessColor)
            {
                int size_Chess = Convert.ToInt16(_FormDraw.CB_BlockWidth * 0.7);
                //Gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //Brush bush = new SolidBrush(ChessColor);//填充的颜色
                int x = _FormDraw.StartLocation_X + Convert.ToInt16(col * _FormDraw.CB_BlockWidth + size_Chess / 2 - size_Chess / 5);
                int y = _FormDraw.StartLocation_Y + Convert.ToInt16(row * _FormDraw.CB_BlockWidth + size_Chess / 2 - size_Chess / 5);

                return new Point(x, y);

                //Gr.FillEllipse(bush, x, y, size_Chess, size_Chess);//画填充椭圆的方法，x坐标、y坐标、宽、高，如果是100，则半径为50 
            }
            /// <summary>
            /// 获得八皇后棋子绘制位置
            /// </summary>
            /// <param name="row">行</param>
            /// <param name="col">列</param>
            /// <returns>位置坐标</returns>      
            public Point GetQueenChessLocation(int row, int col)
            {
                int size_Chess = Convert.ToInt16(_FormDraw.CB_BlockWidth * 0.7);
                //Gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //Brush bush = new SolidBrush(ChessColor);//填充的颜色
                int x = 14 + Convert.ToInt16(col * _FormDraw.CB_BlockWidth  
                    + size_Chess / 2 - size_Chess / 4);
                int y = 12 + Convert.ToInt16(row * _FormDraw.CB_BlockWidth 
                    + size_Chess / 2 - size_Chess / 4);

                return new Point(x, y);

                //Gr.FillEllipse(bush, x, y, size_Chess, size_Chess);//画填充椭圆的方法，x坐标、y坐标、宽、高，如果是100，则半径为50 
            }

        }
        public Form1()
        {
            InitializeComponent();

        }
        public Form1(GameModeStatus GM)
        {
            InitializeComponent();
            GameMode = GM;
        }
        QuoridorAI NowQuoridor = new QuoridorAI();
        List<CCWin.SkinControl.SkinPictureBox> QueenChessList = new List<CCWin.SkinControl.SkinPictureBox>();
        List<Point> QueenChessLocation = new List<Point>();
        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
            this.Size = new System.Drawing.Size(1024, 720);
            _FormDraw FD = new _FormDraw();

            if (GameMode == GameModeStatus.DoublePlay || GameMode == GameModeStatus.SinglePlay)
            {
                this.Text = "步步为营游戏_" + GameMode.ToString() + "模式_上海海事大学";

                RandomPlaceBTN.Dispose();
                CustomPlaceBTN.Dispose();
                Test2BTN.Dispose();

                ChessBoardPB.Size = new Size(630, 630);
                ChessBoardPB.Image = Resource1.qipan;
                Gr = Graphics.FromImage(ChessBoardPB.Image);
                FD.DrawChessBoard(Gr, 630, 630, 10, 83.5F);

                ChessWhitePB.Size = new Size(58, 58);
                ChessBoardPB.Controls.Add(ChessWhitePB);
                ChessWhitePB.Location = new Point(0, 0);
                ChessWhitePB.BackColor = Color.Transparent;
                ChessBlackPB.Size = new Size(58, 58);
                ChessBoardPB.Controls.Add(ChessBlackPB);
                ChessBlackPB.Location = new Point(100, 100);
                ChessBlackPB.BackColor = Color.Transparent;

                ChessWhitePB.Location = FD.DrawChess(Gr, 0, 3, Color.White);
                ChessBlackPB.Location = FD.DrawChess(Gr, 6, 3, Color.Black);

                VBoardPB.Size = new System.Drawing.Size(10, 170);
                HBoardPB.Size = new System.Drawing.Size(170, 10);

                //刷新初始棋盘
                NowQuoridor.ThisChessBoard.DrawNowChessBoard(ref Gr, ChessWhitePB, ChessBlackPB);
                ChessBoardPB.Refresh();

                TestTB.Text = "当前行动玩家：白子";
                TestTB.Text += System.Environment.NewLine;
                TestTB.Text += "白子剩余挡板：" + NowQuoridor.NumPlayer1Board.ToString();
                TestTB.Text += System.Environment.NewLine;
                TestTB.Text += "黑子剩余挡板：" + NowQuoridor.NumPlayer2Board.ToString(); 
            }
            else
            {
                this.Text = "八皇后路径寻优仿真平台_上海海事大学";

                FD.DrawChessBoard(Gr, 630, 630, 7, 74.5F);

                ChessBoardPB.Size = new Size(630, 630);
                ChessBoardPB.Image = Resource1.qipan2019;
                Gr = Graphics.FromImage(ChessBoardPB.Image);

                ChessWhitePB.Dispose();
                ChessBlackPB.Dispose();
                PlaceVerticalBoardBTN.Dispose();
                PlaceHorizontalBoardBTN.Dispose();
                TestBTN.Dispose();

                QueenChessList.Add(QueenChess1PB);
                QueenChessList.Add(QueenChess2PB);
                QueenChessList.Add(QueenChess3PB);
                QueenChessList.Add(QueenChess4PB);
                QueenChessList.Add(QueenChess5PB);
                QueenChessList.Add(QueenChess6PB);
                QueenChessList.Add(QueenChess7PB);
                QueenChessList.Add(QueenChess8PB);

                foreach (CCWin.SkinControl.SkinPictureBox SPB in QueenChessList)
                {
                    SPB.Size = new Size(58, 58);
                    ChessBoardPB.Controls.Add(SPB);
                    SPB.Visible = false;
                    SPB.BackColor = Color.Transparent;
                    QueenChessLocation.Add(new Point(-1, -1));
                }

                ChessBoardPB.Refresh();
            }
        }

        bool IfPlaceBoard = false;

        private void PlaceBoardBTN_Click(object sender, EventArgs e)
        {
            PlayerNowAction = NowAction.Action_PlaceVerticalBoard;
            PlaceVerticalBoardBTN.Enabled = false;
            PlaceHorizontalBoardBTN.Enabled = false;
            IfPlaceBoard = true;
            RestartFollow();
        }

        private void MoveBTN_Click(object sender, EventArgs e)
        {
            if(NowPlayer == EnumNowPlayer.Player1) 
                PlayerNowAction = NowAction.Action_Move_Player1;
            else
                PlayerNowAction = NowAction.Action_Move_Player2;
        }
        public System.Drawing.Point TP = new System.Drawing.Point();

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

    
        }
        private void ChessBoardPB_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void ChessWhitePB_Click(object sender, EventArgs e)
        {
            string Hint1 = "This is Not Empty!";
            Console.WriteLine(Hint1);

        }

        private void PlaceHorizontalBoardBTN_Click(object sender, EventArgs e)
        {
            PlayerNowAction = NowAction.Action_PlaceHorizontalBoard;
            PlaceVerticalBoardBTN.Enabled = false;
            PlaceHorizontalBoardBTN.Enabled = false;
            IfPlaceBoard = true;
            RestartFollow();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            FreeConsole();
            Application.Exit();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        string MouseAction_PlayChess(Point TP)
        {
            string Hint = "OK";
            # region 计算相关操作对应的操作位置的行和列

            int col = 0, row = 0;//0~7行列
            if (PlayerNowAction == NowAction.Action_PlaceVerticalBoard)
            {
                col = Convert.ToInt16((TP.X - _FormDraw.StartLocation_X) / _FormDraw.CB_BlockWidth);
                row = Convert.ToInt16((TP.Y + _FormDraw.CB_BlockWidth / 2 - _FormDraw.StartLocation_Y) / _FormDraw.CB_BlockWidth) - 1;
            }
            else if (PlayerNowAction == NowAction.Action_PlaceHorizontalBoard)
            {
                col = Convert.ToInt16((TP.X + _FormDraw.CB_BlockWidth / 2 - _FormDraw.StartLocation_X) / _FormDraw.CB_BlockWidth) - 1;
                row = Convert.ToInt16((TP.Y - _FormDraw.StartLocation_Y) / _FormDraw.CB_BlockWidth);
            }
            else if (PlayerNowAction == NowAction.Action_Move_Player1 || PlayerNowAction == NowAction.Action_Move_Player2)
            {
                col = Convert.ToInt16(TP.X / _FormDraw.CB_BlockWidth) - 1;
                row = Convert.ToInt16(TP.Y / _FormDraw.CB_BlockWidth) - 1;
            }

            # endregion

            if (!(row >= 0 && row <= 6 && col >= 0 && col <= 6))
            {
                Hint = "点击越界！";
                return Hint;
            }
            string Hint1 = "";
            string Hint2 = "";
            Hint1 = NowQuoridor.CheckBoard(PlayerNowAction, EnumNowPlayer.Player1, row, col);
            Hint2 = NowQuoridor.CheckBoard(PlayerNowAction, EnumNowPlayer.Player2, row, col);


            if (Hint1 == "Player1 No Board")
            {
                if (PlayerNowAction == NowAction.Action_PlaceHorizontalBoard
                    || PlayerNowAction == NowAction.Action_PlaceVerticalBoard)
                {
                    MessageBox.Show(Hint1);
                    return Hint1;
                }
            }
            else if (Hint2 == "Player2 No Board")
            {
                if (PlayerNowAction == NowAction.Action_PlaceHorizontalBoard
                    || PlayerNowAction == NowAction.Action_PlaceVerticalBoard)
                {
                    MessageBox.Show(Hint2);
                    return Hint2;
                }
            }

            if ((Hint1 != "Player1 No Board" && Hint2 != "Player2 No Board")
                && (Hint1 != "OK" || Hint2 != "OK"))
            {
                if (Hint1 != "OK" && Hint2 == "OK")
                    MessageBox.Show(Hint1);
                else if (Hint2 != "OK" && Hint1 == "OK")
                    MessageBox.Show(Hint2);
                else if (Hint2 != "OK" && Hint1 != "OK")
                    MessageBox.Show("P1:" + Hint1 + " P2:" + Hint2);
                return Hint1 + Hint2;
            }

            Hint = NowQuoridor.ThisChessBoard.Action(row, col, PlayerNowAction);

            if (Hint != "OK")
            {
                MessageBox.Show(Hint);
                return Hint;
            }
            if (NowPlayer == EnumNowPlayer.Player1)
            {
                if (PlayerNowAction == NowAction.Action_PlaceVerticalBoard
                    || PlayerNowAction == NowAction.Action_PlaceHorizontalBoard)
                    NowQuoridor.NumPlayer1Board -= 2;
                NowPlayer = EnumNowPlayer.Player2;
            }
            else if (NowPlayer == EnumNowPlayer.Player2)
            {
                if (PlayerNowAction == NowAction.Action_PlaceVerticalBoard
                    || PlayerNowAction == NowAction.Action_PlaceHorizontalBoard)
                    NowQuoridor.NumPlayer2Board -= 2;
                NowPlayer = EnumNowPlayer.Player1;
            }

            NowQuoridor.ThisChessBoard.DrawNowChessBoard(ref Gr, ChessWhitePB, ChessBlackPB);
            ChessBoardPB.Refresh();

            string result = NowQuoridor.CheckResult();
            if (result != "No success")
            {
                MessageBox.Show(result);
            }

            if (NowPlayer == EnumNowPlayer.Player1)
            {
                //MessageBox.Show("现在轮到玩家1操作！");
                TestTB.Text = "当前行动玩家：白子";
                TestTB.Text += System.Environment.NewLine;
                TestTB.Text += "白子剩余挡板：" + NowQuoridor.NumPlayer1Board.ToString();
                TestTB.Text += System.Environment.NewLine;
                TestTB.Text += "黑子剩余挡板：" + NowQuoridor.NumPlayer2Board.ToString();

                PlayerNowAction = NowAction.Action_Move_Player1;
            }
            if (NowPlayer == EnumNowPlayer.Player2)
            {
                //MessageBox.Show("现在轮到玩家2操作！");
                TestTB.Text = "当前行动玩家：黑子";
                TestTB.Text += System.Environment.NewLine;
                TestTB.Text += "白子剩余挡板：" + NowQuoridor.NumPlayer1Board.ToString();
                TestTB.Text += System.Environment.NewLine;
                TestTB.Text += "黑子剩余挡板：" + NowQuoridor.NumPlayer2Board.ToString();

                PlayerNowAction = NowAction.Action_Move_Player2;
            }
            NowQuoridor.Player_Now = NowPlayer;

            #region AI落子
            if (GameMode == GameModeStatus.SinglePlay)
            {
                QuoridorAction AIAction = NowQuoridor.AIAction_Greedy(EnumNowPlayer.Player2);
                Hint = NowQuoridor.ThisChessBoard.Action(AIAction.ActionPoint.X, AIAction.ActionPoint.Y, AIAction.PlayerAction);
                PlayerNowAction = AIAction.PlayerAction;
                if (Hint != "OK")
                {
                    MessageBox.Show(Hint);
                    return Hint;
                }
                if (NowPlayer == EnumNowPlayer.Player1)
                {
                    if (PlayerNowAction == NowAction.Action_PlaceVerticalBoard
                        || PlayerNowAction == NowAction.Action_PlaceHorizontalBoard)
                        NowQuoridor.NumPlayer1Board -= 2;
                    NowPlayer = EnumNowPlayer.Player2;
                }
                else if (NowPlayer == EnumNowPlayer.Player2)
                {
                    if (PlayerNowAction == NowAction.Action_PlaceVerticalBoard
                        || PlayerNowAction == NowAction.Action_PlaceHorizontalBoard)
                        NowQuoridor.NumPlayer2Board -= 2;
                    NowPlayer = EnumNowPlayer.Player1;
                }

                NowQuoridor.ThisChessBoard.DrawNowChessBoard(ref Gr, ChessWhitePB, ChessBlackPB);
                ChessBoardPB.Refresh();

                result = NowQuoridor.CheckResult();
                if (result != "No success")
                {
                    MessageBox.Show(result);
                }

                if (NowPlayer == EnumNowPlayer.Player1)
                {
                    //MessageBox.Show("现在轮到玩家1操作！");
                    TestTB.Text = "当前行动玩家：白子";
                    TestTB.Text += System.Environment.NewLine;
                    TestTB.Text += "白子剩余挡板：" + NowQuoridor.NumPlayer1Board.ToString();
                    TestTB.Text += System.Environment.NewLine;
                    TestTB.Text += "黑子剩余挡板：" + NowQuoridor.NumPlayer2Board.ToString();

                    PlayerNowAction = NowAction.Action_Move_Player1;
                }
                if (NowPlayer == EnumNowPlayer.Player2)
                {
                    //MessageBox.Show("现在轮到玩家2操作！");
                    TestTB.Text = "当前行动玩家：黑子";
                    TestTB.Text += System.Environment.NewLine;
                    TestTB.Text += "白子剩余挡板：" + NowQuoridor.NumPlayer1Board.ToString();
                    TestTB.Text += System.Environment.NewLine;
                    TestTB.Text += "黑子剩余挡板：" + NowQuoridor.NumPlayer2Board.ToString();

                    PlayerNowAction = NowAction.Action_Move_Player2;
                }
                NowQuoridor.Player_Now = NowPlayer;
            }
            #endregion

            PlaceVerticalBoardBTN.Enabled = true;
            PlaceHorizontalBoardBTN.Enabled = true;

            return Hint;
        }
        int Place_Index = 0;
        void MouseAction_PlaceQueen(Point TP)
        {
            string Hint = "OK";
            # region 计算相关操作对应的操作位置的行和列           
            int col = 0, row = 0;//0~7行列

            col = Convert.ToInt16((TP.X - 14 + _FormDraw.CB_BlockWidth / 2) / _FormDraw.CB_BlockWidth) - 1;
            row = Convert.ToInt16((TP.Y - 12 + _FormDraw.CB_BlockWidth / 2) / _FormDraw.CB_BlockWidth) - 1;

            # endregion

            Console.WriteLine("row = " + row.ToString() + " col = " + col.ToString());

            if (!(row >= 0 && row <= 7 && col >= 0 && col <= 7))
            {
                Hint = "点击越界！";
            }

            if (Hint == "OK")
            {
                _FormDraw FDBuf = new _FormDraw();
                QueenChessList[Place_Index].Visible = true;
                QueenChessList[Place_Index].Location = FDBuf.GetQueenChessLocation(row, col);
                QueenChessLocation[Place_Index] = new Point(row, col);
                Place_Index++;
                if(Place_Index >= 8)
                {
                    Place_Index = 0;
                    CustomPlaceBTN.Enabled = true;
                }
            }
        }
        /// <summary>
        /// 重启挡板跟随鼠标移动显示
        /// </summary>
        public void RestartFollow()
        {
            IfShowFollow = true;
            if (PlayerNowAction == NowAction.Action_PlaceHorizontalBoard)
                HBoardPB.Visible = true;
            else if(PlayerNowAction == NowAction.Action_PlaceVerticalBoard)
                VBoardPB.Visible = true;
        }

        private void ChessBoardPB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)//右键取消正在执行的操作
            {
                if (IfPlaceBoard)
                {
                    IfPlaceBoard = false;
                    if (NowPlayer == EnumNowPlayer.Player1)
                    {
                        PlayerNowAction = NowAction.Action_Move_Player1;
                    }
                    if (NowPlayer == EnumNowPlayer.Player2)
                    {
                        PlayerNowAction = NowAction.Action_Move_Player2;
                    }
                    PlaceVerticalBoardBTN.Enabled = true;
                    PlaceHorizontalBoardBTN.Enabled = true;

                    IfShowFollow = false;
                    VBoardPB.Visible = false;
                    VBoardPB.Location = new Point(837, 569);
                    HBoardPB.Visible = false;
                    HBoardPB.Location = new Point(837, 569);
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)//左键执行某个行动
            {
                TP = e.Location;

                if (GameMode == GameModeStatus.SinglePlay || GameMode == GameModeStatus.DoublePlay)
                {
                    string Result = MouseAction_PlayChess(TP);
                    if (Result == "OK")
                    {
                        IfShowFollow = false;
                        VBoardPB.Visible = false;
                        VBoardPB.Location = new Point(837, 569);
                        HBoardPB.Visible = false;
                        HBoardPB.Location = new Point(837, 569);
                    }
                    else 
                    {
                        RestartFollow();
                    }
                }
                else
                {
                    Console.WriteLine(TP.X.ToString() + "," + TP.Y.ToString());
                    MouseAction_PlaceQueen(TP);
                }
            }

        }

        private void TestBTN_Click(object sender, EventArgs e)
        {
            //int rowbuff = NowQuoridor.ThisChessBoard.Player1Location.X;
            //int colbuff = NowQuoridor.ThisChessBoard.Player1Location.Y;
            //int player1dis = NowQuoridor.AstarRestart(EnumNowPlayer.Player1, rowbuff, colbuff);
            //Console.WriteLine("玩家1最短路径长度：");
            //Console.WriteLine(player1dis.ToString());

            //rowbuff = NowQuoridor.ThisChessBoard.Player2Location.X;
            //colbuff = NowQuoridor.ThisChessBoard.Player2Location.Y;
            //int player2dis = NowQuoridor.AstarRestart(EnumNowPlayer.Player2, rowbuff, colbuff);
            //Console.WriteLine("玩家2最短路径长度：");
            //Console.WriteLine(player2dis.ToString());

            //List<Point> Roadbuff = NowQuoridor.Player1MinRoad;
            //Console.WriteLine("Player1最短路径：");
   
            //for (int i = Roadbuff.Count - 1; i >= 0; i--)
            //{
            //    Console.WriteLine(Roadbuff[i].X.ToString()+ ", " +Roadbuff[i].Y.ToString());
            //}
            //Roadbuff = NowQuoridor.Player2MinRoad;
            //Console.WriteLine("Player2最短路径：");
            //for (int i = Roadbuff.Count - 1; i >= 0; i--)
            //{
            //    Console.WriteLine(Roadbuff[i].X.ToString() + ", " + Roadbuff[i].Y.ToString());
            //}
            NowQuoridor.TestEvaluation();
        }
        bool IfShowFollow = false;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IfShowFollow)
            {
                if (PlayerNowAction == NowAction.Action_PlaceHorizontalBoard)
                {
                    HBoardPB.Location = e.Location;
                }
                else if (PlayerNowAction == NowAction.Action_PlaceVerticalBoard)
                {
                    VBoardPB.Location = e.Location;
                }
            }
        }

        private void VBoardPB_Click(object sender, EventArgs e)
        {

        }

        int delaycount = 0;
        private void ChessBoardPB_MouseMove(object sender, MouseEventArgs e)
        {
            if (IfShowFollow)
            {
                delaycount++;

                if (delaycount >= 3)
                {
                    delaycount = 0;
                    int L_X = e.Location.X;
                    int L_Y = e.Location.Y;

                    Point MovePoint = new Point(L_X - 5, L_Y + 5);

                    if (PlayerNowAction == NowAction.Action_PlaceHorizontalBoard)
                    {
                        HBoardPB.Location = MovePoint;
                    }
                    else if (PlayerNowAction == NowAction.Action_PlaceVerticalBoard)
                    {
                        VBoardPB.Location = MovePoint;
                    }
                }
            }
        }

        private void skinPictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void CustomPlaceBTN_Click(object sender, EventArgs e)
        {
            CustomPlaceBTN.Enabled = false;
            RandomPlaceBTN.Enabled = false;
        }

    }
}
