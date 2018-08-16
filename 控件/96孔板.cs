using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Drawing.Graphics;
using System.Drawing.Drawing2D;
using System.Collections;
using System.IO;

namespace 控件
{
    public partial class Microwell_96: UserControl
    {
        #region 定义graphic
        ClassGraphic graphic1 = new ClassGraphic();
        ClassGraphic graphic2 = new ClassGraphic();
        ClassGraphic graphic3 = new ClassGraphic();
        ClassGraphic graphic4 = new ClassGraphic();
        ClassGraphic graphic5 = new ClassGraphic();
        ClassGraphic graphic6 = new ClassGraphic();
        ClassGraphic graphic7 = new ClassGraphic();
        ClassGraphic graphic8 = new ClassGraphic();
        ClassGraphic graphic9 = new ClassGraphic();
        ClassGraphic graphic10 = new ClassGraphic();
        ClassGraphic graphic11 = new ClassGraphic();
        ClassGraphic graphic12 = new ClassGraphic();
        ClassGraphic graphic13 = new ClassGraphic();
        ClassGraphic graphic14 = new ClassGraphic();
        ClassGraphic graphic15 = new ClassGraphic();
        ClassGraphic graphic16 = new ClassGraphic();
        ClassGraphic graphic17 = new ClassGraphic();
        ClassGraphic graphic18 = new ClassGraphic();
        ClassGraphic graphic19 = new ClassGraphic();
        ClassGraphic graphic20 = new ClassGraphic();
        ClassGraphic graphic21 = new ClassGraphic();
        ClassGraphic graphic22 = new ClassGraphic();
        ClassGraphic graphic23 = new ClassGraphic();
        ClassGraphic graphic24 = new ClassGraphic();
        ClassGraphic graphic25 = new ClassGraphic();
        ClassGraphic graphic26 = new ClassGraphic();
        ClassGraphic graphic27 = new ClassGraphic();
        ClassGraphic graphic28 = new ClassGraphic();
        ClassGraphic graphic29 = new ClassGraphic();
        ClassGraphic graphic30 = new ClassGraphic();
        ClassGraphic graphic31 = new ClassGraphic();
        ClassGraphic graphic32 = new ClassGraphic();
        ClassGraphic graphic33 = new ClassGraphic();
        ClassGraphic graphic34 = new ClassGraphic();
        ClassGraphic graphic35 = new ClassGraphic();
        ClassGraphic graphic36 = new ClassGraphic();
        ClassGraphic graphic37 = new ClassGraphic();
        ClassGraphic graphic38 = new ClassGraphic();
        ClassGraphic graphic39 = new ClassGraphic();
        ClassGraphic graphic40 = new ClassGraphic();
        ClassGraphic graphic41 = new ClassGraphic();
        ClassGraphic graphic42 = new ClassGraphic();
        ClassGraphic graphic43 = new ClassGraphic();
        ClassGraphic graphic44 = new ClassGraphic();
        ClassGraphic graphic45 = new ClassGraphic();
        ClassGraphic graphic46 = new ClassGraphic();
        ClassGraphic graphic47 = new ClassGraphic();
        ClassGraphic graphic48 = new ClassGraphic();
        ClassGraphic graphic49 = new ClassGraphic();
        ClassGraphic graphic50 = new ClassGraphic();
        ClassGraphic graphic51 = new ClassGraphic();
        ClassGraphic graphic52 = new ClassGraphic();
        ClassGraphic graphic53 = new ClassGraphic();
        ClassGraphic graphic54 = new ClassGraphic();
        ClassGraphic graphic55 = new ClassGraphic();
        ClassGraphic graphic56 = new ClassGraphic();
        ClassGraphic graphic57 = new ClassGraphic();
        ClassGraphic graphic58 = new ClassGraphic();
        ClassGraphic graphic59 = new ClassGraphic();
        ClassGraphic graphic60 = new ClassGraphic();
        ClassGraphic graphic61 = new ClassGraphic();
        ClassGraphic graphic62 = new ClassGraphic();
        ClassGraphic graphic63 = new ClassGraphic();
        ClassGraphic graphic64 = new ClassGraphic();
        ClassGraphic graphic65 = new ClassGraphic();
        ClassGraphic graphic66 = new ClassGraphic();
        ClassGraphic graphic67 = new ClassGraphic();
        ClassGraphic graphic68 = new ClassGraphic();
        ClassGraphic graphic69 = new ClassGraphic();
        ClassGraphic graphic70 = new ClassGraphic();
        ClassGraphic graphic71 = new ClassGraphic();
        ClassGraphic graphic72 = new ClassGraphic();
        ClassGraphic graphic73 = new ClassGraphic();
        ClassGraphic graphic74 = new ClassGraphic();
        ClassGraphic graphic75 = new ClassGraphic();
        ClassGraphic graphic76 = new ClassGraphic();
        ClassGraphic graphic77 = new ClassGraphic();
        ClassGraphic graphic78 = new ClassGraphic();
        ClassGraphic graphic79 = new ClassGraphic();
        ClassGraphic graphic80 = new ClassGraphic();
        ClassGraphic graphic81 = new ClassGraphic();
        ClassGraphic graphic82 = new ClassGraphic();
        ClassGraphic graphic83 = new ClassGraphic();
        ClassGraphic graphic84 = new ClassGraphic();
        ClassGraphic graphic85 = new ClassGraphic();
        ClassGraphic graphic86 = new ClassGraphic();
        ClassGraphic graphic87 = new ClassGraphic();
        ClassGraphic graphic88 = new ClassGraphic();
        ClassGraphic graphic89 = new ClassGraphic();
        ClassGraphic graphic90 = new ClassGraphic();
        ClassGraphic graphic91 = new ClassGraphic();
        ClassGraphic graphic92 = new ClassGraphic();
        ClassGraphic graphic93 = new ClassGraphic();
        ClassGraphic graphic94 = new ClassGraphic();
        ClassGraphic graphic95 = new ClassGraphic();
        ClassGraphic graphic96 = new ClassGraphic();
        #endregion
        bool[,] checks = new bool[8, 12];
        int i_coordinate=-1;
        int j_coordinate=-1;
        ArrayList graphiclist=new ArrayList();//图像的集合
        ArrayList checklist = new ArrayList();//选取的集合
        EventCheckChangeInt i_check = new EventCheckChangeInt();
        EventCheckChangeInt j_check = new EventCheckChangeInt();
        #region 属性
        /// <summary>
        /// 是否选中的数组
        /// </summary>
        public bool[,] Checks
        {
            get { return checks; }
            set { checks = value; }
        }
        /// <summary>
        /// 将要操作的i坐标
        /// </summary>
        public int i_Coordinate
        {
            get { return i_coordinate; }
            set { i_coordinate = value; }
        }
        /// <summary>
        /// 将要操作的j坐标
        /// </summary>
        public int j_Coordinate
        {
            get { return j_coordinate; }
            set { j_coordinate = value; }
        }    
        #endregion
        public Microwell_96()
        {
            InitializeComponent();
            Pen pen = new Pen(Color.Black);//画笔颜色定义为黑色
           Graphics HoleGraphics; //Graphics 类提供将对象绘制到显示设备的方法
            Bitmap HoleBitmap; //位图对象
            Brush redbush = new SolidBrush(Color.Red);//填充颜色定义为红色 
            Brush greenbush = new SolidBrush(Color.Green);//填充颜色定义为绿色
            Brush bgbush = new SolidBrush(Color.Snow);//填充颜色定义为白色
            Brush blackbush = new SolidBrush(Color.Snow);//填充颜色定义为黑色
            int width = 0;
            width = picture1.Width;
            //FileStream green_path = new FileStream("green.jpg", FileMode.Create);
           // FileStream red_path = new FileStream("red.jpg", FileMode.Create);
          //  green_path.Close();
          //  red_path.Close();
            HoleBitmap = new Bitmap(width, width);//根据给定的高度和宽度创建一个位图图像
            HoleGraphics = Graphics.FromImage(HoleBitmap);//从指定的 objBitmap 对象创建 objGraphics 对象 (即在objBitmap对象中画图)
            HoleGraphics.FillRectangle(bgbush, 0, 0, width, width);
            HoleGraphics.DrawEllipse(pen, 0, 0, 2 * Width / 2, 2 * Width / 2);
            HoleGraphics.FillEllipse(greenbush, 0, 0, 2 * width / 2, 2 * width / 2);
            picture1.Image = Image.FromHbitmap(HoleBitmap.GetHbitmap());
            if (!File.Exists("green.jpg"))
            {
                picture1.Image.Save("green.jpg");
            }
            picture1.Image.Dispose();
            HoleGraphics.FillEllipse(blackbush, 1 * (int)width / 4, 1 * (int)width / 4, (int)width / 2, (int)width / 2);
            picture1.Image = Image.FromHbitmap(HoleBitmap.GetHbitmap());
            if (!File.Exists("blackgreen.jpg"))
            {
                picture1.Image.Save("blackgreen.jpg");
            }
            picture1.Image.Dispose();
            HoleGraphics.FillEllipse(redbush, 0, 0, 2 * width / 2, 2 * width / 2);
            picture1.Image = Image.FromHbitmap(HoleBitmap.GetHbitmap());
            if (!File.Exists("red.jpg"))
            {

                picture1.Image.Save("red.jpg");
            }
            picture1.Image.Dispose();
            for (int i=0;i<96;i++)
            {
                checklist.Add(new EventCheckChange());
                ((EventCheckChange)checklist[i]).OntempChange += new EventCheckChange.tempChange(event_stepchange);
            }
            i_check.OntempChange += new EventCheckChangeInt.tempChange(event_stepchange);
            j_check.OntempChange += new EventCheckChangeInt.tempChange(event_stepchange);
            Bitmap Bitmap97 = new Bitmap(333, 204); //位图对象
            Graphics Graphics97 = Graphics.FromImage(Bitmap97); //Graphics 类提供将对象绘制到显示设备的方法
            PointF[] CurvePointF = new PointF[6];
            CurvePointF[0] = new PointF(0,0);
            CurvePointF[1] = new PointF(0, 203);
            CurvePointF[2] = new PointF(332, 203);
            CurvePointF[3] = new PointF(332, 10);
            CurvePointF[4] = new PointF(322, 0);
            CurvePointF[5] = new PointF(0, 0);
            Graphics97.FillRectangle(new SolidBrush(Color.Snow), 0, 0, pictureBox97.Width, pictureBox97.Height);
            Graphics97.DrawCurve(pen, CurvePointF, 0);
            pictureBox97.Image = Image.FromHbitmap(Bitmap97.GetHbitmap());
            Bitmap97.Dispose();
            Graphics97.Dispose();
            
            #region graphic赋初值
            graphic1 = new ClassGraphic(picture1);
            graphic2 = new ClassGraphic(picture2);
            graphic3 = new ClassGraphic(picture3);
            graphic4 = new ClassGraphic(picture4);
            graphic5 = new ClassGraphic(picture5);
            graphic6 = new ClassGraphic(picture6);
            graphic7 = new ClassGraphic(picture7);
            graphic8 = new ClassGraphic(picture8);
            graphic9 = new ClassGraphic(picture9);
            graphic10 = new ClassGraphic(picture10);
            graphic11 = new ClassGraphic(picture11);
            graphic12 = new ClassGraphic(picture12);
            graphic13 = new ClassGraphic(picture13);
            graphic14 = new ClassGraphic(picture14);
            graphic15 = new ClassGraphic(picture15);
            graphic16 = new ClassGraphic(picture16);
            graphic17 = new ClassGraphic(picture17);
            graphic18 = new ClassGraphic(picture18);
            graphic19 = new ClassGraphic(picture19);
            graphic20 = new ClassGraphic(picture20);
            graphic21 = new ClassGraphic(picture21);
            graphic22 = new ClassGraphic(picture22);
            graphic23 = new ClassGraphic(picture23);
            graphic24 = new ClassGraphic(picture24);
            graphic25 = new ClassGraphic(picture25);
            graphic26 = new ClassGraphic(picture26);
            graphic27 = new ClassGraphic(picture27);
            graphic28 = new ClassGraphic(picture28);
            graphic29 = new ClassGraphic(picture29);
            graphic30 = new ClassGraphic(picture30);
            graphic31 = new ClassGraphic(picture31);
            graphic32 = new ClassGraphic(picture32);
            graphic33 = new ClassGraphic(picture33);
            graphic34 = new ClassGraphic(picture34);
            graphic35 = new ClassGraphic(picture35);
            graphic36 = new ClassGraphic(picture36);
            graphic37 = new ClassGraphic(picture37);
            graphic38 = new ClassGraphic(picture38);
            graphic39 = new ClassGraphic(picture39);
            graphic40 = new ClassGraphic(picture40);
            graphic41 = new ClassGraphic(picture41);
            graphic42 = new ClassGraphic(picture42);
            graphic43 = new ClassGraphic(picture43);
            graphic44 = new ClassGraphic(picture44);
            graphic45 = new ClassGraphic(picture45);
            graphic46 = new ClassGraphic(picture46);
            graphic47 = new ClassGraphic(picture47);
            graphic48 = new ClassGraphic(picture48);
            graphic49 = new ClassGraphic(picture49);
            graphic50 = new ClassGraphic(picture50);
            graphic51 = new ClassGraphic(picture51);
            graphic52 = new ClassGraphic(picture52);
            graphic53 = new ClassGraphic(picture53);
            graphic54 = new ClassGraphic(picture54);
            graphic55 = new ClassGraphic(picture55);
            graphic56 = new ClassGraphic(picture56);
            graphic57 = new ClassGraphic(picture57);
            graphic58 = new ClassGraphic(picture58);
            graphic59 = new ClassGraphic(picture59);
            graphic60 = new ClassGraphic(picture60);
            graphic61 = new ClassGraphic(picture61);
            graphic62 = new ClassGraphic(picture62);
            graphic63 = new ClassGraphic(picture63);
            graphic64 = new ClassGraphic(picture64);
            graphic65 = new ClassGraphic(picture65);
            graphic66 = new ClassGraphic(picture66);
            graphic67 = new ClassGraphic(picture67);
            graphic68 = new ClassGraphic(picture68);
            graphic69 = new ClassGraphic(picture69);
            graphic70 = new ClassGraphic(picture70);
            graphic71 = new ClassGraphic(picture71);
            graphic72 = new ClassGraphic(picture72);
            graphic73 = new ClassGraphic(picture73);
            graphic74 = new ClassGraphic(picture74);
            graphic75 = new ClassGraphic(picture75);
            graphic76 = new ClassGraphic(picture76);
            graphic77 = new ClassGraphic(picture77);
            graphic78 = new ClassGraphic(picture78);
            graphic79 = new ClassGraphic(picture79);
            graphic80 = new ClassGraphic(picture80);
            graphic81 = new ClassGraphic(picture81);
            graphic82 = new ClassGraphic(picture82);
            graphic83 = new ClassGraphic(picture83);
            graphic84 = new ClassGraphic(picture84);
            graphic85 = new ClassGraphic(picture85);
            graphic86 = new ClassGraphic(picture86);
            graphic87 = new ClassGraphic(picture87);
            graphic88 = new ClassGraphic(picture88);
            graphic89 = new ClassGraphic(picture89);
            graphic90 = new ClassGraphic(picture90);
            graphic91 = new ClassGraphic(picture91);
            graphic92 = new ClassGraphic(picture92);
            graphic93 = new ClassGraphic(picture93);
            graphic94 = new ClassGraphic(picture94);
            graphic95 = new ClassGraphic(picture95);
            graphic96 = new ClassGraphic(picture96);
            #endregion
            #region 添加graphic到集合中
            graphiclist.Add(graphic1);
            graphiclist.Add(graphic2);
            graphiclist.Add(graphic3);
            graphiclist.Add(graphic4);
            graphiclist.Add(graphic5);
            graphiclist.Add(graphic6);
            graphiclist.Add(graphic7);
            graphiclist.Add(graphic8);
            graphiclist.Add(graphic9);
            graphiclist.Add(graphic10);
            graphiclist.Add(graphic11);
            graphiclist.Add(graphic12);
            graphiclist.Add(graphic13);
            graphiclist.Add(graphic14);
            graphiclist.Add(graphic15);
            graphiclist.Add(graphic16);
            graphiclist.Add(graphic17);
            graphiclist.Add(graphic18);
            graphiclist.Add(graphic19);
            graphiclist.Add(graphic20);
            graphiclist.Add(graphic21);
            graphiclist.Add(graphic22);
            graphiclist.Add(graphic23);
            graphiclist.Add(graphic24);
            graphiclist.Add(graphic25);
            graphiclist.Add(graphic26);
            graphiclist.Add(graphic27);
            graphiclist.Add(graphic28);
            graphiclist.Add(graphic29);
            graphiclist.Add(graphic30);
            graphiclist.Add(graphic31);
            graphiclist.Add(graphic32);
            graphiclist.Add(graphic33);
            graphiclist.Add(graphic34);
            graphiclist.Add(graphic35);
            graphiclist.Add(graphic36);
            graphiclist.Add(graphic37);
            graphiclist.Add(graphic38);
            graphiclist.Add(graphic39);
            graphiclist.Add(graphic40);
            graphiclist.Add(graphic41);
            graphiclist.Add(graphic42);
            graphiclist.Add(graphic43);
            graphiclist.Add(graphic44);
            graphiclist.Add(graphic45);
            graphiclist.Add(graphic46);
            graphiclist.Add(graphic47);
            graphiclist.Add(graphic48);
            graphiclist.Add(graphic49);
            graphiclist.Add(graphic50);
            graphiclist.Add(graphic51);
            graphiclist.Add(graphic52);
            graphiclist.Add(graphic53);
            graphiclist.Add(graphic54);
            graphiclist.Add(graphic55);
            graphiclist.Add(graphic56);
            graphiclist.Add(graphic57);
            graphiclist.Add(graphic58);
            graphiclist.Add(graphic59);
            graphiclist.Add(graphic60);
            graphiclist.Add(graphic61);
            graphiclist.Add(graphic62);
            graphiclist.Add(graphic63);
            graphiclist.Add(graphic64);
            graphiclist.Add(graphic65);
            graphiclist.Add(graphic66);
            graphiclist.Add(graphic67);
            graphiclist.Add(graphic68);
            graphiclist.Add(graphic69);
            graphiclist.Add(graphic70);
            graphiclist.Add(graphic71);
            graphiclist.Add(graphic72);
            graphiclist.Add(graphic73);
            graphiclist.Add(graphic74);
            graphiclist.Add(graphic75);
            graphiclist.Add(graphic76);
            graphiclist.Add(graphic77);
            graphiclist.Add(graphic78);
            graphiclist.Add(graphic79);
            graphiclist.Add(graphic80);
            graphiclist.Add(graphic81);
            graphiclist.Add(graphic82);
            graphiclist.Add(graphic83);
            graphiclist.Add(graphic84);
            graphiclist.Add(graphic85);
            graphiclist.Add(graphic86);
            graphiclist.Add(graphic87);
            graphiclist.Add(graphic88);
            graphiclist.Add(graphic89);
            graphiclist.Add(graphic90);
            graphiclist.Add(graphic91);
            graphiclist.Add(graphic92);
            graphiclist.Add(graphic93);
            graphiclist.Add(graphic94);
            graphiclist.Add(graphic95);
            graphiclist.Add(graphic96);
            #endregion
           
            timer1.Enabled = true;
        }
        /// <summary>
        /// 刷新一下选择
        /// </summary>
        public void RefreshChecks()
        {
            for(int i=0;i<8;i++)
            {
                for(int j=0;j<12;j++)
                {
                    if (checks[i, j] == true)
                    {
                        if(i==i_coordinate&&j==j_coordinate&&!Enabled)
                        {
                            ((ClassGraphic)graphiclist[j * 8 + i]).BeOperating();
                        }
                        else
                        {
                            ((ClassGraphic)graphiclist[j * 8 + i]).BeChecked();
                        }  
                    }
                    else
                    {
                        ((ClassGraphic)graphiclist[j * 8 + i]).UnChecked();
                    }
                }
            }
            #region check框的勾选绑定
            bool allchecked1 = true;
            bool allchecked2 = true;
            bool allchecked3 = true;
            bool allchecked4 = true;
            bool allchecked5= true;
            bool allchecked6= true;
            bool allchecked7 = true;
            bool allchecked8 = true;
            bool allchecked9 = true;
            bool allchecked10 = true;
            bool allchecked11= true;
            bool allchecked12= true;
            bool allcheckedA = true;
            bool allcheckedB = true;
            bool allcheckedC = true;
            bool allcheckedD = true;
            bool allcheckedE = true;
            bool allcheckedF = true;
            bool allcheckedG= true;
            bool allcheckedH = true;
            bool allchecked = true;
            for (int i = 0; i < 8; i++)
            {
                allchecked1 = checks[i, 0] && allchecked1;
            }
            for (int i = 0; i < 8; i++)
            {
                allchecked2 = checks[i, 1] && allchecked2;
            }
            for (int i = 0; i < 8; i++)
            {
                allchecked3 = checks[i, 2] && allchecked3;
            }
            for (int i = 0; i < 8; i++)
            {
                allchecked4 = checks[i, 3] && allchecked4;
            }
            for (int i = 0; i < 8; i++)
            {
                allchecked5 = checks[i, 4] && allchecked5;
            }
            for (int i = 0; i < 8; i++)
            {
                allchecked6 = checks[i, 5] && allchecked6;
            }
            for (int i = 0; i < 8; i++)
            {
                allchecked7 = checks[i, 6] && allchecked7;
            }
            for (int i = 0; i < 8; i++)
            {
                allchecked8 = checks[i, 7] && allchecked8;
            }
            for (int i = 0; i < 8; i++)
            {
                allchecked9 = checks[i, 8] && allchecked9;
            }
            for (int i = 0; i < 8; i++)
            {
                allchecked10 = checks[i, 9] && allchecked10;
            }
            for (int i = 0; i < 8; i++)
            {
                allchecked11 = checks[i, 10] && allchecked11;
            }
            for (int i = 0; i < 8; i++)
            {
                allchecked12= checks[i, 11] && allchecked12;
            }
            for (int i = 0; i < 12; i++)
            {
                allcheckedA = checks[0, i] && allcheckedA;
            }
            for (int i = 0; i < 12; i++)
            {
                allcheckedB = checks[1,i] && allcheckedB;
            }
            for (int i = 0; i < 12; i++)
            {
                allcheckedC = checks[2, i] && allcheckedC;
            }
            for (int i = 0; i < 12; i++)
            {
                allcheckedD = checks[3, i] && allcheckedD;
            }
            for (int i = 0; i < 12; i++)
            {
                allcheckedE = checks[4, i] && allcheckedE;
            }
            for (int i = 0; i < 12; i++)
            {
                allcheckedF = checks[5, i] && allcheckedF;
            }
            for (int i = 0; i < 12; i++)
            {
                allcheckedG = checks[6, i] && allcheckedG;
            }
            for (int i = 0; i < 12; i++)
            {
                allcheckedH = checks[7, i] && allcheckedH;
            }
            for (int i = 0; i < 8; i++)
            {
                for(int j=0;j<12;j++)
                {
                    allchecked = checks[i, j] && allchecked;
                }
            }
            if (allchecked1 == true)
            {
                check1.Checked = true;
            }
            else
            {
                check1.Checked = false;
            }
            if (allchecked2 == true)
            {
                check2.Checked = true;
            }
            else
            {
                check2.Checked = false;
            }
            if (allchecked3 == true)
            {
                check3.Checked = true;
            }
            else
            {
                check3.Checked = false;
            }
            if (allchecked4 == true)
            {
                check4.Checked = true;
            }
            else
            {
                check4.Checked = false;
            }
            if (allchecked5 == true)
            {
                check5.Checked = true;
            }
            else
            {
                check5.Checked = false;
            }
            if (allchecked6 == true)
            {
                check6.Checked = true;
            }
            else
            {
                check6.Checked = false;
            }
            if (allchecked7 == true)
            {
                check7.Checked = true;
            }
            else
            {
                check7.Checked = false;
            }
            if (allchecked8 == true)
            {
                check8.Checked = true;
            }
            else
            {
                check8.Checked = false;
            }
            if (allchecked9 == true)
            {
                check9.Checked = true;
            }
            else
            {
                check9.Checked = false;
            }
            if (allchecked10 == true)
            {
                check10.Checked = true;
            }
            else
            {
                check10.Checked = false;
            }
            if (allchecked11 == true)
            {
                check11.Checked = true;
            }
            else
            {
                check11.Checked = false;
            }
            if (allchecked12== true)
            {
                check12.Checked = true;
            }
            else
            {
                check12.Checked = false;
            }
            if (allcheckedA == true)
            {
                checkA.Checked = true;
            }
            else
            {
                checkA.Checked = false;
            }
            if (allcheckedB == true)
            {
                checkB.Checked = true;
            }
            else
            {
                checkB.Checked = false;
            }
            if (allcheckedC == true)
            {
                checkC.Checked = true;
            }
            else
            {
                checkC.Checked = false;
            }
            if (allcheckedD == true)
            {
                checkD.Checked = true;
            }
            else
            {
                checkD.Checked = false;
            }
            if (allcheckedE== true)
            {
                checkE.Checked = true;
            }
            else
            {
                checkE.Checked = false;
            }
            if (allcheckedF == true)
            {
                checkF.Checked = true;
            }
            else
            {
                checkF.Checked = false;
            }
            if (allcheckedG == true)
            {
                checkG.Checked = true;
            }
            else
            {
                checkG.Checked = false;
            }
            if (allcheckedH == true)
            {
                checkH.Checked = true;
            }
            else
            {
                checkH.Checked = false;
            }
            if (allchecked == true)
            {
                checkAll.Checked = true;
            }
            else
            {
                checkAll.Checked = false;
            }
            #endregion

        }
        void event_stepchange(object sender, EventArgs e)
        {
            RefreshChecks();
        }
        #region 单击事件
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            checks[0, 0] = !checks[0, 0];
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            checks[1, 0] = !checks[1, 0];
        }
        private void picture3_Click(object sender, EventArgs e)
        {
            checks[2, 0] = !checks[2, 0];
        }
        private void picture4_Click(object sender, EventArgs e)
        {
            checks[3, 0] = !checks[3, 0];
        }
        private void picture5_Click(object sender, EventArgs e)
        {
            checks[4, 0] = !checks[4, 0];
        }

