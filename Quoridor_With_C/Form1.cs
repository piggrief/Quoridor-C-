using System;
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
using Queen;
using DebugToolForm;

namespace Quoridor_With_C
{
    public partial class Form1 : Skin_Metro
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole(); 

        public static Graphics Gr;
        Graphics Gr_Chess1;

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
        public static Form1 form1;
        public Form1()
        {
           InitializeComponent();
           form1 = this;

        }
        public Form1(GameModeStatus GM)
        {
            InitializeComponent();
            GameMode = GM;
            form1 = this;

        }
        QuoridorAI NowQuoridor = new QuoridorAI();
        List<CCWin.SkinControl.SkinPictureBox> QueenChessList = new List<CCWin.SkinControl.SkinPictureBox>();
        List<CCWin.SkinControl.SkinPictureBox> QueenList = new List<CCWin.SkinControl.SkinPictureBox>();      
        public static List<Point> QueenChessLocation = new List<Point>();
        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
            this.Size = new System.Drawing.Size(1024, 720);
            _FormDraw FD = new _FormDraw();

            QueenList.Add(Queen1PB);
            QueenList.Add(Queen2PB);
            QueenList.Add(Queen3PB);
            QueenList.Add(Queen4PB);
            QueenList.Add(Queen5PB);
            QueenList.Add(Queen6PB);
            QueenList.Add(Queen7PB);
            QueenList.Add(Queen8PB);

