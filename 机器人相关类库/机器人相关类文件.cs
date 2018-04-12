using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;
using  MLApp;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using static RobotClassLib.RobotCalculate;
using System.Collections;
using System.IO.Ports;
using csLTDMC;
using System.Timers;
using System.ComponentModel;

namespace RobotClassLib
{
    /// <summary>
    /// 机器人计算类
    /// </summary>
    /* class RobotCalculate
     {
         #region 私有字段
         private static double[] alpha = new double[6] { 0, PI / 2, 0, PI / 2, -PI / 2, PI / 2 };
         private static double[] a = new double[6] { 0, 80, 260, 80, 0, 0 };
         private static double[] d = new double[] { 0, 0, 0, 300, 0, 0 };
         static private double alpha1 = alpha[0];
         static private double alpha2 = alpha[1];
         static private double alpha3 = alpha[2];
         static private double alpha4 = alpha[3];
         static private double alpha5 = alpha[4];
         static private double alpha6 = alpha[5];
         static private double a1 = a[0];
         static private double a2 = a[1];
         static private double a3 = a[2];
         static private double a4 = a[3];
         static private double a5 = a[4];
         static private double a6 = a[5];
         static private double d1 = d[0];
         static private double d2 = d[1];
         static private double d3 = d[2];
         static private double d4 = d[3];
         static private double d5 = d[4];
         static private double d6 = d[5];
         #endregion
         /// <summary>
         /// Alpha[n]为从Zn-1轴道Zn轴绕Xn-1旋转的角度
         /// </summary>
         public static double[] Alpha
         {
             get
             {
                 return alpha;
             }
         }
         /// <summary>
         /// A[n]为从Zn-1轴到Zn轴沿Xn-1测量的距离
         /// </summary>
         public static double[] A
         {
             get
             {
                 return a;
             }
         }
         /// <summary>
         /// D[n]为从Xn-1轴到Xn轴沿Zn测量的距离
         /// </summary>
         public static double[] D
         {
             get
             {
                 return d;
             }
         }
         /// <summary>
         /// 机器人运动学正解，静态方法，参数theta为角度向量值，返回一个4x4的位姿矩阵
         /// </summary>
         /// <param name="theta"></param>
         /// <returns></returns>
         static public double[,] PosSolution(double[] theta)
         {
             double[,] solution = new double[4,4];
             #region 简写
             double theta1 = theta[0];
             double theta2 = theta[1];
             double theta3 = theta[2];
             double theta4 = theta[3];
             double theta5 = theta[4];
             double theta6 = theta[5];
             #endregion
             if (theta.Length!=6)
             {
                 throw new Exception("参数不是长度为6的向量");

             }
             solution[0, 0] = Cos(theta1) * (Cos(theta2 + theta3) *( Cos(theta4) * Cos(theta5) * Cos(theta6) - Sin(theta4) * Sin(theta6)) - Sin(theta2 + theta3) * Sin(theta4) * Cos(theta6)) + Sin(theta1) * (Sin(theta4) * Cos(theta5) * Cos(theta6) + Cos(theta4) * Sin(theta6));
             solution[1, 0] = Sin(theta1) * (Cos(theta2 + theta3) * (Cos(theta4) * Cos(theta5) * Cos(theta6) - Sin(theta4) * Sin(theta6)) - Sin(theta2 + theta3) * Sin(theta4) * Cos(theta6)) - Cos(theta1) * (Sin(theta4) * Cos(theta5) * Cos(theta6) + Cos(theta4) * Sin(theta6));
             solution[2, 0] = -Sin(theta2 + theta3) * (Cos(theta4) * Cos(theta5) * Cos(theta6) - Sin(theta4) * Sin(theta6)) + Cos(theta2 + theta3) * Sin(theta4) * Cos(theta6);
             solution[0, 1] = Cos(theta1) * (-Cos(theta2 + theta3) * (Cos(theta4) * Cos(theta5) * Sin(theta6) + Sin(theta4) * Cos(theta6) + Sin(theta2 + theta3) * Sin(theta5) * Sin(theta6))) - Sin(theta1) * (Sin(theta4) * Cos(theta5) * Sin(theta6) - Cos(theta4) * Cos(theta6));
             solution[1, 1] = Sin(theta1) * (-Cos(theta2 + theta3) * (Cos(theta4) * Cos(theta5) * Sin(theta6) + Sin(theta4) * Cos(theta6)) + Sin(theta2 + theta3) * Sin(theta5) * Sin(theta6)) + Cos(theta1) * (Sin(theta4) * Cos(theta5) * Sin(theta6) - Cos(theta4) * Cos(theta6));
             solution[2, 1] = -Sin(theta2 + theta3) * (Cos(theta4) * Cos(theta5) * Sin(theta6) + Sin(theta4) * Cos(theta6)) - Cos(theta2 + theta3) * Sin(theta5) * Sin(theta6);
             solution[0, 2] = Cos(theta1) * (Cos(theta2 + theta3) * Cos(theta4) * Sin(theta5) + Sin(theta2 + theta3) * Cos(theta5)) + Sin(theta1) * Sin(theta4) * Sin(theta5);
             solution[1, 2] = Sin(theta1) * (Cos(theta2 + theta3) * Cos(theta4) * Sin(theta5) + Sin(theta2 + theta3) * Cos(theta5)) - Cos(theta1) * Sin(theta4) * Sin(theta5);
             solution[2, 2] = Sin(theta2 + theta3) * Cos(theta4) * Sin(theta5) - Cos(theta2 + theta3) * Cos(theta5);
             solution[0, 3] = Cos(theta1) * (a4 * Cos(theta2 + theta3) + d4 * Sin(theta2 + theta3) + a3 * Cos(theta2) + a2);
             solution[1, 3] = Sin(theta1) * (a4 * Cos(theta2 + theta3) + d4 * Sin(theta2 + theta3) + a3 * Cos(theta2) + a2);
             solution[2, 3] = a4 * Sin(theta2 + theta3) - d4 * Cos(theta2 + theta3) + a3 * Sin(theta2);
             solution[3, 3] = 1;
             solution[3, 0] = 0;
             solution[3, 1] = 0;
             solution[3, 2] = 0;
             return solution;
         }
         /// <summary>
         /// 机器人运动学逆解，静态方法，参数positi为机械臂末端位姿，返回一个6维角度向量
         /// </summary>
         /// <param name="position"></param>
         /// <returns></returns>
         static public double[] InvSolution(double[,]position )
         {
             double[] theta = new double[6];
             if (position.Length!=16)
             {
                 throw new Exception("参数不是长度为6的向量");
             }
             #region 简写
             double nx = position[0, 0];
             double ny = position[1, 0];
             double nz = position[2, 0];
             double ox = position[0, 1];
             double oy = position[1, 1];
             double oz = position[2, 1];
             double ax = position[0, 2];
             double ay = position[1, 2];
             double az = position[2, 2];
             double px = position[0, 3];
             double py = position[1, 3];
             double pz = position[2, 3];
             double theta1 = theta[0];
             double theta2 = theta[1];
             double theta3 = theta[2];
             double theta4 = theta[3];
             double theta5 = theta[4];
             double theta6 = theta[5];
             double K = new double();
             double Rho = new double();
             double theta23 = new double();
             double sin5 = new double();
             double cos5 = new double();
             double sin6 = new double();
             double cos6 = new double();
             #endregion
             theta1 = Atan2(py , px);//theta1的值
             K = (Pow(px, 2) + Pow(py, 2) + Pow(pz, 2) + Pow(a2, 2) - 2 * a2 * px * Cos(theta1) - 2 * a2 * py * Sin(theta1) - Pow(a4, 2) - Pow(d4, 2) - Pow(a3, 2)) / (2 * a3);
             Rho = Sqrt(Pow(a4, 2) + Pow(d4, 2));
             theta3 = Atan2(d4 , a4) - Atan2(Sqrt(Pow(Rho, 2) - Pow(K, 2)) , K);//theta3的值
             theta23 = Atan2((pz * (a4 + a3 * Cos(theta3)) + (d4 + a3 * Sin(theta3)) * (px * Cos(theta1) + py * Sin(theta1) - az)) , ((a4 + a3 * Cos(theta3)) * (px * Cos(theta1) + py * Sin(theta1) - az) - pz * (a4 + a3 * Cos(theta3))));
             theta2 = theta23 - theta3;//theta2的值
             theta4 = Atan2((ax * Sin(theta1) - ay * Cos(theta1)) , (ax * Cos(theta1) * Cos(theta2 + theta3) + ay * Sin(theta1) * Cos(theta2 + theta3) + az * Sin(theta2 + theta3)));
             sin5 = ax * (Cos(theta1) * Cos(theta2 + theta3) * Cos(theta4) + Sin(theta1) * Sin(theta4)) + ay * (Sin(theta1) * Cos(theta2 + theta3) * Cos(theta4) - Cos(theta1) * Sin(theta4)) + az * Sin(theta2 + theta3) * Cos(theta4);
             cos5 = ax * Cos(theta1) * Sin(theta2 + theta3) + ay * Sin(theta1) * Sin(theta2 + theta3) - az * Cos(theta2 + theta3);
             theta5 = Atan2(sin5 , cos5);//theta5的值
             sin6 = nx * (Sin(theta1) * Cos(theta4) - Cos(theta1) * Cos(theta2 + theta3) * Sin(theta4)) - ny * (Sin(theta1) * Cos(theta2 + theta3) * Sin(theta4) + Cos(theta1) * Cos(theta4)) - nz * Sin(theta2 + theta3) * Sin(theta4);
             cos6 = ox * (Sin(theta1) * Cos(theta4) - Cos(theta1) * Cos(theta2 + theta3) * Sin(theta4)) - oy * (Sin(theta1) * Cos(theta2 + theta3) * Sin(theta4) + Cos(theta1) * Cos(theta4)) - oz * Sin(theta2 + theta3) * Sin(theta4);
             theta6 =Atan2(sin6 , cos6);
             #region 给返回值赋值
             theta[0] = theta1;
             theta[1] = theta2;
             theta[2] = theta3;
             theta[3] = theta4;
             theta[4] = theta5;
             theta[5] = theta6;
             #endregion
             return theta;
         }
     }*/
    /*
    class RobotCalculate
    {
        static  private MLApp.MLApp matlab = null;
        /// <summary>
        /// 初始化matlab
        /// </summary>
        static public void InitializationCalculate()
        {
            Type matlabAppType = System.Type.GetTypeFromProgID("Matlab.Application");
            matlab = System.Activator.CreateInstance(matlabAppType) as MLApp.MLApp;
            matlab.Visible = 1;
            string path_project = Directory.GetCurrentDirectory();   //工程文件的路径，如bin下面的debug  
            string path_matlab = "cd('" + path_project + @"\mfunction"+"')";     //自定义matlab工作路径    这里我注释调用 
            matlab.Execute(path_matlab);
            path_matlab = "userpath('" + path_project + @"\mfunction" + "')";
            matlab.Execute(path_matlab);
            path_matlab = "savepath";
            matlab.Execute(path_matlab);
            matlab.Execute("clear all");//这条语句很重要
        }
        /// <summary>
        /// 关闭matlab
        /// </summary>
        static public void CloseMatlab()
        {

            matlab.Quit();
            matlab = null;
        }
        /// <summary>
        /// 机器人运动学正解，theta为输入角度向量，返回一个4x4的位姿矩阵
        /// </summary>
        /// <param name="alpha"></param>
        /// <returns></returns>
        static public double[,] PosSolution(double[] theta)
        {
            matlab.Execute("clear all");//这条语句重要 
            string Theta = '[' + string.Join(",", theta) + ']';//Theta值在matlab中的表示方法
            double[,] result;//返回位姿矩阵
            string command;
            command = @"T=PosSolution(" + Theta + ")";
            matlab.Execute(command);
            try
            {

                result = matlab.GetVariable("T", "base");//获取结果，就是传入base字符串，而不是其他的。

            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
        /// <summary>
        /// 机器人运动学逆解，TR为输入的4x4位姿矩阵，输出一个6维的向量矩阵
        /// </summary>
        /// <param name="TR"></param>
        /// <returns></returns>
        static public double[] InvSolution(double[,] TR)
        {
            matlab.Execute("clear all");//这条语句很重要 
            double[] result = new double[6];//返回角度向量
            double[,] Q;
            string Tr = null;
            Tr = Tr + '[';
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Tr = Tr + TR[i, j].ToString();
                    if (j == 3 && i != 3)
                    {
                        Tr = Tr + ';';
                    }
                    else if (j != 3)
                    {
                        Tr = Tr + ',';
                    }
                }
            }
            Tr = Tr + ']';
            string command;
            command = @"Q=InvSolution(" + Tr + ")";
            matlab.Execute(command);
            try
            {
                Q = matlab.GetVariable("Q", "base");//获取结果，就是传入base字符串，而不是其他的。
            }
            catch (Exception)
            {
                throw;
            }
            for (int i = 0; i < 6; i++)
            {
                result[i] = Q[0, i];
            }
            return result;
        }
        /// <summary>
        /// 机器人轨迹规划，Q为从状态Q0到Q1的关节空间轨迹规划，Nx6矩阵,QD,QDD分别为规划轨迹的速度和加速度，为Nx6矩阵，Q0,Q1为起始和终止关节角度向量，为长度为6的向量,N为中间点数量，TIME为预计时间
        /// </summary>
        /// <param name="Q"></param>
        /// <param name="QD"></param>
        /// <param name="QDD"></param>
        /// <param name="Q0"></param>
        /// <param name="Q1"></param>
        /// <param name="N"></param>
        static public void JTRAJ(ref double[,] Q, ref double[,] QD, ref double[,] QDD, double[] Q0, double[] Q1, int N, double TIME)
        {

            matlab.Execute("clear all");//这条语句重要 
            string Str_Q0 = null;//Q0的字符串表示方法
            Str_Q0 = Str_Q0 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q0 = Str_Q0 + Q0[i].ToString();
                if (i != 5)
                {
                    Str_Q0 = Str_Q0 + ',';
                }

            }
            Str_Q0 = Str_Q0 + ']';
            string Str_Q1 = null;//Q1的字符串表示方法
            Str_Q1 = Str_Q1 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q1 = Str_Q1 + Q1[i].ToString();
                if (i != 5)
                {
                    Str_Q1 = Str_Q1 + ',';
                }

            }
            Str_Q1 = Str_Q1 + ']';
            string command;
            command = @"[Q,QD,QDD]=FJTRAJ(" + Str_Q0 + "," + Str_Q1 + "," + N.ToString() + "," + TIME.ToString() + ")";
            matlab.Execute(command);
            try
            {

                Q = matlab.GetVariable("Q", "base");//获取结果，就是传入base字符串，而不是其他的。
                QD = matlab.GetVariable("QD", "base");//获取结果，就是传入base字符串，而不是其他的。
                QDD = matlab.GetVariable("QDD", "base");//获取结果，就是传入base字符串，而不是其他的。

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 机器人在位置Q0处实现水平变化，将机械臂末端保持水平，Q0位长度为6的向量，Q为从状态Q0到机械臂末端水平位置的关节空间轨迹规划，Nx6矩阵,QD,QDD分别为规划轨迹的速度和加速度，为Nx6矩阵。N为中间点数量，TIME为预计时间
        /// </summary>
        /// <param name="Q"></param>
        /// <param name="QD"></param>
        /// <param name="QDD"></param>
        /// <param name="Q0"></param>
        /// <param name="N"></param>
        static public void ToLevel(ref double[,] Q, ref double[,] QD, ref double[,] QDD, double[] Q0, int N, double TIME)
        {
            matlab.Execute("clear all");//这条语句重要 
            string Str_Q0 = null;//Q0的字符串表示方法
            Str_Q0 = Str_Q0 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q0 = Str_Q0 + Q0[i].ToString();
                if (i != 5)
                {
                    Str_Q0 = Str_Q0 + ',';
                }
            }
            Str_Q0 = Str_Q0 + ']';
            string command;
            command = @"[Q,QD,QDD]=ToLevel(" + Str_Q0 + "," + N.ToString() + "," + TIME.ToString() + ")";
            matlab.Execute(command);
            try
            {

                Q = matlab.GetVariable("Q", "base");//获取结果，就是传入base字符串，而不是其他的。
                QD = matlab.GetVariable("QD", "base");//获取结果，就是传入base字符串，而不是其他的。
                QDD = matlab.GetVariable("QDD", "base");//获取结果，就是传入base字符串，而不是其他的。

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 机器人在位置Q0处实现垂直，将机械臂末端保持垂直，Q0位长度为6的向量，Q为从状态Q0到机械臂末端垂直位置的关节空间轨迹规划，Nx6矩阵,QD,QDD分别为规划轨迹的速度和加速度，为Nx6矩阵。N为中间点数量，TIME为预计时间
        /// </summary>
        /// <param name="Q"></param>
        /// <param name="QD"></param>
        /// <param name="QDD"></param>
        /// <param name="Q0"></param>
        /// <param name="N"></param>
        static public void ToVertical(ref double[,] Q, ref double[,] QD, ref double[,] QDD, double[] Q0, int N, double TIME)
        {
            matlab.Execute("clear all");//这条语句重要 
            string Str_Q0 = null;//Q0的字符串表示方法
            Str_Q0 = Str_Q0 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q0 = Str_Q0 + Q0[i].ToString();
                if (i != 5)
                {
                    Str_Q0 = Str_Q0 + ',';
                }
            }
            Str_Q0 = Str_Q0 + ']';
            string command;
            command = @"[Q,QD,QDD]=ToVertical(" + Str_Q0 + "," + N.ToString() + "," + TIME.ToString() + ")";
            matlab.Execute(command);
            try
            {

                Q = matlab.GetVariable("Q", "base");//获取结果，就是传入base字符串，而不是其他的。
                QD = matlab.GetVariable("QD", "base");//获取结果，就是传入base字符串，而不是其他的。
                QDD = matlab.GetVariable("QDD", "base");//获取结果，就是传入base字符串，而不是其他的。

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 机器人在保持现有姿态运动，Q0位为现在的角度向量，长度为6，Vn为位置变化向量，长度为3，Q为从状态Q0到变化后位置的关节空间轨迹规划，Nx6矩阵,QD,QDD分别为规划轨迹的速度和加速度，为Nx6矩阵。N为中间点数量，TIME为预计时间
        /// </summary>
        /// <param name="Q"></param>
        /// <param name="QD"></param>
        /// <param name="QDD"></param>
        /// <param name="Q0"></param>
        /// <param name="N"></param>
        static public void ToTrans(ref double[,] Q, ref double[,] QD, ref double[,] QDD, double[] Q0, double[] Vn, int N, double TIME)
        {
            matlab.Execute("clear all");//这条语句重要 
            string Str_Q0 = null;//Q0的字符串表示方法
            Str_Q0 = Str_Q0 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q0 = Str_Q0 + Q0[i].ToString();
                if (i != 5)
                {
                    Str_Q0 = Str_Q0 + ',';
                }
            }
            Str_Q0 = Str_Q0 + ']';
            string Str_Vn = null;
            Str_Vn = Str_Vn + '[';
            for (int i = 0; i < 3; i++)
            {
                Str_Vn = Str_Vn + Vn[i].ToString();
                if (i != 2)
                {
                    Str_Vn = Str_Vn + ',';
                }
            }
            Str_Vn = Str_Vn + ']';
            string command;
            command = @"[Q,QD,QDD]=ToTrans(" + Str_Q0 + "," + Str_Vn + "," + N.ToString() + "," + TIME.ToString() + ")";
            matlab.Execute(command);
            try
            {

                Q = matlab.GetVariable("Q", "base");//获取结果，就是传入base字符串，而不是其他的。
                QD = matlab.GetVariable("QD", "base");//获取结果，就是传入base字符串，而不是其他的。
                QDD = matlab.GetVariable("QDD", "base");//获取结果，就是传入base字符串，而不是其他的。

            }
            catch (Exception)
            {

                throw;
            }
        }
 
    }*/
    public class RobotCalculate
    {
        static private MLApp.MLApp matlab = null;
        static private bool ifplot = true;//是否可以仿真
        /// <summary>
        /// 初始化matlab
        /// </summary>
        static public void InitializationCalculate()
        {
            Type matlabAppType = System.Type.GetTypeFromProgID("Matlab.Application");
            matlab = System.Activator.CreateInstance(matlabAppType) as MLApp.MLApp;
            matlab.Visible = 0;
            string path_project = Directory.GetCurrentDirectory();   //工程文件的路径，如bin下面的debug  
            string path_matlab = "cd('" + path_project + @"\mfunction" + "')";     //自定义matlab工作路径    这里我注释调用 
            matlab.Execute(path_matlab);
            path_matlab = "userpath('" + path_project + @"\mfunction" + "')";
            matlab.Execute(path_matlab);
            path_matlab = "savepath";
            matlab.Execute(path_matlab);
            matlab.Execute("clear all");//这条语句很重要
        }
        /// <summary>
        /// 关闭matlab
        /// </summary>
        static public void CloseMatlab()
        {

            matlab.Quit();
            matlab = null;
        }
        /// <summary>
        /// 机器人运动学正解，theta为输入角度向量，返回一个4x4的位姿矩阵
        /// </summary>
        /// <param name="alpha"></param>
        /// <returns></returns>
        static public double[,] PosSolution(double[] theta)
        {
            ifplot = false;
            string Theta = '[' + string.Join(",", theta) + ']';//Theta值在matlab中的表示方法
            double[,] result;//返回位姿矩阵
            string command;
            command = "clear all;" + @"T=PosSolution(" + Theta + ")";
            matlab.Execute(command);
            try
            {

                result = matlab.GetVariable("T", "base");//获取结果，就是传入base字符串，而不是其他的。

            }
            catch (Exception)
            {

                throw;
            }

            ifplot = true;//释放资源
            return result;
        }
        /// <summary>
        /// 机器人运动学逆解，TR为输入的4x4位姿矩阵，输出一个6维的向量矩阵
        /// </summary>
        /// <param name="TR"></param>
        /// <returns></returns>
        static public double[] InvSolution(double[,] TR)
        {
            ifplot = false;
            matlab.Execute("clear all");//这条语句很重要 
            double[] result = new double[6];//返回角度向量
            double[,] Q;
            string Tr = null;
            Tr = Tr + '[';
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Tr = Tr + TR[i, j].ToString();
                    if (j == 3 && i != 3)
                    {
                        Tr = Tr + ';';
                    }
                    else if (j != 3)
                    {
                        Tr = Tr + ',';
                    }
                }
            }
            Tr = Tr + ']';
            string command;
            command = @"Q=InvSolution(" + Tr + ")";
            matlab.Execute(command);
            try
            {
                Q = matlab.GetVariable("Q", "base");//获取结果，就是传入base字符串，而不是其他的。
            }
            catch (Exception)
            {
                throw;
            }
            for (int i = 0; i < 6; i++)
            {
                result[i] = Q[0, i];
            }
            ifplot = true;
            return result;
        }
        /// <summary>
        /// 机器人轨迹规划，Q为从状态Q0到Q1的关节空间轨迹规划，Nx6矩阵,QD,QDD分别为规划轨迹的速度和加速度，为Nx6矩阵，Q0,Q1为起始和终止关节角度向量，为长度为6的向量,N为中间点数量，TIME为预计时间
        /// </summary>
        /// <param name="Q"></param>
        /// <param name="QD"></param>
        /// <param name="QDD"></param>
        /// <param name="Q0"></param>
        /// <param name="Q1"></param>
        /// <param name="N"></param>
        static public void JTRAJ(ref double[,] Q, ref double[,] QD, ref double[,] QDD, double[] Q0, double[] Q1, int N, double TIME)
        {
            ifplot = false;
            matlab.Execute("clear all");//这条语句重要 
            string Str_Q0 = null;//Q0的字符串表示方法
            Str_Q0 = Str_Q0 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q0 = Str_Q0 + Q0[i].ToString();
                if (i != 5)
                {
                    Str_Q0 = Str_Q0 + ',';
                }

            }
            Str_Q0 = Str_Q0 + ']';
            string Str_Q1 = null;//Q1的字符串表示方法
            Str_Q1 = Str_Q1 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q1 = Str_Q1 + Q1[i].ToString();
                if (i != 5)
                {
                    Str_Q1 = Str_Q1 + ',';
                }

            }
            Str_Q1 = Str_Q1 + ']';
            string command;
            command = @"[Q,QD,QDD]=FJTRAJ(" + Str_Q0 + "," + Str_Q1 + "," + N.ToString() + "," + TIME.ToString() + ")";
            matlab.Execute(command);
            try
            {

                Q = matlab.GetVariable("Q", "base");//获取结果，就是传入base字符串，而不是其他的。
                QD = matlab.GetVariable("QD", "base");//获取结果，就是传入base字符串，而不是其他的。
                QDD = matlab.GetVariable("QDD", "base");//获取结果，就是传入base字符串，而不是其他的。

            }
            catch (Exception)
            {

                throw;
            }
            ifplot = true;
        }
        /// <summary>
        /// 机器人笛卡尔坐标系下的轨迹规划
        /// </summary>
        /// <param name="Q"></param>
        /// <param name="Q0"></param>
        /// <param name="Q1"></param>
        /// <param name="N"></param>
        static public void CTRAJ(ref double[,] Q,double[] Q0,double[] Q1,int N)
        {
            ifplot = false;
            matlab.Execute("clear all");//这条语句重要 
            string Str_Q0 = null;//Q0的字符串表示方法
            Str_Q0 = Str_Q0 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q0 = Str_Q0 + Q0[i].ToString();
                if (i != 5)
                {
                    Str_Q0 = Str_Q0 + ',';
                }
            }
            Str_Q0 = Str_Q0 + ']';
            string Str_Q1 = null;//Q1的字符串表示方法
            Str_Q1 = Str_Q1 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q1 = Str_Q1 + Q1[i].ToString();
                if (i != 5)
                {
                    Str_Q1 = Str_Q1 + ',';
                }

            }
            Str_Q1 = Str_Q1 + ']';
            string command;
            command = @"Q=FCTRAJ(" + Str_Q0 + "," + Str_Q1 + "," + N.ToString() + ")";
            matlab.Execute(command);
            try
            {

                Q = matlab.GetVariable("Q", "base");//获取结果，就是传入base字符串，而不是其他的。

            }
            catch (Exception)
            {

                throw;
            }
            ifplot = true;
        }
        /// <summary>
        /// 两点去N个中间点，水平间距相等
        /// </summary>
        /// <param name="Vn"></param>
        /// <param name="Q0"></param>
        /// <param name="Q1"></param>
        /// <param name="N"></param>
        static public void PathWay(ref double[,] Q, double[] Q0,double[] Q1,int N)
        {
            ifplot = false;
            matlab.Execute("clear all");//这条语句重要 
            string Str_Q0 = null;//Q0的字符串表示方法
            Str_Q0 = Str_Q0 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q0 = Str_Q0 + Q0[i].ToString();
                if (i != 5)
                {
                    Str_Q0 = Str_Q0 + ',';
                }
            }
            Str_Q0 = Str_Q0 + ']';
            string Str_Q1 = null;//Q1的字符串表示方法
            Str_Q1 = Str_Q1 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q1 = Str_Q1 + Q1[i].ToString();
                if (i != 5)
                {
                    Str_Q1 = Str_Q1 + ',';
                }

            }
            Str_Q1 = Str_Q1 + ']';
            string command;
            command = @"Q=PathWay(" + Str_Q0 + "," + Str_Q1 + "," + N.ToString() + ")";
            matlab.Execute(command);
            try
            {

                Q= matlab.GetVariable("Q", "base");//获取结果，就是传入base字符串，而不是其他的。

            }
            catch (Exception)
            {

                throw;
            }
            ifplot = true;
        }
        /// <summary>
        /// 机器人在位置Q0处实现水平变化，将机械臂末端保持水平，Q0位长度为6的向量，Q为从状态Q0到机械臂末端水平位置的关节空间轨迹规划，Nx6矩阵,QD,QDD分别为规划轨迹的速度和加速度，为Nx6矩阵。N为中间点数量，TIME为预计时间
        /// </summary>
        /// <param name="Q"></param>
        /// <param name="QD"></param>
        /// <param name="QDD"></param>
        /// <param name="Q0"></param>
        /// <param name="N"></param>
        static public void ToLevel(ref double[,] Q, ref double[,] QD, ref double[,] QDD, double[] Q0, int N, double TIME)
        {
            ifplot = false;
            matlab.Execute("clear all");//这条语句重要 
            string Str_Q0 = null;//Q0的字符串表示方法
            Str_Q0 = Str_Q0 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q0 = Str_Q0 + Q0[i].ToString();
                if (i != 5)
                {
                    Str_Q0 = Str_Q0 + ',';
                }
            }
            Str_Q0 = Str_Q0 + ']';
            string command;
            command = @"[Q,QD,QDD]=ToLevel(" + Str_Q0 + "," + N.ToString() + "," + TIME.ToString() + ")";
            matlab.Execute(command);
            try
            {

                Q = matlab.GetVariable("Q", "base");//获取结果，就是传入base字符串，而不是其他的。
                QD = matlab.GetVariable("QD", "base");//获取结果，就是传入base字符串，而不是其他的。
                QDD = matlab.GetVariable("QDD", "base");//获取结果，就是传入base字符串，而不是其他的。

            }
            catch (Exception)
            {

                throw;
            }
            ifplot = true;
        }
        /// <summary>
        /// 机器人在位置Q0处实现垂直，将机械臂末端保持垂直，Q0位长度为6的向量，Q为从状态Q0到机械臂末端垂直位置的关节空间轨迹规划，Nx6矩阵,QD,QDD分别为规划轨迹的速度和加速度，为Nx6矩阵。N为中间点数量，TIME为预计时间
        /// </summary>
        /// <param name="Q"></param>
        /// <param name="QD"></param>
        /// <param name="QDD"></param>
        /// <param name="Q0"></param>
        /// <param name="N"></param>
        static public void ToVertical(ref double[,] Q, ref double[,] QD, ref double[,] QDD, double[] Q0, int N, double TIME)
        {
            ifplot = false;
            matlab.Execute("clear all");//这条语句重要 
            string Str_Q0 = null;//Q0的字符串表示方法
            Str_Q0 = Str_Q0 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q0 = Str_Q0 + Q0[i].ToString();
                if (i != 5)
                {
                    Str_Q0 = Str_Q0 + ',';
                }
            }
            Str_Q0 = Str_Q0 + ']';
            string command;
            command = @"[Q,QD,QDD]=ToVertical(" + Str_Q0 + "," + N.ToString() + "," + TIME.ToString() + ")";
            matlab.Execute(command);
            try
            {
                Q = matlab.GetVariable("Q", "base");//获取结果，就是传入base字符串，而不是其他的。
                QD = matlab.GetVariable("QD", "base");//获取结果，就是传入base字符串，而不是其他的。
                QDD = matlab.GetVariable("QDD", "base");//获取结果，就是传入base字符串，而不是其他的。

            }
            catch (Exception)
            {

                throw;
            }
            ifplot = true;
        }
        /// <summary>
        /// 机器人在保持现有姿态运动，Q0位为现在的角度向量，长度为6，Vn为位置变化向量，长度为3，Q为从状态Q0到变化后位置的关节空间轨迹规划，Nx6矩阵,QD,QDD分别为规划轨迹的速度和加速度，为Nx6矩阵。N为中间点数量，TIME为预计时间
        /// </summary>
        /// <param name="Q"></param>
        /// <param name="QD"></param>
        /// <param name="QDD"></param>
        /// <param name="Q0"></param>
        /// <param name="N"></param>
        static public void ToTrans(ref double[,] Q, ref double[,] QD, ref double[,] QDD, double[] Q0, double[] Vn, int N, double TIME)
        {
            ifplot = false;
            matlab.Execute("clear all");//这条语句重要 
            string Str_Q0 = null;//Q0的字符串表示方法
            Str_Q0 = Str_Q0 + '[';
            for (int i = 0; i < 6; i++)
            {
                Str_Q0 = Str_Q0 + Q0[i].ToString();
                if (i != 5)
                {
                    Str_Q0 = Str_Q0 + ',';
                }
            }
            Str_Q0 = Str_Q0 + ']';
            string Str_Vn = null;
            Str_Vn = Str_Vn + '[';
            for (int i = 0; i < 3; i++)
            {
                Str_Vn = Str_Vn + Vn[i].ToString();
                if (i != 2)
                {
                    Str_Vn = Str_Vn + ',';
                }
            }
            Str_Vn = Str_Vn + ']';
            string command;
            command = @"[Q,QD,QDD]=ToTrans(" + Str_Q0 + "," + Str_Vn + "," + N.ToString() + "," + TIME.ToString() + ")";
            matlab.Execute(command);
            try
            {

                Q = matlab.GetVariable("Q", "base");//获取结果，就是传入base字符串，而不是其他的。
                QD = matlab.GetVariable("QD", "base");//获取结果，就是传入base字符串，而不是其他的。
                QDD = matlab.GetVariable("QDD", "base");//获取结果，就是传入base字符串，而不是其他的。

            }
            catch (Exception)
            {

                throw;
            }
            ifplot = true;
        }
        /// <summary>
        /// 机械臂仿真
        /// </summary>
        /// <param name="q"></param>
        static public void PaintRobot(double[] q)
        {
            if (ifplot)
            {

                string Str_q = null;//q的字符串表示方法
                Str_q = Str_q + '[';
                for (int i = 0; i < 6; i++)
                {
                    Str_q = Str_q + q[i].ToString();
                    if (i != 5)
                    {
                        Str_q = Str_q + ',';
                    }
                }
                Str_q = Str_q + ']';
                string command;
                command = "clear all;" + @"PlotRobot(" + Str_q + ")";
                matlab.Execute(command);
            }

        }
    }
    /*
    /// <summary>
    /// 机械臂仿真函数
    /// </summary>
    class RobotPlot
    {
        static public MLApp.MLApp matlab1 = new MLApp.MLApp();
        /// <summary>
        /// 初始化matlab
        /// </summary>
        static public void InitializationPlot()
        {
            
            matlab1.Visible = 0;
            string path_project = Directory.GetCurrentDirectory();   //工程文件的路径，如bin下面的debug  
            string path_matlab = "cd('" + path_project + @"\mfunction" + "')";     //自定义matlab工作路径    这里我注释调用 
            matlab1.Execute(path_matlab);
            path_matlab = "userpath('" + path_project + @"\mfunction" + "')";
            matlab1.Execute(path_matlab);
            path_matlab = "savepath";
            matlab1.Execute(path_matlab);
            matlab1.Execute("clear all");//这条语句很重要
        }
    }*/
    /// <summary>
    /// 机器人类
    /// </summary>
    public class Robot
    {
        #region 字段
        public bool ifmove = true;//是否运动的标志位，只有当ifmove为true时才运动
        internal ushort card_id;
        private bool plotrobot = false;//是否图形仿真，默认为不仿真
        internal ArrayList q = new ArrayList();//位置集合，元素为1X6的double向量
        internal ArrayList qd = new ArrayList();//速度集合，元素为1X6的doulbe向量
        internal ArrayList qdd = new ArrayList();//加速度集合，元素为1X6的double向量
        internal ArrayList vel_vector = new ArrayList();//机械臂插补速度，保留
        public double default_vel = 0.4;//默认速度rad/s
        internal ArrayList stepwork = new ArrayList();//每一步的工作集合，元素为int量，0运动，1延时，2泵，3打孔，4机械爪
        internal int step = -1;//表示正在向q[step]位置移动
        internal int substep = -1;//细分step
        internal ArrayList stepnum = new ArrayList();//每一步细分集合，元素为int量
        private bool pumpenable = false;//泵是否激活
        private bool holeenable = false;//打孔器是否激活
        private bool clawenable = false;//机械抓是否激活
        private double equivx = 10000 * 50 / (2*PI), equivy = 10000 * 51 /(2*PI), equivz = 25600 * 51 / (2*PI), equiva = 25600 * 50 /(2*PI), equivb = 25600 * 50 / (2*PI), equivc = 25600 * 50 / (2*PI);//X、Y、Z轴脉冲当量
        internal ushort axisx = 0;//各轴标号
        internal ushort axisy = 1;
        internal ushort axisz = 2;
        internal ushort axisa = 3;
        internal ushort axisb = 4;
        internal ushort axisc = 5;
        public SerialPort ComDevice = new SerialPort();//定义端口类
        private byte[] readdate = new byte[8];//
        private string portname ;//泵连接的串口名
        private int bitrate = 0;//泵连接串口的波特率
        private double xmax_vel = 0.1 * PI;//各轴最大回零速度
        private double ymax_vel = 0.05 * PI;
        private double zmax_vel = 0.03 * PI;
        private double amax_vel = 0.2 * PI;
        private double bmax_vel = 0.2 * PI;
        private double cmax_vel = 0.2* PI;
        #endregion
        #region 属性
        /// <summary>
        /// 运动控制卡的ID
        /// </summary>
        public ushort Card_ID
        {
            get { return card_id; }
        }
        /// <summary>
        /// 位置集合，元素为1X6的double向量
        /// </summary>
        public ArrayList Q
        {
            get { return q; }  
        }
        /// <summary>
        /// 速度集合，元素为1X6的doulbe向量
        /// </summary>
        public ArrayList QD
        {
            get { return qd; }
        }
        /// <summary>
        /// 加速度集合，元素为1X6的double向量
        /// </summary>
        public ArrayList QDD
        {
            get { return qdd; }
        }
        /// <summary>
        /// 机械臂插补速度，保留
        /// </summary>
        public ArrayList Vel_Vector
        {
            get { return vel_vector; }
        }
        /// <summary>
        /// 每一步的工作集合，元素为int量，0运动，1延时，2泵，3打孔，4机械爪
        /// </summary>
        public ArrayList StepWork
        {
            get { return stepwork; }
        }
        /// <summary>
        /// 表示正在向q[step]位置移动
        /// </summary>
        public int Step
        {
            get { return step; }
        }
        /// <summary>
        /// 细分step
        /// </summary>
        public int SubStep
        {
            get { return substep; }
        }
        /// <summary>
        /// 每一步细分集合，元素为int量
        /// </summary>
        public ArrayList StepNum
        {
            get { return stepnum; }
        }
        /// <summary>
        /// 泵是否激活
        /// </summary>
        public bool PumpEnable
        {
            get { return pumpenable; }
            set { pumpenable = value; }
        }
        /// <summary>
        /// 打孔器是否激活
        /// </summary>
        public bool HoleEnable
        {
            get { return holeenable; }
            set { holeenable = value; }
        }
        /// <summary>
        /// 机械爪是否激活
        /// </summary>
        public bool ClawEnable
        {
            get { return clawenable; }
            set { clawenable = value; }
        }
        /// <summary>
        /// 检测到的串口的所有名字
        /// </summary>
        public string PortName
        {
            get { return portname; }
            set { portname = value; }
        }
        /// <summary>
        ///泵连接串口的比特率
        /// </summary>
        public int BitRate
        {
            get { return BitRate; }
            set { bitrate = value; }
        }
        /// <summary>
        /// 是否图形仿真，默认为不仿真
        /// </summary>
        public bool PlotRobot
        {
            get { return plotrobot;}
            set { plotrobot = value; }
        }
     
        #endregion
        /// <summary>
        /// 把默认构造函数私有化
        /// </summary>
        private Robot()
        {
        }
        /// <summary>
        /// 机器人类构造函数，输入参数为运动控制卡的ID
        /// </summary>
        /// <param name="CARD_ID"></param>
        public Robot(ushort CARD_ID)
        {
            card_id = CARD_ID;
        }
        /// <summary>
        /// 机器人初始化函数，包括回零，并且运动到初始状态
        /// </summary>
        public void Initialize()
        {
            InitralConfig();
            //设置脉冲当量
            LTDMC.dmc_set_equiv(card_id, axisx, equivx);
            LTDMC.dmc_set_equiv(card_id, axisy, equivy);
            LTDMC.dmc_set_equiv(card_id, axisz, equivz);
            LTDMC.dmc_set_equiv(card_id, axisa, equiva);
            LTDMC.dmc_set_equiv(card_id, axisb, equivb);
            LTDMC.dmc_set_equiv(card_id, axisc, equivc);
            //位置清零
            LTDMC.dmc_set_position_unit(card_id, axisx, 0);
            LTDMC.dmc_set_position_unit(card_id, axisy, 0);
            LTDMC.dmc_set_position_unit(card_id, axisz, 0);
            LTDMC.dmc_set_position_unit(card_id, axisa, 0);
            LTDMC.dmc_set_position_unit(card_id, axisb, 0);
            LTDMC.dmc_set_position_unit(card_id, axisc, 0);
            //脉冲输出模式0
            LTDMC.dmc_set_pulse_outmode(card_id, axisx, 0);
            LTDMC.dmc_set_pulse_outmode(card_id, axisy, 2);
            LTDMC.dmc_set_pulse_outmode(card_id, axisz, 0);
            LTDMC.dmc_set_pulse_outmode(card_id, axisa, 0);
            LTDMC.dmc_set_pulse_outmode(card_id, axisb, 2);
            LTDMC.dmc_set_pulse_outmode(card_id, axisc, 0);
            GoHome(axisx);
            GoInitialPosition(axisx);
            GoHome(axisy);
            GoHome(axisz);
            GoInitialPosition(axisz);
            GoHome(axisa);
            GoInitialPosition(axisa);
            GoHome(axisb);
            GoInitialPosition(axisb);
            GoHome(axisc);
            GoInitialPosition(axisc);
            GoInitialPosition(axisy);

        }
        /// <summary>
        /// axis轴回零
        /// </summary>
        /// <param name="axis"></param>
        private void GoHome(ushort axis)
        {
           if(ifmove==true)
            {
                double x_pos = -PI;//设置机械零点在D—H模型中的对应值
                double y_pos = 179.25 / 180 * PI;
                double z_pos = -PI / 2;
                double a_pos = -(double)156.62 / 180 * PI;
                double b_pos = -(double)115.02 / 180 * PI;
                double c_pos = (double)129.99 / 180 * PI;
                double pos = 0;
                ushort homemode = 1;//回零模式
                ushort myhome_dir = 0;//回零方向
                ushort vel_mode = 1;//回零速度模式，1为高速回零
                double max_vel = 0;
                if (axis == 1 || axis == 5)
                {
                    myhome_dir = 1;
                }
                else
                {
                    myhome_dir = 0;
                }
                LTDMC.dmc_set_home_pin_logic(card_id, axis, 0, 0);//设置原点信号
                LTDMC.dmc_set_homemode(card_id, axis, myhome_dir, vel_mode, homemode, 0);//设置脉冲当量
                if (axis == 0)
                {
                    max_vel = xmax_vel;
                    pos = x_pos;
                }
                else if (axis == 1)
                {
                    max_vel = ymax_vel;
                    pos = y_pos;
                }
                else if (axis == 2)
                {
                    max_vel = zmax_vel;
                    pos = z_pos;
                }
                else if (axis == 3)
                {
                    max_vel = amax_vel;
                    pos = a_pos;
                }
                else if (axis == 4)
                {
                    max_vel = bmax_vel;
                    pos = b_pos;
                }
                else if (axis == 5)
                {
                    max_vel = cmax_vel;
                    pos = c_pos;
                }

                LTDMC.dmc_set_profile_unit(card_id, axis, 0, max_vel, 0.1, 0.1, 0);//设置速度
                LTDMC.dmc_set_s_profile(card_id, axis, 0, 0.05);//设置S段时间
                LTDMC.dmc_home_move(card_id, axis);//回零运动
                while (LTDMC.dmc_check_done(card_id, axis) == 0)
                {
                    Application.DoEvents();//待定，不做任何事
                }
                LTDMC.dmc_set_position_unit(card_id, axis, pos);//设置当前位置
            }
        }
        /// <summary>
        /// axis轴回到初始位置点
        /// </summary>
        /// <param name="axis"></param>
        private void GoInitialPosition(ushort axis)
        {
            if(ifmove==true)
            {
                double x_move = 0;//到初始位姿各轴运动的角度
                double y_move = PI / 2;
                double z_move = 0;
                double a_move = 0;
                double b_move = 0;
                double c_move = 0;
                double move = 0;
                double max_vel = 0;

                if (axis == 0)
                {
                    max_vel = xmax_vel;
                    move = x_move;
                }
                else if (axis == 1)
                {

                    max_vel = ymax_vel;
                    move = y_move;
                }
                else if (axis == 2)
                {
                    max_vel = zmax_vel;
                    move = z_move;
                }
                else if (axis == 3)
                {
                    max_vel = amax_vel;
                    move = a_move;
                }
                else if (axis == 4)
                {
                    max_vel = bmax_vel;
                    move = b_move;
                }
                else if (axis == 5)
                {
                    max_vel = cmax_vel;
                    move = c_move;
                }
                LTDMC.dmc_set_profile_unit(card_id, axis, 0, max_vel, 0.1, 0.1, 0);//设置速度
                LTDMC.dmc_set_s_profile(card_id, axis, 0, 0.05);//设置S段时间
                LTDMC.dmc_pmove_unit(card_id, axis, move, 1);
                while (LTDMC.dmc_check_done(card_id, axis) == 0)
                {
                    Application.DoEvents();//待定，不做任何事
                }
            }
        }
        /// <summary>
        /// 以现有位置、速度、加速度集合的最后一个元素为初始点，现在位置为终点，N为插入点数，TIME为消耗时间，规划轨迹，并加入到q,qd,qdd集合中
        /// </summary>
        /// <param name="N"></param>
        /// <param name="TIME"></param>
        public void AddQ(int N, double TIME)
        {
            double[] q1 = new double[6];
            double[] qd1 = new double[6];//先初始化为零
            double[] qdd1 = new double[6];
            double[,] Qr = new double[N, 6];
            double[,] QDr = new double[N, 6];
            double[,] QDDr = new double[N, 6];
            double[] vel_vector1 = new double[N];
            int n = 0;
            n = stepwork.Count - 1;
            LTDMC.dmc_get_position_unit(card_id, axisx, ref q1[0]);
            LTDMC.dmc_get_position_unit(card_id, axisy, ref q1[1]);
            LTDMC.dmc_get_position_unit(card_id, axisz, ref q1[2]);
            LTDMC.dmc_get_position_unit(card_id, axisa, ref q1[3]);
            LTDMC.dmc_get_position_unit(card_id, axisb, ref q1[4]);
            LTDMC.dmc_get_position_unit(card_id, axisc, ref q1[5]);
            if (n >= 0)
            {
                while ((int)stepwork[n] != 0)
                {
                    n = n - 1;
                    if(n<0)
                    {
                        break;
                    }
                }
                if(n>=0)
                {
                    try
                    {
                        int subn = TransSub(n);
                        JTRAJ(ref Qr, ref QDr, ref QDDr, (double[])Q[subn], q1, N, TIME);
                    }
                    catch
                    {
                        MessageBox.Show("无法插值，请换个位置！", "提示");
                        return;
                    }
                    for (int i = 0; i < N; i++)
                    {
                        double[] Qt = new double[6];//必须每循环一次声明一次
                        double[] QDt = new double[6];
                        double[] QDDt = new double[6];
                        for (int j = 0; j < 6; j++)
                        {
                            Qt[j] = Qr[i, j];
                            QDt[j] = QDr[i, j];
                            QDDt[j] = QDDr[i, j];
                        }
                        q.Add(Qt);
                        qd.Add(QDt);
                        qdd.Add(QDt);
                        vel_vector1[i] = default_vel;
                        vel_vector.Add(vel_vector1[i]);
                    }
                    stepwork.Add(0);
                    stepnum.Add(N);
                }
                else if(n==-1)
                {
                    q.Add(q1);
                    qd.Add(qd1);
                    qdd.Add(qdd1);
                    stepwork.Add(0);
                    stepnum.Add(1);
                    vel_vector.Add((double)0);
                }
                else
                {
                    MessageBox.Show("添加出错，请重试！", "错误");
                    return;
                }
            }
            else
            {
                q.Add(q1);
                qd.Add(qd1);
                qdd.Add(qdd1);
                stepwork.Add(0);
                stepnum.Add(1);
                vel_vector.Add((double)0);
            }
        }
        /// <summary>
        /// 添加延时，Q添加time{TIME,0,0,0,0,0,0}元素，stepwork添加1
        /// </summary>
        /// <param name="TIME"></param>
        public void AddDelay(double TIME)
        {
            double[] qd1 = new double[6];//先初始化为零
            double[] qdd1 = new double[6];
            double[] time = new double[6];
            time[0] = TIME;
            q.Add(time);
            qd.Add(qd1);
            qdd.Add(qdd1);
            stepwork.Add(1);
            stepnum.Add(1);
            vel_vector.Add((double)0);
        }
        /// <summary>
        /// 添加泵，OPERATION为ture时表示吸液，Q添加Operation{1,VOLUMW,0,0,0,0,0},为false时表示排液，Q添加Operation{0,VOLUMW,0,0,0,0,0},元素VOLUME表示吸、排液量，stepwork集合添加2
        /// </summary>
        /// <param name="OPERATION"></param>
        /// <param name="VOLUME"></param>
        public void AddPump(bool OPERATION,double VELOCITY, double VOLUME)
        {
            double[] qd1 = new double[6];//先初始化为零
            double[] qdd1 = new double[6];
            double[] operation = new double[6];
            if (OPERATION == true)
            {
                operation = new double[6] { 1, VELOCITY, VOLUME, 0, 0, 0 };
            }
            else
            {
                operation = new double[6] { 0, VELOCITY, VOLUME, 0, 0, 0 };
            }
            q.Add(operation);
            qd.Add(qd1);
            qdd.Add(qdd1);
            vel_vector.Add((double)0);
            stepwork.Add(2);
            stepnum.Add(1);
        }
        /// <summary>
        /// 添加打孔器操作，并操作一下打孔器，stepwork集合加3
        /// </summary>
        public void AddHole()
        {
            double[] q1 = new double[6];
            double[] qd1 = new double[6];//先初始化为零
            double[] qdd1 = new double[6];
            q.Add(q1);
            qd.Add(qd1);
            qdd.Add(qdd1);
            stepwork.Add(3);
            stepnum.Add(1);
            vel_vector.Add((double)0);
        }
        /// <summary>
        /// 添加机械爪操作，OPERATION为ture表示夹紧，Q添加Operation{1,0,0,0,0,0,0},false表示松开，Q添加Operation{0,0,0,0,0,0,0}stepwork集合添加4
        /// </summary>
        /// <param name="OPERATION"></param>
        public void AddClaw(bool OPERATION)
        {
            double[] qd1 = new double[6];//先初始化为零
            double[] qdd1 = new double[6];
            double[] operation = new double[6];
            if (OPERATION == true)
            {
                operation = new double[6] { 1, 0, 0, 0, 0, 0 };
            }
            else
            {
                operation = new double[6] { 0, 0, 0, 0, 0, 0 };
            }
            q.Add(operation);
            qd.Add(qd1);
            qdd.Add(qdd1);
            stepwork.Add(4);
            stepnum.Add(1);
            vel_vector.Add((double)0);
        }
        /// <summary>
        /// 删除n个位置、速度、加速度集合的元素
        /// </summary>
        /// <param name="n"></param>
        public void DeleteQ(int n)
        {
            int allstep = q.Count - 1;
            int count = stepwork.Count - 1;
            if (count + 1 < n)
            {
                throw new Exception("删除步数过多");
            }
            for (int i = 0; i < n; i++)
            {
                allstep = q.Count - 1;
                count = stepwork.Count - 1;
                if ((int)stepwork[count] != 0)
                {
                    stepnum.RemoveAt(count);
                    stepwork.RemoveAt(count);
                    q.RemoveAt(allstep);
                    qdd.RemoveAt(allstep);
                    qd.RemoveAt(allstep);
                    vel_vector.RemoveAt(allstep);
                    
                }
                else
                {
                    for (int j = 0; j < (int)stepnum[count]; j++)
                    {
                        q.RemoveAt(allstep);
                        qdd.RemoveAt(allstep);
                        qd.RemoveAt(allstep);
                        vel_vector.RemoveAt(allstep);
                        allstep = allstep - 1;
                    }
                    stepnum.RemoveAt(count);
                    stepwork.RemoveAt(count);
                    count = count - 1;
                }
            }
            step = -1;
            substep = -1;
        }
        /// <summary>
        /// 开始运动，从STEPSTART步到STEPSTOP步
        /// </summary>
        /// <param name="STEPSTART"></param>
        /// <param name="STEPSTOP"></param>
        public void Start(int STEPSTART, int STEPSTOP)
        {
            double max_vel = 0;
            if (STEPSTOP > stepnum.Count - 1 || STEPSTART > stepnum.Count - 1)
            {
                MessageBox.Show("超出范围！", "提示");
                return;
            }
            if (STEPSTART <= STEPSTOP)
            {
                for (int k = STEPSTART; k <= STEPSTOP; k++)
                {
                    step = k;
                    int test1 = q.Count;
                    int test2 = stepwork.Count;
                    substep = TransSub(step) - (int)stepnum[step] + 1;
                    if ((int)stepwork[step] == 0)//如果为运动
                    {
                        for (int i = 0; i < (int)stepnum[step]; i++)
                        {
                            max_vel = (double)vel_vector[substep];
                            //当出现max_vel为零，而要运动的位置不为零时
                            if (max_vel != 0)
                            {
                                LTDMC.dmc_set_vector_profile_unit(card_id, 0, 0, max_vel, 0.1, 0.1, 0);
                                LTDMC.dmc_conti_set_s_profile(card_id, 0, 0, 0.05);
                                LTDMC.dmc_line_unit(card_id, 0, 6, new ushort[] { 0, 1, 2, 3, 4, 5 }, (double[])q[substep], 1);
                                while (LTDMC.dmc_check_done_multicoor(card_id, 0) == 0)
                                {
                                    Application.DoEvents();
                                }
                            }
                            substep = substep + 1;
                        }
                    }
                    else if ((int)stepwork[step] == 1)//如果为延时
                    {
                        double second = ((double[])q[substep])[0];
                        delayTime(second);
                        substep = substep + 1;
                    }
                    else if ((int)stepwork[step] == 2)//如果为泵操作
                    {
                        double operation = ((double[])q[substep])[0];
                        double velocity = ((double[])q[substep])[1];
                        double volume = ((double[])q[substep])[2];
                        if (operation == 1)
                        {
                            PumpAbsorb((int)velocity, volume);//速度单位为转/min
                        }
                        else
                        {
                            PumpEject((int)velocity, volume);
                        }
                        substep = substep + 1;
                    }
                    else if ((int)stepwork[step] == 3)//如果为打孔器操作
                    {
                        HoleOperate();
                        substep = substep + 1;
                    }
                    else if ((int)stepwork[step] == 4)//如果为机械爪操作
                    {
                        double operation = ((double[])q[substep])[0];
                        if (operation == 1)
                        {
                            ClawOperate(0);
                        }
                        else
                        {
                            ClawOperate(1);
                        }
                        substep = substep + 1;
                    }
                    if(ifmove==false)//如果检测到运动标志位为false
                    {
                        return;
                    }
                }
            }
            else
            {
                for (int k = STEPSTART; k > STEPSTOP; k--)
                {
                    step = k;
                    substep = TransSub(step);
                    if ((int)stepwork[step - 1] == 0)//如果为运动
                    {
                        for (int i = 0; i < (int)stepnum[step - 1]; i++)
                        {

                            max_vel = (double)vel_vector[substep - 1];
                            //当出现max_vel为零，而要运动的位置不为零时
                            if (max_vel != 0)
                            {
                                LTDMC.dmc_set_vector_profile_unit(card_id, 0, 0, max_vel, 0.1, 0.1, 0);
                                LTDMC.dmc_conti_set_s_profile(card_id, 0, 0, 0.05);
                                LTDMC.dmc_line_unit(card_id, 0, 6, new ushort[] { 0, 1, 2, 3, 4, 5 }, (double[])q[substep - 1], 1);
                                while (LTDMC.dmc_check_done_multicoor(card_id, 0) == 0)
                                {
                                    Application.DoEvents();
                                }
                            }


                            substep = substep - 1;
                        }

                    }
                    else if ((int)stepwork[step - 1] == 1)//如果为延时
                    {
                        substep = substep - 1;
                    }
                    else if ((int)stepwork[step] == 2)//如果为泵操作
                    {
                        substep = substep - 1;
                    }
                    else if ((int)stepwork[step] == 3)//如果为打孔器操作
                    {
                        substep = substep - 1;
                    }
                    else if ((int)stepwork[step] == 4)//如果为机械爪操作
                    {
                        substep = substep - 1;
                    }
                    if (ifmove == false)//如果检测到运动标志位为false
                    {
                        return;
                    }
                }
            }

        }
        /// <summary>
        /// 第AXIS轴以VELOTCITY的角度速度旋转ANGLE度。
        /// </summary>
        /// <param name="AXIS"></param>
        /// <param name="ANGLE"></param>
        /// <param name="VELOCITY"></param>
        public void RotByAngle(ushort AXIS, double ANGLE, double VELOCITY)
        {
            if(ifmove==true)
            {
                double arc = TransAngleToArc(ANGLE);
                double arc_speed = TransAngleToArc(VELOCITY);
                LTDMC.dmc_set_profile_unit(card_id, AXIS, 0, arc_speed, 0.1, 0.1, 0);
                LTDMC.dmc_pmove_unit(card_id, AXIS, arc, 0);
                while (LTDMC.dmc_check_done(card_id, AXIS) == 0)
                {
                    Application.DoEvents();
                }
            }
        }
        /// <summary>
        /// 第AXIS轴以VELOTCITY的角度速度旋转到ANGLE度。
        /// </summary>
        /// <param name="AXIS"></param>
        /// <param name="ANGLE"></param>
        /// <param name="VELOCITY"></param>
        public void RotToAngle(ushort AXIS, double ANGLE, double VELOCITY)
        {
            if(ifmove==true)
            {
                double arc = TransAngleToArc(ANGLE);
                double arc_speed = TransAngleToArc(VELOCITY);
                LTDMC.dmc_set_profile_unit(card_id, AXIS, 0, arc_speed, 0.1, 0.1, 0);
                LTDMC.dmc_pmove_unit(card_id, AXIS, arc, 1);
                while (LTDMC.dmc_check_done(card_id, AXIS) == 0)
                {
                    Application.DoEvents();
                }
            }
        }
        /// <summary>
        /// 停止运动
        /// </summary>
        public void Stop()
        {
            LTDMC.dmc_conti_stop_list(card_id, 0, 0);
            LTDMC.dmc_stop_multicoor(card_id, 0,0);  
        }
        /// <summary>
        /// 机械臂保持姿态运动，VN为运动向量，N为插入点数，TIME为预计运动时间
        /// </summary>
        /// <param name="VN"></param>
        /// <param name="N"></param>
        /// <param name="TIME"></param>
        public void Move(double[] VN, int N, double TIME)
        {
            if(ifmove==true)
            {
                double[] q1 = new double[6];
                double[,] q2 = new double[N, 6];
                double[,] qd2 = new double[N, 6];
                double[,] qdd2 = new double[N, 6];
                LTDMC.dmc_get_position_unit(card_id, axisx, ref q1[0]);
                LTDMC.dmc_get_position_unit(card_id, axisy, ref q1[1]);
                LTDMC.dmc_get_position_unit(card_id, axisz, ref q1[2]);
                LTDMC.dmc_get_position_unit(card_id, axisa, ref q1[3]);
                LTDMC.dmc_get_position_unit(card_id, axisb, ref q1[4]);
                LTDMC.dmc_get_position_unit(card_id, axisc, ref q1[5]);
                try
                {
                    ToTrans(ref q2, ref qd2, ref qdd2, q1, VN, N, TIME);
                }
                catch
                {
                    MessageBox.Show("无法到达，更改距离！", "提示");
                    return;
                }
                for (int i = 0; i < N; i++)
                {
                    double[] Qt = new double[6];
                    double[] QDt = new double[6];
                    double[] QDDt = new double[6];
                    double max_vel = 0;
                    for (int j = 0; j < 6; j++)
                    {
                        Qt[j] = q2[i, j];
                        QDt[j] = qd2[i, j];
                        QDDt[j] = qdd2[i, j];
                    }
                    max_vel = default_vel;
                    //当出现max_vel为零，而要运动的位置不为零时
                    if (max_vel != 0)
                    {

                        LTDMC.dmc_set_vector_profile_unit(card_id, 0, 0, max_vel, 0.1, 0.1, 0);
                        LTDMC.dmc_conti_set_s_profile(card_id, 0, 0, 0.05);
                        LTDMC.dmc_line_unit(card_id, 0, 6, new ushort[] { 0, 1, 2, 3, 4, 5 }, Qt, 1);
                        while (LTDMC.dmc_check_done_multicoor(card_id, 0) == 0)
                        {
                            Application.DoEvents();
                        }

                    }
                }
            }
        }
        /// <summary>
        ///机械臂运动到当前位置的水平姿态，N为插入点数，TIME为预计运动时间
        /// </summary>
        /// <param name="N"></param>
        /// <param name="TIME"></param>
        public void GoLevel(int N, double TIME)
        {
            if(ifmove==true)
            {
                double[] q1 = new double[6];
                double[,] q2 = new double[N, 6];
                double[,] qd2 = new double[N, 6];
                double[,] qdd2 = new double[N, 6];
                LTDMC.dmc_get_position_unit(card_id, axisx, ref q1[0]);
                LTDMC.dmc_get_position_unit(card_id, axisy, ref q1[1]);
                LTDMC.dmc_get_position_unit(card_id, axisz, ref q1[2]);
                LTDMC.dmc_get_position_unit(card_id, axisa, ref q1[3]);
                LTDMC.dmc_get_position_unit(card_id, axisb, ref q1[4]);
                LTDMC.dmc_get_position_unit(card_id, axisc, ref q1[5]);
                try
                {
                    ToLevel(ref q2, ref qd2, ref qdd2, q1, N, TIME);
                }
                catch
                {
                    MessageBox.Show("无法到达，更改距离！", "提示");
                    return;
                }
                for (int i = 0; i < N; i++)
                {
                    double[] Qt = new double[6];
                    double[] QDt = new double[6];
                    double[] QDDt = new double[6];
                    double max_vel = 0;
                    for (int j = 0; j < 6; j++)
                    {
                        Qt[j] = q2[i, j];
                        QDt[j] = qd2[i, j];
                        QDDt[j] = qdd2[i, j];
                    }
                    max_vel = Sqrt(Pow(QDt[0], 2) + Pow(QDt[1], 2) + Pow(QDt[2], 2) + Pow(QDt[3], 2) + Pow(QDt[4], 2) + Pow(QDt[5], 2));
                    //当出现max_vel为零，而要运动的位置不为零时
                    LTDMC.dmc_set_profile_unit(card_id, 0, 0, 0.2, 0.1, 0.1, 0);
                    LTDMC.dmc_pmove_unit(card_id, 0, Qt[0], 1);
                    LTDMC.dmc_set_profile_unit(card_id, 1, 0, 0.2, 0.1, 0.1, 0);
                    LTDMC.dmc_pmove_unit(card_id, 1, Qt[1], 1);
                    LTDMC.dmc_set_profile_unit(card_id, 2, 0, 0.2, 0.1, 0.1, 0);
                    LTDMC.dmc_pmove_unit(card_id, 2, Qt[2], 1);
                    LTDMC.dmc_set_profile_unit(card_id, 3, 0, 0.2, 0.1, 0.1, 0);
                    LTDMC.dmc_pmove_unit(card_id, 3, Qt[3], 1);
                    LTDMC.dmc_set_profile_unit(card_id, 4, 0, 0.2, 0.1, 0.1, 0);
                    LTDMC.dmc_pmove_unit(card_id, 4, Qt[4], 1);
                    LTDMC.dmc_set_profile_unit(card_id, 5, 0, 0.2, 0.1, 0.1, 0);
                    LTDMC.dmc_pmove_unit(card_id, 5, Qt[5], 1);
                    while (LTDMC.dmc_check_done(card_id, 0) == 0 || LTDMC.dmc_check_done(card_id, 1) == 0 || LTDMC.dmc_check_done(card_id, 2) == 0 || LTDMC.dmc_check_done(card_id, 3) == 0 || LTDMC.dmc_check_done(card_id, 4) == 0 || LTDMC.dmc_check_done(card_id, 5) == 0)
                    {
                        Application.DoEvents();
                    }
                }
            }
            
            /*double[] q1 = new double[6];
            double[,] q2 = new double[N, 6];
            double[,] qd2 = new double[N, 6];
            double[,] qdd2 = new double[N, 6];
            LTDMC.dmc_get_position_unit(card_id, axisx, ref q1[0]);
            LTDMC.dmc_get_position_unit(card_id, axisy, ref q1[1]);
            LTDMC.dmc_get_position_unit(card_id, axisz, ref q1[2]);
            LTDMC.dmc_get_position_unit(card_id, axisa, ref q1[3]);
            LTDMC.dmc_get_position_unit(card_id, axisb, ref q1[4]);
            LTDMC.dmc_get_position_unit(card_id, axisc, ref q1[5]);
            try
            {
                ToLevel(ref q2, ref qd2, ref qdd2, q1, N, TIME);
            }
            catch
            {
                MessageBox.Show("此位置不适合水平姿态", "提示");
                return;
            }
            double[] Qt = new double[6];
            double[] QDt = new double[6];
            double[] QDDt = new double[6];
            double max_vel = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Qt[j] = q2[i, j];
                    QDt[j] = qd2[i, j];
                    QDDt[j] = qdd2[i, j];
                }
                max_vel = Sqrt(Pow(QDt[0], 2) + Pow(QDt[1], 2) + Pow(QDt[2], 2) + Pow(QDt[3], 2) + Pow(QDt[4], 2) + Pow(QDt[5], 2));
                //当出现max_vel为零，而要运动的位置不为零时
                if (max_vel != 0 )
                {
                    MessageBox.Show("在运动" + max_vel.ToString() + "  " + Qt[4].ToString(), "提示");

                    LTDMC.dmc_set_vector_profile_unit(card_id, 0, 0, max_vel, 0.1, 0.1, 0);
                    LTDMC.dmc_conti_set_s_profile(card_id, 0, 0, 0.05);
                    LTDMC.dmc_line_unit(card_id, 0, 6, new ushort[] { 0, 1, 2, 3, 4, 5 }, Qt, 1);
                    while (LTDMC.dmc_check_done_multicoor(card_id, 0) == 0)
                    {
                        Application.DoEvents();
                    }
                }
                
            }*/
        }
        /// <summary>
        /// 机械臂运动到当前位置的垂直姿态，N为插入点数，TIME为预计运动时间
        /// </summary>
        /// <param name="N"></param>
        /// <param name="TIME"></param>
        public void GoVertical(int N, double TIME)
        {
            double[] q1 = new double[6];
            double[,] q2 = new double[N, 6];
            double[,] qd2 = new double[N, 6];
            double[,] qdd2 = new double[N, 6];
            LTDMC.dmc_get_position_unit(card_id, axisx, ref q1[0]);
            LTDMC.dmc_get_position_unit(card_id, axisy, ref q1[1]);
            LTDMC.dmc_get_position_unit(card_id, axisz, ref q1[2]);
            LTDMC.dmc_get_position_unit(card_id, axisa, ref q1[3]);
            LTDMC.dmc_get_position_unit(card_id, axisb, ref q1[4]);
            LTDMC.dmc_get_position_unit(card_id, axisc, ref q1[5]);
            try
            {
                ToVertical(ref q2, ref qd2, ref qdd2, q1, N, TIME);
            }
            catch
            {
                MessageBox.Show("此位置不适合垂直姿态", "提示");
                return;
            }
            for (int i = 0; i < N; i++)
            {
                double[] Qt = new double[6];
                double[] QDt = new double[6];
                double[] QDDt = new double[6];
                double max_vel = 0;
                for (int j = 0; j < 6; j++)
                {
                    Qt[j] = q2[i, j];
                    QDt[j] = qd2[i, j];
                    QDDt[j] = qdd2[i, j]; 
                }
                 max_vel = Sqrt(Pow(QDt[0], 2) + Pow(QDt[1], 2) + Pow(QDt[2], 2) + Pow(QDt[3], 2) + Pow(QDt[4], 2) + Pow(QDt[5], 2));
                //当出现max_vel为零，而要运动的位置不为零时
                LTDMC.dmc_set_profile_unit(card_id, 0, 0, 0.2, 0.1, 0.1, 0);
                LTDMC.dmc_pmove_unit(card_id, 0, Qt[0], 1);
                LTDMC.dmc_set_profile_unit(card_id, 1, 0, 0.2, 0.1, 0.1, 0);
                LTDMC.dmc_pmove_unit(card_id, 1, Qt[1], 1);
                LTDMC.dmc_set_profile_unit(card_id, 2, 0, 0.2, 0.1, 0.1, 0);
                LTDMC.dmc_pmove_unit(card_id, 2, Qt[2], 1);
                LTDMC.dmc_set_profile_unit(card_id, 3, 0, 0.2, 0.1, 0.1, 0);
                LTDMC.dmc_pmove_unit(card_id, 3, Qt[3], 1);
                LTDMC.dmc_set_profile_unit(card_id, 4, 0, 0.2, 0.1, 0.1, 0);
                LTDMC.dmc_pmove_unit(card_id, 4, Qt[4], 1);
                LTDMC.dmc_set_profile_unit(card_id, 5, 0, 0.2, 0.1, 0.1, 0);
                LTDMC.dmc_pmove_unit(card_id, 5, Qt[5], 1);
                while (LTDMC.dmc_check_done(card_id, 0) == 0 || LTDMC.dmc_check_done(card_id, 1) == 0 || LTDMC.dmc_check_done(card_id, 2) == 0 || LTDMC.dmc_check_done(card_id, 3) == 0 || LTDMC.dmc_check_done(card_id, 4) == 0 || LTDMC.dmc_check_done(card_id, 5) == 0)
                {
                    Application.DoEvents();
                }
            }
        }
        /// <summary>
        /// 获取当前机械臂实时弧度位置
        /// </summary>
        /// <param name="QSTATE"></param>
        /// <param name="QDSTATE"></param>
        /// <param name="QDDSTATE"></param>
        public  double[] GetPositionArc( )
        {
            double[] q0 = new double[6];
            LTDMC.dmc_get_position_unit(card_id, 0, ref q0[0]);
            LTDMC.dmc_get_position_unit(card_id, 1, ref q0[1]);
            LTDMC.dmc_get_position_unit(card_id, 2, ref q0[2]);
            LTDMC.dmc_get_position_unit(card_id, 3, ref q0[3]);
            LTDMC.dmc_get_position_unit(card_id, 4, ref q0[4]);
            LTDMC.dmc_get_position_unit(card_id, 5, ref q0[5]);
            return q0;
        }
        /// <summary>
        /// 仿真机器人
        /// </summary>
        public void Plot_Robot()
        {
            
            if (plotrobot==true)
            {
                double[] q0 = new double[6];
                LTDMC.dmc_get_position_unit(card_id, 0, ref q0[0]);
                LTDMC.dmc_get_position_unit(card_id, 1, ref q0[1]);
                LTDMC.dmc_get_position_unit(card_id, 2, ref q0[2]);
                LTDMC.dmc_get_position_unit(card_id, 3, ref q0[3]);
                LTDMC.dmc_get_position_unit(card_id, 4, ref q0[4]);
                LTDMC.dmc_get_position_unit(card_id, 5, ref q0[5]);
                PaintRobot(q0);
            }
       
        }
        /// <summary>
        /// 获取当前机械臂实时角度位置
        /// </summary>
        /// <param name="QSTATE"></param>
        /// <param name="QDSTATE"></param>
        /// <param name="QDDSTATE"></param>
        public double[] GetPositionAngle()
        {
            double[] q0 = new double[6];
            LTDMC.dmc_get_position_unit(card_id, 0, ref q0[0]);
            LTDMC.dmc_get_position_unit(card_id, 1, ref q0[1]);
            LTDMC.dmc_get_position_unit(card_id, 2, ref q0[2]);
            LTDMC.dmc_get_position_unit(card_id, 3, ref q0[3]);
            LTDMC.dmc_get_position_unit(card_id, 4, ref q0[4]);
            LTDMC.dmc_get_position_unit(card_id, 5, ref q0[5]);
            for(int i=0;i<6;i++)
            {
                q0[i] = TransArcToAngle(q0[i]);
            }
            return q0;
        }
        /// <summary>
        /// 串口初始化
        /// </summary>
        private void InitralConfig()
        {

            //查询主机上存在的串口
            //向ComDevice.DataReceived（是一个事件）注册一个方法Com_DataReceived，当端口类接收到信息时时会自动调用Com_DataReceived方法
            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);
        }
        /// <summary>
        /// 一旦ComDevice.DataReceived事件发生，就将从串口接收到的数据显示到接收端对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //开辟接收缓冲区
            byte[] ReDatas = new byte[ComDevice.BytesToRead];
            //从串口读取数据
            ComDevice.Read(ReDatas, 0, ReDatas.Length);
            readdate = ReDatas;
        }
        /// <summary>
        /// 开启串口函数
        /// </summary>
        public void OpenCom()
        {
            //设置串口相关属性
            ComDevice.PortName = portname;
            ComDevice.BaudRate = bitrate;
            ComDevice.Parity = 0;
            ComDevice.DataBits = 8;
            ComDevice.StopBits = (StopBits)1;
            try
            {
                //开启串口
                ComDevice.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("未能成功开启串口"+ex.ToString());// MessageBox.Show(ex.Message, "未能成功开启串口", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        /// <summary>
        /// 关闭串口函数
        /// </summary>
        public void CloseCom()
        {
            try
            {
                //关闭串口
                ComDevice.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("串口关闭错误"+ex.ToString()); //MessageBox.Show(ex.Message, "串口关闭错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 数据传送函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SendData(byte[] data)
        {
            if (ComDevice.IsOpen)
            {
                try
                {
                    //将消息传递给串口
                    ComDevice.Write(data, 0, data.Length);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("发送失败"+ex.ToString()); //MessageBox.Show(ex.Message, "发送失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                throw new Exception("串口未开启");// MessageBox.Show("串口未开启", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        /// <summary>
        /// 机械爪操作函数,MyBitNo,IO口选择，OUT0接夹紧，OUT1接张开
        /// </summary>
        /// <param name="MyBitNo"></param>
        /// <param name="?"></param>
        public void ClawOperate( ushort MyBitNo)
        {
            LTDMC.dmc_write_outbit(card_id, MyBitNo, 0);
            delayTime(0.5);
            LTDMC.dmc_write_outbit(card_id, MyBitNo, 1);
        }
        /// <summary>
        /// 吸液函数 参数分别为速度，步距
        /// </summary>
        /// <param name="velocity"></param>
        /// <param name="step1"></param>
        public void PumpAbsorb(int velocity, double VOLUME)
        {
            int step = (int)(VOLUME / 0.0004154);
            byte[] sendDat = new byte[14];
            int addnum1 = 0;
            sendDat[0] = 0xcc;
            sendDat[1] = 0x00;
            sendDat[2] = 0x07;
            sendDat[3] = 0xff;
            sendDat[4] = 0xee;
            sendDat[5] = 0xbb;
            sendDat[6] = 0xaa;
            sendDat[7] = (byte)(velocity % 256);
            sendDat[8] = (byte)(velocity / 256);
            sendDat[9] = 0x00;
            sendDat[10] = 0x00;
            sendDat[11] = 0xdd;
            for (int i = 0; i <= 11; i++)
            {
                addnum1 = addnum1 + (int)sendDat[i];
            }
            sendDat[12] = (byte)(addnum1 % 256);
            sendDat[13] = (byte)(addnum1 / 256);
            SendData(sendDat);

            byte[] sendDat1 = new byte[8];
            int addnum2 = 0;
            sendDat1[0] = 0xcc;
            sendDat1[1] = 0x00;
            sendDat1[2] = 0x41;
            sendDat1[3] = (byte)(step % 256);
            sendDat1[4] = (byte)(step / 256);
            sendDat1[5] = 0xdd;
            for (int i = 0; i <= 5; i++)
            {
                addnum2 = addnum2 + (int)sendDat1[i];
            }
            sendDat1[6] = (byte)(addnum2 % 256); ;
            sendDat1[7] = (byte)(addnum2 / 256);
            SendData(sendDat1);
            byte[] sendDat2 = new byte[8];
            delayTime(0.1);
            int addnum3 = 0;
            sendDat2[0] = 0xcc;
            sendDat2[1] = 0x00;
            sendDat2[2] = 0x4a;
            sendDat2[3] = 0;
            sendDat2[4] = 0;
            sendDat2[5] = 0xdd;
            for (int i = 0; i <= 5; i++)
            {
                addnum3 = addnum3 + (int)sendDat2[i];
            }
            sendDat2[6] = (byte)(addnum3 % 256); ;
            sendDat2[7] = (byte)(addnum3 / 256);
            SendData(sendDat2);
            delayTime(0.1);
            while (readdate[2] != 0)
            {
                Application.DoEvents();

            }




        }
        /// <summary>
        /// 排液函数,参数分别为速度、步距
        /// </summary>
        /// <param name="velocity"></param>
        /// <param name="step1"></param>
        public void PumpEject(int velocity, double VOLUME)
        {
            int step = (int)(VOLUME / 0.0004154);
            byte[] sendDat = new byte[14];
            int addnum1 = 0;
            sendDat[0] = 0xcc;
            sendDat[1] = 0x00;
            sendDat[2] = 0x07;
            sendDat[3] = 0xff;
            sendDat[4] = 0xee;
            sendDat[5] = 0xbb;
            sendDat[6] = 0xaa;
            sendDat[7] = (byte)(velocity % 256);
            sendDat[8] = (byte)(velocity / 256);
            sendDat[9] = 0x00;
            sendDat[10] = 0x00;
            sendDat[11] = 0xdd;
            for (int i = 0; i <= 11; i++)
            {
                addnum1 = addnum1 + (int)sendDat[i];
            }
            sendDat[12] = (byte)(addnum1 % 256);
            sendDat[13] = (byte)(addnum1 / 256);
            SendData(sendDat);

            byte[] sendDat1 = new byte[8];
            int addnum2 = 0;
            sendDat1[0] = 0xcc;
            sendDat1[1] = 0x00;
            sendDat1[2] = 0x42;
            sendDat1[3] = (byte)(step % 256);
            sendDat1[4] = (byte)(step / 256);
            sendDat1[5] = 0xdd;
            for (int i = 0; i <= 5; i++)
            {
                addnum2 = addnum2 + (int)sendDat1[i];
            }
            sendDat1[6] = (byte)(addnum2 % 256); ;
            sendDat1[7] = (byte)(addnum2 / 256);
            SendData(sendDat1);
            delayTime(0.1);
            byte[] sendDat2 = new byte[8];
            int addnum3 = 0;
            sendDat2[0] = 0xcc;
            sendDat2[1] = 0x00;
            sendDat2[2] = 0x4a;
            sendDat2[3] = 0;
            sendDat2[4] = 0;
            sendDat2[5] = 0xdd;
            for (int i = 0; i <= 5; i++)
            {
                addnum3 = addnum3 + (int)sendDat2[i];
            }
            sendDat2[6] = (byte)(addnum3 % 256); ;
            sendDat2[7] = (byte)(addnum3 / 256);
            SendData(sendDat2);
            delayTime(0.1);
            while (readdate[2] != 0)
            {
                Application.DoEvents();


            }


        }
        /// <summary>
        /// 泵停止函数
        /// </summary>
        public void PumpStop()
        {
            byte[] sendDat1 = new byte[8];
            int addnum2 = 0;
            sendDat1[0] = 0xcc;
            sendDat1[1] = 0x00;
            sendDat1[2] = 0x49;
            sendDat1[3] = 0x00;
            sendDat1[4] = 0x00;
            sendDat1[5] = 0xdd;
            for (int i = 0; i <= 5; i++)
            {
                addnum2 = addnum2 + (int)sendDat1[i];
            }
            sendDat1[6] = (byte)(addnum2 % 256); ;
            sendDat1[7] = (byte)(addnum2 / 256);
            SendData(sendDat1);
        }
        /// <summary>
        /// 泵复位函数
        /// </summary>
        /// <param name="VELOCITY"></param>
        public void PumpReset(int VELOCITY)
        {
            int B3B4 = VELOCITY;
            byte[] sendDat = new byte[14];
            int addnum1 = 0;
            sendDat[0] = 0xcc;
            sendDat[1] = 0x00;
            sendDat[2] = 0x07;
            sendDat[3] = 0xff;
            sendDat[4] = 0xee;
            sendDat[5] = 0xbb;
            sendDat[6] = 0xaa;
            sendDat[7] = (byte)(B3B4 % 256);
            sendDat[8] = (byte)(B3B4 / 256);
            sendDat[9] = 0x00;
            sendDat[10] = 0x00;
            sendDat[11] = 0xdd;
            for (int i = 0; i <= 11; i++)
            {
                addnum1 = addnum1 + (int)sendDat[i];
            }
            sendDat[12] = (byte)(addnum1 % 256);
            sendDat[13] = (byte)(addnum1 / 256);
            SendData(sendDat);
            byte[] sendDat1 = new byte[8];
            int addnum2 = 0;
            sendDat1[0] = 0xcc;
            sendDat1[1] = 0x00;
            sendDat1[2] = 0x45;
            sendDat1[3] = 0x00;
            sendDat1[4] = 0x00;
            sendDat1[5] = 0xdd;
            for (int i = 0; i <= 5; i++)
            {
                addnum2 = addnum2 + (int)sendDat1[i];
            }
            sendDat1[6] = (byte)(addnum2 % 256); ;
            sendDat1[7] = (byte)(addnum2 / 256);
            SendData(sendDat1);
        }
        /// <summary>
        /// step转换到substep
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int TransSub(int n)
        {
            int subn = -1;
            for(int i=0;i<=n;i++)
            {
                subn = (int)stepnum[i] + subn;
            }
            return subn;
        }
        /// <summary>
        /// 将弧度转换为角度单位
        /// </summary>
        /// <param name="ARC"></param>
        /// <returns></returns>
        public double TransArcToAngle(double ARC)
        {
            double angle = 0;
            angle = ARC / PI * 180;
            return angle;
        }
        /// <summary>
        /// 将角度转换为弧度单位
        /// </summary>
        /// <param name="ANGLE"></param>
        /// <returns></returns>
        public double TransAngleToArc(double ANGLE)
        {
            double arc;
            arc = ANGLE / 180 *PI;
            return arc;
        }
        /// <summary>
        /// 操作一次打孔器，打孔器连接OUT2
        /// </summary>
        public void HoleOperate()
        {
            LTDMC.dmc_write_outbit(card_id, 2, 0);
            delayTime(0.5);
            LTDMC.dmc_write_outbit(card_id, 2, 1);
        }
        /// <summary>
        /// 将路径为path的文件的数据读取到Robot的类中
        /// </summary>
        /// <param name="PATH"></param>
        public void AddData(string PATHFILE)
        {
            try
            {
                DeleteQ(stepnum.Count);//清空现有数据
                int count = 0;//总步数
                int subcount = 0;//分步数
                FileStream path_stream = new FileStream(PATHFILE, FileMode.Open);
                byte[] arraybyte_readkey = new byte[4];//读取数据文件标，占4位
                byte[] arraybyte_key = new byte[4] { 0xF1, 0xE1, 0xD1, 0xA0 };//将读取得数据文件表示与此数组对比
                byte[] arraybyte_readcount = new byte[4];//读取的总步数和分步数,以及stepnum,stepwork
                byte[] arraybyte_readdata = new byte[8];//读取的数据数组，占8位
                path_stream.Seek(0, SeekOrigin.Begin);
                path_stream.Read(arraybyte_readkey, 0, 4);
                if (arraybyte_readkey[0] != arraybyte_key[0] || arraybyte_readkey[1] != arraybyte_key[1] || arraybyte_readkey[2] != arraybyte_key[2] || arraybyte_readkey[3] != arraybyte_key[3])//如果文件表示对不上，则提示文件格式错误,可改为浅度复制
                {
                    MessageBox.Show("文件格式错误，请检查文件！", "错误");
                    return;
                }
                path_stream.Seek(4, SeekOrigin.Begin);
                path_stream.Read(arraybyte_readcount, 0, 4);
                count = BitConverter.ToInt32(arraybyte_readcount, 0);
                path_stream.Read(arraybyte_readcount, 0, 4);
                subcount = BitConverter.ToInt32(arraybyte_readcount, 0);
                path_stream.Seek(12, SeekOrigin.Begin);
                for (int i = 0; i < subcount; i++)//添加q0
                {
                    double[] q0 = new double[6];//存储每一步的q,没循环一次，都要创建一个q0，下同
                    for (int j = 0; j < 6; j++)
                    {
                        path_stream.Read(arraybyte_readdata, 0, 8);
                        q0[j] = BitConverter.ToDouble(arraybyte_readdata, 0);
                    }
                    q.Add(q0);
                }
                for (int i = 0; i < subcount; i++)//添加qd0
                {
                    double[] qd0 = new double[6];//存储每一步的qd
                    for (int j = 0; j < 6; j++)
                    {
                        path_stream.Read(arraybyte_readdata, 0, 8);
                        qd0[j] = BitConverter.ToDouble(arraybyte_readdata, 0);
                    }
                    qd.Add(qd0);
                }
                for (int i = 0; i < subcount; i++)//添加qdd0
                {
                    double[] qdd0 = new double[6];//存储每一步的qdd
                    for (int j = 0; j < 6; j++)
                    {
                        path_stream.Read(arraybyte_readdata, 0, 8);
                        qdd0[j] = BitConverter.ToDouble(arraybyte_readdata, 0);
                    }
                    qdd.Add(qdd0);
                }
                for (int i = 0; i < subcount; i++)//添加速度
                {
                    double vel_vector0 = 0;//存储每一步速度
                    path_stream.Read(arraybyte_readdata, 0, 8);
                    vel_vector0 = BitConverter.ToDouble(arraybyte_readdata, 0);
                    vel_vector.Add(vel_vector0);
                }
                for (int i = 0; i < count; i++)//添加stepnum
                {
                    int stepnum0 = 0;//存储每一步stepnum
                    path_stream.Read(arraybyte_readcount, 0, 4);
                    stepnum0 = BitConverter.ToInt32(arraybyte_readcount, 0);
                    stepnum.Add(stepnum0);
                }
                for (int i = 0; i < count; i++)//添加stepwork
                {
                    int stepwork0 = 0;//存储每一步stepwork
                    path_stream.Read(arraybyte_readcount, 0, 4);
                    stepwork0 = BitConverter.ToInt32(arraybyte_readcount, 0);
                    stepwork.Add(stepwork0);
                }
                path_stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开文件失败，请重试！" + ex.ToString(), "错误");
                return;
            }
        }
        /// <summary>
  /// 保存数据函数
  /// </summary>
  /// <param name="PATHFILE"></param>
        public void SaveData(string PATHFILE)
        {
            try
            {
                int count = stepnum.Count;
                int subcount = q.Count;
                FileStream file_path = new FileStream(PATHFILE, FileMode.Create);
                byte[] arraybyte4 = new byte[4];//存储4位byte
                byte[] arraybyte8 = new byte[8];//存储8位byte
                byte[] arraybyte_key = new byte[4] { 0xF1, 0xE1, 0xD1, 0xA0 };//文件标识
                file_path.Seek(0, SeekOrigin.Begin);
                file_path.Write(arraybyte_key, 0, 4);//写入文件表示符
                arraybyte4 = BitConverter.GetBytes(count);//写入count
                file_path.Write(arraybyte4, 0, 4);
                arraybyte4 = BitConverter.GetBytes(subcount);//写入subcount
                file_path.Write(arraybyte4, 0, 4);
                for (int i = 0; i < subcount; i++)//写入q
                {
                    for (int j = 0; j < 6; j++)
                    {
                        arraybyte8 = BitConverter.GetBytes(((double[])q[i])[j]);
                        file_path.Write(arraybyte8, 0, 8);
                    }
                }
                for (int i = 0; i < subcount; i++)//写入qd
                {
                    for (int j = 0; j < 6; j++)
                    {
                        arraybyte8 = BitConverter.GetBytes(((double[])qd[i])[j]);
                        file_path.Write(arraybyte8, 0, 8);
                    }
                }
                for (int i = 0; i < subcount; i++)//写入qdd
                {
                    for (int j = 0; j < 6; j++)
                    {
                        arraybyte8 = BitConverter.GetBytes(((double[])qdd[i])[j]);
                        file_path.Write(arraybyte8, 0, 8);
                    }
                }
                for (int i = 0; i < subcount; i++)//写入速度
                {
                    arraybyte8 = BitConverter.GetBytes((double)vel_vector[i]);
                    file_path.Write(arraybyte8, 0, 8);
                }
                for (int i = 0; i < count; i++)//写入stepnum
                {
                    arraybyte4 = BitConverter.GetBytes((int)stepnum[i]);
                    file_path.Write(arraybyte4, 0, 4);
                }
                for (int i = 0; i < count; i++)//写入stepwork
                {
                    arraybyte4 = BitConverter.GetBytes((int)stepwork[i]);
                    file_path.Write(arraybyte4, 0, 4);
                }
                file_path.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件保存出错，请重试！" + ex.ToString(), "错误");
                return;
            }
        }
    }
    /// <summary>
    /// 打孔器机械臂类
    /// </summary>
    public class HoleRobot:Robot
    {
        public delegate void PropertyChangedHander();
        public event PropertyChangedHander PropertyChanged;
        private int keypointnum = 6;//关键点的个数
        private int holekeypointnum = 4;//关键孔位点数量
        private int holepointnum = 96;//孔位点数量
        private ArrayList keypoint = new ArrayList();//关键点集合
        private ArrayList holepoint = new ArrayList();//96孔板孔位集合
        private ArrayList holekeypoint = new ArrayList();//96孔板关键点集合（四个角的点）第一个：A1,第二个H1，第三个:A12,第四个:H12
        private bool[,] holechoose;//96孔需加样的孔位
        private bool keypointfinished=false;//关键点是否标定完成
        private bool holekeypointfinished=false;//孔位关键点是否标定完成
        private bool holepointfinished=false;//空位点是否标定完成
        private ArrayList arrayhole = new ArrayList();//存储用于holestep和step转换的数组

        #region property
        /// <summary>
        /// 关键点集合属性
        /// </summary>
        public ArrayList KeyPoint
        {
            get { return keypoint; }
            private set
            {
                keypoint = value;
                if (keypoint.Count==keypointnum)
                {
                    KeyPointFinished = true;
                }
                else
                {
                    KeyPointFinished = false;
                }
                
            }
        }
        /// <summary>
        /// 96孔板孔位集合属性
        /// </summary>
        public ArrayList HolePoint
        {
            get { return holepoint; }
            private set
            {
                holepoint = value;
                if (holepoint.Count==holepointnum)
                {
                    HolePointFinished = true;
                }
                else
                {
                    HolePointFinished = false;
                }
               
            }
        }
        /// <summary>
        /// 96孔板关键点集合
        /// </summary>
        public ArrayList HoleKeyPoint
        {
            get { return holekeypoint; }
            private set
            {
                holekeypoint = value;
                if (holekeypoint.Count==holekeypointnum)
                {
                    HoleKeyPointFinished = true;
                }
                else
                {
                    HoleKeyPointFinished = false;
                }
                
            }
        }
        /// <summary>
        /// 96孔需加样的孔位
        /// </summary>
        public bool[,] HoleChoose
        {
            get { return holechoose; }
            set {
                if(holechoose!=value)
                {

                }
                holechoose = value; }
        }
        /// <summary>
        /// 关键点个数
        /// </summary>
        public int KeyPointNum
        {
            get { return keypointnum; }
        }
        /// <summary>
            /// 板孔点个数
            /// </summary>
        public int HolePointNmu
        {
            get { return holepointnum; }
        }
        /// <summary>
        /// 关键孔位点数量
        /// </summary>
        public int HoleKeyPointNmu
        {
            get { return holekeypointnum; }
        }
        /// <summary>
        /// keypoint是否添加完成属性
        /// </summary>
        public bool KeyPointFinished
        {
            get { return keypointfinished; }
            private set
            {
                if (keypointfinished != value)
                {
                    keypointfinished = value;
                    PropertyChanged();
                }
                
            }
        }
        /// <summary>
        /// holepoint是否添加完成属性
        /// </summary>
        public bool HolePointFinished
        {
            get { return holepointfinished; }
            private set
            {
                if (holepointfinished != value)
                {
                    holepointfinished = value;
                    PropertyChanged();
                }
            }
        }
        /// <summary>
        /// holekeypoint是否添加完成
        /// </summary>
        public bool HoleKeyPointFinished
        {
            get { return holekeypointfinished; }
            private set
            {
                if (holekeypointfinished != value)
                {
                    holekeypointfinished = value;
                    PropertyChanged();
                }  
            }
        }
        /// <summary>
        /// 是否已生成过Q属性
        /// </summary>
        public bool QGenerateFinished
        {
            get { return Q.Count != 0; }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CARD_ID"></param>
        public HoleRobot(ushort CARD_ID):base(CARD_ID)
        {
        }
        /// <summary>
        /// 添加关键点
        /// </summary>
        public void AddKeyPoint()
        {
            double[] K = new double[6];
            LTDMC.dmc_get_position_unit(card_id, axisx, ref K[0]);
            LTDMC.dmc_get_position_unit(card_id, axisy, ref K[1]);
            LTDMC.dmc_get_position_unit(card_id, axisz, ref K[2]);
            LTDMC.dmc_get_position_unit(card_id, axisa, ref K[3]);
            LTDMC.dmc_get_position_unit(card_id, axisb, ref K[4]);
            LTDMC.dmc_get_position_unit(card_id, axisc, ref K[5]);
            keypoint.Add(K);
            KeyPoint = keypoint;//给属性赋值，触发属性改变事件，下同
        }
        /// <summary>
        /// 添加孔位点
        /// </summary>
        public void AddHoleKeyPoint()
        {
            double[] H = new double[6];
            LTDMC.dmc_get_position_unit(card_id, axisx, ref H[0]);
            LTDMC.dmc_get_position_unit(card_id, axisy, ref H[1]);
            LTDMC.dmc_get_position_unit(card_id, axisz, ref H[2]);
            LTDMC.dmc_get_position_unit(card_id, axisa, ref H[3]);
            LTDMC.dmc_get_position_unit(card_id, axisb, ref H[4]);
            LTDMC.dmc_get_position_unit(card_id, axisc, ref H[5]);
            holekeypoint.Add(H);
            HoleKeyPoint = holekeypoint;
        }
        /// <summary>
        /// 添加holekeypoint后，生成holepoint
        /// </summary>
        public void GenerateHolePoint()
        {
            int holepointnum = holepoint.Count;
           for (int i=0;i< holepointnum;i++)
            {
                holepoint.RemoveAt(holepoint.Count - 1);
            }
            double[,] Q0 = new double[8, 6];//每一列点的集合
            double[,] Q1 = new double[12, 6];//第A排点的集合
            double[,] Q2 = new double[12, 6];//第H排点的集合
            double[,] QD = new double[8, 6];
            double[,] QDD = new double[8, 6];
            double[,] QD1 = new double[12, 6];
            double[,] QDD1 = new double[12, 6];
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    CTRAJ(ref Q1, (double[])(holekeypoint[0]), (double[])(holekeypoint[2]), 12);//时间先默认3秒
                   // JTRAJ(ref Q1, ref QD1, ref QDD1, (double[])(holekeypoint[0]), (double[])(holekeypoint[2]), 12, 3);//时间先默认3秒
                    break;//如果成功就退出
                }
                catch
                {
                    if (i == 9)
                    {
                        MessageBox.Show("生成holepoint失败，请重试！", "错误");
                        return;
                    }
                }
            }
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    CTRAJ(ref Q2, (double[])(holekeypoint[1]), (double[])(holekeypoint[3]), 12);//时间先默认3秒
                   // JTRAJ(ref Q2, ref QD1, ref QDD1, (double[])(holekeypoint[1]), (double[])(holekeypoint[3]), 12, 3);//时间先默认3秒
                    break;//如果成功就退出
                }
                catch
                {
                    if (i == 9)
                    {
                        MessageBox.Show("生成holepoint失败，请重试！", "错误");
                        return;
                    }
                }
            }
            for (int i = 0; i < 12; i++)
            {
                double[] q1 = new double[6];
                double[] q2 = new double[6];
                for (int j = 0; j < 6; j++)
                {
                    q1[j] = Q1[i, j];
                    q2[j] = Q2[i, j];
                }
                for (int j = 0; j < 10; j++)
                {
                    try
                    {
                        CTRAJ(ref Q0,q1, q2, 8);
                        //JTRAJ(ref Q0, ref QD, ref QDD, q1, q2, 8, 3);
                        break;
                    }
                    catch
                    {
                        if (j == 9)
                        {
                            MessageBox.Show("生成holepoint失败，请重试！", "错误");
                            return;
                        }
                    }
                }
                for (int j = 0; j < 8; j++)
                {
                    double[] q = new double[6];
                    for (int w = 0; w < 6; w++)
                    {
                        q[w] = Q0[j, w];
                    }
                    holepoint.Add(q);
                    HolePoint = holepoint;
                }
            }
        }
        /// <summary>
        /// 删除KeyPoint
        /// </summary>
        public void DeleteKeyPoint()
        {
            keypoint.RemoveAt(keypoint.Count - 1);
            KeyPoint = keypoint;
        }
        /// <summary>
        /// 删除HolePoint
        /// </summary>
        public void DeleteHolePoint()
        {
            holepoint.RemoveAt(holepoint.Count - 1);
            HolePoint = holepoint;
        }
        /// <summary>
        /// 删除holekeypoint
        /// </summary>
        public void DeleteHoleKeyPoint()
        {
            holekeypoint.RemoveAt(holekeypoint.Count - 1);
            HoleKeyPoint = holekeypoint;
        }
        /// <summary>
        /// 添加Q
        /// </summary>
        public void AddQ(double[] POINT,double PERCENT)
        {
            double[] qd1 = new double[6];//先初始化为零
            double[] qdd1 = new double[6];
            q.Add(POINT);
            qd.Add(qd1);
            qdd.Add(qdd1);
            stepwork.Add(0);
            stepnum.Add(1);
            vel_vector.Add(PERCENT * default_vel);
            /*double[] q1 = new double[6];
            double[] qd1 = new double[6];//先初始化为零
            double[] qdd1 = new double[6];
            double[,] Qr = new double[2, 6];
            double[,] QDr = new double[2, 6];
            double[,] QDDr = new double[2, 6];
            double[] vel_vector1 = new double[2];
            int n = 0;
            n = stepwork.Count - 1;
            q1 = POINT;
            if (n >= 0)
            {
                while ((int)stepwork[n] != 0)
                {
                    n = n - 1;
                    if (n < 0)
                    {
                        break;
                    }
                }
                if (n >= 0)
                {
                    for(int i = 0; i < 10; i++)//循环插值10次，保证插值成功
                    {
                        try
                        {
                            int subn = TransSub(n);
                            JTRAJ(ref Qr, ref QDr, ref QDDr, (double[])Q[subn], q1, 2, 3);
                            break;//如果插值成功，则跳出
                        }
                        catch
                        {
                            if (i==9)//如果第九次都未成功，则跳出返回
                            {
                                MessageBox.Show("无法插值，请换个位置！", "提示");
                                return;
                            } 
                        }
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        double[] Qt = new double[6];//必须每循环一次声明一次
                        double[] QDt = new double[6];
                        double[] QDDt = new double[6];
                        for (int j = 0; j < 6; j++)
                        {
                            Qt[j] = Qr[i, j];
                            QDt[j] = QDr[i, j];
                            QDDt[j] = QDDr[i, j];
                        }
                        q.Add(Qt);
                        qd.Add(QDt);
                        qdd.Add(QDt);
                        vel_vector1[i] =PERCENT*default_vel;
                        vel_vector.Add(vel_vector1[i]);
                    }
                    stepwork.Add(0);
                    stepnum.Add(2);
                }
                else if (n == -1)
                {
                    q.Add(q1);
                    qd.Add(qd1);
                    qdd.Add(qdd1);
                    stepwork.Add(0);
                    stepnum.Add(1);
                    vel_vector.Add((double)0);
                }
                else
                {
                    MessageBox.Show("添加出错，请重试！", "错误");
                    return;
                }
            }
            else
            {
                q.Add(q1);
                qd.Add(qd1);
                qdd.Add(qdd1);
                stepwork.Add(0);
                stepnum.Add(1);
                vel_vector.Add((double)0);
            }*/
        }
        /// <summary>
        /// 删除n个位置、速度、加速度集合的元素
        /// </summary>
        /// <param name="n"></param>
        new public void DeleteQ(int n)
        {
            int allstep = q.Count - 1;
            int count = stepwork.Count - 1;
            if (count + 1 < n)
            {
                throw new Exception("删除步数过多");
            }
            for (int i = 0; i < n; i++)
            {
                allstep = q.Count - 1;
                count = stepwork.Count - 1;
                if ((int)stepwork[count] != 0)
                {
                    stepnum.RemoveAt(count);
                    stepwork.RemoveAt(count);
                    q.RemoveAt(allstep);
                    qdd.RemoveAt(allstep);
                    qd.RemoveAt(allstep);
                    vel_vector.RemoveAt(allstep);

                }
                else
                {
                    for (int j = 0; j < (int)stepnum[count]; j++)
                    {
                        q.RemoveAt(allstep);
                        qdd.RemoveAt(allstep);
                        qd.RemoveAt(allstep);
                        vel_vector.RemoveAt(allstep);
                        allstep = allstep - 1;
                    }
                    stepnum.RemoveAt(count);
                    stepwork.RemoveAt(count);
                    count = count - 1;
                }
            }
            step = -1;
            substep = -1;
            PropertyChanged();
        }
        /// <summary>
        /// 生成Q
        /// </summary>
        public void GenerateQ()
        {
            DeleteQ(stepwork.Count);
            int n = arrayhole.Count;
            arrayhole.Clear();
            if(KeyPointFinished&&HolePointFinished)
            {
                AddQ((double[])keypoint[0],1);
                for (int i = 0; i < 95; i++)
                {
                    int k = 0;
                    int w = 0;
                    Trans1DTo2D(ref k, ref w, i);
                    if(holechoose[k,w]==true)
                    {
                        AddQ((double[])keypoint[1],1);
                        AddQ((double[])keypoint[2],0.25);
                        AddDelay(1);
                        AddQ((double[])keypoint[1],0.5);
                        AddQ((double[])keypoint[3],1);
                        AddQ((double[])keypoint[4],1);
                        AddQ((double[])keypoint[5],1);
                        AddQ((double[])holepoint[i],0.25);
                        AddHole();
                        AddDelay(2);
                        arrayhole.Add(stepwork.Count-1);
                        AddQ((double[])keypoint[5],0.5);
                        AddQ((double[])keypoint[4],1);
                        AddQ((double[])keypoint[3],1);
                    }
                }
                if(holechoose[7,11]==true)
                {
                    AddQ((double[])keypoint[1],1);
                    AddQ((double[])keypoint[2],0.25);
                    AddDelay(1);
                    AddQ((double[])keypoint[1],0.5);
                    AddQ((double[])keypoint[3],1);
                    AddQ((double[])keypoint[4],1);
                    AddQ((double[])keypoint[5],1);
                    AddQ((double[])holepoint[95],0.25);
                    AddHole();
                    AddDelay(2);
                    arrayhole.Add(stepwork.Count-1);
                    AddQ((double[])keypoint[5],0.5);
                }
                AddQ((double[])keypoint[0], 1);
            }
            else
            {
                MessageBox.Show("关键点或空位点数量错误！", "错误");
                return;
            }
            PropertyChanged();
        }
        /// <summary>
        /// 开始运动
        /// </summary>
        public void Start()
        {
            double max_vel = 0;
          
                for (int k = 0; k <= stepnum.Count-1; k++)
                {
                    step = k;
                    substep = TransSub(step) - (int)stepnum[step] + 1;
                    if ((int)stepwork[step] == 0)//如果为运动
                    {
                        for (int i = 0; i < (int)stepnum[step]; i++)
                        {
                            max_vel = (double)vel_vector[substep];
                            //当出现max_vel为零，而要运动的位置不为零时
                            if (max_vel != 0)
                            {
                                LTDMC.dmc_set_vector_profile_unit(card_id, 0, 0, max_vel, 0.1, 0.1, 0);
                                LTDMC.dmc_conti_set_s_profile(card_id, 0, 0, 0.05);
                                LTDMC.dmc_line_unit(card_id, 0, 6, new ushort[] { 0, 1, 2, 3, 4, 5 }, (double[])q[substep], 1);
                                while (LTDMC.dmc_check_done_multicoor(card_id, 0) == 0)
                                {
                                    Application.DoEvents();
                                }
                            }
                            substep = substep + 1;
                        }
                    }
                    else if ((int)stepwork[step] == 1)//如果为延时
                    {
                        double second = ((double[])q[substep])[0];
                        delayTime(second);
                        substep = substep + 1;
                    }
                    else if ((int)stepwork[step] == 2)//如果为泵操作
                    {
                        double operation = ((double[])q[substep])[0];
                        double velocity = ((double[])q[substep])[1];
                        double volume = ((double[])q[substep])[2];
                        if (operation == 1)
                        {
                            PumpAbsorb((int)velocity, volume);//速度单位为转/min
                        }
                        else
                        {
                            PumpEject((int)velocity, volume);
                        }
                        substep = substep + 1;
                    }
                    else if ((int)stepwork[step] == 3)//如果为打孔器操作
                    {
                        HoleOperate();
                        substep = substep + 1;
                    }
                    else if ((int)stepwork[step] == 4)//如果为机械爪操作
                    {
                        double operation = ((double[])q[substep])[0];
                        if (operation == 1)
                        {
                            ClawOperate(0);
                        }
                        else
                        {
                            ClawOperate(1);
                        }
                        substep = substep + 1;
                    }
                    if (ifmove == false)//如果检测到运动标志位为false
                    {
                        return;
                    }
            }
        }
        /// <summary>
        /// Checks[,]数组的下标变换到1维
        /// </summary>
        /// <param name="I"></param>
        /// <param name="J"></param>
        /// <returns></returns>
        public int Trans2DTo1D(int I,int J)
        {
            return I + J * 8;
        }
        /// <summary>
        /// Checks[,]数组的下标变换到2维
        /// </summary>
        /// <param name="I"></param>
        /// <param name="J"></param>
        /// <param name="K"></param>
        public void Trans1DTo2D(ref int I,ref int J,int K)
        {
            I = K %8;
            J = K / 8;
        }
        /// <summary>
        /// 计算目前正在操作哪个孔。
        /// </summary>
        /// <param name="STEP"></param>
        /// <returns></returns>
        public int TransStepToHoleStep(int STEP)
        {
            int _holestep = 0;
            int holestep = -1;
            int _step = step;
            int n = stepwork.Count;
            int m = arrayhole.Count;
            int k = -1;
            if(m>0)
            {
                while (_step > (int)arrayhole[_holestep])
                {
                    _holestep = _holestep + 1;
                    if (_holestep >= m)
                    {
                        _holestep = _holestep - 1;
                        break;
                    }      
                } 
            }
           else
            {
                _holestep = -1;
            }
            for(int j=0;j<12;j++)
            {
                for(int i=0;i<8;i++)
                {
                    if(HoleChoose[i,j]==true)
                    {
                        holestep = holestep + 1;
                    }
                    k = k + 1;
                    if(holestep==_holestep)
                    {
                        return k;
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// 将路径为path的文件的数据读取到Robot的类中
        /// </summary>
        /// <param name="PATH"></param>
        new public void AddData(string PATHFILE)
        {
            try
            {
                int nk = keypoint.Count;
                int nh = holekeypoint.Count;
                for (int i=0;i<nk;i++)
                {
                    DeleteKeyPoint();//清空keypoint
                }
                for(int i=0;i<nh;i++)
                {
                    DeleteHoleKeyPoint();//清空holepoint
                }
                FileStream path_stream = new FileStream(PATHFILE, FileMode.Open);
                byte[] arraybyte4 = new byte[4];//读取数据文件标，占4位
                byte[] arraybyte8 = new byte[8];//读取的数据数组，占8位
                byte[] arraybyte_key = new byte[4] { 0xF2, 0xE1, 0xD1, 0xA0 };//将读取得数据文件表示与此数组对比
                path_stream.Seek(0, SeekOrigin.Begin);
                path_stream.Read(arraybyte4, 0, 4);//读取文件标识
                if (arraybyte4[0] != arraybyte_key[0] || arraybyte4[1] != arraybyte_key[1] || arraybyte4[2] != arraybyte_key[2] || arraybyte4[3] != arraybyte_key[3])//如果文件表示对不上，则提示文件格式错误,可改为浅度复制
                {
                    MessageBox.Show("文件格式错误，请检查文件！", "错误");
                    return;
                }
                for (int i = 0; i < keypointnum; i++)//添加keypoint
                {
                    double[] k0 = new double[6];//存储每一步的k,没循环一次，都要创建一个k0，下同
                    for (int j = 0; j < 6; j++)
                    {
                        path_stream.Read(arraybyte8, 0, 8);
                        k0[j] = BitConverter.ToDouble(arraybyte8, 0);
                    }
                    keypoint.Add(k0);
                    KeyPoint = keypoint;
                }
                for (int i = 0; i < holekeypointnum; i++)//添加holepoint
                {
                    double[] h0 = new double[6];//存储每一步的h0
                    for (int j = 0; j < 6; j++)
                    {
                        path_stream.Read(arraybyte8, 0, 8);
                        h0[j] = BitConverter.ToDouble(arraybyte8, 0);
                    }
                    holekeypoint.Add(h0);
                    HoleKeyPoint = holekeypoint;
                }
                path_stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开文件失败，请重试！" + ex.ToString(), "错误");
                return;
            }
        }
        /// <summary>
        /// 保存数据函数
        /// </summary>
        /// <param name="PATHFILE"></param>
        new public void SaveData(string PATHFILE)
        {
            try
            {
                if(keypoint.Count!=keypointnum||holepoint.Count!=holepointnum)//如果插入的关键点和96孔点不够
                {
                    MessageBox.Show("请插入所有数据点再保存！", "错误");
                    return;
                }
                int kcount =keypoint.Count;
                int hkcount = holekeypoint.Count;
                FileStream file_path = new FileStream(PATHFILE, FileMode.Create);
                byte[] arraybyte4 = new byte[4];//存储4位byte
                byte[] arraybyte8 = new byte[8];//存储8位byte
                byte[] arraybyte_key = new byte[4] { 0xF2, 0xE1, 0xD1, 0xA0 };//文件标识
                file_path.Seek(0, SeekOrigin.Begin);
                file_path.Write(arraybyte_key, 0, 4);//写入文件标示符
                for (int i = 0; i < kcount; i++)//写入keypoint
                {
                    for (int j = 0; j < 6; j++)
                    {
                        arraybyte8 = BitConverter.GetBytes(((double[])keypoint[i])[j]);
                        file_path.Write(arraybyte8, 0, 8);
                    }
                }
                for (int i = 0; i < hkcount; i++)//写入hpoint
                {
                    for (int j = 0; j < 6; j++)
                    {
                        arraybyte8 = BitConverter.GetBytes(((double[])holekeypoint[i])[j]);
                        file_path.Write(arraybyte8, 0, 8);
                    }
                }
                file_path.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件保存出错，请重试！" + ex.ToString(), "错误");
                return;
            }
        }
    }
    /// <summary>
    /// 微流量泵机械臂类
    /// </summary>
    public class PumpRobot:Robot
    {
        private int keypointnum = 2;//关键点的个数
        private int holepointnum = 96;//空位点数量
        private int holekeypointnum = 4;//关键孔位点数量
        private ArrayList keypoint = new ArrayList();//关键点集合
        private ArrayList holepoint = new ArrayList();//96孔板孔位集合
        private ArrayList holekeypoint = new ArrayList();//96孔板关键点集合（四个角的点）第一个：A1,第二个H1，第三个:A12,第四个:H12
        private bool[,] holechoose;//96孔需加样的孔位
        private int defpumpspeed=200;//泵默认速度
        private double pumpvolume=0.1;//泵的一次进给量
        private bool keypointfinished = false;//关键点是否标定完成
        private bool holekeypointfinished = false;//孔位关键点是否标定完成
        private bool holepointfinished = false;//空位点是否标定完成
        public delegate void PropertyChangedHander();
        public event PropertyChangedHander PropertyChanged;
        private ArrayList arrayhole = new ArrayList();//存储用于holestep和step转换的数组

        #region property
        /// <summary>
        /// 关键点集合属性
        /// </summary>
        public ArrayList KeyPoint
        {
            get { return keypoint; }
            private set
            {
                keypoint = value;
                if (keypoint.Count == keypointnum)
                {
                    KeyPointFinished = true;
                }
                else
                {
                    KeyPointFinished = false;
                }

            }
        }
        /// <summary>
        /// 96孔板孔位集合属性
        /// </summary>
        public ArrayList HolePoint
        {
            get { return holepoint; }
            private set
            {
                holepoint = value;
                if (holepoint.Count == holepointnum)
                {
                    HolePointFinished = true;
                }
                else
                {
                    HolePointFinished = false;
                }

            }
        }
        /// <summary>
        /// 96孔板关键点集合
        /// </summary>
        public ArrayList HoleKeyPoint
        {
            get { return holekeypoint; }
            private set
            {
                holekeypoint = value;
                if (holekeypoint.Count == holekeypointnum)
                {
                    HoleKeyPointFinished = true;
                }
                else
                {
                    HoleKeyPointFinished = false;
                }

            }
        }
        /// <summary>
        /// 96孔需加样的孔位
        /// </summary>
        public bool[,] HoleChoose
        {
            get { return holechoose; }
            set
            {
                if (holechoose != value)
                {

                }
                holechoose = value;
            }
        }
        /// <summary>
        /// 关键点个数
        /// </summary>
        public int KeyPointNum
        {
            get { return keypointnum; }
        }
        /// <summary>
        /// 板孔点个数
        /// </summary>
        public int HolePointNmu
        {
            get { return holepointnum; }
        }
        /// <summary>
        /// 关键孔位点数量
        /// </summary>
        public int HoleKeyPointNmu
        {
            get { return holekeypointnum; }
        }
        /// <summary>
        /// keypoint是否添加完成属性
        /// </summary>
        public bool KeyPointFinished
        {
            get { return keypointfinished; }
            private set
            {
                if (keypointfinished != value)
                {
                    keypointfinished = value;
                    PropertyChanged();
                }

            }
        }
        /// <summary>
        /// holepoint是否添加完成属性
        /// </summary>
        public bool HolePointFinished
        {
            get { return holepointfinished; }
            private set
            {
                if (holepointfinished != value)
                {
                    holepointfinished = value;
                    PropertyChanged();
                }
            }
        }
        /// <summary>
        /// holekeypoint是否添加完成
        /// </summary>
        public bool HoleKeyPointFinished
        {
            get { return holekeypointfinished; }
            private set
            {
                if (holekeypointfinished != value)
                {
                    holekeypointfinished = value;
                    PropertyChanged();
                }
            }
        }
        /// <summary>
        /// 是否已生成过Q属性
        /// </summary>
        public bool QGenerateFinished
        {
            get { return Q.Count != 0; }
        }
        /// <summary>
        /// 泵的默认速度
        /// </summary>
        public int DefPumpSpeed
        {
            get { return defpumpspeed; }
            set { defpumpspeed = value; }
        }
        /// <summary>
        /// 泵的一次进给量
        /// </summary>
        public double PumpVolume
        {
            get { return pumpvolume; }
            set { pumpvolume = value; }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CARD_ID"></param>
        public PumpRobot(ushort CARD_ID):base(CARD_ID)
        {

        }
        /// <summary>
        /// 添加关键点
        /// </summary>
        /// <summary>
        /// 添加关键点
        /// </summary>
        public void AddKeyPoint()
        {
            double[] K = new double[6];
            LTDMC.dmc_get_position_unit(card_id, axisx, ref K[0]);
            LTDMC.dmc_get_position_unit(card_id, axisy, ref K[1]);
            LTDMC.dmc_get_position_unit(card_id, axisz, ref K[2]);
            LTDMC.dmc_get_position_unit(card_id, axisa, ref K[3]);
            LTDMC.dmc_get_position_unit(card_id, axisb, ref K[4]);
            LTDMC.dmc_get_position_unit(card_id, axisc, ref K[5]);
            keypoint.Add(K);
            KeyPoint = keypoint;//给属性赋值，触发属性改变事件，下同
        }
        /// <summary>
        /// 添加孔位点
        /// </summary>
        public void AddHoleKeyPoint()
        {
            double[] H = new double[6];
            LTDMC.dmc_get_position_unit(card_id, axisx, ref H[0]);
            LTDMC.dmc_get_position_unit(card_id, axisy, ref H[1]);
            LTDMC.dmc_get_position_unit(card_id, axisz, ref H[2]);
            LTDMC.dmc_get_position_unit(card_id, axisa, ref H[3]);
            LTDMC.dmc_get_position_unit(card_id, axisb, ref H[4]);
            LTDMC.dmc_get_position_unit(card_id, axisc, ref H[5]);
            holekeypoint.Add(H);
            HoleKeyPoint = holekeypoint;
        }
        /// <summary>
        /// 添加holekeypoint后，生成holepoint
        /// </summary>
        public void GenerateHolePoint()
        {
            int holepointnum = holepoint.Count;
            for (int i = 0; i < holepointnum; i++)
            {
                holepoint.RemoveAt(holepoint.Count - 1);
            }
            double[,] Q0 = new double[8, 6];//每一列点的集合
            double[,] Q1 = new double[12, 6];//第A排点的集合
            double[,] Q2 = new double[12, 6];//第H排点的集合
            double[,] QD = new double[8, 6];
            double[,] QDD = new double[8, 6];
            double[,] QD1 = new double[12, 6];
            double[,] QDD1 = new double[12, 6];
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    CTRAJ(ref Q1, (double[])(holekeypoint[0]), (double[])(holekeypoint[2]), 12);//时间先默认3秒
                                                                                                // JTRAJ(ref Q1, ref QD1, ref QDD1, (double[])(holekeypoint[0]), (double[])(holekeypoint[2]), 12, 3);//时间先默认3秒
                    break;//如果成功就退出
                }
                catch
                {
                    if (i == 9)
                    {
                        MessageBox.Show("生成holepoint失败，请重试！", "错误");
                        return;
                    }
                }
            }
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    CTRAJ(ref Q2, (double[])(holekeypoint[1]), (double[])(holekeypoint[3]), 12);//时间先默认3秒
                                                                                                // JTRAJ(ref Q2, ref QD1, ref QDD1, (double[])(holekeypoint[1]), (double[])(holekeypoint[3]), 12, 3);//时间先默认3秒
                    break;//如果成功就退出
                }
                catch
                {
                    if (i == 9)
                    {
                        MessageBox.Show("生成holepoint失败，请重试！", "错误");
                        return;
                    }
                }
            }
            for (int i = 0; i < 12; i++)
            {
                double[] q1 = new double[6];
                double[] q2 = new double[6];
                for (int j = 0; j < 6; j++)
                {
                    q1[j] = Q1[i, j];
                    q2[j] = Q2[i, j];
                }
                for (int j = 0; j < 10; j++)
                {
                    try
                    {
                        CTRAJ(ref Q0, q1, q2, 8);
                        //JTRAJ(ref Q0, ref QD, ref QDD, q1, q2, 8, 3);
                        break;
                    }
                    catch
                    {
                        if (j == 9)
                        {
                            MessageBox.Show("生成holepoint失败，请重试！", "错误");
                            return;
                        }
                    }
                }
                for (int j = 0; j < 8; j++)
                {
                    double[] q = new double[6];
                    for (int w = 0; w < 6; w++)
                    {
                        q[w] = Q0[j, w];
                    }
                    holepoint.Add(q);
                    HolePoint = holepoint;
                }
            }
        }
        /// <summary>
        /// 删除KeyPoint
        /// </summary>
        public void DeleteKeyPoint()
        {
            keypoint.RemoveAt(keypoint.Count - 1);
            KeyPoint = keypoint;
        }
        /// <summary>
        /// 删除HolePoint
        /// </summary>
        public void DeleteHolePoint()
        {
            holepoint.RemoveAt(holepoint.Count - 1);
            HolePoint = holepoint;
        }
        /// <summary>
        /// 删除holekeypoint
        /// </summary>
        public void DeleteHoleKeyPoint()
        {
            holekeypoint.RemoveAt(holekeypoint.Count - 1);
            HoleKeyPoint = holekeypoint;
        }
        /// <summary>
        /// 添加Q
        /// </summary>
        public void AddQ(double[] POINT, double PERCENT)
        {
            double[] qd1 = new double[6];//先初始化为零
            double[] qdd1 = new double[6];
            q.Add(POINT);
            qd.Add(qd1);
            qdd.Add(qdd1);
            stepwork.Add(0);
            stepnum.Add(1);
            vel_vector.Add(PERCENT * default_vel);
            /*double[] q1 = new double[6];
            double[] qd1 = new double[6];//先初始化为零
            double[] qdd1 = new double[6];
            double[,] Qr = new double[2, 6];
            double[,] QDr = new double[2, 6];
            double[,] QDDr = new double[2, 6];
            double[] vel_vector1 = new double[2];
            int n = 0;
            n = stepwork.Count - 1;
            q1 = POINT;
            if (n >= 0)
            {
                while ((int)stepwork[n] != 0)
                {
                    n = n - 1;
                    if (n < 0)
                    {
                        break;
                    }
                }
                if (n >= 0)
                {
                    for(int i = 0; i < 10; i++)//循环插值10次，保证插值成功
                    {
                        try
                        {
                            int subn = TransSub(n);
                            JTRAJ(ref Qr, ref QDr, ref QDDr, (double[])Q[subn], q1, 2, 3);
                            break;//如果插值成功，则跳出
                        }
                        catch
                        {
                            if (i==9)//如果第九次都未成功，则跳出返回
                            {
                                MessageBox.Show("无法插值，请换个位置！", "提示");
                                return;
                            } 
                        }
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        double[] Qt = new double[6];//必须每循环一次声明一次
                        double[] QDt = new double[6];
                        double[] QDDt = new double[6];
                        for (int j = 0; j < 6; j++)
                        {
                            Qt[j] = Qr[i, j];
                            QDt[j] = QDr[i, j];
                            QDDt[j] = QDDr[i, j];
                        }
                        q.Add(Qt);
                        qd.Add(QDt);
                        qdd.Add(QDt);
                        vel_vector1[i] =PERCENT*default_vel;
                        vel_vector.Add(vel_vector1[i]);
                    }
                    stepwork.Add(0);
                    stepnum.Add(2);
                }
                else if (n == -1)
                {
                    q.Add(q1);
                    qd.Add(qd1);
                    qdd.Add(qdd1);
                    stepwork.Add(0);
                    stepnum.Add(1);
                    vel_vector.Add((double)0);
                }
                else
                {
                    MessageBox.Show("添加出错，请重试！", "错误");
                    return;
                }
            }
            else
            {
                q.Add(q1);
                qd.Add(qd1);
                qdd.Add(qdd1);
                stepwork.Add(0);
                stepnum.Add(1);
                vel_vector.Add((double)0);
            }*/
        }
        /// <summary>
        /// 删除n个位置、速度、加速度集合的元素
        /// </summary>
        /// <param name="n"></param>
        new public void DeleteQ(int n)
        {
            int allstep = q.Count - 1;
            int count = stepwork.Count - 1;
            if (count + 1 < n)
            {
                throw new Exception("删除步数过多");
            }
            for (int i = 0; i < n; i++)
            {
                allstep = q.Count - 1;
                count = stepwork.Count - 1;
                if ((int)stepwork[count] != 0)
                {
                    stepnum.RemoveAt(count);
                    stepwork.RemoveAt(count);
                    q.RemoveAt(allstep);
                    qdd.RemoveAt(allstep);
                    qd.RemoveAt(allstep);
                    vel_vector.RemoveAt(allstep);

                }
                else
                {
                    for (int j = 0; j < (int)stepnum[count]; j++)
                    {
                        q.RemoveAt(allstep);
                        qdd.RemoveAt(allstep);
                        qd.RemoveAt(allstep);
                        vel_vector.RemoveAt(allstep);
                        allstep = allstep - 1;
                    }
                    stepnum.RemoveAt(count);
                    stepwork.RemoveAt(count);
                    count = count - 1;
                }
            }
            step = -1;
            substep = -1;
            PropertyChanged();
        }
        /// <summary>
        /// 生成Q
        /// </summary>
        public void GenerateQ()
        {
            DeleteQ(stepwork.Count);
            if (KeyPointFinished && HolePointFinished)
            {
                AddQ((double[])keypoint[0], 1);
                AddQ((double[])keypoint[1], 1);
                for (int i = 0; i < 95; i++)
                {
                    int k = 0;
                    int w = 0;
                    Trans1DTo2D(ref k, ref w, i);
                    if (holechoose[k, w] == true)
                    {
                       
                        AddQ((double[])holepoint[i], 0.5);
                        AddDelay(0.5);
                        AddPump(false, defpumpspeed, pumpvolume);
                        arrayhole.Add(stepwork.Count - 1);
                        AddDelay(0.5);
                    }
                }
                AddQ((double[])keypoint[1], 0.5);
                AddQ((double[])keypoint[0], 1);
            }
            else
            {
                MessageBox.Show("关键点或空位点数量错误！", "错误");
                return;
            }
            PropertyChanged();
        }
        /// <summary>
        /// 开始运动
        /// </summary>
        public void Start()
        {
            double max_vel = 0;

            for (int k = 0; k <= stepnum.Count - 1; k++)
            {
                step = k;
                substep = TransSub(step) - (int)stepnum[step] + 1;
                if ((int)stepwork[step] == 0)//如果为运动
                {
                    for (int i = 0; i < (int)stepnum[step]; i++)
                    {
                        max_vel = (double)vel_vector[substep];
                        //当出现max_vel为零，而要运动的位置不为零时
                        if (max_vel != 0)
                        {
                            LTDMC.dmc_set_vector_profile_unit(card_id, 0, 0, max_vel, 0.1, 0.1, 0);
                            LTDMC.dmc_conti_set_s_profile(card_id, 0, 0, 0.05);
                            LTDMC.dmc_line_unit(card_id, 0, 6, new ushort[] { 0, 1, 2, 3, 4, 5 }, (double[])q[substep], 1);
                            while (LTDMC.dmc_check_done_multicoor(card_id, 0) == 0)
                            {
                                Application.DoEvents();
                            }
                        }
                        substep = substep + 1;
                    }
                }
                else if ((int)stepwork[step] == 1)//如果为延时
                {
                    double second = ((double[])q[substep])[0];
                    delayTime(second);
                    substep = substep + 1;
                }
                else if ((int)stepwork[step] == 2)//如果为泵操作
                {
                    double operation = ((double[])q[substep])[0];
                    double velocity = ((double[])q[substep])[1];
                    double volume = ((double[])q[substep])[2];
                    if (operation == 1)
                    {
                        PumpAbsorb((int)velocity, volume);//速度单位为转/min
                    }
                    else
                    {
                        PumpEject((int)velocity, volume);
                    }
                    substep = substep + 1;
                }
                else if ((int)stepwork[step] == 3)//如果为打孔器操作
                {
                    HoleOperate();
                    substep = substep + 1;
                }
                else if ((int)stepwork[step] == 4)//如果为机械爪操作
                {
                    double operation = ((double[])q[substep])[0];
                    if (operation == 1)
                    {
                        ClawOperate(0);
                    }
                    else
                    {
                        ClawOperate(1);
                    }
                    substep = substep + 1;
                }
                if (ifmove == false)//如果检测到运动标志位为false
                {
                    return;
                }
            }
        }
        /// <summary>
        /// Checks[,]数组的下标变换到1维
        /// </summary>
        /// <param name="I"></param>
        /// <param name="J"></param>
        /// <returns></returns>
        public int Trans2DTo1D(int I, int J)
        {
            return I + J * 8;
        }
        /// <summary>
        /// Checks[,]数组的下标变换到2维
        /// </summary>
        /// <param name="I"></param>
        /// <param name="J"></param>
        /// <param name="K"></param>
        public void Trans1DTo2D(ref int I, ref int J, int K)
        {
            I = K % 8;
            J = K / 8;
        }       
        /// <summary>
        /// 计算目前正在操作哪个孔。
        /// </summary>
        /// <param name="STEP"></param>
        /// <returns></returns>
        public int TransStepToHoleStep(int STEP)
        {
            int _holestep = 0;
            int holestep = -1;
            int _step = step;
            int n = stepwork.Count;
            int m = arrayhole.Count;
            int k = -1;
            if (m > 0)
            {
                while (_step > (int)arrayhole[_holestep])
                {
                    _holestep = _holestep + 1;
                    if (_holestep >= m)
                    {
                        _holestep = _holestep - 1;
                        break;
                    }
                }
            }
            else
            {
                _holestep = -1;
            }
            for (int j = 0; j < 12; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (HoleChoose[i, j] == true)
                    {
                        holestep = holestep + 1;
                    }
                    k = k + 1;
                    if (holestep == _holestep)
                    {
                        return k;
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// 将路径为path的文件的数据读取到Robot的类中
        /// </summary>
        /// <param name="PATH"></param>
        new public void AddData(string PATHFILE)
        {
            try
            {
                int nk = keypoint.Count;
                int nh = holekeypoint.Count;
                for (int i = 0; i < nk; i++)
                {
                    DeleteKeyPoint();//清空keypoint
                }
                for (int i = 0; i < nh; i++)
                {
                    DeleteHoleKeyPoint();//清空holepoint
                }
                FileStream path_stream = new FileStream(PATHFILE, FileMode.Open);
                byte[] arraybyte4 = new byte[4];//读取数据文件标，占4位
                byte[] arraybyte8 = new byte[8];//读取的数据数组，占8位
                byte[] arraybyte_key = new byte[4] { 0xF2, 0xE1, 0xD1, 0xA0 };//将读取得数据文件表示与此数组对比
                path_stream.Seek(0, SeekOrigin.Begin);
                path_stream.Read(arraybyte4, 0, 4);//读取文件标识
                if (arraybyte4[0] != arraybyte_key[0] || arraybyte4[1] != arraybyte_key[1] || arraybyte4[2] != arraybyte_key[2] || arraybyte4[3] != arraybyte_key[3])//如果文件表示对不上，则提示文件格式错误,可改为浅度复制
                {
                    MessageBox.Show("文件格式错误，请检查文件！", "错误");
                    return;
                }
                for (int i = 0; i < keypointnum; i++)//添加keypoint
                {
                    double[] k0 = new double[6];//存储每一步的k,没循环一次，都要创建一个k0，下同
                    for (int j = 0; j < 6; j++)
                    {
                        path_stream.Read(arraybyte8, 0, 8);
                        k0[j] = BitConverter.ToDouble(arraybyte8, 0);
                    }
                    keypoint.Add(k0);
                    KeyPoint = keypoint;
                }
                for (int i = 0; i < holekeypointnum; i++)//添加holepoint
                {
                    double[] h0 = new double[6];//存储每一步的h0
                    for (int j = 0; j < 6; j++)
                    {
                        path_stream.Read(arraybyte8, 0, 8);
                        h0[j] = BitConverter.ToDouble(arraybyte8, 0);
                    }
                    holekeypoint.Add(h0);
                    HoleKeyPoint = holekeypoint;
                }
                path_stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开文件失败，请重试！" + ex.ToString(), "错误");
                return;
            }
        }
        /// <summary>
        /// 保存数据函数
        /// </summary>
        /// <param name="PATHFILE"></param>
        new public void SaveData(string PATHFILE)
        {
            try
            {
                if (keypoint.Count != keypointnum || holepoint.Count != holepointnum)//如果插入的关键点和96孔点不够
                {
                    MessageBox.Show("请插入所有数据点再保存！", "错误");
                    return;
                }
                int kcount = keypoint.Count;
                int hkcount = holekeypoint.Count;
                FileStream file_path = new FileStream(PATHFILE, FileMode.Create);
                byte[] arraybyte4 = new byte[4];//存储4位byte
                byte[] arraybyte8 = new byte[8];//存储8位byte
                byte[] arraybyte_key = new byte[4] { 0xF2, 0xE1, 0xD1, 0xA0 };//文件标识
                file_path.Seek(0, SeekOrigin.Begin);
                file_path.Write(arraybyte_key, 0, 4);//写入文件标示符
                for (int i = 0; i < kcount; i++)//写入keypoint
                {
                    for (int j = 0; j < 6; j++)
                    {
                        arraybyte8 = BitConverter.GetBytes(((double[])keypoint[i])[j]);
                        file_path.Write(arraybyte8, 0, 8);
                    }
                }
                for (int i = 0; i < hkcount; i++)//写入hpoint
                {
                    for (int j = 0; j < 6; j++)
                    {
                        arraybyte8 = BitConverter.GetBytes(((double[])holekeypoint[i])[j]);
                        file_path.Write(arraybyte8, 0, 8);
                    }
                }
                file_path.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件保存出错，请重试！" + ex.ToString(), "错误");
                return;
            }
        }
    }
}
