using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using Modbus;
using System.Threading;

namespace MasterTCP
{
    public partial class MasterTCP : Form
    {
        
        // Crete instance of modbus serial RTU (replace COMx with a free serial port - ex. COM5)
        ModbusMasterTCP mm = new ModbusMasterTCP("192.168.0.1", 502);
        // Exec the connection
      
        public MasterTCP()
        {
            InitializeComponent();
            textBox1.Text = "192.168.0.1";
            textBox5.Text = "1";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Read and write some registers on RTU n. 5
            mm.WriteSingleRegister(byte.Parse(textBox5.Text), 4,ushort.Parse(textBox2.Text));
            mm.WriteSingleCoil(byte.Parse(textBox5.Text), 2, checkBox1.Checked);
            textBox3.Text =mm.ReadHoldingRegisters(byte.Parse(textBox5.Text), 4, 1).First().ToString("D5");
            textBox4.Text =mm.ReadCoils(byte.Parse(textBox5.Text), 2, 1).First().ToString();
            // Exec the cicle each 2 seconds
        }
        /// <summary>
        /// 延时函数
        /// </summary>
        /// <param name="secend"></param>
        public void delayTime(double secend)
        {
            DateTime tempTime = DateTime.Now;
            while (tempTime.AddSeconds(secend).CompareTo(DateTime.Now) > 0)
            {
                Application.DoEvents();//待定，空执行
            }
        }
        private void MasterTCP_FormClosed(object sender, FormClosedEventArgs e)
        { 
            //mm.Disconnect();    
        }
/// <summary>
/// 连接按钮
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if(button3.Text=="连接")
            {
                mm = new ModbusMasterTCP(textBox1.Text, 502);
                mm.Connect();
                button3.Text ="断开";
            }
            else
            {
                mm.Disconnect();
                button3.Text = "连接";
            }
            
        }

        private void MasterTCP_Load(object sender, EventArgs e)
        {

        }
    }
}
