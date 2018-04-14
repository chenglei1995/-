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
        bool ifrun = true;
        bool coilbool = false;
        public MasterTCP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte unit_id = 1;
            // Crete instance of modbus serial RTU (replace COMx with a free serial port - ex. COM5)
            ModbusMasterTCP mm = new ModbusMasterTCP("192.168.0.2",502);
            // Exec the connection
            mm.Connect();
            // Read and write some registers on RTU n. 5
            Random rnd = new Random();
            
            while (ifrun)
            {
                textBox1.Text = "Holding register n.1  : " + mm.ReadHoldingRegisters(unit_id, 0, 1).First().ToString("D5") +
                     "Input register   n.41 : " + mm.ReadInputRegisters(unit_id, 40, 1).First().ToString("D5") +
                     "Coil register    n 23 : " + mm.ReadCoils(unit_id, 22, 1).First().ToString();
                mm.WriteSingleRegister(unit_id, 4, (ushort)rnd.Next(ushort.MinValue, ushort.MaxValue));
                textBox2.Text = "Holding register n.5  : " + mm.ReadHoldingRegisters(unit_id, 4, 1).First().ToString("D5");
                mm.WriteSingleCoil(unit_id, 2,coilbool=!coilbool);
                textBox3.Text = "Coil register    n.3  : " + mm.ReadCoils(unit_id, 2, 1).First().ToString();
                // Exec the cicle each 2 seconds
                delayTime(2);
            }
            mm.Disconnect();
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

        private void button2_Click(object sender, EventArgs e)
        {
            ifrun = false;
        }
    }
}
