using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gobang
{
    public partial class Form1 : Form
    {
        private bool start;
        private bool ChessCheck = true;
        private const int size = 13;
        private int[ , ] CheckBoard = new int[size+1,size+1];
        private int round = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initailizeGame();
            start = true;   
            button1.Text = "重新开始";
            label2.Text = "等待红子落子";
            round = 1;
            label4.Text = round + "";
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void initailizeGame()
        {
            start = false;

            for(int i = 0; i <= size; i++)
            {
                for(int j = 0; j <= size; j++)
                {
                    CheckBoard[i, j] = 0;
                }
            }

            Point pt0 = new Point(0, 0);
            int width = 30;
            var g = pictureBox1.CreateGraphics();  
            g.Clear(Color.White);
            var pen = new Pen(Color.Black);

            for(int line  = 0; line < size; line++)
            {
                for(int i = 0;i < size; i++)
                {
                    if (line % 2 == 0 && i % 2 == 0)
                    { g.DrawRectangle(pen, i * width + pt0.X, line * width + pt0.Y, width, width); }
                    else
                    { g.DrawRectangle(pen, i * width + pt0.X, line * width + pt0.Y, width, width); }
                }
            }
            g.Dispose();    
        }//初始化对局
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            if (start)
            {
                Point box1Point = pictureBox1.PointToClient(Control.MousePosition);

                int x = box1Point.X % 30 > 15 ? box1Point.X / 30 + 1 : box1Point.X / 30;
                int y = box1Point.Y % 30 > 15 ? box1Point.Y / 30 + 1 : box1Point.Y / 30;

                if (CheckBoard[x, y] != 0)
                {
                    MessageBox.Show("请勿重复落字");
                }
                else
                {
                    if (ChessCheck)
                    {
                        g.FillEllipse(Brushes.Red, x * 30 - 13, y * 30 - 13, 26, 22);
                        CheckBoard[x, y] = 1;
                        ChessCheck = !ChessCheck;
                        label2.Text = "等待黑子落棋";
                    }
                    else
                    {
                        g.FillEllipse(Brushes.Black, x * 30 - 13, y * 30 - 13, 26, 22);
                        CheckBoard[x, y] = -1;
                        ChessCheck = !ChessCheck;
                        label2.Text = "等待红子落棋";
                        round++;
                        label4.Text = round + " ";
                    }
                    g.Dispose();
                    SuccessCheck(x, y);
                }
            }
        }

        private void SuccessCheck(int x, int y)
        {
            int color = CheckBoard[x, y];
            int success = 0;
            int width = 30;
            if (x - 4 >= 0 && CheckBoard[x - 4, y] == color)//判断左边
            {
                for(int i = 0; i < 3; i++)
                {
                    if (CheckBoard[x - i - 1, y]  == color) { success++; }
                }
                if(success  == 3)
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Color.Red, 2), new Point(x * width, y * width), new Point((x - 4) * width, y * width));
                    MessageBox.Show(color == 1 ? "红子胜利！" : "黑子胜利！");
                    g.Dispose();
                }
                success = 0;
            }
            else if (x + 4 <= 13 && CheckBoard[x + 4, y] == color)//判断右边
            {
                for (int i = 0; i < 3; i++)
                {
                    if (CheckBoard[x + i + 1, y] == color) { success++; }
                }
                if (success == 3)
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Color.Red, 2), new Point(x * width, y * width), new Point((x + 4) * width, y * width));
                    MessageBox.Show(color == 1 ? "红子胜利！" : "黑子胜利！");
                    g.Dispose();
                }
                success = 0;
            }
            else if (y + 4 <= 13 && CheckBoard[x, y + 4] == color)//判断上边
            {
                for (int i = 0; i < 3; i++)
                {
                    if (CheckBoard[x, y + i + 1] == color) { success++; }
                }
                if (success == 3)
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Color.Red, 2), new Point(x * width, y * width), new Point(x * width, (y + 4) * width));
                    MessageBox.Show(color == 1 ? "红子胜利！" : "黑子胜利！");
                    g.Dispose();
                }
                success = 0;
            }
            else if (y - 4 >= 0 && CheckBoard[x, y - 4] == color)//判断下边
            {
                for (int i = 0; i < 3; i++)
                {
                    if (CheckBoard[x, y - i - 1] == color) { success++; }
                }
                if (success == 3)
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Color.Red, 2), new Point(x * width, y * width), new Point(x * width, (y - 4) * width));
                    MessageBox.Show(color == 1 ? "红子胜利！" : "黑子胜利！");
                    g.Dispose();
                }
                success = 0;
            }
            else if (x - 4 >= 0 && y - 4 >= 0 && CheckBoard[x - 4, y - 4] == color)//判断左上
            {
                for (int i = 0; i < 3; i++)
                {
                    if (CheckBoard[x - i - 1, y - i - 1] == color) { success++; }
                }
                if (success == 3)
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Color.Red, 2), new Point(x * width, y * width), new Point((x - 4) * width, (y - 4) * width));
                    MessageBox.Show(color == 1 ? "红子胜利！" : "黑子胜利！");
                    g.Dispose();
                }
                success = 0;
            }
            else if (x + 4 <= 13 && y + 4 <= 13 && CheckBoard[x + 4, y + 4] == color)//判断右下
            {
                for (int i = 0; i < 3; i++)
                {
                    if (CheckBoard[x + i + 1, y + i + 1] == color) { success++; }
                }
                if (success == 3)
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Color.Red, 2), new Point(x * width, y * width), new Point((x + 4) * width, (y + 4) * width));
                    MessageBox.Show(color == 1 ? "红子胜利！" : "黑子胜利！");
                    g.Dispose();
                }
                success = 0;
            }
            else if (x - 4 >= 0 && y + 4 <= 13 && CheckBoard[x - 4, y + 4] == color)//判断右上
            {
                for (int i = 0; i < 3; i++)
                {
                    if (CheckBoard[x - i - 1, y + i + 1] == color) { success++; }
                }
                if (success == 3)
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Color.Red, 2), new Point(x * width, y * width), new Point((x - 4) * width, (y + 4) * width));
                    MessageBox.Show(color == 1 ? "红子胜利！" : "黑子胜利！");
                    g.Dispose();
                }
                success = 0;
            }
            else if (x + 4 <= 13 && y - 4 >= 0 && CheckBoard[x + 4, y - 4] == color)//判断左上
            {
                for (int i = 0; i < 3; i++)
                {
                    if (CheckBoard[x + i + 1, y - i - 1] == color) { success++; }
                }
                if (success == 3)
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Color.Red, 2), new Point(x * width, y * width), new Point((x + 4) * width, (y - 4) * width));
                    MessageBox.Show(color == 1 ? "红子胜利！" : "黑子胜利！");
                    g.Dispose();
                }
                success = 0;
            }
        }
    }
}
