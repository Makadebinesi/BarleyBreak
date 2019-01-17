using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarleyBreak
{
    public partial class Form1 : Form
    {
        Button[,] btns;
        GameArea gameArea;

        public Form1()
        {
            InitializeComponent();

            btns = new Button[4, 4];
            BtnsIni();

            gameArea = new GameArea();
            gameArea.GameWinned += GameArea_GameWinned;
            UpdateGameTable();
        }

        private void GameArea_GameWinned()
        {
            MessageBox.Show("Игра закончена. Поздравляю!\nНачинается новая игра.");
            gameArea = new GameArea();
            UpdateGameTable();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            gameArea.Move(int.Parse(clickedBtn.Text));
            UpdateGameTable();
        }

        private void BtnsIni()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    btns[i, j] = this.Controls["button" + i + j] as Button;
                }
            }
        }

        private void UpdateGameTable()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    btns[i, j].Text = gameArea.area[i, j].ToString();
                }
            }
            foreach (var btn in btns)
            {
                if(btn.Text == "0")
                {
                    btn.Enabled = false;
                    btn.Text = "";
                }
                else
                {
                    btn.Enabled = true;
                }
            }
        }
    }
}
