using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Tetris.Shapes;

namespace Tetris
{
    internal static class Game
    {
        internal static int Width { get; set; } = 10;
        internal static int Height { get; set; } = 20;
        internal static int FallTime { get; set; } = 1000;
        internal static float SpeedUp { get; set; } = 0.8f;
        internal static int Score { get; set; } = 0;
        internal static bool IsDrawing { get; set; } = false;
        internal static int Level { get; set; } = 0;
        internal static GameBoard Board { get; set; }

        private static Shape _currentShape;
        private static Shape _nextShape;

        private static bool _isDoneControl = false;
        private static System.Timers.Timer _timer;


        internal static int StartGame()
        {
            InitGame();
            _nextShape = ShapesHandler.GetRandomShape();
            DrawGameStats();
            Board.DrawBoard();
            do
            {
                InstantShape();
                Draw_nextShape();
                MoveShape();
                Board.RemoveFullRows();
            }
            while (Board.IsGameOver() == false);
            _timer.Dispose();
            return Score;
        }

        internal static void InitGame()
        {
            Level = 1;
            Board = new(Width, Height, 0, 0);
            _timer = new() { AutoReset = true, Interval = FallTime * SpeedUp * Level };
            _timer.Elapsed += OnTimerElapsed;
        }

        internal static void UpdateScore(int score)
        {
            Score += score;
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string result = Score.ToString();
            Console.SetCursorPosition(Width * 2 + 20 - result.Length, 7);
            Console.Write(result);
            Console.ForegroundColor = prevColor;
            CheckScore();
        }

        internal static void CheckScore()
        {
            if (Score % 50 == 0)
            {
                Level++;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(Width * 2 + 13, 11);
                Console.Write($"{Level}");
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
        }

        internal static void DrawGameStats()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(Width * 2 + 5, 1);
            Console.Write("NEXT");
            Console.SetCursorPosition(Width * 2 + 5, 5);
            Console.Write("SCORE");
            Console.SetCursorPosition(Width * 2 + 5, 9);
            Console.Write("LEVEL");
            Console.SetCursorPosition(Width * 2 + 13, 11);
            Console.Write("1");

            Console.SetCursorPosition(Width * 2 + 5, 13);
            Console.Write("MOVE        ROTATE");
            Console.SetCursorPosition(Width * 2 + 4, 15);
            Console.Write("<   >         A - LEFT");
            Console.SetCursorPosition(Width * 2 + 6, 16);
            Console.Write("v           D - RIGHT");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(Width * 2 + 8, 7);
            Console.Write("000000000000");
        }

        internal static void Draw_nextShape()
        {
            ShapesHandler.DisplayToSide(_nextShape);
        }

        internal static void MoveShape()
        {
            _isDoneControl = false;
            _timer.Start();

            while (_isDoneControl == false)
            {
                while (Console.KeyAvailable == false)
                {
                    if (_isDoneControl) return;
                };

                var keyInfo = Console.ReadKey(true);
                Input(keyInfo);
            }
        }

        internal static void InstantShape()
        {
            _currentShape = _nextShape;
            _nextShape = ShapesHandler.GetRandomShape();
            ShapesHandler.DrawFirst(_currentShape);
        }

        static void Input(ConsoleKeyInfo keyInfo)
        {
            if (IsDrawing) return;
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    ShapesHandler.MoveShapeLeft(ref _currentShape);
                    break;
                case ConsoleKey.DownArrow:
                    if (ShapesHandler.MoveShapeDown(ref _currentShape) == false)
                        StopControl();
                    break;
                case ConsoleKey.RightArrow:
                    ShapesHandler.MoveShapeRight(ref _currentShape);
                    break;
                case ConsoleKey.A:
                    ShapesHandler.RotateShapeLeft(ref _currentShape);
                    break;
                case ConsoleKey.D:
                    ShapesHandler.RotateShapeRight(ref _currentShape);
                    break;
                case ConsoleKey.Escape:
                    _isDoneControl = true;
                    break;
                default:
                    break;
            }
        }

        static void StopControl()
        {
            _timer.Stop();
            Board.AddShape(_currentShape);
            _isDoneControl = true;
        }

        private static void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            if (IsDrawing) return;
            if (ShapesHandler.MoveShapeDown(ref _currentShape) == false)
                StopControl();
        }

    }
}
