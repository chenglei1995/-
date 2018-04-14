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

namespace ModbusSlaverSearial
{

    public partial class Slaver : Form
    {
        public Slaver()
        {
            InitializeComponent();
        }

        private void Slaver_Load(object sender, EventArgs e)
        {
            
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

        private void button1_Click(object sender, EventArgs e)
        {
            byte unit_id = 1;
            // Created datastore for unit ID 1
            Datastore ds = new Datastore(unit_id);
            // Crete instance of modbus serial RTU (replace COMx with a free serial port - ex. COM5)
            ModbusSlaveSerial ms = new ModbusSlaveSerial(new Datastore[] { ds }, ModbusSerialType.RTU, "COM1", 9600, 8, Parity.Even, StopBits.One, Handshake.None);
            // Start listen
            ms.StartListen();
            // Print and write some registers...
            Random rnd = new Random();
            while (true)
            {
                textBox1.Text = "Holding register n.1  : " + ms.ModbusDB.Single(x => x.UnitID == unit_id).HoldingRegisters[0].ToString("D5") +
                    "Holding register n.60 : " + ms.ModbusDB.Single(x => x.UnitID == unit_id).HoldingRegisters[59].ToString("D5") +
                    "Coil register    n.32 : " + ms.ModbusDB.Single(x => x.UnitID == unit_id).Coils[31].ToString();
                ms.ModbusDB.Single(x => x.UnitID == unit_id).HoldingRegisters[1] = (ushort)rnd.Next(ushort.MinValue, ushort.MaxValue);
                textBox2.Text = "Holding register n.2  : " + ms.ModbusDB.Single(x => x.UnitID == unit_id).HoldingRegisters[1].ToString("D5");
                ms.ModbusDB.Single(x => x.UnitID == unit_id).Coils[15] = Convert.ToBoolean(rnd.Next(0, 1));
                textBox3.Text = "Coil register    n.16 : " + ms.ModbusDB.Single(x => x.UnitID == unit_id).Coils[15].ToString();
                // Exec the cicle each 2 seconds
                delayTime(2);
            }
        }
    }
}
