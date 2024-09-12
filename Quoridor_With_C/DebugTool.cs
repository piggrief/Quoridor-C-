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
using System.IO.Ports;
using System.IO;
using System.Threading;
using UartComunication;
using System.Windows.Forms.DataVisualization.Charting;
using Quoridor;
using Quoridor_With_C;

namespace DebugToolForm
{
    public partial class DebugTool : Skin_Metro
    {
        int Size_x = 640;
        int Size_y = 480;
        public DebugTool()
        {
            InitializeComponent();
        }
        Form1 Form_ChessBorad = null;
        public DebugTool(Form1 Formbuff)
        {
            InitializeComponent();
            Form_ChessBorad = Formbuff;
        }
        /// <summary>
        /// 串口设置界面初始化，一般就是选择ComboBox控件的默认选项
        /// </summary>
        public void SerialPortSetInit()
        {
            BaudCB.SelectedIndex = 2;//115200
            DataBitsCB.SelectedIndex = 0;
            StopBitsCB.SelectedIndex = 0;
            ParityCB.SelectedIndex = 0;
            PortCBFresh();
            if (PortCB.Items.Count > 0)
                PortCB.SelectedIndex = 0;
        }
        /// <summary>
        /// 图表设置界面初始化，一般就是选择ComboBox控件的默认选项
        /// </summary>
        public void ChartSetInit()
        {
            IfSASelctCB.SelectedIndex = 0;
            SAModeSetTB.SelectedIndex = 0;
        }
        /// <summary>
        /// 串口端口号Combox刷新
        /// </summary>
        public void PortCBFresh()
        {
            string[] ports = SerialPort.GetPortNames();
            PortCB.Items.Clear();
            foreach (string port in ports)
            {
                PortCB.Items.Add(port);
            }
            if (PortCB.Items.Count == 0)
            {
                PortCB.Text = "";
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void DebugTool_Load(object sender, EventArgs e)
        { 
            Control.CheckForIllegalCrossThreadCalls = false;

            SerialPortSetInit();
            ChartSetInit();
            this.Size = new System.Drawing.Size(640, 480);
            Size_x = this.Size.Width;
            Size_y = this.Size.Height;
            skinTabControl1.Location = new Point(skinMenuStrip1.Location.X, skinMenuStrip1.Size.Height + skinMenuStrip1.Location.Y);
            skinTabControl1.Size = new Size(this.Size.Width - skinMenuStrip1.Location.X * 2, this.Size.Height - skinTabControl1.Location.Y - skinMenuStrip1.Location.Y);

            ShowPoint = Chart1.Series.First().Points;
            ShowPoint2 = Chart1.Series[1].Points;
            ShowPoint.Add(new DataPoint(1, 1));
            ShowPoint.Add(new DataPoint(2, 5));
            ShowPoint.Add(new DataPoint(3, -2));
            ShowPoint.Add(new DataPoint(4, 0));
            ShowPoint.Add(new DataPoint(5, 4.5));

        }
        /// <summary>
        /// 端口设置ComboBox控件点击事件，用来刷新串口端口
        /// </summary>
        private void PortCB_Click(object sender, EventArgs e)
        {
            PortCBFresh();
        }
        System.Drawing.Size ChangeSize(System.Drawing.Size S, double rate_x, double rate_y)
        {

            int h = S.Height;
            int w = S.Width;

            int newh = Convert.ToInt32(h * rate_y);
            int neww = Convert.ToInt32(w * rate_x);

            System.Drawing.Size S_new = new System.Drawing.Size(neww, newh);

            return S_new;
        }
        private void DebugTool_Resize(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 控件尺寸跟随改变
        /// </summary>
        private void DebugTool_SizeChanged(object sender, EventArgs e)
        {
            int NewSize_x = this.Size.Width;
            int NewSize_y = this.Size.Height;
            double rate_x = Convert.ToDouble(NewSize_x) / Convert.ToDouble(Size_x);
            double rate_y = Convert.ToDouble(NewSize_y) / Convert.ToDouble(Size_y);

            skinTabControl1.Location = new Point(skinMenuStrip1.Location.X, skinMenuStrip1.Size.Height + skinMenuStrip1.Location.Y);
            skinTabControl1.Size = new Size(this.Size.Width - skinMenuStrip1.Location.X * 2,this.Size.Height - skinTabControl1.Location.Y - skinMenuStrip1.Location.Y);

            Size_x = NewSize_x;
            Size_y = NewSize_y;
        }
        /// <summary>
        /// 控件尺寸跟随改变
        /// </summary>
        private void skinTabControl1_SizeChanged(object sender, EventArgs e)
        {
            #region 接收区调整
            ReceiveGB.Size = new Size(skinTabControl1.Size.Width - ReceiveGB.Location.X, Convert.ToInt32(skinTabControl1.Size.Height / 2.0 - ReceiveGB.Location.Y));

            ReceiveClearBTN.Location = new Point(Convert.ToInt32(ReceiveGB.Size.Width - ReceiveClearBTN.Size.Width*1.1)
                                                , Convert.ToInt32(ReceiveGB.Size.Height - ReceiveClearBTN.Size.Height*1.1));
            SaveDataBTN.Location = new Point(Convert.ToInt32(ReceiveGB.Size.Width - SaveDataBTN.Size.Width * 1.1)
                                                , Convert.ToInt32(ReceiveClearBTN.Location.Y - SaveDataBTN.Size.Height * 1.1));
            SwitchReceiveBTN.Location = new Point(Convert.ToInt32(ReceiveGB.Size.Width - SwitchReceiveBTN.Size.Width * 1.1)
                                                , Convert.ToInt32(SaveDataBTN.Location.Y - SwitchReceiveBTN.Size.Height * 1.1));

            HexCB.Location = new Point(Convert.ToInt32(ReceiveGB.Size.Width - HexCB.Size.Width * 1.1)
                                                , Convert.ToInt32(SwitchReceiveBTN.Location.Y - HexCB.Size.Height * 1.1));

            ReceiveTB.Size = new Size(Convert.ToInt32(ReceiveGB.Size.Width - ReceiveClearBTN.Size.Width * 1.1 - ReceiveClearBTN.Size.Height * 0.5)
                                    , ReceiveGB.Size.Height - ReceiveTB.Location.Y - 5);
            #endregion

            #region 发送区调整
            SendGB.Location = new Point(ReceiveGB.Location.X, ReceiveGB.Size.Height + ReceiveGB.Location.Y);
            SendGB.Size = new Size(skinTabControl1.Size.Width - ReceiveGB.Location.X, Convert.ToInt32(skinTabControl1.Size.Height - ReceiveGB.Size.Height * 1.15));

            SendBTN.Location = new Point(Convert.ToInt32(SendGB.Size.Width - SendBTN.Size.Width * 1.1)
                                                , Convert.ToInt32(SendGB.Size.Height - SendBTN.Size.Height * 1.1));
            SendClearBTN.Location = new Point(Convert.ToInt32(SendGB.Size.Width - SendClearBTN.Size.Width * 1.1)
                                                , Convert.ToInt32(SendBTN.Location.Y - SendClearBTN.Size.Height * 1.1));

            HexSendCB.Location = new Point(Convert.ToInt32(SendGB.Size.Width - HexSendCB.Size.Width * 1.1)
                                                , Convert.ToInt32(SendClearBTN.Location.Y - HexSendCB.Size.Height * 1.1));

            SendTB.Size = new Size(Convert.ToInt32(SendGB.Size.Width - SendBTN.Size.Width * 1.1 - SendBTN.Size.Height * 0.5)
                                    , SendGB.Size.Height - ReceiveTB.Location.Y - 10);
            #endregion

            #region 图表页调整

            //skinTabControl2.Location = new Point(0, 0);
            skinTabControl2.Size = new Size(skinTabControl1.Size.Width, 85);

            SATest1BTN.Location = new Point(SATest1BTN.Size.Width / 4, (skinTabControl2.Size.Height - SATest1BTN.Size.Width) / 2);
            TestAllSABTN.Location = new Point(SATest1BTN.Location.X * 2 + SATest1BTN.Size.Width, SATest1BTN.Location.Y);

            InfoPrintTB.Size = new System.Drawing.Size(180,
                skinTabControl1.Size.Height - skinTabControl2.Location.Y - skinTabControl2.Size.Height - 36);

            Chart1.Location = new Point(0, skinTabControl2.Size.Height);
            Chart1.Size = new Size(skinTabControl2.Size.Width - InfoPrintTB.Size.Width,
                skinTabControl1.Size.Height - skinTabControl2.Location.Y - skinTabControl2.Size.Height - 36);

            InfoPrintTB.Location = new Point(Chart1.Size.Width, Chart1.Location.Y);
            #endregion
        }
        class TimerExampleState
        {
            public int counter = 0;
            public System.Threading.Timer tmr;
        }

        public UART Uart1 = new UART();
        /// <summary>
        /// 串口接受状态枚举
        /// </summary>
        public enum UARTReceiveStatus
        {
            SerialPortClosed,
            SerialPortOpen,
            DataReceiving,
            DataReceiveFinish,
        }
        UARTReceiveStatus NowUartReceiveStatus = UARTReceiveStatus.SerialPortClosed;
        /// <summary>
        /// 串口发送状态枚举
        /// </summary>
        public enum UARTSendStatus
        {
            SerialPortClosed,
            SerialPortOpen,
            DataSending,
            DataSendFinish
        }
        UARTSendStatus NowUartSendStatus = UARTSendStatus.SerialPortClosed;
        /// <summary>是否正在进行接收标志位</summary>
        Thread getRecevice;
        string strRecieve;
        TimerExampleState s = new TimerExampleState();
        /// <summary>
        /// 用来切换串口接收或者串口不接收
        /// </summary>
        private void SwitchReceiveBTN_Click(object sender, EventArgs e)
        {
            if (SwitchReceiveBTN.Text == "停止接收")
            {
                NowUartReceiveStatus = UARTReceiveStatus.DataReceiveFinish;
            }
            if (NowUartReceiveStatus == UARTReceiveStatus.SerialPortClosed || NowUartSendStatus == UARTSendStatus.SerialPortClosed)
            {
                Uart1.SetSerialPort(PortCB.Text, int.Parse(BaudCB.Text), int.Parse(DataBitsCB.Text), int.Parse(StopBitsCB.Text));

                if (Uart1.SwtichSP(true) == true)//打开成功
                {
                    NowUartReceiveStatus = UARTReceiveStatus.SerialPortOpen;
                    NowUartSendStatus = UARTSendStatus.SerialPortOpen;
                }
                else
                {
                    PortCBFresh();
                }
            }
            if (NowUartReceiveStatus == UARTReceiveStatus.SerialPortOpen)
            {
                Uart1.sp.Encoding = Encoding.GetEncoding("GB2312");
                //使用委托以及多线程进行
                #region 打开多线程定时器
                //创建代理对象TimerCallback，该代理将被定时调用
                TimerCallback timerDelegate = new TimerCallback(TimerRecive);
                //创建一个时间间隔为1s的定时器
                System.Threading.Timer timer = new System.Threading.Timer(timerDelegate, s, 0, 100);
                s.tmr = timer;
                #endregion

                SwitchReceiveBTN.Text = "停止接收";
                NowUartReceiveStatus = UARTReceiveStatus.DataReceiving;
            }
        }
        /// <summary>
        /// 串口接收用定时器函数
        /// </summary>
        /// <param name="state"></param>
        void TimerRecive(Object state)
        {
            TimerExampleState s = (TimerExampleState)state;
            if (NowUartReceiveStatus == UARTReceiveStatus.DataReceiveFinish)
            {
                if (Uart1.SwtichSP(false) == true)//关闭成功
                {
                    SwitchReceiveBTN.Text = "开始接收";
                    NowUartReceiveStatus = UARTReceiveStatus.SerialPortClosed;
                    NowUartSendStatus = UARTSendStatus.SerialPortClosed;
                }
                else
                {
                    MessageBox.Show("关闭串口失败！");
                }

                s.tmr.Dispose();
                s.tmr = null;
                return;
            }
            try
            {
                if (HexCB.Checked == true)
                {
                    strRecieve = "";
                    byte[] Receivebuff = new byte[500];
                    int ReceiveNum = Uart1.sp.Read(Receivebuff, 0, Uart1.sp.BytesToRead);

                    if (ReceiveNum > 0)
                    {
                        for (int i = 0; i < ReceiveNum; i++)
                        {
                            int hexnum1 = Receivebuff[i] / 16;
                            int hexnum2 = Receivebuff[i] % 16;

                            strRecieve += hexnum1.ToString();
                            strRecieve += hexnum2.ToString();
                            strRecieve += " ";

                            //strRecieve += Receivebuff[i];
                        }

                        Console.WriteLine(strRecieve);
                        ReceiveTB.AppendText(strRecieve);
                    }


                    //Console.WriteLine(Receivebuff.ToString());
                }
                else
                { 
                    strRecieve = Uart1.sp.ReadExisting();
                    if(strRecieve != "")
                    {                     
                        Console.WriteLine(strRecieve);
                        ReceiveTB.AppendText(strRecieve);

                        if (ReceiveTB.Text.Length >= 10000)
                        {
                            NowUartReceiveStatus = UARTReceiveStatus.DataReceiveFinish;
                            SwitchReceiveBTN.Text = "开始接收";
                        }
                    }
                }
            }
            catch (Exception ex) { }
            //}
        }
        /// <summary>
        /// 用来将发送区文本框的文本通过串口发送
        /// </summary>
        private void SendBTN_Click(object sender, EventArgs e)
        {
            if (NowUartSendStatus == UARTSendStatus.SerialPortClosed)
            {
                Uart1.SetSerialPort(PortCB.Text, int.Parse(BaudCB.Text), int.Parse(DataBitsCB.Text), int.Parse(StopBitsCB.Text));

                if (Uart1.SwtichSP(true) == true)//打开成功
                {
                    NowUartSendStatus = UARTSendStatus.SerialPortOpen;
                    NowUartReceiveStatus = UARTReceiveStatus.SerialPortOpen;
                }
                else
                {
                    PortCBFresh();
                }
            }
            if (NowUartSendStatus == UARTSendStatus.SerialPortOpen)
            {
                NowUartSendStatus = UARTSendStatus.DataSending;

                string strbuff = SendTB.Text;
                if (HexSendCB.Checked == true)
                {
                    ///检测是否符合格式
                    for (int i = 0; i < strbuff.Length; i+=3)
                    {
                        try
                        {
                            int var1 = Convert.ToInt16(strbuff[i]);
                            if (var1 < 48 || var1 > 57)
                                throw new Exception();
                            var1 = Convert.ToInt16(strbuff[i + 1]);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("不符合Hex的发送格式！\r\n格式为：3A 2B\r\n最后没有空格！！！");
                            return;
                        }
                        if (i + 2 < strbuff.Length && strbuff[i + 2] != ' ')
                        {
                            MessageBox.Show("不符合Hex的发送格式！\r\n格式为：3A 2B\r\n最后没有空格！！！");
                            return; 
                        }
                        
                    }

                    ///正式发送
                    int size = (strbuff.Length + 1) / 3;
                    if ((strbuff.Length + 1) % 3 != 0)
                    {
                        MessageBox.Show("不符合Hex的发送格式！\r\n格式为：3A 2B\r\n最后没有空格！！！");
                        return;  
                    }
                    if (size <= 0)
                    {
                        MessageBox.Show("至少要一个16进制数！");
                        return;  
                    }
                    byte[] sendbuff = new byte[size];
                    for (int i = 0; i < strbuff.Length; i += 3)
                    {
                        byte num1 = Convert.ToByte(Convert.ToByte(strbuff[i] - '0') * 16);
                        byte num2 = Convert.ToByte(Convert.ToByte(strbuff[i + 1] - '0'));
                        sendbuff[i / 3] = Convert.ToByte(num1 + num2);
                    }

                    Uart1.sp.Write(sendbuff, 0, size);
                }
                else
                    Uart1.sp.Write(strbuff);

                NowUartSendStatus = UARTSendStatus.DataSendFinish;
                NowUartSendStatus = UARTSendStatus.SerialPortOpen;
            }
        }
        /// <summary>
        /// 用来清除发送区文本框内的内容
        /// </summary>
        private void SendClearBTN_Click(object sender, EventArgs e)
        {
            SendTB.Text = "";
        }
        /// <summary>
        /// 用来清除接收区文本框内的内容
        /// </summary>
        private void ReceiveClearBTN_Click(object sender, EventArgs e)
        {
            ReceiveTB.Text = "";
        }
        public Queen.QueenSolve NowQueenSolve = new Queen.QueenSolve(Queen.QueenSolve.DistanceCalMethod.ManhattanDistance
                                                         , Queen.QueenSolve.InitResultMethod.Dijkstra, 30);
        DataPointCollection ShowPoint;///用来显示的点集
        DataPointCollection ShowPoint2;///用来显示的点集2

        private void SATest1BTN_Click(object sender, EventArgs e)
        {
            NowQueenSolve = Form1.ThisQueenSolve;
            NowQueenSolve.ChessLocationList = Form1.QueenChessLocation;

            NowQueenSolve.QueenLocationList = new List<Point>();
            for (int i = 0; i < 8; i++)
            {
                NowQueenSolve.QueenLocationList.Add(new Point(i, NowQueenSolve.EightQueenResult[0, i] - 1));
            }

            SimulateAnneal.Annealing.SAMode UsedSAMode = SimulateAnneal.Annealing.SAMode.SA;
            double InitTemp = 0, alpha = 0, SALenght = 0, FSAh = 0;
            int TestNum = 0;
            try 
	        {	        
                InitTemp = Convert.ToDouble(InitTempTB.Text);
                alpha = Convert.ToDouble(AlphaTB.Text);
                SALenght = Convert.ToDouble(SALenghtTB.Text);
                FSAh = Convert.ToDouble(FSAhTB.Text);
                TestNum = Convert.ToInt32(TestNumSetTB.Text);

                if (SAModeSetTB.SelectedIndex == 0)//SA
                    UsedSAMode = SimulateAnneal.Annealing.SAMode.SA;
                else
                    UsedSAMode = SimulateAnneal.Annealing.SAMode.FastSA;

	        }
	        catch (Exception)
	        {
		
		        throw;
	        }

            Chart1.Series[0].ChartType = SeriesChartType.Line;
            Chart1.Series[0].MarkerSize = 0;
            Chart1.Series[0].IsValueShownAsLabel = false;
            Chart1.Series[0].ToolTip = "第#VALX次模拟退火中\r\n局部最短路径为：#VAL";
            //Chart1.ChartAreas[0].AxisY.Maximum = 1;
            //Chart1.ChartAreas[0].AxisY.Minimum = 30;
            Chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            Chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            Chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            Chart1.ChartAreas[0].AxisX.MajorTickMark.Interval = 5;
            Chart1.ChartAreas[0].AxisX.MinorTickMark.Interval = 1;

            Chart1.Series[1].ChartType = SeriesChartType.Line;
            Chart1.Series[1].MarkerSize = 0;
            Chart1.Series[1].IsValueShownAsLabel = false;
            Chart1.Series[1].ToolTip = "第#VALX次模拟退火中\r\n局部最短路径为：#VAL";

            NowQueenSolve.Test_SAParameter(InitTemp, alpha, SALenght, FSAh, UsedSAMode, TestNum);

            ShowPoint.Clear();

            List<double> DisList = NowQueenSolve.TestDisList.ToList();
            List<double> UsedTimeList = NowQueenSolve.TestUsedTime.ToList();

            for (int i = 0; i < NowQueenSolve.TestDisList.Count; i++)
            {
                ShowPoint.Add(new DataPoint(i, DisList[i]));
            }
            ShowPoint2.Clear();
            for (int i = 0; i < NowQueenSolve.TestUsedTime.Count; i++)
            {
                ShowPoint2.Add(new DataPoint(i, UsedTimeList[i]));
            }

            InfoPrintTB.Text = "模拟退火参数：" + System.Environment.NewLine;
            InfoPrintTB.Text += "T0 = " + InitTemp.ToString()+ System.Environment.NewLine;
            InfoPrintTB.Text += "a = " + alpha.ToString() + System.Environment.NewLine;
            InfoPrintTB.Text += "L = " + SALenght.ToString() + System.Environment.NewLine;

        }

        private void TestAllSABTN_Click(object sender, EventArgs e)
        {
            ShowPoint.Clear();

            NowQueenSolve = Form1.ThisQueenSolve;
            NowQueenSolve.ChessLocationList = Form1.QueenChessLocation;

            NowQueenSolve.QueenLocationList = new List<Point>();
            for (int i = 0; i < 8; i++)
            {
                NowQueenSolve.QueenLocationList.Add(new Point(i, NowQueenSolve.EightQueenResult[0, i] - 1));
            }

            double InitTemp = 0, alpha = 0, SALenght = 0;
            try
            {
                InitTemp = Convert.ToDouble(InitTempTB.Text);
                alpha = Convert.ToDouble(AlphaTB.Text);
                SALenght = Convert.ToDouble(SALenghtTB.Text);
            }
            catch (Exception)
            {

                throw;
            }

            Chart1.Series[0].ChartType = SeriesChartType.Point;
            Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.Series[0].MarkerSize = 6;
            Chart1.Series[0].ToolTip = "第#VALX组八皇后的解\r\n寻优后的最短路径为：#VAL";
            Chart1.ChartAreas[0].AxisY.Maximum = 55;
            Chart1.ChartAreas[0].AxisY.Minimum = 30;
            Chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            Chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            Chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 100;
            Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            Chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
            Chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            Chart1.ChartAreas[0].AxisX.MajorTickMark.Interval = 5;
            Chart1.ChartAreas[0].AxisX.MinorTickMark.Interval = 1;


            //NowQueenSolve.InitSA(InitTemp, alpha, SALenght, 0, SimulateAnneal.Annealing.SAMode.SA);
            NowQueenSolve.InitSA(1000, 0.9, 90, 0.1, SimulateAnneal.Annealing.SAMode.FastSA);
            //NowQueenSolve.InitSA(1000, 0.9, 90, 0, SimulateAnneal.Annealing.SAMode.SA);

            List<Point> BestResult_QueenLocation = new List<Point>();
            List<int> MoveSequence = new List<int>();
            double disall = 0;
            MoveSequence = NowQueenSolve.SearchResult_ForOverall(ref disall, ref BestResult_QueenLocation, ShowPoint);

            #region 92组解评估
            List<double> ResultEve = new List<double>();
            ResultEve = NowQueenSolve.QueenResultEvaluation();

            ShowPoint2.Clear();
            for (int i = 0; i < 92; i++)
            {
                ShowPoint2.Add(new DataPoint(i, ResultEve[i]));
            }
            #endregion

            InfoPrintTB.Text = "模拟退火参数：" + System.Environment.NewLine;
            InfoPrintTB.Text += "T0 = " + InitTemp.ToString() + System.Environment.NewLine;
            InfoPrintTB.Text += "a = " + alpha.ToString() + System.Environment.NewLine;
            InfoPrintTB.Text += "L = " + SALenght.ToString() + System.Environment.NewLine;
            InfoPrintTB.Text += "本次最优距离为：" + System.Environment.NewLine;
            InfoPrintTB.Text += disall.ToString() + System.Environment.NewLine;
        }

        private void SAModeSelectCB_Click(object sender, EventArgs e)
        {

        }
    }
}
