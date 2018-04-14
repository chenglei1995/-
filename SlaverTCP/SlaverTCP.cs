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
using System.Net;
using System.Net.Sockets;

namespace SlaverTCP
{
    public partial class SlaverTCP : Form
    {
        bool ifrun = true;
        public SlaverTCP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte unit_id = 1;
            // Created datastore for unit ID 1
            Datastore ds = new Datastore(unit_id);
            // Crete instance of modbus serial RTU (replace COMx with a free serial port - ex. COM5)
            ModbusSlaveTCP ms = new ModbusSlaveTCP(new Datastore[] { ds }, IPAddress.Parse("192.168.0.2"),502);
            // Start listen
            ms.StartListen();
            // Print and write some registers...
            Random rnd = new Random();
            while (ifrun)
            {
                textBox1.Text = "Holding register n.1  : " + ms.ModbusDB.Single(x => x.UnitID == unit_id).HoldingRegisters[0].ToString("D5") +
                    "Holding register n.41 : " + ms.ModbusDB.Single(x => x.UnitID == unit_id).HoldingRegisters[40].ToString("D5") +
                    "Coil register    n.23 : " + ms.ModbusDB.Single(x => x.UnitID == unit_id).Coils[22].ToString();
               
                textBox2.Text = "Holding register n.5  : " + ms.ModbusDB.Single(x => x.UnitID == unit_id).HoldingRegisters[4].ToString("D5");
                
                textBox3.Text = "Coil register    n.3 : " + ms.ModbusDB.Single(x => x.UnitID == unit_id).Coils[2].ToString();
                // Exec the cicle each 2 seconds
                delayTime(2);
            }
            ms.StopListen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ifrun = false;
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
    }
  
}
