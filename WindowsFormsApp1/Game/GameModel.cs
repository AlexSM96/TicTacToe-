using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace TicTacToe
{
    public static class GameModel
    {
        private const int _sizeMap = 3;
        private const int _cellSize = 100;
        private const int _menuHeight = 25;
        private static readonly Button[,] _buttons = new Button[_cellSize, _cellSize];
        private static bool _firstTurn = true;
        private static int _countStep;

        private static void Menu(Form current)
        {
            var menu = new MenuStrip();
            var restart = menu.Items.Add("Restart");
            current.Controls.Add(menu);
            restart.Click += (sender, args) => Restart(current);
        }
        private static void SetButton(Form current)
        {
            Menu(current);
            SetGameField(current);
        }

        private static void Restart(Form current)
        {
            _countStep = 0;
            foreach (Control button in current.Controls)
            {
                button.Enabled = true;
                button.Text = "";
                button.BackColor = Color.White;
            }
        }

        private static void SetGameField(Form current)
        {
            for (int i = 0; i < _sizeMap; i++)
            {
                for (int j = 0; j < _sizeMap; j++)
                {
                    var button = new Button();
                    button.Location = new Point(j * _cellSize, i * _cellSize + _menuHeight);
                    button.Size = new Size(_cellSize, _cellSize);
                    button.Text = "-";
                    button.BackColor = Color.White;
                    button.Font = new Font("Microsoft Sans Serif", 40F, FontStyle.Bold, GraphicsUnit.Point);
                    current.Controls.Add(button);
                    _buttons[i, j] = button;
                    button.Click += (sender, args) => ClickOnButton(current, button);
                }
            }
        }

        private static void ClickOnButton(Form current, Button pressedButton)
        {
            var playerX = Player.X.ToString();
            var playerO = Player.O.ToString();

            var step = _firstTurn ? pressedButton.Text = playerX : pressedButton.Text = playerO;
            pressedButton.Enabled = false;
            _firstTurn = !_firstTurn;
            switch (pressedButton.Text)
            {
                case "X":
                    CheckWinner(current, playerX);
                    break;
                case "O":
                    CheckWinner(current, playerO);
                    break;
            }
        }
        private static void CheckWinner(Form current, string player)
        {
            _countStep++;
            if (_buttons[0, 0].Text == player && _buttons[0, 1].Text == player && _buttons[0, 2].Text == player ||
                _buttons[1, 0].Text == player && _buttons[1, 1].Text == player && _buttons[1, 2].Text == player ||
                _buttons[2, 0].Text == player && _buttons[2, 1].Text == player && _buttons[2, 2].Text == player ||
                _buttons[0, 0].Text == player && _buttons[1, 0].Text == player && _buttons[2, 0].Text == player ||
                _buttons[0, 1].Text == player && _buttons[1, 1].Text == player && _buttons[2, 1].Text == player ||
                _buttons[0, 2].Text == player && _buttons[1, 2].Text == player && _buttons[2, 2].Text == player ||
                _buttons[0, 0].Text == player && _buttons[1, 1].Text == player && _buttons[2, 2].Text == player ||
                _buttons[0, 2].Text == player && _buttons[1, 1].Text == player && _buttons[2, 0].Text == player)
            { 
                MessageBox.Show(player + " You Win!!!");
                DisableButtons(current);
            }
            else if (_countStep == 9)
            {
                MessageBox.Show("DRAW!");
            }
        }
        private static void DisableButtons(Form current)
        {
            foreach (Control button in current.Controls)
            {
                if (button.Text.Contains("-"))
                    button.Enabled = false; 
            }
        }
        public static void Run(Form current)
        {
            current.ClientSize = new Size(_cellSize * _sizeMap, _cellSize * _sizeMap + _menuHeight);
            current.FormBorderStyle = FormBorderStyle.FixedDialog;
            current.MaximizeBox = false;
            SetButton(current);
        }
    }
}