        private void picture6_Click(object sender, EventArgs e)
        {
            checks[5, 0] = !checks[5, 0];
        }

        private void picture7_Click(object sender, EventArgs e)
        {
            checks[6, 0] = !checks[6, 0];
        }

        private void picture8_Click(object sender, EventArgs e)
        {
            checks[7, 0] = !checks[7, 0];
        }

        private void picture9_Click(object sender, EventArgs e)
        {
            checks[0, 1] = !checks[0, 1];
        }

        private void picture10_Click(object sender, EventArgs e)
        {
            checks[1, 1] = !checks[1, 1];
        }

        private void picture11_Click(object sender, EventArgs e)
        {
            checks[2, 1] = !checks[2, 1];
        }

        private void picture12_Click(object sender, EventArgs e)
        {
            checks[3, 1] = !checks[3, 1];
        }

        private void picture13_Click(object sender, EventArgs e)
        {
            checks[4, 1] = !checks[4, 1];
        }

        private void picture14_Click(object sender, EventArgs e)
        {
            checks[5, 1] = !checks[5, 1];
        }

        private void picture15_Click(object sender, EventArgs e)
        {
            checks[6, 1] = !checks[6, 1];
        }

        private void picture16_Click(object sender, EventArgs e)
        {
            checks[7, 1] = !checks[7, 1];
        }

