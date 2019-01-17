using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarleyBreak
{
    class GameArea
    {
        public int[,] area { get; private set; }

        public GameArea()
        {
            area = new int[4, 4];
            FillRandomArea();
        }

        public event Action GameWinned;

        /// <summary>
        /// Заполнение поля случайными неповторяющимися числами от 0 до 15
        /// </summary>
        private void FillRandomArea()
        {
            List<int> nums = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                nums.Add(i);
            }
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int randomId = rnd.Next(0, nums.Count - 1);
                    area[i, j] = nums[randomId];
                    nums.RemoveAt(randomId);
                }
            }
        }

        /// <summary>
        /// Проверка, закончена ли игра
        /// </summary>
        private void CheckForWin()
        {
            int nextNum = 0;
            foreach (int item in area)
            {
                if (item != nextNum)
                    return;
                nextNum++;
            }
            GameWinned.Invoke();
        }

        /// <summary>
        /// Попытка перемещения элемента на игровом поле
        /// </summary>        
        public void Move(int num)
        {
            //Ищем "координаты" элемента
            int x = 0, y = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (area[i, j] == num)
                    {
                        x = i;
                        y = j;
                    }
                }
            }
            //Проверяем, есть ли элемент со значением 0 (пустой квадрат) с какой-либо стороны, меняем местами значения если так.
            try
            {
                if (area[x - 1, y] == 0)
                {
                    area[x, y] += area[x - 1, y];
                    area[x - 1, y] = area[x, y] - area[x - 1, y];
                    area[x, y] -= area[x - 1, y];
                }
            }
            catch { };

            try
            {
                if (area[x + 1, y] == 0)
                {
                    area[x, y] += area[x + 1, y];
                    area[x + 1, y] = area[x, y] - area[x + 1, y];
                    area[x, y] -= area[x + 1, y];
                }
            }
            catch { };

            try
            {
                if (area[x, y - 1] == 0)
                {
                    area[x, y] += area[x, y - 1];
                    area[x, y - 1] = area[x, y] - area[x, y - 1];
                    area[x, y] -= area[x, y - 1];
                }
            }
            catch { };

            try
            {
                if (area[x, y + 1] == 0)
                {
                    area[x, y] += area[x, y + 1];
                    area[x, y + 1] = area[x, y] - area[x, y + 1];
                    area[x, y] -= area[x, y + 1];
                }
            }
            catch { };
            CheckForWin();
        }
    }
}
