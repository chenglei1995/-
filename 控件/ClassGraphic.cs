using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Collections;
using System.IO;
using static System.Drawing.Graphics;
using System.Windows.Forms;
using System.Drawing;

namespace 控件
{
    public class ClassGraphic
    {
        private PictureBox picturebox;
 
        public ClassGraphic()
        {
        }
        /// <summary>
        /// 结构函数
        /// </summary>
        /// <param name="PICTUREBOX"></param>
        public ClassGraphic(PictureBox PICTUREBOX)
        {
            
            picturebox = PICTUREBOX;
            if(!Directory.Exists("图片缓存文件夹"))
            {
                Directory.CreateDirectory("图片缓存文件夹");
            }
            File.Copy("red.jpg", "图片缓存文件夹"+"\\"+"red_" +picturebox.Name+".jpg",true);
            File.Copy("green.jpg", "图片缓存文件夹" + "\\" + "green_" + picturebox.Name+ ".jpg",true);
            File.Copy("blackgreen.jpg", "图片缓存文件夹" + "\\" + "blackgreen_" + picturebox.Name+ ".jpg",true);
            Image imgred = Image.FromFile("图片缓存文件夹" + "\\" + "red_" + picturebox.Name + ".jpg");
            picturebox.Image = imgred;
        }
        /// <summary>
        /// 圆被选中
        /// </summary>
        public void BeChecked()
        {
            picturebox.Image.Dispose();
            Image imggreen = Image.FromFile("图片缓存文件夹" + "\\" + "green_" + picturebox.Name + ".jpg");
            picturebox.Image = imggreen;
        }
        /// <summary>
        /// 圆不被选中
        /// </summary>
        public void UnChecked()
        {
            picturebox.Image.Dispose();
            Image imgred = Image.FromFile("图片缓存文件夹" + "\\" + "red_" + picturebox.Name + ".jpg");
            picturebox.Image = imgred;
            /*  int width = 0;
              width = picturebox.Width;
              Width = width;
              HoleBitmap = new Bitmap(width, width);//根据给定的高度和宽度创建一个位图图像
              HoleGraphics = Graphics.FromImage(HoleBitmap);//从指定的 objBitmap 对象创建 objGraphics 对象 (即在objBitmap对象中画图)
              HoleGraphics.FillRectangle(bgbush, 0, 0, Width, Width);
              HoleGraphics.DrawEllipse(pen, 0, 0, 2 * Width / 2, 2 * Width / 2);
              HoleGraphics.FillEllipse(redbush, 0, 0, 2 * Width / 2, 2 * Width / 2);
              picturebox.Image = Image.FromHbitmap(HoleBitmap.GetHbitmap());
              HoleBitmap.Dispose();
              HoleGraphics.Dispose();*/
        }
        /// <summary>
        /// 下一个将要操作的孔
        /// </summary>
        public void BeOperating()
        {
            picturebox.Image.Dispose();
            Image imgblackgreen = Image.FromFile("图片缓存文件夹" + "\\" + "blackgreen_" + picturebox.Name + ".jpg");
            picturebox.Image = imgblackgreen;

        }
        public void Graphic_Close()
        {
            picturebox.Image.Dispose();
    }
    }
    /// <summary>
    /// 检测变量变化的类
    /// </summary>
    class EventCheckChange
    {
        public delegate void tempChange(object sender, EventArgs e);
        public event tempChange OntempChange;
        bool temp;
        public bool Temp
        {
            get { return temp; }
            set
            {
                if (temp != value)
                {
                    OntempChange(this, new EventArgs());
                }
                temp = value;
            }
        }
    }
    /// <summary>
    /// 检测int变量变化的类
    /// </summary>
    class EventCheckChangeInt
    {
        public delegate void tempChange(object sender, EventArgs e);
        public event tempChange OntempChange;
        int temp;
        public int Temp
        {
            get { return temp; }
            set
            {
                if (temp != value)
                {
                    OntempChange(this, new EventArgs());
                }
                temp = value;
            }
        }
    }
}