        private void picture17_Click(object sender, EventArgs e)
        {
            checks[0, 2] = !checks[0, 2];
        }

        private void picture18_Click(object sender, EventArgs e)
        {
            checks[1, 2] = !checks[1, 2];
        }

        private void picture19_Click(object sender, EventArgs e)
        {
            checks[2, 2] = !checks[2, 2];
        }

        private void picture20_Click(object sender, EventArgs e)
        {
            checks[3, 2] = !checks[3, 2];
        }

        private void picture21_Click(object sender, EventArgs e)
        {
            checks[4, 2] = !checks[4, 2];
        }

        private void picture22_Click(object sender, EventArgs e)
        {
            checks[5, 2] = !checks[5, 2];
        }

        private void picture23_Click(object sender, EventArgs e)
        {
            checks[6, 2] = !checks[6, 2];
        }

        private void picture24_Click(object sender, EventArgs e)
        {
            checks[7, 2] = !checks[7, 2];
        }

        private void picture25_Click(object sender, EventArgs e)
        {
            checks[0, 3] = !checks[0, 3];
        }

        private void picture26_Click(object sender, EventArgs e)
        {
            checks[1, 3] = !checks[1, 3];
        }
        
        private void picture27_Click(object sender, EventArgs e)
        {
            checks[2, 3] = !checks[2, 3];
        }

