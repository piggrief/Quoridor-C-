using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace UartComunication
{
    /// <summary>
    /// 串口通信类
    /// </summary>
    public class UART
    {
        public SerialPort sp = new SerialPort();//实例化串口通讯类
        public int ReadTimeLimit = 500;//读取数据的超时时间，引发ReadExisting异常
        public string PortNameBuff = "";
        /// <summary>
        /// 设置串口函数，即给sp字段赋值
        /// </summary>
        public void SetSerialPort(string PortName, int BaudRate, int DataBits, int StopBits)
        {
            PortNameBuff = PortName;
            if (PortName != "")
            { 
                sp.PortName = PortName; 
            }
            sp.BaudRate = BaudRate;
            sp.DataBits = DataBits;
            sp.StopBits = (StopBits)StopBits;
            sp.ReadTimeout = ReadTimeLimit;
        }
        /// <summary>
        /// 开关串口状态
        /// </summary>
        /// <param name="IsOpenSp">是否打开串口，true表示打开串口操作</param>
        public bool SwtichSP(bool IsOpenSp)
        {
            if (IsOpenSp == true)
            {
                if (PortNameBuff != "")
                {
                    try
                    {
                        if (sp.IsOpen)
                        {
                            sp.Close();
                            sp.Open();//打开串口
                        }
                        else
                        {
                            sp.Open();//打开串口
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误：" + ex.Message, "C#串口通信");
                    }
                }
                else
                {
                    MessageBox.Show("请先设置串口！", "RS232串口通信");
                    return false;
                }
            }
            else
            {
                if (sp.IsOpen)
                    sp.Close();
            }
            return true;
        }
    }
}
