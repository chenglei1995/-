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

namespace ModbusMasterSearial
{
    public partial class Mater : Form
    {
        public Mater()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            byte unit_id = 1;
            // Crete instance of modbus serial RTU (replace COMx with a free serial port - ex. COM5)
            ModbusMasterSerial mm = new ModbusMasterSerial(ModbusSerialType.RTU, "COM1", 9600, 8, Parity.Even, StopBits.One, Handshake.None);
            // Exec the connection
            mm.Connect();
            // Read and write some registers on RTU n. 5
            Random rnd = new Random();
            while (true)
            {
                textBox1.Text = "Holding register n.1  : " + mm.ReadHoldingRegisters(unit_id, 0, 1).First().ToString("D5") +
                     "Input register   n.41 : " + mm.ReadInputRegisters(unit_id, 40, 1).First().ToString("D5") +
                     "Coil register    n 23 : " + mm.ReadCoils(unit_id, 22, 1).First().ToString();
                mm.WriteSingleRegister(unit_id, 4, (ushort)rnd.Next(ushort.MinValue, ushort.MaxValue));
                textBox2.Text = "Holding register n.5  : " + mm.ReadHoldingRegisters(unit_id, 4, 1).First().ToString("D5");
                mm.WriteSingleCoil(unit_id, 2, Convert.ToBoolean(rnd.Next(0, 1)));
                textBox3.Text = "Coil register    n.3  : " + mm.ReadCoils(unit_id, 2, 1).First().ToString();
                // Exec the cicle each 2 seconds
                Thread.Sleep(2000);
            }
        }
    }
}