        private void picture28_Click(object sender, EventArgs e)
        {
            checks[3, 3] = !checks[3, 3];
        }

        private void picture29_Click(object sender, EventArgs e)
        {
            checks[4, 3] = !checks[4, 3];
        }

        private void picture30_Click(object sender, EventArgs e)
        {
            checks[5, 3] = !checks[5, 3];
        }

        private void picture31_Click(object sender, EventArgs e)
        {
            checks[6, 3] = !checks[6, 3];
        }

        private void picture32_Click(object sender, EventArgs e)
        {
            checks[7, 3] = !checks[7, 3];
        }

        private void picture33_Click(object sender, EventArgs e)
        {
            checks[0, 4] = !checks[0, 4];
        }

        private void picture34_Click(object sender, EventArgs e)
        {
            checks[1, 4] = !checks[1, 4];
        }

        private void picture35_Click(object sender, EventArgs e)
        {
            checks[2, 4] = !checks[2, 4];
        }

        private void picture36_Click(object sender, EventArgs e)
        {
            checks[3, 4] = !checks[3, 4];
        }

        private void picture37_Click(object sender, EventArgs e)
        {
            checks[4, 4] = !checks[4, 4];
        }

        private void picture38_Click(object sender, EventArgs e)
        {
            checks[5, 4] = !checks[5, 4];
        }

