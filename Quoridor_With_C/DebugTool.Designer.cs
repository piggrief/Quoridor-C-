namespace DebugToolForm
{
    partial class DebugTool
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            try
            {
                base.Dispose(disposing);
            }
            catch (System.Exception)
            {
                
                //throw;
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugTool));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.skinTabControl1 = new CCWin.SkinControl.SkinTabControl();
            this.PortTabPage = new CCWin.SkinControl.SkinTabPage();
            this.SendGB = new CCWin.SkinControl.SkinGroupBox();
            this.SendTB = new System.Windows.Forms.TextBox();
            this.HexSendCB = new CCWin.SkinControl.SkinCheckBox();
            this.SendClearBTN = new CCWin.SkinControl.SkinButton();
            this.SendBTN = new CCWin.SkinControl.SkinButton();
            this.ReceiveGB = new CCWin.SkinControl.SkinGroupBox();
            this.ReceiveTB = new System.Windows.Forms.TextBox();
            this.HexCB = new CCWin.SkinControl.SkinCheckBox();
            this.ReceiveClearBTN = new CCWin.SkinControl.SkinButton();
            this.SaveDataBTN = new CCWin.SkinControl.SkinButton();
            this.SwitchReceiveBTN = new CCWin.SkinControl.SkinButton();
            this.ChartTabPage = new CCWin.SkinControl.SkinTabPage();
            this.InfoPrintTB = new CCWin.SkinControl.SkinTextBox();
            this.skinTabControl2 = new CCWin.SkinControl.SkinTabControl();
            this.skinTabPage2 = new CCWin.SkinControl.SkinTabPage();
            this.TestAllSABTN = new CCWin.SkinControl.SkinButton();
            this.SATest1BTN = new CCWin.SkinControl.SkinButton();
            this.Chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.CameraTabPage = new CCWin.SkinControl.SkinTabPage();
            this.skinMenuStrip1 = new CCWin.SkinControl.SkinMenuStrip();
            this.串口设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.PortCB = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.BaudCB = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.DataBitsCB = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.StopBitsCB = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.ParityCB = new System.Windows.Forms.ToolStripComboBox();
            this.图表设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.IfSASelctCB = new System.Windows.Forms.ToolStripComboBox();
            this.sA参数设定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.SAModeSetTB = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.InitTempTB = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.AlphaTB = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.SALenghtTB = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.FSAhTB = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.TestNumSetTB = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.InitTempSetTB = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.AlphaSetTB = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.SALenghtSetTB = new System.Windows.Forms.ToolStripTextBox();
            this.skinTabControl1.SuspendLayout();
            this.PortTabPage.SuspendLayout();
            this.SendGB.SuspendLayout();
            this.ReceiveGB.SuspendLayout();
            this.ChartTabPage.SuspendLayout();
            this.skinTabControl2.SuspendLayout();
            this.skinTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            this.skinMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinTabControl1
            // 
            this.skinTabControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.skinTabControl1.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.skinTabControl1.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.skinTabControl1.Controls.Add(this.PortTabPage);
            this.skinTabControl1.Controls.Add(this.ChartTabPage);
            this.skinTabControl1.Controls.Add(this.CameraTabPage);
            this.skinTabControl1.HeadBack = null;
            this.skinTabControl1.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.skinTabControl1.ItemSize = new System.Drawing.Size(70, 36);
            this.skinTabControl1.Location = new System.Drawing.Point(8, 61);
            this.skinTabControl1.Name = "skinTabControl1";
            this.skinTabControl1.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowDown")));
            this.skinTabControl1.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowHover")));
            this.skinTabControl1.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseHover")));
            this.skinTabControl1.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseNormal")));
            this.skinTabControl1.PageDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageDown")));
            this.skinTabControl1.PageHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageHover")));
            this.skinTabControl1.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.skinTabControl1.PageNorml = null;
            this.skinTabControl1.SelectedIndex = 1;
            this.skinTabControl1.Size = new System.Drawing.Size(621, 408);
            this.skinTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.skinTabControl1.TabIndex = 0;
            this.skinTabControl1.SizeChanged += new System.EventHandler(this.skinTabControl1_SizeChanged);
            // 
            // PortTabPage
            // 
            this.PortTabPage.BackColor = System.Drawing.Color.White;
            this.PortTabPage.Controls.Add(this.SendGB);
            this.PortTabPage.Controls.Add(this.ReceiveGB);
            this.PortTabPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PortTabPage.Location = new System.Drawing.Point(0, 36);
            this.PortTabPage.Name = "PortTabPage";
            this.PortTabPage.Size = new System.Drawing.Size(621, 372);
            this.PortTabPage.TabIndex = 0;
            this.PortTabPage.TabItemImage = null;
            this.PortTabPage.Text = "串口助手";
            // 
            // SendGB
            // 
            this.SendGB.BackColor = System.Drawing.Color.Transparent;
            this.SendGB.BorderColor = System.Drawing.Color.SteelBlue;
            this.SendGB.Controls.Add(this.SendTB);
            this.SendGB.Controls.Add(this.HexSendCB);
            this.SendGB.Controls.Add(this.SendClearBTN);
            this.SendGB.Controls.Add(this.SendBTN);
            this.SendGB.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SendGB.ForeColor = System.Drawing.Color.Black;
            this.SendGB.Location = new System.Drawing.Point(3, 204);
            this.SendGB.Name = "SendGB";
            this.SendGB.RectBackColor = System.Drawing.Color.White;
            this.SendGB.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.SendGB.Size = new System.Drawing.Size(615, 165);
            this.SendGB.TabIndex = 1;
            this.SendGB.TabStop = false;
            this.SendGB.Text = "发送助手";
            this.SendGB.TitleBorderColor = System.Drawing.Color.Transparent;
            this.SendGB.TitleRectBackColor = System.Drawing.Color.White;
            this.SendGB.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // SendTB
            // 
            this.SendTB.Location = new System.Drawing.Point(6, 32);
            this.SendTB.Multiline = true;
            this.SendTB.Name = "SendTB";
            this.SendTB.Size = new System.Drawing.Size(446, 115);
            this.SendTB.TabIndex = 7;
            // 
            // HexSendCB
            // 
            this.HexSendCB.AutoSize = true;
            this.HexSendCB.BackColor = System.Drawing.Color.Transparent;
            this.HexSendCB.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.HexSendCB.DownBack = null;
            this.HexSendCB.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HexSendCB.Location = new System.Drawing.Point(485, 37);
            this.HexSendCB.MouseBack = null;
            this.HexSendCB.Name = "HexSendCB";
            this.HexSendCB.NormlBack = null;
            this.HexSendCB.SelectedDownBack = null;
            this.HexSendCB.SelectedMouseBack = null;
            this.HexSendCB.SelectedNormlBack = null;
            this.HexSendCB.Size = new System.Drawing.Size(127, 29);
            this.HexSendCB.TabIndex = 5;
            this.HexSendCB.Text = "以Hex发送";
            this.HexSendCB.UseVisualStyleBackColor = false;
            // 
            // SendClearBTN
            // 
            this.SendClearBTN.BackColor = System.Drawing.Color.Transparent;
            this.SendClearBTN.BaseColor = System.Drawing.Color.Silver;
            this.SendClearBTN.BorderColor = System.Drawing.Color.Transparent;
            this.SendClearBTN.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.SendClearBTN.DownBack = null;
            this.SendClearBTN.Location = new System.Drawing.Point(485, 72);
            this.SendClearBTN.MouseBack = null;
            this.SendClearBTN.Name = "SendClearBTN";
            this.SendClearBTN.NormlBack = null;
            this.SendClearBTN.Size = new System.Drawing.Size(124, 36);
            this.SendClearBTN.TabIndex = 6;
            this.SendClearBTN.Text = "清除";
            this.SendClearBTN.UseVisualStyleBackColor = false;
            this.SendClearBTN.Click += new System.EventHandler(this.SendClearBTN_Click);
            // 
            // SendBTN
            // 
            this.SendBTN.BackColor = System.Drawing.Color.Transparent;
            this.SendBTN.BaseColor = System.Drawing.Color.Silver;
            this.SendBTN.BorderColor = System.Drawing.Color.Transparent;
            this.SendBTN.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.SendBTN.DownBack = null;
            this.SendBTN.Location = new System.Drawing.Point(485, 123);
            this.SendBTN.MouseBack = null;
            this.SendBTN.Name = "SendBTN";
            this.SendBTN.NormlBack = null;
            this.SendBTN.Size = new System.Drawing.Size(124, 36);
            this.SendBTN.TabIndex = 5;
            this.SendBTN.Text = "发送";
            this.SendBTN.UseVisualStyleBackColor = false;
            this.SendBTN.Click += new System.EventHandler(this.SendBTN_Click);
            // 
            // ReceiveGB
            // 
            this.ReceiveGB.BackColor = System.Drawing.Color.Transparent;
            this.ReceiveGB.BorderColor = System.Drawing.Color.SteelBlue;
            this.ReceiveGB.Controls.Add(this.ReceiveTB);
            this.ReceiveGB.Controls.Add(this.HexCB);
            this.ReceiveGB.Controls.Add(this.ReceiveClearBTN);
            this.ReceiveGB.Controls.Add(this.SaveDataBTN);
            this.ReceiveGB.Controls.Add(this.SwitchReceiveBTN);
            this.ReceiveGB.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ReceiveGB.ForeColor = System.Drawing.Color.Black;
            this.ReceiveGB.Location = new System.Drawing.Point(3, 3);
            this.ReceiveGB.Name = "ReceiveGB";
            this.ReceiveGB.RectBackColor = System.Drawing.Color.White;
            this.ReceiveGB.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.ReceiveGB.Size = new System.Drawing.Size(615, 195);
            this.ReceiveGB.TabIndex = 0;
            this.ReceiveGB.TabStop = false;
            this.ReceiveGB.Text = "接收助手";
            this.ReceiveGB.TitleBorderColor = System.Drawing.Color.Transparent;
            this.ReceiveGB.TitleRectBackColor = System.Drawing.Color.White;
            this.ReceiveGB.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // ReceiveTB
            // 
            this.ReceiveTB.Location = new System.Drawing.Point(6, 33);
            this.ReceiveTB.Multiline = true;
            this.ReceiveTB.Name = "ReceiveTB";
            this.ReceiveTB.Size = new System.Drawing.Size(446, 116);
            this.ReceiveTB.TabIndex = 5;
            // 
            // HexCB
            // 
            this.HexCB.AutoSize = true;
            this.HexCB.BackColor = System.Drawing.Color.Transparent;
            this.HexCB.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.HexCB.DownBack = null;
            this.HexCB.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HexCB.Location = new System.Drawing.Point(485, 30);
            this.HexCB.MouseBack = null;
            this.HexCB.Name = "HexCB";
            this.HexCB.NormlBack = null;
            this.HexCB.SelectedDownBack = null;
            this.HexCB.SelectedMouseBack = null;
            this.HexCB.SelectedNormlBack = null;
            this.HexCB.Size = new System.Drawing.Size(127, 29);
            this.HexCB.TabIndex = 4;
            this.HexCB.Text = "以Hex显示";
            this.HexCB.UseVisualStyleBackColor = false;
            // 
            // ReceiveClearBTN
            // 
            this.ReceiveClearBTN.BackColor = System.Drawing.Color.Transparent;
            this.ReceiveClearBTN.BaseColor = System.Drawing.Color.Silver;
            this.ReceiveClearBTN.BorderColor = System.Drawing.Color.Transparent;
            this.ReceiveClearBTN.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.ReceiveClearBTN.DownBack = null;
            this.ReceiveClearBTN.Location = new System.Drawing.Point(485, 149);
            this.ReceiveClearBTN.MouseBack = null;
            this.ReceiveClearBTN.Name = "ReceiveClearBTN";
            this.ReceiveClearBTN.NormlBack = null;
            this.ReceiveClearBTN.Size = new System.Drawing.Size(124, 36);
            this.ReceiveClearBTN.TabIndex = 3;
            this.ReceiveClearBTN.Text = "清除数据";
            this.ReceiveClearBTN.UseVisualStyleBackColor = false;
            this.ReceiveClearBTN.Click += new System.EventHandler(this.ReceiveClearBTN_Click);
            // 
            // SaveDataBTN
            // 
            this.SaveDataBTN.BackColor = System.Drawing.Color.Transparent;
            this.SaveDataBTN.BaseColor = System.Drawing.Color.Silver;
            this.SaveDataBTN.BorderColor = System.Drawing.Color.Transparent;
            this.SaveDataBTN.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.SaveDataBTN.DownBack = null;
            this.SaveDataBTN.Location = new System.Drawing.Point(485, 107);
            this.SaveDataBTN.MouseBack = null;
            this.SaveDataBTN.Name = "SaveDataBTN";
            this.SaveDataBTN.NormlBack = null;
            this.SaveDataBTN.Size = new System.Drawing.Size(124, 36);
            this.SaveDataBTN.TabIndex = 2;
            this.SaveDataBTN.Text = "保存数据";
            this.SaveDataBTN.UseVisualStyleBackColor = false;
            // 
            // SwitchReceiveBTN
            // 
            this.SwitchReceiveBTN.BackColor = System.Drawing.Color.Transparent;
            this.SwitchReceiveBTN.BaseColor = System.Drawing.Color.Silver;
            this.SwitchReceiveBTN.BorderColor = System.Drawing.Color.Transparent;
            this.SwitchReceiveBTN.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.SwitchReceiveBTN.DownBack = null;
            this.SwitchReceiveBTN.Location = new System.Drawing.Point(485, 65);
            this.SwitchReceiveBTN.MouseBack = null;
            this.SwitchReceiveBTN.Name = "SwitchReceiveBTN";
            this.SwitchReceiveBTN.NormlBack = null;
            this.SwitchReceiveBTN.Size = new System.Drawing.Size(124, 36);
            this.SwitchReceiveBTN.TabIndex = 1;
            this.SwitchReceiveBTN.Text = "开始接收";
            this.SwitchReceiveBTN.UseVisualStyleBackColor = false;
            this.SwitchReceiveBTN.Click += new System.EventHandler(this.SwitchReceiveBTN_Click);
            // 
            // ChartTabPage
            // 
            this.ChartTabPage.BackColor = System.Drawing.Color.White;
            this.ChartTabPage.Controls.Add(this.InfoPrintTB);
            this.ChartTabPage.Controls.Add(this.skinTabControl2);
            this.ChartTabPage.Controls.Add(this.Chart1);
            this.ChartTabPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartTabPage.Location = new System.Drawing.Point(0, 36);
            this.ChartTabPage.Name = "ChartTabPage";
            this.ChartTabPage.Size = new System.Drawing.Size(621, 372);
            this.ChartTabPage.TabIndex = 1;
            this.ChartTabPage.TabItemImage = null;
            this.ChartTabPage.Text = "退火图表";
            // 
            // InfoPrintTB
            // 
            this.InfoPrintTB.BackColor = System.Drawing.Color.Transparent;
            this.InfoPrintTB.DownBack = null;
            this.InfoPrintTB.Icon = null;
            this.InfoPrintTB.IconIsButton = false;
            this.InfoPrintTB.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.InfoPrintTB.IsPasswordChat = '\0';
            this.InfoPrintTB.IsSystemPasswordChar = false;
            this.InfoPrintTB.Lines = new string[] {
        "第一次的解为"};
            this.InfoPrintTB.Location = new System.Drawing.Point(442, 110);
            this.InfoPrintTB.Margin = new System.Windows.Forms.Padding(0);
            this.InfoPrintTB.MaxLength = 32767;
            this.InfoPrintTB.MinimumSize = new System.Drawing.Size(28, 28);
            this.InfoPrintTB.MouseBack = null;
            this.InfoPrintTB.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.InfoPrintTB.Multiline = true;
            this.InfoPrintTB.Name = "InfoPrintTB";
            this.InfoPrintTB.NormlBack = null;
            this.InfoPrintTB.Padding = new System.Windows.Forms.Padding(5);
            this.InfoPrintTB.ReadOnly = true;
            this.InfoPrintTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.InfoPrintTB.Size = new System.Drawing.Size(176, 244);
            // 
            // 
            // 
            this.InfoPrintTB.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InfoPrintTB.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoPrintTB.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.InfoPrintTB.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.InfoPrintTB.SkinTxt.Multiline = true;
            this.InfoPrintTB.SkinTxt.Name = "BaseText";
            this.InfoPrintTB.SkinTxt.ReadOnly = true;
            this.InfoPrintTB.SkinTxt.Size = new System.Drawing.Size(166, 234);
            this.InfoPrintTB.SkinTxt.TabIndex = 0;
            this.InfoPrintTB.SkinTxt.Text = "第一次的解为";
            this.InfoPrintTB.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.InfoPrintTB.SkinTxt.WaterText = "";
            this.InfoPrintTB.TabIndex = 4;
            this.InfoPrintTB.Text = "第一次的解为";
            this.InfoPrintTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.InfoPrintTB.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.InfoPrintTB.WaterText = "";
            this.InfoPrintTB.WordWrap = true;
            // 
            // skinTabControl2
            // 
            this.skinTabControl2.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.skinTabControl2.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.skinTabControl2.Controls.Add(this.skinTabPage2);
            this.skinTabControl2.HeadBack = null;
            this.skinTabControl2.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.skinTabControl2.ItemSize = new System.Drawing.Size(70, 36);
            this.skinTabControl2.Location = new System.Drawing.Point(3, 0);
            this.skinTabControl2.Name = "skinTabControl2";
            this.skinTabControl2.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl2.PageArrowDown")));
            this.skinTabControl2.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl2.PageArrowHover")));
            this.skinTabControl2.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl2.PageCloseHover")));
            this.skinTabControl2.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("skinTabControl2.PageCloseNormal")));
            this.skinTabControl2.PageDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl2.PageDown")));
            this.skinTabControl2.PageHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl2.PageHover")));
            this.skinTabControl2.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.skinTabControl2.PageNorml = null;
            this.skinTabControl2.SelectedIndex = 0;
            this.skinTabControl2.Size = new System.Drawing.Size(618, 84);
            this.skinTabControl2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.skinTabControl2.TabIndex = 3;
            // 
            // skinTabPage2
            // 
            this.skinTabPage2.BackColor = System.Drawing.Color.White;
            this.skinTabPage2.Controls.Add(this.TestAllSABTN);
            this.skinTabPage2.Controls.Add(this.SATest1BTN);
            this.skinTabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPage2.Location = new System.Drawing.Point(0, 36);
            this.skinTabPage2.Name = "skinTabPage2";
            this.skinTabPage2.Size = new System.Drawing.Size(618, 48);
            this.skinTabPage2.TabIndex = 1;
            this.skinTabPage2.TabItemImage = null;
            this.skinTabPage2.Text = "单次SA";
            // 
            // TestAllSABTN
            // 
            this.TestAllSABTN.BackColor = System.Drawing.Color.Transparent;
            this.TestAllSABTN.BaseColor = System.Drawing.Color.Silver;
            this.TestAllSABTN.BorderColor = System.Drawing.Color.Transparent;
            this.TestAllSABTN.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.TestAllSABTN.DownBack = null;
            this.TestAllSABTN.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TestAllSABTN.Location = new System.Drawing.Point(130, 3);
            this.TestAllSABTN.MouseBack = null;
            this.TestAllSABTN.Name = "TestAllSABTN";
            this.TestAllSABTN.NormlBack = null;
            this.TestAllSABTN.Size = new System.Drawing.Size(124, 36);
            this.TestAllSABTN.TabIndex = 3;
            this.TestAllSABTN.Text = "测试92组解";
            this.TestAllSABTN.UseVisualStyleBackColor = false;
            this.TestAllSABTN.Click += new System.EventHandler(this.TestAllSABTN_Click);
            // 
            // SATest1BTN
            // 
            this.SATest1BTN.BackColor = System.Drawing.Color.Transparent;
            this.SATest1BTN.BaseColor = System.Drawing.Color.Silver;
            this.SATest1BTN.BorderColor = System.Drawing.Color.Transparent;
            this.SATest1BTN.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.SATest1BTN.DownBack = null;
            this.SATest1BTN.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SATest1BTN.Location = new System.Drawing.Point(0, 3);
            this.SATest1BTN.MouseBack = null;
            this.SATest1BTN.Name = "SATest1BTN";
            this.SATest1BTN.NormlBack = null;
            this.SATest1BTN.Size = new System.Drawing.Size(124, 36);
            this.SATest1BTN.TabIndex = 2;
            this.SATest1BTN.Text = "测试当前参数";
            this.SATest1BTN.UseVisualStyleBackColor = false;
            this.SATest1BTN.Click += new System.EventHandler(this.SATest1BTN_Click);
            // 
            // Chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.Chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Chart1.Legends.Add(legend1);
            this.Chart1.Location = new System.Drawing.Point(0, 99);
            this.Chart1.Name = "Chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Font = new System.Drawing.Font("微软雅黑", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.IsValueShownAsLabel = true;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.MarkerBorderColor = System.Drawing.Color.Black;
            series1.MarkerColor = System.Drawing.Color.Red;
            series1.MarkerSize = 6;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series1.Name = "Series1";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.IsValueShownAsLabel = true;
            series2.Legend = "Legend1";
            series2.MarkerBorderColor = System.Drawing.Color.Black;
            series2.MarkerColor = System.Drawing.Color.Green;
            series2.MarkerSize = 6;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Name = "Series2";
            this.Chart1.Series.Add(series1);
            this.Chart1.Series.Add(series2);
            this.Chart1.Size = new System.Drawing.Size(618, 274);
            this.Chart1.TabIndex = 0;
            this.Chart1.Text = "chart1";
            // 
            // CameraTabPage
            // 
            this.CameraTabPage.BackColor = System.Drawing.Color.White;
            this.CameraTabPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CameraTabPage.Location = new System.Drawing.Point(0, 36);
            this.CameraTabPage.Name = "CameraTabPage";
            this.CameraTabPage.Size = new System.Drawing.Size(621, 372);
            this.CameraTabPage.TabIndex = 2;
            this.CameraTabPage.TabItemImage = null;
            this.CameraTabPage.Text = "摄像头";
            // 
            // skinMenuStrip1
            // 
            this.skinMenuStrip1.Arrow = System.Drawing.Color.Black;
            this.skinMenuStrip1.Back = System.Drawing.Color.White;
            this.skinMenuStrip1.BackRadius = 4;
            this.skinMenuStrip1.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.skinMenuStrip1.Base = System.Drawing.Color.Silver;
            this.skinMenuStrip1.BaseFore = System.Drawing.Color.Black;
            this.skinMenuStrip1.BaseForeAnamorphosis = false;
            this.skinMenuStrip1.BaseForeAnamorphosisBorder = 4;
            this.skinMenuStrip1.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.skinMenuStrip1.BaseHoverFore = System.Drawing.Color.White;
            this.skinMenuStrip1.BaseItemAnamorphosis = true;
            this.skinMenuStrip1.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.BaseItemBorderShow = true;
            this.skinMenuStrip1.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("skinMenuStrip1.BaseItemDown")));
            this.skinMenuStrip1.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("skinMenuStrip1.BaseItemMouse")));
            this.skinMenuStrip1.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.BaseItemRadius = 4;
            this.skinMenuStrip1.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinMenuStrip1.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinMenuStrip1.Fore = System.Drawing.Color.Black;
            this.skinMenuStrip1.HoverFore = System.Drawing.Color.White;
            this.skinMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.skinMenuStrip1.ItemAnamorphosis = true;
            this.skinMenuStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.ItemBorderShow = true;
            this.skinMenuStrip1.ItemHover = System.Drawing.Color.LightCoral;
            this.skinMenuStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.ItemRadius = 4;
            this.skinMenuStrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.串口设置ToolStripMenuItem,
            this.图表设置ToolStripMenuItem});
            this.skinMenuStrip1.Location = new System.Drawing.Point(8, 30);
            this.skinMenuStrip1.Name = "skinMenuStrip1";
            this.skinMenuStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinMenuStrip1.Size = new System.Drawing.Size(624, 28);
            this.skinMenuStrip1.SkinAllColor = true;
            this.skinMenuStrip1.TabIndex = 1;
            this.skinMenuStrip1.Text = "skinMenuStrip1";
            this.skinMenuStrip1.TitleAnamorphosis = true;
            this.skinMenuStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinMenuStrip1.TitleRadius = 4;
            this.skinMenuStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // 串口设置ToolStripMenuItem
            // 
            this.串口设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
            this.串口设置ToolStripMenuItem.Name = "串口设置ToolStripMenuItem";
            this.串口设置ToolStripMenuItem.Size = new System.Drawing.Size(100, 24);
            this.串口设置ToolStripMenuItem.Text = "串口设置(&S)";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PortCB});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(129, 26);
            this.toolStripMenuItem1.Text = "端口号";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // PortCB
            // 
            this.PortCB.Name = "PortCB";
            this.PortCB.Size = new System.Drawing.Size(121, 28);
            this.PortCB.Click += new System.EventHandler(this.PortCB_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BaudCB});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(129, 26);
            this.toolStripMenuItem2.Text = "波特率";
            // 
            // BaudCB
            // 
            this.BaudCB.Items.AddRange(new object[] {
            "256000",
            "128000",
            "115200",
            "57600",
            "56000",
            "38400",
            "19200",
            "14400",
            "9600",
            "4800",
            "2400"});
            this.BaudCB.Name = "BaudCB";
            this.BaudCB.Size = new System.Drawing.Size(121, 28);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataBitsCB});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(129, 26);
            this.toolStripMenuItem3.Text = "数据位";
            // 
            // DataBitsCB
            // 
            this.DataBitsCB.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.DataBitsCB.Name = "DataBitsCB";
            this.DataBitsCB.Size = new System.Drawing.Size(121, 28);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StopBitsCB});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(129, 26);
            this.toolStripMenuItem4.Text = "停止位";
            // 
            // StopBitsCB
            // 
            this.StopBitsCB.Items.AddRange(new object[] {
            "1"});
            this.StopBitsCB.Name = "StopBitsCB";
            this.StopBitsCB.Size = new System.Drawing.Size(121, 28);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ParityCB});
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(129, 26);
            this.toolStripMenuItem5.Text = "校验位";
            // 
            // ParityCB
            // 
            this.ParityCB.Items.AddRange(new object[] {
            "无"});
            this.ParityCB.Name = "ParityCB";
            this.ParityCB.Size = new System.Drawing.Size(121, 28);
            // 
            // 图表设置ToolStripMenuItem
            // 
            this.图表设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem6,
            this.sA参数设定ToolStripMenuItem,
            this.toolStripMenuItem15});
            this.图表设置ToolStripMenuItem.Name = "图表设置ToolStripMenuItem";
            this.图表设置ToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.图表设置ToolStripMenuItem.Text = "图表设置(&C)";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IfSASelctCB});
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem6.Text = "用途";
            // 
            // IfSASelctCB
            // 
            this.IfSASelctCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IfSASelctCB.Items.AddRange(new object[] {
            "模拟退火测试",
            "其它"});
            this.IfSASelctCB.Name = "IfSASelctCB";
            this.IfSASelctCB.Size = new System.Drawing.Size(121, 28);
            // 
            // sA参数设定ToolStripMenuItem
            // 
            this.sA参数设定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem14,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13});
            this.sA参数设定ToolStripMenuItem.Name = "sA参数设定ToolStripMenuItem";
            this.sA参数设定ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.sA参数设定ToolStripMenuItem.Text = "SA参数设定";
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SAModeSetTB});
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem14.Text = "退火方式";
            // 
            // SAModeSetTB
            // 
            this.SAModeSetTB.Items.AddRange(new object[] {
            "模拟退火(SA)",
            "快速模拟退火(FSA)"});
            this.SAModeSetTB.Name = "SAModeSetTB";
            this.SAModeSetTB.Size = new System.Drawing.Size(121, 28);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InitTempTB});
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem10.Text = "初始温度";
            // 
            // InitTempTB
            // 
            this.InitTempTB.Name = "InitTempTB";
            this.InitTempTB.Size = new System.Drawing.Size(100, 27);
            this.InitTempTB.Text = "1000";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AlphaTB});
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem11.Text = "衰减常数";
            // 
            // AlphaTB
            // 
            this.AlphaTB.Name = "AlphaTB";
            this.AlphaTB.Size = new System.Drawing.Size(100, 27);
            this.AlphaTB.Text = "0.9";
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SALenghtTB});
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem12.Text = "迭代长度";
            // 
            // SALenghtTB
            // 
            this.SALenghtTB.Name = "SALenghtTB";
            this.SALenghtTB.Size = new System.Drawing.Size(100, 27);
            this.SALenghtTB.Text = "30";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FSAhTB});
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem13.Text = "FSA-h";
            // 
            // FSAhTB
            // 
            this.FSAhTB.Name = "FSAhTB";
            this.FSAhTB.Size = new System.Drawing.Size(100, 27);
            this.FSAhTB.Text = "5";
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem16});
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem15.Text = "测试设置";
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TestNumSetTB});
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(174, 26);
            this.toolStripMenuItem16.Text = "参数测试次数";
            // 
            // TestNumSetTB
            // 
            this.TestNumSetTB.Name = "TestNumSetTB";
            this.TestNumSetTB.Size = new System.Drawing.Size(100, 27);
            this.TestNumSetTB.Text = "50";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InitTempSetTB});
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(144, 26);
            this.toolStripMenuItem7.Text = "初始温度";
            // 
            // InitTempSetTB
            // 
            this.InitTempSetTB.MaxLength = 20;
            this.InitTempSetTB.Name = "InitTempSetTB";
            this.InitTempSetTB.Size = new System.Drawing.Size(100, 27);
            this.InitTempSetTB.Text = "10000";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AlphaSetTB});
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(144, 26);
            this.toolStripMenuItem8.Text = "衰减常数";
            // 
            // AlphaSetTB
            // 
            this.AlphaSetTB.Name = "AlphaSetTB";
            this.AlphaSetTB.Size = new System.Drawing.Size(100, 27);
            this.AlphaSetTB.Text = "0.99";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SALenghtSetTB});
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(144, 26);
            this.toolStripMenuItem9.Text = "迭代长度";
            // 
            // SALenghtSetTB
            // 
            this.SALenghtSetTB.Name = "SALenghtSetTB";
            this.SALenghtSetTB.Size = new System.Drawing.Size(100, 27);
            this.SALenghtSetTB.Text = "100";
            // 
            // DebugTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.skinTabControl1);
            this.Controls.Add(this.skinMenuStrip1);
            this.MainMenuStrip = this.skinMenuStrip1;
            this.Name = "DebugTool";
            this.Text = "调试助手 by上海海事大学对弈创意组";
            this.Load += new System.EventHandler(this.DebugTool_Load);
            this.SizeChanged += new System.EventHandler(this.DebugTool_SizeChanged);
            this.Resize += new System.EventHandler(this.DebugTool_Resize);
            this.skinTabControl1.ResumeLayout(false);
            this.PortTabPage.ResumeLayout(false);
            this.SendGB.ResumeLayout(false);
            this.SendGB.PerformLayout();
            this.ReceiveGB.ResumeLayout(false);
            this.ReceiveGB.PerformLayout();
            this.ChartTabPage.ResumeLayout(false);
            this.skinTabControl2.ResumeLayout(false);
            this.skinTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            this.skinMenuStrip1.ResumeLayout(false);
            this.skinMenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinTabControl skinTabControl1;
        private CCWin.SkinControl.SkinMenuStrip skinMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 串口设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripComboBox PortCB;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripComboBox BaudCB;
        private System.Windows.Forms.ToolStripComboBox DataBitsCB;
        private System.Windows.Forms.ToolStripComboBox StopBitsCB;
        private System.Windows.Forms.ToolStripComboBox ParityCB;
        private CCWin.SkinControl.SkinTabPage PortTabPage;
        private CCWin.SkinControl.SkinGroupBox ReceiveGB;
        private CCWin.SkinControl.SkinGroupBox SendGB;
        private CCWin.SkinControl.SkinButton ReceiveClearBTN;
        private CCWin.SkinControl.SkinButton SaveDataBTN;
        private CCWin.SkinControl.SkinButton SwitchReceiveBTN;
        private CCWin.SkinControl.SkinCheckBox HexCB;
        private CCWin.SkinControl.SkinButton SendClearBTN;
        private CCWin.SkinControl.SkinButton SendBTN;
        private CCWin.SkinControl.SkinCheckBox HexSendCB;
        private System.Windows.Forms.TextBox SendTB;
        private System.Windows.Forms.TextBox ReceiveTB;
        private CCWin.SkinControl.SkinTabPage ChartTabPage;
        public System.Windows.Forms.DataVisualization.Charting.Chart Chart1;
        private CCWin.SkinControl.SkinButton SATest1BTN;
        private CCWin.SkinControl.SkinTabControl skinTabControl2;
        private CCWin.SkinControl.SkinTabPage skinTabPage2;
        private System.Windows.Forms.ToolStripMenuItem 图表设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripComboBox IfSASelctCB;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripTextBox InitTempSetTB;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripTextBox AlphaSetTB;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripTextBox SALenghtSetTB;
        private System.Windows.Forms.ToolStripMenuItem sA参数设定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripTextBox InitTempTB;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripTextBox AlphaTB;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripTextBox SALenghtTB;
        private CCWin.SkinControl.SkinTextBox InfoPrintTB;
        private CCWin.SkinControl.SkinButton TestAllSABTN;
        private CCWin.SkinControl.SkinTabPage CameraTabPage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
        private System.Windows.Forms.ToolStripTextBox TestNumSetTB;
        private System.Windows.Forms.ToolStripTextBox FSAhTB;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripComboBox SAModeSetTB;
    }
}