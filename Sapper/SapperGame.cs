using System;
using System.Collections.Generic;
using System.Text;

namespace Sapper
{
    class SapperGame
    {
        //массив минное поле
        public int[,] Area { get; private set; }
        public int N { get; private set; }//размерность массива
        public int MinesCount { get; private set; }//количество мин
        public SapperGame()//создание поля по умолчанию
        {
            N = 10;
            MinesCount = 10;
            Area = new int[N, N];
            GenerateArea();
        }
        public SapperGame(int N, int MinesCount)//создание с заданными параметрами
        {
            this.N = Math.Abs(N);
            this.MinesCount = Math.Abs(MinesCount);

            if (MinesCount >= N * N)
                this.MinesCount = N;

            Area = new int[N, N];
            GenerateArea();
        }
        private void GenerateArea()
        {
            Random random = new Random();
            for (int i = 0; i < MinesCount; i++)
            {
                int x = random.Next(N);
                int y = random.Next(N);
                if (Area[x, y] < 10)
                {
                    Area[x, y] = 50;//берем заведомо число, большее 8, либо надо брать отрицательное
                    i++;
                    //соседи точки
                    if (x > 0) Area[x - 1, y]++;
                    if (x < N - 1) Area[x + 1, y]++;

                    if (x > 0 && y > 0) Area[x - 1, y - 1]++;
                    if (y > 0 && x < N - 1) Area[x + 1, y - 1]++;
                    if (y > 0) Area[x, y - 1]++;

                    if (x > 0 && y < N - 1) Area[x - 1, y + 1]++;
                    if (x < N - 1 && y < N - 1) Area[x + 1, y + 1]++;
                    if (y < N - 1) Area[x, y + 1]++;


                }

            }
        }
    }
}