        private void picture39_Click(object sender, EventArgs e)
        {
            checks[6, 4] = !checks[6, 4];
        }

        private void picture40_Click(object sender, EventArgs e)
        {
            checks[7, 4] = !checks[7, 4];
        }

        private void picture41_Click(object sender, EventArgs e)
        {
            checks[0 , 5] = !checks[0 , 5];
        }

        private void picture42_Click(object sender, EventArgs e)
        {
            checks[1, 5] = !checks[1, 5];
        }

        private void picture43_Click(object sender, EventArgs e)
        {
            checks[2, 5] = !checks[2, 5];
        }

        private void picture44_Click(object sender, EventArgs e)
        {
            checks[3, 5] = !checks[3, 5];
        }

        private void picture45_Click(object sender, EventArgs e)
        {
            checks[4, 5] = !checks[4, 5];
        }

        private void picture46_Click(object sender, EventArgs e)
        {
            checks[5, 5] = !checks[5, 5];
        }

        private void picture47_Click(object sender, EventArgs e)
        {
            checks[6, 5] = !checks[6, 5];
        }

        private void picture48_Click(object sender, EventArgs e)
        {
            checks[7, 5] = !checks[7, 5];
        }

        private void picture49_Click(object sender, EventArgs e)
        {
            checks[0 , 6] = !checks[0 , 6];
        }

