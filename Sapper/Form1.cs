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
            dataGridView1.ColumnCount = sapperGame.N;
            dataGridView1.RowCount = sapperGame.N;
            for(int i=0;i<sapperGame.N;i++)
            {
                dataGridView1.Rows[i].Height = 50;
                dataGridView1.Columns[i].Width = 50;
            }
            Height = 50 * sapperGame.N + 80;//форма становится под размер поля
            Width = 50 * sapperGame.N + 70;

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
            ShowData();
        }
    }
}
