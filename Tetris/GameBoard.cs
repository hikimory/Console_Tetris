using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Shapes;

namespace Tetris
{
    internal class GameBoard
    {
        private readonly int[][] _board;
        private int _width, _height;
        private int _offsetX, _offsetY;

        public GameBoard(int width, int height, int offsetX, int offsetY)
        {
            _width = width;
            _height = height;
            _offsetX = offsetX;
            _offsetY = offsetY;

            _board = new int[_height][];
            for (int i = 0; i < _height; i++)
            {
                _board[i] = new int[_width];
            }
        }

        public void RemoveFullRows()
        {
            int lineCount = 0;
            for (int i = 0; i < _board.Length; i++)
            {
                if (IsRowFull(i))
                {
                    lineCount++;
                    RemoveRow(i);
                    ShiftRowsDown(i);
                }
            }
            if(lineCount > 0)
            {
                RedrawShapes();
                int score = CalculateScore(lineCount);
                Game.UpdateScore(score);
            }
        }

        private int CalculateScore(int lineCount)
        {
            int points = lineCount * 10;

            switch (lineCount)
            {
                case 2:
                    points += 20;
                    break;
                case 3:
                    points += 40;
                    break;
                case 4:
                    points += 80;
                    break;
            }
            return points;
        }

        private void RemoveRow(int row)
        {
            for (int i = 0; i < _board[row].Length; i++)
                _board[row][i] = 0;
        }

        private void ShiftRowsDown(int row)
        {
            for (int i = row; i > 0; i--)
                _board[i] = _board[i - 1];

            _board[0] = new int[_board[0].Length];
        }

        private void RedrawShapes()
        {
            for (int row = 0; row < _board.Length; row++)
            {
                for (int col = 0; col < _board[row].Length; col++)
                {
                    if (_board[row][col] == 0)
                        PrintSymbols("[]", ConsoleColor.Black, row, col);
                    else
                        PrintSymbols("  ", (ConsoleColor)_board[row][col], row, col);
                }
            }
        }

        public void DrawBoard()
        {
            DrawBorder();
            DrawCells();
        }

        private void DrawBorder()
        {
            string horizontal = "─", vertical = "│";
            string ul_corner = "┌", ur_corner = "┐";
            string ll_corner = "└", lr_corner = "┘";

            Console.SetCursorPosition(_offsetX, _offsetY);
            Console.Write(ul_corner);
            Console.SetCursorPosition(_width * 2 + _offsetX + 1, _offsetY);
            Console.Write(ur_corner);

            Console.SetCursorPosition(_offsetX + 1, _offsetY);
            Console.Write(horizontal.Repeat(_width * 2));

            for (int i = _offsetY; i < _height + _offsetY; i++)
            {
                Console.SetCursorPosition(_offsetX, i + 1);
                Console.Write(vertical);
                Console.SetCursorPosition(_width * 2 + _offsetX + 1, i + 1);
                Console.Write(vertical);
            }

            Console.SetCursorPosition(_offsetX, _height + _offsetY + 1);
            Console.Write(ll_corner);
            Console.SetCursorPosition(_width * 2 + _offsetX + 1, _height + _offsetY + 1);
            Console.Write(lr_corner);
            Console.SetCursorPosition(_offsetX + 1, _height + _offsetY + 1);
            Console.Write(horizontal.Repeat(_width * 2));
        }

        private void DrawCells()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < _height; i++)
            {
                Utilities.PlaceCursor(_offsetX, i + _offsetY);
                Console.Write("[]".Repeat(_width));
            }
        }

        private void PrintSymbols(string symbols, ConsoleColor color, int row, int col)
        {
            Console.BackgroundColor = color;
            Utilities.PlaceCursor(col, row);
            Console.Write(symbols);
        }

        public void AddShape(Shape shape)
        {
            foreach (Point point in shape.Dots)
                _board[point.Y][point.X] = ((int)shape.Color);
        }

        public bool Contains(Shape shape)
        {
            foreach (Point point in shape.Dots)
                if (_board[point.Y][point.X] != 0) return true;
            
            return false;
        }

        private bool IsRowFull(int row)
        {
            for (int i = 0; i < _board[row].Length; i++)
                if (_board[row][i] == 0) return false;

            return true;
        }

        public bool IsGameOver()
        {
            for (int i = 0; i < _board[1].Length; i++)
                if (_board[1][i] != 0) return true;

            return false;
        }
    }
}