        private void picture50_Click(object sender, EventArgs e)
        {
            checks[1, 6] = !checks[1, 6];
        }

        private void picture51_Click(object sender, EventArgs e)
        {
            checks[2, 6] = !checks[2, 6];
        }

        private void picture52_Click(object sender, EventArgs e)
        {
            checks[3, 6] = !checks[3, 6];
        }

        private void picture53_Click(object sender, EventArgs e)
        {
            checks[4, 6] = !checks[4, 6];
        }

        private void picture54_Click(object sender, EventArgs e)
        {
            checks[5, 6] = !checks[5, 6];
        }

        private void picture55_Click(object sender, EventArgs e)
        {
            checks[6, 6] = !checks[6, 6];
        }

        private void picture56_Click(object sender, EventArgs e)
        {
            checks[7, 6] = !checks[7, 6];
        }

        private void picture57_Click(object sender, EventArgs e)
        {
            checks[0, 7] = !checks[0, 7];
        }

        private void picture58_Click(object sender, EventArgs e)
        {
            checks[1, 7] = !checks[1, 7];
        }

        private void picture59_Click(object sender, EventArgs e)
        {
            checks[2, 7] = !checks[2, 7];
        }

        private void picture60_Click(object sender, EventArgs e)
        {
            checks[3, 7] = !checks[3, 7];
        }

        private void picture61_Click(object sender, EventArgs e)
        {
            checks[4, 7] = !checks[4, 7];
        }

        private void picture62_Click(object sender, EventArgs e)
        {
            checks[5, 7] = !checks[5, 7];

        }

        private void picture63_Click(object sender, EventArgs e)
        {
            checks[6, 7] = !checks[6, 7];
        }

        private void picture64_Click(object sender, EventArgs e)
        {
            checks[7, 7] = !checks[7, 7];
        }

        private void picture65_Click(object sender, EventArgs e)
        {
            checks[0, 8] = !checks[0, 8];
        }

        private void picture66_Click(object sender, EventArgs e)
        {
            checks[1, 8] = !checks[1, 8];
        }

