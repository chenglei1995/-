﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RobotClassLib;
using static RobotClassLib.RobotCalculate;
using csLTDMC;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using static System.Math;
using System.IO;
using System.Runtime.InteropServices;
using Modbus;

namespace 机械臂控制软件
{
    public partial class 取样机械臂上位机界面 : Form
    {
        DoubleBufferListView listView1 = new DoubleBufferListView();//创建一个listview
        Thread m_thread;//开启一个新线程显图像
        static ushort _cardid = 0;
        HoleRobot robot1 = new HoleRobot(_cardid);
        short board_num = 0;
        private EventCheckChange checkstep=new EventCheckChange();
        /// <summary>
        /// 界面初始化
        /// </summary>
        public 取样机械臂上位机界面()
        {
            #region 设置listview
            this.listView1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(5, 41);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(425, 206);
            this.listView1.TabIndex = 20;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            listView1.Visible = false;//使listview1不可见
            listView1.Clear();
            listView1.Columns.Add("步数", 65, System.Windows.Forms.HorizontalAlignment.Left);
            listView1.Columns.Add("x位置", 60, System.Windows.Forms.HorizontalAlignment.Left);
            listView1.Columns.Add("y位置", 60, System.Windows.Forms.HorizontalAlignment.Left);
            listView1.Columns.Add("z位置", 60, System.Windows.Forms.HorizontalAlignment.Left);
            listView1.Columns.Add("a位置", 60, System.Windows.Forms.HorizontalAlignment.Left);
            listView1.Columns.Add("b位置", 60, System.Windows.Forms.HorizontalAlignment.Left);
            listView1.Columns.Add("c位置", 60, System.Windows.Forms.HorizontalAlignment.Left);
            this.Controls.Add(listView1);
            #endregion
            checkstep.OntempChange += new EventCheckChange.tempChange(event_stepchange);
            InitializeComponent();
            button_cycle_start.Enabled = false;
            short num = LTDMC.dmc_board_init();//获取卡数量
            board_num = num;
            if (num <= 0 || num > 6)
            {
                MessageBox.Show("初始卡失败!", "出错");
            }
            ushort _num = 0;
            ushort[] cardids = new ushort[6];
            uint[] cardtypes = new uint[6];
            short res = LTDMC.dmc_get_CardInfList(ref _num, cardtypes, cardids);
            if (res != 0)
            {
                MessageBox.Show("获取卡信息失败!");
            }
            _cardid = cardids[0];
            InitializationCalculate();//初始化matlab
            //InitializationPlot();//
            robot1 = new HoleRobot(_cardid);
            m_thread = new Thread(Display);
            m_thread.Start();
            robot1.PlotRobot = true;//开启图像仿真
            timer1.Start();
            timer_Hole.Start();
            robot1.PropertyChanged += PropertyChangedManage;
            button6.Enabled = true;
            button2.Enabled = true;
            button_cycle_start.Enabled = false;
            button3.Enabled = false;
            microwell_961.Enabled = false;
            //  double[] q0 = new double[6] { PI / 2, PI / 2, 0, 0, 0, 0 };
            //PaintRobot(q0);
        }
        /// <summary>
        /// 属性改变事件处理函数
        /// </summary>
        void PropertyChangedManage()
        {
            if(robot1.KeyPointFinished)
            {
                button6.Enabled = false;
            }
            else
            {
                button6.Enabled = true;
            }
            if(robot1.HoleKeyPointFinished)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
            if(robot1.KeyPointFinished&&robot1.HoleKeyPointFinished&&(!robot1.HolePointFinished||!robot1.QGenerateFinished))
            {
                button3.Enabled = true;
                microwell_961.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
                microwell_961.Enabled = false;
            }
            if(robot1.QGenerateFinished)
            {
                button_cycle_start.Enabled = true;
            }
            else
            {
                button_cycle_start.Enabled = false;
                bool test9 = microwell_961.Checks[0, 0];
            }
        }
        /// <summary>
        /// step值改变函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void event_stepchange(object sender, EventArgs e)
        {
            DynamicDisplayListView();
        }
        /// <summary>
        /// 机械臂初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_initialize_Click(object sender, EventArgs e)
        {
            robot1.Initialize();
        }
        /// <summary>
        /// 紧急停止运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_startstop_Click(object sender, EventArgs e)
        {
           robot1.Emerg_Stop();
        }
        /// <summary>
        /// 添加运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            robot1.AddKeyPoint();
            DisplayListView();
        }
        private void numericUpDown_anglevel_ValueChanged(object sender, EventArgs e)
        {
            double angle_vel = decimal.ToDouble(numericUpDown_anglevel.Value);
            robot1.default_vel = robot1.TransAngleToArc(angle_vel);
        }
        /// <summary>
        /// 添加96孔板孔位按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (robot1.HoleKeyPoint.Count < robot1.HoleKeyPointNmu - 1)
            {
                robot1.AddHoleKeyPoint();
                DisplayListView();
            }
            else
            {

                DisplayListView();

                for (int k = 0; k < 8; k++)
                {
                    for (int w = 0; w < 12; w++)
                    {
                        microwell_961.Checks[k, w] = false;
                    }
                }
                robot1.AddHoleKeyPoint();
                MessageBox.Show("请选择需要加样的孔", "提示");
            }

        }
        /// <summary>
        /// 删除一个关键点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (robot1.KeyPoint.Count < 6)
            {
                if (robot1.KeyPoint.Count == 0)
                {
                    //如果集合为空，则不操作
                }
                else
                {
                    robot1.DeleteKeyPoint();
                }
            }
            else
            {
                robot1.DeleteKeyPoint();
                button6.Enabled = true;
            }
        }
        /// <summary>
        /// 删除一个孔板点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (robot1.HoleKeyPoint.Count < robot1.HoleKeyPointNmu)
            {
                if (robot1.HoleKeyPoint.Count == 0)
                {
                    //如果集合为空，则不操作
                }
                else
                {
                    robot1.DeleteHoleKeyPoint();
                }
            }
            else
            {
                robot1.DeleteHoleKeyPoint();
                button2.Enabled = true;
            }
        }
        /// <summary>
        /// 清空按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            int n = 0;
            n = robot1.StepWork.Count;
            robot1.DeleteQ(n);
            DisplayListView();
        }
        /// <summary>
        /// x正向运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_to_posx_Click(object sender, EventArgs e)
        {
            double time = decimal.ToDouble(numericUpDown_time.Value);
            int n = decimal.ToInt32(numericUpDown_addnum.Value);
            double distance = decimal.ToDouble(numericUpDown_distance.Value);
            double[] vn = new double[3] { distance, 0, 0 };
            robot1.Move(vn, n, time);
        }
        /// <summary>
        /// x负向运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_to_negx_Click(object sender, EventArgs e)
        {
            double time = decimal.ToDouble(numericUpDown_time.Value);
            int n = decimal.ToInt32(numericUpDown_addnum.Value);
            double distance = decimal.ToDouble(numericUpDown_distance.Value);
            double[] vn = new double[3] { -distance, 0, 0 };
            robot1.Move(vn, n, time);
        }
        /// <summary>
        /// y正向运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_to_posy_Click(object sender, EventArgs e)
        {
            double time = decimal.ToDouble(numericUpDown_time.Value);
            int n = decimal.ToInt32(numericUpDown_addnum.Value);
            double distance = decimal.ToDouble(numericUpDown_distance.Value);
            double[] vn = new double[3] { 0,distance, 0 };
            robot1.Move(vn, n, time);
        }
        /// <summary>
        /// y负向按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_to_negy_Click(object sender, EventArgs e)
        {
            double time = decimal.ToDouble(numericUpDown_time.Value);
            int n = decimal.ToInt32(numericUpDown_addnum.Value);
            double distance = decimal.ToDouble(numericUpDown_distance.Value);
            double[] vn = new double[3] {  0, -distance, 0 };
            robot1.Move(vn, n, time);
        }
        /// <summary>
        /// z正向运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_to_posz_Click(object sender, EventArgs e)
        {
            double time = decimal.ToDouble(numericUpDown_time.Value);
            int n = decimal.ToInt32(numericUpDown_addnum.Value);
            double distance = decimal.ToDouble(numericUpDown_distance.Value);
            double[] vn = new double[3] { 0, 0, distance };
            robot1.Move(vn, n, time);
        }
        /// <summary>
        /// z负向运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_to_negz_Click(object sender, EventArgs e)
        {
            double time = decimal.ToDouble(numericUpDown_time.Value);
            int n = decimal.ToInt32(numericUpDown_addnum.Value);
            double distance = decimal.ToDouble(numericUpDown_distance.Value);
            double[] vn = new double[3] { 0, 0, -distance };
            robot1.Move(vn, n, time);
        }
        /// <summary>
        /// 运动到水平姿态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_tolevel_Click(object sender, EventArgs e)
        {
            double time = decimal.ToDouble(numericUpDown_time.Value);
            int n = decimal.ToInt32(numericUpDown_addnum.Value);
            robot1.GoLevel(n,time);
        }
        /// <summary>
        /// 运动到垂直姿态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_tovertical_Click(object sender, EventArgs e)
        {
            double time = decimal.ToDouble(numericUpDown_time.Value);
            int n = decimal.ToInt32(numericUpDown_addnum.Value);
            robot1.GoVertical(n, time);
        }
        /// <summary>
        /// 添加加样按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            robot1.AddHole();
            DisplayListView();
        }
        /// <summary>
        /// 开始循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_cycle_start_Click(object sender, EventArgs e)
        {
                robot1.Start();

        }
        /// <summary>
        /// 暂停运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_cycle_stop_Click(object sender, EventArgs e)
        {
            if (button_cycle_stop.Text == "暂停")
            {
                robot1.Conti_Pause();
                button_cycle_stop.Text = "重启";
            }
            else//重启运动控制卡
            {
                robot1.Conti_Restart();
                button_cycle_stop.Text = "暂停";
            }
        }
        /// <summary>
        /// x+向旋转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            double step = decimal.ToDouble(numericUpDown_xstep.Value);
            double speed = decimal.ToDouble(numericUpDown_xspeed.Value);
            robot1.RotByAngle(0, step, speed);
        }
        /// <summary>
        /// x-向旋转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            double step = decimal.ToDouble(numericUpDown_xstep.Value);
            double speed = decimal.ToDouble(numericUpDown_xspeed.Value);
            robot1.RotByAngle(0, -step, speed);
        }
        /// <summary>
        /// y+向旋转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {
            double step = decimal.ToDouble(numericUpDown_ystep.Value);
            double speed = decimal.ToDouble(numericUpDown_yspeed.Value);
            robot1.RotByAngle(1, step, speed);
        }
        /// <summary>
        /// y-向旋转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            double step = decimal.ToDouble(numericUpDown_ystep.Value);
            double speed = decimal.ToDouble(numericUpDown_yspeed.Value);
            robot1.RotByAngle(1, -step, speed);
        }
        /// <summary>
        /// z+向旋转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {
            double step = decimal.ToDouble(numericUpDown_zstep.Value);
            double speed = decimal.ToDouble(numericUpDown_zspeed.Value);
            robot1.RotByAngle(2, step, speed);
        }
        /// <summary>
        /// z-向旋转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            double step = decimal.ToDouble(numericUpDown_zstep.Value);
            double speed = decimal.ToDouble(numericUpDown_zspeed.Value);
            robot1.RotByAngle(2, -step, speed);
        }
        /// <summary>
        /// a+向旋转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button16_Click(object sender, EventArgs e)
        {
            double step = decimal.ToDouble(numericUpDown_astep.Value);
            double speed = decimal.ToDouble(numericUpDown_aspeed.Value);
            robot1.RotByAngle(3, step, speed);
        }
        /// <summary>
        /// a-向旋转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            double step = decimal.ToDouble(numericUpDown_astep.Value);
            double speed = decimal.ToDouble(numericUpDown_aspeed.Value);
            robot1.RotByAngle(3, -step, speed);
        }
        /// <summary>
        /// b+向旋转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button22_Click(object sender, EventArgs e)
        {
            double step = decimal.ToDouble(numericUpDown_bstep.Value);
            double speed = decimal.ToDouble(numericUpDown_bspeed.Value);
            robot1.RotByAngle(4, step, speed);
        }
        /// <summary>
        /// b-向旋转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            double step = decimal.ToDouble(numericUpDown_bstep.Value);
            double speed = decimal.ToDouble(numericUpDown_bspeed.Value);
            robot1.RotByAngle(4, -step, speed);
        }
        /// <summary>
        /// c+向旋转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button20_Click(object sender, EventArgs e)
        {
            double step = decimal.ToDouble(numericUpDown_cstep.Value);
            double speed = decimal.ToDouble(numericUpDown_cspeed.Value);
            robot1.RotByAngle(5, step, speed);
        }
        /// <summary>
        /// c-向旋转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            double step = decimal.ToDouble(numericUpDown_cstep.Value);
            double speed = decimal.ToDouble(numericUpDown_cspeed.Value);
            robot1.RotByAngle(5, -step, speed);
        }
        /// <summary>
        /// 机械爪张开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_claw_open_Click(object sender, EventArgs e)
        {
            robot1.ClawOperate(1);
        }
        /// <summary>
        /// 机械爪闭合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_claw_close_Click(object sender, EventArgs e)
        {
            robot1.ClawOperate(0);
        }
        /// <summary>
        /// 打孔器操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_hole_operate_Click(object sender, EventArgs e)
        {
            robot1.HoleOperate();
        }
        /// <summary>
        /// 显示仿真
        /// </summary>
        private void Display()
        {


            while (true)
            {

                if (robot1.PlotRobot == true)//如果检测到运动控制卡且开启图像仿真
                {

                    robot1.Plot_Robot();
                }

            }

        }
        /// <summary>
        /// x轴绝对运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button24_Click(object sender, EventArgs e)
        {
            double angle = decimal.ToDouble(numericUpDown_toanglex.Value);
            double speed = decimal.ToDouble(numericUpDown_speedx.Value);
            robot1.RotToAngle(0, angle, speed);
        }
        /// <summary>
        /// y轴绝对运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {
            double angle = decimal.ToDouble(numericUpDown_toangley.Value);
            double speed = decimal.ToDouble(numericUpDown_speedy.Value);
            robot1.RotToAngle(1, angle, speed);
        }
        /// <summary>
        /// z轴绝对运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button27_Click(object sender, EventArgs e)
        {
            double angle = decimal.ToDouble(numericUpDown_toanglez.Value);
            double speed = decimal.ToDouble(numericUpDown_speedz.Value);
            robot1.RotToAngle(2, angle, speed);
        }
        /// <summary>
        /// a轴绝对运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button26_Click(object sender, EventArgs e)
        {
            double angle = decimal.ToDouble(numericUpDown_toanglea.Value);
            double speed = decimal.ToDouble(numericUpDown_speeda.Value);
            robot1.RotToAngle(3, angle, speed);
        }
        /// <summary>
        /// b轴绝对运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button29_Click(object sender, EventArgs e)
        {
            double angle = decimal.ToDouble(numericUpDown_toangleb.Value);
            double speed = decimal.ToDouble(numericUpDown_speedb.Value);
            robot1.RotToAngle(4, angle, speed);
        }
        /// <summary>
        /// c轴绝对运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button28_Click(object sender, EventArgs e)
        {
            double angle = decimal.ToDouble(numericUpDown_toanglec.Value);
            double speed = decimal.ToDouble(numericUpDown_speedc.Value);
            robot1.RotToAngle(5, angle, speed);
        }
        /// <summary>
        /// 浏览按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button23_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileName = new OpenFileDialog();//定义一个文件打开控件
            string startpath = Path.Combine(Application.StartupPath, "程序文件夹");
            Directory.CreateDirectory(startpath);//创建程序文件夹，如存在则不创建
            fileName.InitialDirectory = startpath;//设置打开控件后，默认目录为exe运行文件所在的文件夹
            fileName.Filter = "数据文件|*.dat";//设置打开文件类型为.dat
            fileName.FilterIndex = 1;//设置控件打开文件类型的显示顺序
            fileName.RestoreDirectory = true;//设置对话框是否记忆之前打开的目录
            if (fileName.ShowDialog() == DialogResult.OK)
            {
                string path_dataadd = fileName.FileName.ToString();//获得用户选择的文件完整路径
                string file_dataadd = path_dataadd.Substring(path_dataadd.LastIndexOf("\\") + 1);//获取用户选择的不带路径的文件名
                robot1.AddData(path_dataadd);
                textBox_file_read.Text = file_dataadd;
            }
            DisplayListView();
        }
        /// <summary>
        /// 保存数据按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button30_Click(object sender, EventArgs e)
        {
            string localFilePath = "";
            //string localFilePath, fileNameExt, newFileName, FilePath; 
            SaveFileDialog filename = new SaveFileDialog();
            filename.Filter = "数据文件|*.dat";//设置文件类型 
            string startpath = Path.Combine(Application.StartupPath, "程序文件夹");
            Directory.CreateDirectory(startpath);//创建程序文件夹，如存在则不创建
            filename.InitialDirectory = startpath;//设置打开控件后，默认目录为exe运行文件所在的文件夹
            filename.FilterIndex = 1;//设置默认文件类型显示顺序 filename.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录 
            if (filename.ShowDialog() == DialogResult.OK)//点了保存按钮进入 
            {
                localFilePath = filename.FileName.ToString(); //获得文件路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                robot1.SaveData(localFilePath);
                //获取文件路径，不带文件名 
                //FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\")); 

                //给文件名前加上时间 
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt; 

                //在文件名里加字符 
                //saveFileDialog1.FileName.Insert(1,"dameng"); 

                //System.IO.FileStream fs = (System.IO.FileStream)filename.OpenFile();//输出文件 

                ////fs输出带文字或图片的文件，就看需求了 
            }
        }
         /// <summary>
         /// 实时位置显示
         /// </summary>
         /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            robot1.Out4_Value = LTDMC.dmc_read_outbit(robot1.Card_ID, 4);
            robot1.Out5_Value = LTDMC.dmc_read_outbit(robot1.Card_ID, 5);
            checkstep.Temp = robot1.Step;
            if (board_num > 0)
            {
                double[] q1 = new double[6];
                q1 = robot1.GetPositionAngle();
                label_x_position.Text = (((double)((int)(q1[0] * 100))) / 100).ToString();
                label_y_position.Text = (((double)((int)(q1[1] * 100))) / 100).ToString();
                label_z_position.Text = (((double)((int)(q1[2] * 100))) / 100).ToString();
                label_a_position.Text = (((double)((int)(q1[3] * 100))) / 100).ToString();
                label_b_position.Text = (((double)((int)(q1[4] * 100))) / 100).ToString();
                label_c_position.Text = (((double)((int)(q1[5] * 100))) / 100).ToString();
            }
        }
        /// <summary>
        /// 退出程序按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 机械臂控制软件界面_FormClosed(object sender, FormClosedEventArgs e)
        {
            microwell_961.Visible = false;
            m_thread.Abort();
            try
            {
                //关闭串口 
                robot1.CloseCom();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "串口关闭错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CloseMatlab();
            LTDMC.dmc_board_close();
        }
        /// <summary>
        /// listview显示函数
        /// </summary>
        private void DisplayListView()
        {
            this.listView1.Items.Clear();
            this.listView1.BeginUpdate();
            int step = robot1.Step;
            int substep = robot1.TransSub(step);
            int count = robot1.StepWork.Count;
            int subi = 0;
            for (int i = 0; i < count; i++)
            {
                subi = robot1.TransSub(i);
                double[] q = new double[6];
                for (int j = 0; j < 6; j++)
                {
                    q[j] = ((double[])robot1.Q[subi])[j];
                }

                ListViewItem p = new ListViewItem();
                if (i == step)
                {
                    p.ForeColor = Color.Red;
                    p.BackColor = Color.LightBlue;
                }
                if ((int)(robot1.StepWork[i]) == 0)//为运动情况
                {
                    for (int j = 0; j < 6; j++)//转换为角度单位
                    {
                        q[j] = robot1.TransArcToAngle(q[j]);
                    }
                    p.Text = i.ToString();
                    p.SubItems.Add((((double)((int)(q[0] * 100))) / 100).ToString());
                    p.SubItems.Add((((double)((int)(q[1] * 100))) / 100).ToString());
                    p.SubItems.Add((((double)((int)(q[2] * 100))) / 100).ToString());
                    p.SubItems.Add((((double)((int)(q[3] * 100))) / 100).ToString());
                    p.SubItems.Add((((double)((int)(q[4] * 100))) / 100).ToString());
                    p.SubItems.Add((((double)((int)(q[5] * 100))) / 100).ToString());
                    this.listView1.Items.Add(p);
                }
                else if ((int)(robot1.StepWork[i]) == 1)//为延时的情况
                {
                    p.Text = i.ToString();
                    p.SubItems.Add("延时" + q[0].ToString() + "秒");
                    this.listView1.Items.Add(p);
                }
                else if ((int)(robot1.StepWork[i]) == 2)//为泵操作情况
                {
                    p.Text = i.ToString();
                    if (q[0] == 1)
                    {
                        p.SubItems.Add("吸液" + q[2].ToString() + "ml");
                    }
                    else
                    {
                        p.SubItems.Add("排液" + q[2].ToString() + "ml");
                    }
                    this.listView1.Items.Add(p);
                }
                else if ((int)(robot1.StepWork[i]) == 3)
                {
                    p.Text = i.ToString();
                    p.SubItems.Add("添加样本");
                    this.listView1.Items.Add(p);
                }
                else if ((int)(robot1.StepWork[i]) == 4)
                {
                    p.Text = i.ToString();
                    if (q[0] == 1)
                    {
                        p.SubItems.Add("夹紧操作");
                    }
                    else
                    {
                        p.SubItems.Add("张开操作");
                    }
                    this.listView1.Items.Add(p);
                }
            }

            this.listView1.EndUpdate();
        }
        /// <summary>
        /// listview动态显示函数
        /// </summary>
        public void DynamicDisplayListView()
        {
            this.listView1.Items.Clear();
            this.listView1.BeginUpdate();
            int step = robot1.Step;
            int substep = robot1.TransSub(step);
            int count = robot1.StepWork.Count;
            int subi = 0;
            for (int i = 0; i < count; i++)
            {
                subi = robot1.TransSub(i);
                double[] q = new double[6];
                for (int j = 0; j < 6; j++)
                {
                    q[j] = ((double[])robot1.Q[subi])[j];
                }

                ListViewItem p = new ListViewItem();
                if (i == step)
                {
                    p.ForeColor = Color.Red;
                    p.BackColor = Color.LightBlue;
                }
                if ((int)(robot1.StepWork[i]) == 0)//为运动情况
                {
                    for (int j = 0; j < 6; j++)//转换为角度单位
                    {
                        q[j] = robot1.TransArcToAngle(q[j]);
                    }
                    p.Text = i.ToString();
                    p.SubItems.Add((((double)((int)(q[0] * 100))) / 100).ToString());
                    p.SubItems.Add((((double)((int)(q[1] * 100))) / 100).ToString());
                    p.SubItems.Add((((double)((int)(q[2] * 100))) / 100).ToString());
                    p.SubItems.Add((((double)((int)(q[3] * 100))) / 100).ToString());
                    p.SubItems.Add((((double)((int)(q[4] * 100))) / 100).ToString());
                    p.SubItems.Add((((double)((int)(q[5] * 100))) / 100).ToString());
                    this.listView1.Items.Add(p);
                }
                else if ((int)(robot1.StepWork[i]) == 1)//为延时的情况
                {
                    p.Text = i.ToString();
                    p.SubItems.Add("延时" + q[0].ToString() + "秒");
                    this.listView1.Items.Add(p);
                }
                else if ((int)(robot1.StepWork[i]) == 2)//为泵操作情况
                {
                    p.Text = i.ToString();
                    if (q[0] == 1)
                    {
                        p.SubItems.Add("吸液" + q[2].ToString() + "ml");
                    }
                    else
                    {
                        p.SubItems.Add("排液" + q[2].ToString() + "ml");
                    }
                    this.listView1.Items.Add(p);
                }
                else if ((int)(robot1.StepWork[i]) == 3)
                {
                    p.Text = i.ToString();
                    p.SubItems.Add("添加样本");
                    this.listView1.Items.Add(p);
                }
                else if ((int)(robot1.StepWork[i]) == 4)
                {
                    p.Text = i.ToString();
                    if (q[0] == 1)
                    {
                        p.SubItems.Add("夹紧操作");
                    }
                    else
                    {
                        p.SubItems.Add("张开操作");
                    }
                    this.listView1.Items.Add(p);
                }
            }
            this.listView1.EndUpdate();
            if(step>=0)
            {
                listView1.Items[step].EnsureVisible();
            }
        }
        /// <summary>
        /// 添加完成按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                robot1.GenerateHolePoint();
                robot1.GenerateQ();
                MessageBox.Show("添加完成", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }
        private void timer_Hole_Tick(object sender, EventArgs e)
        {
            label1.Text = "第" + (robot1.KeyPoint.Count + 1).ToString() + "个关键点（共6个）:";
            label2.Text = "第" + (robot1.HoleKeyPoint.Count + 1).ToString() + "个孔板关键点（4个角）：";
            int test1=robot1.StepWork.Count;
            bool test2 = robot1.HolePointFinished;
            bool test3 = robot1.KeyPointFinished;
            int test4 = robot1.KeyPoint.Count;
            int test5 = robot1.HolePoint.Count;
            int test6 = robot1.HoleKeyPoint.Count;
            bool test7 = robot1.HoleKeyPointFinished;
            bool test8 = button3.Enabled;
            bool test9=microwell_961.Checks[0, 0];
            if (microwell_961.Enabled)//如果已经就绪
            {
                robot1.HoleChoose = (bool[,])microwell_961.Checks.Clone();
            }
            else if(!microwell_961.Enabled&&(!robot1.KeyPointFinished ||!robot1.HoleKeyPointFinished))//如果关键点还没添加完成
            {
                for (int k=0;k<8;k++)
                {
                    for(int w=0;w<12;w++)
                    {
                        microwell_961.Checks[k, w] = false;
                    }
                }
                if(robot1.HoleKeyPoint.Count==0)
                {
                    microwell_961.Checks[0, 0] = true;
                }
                else if(robot1.HoleKeyPoint.Count == 1)
                {
                    microwell_961.Checks[7, 0] = true;
                }
                else if(robot1.HoleKeyPoint.Count == 2)
                {
                    microwell_961.Checks[0, 11] = true;
                }
                else if(robot1.HoleKeyPoint.Count == 3)
                {
                    microwell_961.Checks[7, 11] = true;
                }
            }
            else//添加完成以后
            {
                int i = 0;
                int j = 0;
                robot1.Trans1DTo2D(ref i, ref j, robot1.TransStepToHoleStep(robot1.Step));
                microwell_961.i_Coordinate = i;
                microwell_961.j_Coordinate = j;
            }
        }
        /// <summary>
        /// 开启生产线按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            robot1.Start();
            ModbusMasterTCP mm = new ModbusMasterTCP("192.168.0.2", 502);
            mm.Connect();
            mm.WriteSingleCoil(2, 0, true);
            try
            {
                while (mm.ReadCoils(2, 0, 1).First())
                {
                    //Application.DoEvents();//待定，空执行
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            mm.Disconnect();
            mm = new ModbusMasterTCP("192.168.0.3", 502);
            mm.Connect();
            mm.WriteSingleCoil(3, 0, true);
            /*while (mm.ReadCoils(3, 0, 1).First())
                {
                    //Application.DoEvents();//待定，空执行
                }*/
            mm.Disconnect();


        }

       
    }
    /// <summary>
    /// 改进listview类
    /// </summary>
    public class DoubleBufferListView : ListView
    {
        public DoubleBufferListView()
        {
            SetStyle(ControlStyles.DoubleBuffer |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
    }
    /// <summary>
    /// 检测变量变化的类
    /// </summary>
    class EventCheckChange
    {
        public delegate void tempChange(object sender, EventArgs e);
        public event tempChange OntempChange;
        int temp;
        public int Temp
        {
            get { return temp; }
            set
            {
                if(temp!=value)
                {
                    OntempChange(this, new EventArgs());
                }
                temp = value;
            }
        }
    }

}
