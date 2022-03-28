using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sapper
{
    public partial class Form1 : Form
    {
        SapperGame sapperGame;

        public Form1()
        {
            InitializeComponent();
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
        }
        void RefreshGrid()
        {

            dataGridView1.ColumnCount = 50;
            dataGridView1.RowCount = 50;
            for (int i = 0; i < sapperGame.N; i++)
            {
                dataGridView1.Rows[i].Height = Height / sapperGame.N;//размер клеток подстраивается под размер формы
                dataGridView1.Columns[i].Width = Width / sapperGame.N;
            }
            /*Height = 50 * sapperGame.N + 100;//форма становится под размер поля
            Width = 50 * sapperGame.N + 70;*/

        }
        void ShowData()
        {
            for (int col = 0; col < sapperGame.N; col++)
                for (int row = 0; row < sapperGame.N; row++)
                    dataGridView1[col, row].Value = sapperGame.Area[row, col].ToString();
        }


        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            sapperGame = new SapperGame(10, 5);
            RefreshGrid();
            NewGame();
            //ShowData();
        }
        void NewGame()
        {
            for (int col = 0; col < sapperGame.N; col++)
                for (int row = 0; row < sapperGame.N; row++)
                {
                    dataGridView1[col, row].Style.BackColor = Color.White;
                    dataGridView1[col, row].Value = "";
                }

        }
        void DrawZero(int col, int row)
        {
            if (col < 0 || row < 0 || col > sapperGame.N - 1 || row > sapperGame.N - 1)//если мы за пределами массива
                return;

            if (sapperGame.Area[col, row] != 0)
            {
                dataGridView1[col, row].Style.BackColor = Color.Yellow;
                dataGridView1[col, row].Value = sapperGame.Area[row, col].ToString();
                return;
            }

            if (dataGridView1[col, row].Style.BackColor == Color.Yellow ||
                dataGridView1[col, row].Style.BackColor == Color.LightYellow)
                return;//уже нажимали

            dataGridView1[col, row].Style.BackColor = Color.LightYellow;

            DrawZero(col + 1, row);
            DrawZero(col - 1, row);

            DrawZero(col - 1, row - 1);
            DrawZero(col + 1, row - 1);
            DrawZero(col, row - 1);

            DrawZero(col - 1, row + 1);
            DrawZero(col + 1, row + 1);
            DrawZero(col, row + 1);

        }

        bool Winner()
        {
            int count = 0;
            for (int col = 0; col < sapperGame.N; col++)
                for (int row = 0; row < sapperGame.N; row++)
                {
                    if(dataGridView1[col,row].Style.BackColor==Color.White)
                    {
                        count++;
                    }
                }
            return count == sapperGame.MinesCount;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sapperGame.IsEnd == false)
            {
                int col = e.ColumnIndex;
                int row = e.RowIndex;
                if (sapperGame.Area[row, col] > 10)
                {
                    dataGridView1[col, row].Style.BackColor = Color.Red;
                    sapperGame.IsEnd = true;
                    MessageBox.Show("Вы подорвались на мине");
                }
                else if (sapperGame.Area[col, row] != 0)
                {
                    dataGridView1[col, row].Style.BackColor = Color.Yellow;
                    dataGridView1[col, row].Value = sapperGame.Area[row, col].ToString();

                }
                else
                {
                    DrawZero(col, row);
                    
                }

                if(!sapperGame.IsEnd&&Winner())
                {
                    MessageBox.Show("You are winner!");
                    sapperGame.IsEnd = true;
                }
            }
            dataGridView1.ClearSelection();

        }
    }
}