        private void picture67_Click(object sender, EventArgs e)
        {
            checks[2, 8] = !checks[2, 8];
        }

        private void picture68_Click(object sender, EventArgs e)
        {
            checks[3, 8] = !checks[3, 8];
        }

        private void picture69_Click(object sender, EventArgs e)
        {
            checks[4, 8] = !checks[4, 8];
        }

        private void picture70_Click(object sender, EventArgs e)
        {
            checks[5, 8] = !checks[5, 8];
        }

        private void picture71_Click(object sender, EventArgs e)
        {
            checks[6, 8] = !checks[6, 8];
        }

        private void picture72_Click(object sender, EventArgs e)
        {
            checks[7, 8] = !checks[7, 8];
        }

        private void picture73_Click(object sender, EventArgs e)
        {
            checks[0, 9] = !checks[0, 9];
        }

        private void picture74_Click(object sender, EventArgs e)
        {
            checks[1, 9] = !checks[1, 9];
        }

        private void picture75_Click(object sender, EventArgs e)
        {
            checks[2, 9] = !checks[2, 9];
        }

        private void picture76_Click(object sender, EventArgs e)
        {
            checks[3, 9] = !checks[3, 9];
        }

        private void picture77_Click(object sender, EventArgs e)
        {
            checks[4, 9] = !checks[4, 9];
        }

        private void picture78_Click(object sender, EventArgs e)
        {
            checks[5, 9] = !checks[5, 9];
        }

        private void picture79_Click(object sender, EventArgs e)
        {
            checks[6, 9] = !checks[6, 9];
        }

        private void picture80_Click(object sender, EventArgs e)
        {
            checks[7, 9] = !checks[7, 9];
        }

        private void picture81_Click(object sender, EventArgs e)
        {
            checks[0, 10] = !checks[0, 10];
        }

        private void picture82_Click(object sender, EventArgs e)
        {
            checks[1, 10] = !checks[1, 10];
        }

        private void picture83_Click(object sender, EventArgs e)
        {
            checks[2, 10] = !checks[2, 10];
        }

        private void picture84_Click(object sender, EventArgs e)
        {
            checks[3, 10] = !checks[3, 10];
        }

        private void picture85_Click(object sender, EventArgs e)
        {
            checks[4, 10] = !checks[4, 10];
        }

        private void picture86_Click(object sender, EventArgs e)
        {
            checks[5, 10] = !checks[5, 10];
        }

        private void picture87_Click(object sender, EventArgs e)
        {
            checks[6, 10] = !checks[6, 10];
        }

        private void picture88_Click(object sender, EventArgs e)
        {
            checks[7, 10] = !checks[7, 10];
        }

        private void picture89_Click(object sender, EventArgs e)
        {
            checks[0, 11] = !checks[0, 11];
        }

        private void picture90_Click(object sender, EventArgs e)
        {
            checks[1, 11] = !checks[1, 11];
        }

        private void picture91_Click(object sender, EventArgs e)
        {
            checks[2, 11] = !checks[2, 11];
        }

        private void picture92_Click(object sender, EventArgs e)
        {
            checks[3, 11] = !checks[3, 11];
        }

        private void picture93_Click(object sender, EventArgs e)
        {
            checks[4, 11] = !checks[4, 11];
        }

        private void picture94_Click(object sender, EventArgs e)
        {
            checks[5, 11] = !checks[5, 11];
        }

        private void picture95_Click(object sender, EventArgs e)
        {
            checks[6, 11] = !checks[6, 11];
        }

