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
        static Datastore ds = new Datastore(1);
        ModbusSlaveTCP ms = new ModbusSlaveTCP(new Datastore[] { ds }, IPAddress.Parse("192.168.0.1") , 502);
        public SlaverTCP()
        {
            InitializeComponent();
            textBox4.Text = "192.168.0.1";
            textBox5.Text = "1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text== "开启")
            {
                // Created datastore for unit ID 1
                ds = new Datastore(byte.Parse(textBox5.Text));
                // Crete instance of modbus serial RTU (replace COMx with a free serial port - ex. COM5)
                ms = new ModbusSlaveTCP(new Datastore[] { ds }, IPAddress.Parse(textBox4.Text), 502);
                ms.DatastoreChanged += DatastoreChangedManage;
               // Start listen
               ms.StartListen();
                button1.Text = "关闭";
            }
           else
            {
                ms.StopListen();
                button1.Text = "开启";
            } 
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
        private void DatastoreChangedManage()
        {
            if(button1.Text == "关闭")
            {
                Thread.Sleep(100);
               // textBox1.Text = ms.ModbusDB.Single(x => x.UnitID == byte.Parse(textBox5.Text)).HoldingRegisters[4].ToString("D5");
                //textBox2.Text = ms.ModbusDB.Single(x => x.UnitID == byte.Parse(textBox5.Text)).Coils[2].ToString();
            }
        }
      
    }
}