            foreach (CCWin.SkinControl.SkinPictureBox SPB in QueenList)
            {
                SPB.Size = new Size(58, 58);
                ChessBoardPB.Controls.Add(SPB);
                SPB.Visible = false;
                SPB.BackColor = Color.Transparent;
                QueenChessLocation.Add(new Point(-1, -1));
            }

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
            }


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

                HideQueen(QueenList);

                //刷新初始棋盘
                NowQuoridor.ThisChessBoard.DrawNowChessBoard(ref Gr, ChessWhitePB, ChessBlackPB);
                ChessBoardPB.Refresh();

                TestTB.Text = "当前行动玩家：白子";
                TestTB.Text += System.Environment.NewLine;
                TestTB.Text += "白子剩余挡板：" + QuoridorRuleEngine.NumPlayer1Board.ToString();
                TestTB.Text += System.Environment.NewLine;
                TestTB.Text += "黑子剩余挡板：" + QuoridorRuleEngine.NumPlayer2Board.ToString(); 
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


                ChessBoardPB.Refresh();
            }
            //this.Location = new Point(0, 0);
            DebugTool DT1 = new DebugTool(this);
            //DT1.Location = new Point(this.Size.Width, 0);
            DT1.Show();
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
        long count_AIAction = 0;
        /// <summary>
        /// 根据点击坐标执行下棋操作
        /// </summary>
        /// <param name="TP">点击坐标</param>
        /// <returns>行动提示字符串</returns>
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

            #region 玩家落子
            if (!(row >= 0 && row <= 6 && col >= 0 && col <= 6))
            {
                Hint = "点击越界！";
                return Hint;
            }
            string Hint1 = "";
            string Hint2 = "";
            Hint1 = NowQuoridor.QuoridorRule.CheckBoard(NowQuoridor.ThisChessBoard, PlayerNowAction, EnumNowPlayer.Player1, row, col);
            Hint2 = NowQuoridor.QuoridorRule.CheckBoard(NowQuoridor.ThisChessBoard, PlayerNowAction, EnumNowPlayer.Player2, row, col);


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

            Hint = NowQuoridor.QuoridorRule.Action(ref NowQuoridor.ThisChessBoard,row, col, PlayerNowAction);

            if (Hint != "OK")
            {
                MessageBox.Show(Hint);
                return Hint;
            }
            if (NowPlayer == EnumNowPlayer.Player1)
            {
                if (PlayerNowAction == NowAction.Action_PlaceVerticalBoard
                    || PlayerNowAction == NowAction.Action_PlaceHorizontalBoard)
                    QuoridorRuleEngine.NumPlayer1Board -= 2;
                NowPlayer = EnumNowPlayer.Player2;
            }
            else if (NowPlayer == EnumNowPlayer.Player2)
            {
                if (PlayerNowAction == NowAction.Action_PlaceVerticalBoard
                    || PlayerNowAction == NowAction.Action_PlaceHorizontalBoard)
                    QuoridorRuleEngine.NumPlayer2Board -= 2;
                NowPlayer = EnumNowPlayer.Player1;
            }

            NowQuoridor.ThisChessBoard.DrawNowChessBoard(ref Gr, ChessWhitePB, ChessBlackPB);
            ChessBoardPB.Refresh();

            string result = NowQuoridor.QuoridorRule.CheckResult(NowQuoridor.ThisChessBoard);
            if (result != "No success")
            {
                MessageBox.Show(result);
            }

            if (NowPlayer == EnumNowPlayer.Player1)
            {
                //MessageBox.Show("现在轮到玩家1操作！");
                TestTB.Text = "当前行动玩家：白子";
                TestTB.Text += System.Environment.NewLine;
                TestTB.Text += "白子剩余挡板：" + QuoridorRuleEngine.NumPlayer1Board.ToString();
                TestTB.Text += System.Environment.NewLine;
                TestTB.Text += "黑子剩余挡板：" + QuoridorRuleEngine.NumPlayer2Board.ToString();

                PlayerNowAction = NowAction.Action_Move_Player1;
            }
            if (NowPlayer == EnumNowPlayer.Player2)
            {
                //MessageBox.Show("现在轮到玩家2操作！");
                TestTB.Text = "当前行动玩家：黑子";
                TestTB.Text += System.Environment.NewLine;
                TestTB.Text += "白子剩余挡板：" + QuoridorRuleEngine.NumPlayer1Board.ToString();
                TestTB.Text += System.Environment.NewLine;
                TestTB.Text += "黑子剩余挡板：" + QuoridorRuleEngine.NumPlayer2Board.ToString();

                PlayerNowAction = NowAction.Action_Move_Player2;
            }
            NowQuoridor.Player_Now = NowPlayer;
            #endregion

            #region AI落子
            if (GameMode == GameModeStatus.SinglePlay)
            {
                QuoridorAI.AIRunTime.AstarNum = 0;
                QuoridorAI.AIRunTime.Astar_s = 0;

                GameTreeNode Root = new GameTreeNode();
                Root.NodePlayer = EnumNowPlayer.Player1;
                count_AIAction++;
                Console.WriteLine("第" + count_AIAction.ToString() + "次落子:");
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start(); //  开始监视代码运行时间
                /***************待测代码段****************/
                GameTreeNode.CreateGameTree(Root, NowQuoridor.ThisChessBoard, 1, false);//skinCheckBox1.Checked);//可以改变最大深度来提高算法强度,一定要是奇数
                /***************待测代码段****************/
                stopwatch.Stop(); //  停止监视
                TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
                double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数
                double seconds = timespan.TotalSeconds;
                Console.WriteLine("算法用时：" + seconds.ToString() + "s");
                GameTreeNode.NodeNum = 0;
                GameTreeNode.CalGameTreeNodeNum(Root);
                Console.WriteLine("博弈树节点总数：" + GameTreeNode.NodeNum.ToString() + "个");
                Console.WriteLine("Astar平均用时：" + QuoridorAI.AIRunTime.Astar_s.ToString() + "ms");
                Console.WriteLine("Astar次数：" + QuoridorAI.AIRunTime.AstarNum.ToString() + "次");
                Console.WriteLine("Astar总用时：" + (QuoridorAI.AIRunTime.Astar_s * QuoridorAI.AIRunTime.AstarNum).ToString() + "ms");
                Console.WriteLine("*************");
                
                //NowQuoridor.AlphaBetaPruningInit(NowQuoridor.ThisChessBoard.ChessBoardAll, EnumNowPlayer.Player2);
                //QuoridorAction AIAction = NowQuoridor.AlphaBetaPruning(NowQuoridor.ThisChessBoard, EnumNowPlayer.Player2, 4, -10000, 10000, ref buff);
                //QuoridorAction AIAction = NowQuoridor.AIAction_Greedy(EnumNowPlayer.Player2);
                Hint = NowQuoridor.QuoridorRule.Action(ref NowQuoridor.ThisChessBoard, Root.ActionLocation.X, Root.ActionLocation.Y, Root.NodeAction);
                PlayerNowAction = Root.NodeAction;
                if (Hint != "OK")
                {
                    MessageBox.Show(Hint);
                    return Hint;
                }
                if (NowPlayer == EnumNowPlayer.Player1)
                {
                    if (PlayerNowAction == NowAction.Action_PlaceVerticalBoard
                        || PlayerNowAction == NowAction.Action_PlaceHorizontalBoard)
                        QuoridorRuleEngine.NumPlayer1Board -= 2;
                    NowPlayer = EnumNowPlayer.Player2;
                }
                else if (NowPlayer == EnumNowPlayer.Player2)
                {
                    if (PlayerNowAction == NowAction.Action_PlaceVerticalBoard
                        || PlayerNowAction == NowAction.Action_PlaceHorizontalBoard)
                        QuoridorRuleEngine.NumPlayer2Board -= 2;
                    NowPlayer = EnumNowPlayer.Player1;
                }

                NowQuoridor.ThisChessBoard.DrawNowChessBoard(ref Gr, ChessWhitePB, ChessBlackPB);
                ChessBoardPB.Refresh();

                result = NowQuoridor.QuoridorRule.CheckResult(NowQuoridor.ThisChessBoard);
                if (result != "No success")
                {
                    MessageBox.Show(result);
                }

                if (NowPlayer == EnumNowPlayer.Player1)
                {
                    //MessageBox.Show("现在轮到玩家1操作！");
                    TestTB.Text = "当前行动玩家：白子";
                    TestTB.Text += System.Environment.NewLine;
                    TestTB.Text += "白子剩余挡板：" + QuoridorRuleEngine.NumPlayer1Board.ToString();
                    TestTB.Text += System.Environment.NewLine;
                    TestTB.Text += "黑子剩余挡板：" + QuoridorRuleEngine.NumPlayer2Board.ToString();

                    PlayerNowAction = NowAction.Action_Move_Player1;
                }
                if (NowPlayer == EnumNowPlayer.Player2)
                {
                    //MessageBox.Show("现在轮到玩家2操作！");
                    TestTB.Text = "当前行动玩家：黑子";
                    TestTB.Text += System.Environment.NewLine;
                    TestTB.Text += "白子剩余挡板：" + QuoridorRuleEngine.NumPlayer1Board.ToString();
                    TestTB.Text += System.Environment.NewLine;
                    TestTB.Text += "黑子剩余挡板：" + QuoridorRuleEngine.NumPlayer2Board.ToString();

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
        /// <summary>
        /// 根据点击坐标生成棋子初始位置
        /// </summary>
        /// <param name="TP"></param>
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
        /// <summary>
        /// 用于步步为营AI的一些算法测试
        /// </summary>
        private void TestBTN_Click(object sender, EventArgs e)
        {
            //GameTreeNode Root = new GameTreeNode();
            //Root.NodePlayer = EnumNowPlayer.Player1;
            //GameTreeNode.CreateGameTree(Root, NowQuoridor.ThisChessBoard, 1, true);

            Form1.EnumNowPlayer PlayerSave = EnumNowPlayer.Player2;
            NowQuoridor.Player_Now = PlayerSave;

            List<QuoridorAction> QABuff = NowQuoridor.ActionList;

            QABuff = NowQuoridor.CreateActionList(NowQuoridor.ThisChessBoard);

            Console.WriteLine("Stop!");
        }
        bool IfShowFollow = false;
        /// <summary>
        /// 用于在Form窗体上实现挡板跟随效果
        /// </summary>
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

        int delaycount = 0;//用于挡板跟随延迟计数
        const int DelayTime_Follow = 3;//挡板跟随延迟总数,可以控制挡板跟随效果的延迟
        /// <summary>
        /// 用于挡板跟随
        /// </summary>
        private void ChessBoardPB_MouseMove(object sender, MouseEventArgs e)
        {
            if (IfShowFollow)
            {
                delaycount++;

                if (delaycount >= DelayTime_Follow)
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

        public static QueenSolve ThisQueenSolve = new QueenSolve(QueenSolve.DistanceCalMethod.ManhattanDistance
                                                         ,QueenSolve.InitResultMethod.Dijkstra, 92);
        /// <summary>
        /// 显示八皇后位置（在棋盘上显示）
        /// </summary>
        /// <param name="QueenLocation">皇后位置（行列表示）</param>
        /// <param name="QueenPB">皇后位置图片列表</param>
        public void ShowQueenLocation(List<Point> QueenLocation, List<CCWin.SkinControl.SkinPictureBox> QueenPB)
        {
            _FormDraw FDBuff = new _FormDraw();
            for (int i = 0; i < QueenPB.Count; i++)
            {
                QueenPB[i].Location = FDBuff.GetQueenChessLocation(QueenLocation[i].X,QueenLocation[i].Y);
                QueenPB[i].Visible = true;
            }
        }
        /// <summary>
        /// 隐藏皇后位置
        /// </summary>
        /// <param name="QueenPB">皇后图片列表</param>
        public void HideQueen(List<CCWin.SkinControl.SkinPictureBox> QueenPB)
        {
            foreach (CCWin.SkinControl.SkinPictureBox SPB in QueenPB)
            {
                SPB.Visible = false;
                SPB.Location = new Point(808, 348);
            }
        }
        bool IsShowQueenFlag = true;
       /// <summary>
       /// 主要用于测试八皇后寻优
       /// </summary>
 
        private void Test2BTN_Click(object sender, EventArgs e)
        {
            ThisQueenSolve.ChessLocationList = QueenChessLocation;

            ThisQueenSolve.QueenLocationList = new List<Point>();
            for (int i = 0; i < 8; i++)
            {
                ThisQueenSolve.QueenLocationList.Add(new Point(i, ThisQueenSolve.EightQueenResult[0, i] - 1));
            }

            if (IsShowQueenFlag)
            {
                ShowQueenLocation(ThisQueenSolve.QueenLocationList, QueenList);
                Queen.QueenSolve.SelectedQueenResultNum = 92;
                ///1000 0.96 64
                //ThisQueenSolve.InitSA(1000, 0.96, 64, 0, SimulateAnneal.Annealing.SAMode.SA);
                //ThisQueenSolve.InitSA(1000, 0.9, 90, 0.1, SimulateAnneal.Annealing.SAMode.FastSA);

                List<Point> BestResult_QueenLocation = new List<Point>();
                List<int> MoveSequence = new List<int>();
                double disall = 0;

                #region 算法测试
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start(); //  开始监视代码运行时间
                //  需要测试的代码 
                //MoveSequence = ThisQueenSolve.SearchResult_ForOverall(ref disall, ref BestResult_QueenLocation, SearchPB);
                //ThisQueenSolve.Test_SAParameter(1000, 0.9, 90, 0.1, SimulateAnneal.Annealing.SAMode.FastSA, 50);
                ThisQueenSolve.Test_SAParameter(1000, 0.96, 64, 0, SimulateAnneal.Annealing.SAMode.SA, 10);
                //ThisQueenSolve.InitSA(200, 0.9, 32, 0, SimulateAnneal.Annealing.SAMode.SA);
                //ThisQueenSolve.Test_SAParameter_Auto(ThisQueenSolve.ThisSAMode, ThisQueenSolve.ThisSA
                //    , QueenSolve.WhichSAParameter.Lenght, 2, 4, 50, 20);

                stopwatch.Stop(); //  停止监视
                TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
                double hours = timespan.TotalHours; // 总小时
                double minutes = timespan.TotalMinutes;  // 总分钟
                double seconds = timespan.TotalSeconds;  //  总秒数
                double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数

                //MoveSequence = ThisQueenSolve.CreateInitResult(ThisQueenSolve.ChessLocationList
                //                                              ,ThisQueenSolve.QueenLocationList
                //                                              ,ref disall);

                ThisQueenSolve.PrintMoveSequence(MoveSequence, ThisQueenSolve.ChessLocationList, ThisQueenSolve.QueenLocationList);
                Console.WriteLine("总路径长度为" + disall.ToString());
                Console.WriteLine("用时(s) ：" + seconds.ToString() + "秒");
                Console.WriteLine("用时(ms)：" + milliseconds.ToString() + "毫秒");

                #endregion
            }
            else
            {
                HideQueen(QueenList);
            }
            IsShowQueenFlag = !IsShowQueenFlag;
        }

        private void Queen3PB_Click(object sender, EventArgs e)
        {

        }

        private void SearchPB_Click(object sender, EventArgs e)
        {
            
        }

    }
}