        private void picture96_Click(object sender, EventArgs e)
        {
            checks[7, 11] = !checks[7, 11];
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i_check.Temp != i_coordinate)
            {
                i_check.Temp = i_Coordinate;
                return;
            }
            if (j_check.Temp != j_coordinate)
            {
                j_check.Temp = j_coordinate;
                return;
            }
            for (int i=0;i<8;i++)
            {
                for(int j=0;j<12;j++)
                { 
                   if( ((EventCheckChange)checklist[j * 8 + i]).Temp != checks[i, j])
                    {
                        ((EventCheckChange)checklist[j * 8 + i]).Temp = checks[i, j];
                        return;
                    }
                }
            }
        }
        #region check控件的事件响应函数
        private void check1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (check1.Checked==true)
            {
                for(int i=0;i<8;i++)
                {
                    checks[i, 0] = true;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 0] = false;
                }
            }
        }

        private void check2_CheckedChanged(object sender, EventArgs e)
        {
            if (check2.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 1] = true;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 1] = false;
                }
            }
        }

        private void check3_CheckedChanged(object sender, EventArgs e)
        {
            if (check3.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 2] = true;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 2] = false;
                }
            }
        }

        private void check4_CheckedChanged(object sender, EventArgs e)
        {
            if (check4.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 3] = true;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 3] = false;
                }
            }
        }

        private void check5_CheckedChanged(object sender, EventArgs e)
        {
            if (check5.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 4] = true;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 4] = false;
                }
            }
        }

        private void check6_CheckedChanged(object sender, EventArgs e)
        {
            if (check6.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 5] = true;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 5] = false;
                }
            }
        }

        private void check7_CheckedChanged(object sender, EventArgs e)
        {
            if (check7.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 6] = true;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 6] = false;
                }
            }
        }

        private void check8_CheckedChanged(object sender, EventArgs e)
        {
            if (check8.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 7] = true;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 7] = false;
                }
            }
        }

        private void check9_CheckedChanged(object sender, EventArgs e)
        {
            if (check9.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 8] = true;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 8] = false;
                }
            }
        }

        private void check10_CheckedChanged(object sender, EventArgs e)
        {
            if (check10.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 9] = true;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 9] = false;
                }
            }
        }

        private void check11_CheckedChanged(object sender, EventArgs e)
        {
            if (check11.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 10] = true;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 10] = false;
                }
            }
        }

        private void check12_CheckedChanged(object sender, EventArgs e)
        {
            if (check12.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 11] = true;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    checks[i, 11] = false;
                }
            }
        }

        private void checkA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkA.Checked == true)
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[0, i] = true;
                }
            }
            else
            {
                for (int i = 0; i <12; i++)
                {
                    checks[0, i] = false;
                }
            }
        }

        private void checkB_CheckedChanged(object sender, EventArgs e)
        {
            if (checkB.Checked == true)
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[1, i] = true;
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[1, i] = false;
                }
            }
        }

        private void checkC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkC.Checked == true)
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[2, i] = true;
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[2, i] = false;
                }
            }
        }

        private void checkD_CheckedChanged(object sender, EventArgs e)
        {
            if (checkD.Checked == true)
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[3, i] = true;
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[3, i] = false;
                }
            }
        }

        private void checkE_CheckedChanged(object sender, EventArgs e)
        {
            if (checkE.Checked == true)
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[4, i] = true;
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[4, i] = false;
                }
            }
        }

        private void checkF_CheckedChanged(object sender, EventArgs e)
        {
            if (checkF.Checked == true)
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[5, i] = true;
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[5, i] = false;
                }
            }
        }

        private void checkG_CheckedChanged(object sender, EventArgs e)
        {
            if (checkG.Checked == true)
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[6, i] = true;
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[6, i] = false;
                }
            }
        }

        private void checkH_CheckedChanged(object sender, EventArgs e)
        {
            if (checkH.Checked == true)
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[7, i] = true;
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    checks[7, i] = false;
                }
            }
        }

        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAll.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    for(int j=0;j<12;j++)
                    {
                        checks[i, j] = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    for(int j=0;j<12;j++)
                    {
                        checks[i, j] = false;
                    }
                }
            }
        }


        #endregion

  
     /*   private void Microwell_96_VisibleChanged(object sender, EventArgs e)
        {
            graphic1.Graphic_Close();
            graphic2.Graphic_Close();
            graphic3.Graphic_Close();
            graphic4.Graphic_Close();
            graphic5.Graphic_Close();
            graphic6.Graphic_Close();
            graphic7.Graphic_Close();
            graphic8.Graphic_Close();
            graphic9.Graphic_Close();
            graphic10.Graphic_Close();
            graphic11.Graphic_Close();
            graphic12.Graphic_Close();
            graphic13.Graphic_Close();
            graphic14.Graphic_Close();
            graphic15.Graphic_Close();
            graphic16.Graphic_Close();
            graphic17.Graphic_Close();
            graphic18.Graphic_Close();
            graphic19.Graphic_Close();
            graphic20.Graphic_Close();
            graphic21.Graphic_Close();
            graphic22.Graphic_Close();
            graphic23.Graphic_Close();
            graphic24.Graphic_Close();
            graphic25.Graphic_Close();
            graphic26.Graphic_Close();
            graphic27.Graphic_Close();
            graphic28.Graphic_Close();
            graphic29.Graphic_Close();
            graphic30.Graphic_Close();
            graphic31.Graphic_Close();
            graphic32.Graphic_Close();
            graphic33.Graphic_Close();
            graphic34.Graphic_Close();
            graphic35.Graphic_Close();
            graphic36.Graphic_Close();
            graphic37.Graphic_Close();
            graphic38.Graphic_Close();
            graphic39.Graphic_Close();
            graphic40.Graphic_Close();
            graphic41.Graphic_Close();
            graphic42.Graphic_Close();
            graphic43.Graphic_Close();
            graphic44.Graphic_Close();
            graphic45.Graphic_Close();
            graphic46.Graphic_Close();
            graphic47.Graphic_Close();
            graphic48.Graphic_Close();
            graphic49.Graphic_Close();
            graphic50.Graphic_Close();
            graphic51.Graphic_Close();
            graphic52.Graphic_Close();
            graphic53.Graphic_Close();
            graphic54.Graphic_Close();
            graphic55.Graphic_Close();
            graphic56.Graphic_Close();
            graphic57.Graphic_Close();
            graphic58.Graphic_Close();
            graphic59.Graphic_Close();
            graphic60.Graphic_Close();
            graphic61.Graphic_Close();
            graphic62.Graphic_Close();
            graphic63.Graphic_Close();
            graphic64.Graphic_Close();
            graphic65.Graphic_Close();
            graphic66.Graphic_Close();
            graphic67.Graphic_Close();
            graphic68.Graphic_Close();
            graphic69.Graphic_Close();
            graphic70.Graphic_Close();
            graphic71.Graphic_Close();
            graphic72.Graphic_Close();
            graphic73.Graphic_Close();
            graphic74.Graphic_Close();
            graphic75.Graphic_Close();
            graphic76.Graphic_Close();
            graphic77.Graphic_Close();
            graphic78.Graphic_Close();
            graphic79.Graphic_Close();
            graphic80.Graphic_Close();
            graphic81.Graphic_Close();
            graphic82.Graphic_Close();
            graphic83.Graphic_Close();
            graphic84.Graphic_Close();
            graphic85.Graphic_Close();
            graphic86.Graphic_Close();
            graphic87.Graphic_Close();
            graphic88.Graphic_Close();
            graphic89.Graphic_Close();
            graphic90.Graphic_Close();
            graphic91.Graphic_Close();
            graphic92.Graphic_Close();
            graphic93.Graphic_Close();
            graphic94.Graphic_Close();
            graphic95.Graphic_Close();
            graphic96.Graphic_Close();
            try
            {
                File.Delete("red.jpg");
                File.Delete("green.jpg");
                File.Delete("blackgreen.jpg");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/
    }
}
